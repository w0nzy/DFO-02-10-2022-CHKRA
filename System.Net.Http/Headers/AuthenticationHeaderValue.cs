// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.AuthenticationHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class AuthenticationHeaderValue : ICloneable
  {
    public AuthenticationHeaderValue(string scheme, string parameter)
    {
      Parser.Token.Check(scheme);
      this.Scheme = scheme;
      this.Parameter = parameter;
    }

    private AuthenticationHeaderValue()
    {
    }

    public string Parameter { get; private set; }

    public string Scheme { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj) => obj is AuthenticationHeaderValue authenticationHeaderValue && string.Equals(authenticationHeaderValue.Scheme, this.Scheme, StringComparison.OrdinalIgnoreCase) && authenticationHeaderValue.Parameter == this.Parameter;

    public override int GetHashCode()
    {
      int hashCode = this.Scheme.ToLowerInvariant().GetHashCode();
      if (!string.IsNullOrEmpty(this.Parameter))
        hashCode ^= this.Parameter.ToLowerInvariant().GetHashCode();
      return hashCode;
    }

    public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
    {
      Token t;
      if (AuthenticationHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out t) && (Token.Type) t == Token.Type.End)
        return true;
      parsedValue = (AuthenticationHeaderValue) null;
      return false;
    }

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<AuthenticationHeaderValue> result)
    {
      return CollectionParser.TryParse<AuthenticationHeaderValue>(input, minimalCount, new ElementTryParser<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out AuthenticationHeaderValue parsedValue,
      out Token t)
    {
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
      {
        parsedValue = (AuthenticationHeaderValue) null;
        return false;
      }
      parsedValue = new AuthenticationHeaderValue();
      parsedValue.Scheme = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.Token)
      {
        parsedValue.Parameter = lexer.GetRemainingStringValue(t.StartPosition);
        t = new Token(Token.Type.End, 0, 0);
      }
      return true;
    }

    public override string ToString() => this.Parameter == null ? this.Scheme : this.Scheme + " " + this.Parameter;
  }
}
