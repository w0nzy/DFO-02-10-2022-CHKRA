// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.RangeItemHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

namespace System.Net.Http.Headers
{
  public class RangeItemHeaderValue : ICloneable
  {
    public RangeItemHeaderValue(long? from, long? to)
    {
      if (!from.HasValue && !to.HasValue)
        throw new ArgumentException();
      if (from.HasValue && to.HasValue)
      {
        long? nullable1 = from;
        long? nullable2 = to;
        if ((nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? (nullable1.HasValue & nullable2.HasValue ? 1 : 0) : 0) != 0)
          throw new ArgumentOutOfRangeException(nameof (from));
      }
      long? nullable3 = from;
      long num1 = 0;
      if ((nullable3.GetValueOrDefault() < num1 ? (nullable3.HasValue ? 1 : 0) : 0) != 0)
        throw new ArgumentOutOfRangeException(nameof (from));
      long? nullable4 = to;
      long num2 = 0;
      if ((nullable4.GetValueOrDefault() < num2 ? (nullable4.HasValue ? 1 : 0) : 0) != 0)
        throw new ArgumentOutOfRangeException(nameof (to));
      this.From = from;
      this.To = to;
    }

    public long? From { get; private set; }

    public long? To { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (obj is RangeItemHeaderValue rangeItemHeaderValue)
      {
        long? from = rangeItemHeaderValue.From;
        long? nullable = this.From;
        if ((from.GetValueOrDefault() == nullable.GetValueOrDefault() ? (from.HasValue == nullable.HasValue ? 1 : 0) : 0) != 0)
        {
          nullable = rangeItemHeaderValue.To;
          long? to = this.To;
          return nullable.GetValueOrDefault() == to.GetValueOrDefault() && nullable.HasValue == to.HasValue;
        }
      }
      return false;
    }

    public override int GetHashCode()
    {
      long? nullable = this.From;
      int hashCode1 = nullable.GetHashCode();
      nullable = this.To;
      int hashCode2 = nullable.GetHashCode();
      return hashCode1 ^ hashCode2;
    }

    public override string ToString()
    {
      if (!this.From.HasValue)
        return "-" + (object) this.To.Value;
      return !this.To.HasValue ? this.From.Value.ToString() + "-" : this.From.Value.ToString() + "-" + (object) this.To.Value;
    }
  }
}
