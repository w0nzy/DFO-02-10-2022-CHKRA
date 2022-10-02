// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HttpContentHeaders
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

namespace System.Net.Http.Headers
{
  public sealed class HttpContentHeaders : HttpHeaders
  {
    private readonly HttpContent content;

    internal HttpContentHeaders(HttpContent content)
      : base(HttpHeaderKind.Content)
    {
      this.content = content;
    }

    public long? ContentLength
    {
      get
      {
        long? contentLength = this.GetValue<long?>("Content-Length");
        if (contentLength.HasValue)
          return contentLength;
        long? loadedBufferLength = this.content.LoadedBufferLength;
        if (loadedBufferLength.HasValue)
          return loadedBufferLength;
        long length;
        if (!this.content.TryComputeLength(out length))
          return new long?();
        this.SetValue<long>("Content-Length", length);
        return new long?(length);
      }
    }

    public MediaTypeHeaderValue ContentType
    {
      get => this.GetValue<MediaTypeHeaderValue>("Content-Type");
      set => this.AddOrRemove<MediaTypeHeaderValue>("Content-Type", value);
    }
  }
}
