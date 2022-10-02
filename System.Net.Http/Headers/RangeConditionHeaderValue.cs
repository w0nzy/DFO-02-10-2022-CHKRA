// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.RangeConditionHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Globalization;

namespace System.Net.Http.Headers
{
  public class RangeConditionHeaderValue : ICloneable
  {
    public RangeConditionHeaderValue(DateTimeOffset date) => this.Date = new DateTimeOffset?(date);

    public RangeConditionHeaderValue(EntityTagHeaderValue entityTag) => this.EntityTag = entityTag != null ? entityTag : throw new ArgumentNullException(nameof (entityTag));

    public DateTimeOffset? Date { get; private set; }

    public EntityTagHeaderValue EntityTag { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (!(obj is RangeConditionHeaderValue conditionHeaderValue))
        return false;
      if (this.EntityTag != null)
        return this.EntityTag.Equals((object) conditionHeaderValue.EntityTag);
      DateTimeOffset? date1 = this.Date;
      DateTimeOffset? date2 = conditionHeaderValue.Date;
      if (date1.HasValue != date2.HasValue)
        return false;
      return !date1.HasValue || date1.GetValueOrDefault() == date2.GetValueOrDefault();
    }

    public override int GetHashCode() => this.EntityTag == null ? this.Date.GetHashCode() : this.EntityTag.GetHashCode();

    public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
    {
      parsedValue = (RangeConditionHeaderValue) null;
      Lexer lexer = new Lexer(input);
      Token token = lexer.Scan();
      bool flag;
      if ((Token.Type) token == Token.Type.Token)
      {
        if (lexer.GetStringValue(token) != "W")
        {
          DateTimeOffset date;
          if (!Lexer.TryGetDateValue(input, out date))
            return false;
          parsedValue = new RangeConditionHeaderValue(date);
          return true;
        }
        if (lexer.PeekChar() != 47)
          return false;
        flag = true;
        lexer.EatChar();
        token = lexer.Scan();
      }
      else
        flag = false;
      if ((Token.Type) token != Token.Type.QuotedString || (Token.Type) lexer.Scan() != Token.Type.End)
        return false;
      parsedValue = new RangeConditionHeaderValue(new EntityTagHeaderValue()
      {
        Tag = lexer.GetStringValue(token),
        IsWeak = flag
      });
      return true;
    }

    public override string ToString() => this.EntityTag != null ? this.EntityTag.ToString() : this.Date.Value.ToString("r", (IFormatProvider) CultureInfo.InvariantCulture);
  }
}
