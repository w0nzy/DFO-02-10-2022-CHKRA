// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpClient
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public class HttpClient : HttpMessageInvoker
  {
    private static readonly TimeSpan TimeoutDefault = TimeSpan.FromSeconds(100.0);
    private System.Uri base_address;
    private CancellationTokenSource cts;
    private bool disposed;
    private HttpRequestHeaders headers;
    private long buffer_size;
    private TimeSpan timeout;

    public HttpClient()
      : this(HttpClient.GetDefaultHandler(), true)
    {
    }

    private static HttpMessageHandler GetDefaultHandler()
    {
      System.Type type = System.Type.GetType("Android.Runtime.AndroidEnvironment, Mono.Android");
      if (type == (System.Type) null)
        return HttpClient.GetFallback("Invalid Mono.Android assembly? Cannot find Android.Runtime.AndroidEnvironment");
      MethodInfo method = type.GetMethod("GetHttpMessageHandler", BindingFlags.Static | BindingFlags.NonPublic);
      if (method == (MethodInfo) null)
        return HttpClient.GetFallback("Your Xamarin.Android version does not support obtaining of the custom HttpClientHandler");
      object obj = method.Invoke((object) null, (object[]) null);
      if (obj == null)
        return HttpClient.GetFallback("Xamarin.Android returned no custom HttpClientHandler");
      return !(obj is HttpMessageHandler httpMessageHandler) ? HttpClient.GetFallback(string.Format("{0} is not a valid HttpMessageHandler", (object) obj?.GetType())) : httpMessageHandler;
    }

    private static HttpMessageHandler GetFallback(string message)
    {
      Console.WriteLine(message + ". Defaulting to System.Net.Http.HttpClientHandler");
      return (HttpMessageHandler) new HttpClientHandler();
    }

    public HttpClient(HttpMessageHandler handler, bool disposeHandler)
      : base(handler, disposeHandler)
    {
      this.buffer_size = (long) int.MaxValue;
      this.timeout = HttpClient.TimeoutDefault;
      this.cts = new CancellationTokenSource();
    }

    public HttpRequestHeaders DefaultRequestHeaders => this.headers ?? (this.headers = new HttpRequestHeaders());

    public long MaxResponseContentBufferSize => this.buffer_size;

    protected override void Dispose(bool disposing)
    {
      if (disposing && !this.disposed)
      {
        this.disposed = true;
        this.cts.Dispose();
      }
      base.Dispose(disposing);
    }

    public Task<HttpResponseMessage> GetAsync(string requestUri) => this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));

    public Task<HttpResponseMessage> GetAsync(
      System.Uri requestUri,
      CancellationToken cancellationToken)
    {
      return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
    }

    public Task<HttpResponseMessage> PostAsync(
      string requestUri,
      HttpContent content)
    {
      return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
      {
        Content = content
      });
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => this.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);

    public override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
    }

    public Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      HttpCompletionOption completionOption,
      CancellationToken cancellationToken)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      System.Uri relativeUri = !request.SetIsUsed() ? request.RequestUri : throw new InvalidOperationException("Cannot send the same request message multiple times");
      if (relativeUri == (System.Uri) null)
        request.RequestUri = !(this.base_address == (System.Uri) null) ? this.base_address : throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
      else if (!relativeUri.IsAbsoluteUri || relativeUri.Scheme == System.Uri.UriSchemeFile && relativeUri.OriginalString.StartsWith("/", StringComparison.Ordinal))
        request.RequestUri = !(this.base_address == (System.Uri) null) ? new System.Uri(this.base_address, relativeUri) : throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
      if (this.headers != null)
        request.Headers.AddHeaders(this.headers);
      return this.SendAsyncWorker(request, completionOption, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendAsyncWorker(
      HttpRequestMessage request,
      HttpCompletionOption completionOption,
      CancellationToken cancellationToken)
    {
      HttpResponseMessage httpResponseMessage;
      using (CancellationTokenSource lcts = CancellationTokenSource.CreateLinkedTokenSource(this.cts.Token, cancellationToken))
      {
        lcts.CancelAfter(this.timeout);
        Task<HttpResponseMessage> task = base.SendAsync(request, lcts.Token);
        if (task == null)
          throw new InvalidOperationException("Handler failed to return a value");
        HttpResponseMessage response = await task.ConfigureAwait(false);
        if (response == null)
          throw new InvalidOperationException("Handler failed to return a response");
        if (response.Content != null && (completionOption & HttpCompletionOption.ResponseHeadersRead) == HttpCompletionOption.ResponseContentRead)
          await response.Content.LoadIntoBufferAsync(this.MaxResponseContentBufferSize).ConfigureAwait(false);
        httpResponseMessage = response;
      }
      return httpResponseMessage;
    }
  }
}
