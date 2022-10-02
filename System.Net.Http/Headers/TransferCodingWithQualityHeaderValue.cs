// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.TransferCodingWithQualityHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue
  {
    private TransferCodingWithQualityHeaderValue()
    {
    }

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<TransferCodingWithQualityHeaderValue> result)
    {
      return CollectionParser.TryParse<TransferCodingWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<TransferCodingWithQualityHeaderValue>(TransferCodingWithQualityHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out TransferCodingWithQualityHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (TransferCodingWithQualityHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      TransferCodingWithQualityHeaderValue qualityHeaderValue = new TransferCodingWithQualityHeaderValue();
      qualityHeaderValue.value = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out qualityHeaderValue.parameters, out t) || (Token.Type) t != Token.Type.End))
        return false;
      parsedValue = qualityHeaderValue;
      return true;
    }
  }
}
