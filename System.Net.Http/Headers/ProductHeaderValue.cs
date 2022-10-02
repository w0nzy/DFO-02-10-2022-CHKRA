// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.ProductHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class ProductHeaderValue : ICloneable
  {
    internal ProductHeaderValue()
    {
    }

    public string Name { get; internal set; }

    public string Version { get; internal set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj) => obj is ProductHeaderValue productHeaderValue && string.Equals(productHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(productHeaderValue.Version, this.Version, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
    {
      int hashCode = this.Name.ToLowerInvariant().GetHashCode();
      if (this.Version != null)
        hashCode ^= this.Version.ToLowerInvariant().GetHashCode();
      return hashCode;
    }

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<ProductHeaderValue> result)
    {
      return CollectionParser.TryParse<ProductHeaderValue>(input, minimalCount, new ElementTryParser<ProductHeaderValue>(ProductHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out ProductHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (ProductHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      parsedValue = new ProductHeaderValue();
      parsedValue.Name = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorSlash)
      {
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.Token)
          return false;
        parsedValue.Version = lexer.GetStringValue(t);
        t = lexer.Scan();
      }
      return true;
    }

    public override string ToString() => this.Version != null ? this.Name + "/" + this.Version : this.Name;
  }
}
