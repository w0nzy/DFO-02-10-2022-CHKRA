// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpMethod
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Net.Http.Headers;

namespace System.Net.Http
{
  public class HttpMethod : IEquatable<HttpMethod>
  {
    private static readonly HttpMethod delete_method = new HttpMethod("DELETE");
    private static readonly HttpMethod get_method = new HttpMethod("GET");
    private static readonly HttpMethod head_method = new HttpMethod("HEAD");
    private static readonly HttpMethod options_method = new HttpMethod("OPTIONS");
    private static readonly HttpMethod post_method = new HttpMethod("POST");
    private static readonly HttpMethod put_method = new HttpMethod("PUT");
    private static readonly HttpMethod trace_method = new HttpMethod("TRACE");
    private readonly string method;

    public HttpMethod(string method)
    {
      if (string.IsNullOrEmpty(method))
        throw new ArgumentException(nameof (method));
      Parser.Token.Check(method);
      this.method = method;
    }

    public static HttpMethod Get => HttpMethod.get_method;

    public string Method => this.method;

    public static HttpMethod Post => HttpMethod.post_method;

    public static bool operator ==(HttpMethod left, HttpMethod right) => (object) left == null || (object) right == null ? (object) left == (object) right : left.Equals(right);

    public bool Equals(HttpMethod other) => string.Equals(this.method, other.method, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object obj)
    {
      HttpMethod other = obj as HttpMethod;
      return (object) other != null && this.Equals(other);
    }

    public override int GetHashCode() => this.method.GetHashCode();

    public override string ToString() => this.method;
  }
}
