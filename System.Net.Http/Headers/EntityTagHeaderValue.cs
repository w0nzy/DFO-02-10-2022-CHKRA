// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.EntityTagHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class EntityTagHeaderValue : ICloneable
  {
    private static readonly EntityTagHeaderValue any = new EntityTagHeaderValue()
    {
      Tag = "*"
    };

    internal EntityTagHeaderValue()
    {
    }

    public bool IsWeak { get; internal set; }

    public string Tag { get; internal set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj) => obj is EntityTagHeaderValue entityTagHeaderValue && entityTagHeaderValue.Tag == this.Tag && string.Equals(entityTagHeaderValue.Tag, this.Tag, StringComparison.Ordinal);

    public override int GetHashCode() => this.IsWeak.GetHashCode() ^ this.Tag.GetHashCode();

    public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
    {
      Token t;
      if (EntityTagHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out t) && (Token.Type) t == Token.Type.End)
        return true;
      parsedValue = (EntityTagHeaderValue) null;
      return false;
    }

    private static bool TryParseElement(
      Lexer lexer,
      out EntityTagHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (EntityTagHeaderValue) null;
      t = lexer.Scan();
      bool flag = false;
      if ((Token.Type) t == Token.Type.Token)
      {
        string stringValue = lexer.GetStringValue(t);
        if (stringValue == "*")
        {
          parsedValue = EntityTagHeaderValue.any;
          t = lexer.Scan();
          return true;
        }
        if (stringValue != "W" || lexer.PeekChar() != 47)
          return false;
        flag = true;
        lexer.EatChar();
        t = lexer.Scan();
      }
      if ((Token.Type) t != Token.Type.QuotedString)
        return false;
      parsedValue = new EntityTagHeaderValue();
      parsedValue.Tag = lexer.GetStringValue(t);
      parsedValue.IsWeak = flag;
      t = lexer.Scan();
      return true;
    }

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<EntityTagHeaderValue> result)
    {
      return CollectionParser.TryParse<EntityTagHeaderValue>(input, minimalCount, new ElementTryParser<EntityTagHeaderValue>(EntityTagHeaderValue.TryParseElement), out result);
    }

    public override string ToString() => !this.IsWeak ? this.Tag : "W/" + this.Tag;
  }
}
