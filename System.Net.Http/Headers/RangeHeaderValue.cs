// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.RangeHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Text;

namespace System.Net.Http.Headers
{
  public class RangeHeaderValue : ICloneable
  {
    private List<RangeItemHeaderValue> ranges;
    private string unit;

    public RangeHeaderValue() => this.unit = "bytes";

    private RangeHeaderValue(RangeHeaderValue source)
      : this()
    {
      if (source.ranges == null)
        return;
      foreach (RangeItemHeaderValue range in source.ranges)
        this.Ranges.Add(range);
    }

    public ICollection<RangeItemHeaderValue> Ranges => (ICollection<RangeItemHeaderValue>) this.ranges ?? (ICollection<RangeItemHeaderValue>) (this.ranges = new List<RangeItemHeaderValue>());

    public string Unit => this.unit;

    object ICloneable.Clone() => (object) new RangeHeaderValue(this);

    public override bool Equals(object obj) => obj is RangeHeaderValue rangeHeaderValue && string.Equals(rangeHeaderValue.Unit, this.Unit, StringComparison.OrdinalIgnoreCase) && rangeHeaderValue.ranges.SequenceEqual<RangeItemHeaderValue>(this.ranges);

    public override int GetHashCode() => this.Unit.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<RangeItemHeaderValue>((ICollection<RangeItemHeaderValue>) this.ranges);

    public static bool TryParse(string input, out RangeHeaderValue parsedValue)
    {
      parsedValue = (RangeHeaderValue) null;
      Lexer lexer = new Lexer(input);
      Token token1 = lexer.Scan();
      if ((Token.Type) token1 != Token.Type.Token)
        return false;
      RangeHeaderValue rangeHeaderValue = new RangeHeaderValue();
      rangeHeaderValue.unit = lexer.GetStringValue(token1);
      if ((Token.Type) lexer.Scan() != Token.Type.SeparatorEqual)
        return false;
      Token token2;
      do
      {
        long? from = new long?();
        long? to = new long?();
        bool flag = false;
        token2 = lexer.Scan(true);
        long result;
        switch (token2.Kind)
        {
          case Token.Type.Token:
            string stringValue = lexer.GetStringValue(token2);
            string[] strArray = stringValue.Split(new char[1]
            {
              '-'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (!Parser.Long.TryParse(strArray[0], out result))
              return false;
            long? nullable1;
            long? nullable2;
            switch (strArray.Length)
            {
              case 1:
                token2 = lexer.Scan(true);
                from = new long?(result);
                switch (token2.Kind)
                {
                  case Token.Type.End:
                    if (stringValue.Length > 0 && stringValue[stringValue.Length - 1] != '-')
                      return false;
                    flag = true;
                    break;
                  case Token.Type.SeparatorDash:
                    token2 = lexer.Scan();
                    if ((Token.Type) token2 != Token.Type.Token)
                    {
                      flag = true;
                      break;
                    }
                    if (!lexer.TryGetNumericValue(token2, out result))
                      return false;
                    to = new long?(result);
                    nullable1 = to;
                    nullable2 = from;
                    if ((nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? (nullable1.HasValue & nullable2.HasValue ? 1 : 0) : 0) != 0)
                      return false;
                    break;
                  case Token.Type.SeparatorComma:
                    flag = true;
                    break;
                  default:
                    return false;
                }
                break;
              case 2:
                from = new long?(result);
                if (!Parser.Long.TryParse(strArray[1], out result))
                  return false;
                to = new long?(result);
                nullable2 = to;
                nullable1 = from;
                if ((nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? (nullable2.HasValue & nullable1.HasValue ? 1 : 0) : 0) != 0)
                  return false;
                break;
              default:
                return false;
            }
            break;
          case Token.Type.SeparatorDash:
            token2 = lexer.Scan();
            if (!lexer.TryGetNumericValue(token2, out result))
              return false;
            to = new long?(result);
            break;
          default:
            return false;
        }
        rangeHeaderValue.Ranges.Add(new RangeItemHeaderValue(from, to));
        if (!flag)
          token2 = lexer.Scan();
      }
      while ((Token.Type) token2 == Token.Type.SeparatorComma);
      if ((Token.Type) token2 != Token.Type.End)
        return false;
      parsedValue = rangeHeaderValue;
      return true;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.unit);
      stringBuilder.Append("=");
      for (int index = 0; index < this.Ranges.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append((object) this.ranges[index]);
      }
      return stringBuilder.ToString();
    }
  }
}
