// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HttpRequestHeaders
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public sealed class HttpRequestHeaders : HttpHeaders
  {
    private bool? expectContinue;

    internal HttpRequestHeaders()
      : base(HttpHeaderKind.Request)
    {
    }

    public AuthenticationHeaderValue Authorization
    {
      set => this.AddOrRemove<AuthenticationHeaderValue>(nameof (Authorization), value);
    }

    public HttpHeaderValueCollection<string> Connection => this.GetValues<string>(nameof (Connection));

    public bool? ConnectionClose
    {
      get
      {
        bool? connectionclose = this.connectionclose;
        bool flag = true;
        return (connectionclose.GetValueOrDefault() == flag ? (connectionclose.HasValue ? 1 : 0) : 0) != 0 || this.Connection.Find((Predicate<string>) (l => string.Equals(l, "close", StringComparison.OrdinalIgnoreCase))) != null ? new bool?(true) : this.connectionclose;
      }
    }

    internal bool ConnectionKeepAlive => this.Connection.Find((Predicate<string>) (l => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase))) != null;

    public bool? ExpectContinue
    {
      get
      {
        if (this.expectContinue.HasValue)
          return this.expectContinue;
        return this.TransferEncoding.Find((Predicate<TransferCodingHeaderValue>) (l => string.Equals(l.Value, "100-continue", StringComparison.OrdinalIgnoreCase))) == null ? new bool?() : new bool?(true);
      }
    }

    public string Host => this.GetValue<string>(nameof (Host));

    public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding => this.GetValues<TransferCodingHeaderValue>("Transfer-Encoding");

    public bool? TransferEncodingChunked
    {
      get
      {
        if (this.transferEncodingChunked.HasValue)
          return this.transferEncodingChunked;
        return this.TransferEncoding.Find((Predicate<TransferCodingHeaderValue>) (l => string.Equals(l.Value, "chunked", StringComparison.OrdinalIgnoreCase))) == null ? new bool?() : new bool?(true);
      }
    }

    internal void AddHeaders(HttpRequestHeaders headers)
    {
      foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) headers)
        this.TryAddWithoutValidation(header.Key, header.Value);
    }
  }
}
