// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.WarningHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
  public class WarningHeaderValue : ICloneable
  {
    private WarningHeaderValue()
    {
    }

    public string Agent { get; private set; }

    public int Code { get; private set; }

    public DateTimeOffset? Date { get; private set; }

    public string Text { get; private set; }

    private static bool IsCodeValid(int code) => code >= 0 && code < 1000;

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (!(obj is WarningHeaderValue warningHeaderValue) || this.Code != warningHeaderValue.Code || !string.Equals(warningHeaderValue.Agent, this.Agent, StringComparison.OrdinalIgnoreCase) || !(this.Text == warningHeaderValue.Text))
        return false;
      DateTimeOffset? date1 = this.Date;
      DateTimeOffset? date2 = warningHeaderValue.Date;
      if (date1.HasValue != date2.HasValue)
        return false;
      return !date1.HasValue || date1.GetValueOrDefault() == date2.GetValueOrDefault();
    }

    public override int GetHashCode() => this.Code.GetHashCode() ^ this.Agent.ToLowerInvariant().GetHashCode() ^ this.Text.GetHashCode() ^ this.Date.GetHashCode();

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<WarningHeaderValue> result)
    {
      return CollectionParser.TryParse<WarningHeaderValue>(input, minimalCount, new ElementTryParser<WarningHeaderValue>(WarningHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out WarningHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (WarningHeaderValue) null;
      t = lexer.Scan();
      int code;
      if ((Token.Type) t != Token.Type.Token || !lexer.TryGetNumericValue(t, out code) || !WarningHeaderValue.IsCodeValid(code))
        return false;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      Token end = t;
      if (lexer.PeekChar() == 58)
      {
        lexer.EatChar();
        end = lexer.Scan();
        if ((Token.Type) end != Token.Type.Token)
          return false;
      }
      WarningHeaderValue warningHeaderValue = new WarningHeaderValue();
      warningHeaderValue.Code = code;
      warningHeaderValue.Agent = lexer.GetStringValue(t, end);
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.QuotedString)
        return false;
      warningHeaderValue.Text = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.QuotedString)
      {
        DateTimeOffset dateTimeOffset;
        if (!lexer.TryGetDateValue(t, out dateTimeOffset))
          return false;
        warningHeaderValue.Date = new DateTimeOffset?(dateTimeOffset);
        t = lexer.Scan();
      }
      parsedValue = warningHeaderValue;
      return true;
    }

    public override string ToString()
    {
      string str = this.Code.ToString("000") + " " + this.Agent + " " + this.Text;
      if (this.Date.HasValue)
        str = str + " \"" + this.Date.Value.ToString("r", (IFormatProvider) CultureInfo.InvariantCulture) + "\"";
      return str;
    }
  }
}
