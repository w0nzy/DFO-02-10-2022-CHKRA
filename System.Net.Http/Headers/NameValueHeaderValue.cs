// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.NameValueHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class NameValueHeaderValue : ICloneable
  {
    internal string value;

    protected internal NameValueHeaderValue(NameValueHeaderValue source)
    {
      this.Name = source.Name;
      this.value = source.value;
    }

    internal NameValueHeaderValue()
    {
    }

    public string Name { get; internal set; }

    public string Value => this.value;

    internal static NameValueHeaderValue Create(string name, string value) => new NameValueHeaderValue()
    {
      Name = name,
      value = value
    };

    object ICloneable.Clone() => (object) new NameValueHeaderValue(this);

    public override int GetHashCode()
    {
      int hashCode = this.Name.ToLowerInvariant().GetHashCode();
      if (!string.IsNullOrEmpty(this.value))
        hashCode ^= this.value.ToLowerInvariant().GetHashCode();
      return hashCode;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is NameValueHeaderValue valueHeaderValue) || !string.Equals(valueHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase))
        return false;
      return string.IsNullOrEmpty(this.value) ? string.IsNullOrEmpty(valueHeaderValue.value) : string.Equals(valueHeaderValue.value, this.value, StringComparison.OrdinalIgnoreCase);
    }

    internal static bool TryParsePragma(
      string input,
      int minimalCount,
      out List<NameValueHeaderValue> result)
    {
      return CollectionParser.TryParse<NameValueHeaderValue>(input, minimalCount, new ElementTryParser<NameValueHeaderValue>(NameValueHeaderValue.TryParseElement), out result);
    }

    internal static bool TryParseParameters(
      Lexer lexer,
      out List<NameValueHeaderValue> result,
      out Token t)
    {
      List<NameValueHeaderValue> valueHeaderValueList = new List<NameValueHeaderValue>();
      result = (List<NameValueHeaderValue>) null;
      do
      {
        Token token = lexer.Scan();
        if ((Token.Type) token != Token.Type.Token)
        {
          t = Token.Empty;
          return false;
        }
        string str = (string) null;
        t = lexer.Scan();
        if ((Token.Type) t == Token.Type.SeparatorEqual)
        {
          t = lexer.Scan();
          if ((Token.Type) t != Token.Type.Token && (Token.Type) t != Token.Type.QuotedString)
            return false;
          str = lexer.GetStringValue(t);
          t = lexer.Scan();
        }
        valueHeaderValueList.Add(new NameValueHeaderValue()
        {
          Name = lexer.GetStringValue(token),
          value = str
        });
      }
      while ((Token.Type) t == Token.Type.SeparatorSemicolon);
      result = valueHeaderValueList;
      return true;
    }

    public override string ToString() => string.IsNullOrEmpty(this.value) ? this.Name : this.Name + "=" + this.value;

    private static bool TryParseElement(
      Lexer lexer,
      out NameValueHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (NameValueHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      parsedValue = new NameValueHeaderValue()
      {
        Name = lexer.GetStringValue(t)
      };
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorEqual)
      {
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.Token && (Token.Type) t != Token.Type.QuotedString)
          return false;
        parsedValue.value = lexer.GetStringValue(t);
        t = lexer.Scan();
      }
      return true;
    }
  }
}
