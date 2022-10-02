// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.CollectionParser
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  internal static class CollectionParser
  {
    public static bool TryParse<T>(
      string input,
      int minimalCount,
      ElementTryParser<T> parser,
      out List<T> result)
      where T : class
    {
      Lexer lexer = new Lexer(input);
      result = new List<T>();
      T parsedValue;
      Token token;
      while (parser(lexer, out parsedValue, out token))
      {
        if ((object) parsedValue != null)
          result.Add(parsedValue);
        if ((Token.Type) token != Token.Type.SeparatorComma)
        {
          if ((Token.Type) token == Token.Type.End)
          {
            if (minimalCount <= result.Count)
              return true;
            result = (List<T>) null;
            return false;
          }
          result = (List<T>) null;
          return false;
        }
      }
      return false;
    }

    public static bool TryParse(string input, int minimalCount, out List<string> result) => CollectionParser.TryParse<string>(input, minimalCount, new ElementTryParser<string>(CollectionParser.TryParseStringElement), out result);

    private static bool TryParseStringElement(Lexer lexer, out string parsedValue, out Token t)
    {
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.Token)
      {
        parsedValue = lexer.GetStringValue(t);
        if (parsedValue.Length == 0)
          parsedValue = (string) null;
        t = lexer.Scan();
      }
      else
        parsedValue = (string) null;
      return true;
    }
  }
}
