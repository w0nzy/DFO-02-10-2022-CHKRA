// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpRequestMessage
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
  public class HttpRequestMessage : IDisposable
  {
    private HttpRequestHeaders headers;
    private HttpMethod method;
    private Version version;
    private System.Uri uri;
    private bool is_used;
    private bool disposed;

    public HttpRequestMessage(HttpMethod method, string requestUri)
      : this(method, string.IsNullOrEmpty(requestUri) ? (System.Uri) null : new System.Uri(requestUri, UriKind.RelativeOrAbsolute))
    {
    }

    public HttpRequestMessage(HttpMethod method, System.Uri requestUri)
    {
      this.Method = method;
      this.RequestUri = requestUri;
    }

    public HttpContent Content { get; set; }

    public HttpRequestHeaders Headers => this.headers ?? (this.headers = new HttpRequestHeaders());

    public HttpMethod Method
    {
      get => this.method;
      set => this.method = !(value == (HttpMethod) null) ? value : throw new ArgumentNullException("method");
    }

    public System.Uri RequestUri
    {
      get => this.uri;
      set => this.uri = !(value != (System.Uri) null) || !value.IsAbsoluteUri || HttpRequestMessage.IsAllowedAbsoluteUri(value) ? value : throw new ArgumentException("Only http or https scheme is allowed");
    }

    private static bool IsAllowedAbsoluteUri(System.Uri uri) => uri.Scheme == System.Uri.UriSchemeHttp || uri.Scheme == System.Uri.UriSchemeHttps || uri.Scheme == System.Uri.UriSchemeFile && uri.OriginalString.StartsWith("/", StringComparison.Ordinal);

    public Version Version
    {
      get
      {
        Version version = this.version;
        return (object) version != null ? version : HttpVersion.Version11;
      }
    }

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.disposed)
        return;
      this.disposed = true;
      if (this.Content == null)
        return;
      this.Content.Dispose();
    }

    internal bool SetIsUsed()
    {
      if (this.is_used)
        return true;
      this.is_used = true;
      return false;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Method: ").Append((object) this.method);
      stringBuilder.Append(", RequestUri: '").Append(this.RequestUri != (System.Uri) null ? this.RequestUri.ToString() : "<null>");
      stringBuilder.Append("', Version: ").Append((object) this.Version);
      stringBuilder.Append(", Content: ").Append(this.Content != null ? this.Content.ToString() : "<null>");
      stringBuilder.Append(", Headers:\r\n{\r\n").Append((object) this.Headers);
      if (this.Content != null)
        stringBuilder.Append((object) this.Content.Headers);
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
