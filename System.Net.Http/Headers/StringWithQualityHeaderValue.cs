// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.StringWithQualityHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
  public class StringWithQualityHeaderValue : ICloneable
  {
    private StringWithQualityHeaderValue()
    {
    }

    public double? Quality { get; private set; }

    public string Value { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (!(obj is StringWithQualityHeaderValue qualityHeaderValue) || !string.Equals(qualityHeaderValue.Value, this.Value, StringComparison.OrdinalIgnoreCase))
        return false;
      double? quality1 = qualityHeaderValue.Quality;
      double? quality2 = this.Quality;
      return quality1.GetValueOrDefault() == quality2.GetValueOrDefault() && quality1.HasValue == quality2.HasValue;
    }

    public override int GetHashCode() => this.Value.ToLowerInvariant().GetHashCode() ^ this.Quality.GetHashCode();

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<StringWithQualityHeaderValue> result)
    {
      return CollectionParser.TryParse<StringWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<StringWithQualityHeaderValue>(StringWithQualityHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out StringWithQualityHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (StringWithQualityHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      StringWithQualityHeaderValue qualityHeaderValue = new StringWithQualityHeaderValue();
      qualityHeaderValue.Value = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorSemicolon)
      {
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.Token)
          return false;
        string stringValue = lexer.GetStringValue(t);
        if (stringValue != "q" && stringValue != "Q")
          return false;
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.SeparatorEqual)
          return false;
        t = lexer.Scan();
        double num;
        if (!lexer.TryGetDoubleValue(t, out num) || num > 1.0)
          return false;
        qualityHeaderValue.Quality = new double?(num);
        t = lexer.Scan();
      }
      parsedValue = qualityHeaderValue;
      return true;
    }

    public override string ToString() => this.Quality.HasValue ? this.Value + "; q=" + this.Quality.Value.ToString("0.0##", (IFormatProvider) CultureInfo.InvariantCulture) : this.Value;
  }
}
