// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.ContentRangeHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
  public class ContentRangeHeaderValue : ICloneable
  {
    private string unit = "bytes";

    private ContentRangeHeaderValue()
    {
    }

    public long? From { get; private set; }

    public long? Length { get; private set; }

    public long? To { get; private set; }

    public string Unit => this.unit;

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (!(obj is ContentRangeHeaderValue rangeHeaderValue))
        return false;
      long? nullable1 = rangeHeaderValue.Length;
      long? nullable2 = this.Length;
      if ((nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? (nullable1.HasValue == nullable2.HasValue ? 1 : 0) : 0) != 0)
      {
        nullable2 = rangeHeaderValue.From;
        nullable1 = this.From;
        if ((nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? (nullable2.HasValue == nullable1.HasValue ? 1 : 0) : 0) != 0)
        {
          nullable1 = rangeHeaderValue.To;
          nullable2 = this.To;
          if ((nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? (nullable1.HasValue == nullable2.HasValue ? 1 : 0) : 0) != 0)
            return string.Equals(rangeHeaderValue.unit, this.unit, StringComparison.OrdinalIgnoreCase);
        }
      }
      return false;
    }

    public override int GetHashCode() => this.Unit.GetHashCode() ^ this.Length.GetHashCode() ^ this.From.GetHashCode() ^ this.To.GetHashCode() ^ this.unit.ToLowerInvariant().GetHashCode();

    public static bool TryParse(string input, out ContentRangeHeaderValue parsedValue)
    {
      parsedValue = (ContentRangeHeaderValue) null;
      Lexer lexer = new Lexer(input);
      Token token1 = lexer.Scan();
      if ((Token.Type) token1 != Token.Type.Token)
        return false;
      ContentRangeHeaderValue rangeHeaderValue = new ContentRangeHeaderValue();
      rangeHeaderValue.unit = lexer.GetStringValue(token1);
      Token token2 = lexer.Scan();
      if ((Token.Type) token2 != Token.Type.Token)
        return false;
      if (!lexer.IsStarStringValue(token2))
      {
        long result;
        if (!lexer.TryGetNumericValue(token2, out result))
        {
          string stringValue = lexer.GetStringValue(token2);
          if (stringValue.Length < 3)
            return false;
          string[] strArray = stringValue.Split('-', StringSplitOptions.None);
          if (strArray.Length != 2 || !long.TryParse(strArray[0], NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out result))
            return false;
          rangeHeaderValue.From = new long?(result);
          if (!long.TryParse(strArray[1], NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out result))
            return false;
          rangeHeaderValue.To = new long?(result);
        }
        else
        {
          rangeHeaderValue.From = new long?(result);
          if ((Token.Type) lexer.Scan(true) != Token.Type.SeparatorDash)
            return false;
          Token token3 = lexer.Scan();
          if (!lexer.TryGetNumericValue(token3, out result))
            return false;
          rangeHeaderValue.To = new long?(result);
        }
      }
      if ((Token.Type) lexer.Scan() != Token.Type.SeparatorSlash)
        return false;
      Token token4 = lexer.Scan();
      if (!lexer.IsStarStringValue(token4))
      {
        long num;
        if (!lexer.TryGetNumericValue(token4, out num))
          return false;
        rangeHeaderValue.Length = new long?(num);
      }
      if ((Token.Type) lexer.Scan() != Token.Type.End)
        return false;
      parsedValue = rangeHeaderValue;
      return true;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder1 = new StringBuilder(this.unit);
      stringBuilder1.Append(" ");
      long num;
      if (!this.From.HasValue)
      {
        stringBuilder1.Append("*");
      }
      else
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        num = this.From.Value;
        string str1 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder2.Append(str1);
        stringBuilder1.Append("-");
        StringBuilder stringBuilder3 = stringBuilder1;
        num = this.To.Value;
        string str2 = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder3.Append(str2);
      }
      stringBuilder1.Append("/");
      StringBuilder stringBuilder4 = stringBuilder1;
      string str;
      if (this.Length.HasValue)
      {
        num = this.Length.Value;
        str = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
        str = "*";
      stringBuilder4.Append(str);
      return stringBuilder1.ToString();
    }
  }
}
