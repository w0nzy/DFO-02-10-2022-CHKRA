// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.RetryConditionHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Globalization;

namespace System.Net.Http.Headers
{
  public class RetryConditionHeaderValue : ICloneable
  {
    public RetryConditionHeaderValue(DateTimeOffset date) => this.Date = new DateTimeOffset?(date);

    public RetryConditionHeaderValue(TimeSpan delta) => this.Delta = delta.TotalSeconds <= (double) uint.MaxValue ? new TimeSpan?(delta) : throw new ArgumentOutOfRangeException(nameof (delta));

    public DateTimeOffset? Date { get; private set; }

    public TimeSpan? Delta { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (obj is RetryConditionHeaderValue conditionHeaderValue)
      {
        DateTimeOffset? date1 = conditionHeaderValue.Date;
        DateTimeOffset? date2 = this.Date;
        if ((date1.HasValue == date2.HasValue ? (date1.HasValue ? (date1.GetValueOrDefault() == date2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          TimeSpan? delta1 = conditionHeaderValue.Delta;
          TimeSpan? delta2 = this.Delta;
          if (delta1.HasValue != delta2.HasValue)
            return false;
          return !delta1.HasValue || delta1.GetValueOrDefault() == delta2.GetValueOrDefault();
        }
      }
      return false;
    }

    public override int GetHashCode() => this.Date.GetHashCode() ^ this.Delta.GetHashCode();

    public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
    {
      parsedValue = (RetryConditionHeaderValue) null;
      Lexer lexer = new Lexer(input);
      Token token = lexer.Scan();
      if ((Token.Type) token != Token.Type.Token)
        return false;
      TimeSpan? timeSpanValue = lexer.TryGetTimeSpanValue(token);
      if (timeSpanValue.HasValue)
      {
        if ((Token.Type) lexer.Scan() != Token.Type.End)
          return false;
        parsedValue = new RetryConditionHeaderValue(timeSpanValue.Value);
      }
      else
      {
        DateTimeOffset date;
        if (!Lexer.TryGetDateValue(input, out date))
          return false;
        parsedValue = new RetryConditionHeaderValue(date);
      }
      return true;
    }

    public override string ToString() => !this.Delta.HasValue ? this.Date.Value.ToString("r", (IFormatProvider) CultureInfo.InvariantCulture) : this.Delta.Value.TotalSeconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);
  }
}
