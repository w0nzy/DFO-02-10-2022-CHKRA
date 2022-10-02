// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpClientHandler
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public class HttpClientHandler : HttpMessageHandler
  {
    private static long groupCounter;
    private bool allowAutoRedirect;
    private DecompressionMethods automaticDecompression;
    private CookieContainer cookieContainer;
    private ICredentials credentials;
    private int maxAutomaticRedirections;
    private long maxRequestContentBufferSize;
    private bool preAuthenticate;
    private IWebProxy proxy;
    private bool useCookies;
    private bool useDefaultCredentials;
    private bool useProxy;
    private bool sentRequest;
    private string connectionGroupName;
    private bool disposed;

    public HttpClientHandler()
    {
      this.allowAutoRedirect = true;
      this.maxAutomaticRedirections = 50;
      this.maxRequestContentBufferSize = (long) int.MaxValue;
      this.useCookies = true;
      this.useProxy = true;
      this.connectionGroupName = nameof (HttpClientHandler) + (object) Interlocked.Increment(ref HttpClientHandler.groupCounter);
    }

    public CookieContainer CookieContainer => this.cookieContainer ?? (this.cookieContainer = new CookieContainer());

    public long MaxRequestContentBufferSize => this.maxRequestContentBufferSize;

    protected override void Dispose(bool disposing)
    {
      if (disposing && !this.disposed)
      {
        Volatile.Write(ref this.disposed, true);
        ServicePointManager.CloseConnectionGroup(this.connectionGroupName);
      }
      base.Dispose(disposing);
    }

    internal virtual HttpWebRequest CreateWebRequest(HttpRequestMessage request)
    {
      HttpWebRequest webRequest = new HttpWebRequest(request.RequestUri);
      webRequest.ThrowOnError = false;
      webRequest.AllowWriteStreamBuffering = false;
      webRequest.ConnectionGroupName = this.connectionGroupName;
      webRequest.Method = request.Method.Method;
      webRequest.ProtocolVersion = request.Version;
      if (webRequest.ProtocolVersion == HttpVersion.Version10)
      {
        webRequest.KeepAlive = request.Headers.ConnectionKeepAlive;
      }
      else
      {
        HttpWebRequest httpWebRequest = webRequest;
        bool? connectionClose = request.Headers.ConnectionClose;
        bool flag = true;
        int num = connectionClose.GetValueOrDefault() == flag ? (!connectionClose.HasValue ? 1 : 0) : 1;
        httpWebRequest.KeepAlive = num != 0;
      }
      if (this.allowAutoRedirect)
      {
        webRequest.AllowAutoRedirect = true;
        webRequest.MaximumAutomaticRedirections = this.maxAutomaticRedirections;
      }
      else
        webRequest.AllowAutoRedirect = false;
      webRequest.AutomaticDecompression = this.automaticDecompression;
      webRequest.PreAuthenticate = this.preAuthenticate;
      if (this.useCookies)
        webRequest.CookieContainer = this.CookieContainer;
      if (this.useDefaultCredentials)
        webRequest.UseDefaultCredentials = true;
      else
        webRequest.Credentials = this.credentials;
      if (this.useProxy)
        webRequest.Proxy = this.proxy;
      else
        webRequest.Proxy = (IWebProxy) null;
      ServicePoint servicePoint = webRequest.ServicePoint;
      bool? expectContinue = request.Headers.ExpectContinue;
      bool flag1 = true;
      int num1 = expectContinue.GetValueOrDefault() == flag1 ? (expectContinue.HasValue ? 1 : 0) : 0;
      servicePoint.Expect100Continue = num1 != 0;
      WebHeaderCollection headers = webRequest.Headers;
      foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) request.Headers)
      {
        IEnumerable<string> strings = header.Value;
        if (header.Key == "Host")
        {
          webRequest.Host = request.Headers.Host;
        }
        else
        {
          if (header.Key == "Transfer-Encoding")
            strings = strings.Where<string>((Func<string, bool>) (l => l != "chunked"));
          string singleHeaderString = HttpHeaders.GetSingleHeaderString(header.Key, strings);
          if (singleHeaderString != null)
            headers.AddInternal(header.Key, singleHeaderString);
        }
      }
      return webRequest;
    }

    private HttpResponseMessage CreateResponseMessage(
      HttpWebResponse wr,
      HttpRequestMessage requestMessage,
      CancellationToken cancellationToken)
    {
      HttpResponseMessage responseMessage = new HttpResponseMessage(wr.StatusCode);
      responseMessage.RequestMessage = requestMessage;
      responseMessage.ReasonPhrase = wr.StatusDescription;
      responseMessage.Content = (HttpContent) new StreamContent(wr.GetResponseStream(), cancellationToken);
      WebHeaderCollection headers = wr.Headers;
      for (int index = 0; index < headers.Count; ++index)
      {
        string key = headers.GetKey(index);
        string[] values = headers.GetValues(index);
        (HttpHeaders.GetKnownHeaderKind(key) != HttpHeaderKind.Content ? (HttpHeaders) responseMessage.Headers : (HttpHeaders) responseMessage.Content.Headers).TryAddWithoutValidation(key, (IEnumerable<string>) values);
      }
      requestMessage.RequestUri = wr.ResponseUri;
      return responseMessage;
    }

    private static bool MethodHasBody(HttpMethod method)
    {
      string method1 = method.Method;
      return !(method1 == "HEAD") && !(method1 == "GET") && !(method1 == "MKCOL") && !(method1 == "CONNECT") && !(method1 == "TRACE");
    }

    protected internal override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      HttpClientHandler httpClientHandler = this;
      if (httpClientHandler.disposed)
        throw new ObjectDisposedException(httpClientHandler.GetType().ToString());
      Volatile.Write(ref httpClientHandler.sentRequest, true);
      HttpWebRequest wrequest = httpClientHandler.CreateWebRequest(request);
      HttpWebResponse wresponse = (HttpWebResponse) null;
      try
      {
        CancellationTokenRegistration tokenRegistration = cancellationToken.Register((Action<object>) (l => ((WebRequest) l).Abort()), (object) wrequest);
        try
        {
          HttpContent content = request.Content;
          if (content != null)
          {
            WebHeaderCollection headers = wrequest.Headers;
            foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) content.Headers)
            {
              foreach (string str in header.Value)
                headers.AddInternal(header.Key, str);
            }
            bool? transferEncodingChunked = request.Headers.TransferEncodingChunked;
            bool flag = true;
            ConfiguredTaskAwaitable configuredTaskAwaitable;
            if ((transferEncodingChunked.GetValueOrDefault() == flag ? (transferEncodingChunked.HasValue ? 1 : 0) : 0) != 0)
            {
              wrequest.SendChunked = true;
            }
            else
            {
              long? contentLength = content.Headers.ContentLength;
              if (contentLength.HasValue)
              {
                wrequest.ContentLength = contentLength.Value;
              }
              else
              {
                if (httpClientHandler.MaxRequestContentBufferSize == 0L)
                  throw new InvalidOperationException("The content length of the request content can't be determined. Either set TransferEncodingChunked to true, load content into buffer, or set MaxRequestContentBufferSize.");
                configuredTaskAwaitable = content.LoadIntoBufferAsync(httpClientHandler.MaxRequestContentBufferSize).ConfigureAwait(false);
                await configuredTaskAwaitable;
                wrequest.ContentLength = content.Headers.ContentLength.Value;
              }
            }
            wrequest.ResendContentFactory = new Func<Stream, Task>(content.CopyToAsync);
            using (Stream stream = await wrequest.GetRequestStreamAsync().ConfigureAwait(false))
            {
              configuredTaskAwaitable = request.Content.CopyToAsync(stream).ConfigureAwait(false);
              await configuredTaskAwaitable;
            }
          }
          else if (HttpClientHandler.MethodHasBody(request.Method))
            wrequest.ContentLength = 0L;
          wresponse = (HttpWebResponse) await wrequest.GetResponseAsync().ConfigureAwait(false);
          content = (HttpContent) null;
        }
        finally
        {
          tokenRegistration.Dispose();
        }
        tokenRegistration = new CancellationTokenRegistration();
      }
      catch (WebException ex)
      {
        if (ex.Status != WebExceptionStatus.RequestCanceled)
          throw new HttpRequestException("An error occurred while sending the request", (Exception) ex);
      }
      catch (IOException ex)
      {
        throw new HttpRequestException("An error occurred while sending the request", (Exception) ex);
      }
      if (!cancellationToken.IsCancellationRequested)
        return httpClientHandler.CreateResponseMessage(wresponse, request, cancellationToken);
      TaskCompletionSource<HttpResponseMessage> completionSource = new TaskCompletionSource<HttpResponseMessage>();
      completionSource.SetCanceled();
      return await completionSource.Task;
    }
  }
}
