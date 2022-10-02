// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.ProductInfoHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class ProductInfoHeaderValue : ICloneable
  {
    public ProductInfoHeaderValue(ProductHeaderValue product) => this.Product = product != null ? product : throw new ArgumentNullException();

    private ProductInfoHeaderValue()
    {
    }

    public string Comment { get; private set; }

    public ProductHeaderValue Product { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj)
    {
      if (!(obj is ProductInfoHeaderValue productInfoHeaderValue))
        return false;
      return this.Product == null ? productInfoHeaderValue.Comment == this.Comment : this.Product.Equals((object) productInfoHeaderValue.Product);
    }

    public override int GetHashCode() => this.Product == null ? this.Comment.GetHashCode() : this.Product.GetHashCode();

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<ProductInfoHeaderValue> result)
    {
      List<ProductInfoHeaderValue> productInfoHeaderValueList = new List<ProductInfoHeaderValue>();
      Lexer lexer = new Lexer(input);
      result = (List<ProductInfoHeaderValue>) null;
      ProductInfoHeaderValue parsedValue;
      while (ProductInfoHeaderValue.TryParseElement(lexer, out parsedValue))
      {
        if (parsedValue == null)
        {
          if (productInfoHeaderValueList == null || minimalCount > productInfoHeaderValueList.Count)
            return false;
          result = productInfoHeaderValueList;
          return true;
        }
        productInfoHeaderValueList.Add(parsedValue);
        switch (lexer.PeekChar())
        {
          case -1:
            if (minimalCount <= productInfoHeaderValueList.Count)
            {
              result = productInfoHeaderValueList;
              return true;
            }
            break;
          case 9:
          case 32:
            lexer.EatChar();
            continue;
        }
        return false;
      }
      return false;
    }

    private static bool TryParseElement(Lexer lexer, out ProductInfoHeaderValue parsedValue)
    {
      parsedValue = (ProductInfoHeaderValue) null;
      string str;
      Token readToken;
      if (lexer.ScanCommentOptional(out str, out readToken))
      {
        if (str == null)
          return false;
        parsedValue = new ProductInfoHeaderValue();
        parsedValue.Comment = str;
        return true;
      }
      if ((Token.Type) readToken == Token.Type.End)
        return true;
      if ((Token.Type) readToken != Token.Type.Token)
        return false;
      ProductHeaderValue product = new ProductHeaderValue();
      product.Name = lexer.GetStringValue(readToken);
      int position = lexer.Position;
      if ((Token.Type) lexer.Scan() == Token.Type.SeparatorSlash)
      {
        Token token = lexer.Scan();
        if ((Token.Type) token != Token.Type.Token)
          return false;
        product.Version = lexer.GetStringValue(token);
      }
      else
        lexer.Position = position;
      parsedValue = new ProductInfoHeaderValue(product);
      return true;
    }

    public override string ToString() => this.Product == null ? this.Comment : this.Product.ToString();
  }
}
