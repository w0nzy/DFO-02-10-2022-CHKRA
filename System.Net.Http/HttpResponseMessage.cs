// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpResponseMessage
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
  public class HttpResponseMessage : IDisposable
  {
    private HttpResponseHeaders headers;
    private string reasonPhrase;
    private HttpStatusCode statusCode;
    private Version version;
    private bool disposed;

    public HttpResponseMessage(HttpStatusCode statusCode) => this.StatusCode = statusCode;

    public HttpContent Content { get; set; }

    public HttpResponseHeaders Headers => this.headers ?? (this.headers = new HttpResponseHeaders());

    public bool IsSuccessStatusCode => this.statusCode >= HttpStatusCode.OK && this.statusCode < HttpStatusCode.MultipleChoices;

    public string ReasonPhrase
    {
      get => this.reasonPhrase ?? HttpStatusDescription.Get(this.statusCode);
      set => this.reasonPhrase = value;
    }

    public HttpRequestMessage RequestMessage
    {
      set => this.\u003CRequestMessage\u003Ek__BackingField = value;
    }

    public HttpStatusCode StatusCode
    {
      get => this.statusCode;
      set => this.statusCode = value >= (HttpStatusCode) 0 ? value : throw new ArgumentOutOfRangeException();
    }

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

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StatusCode: ").Append((int) this.StatusCode);
      stringBuilder.Append(", ReasonPhrase: '").Append(this.ReasonPhrase ?? "<null>");
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
