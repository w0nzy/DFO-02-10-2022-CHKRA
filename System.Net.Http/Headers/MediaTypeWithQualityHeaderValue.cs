// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue
  {
    private MediaTypeWithQualityHeaderValue()
    {
    }

    private static bool TryParseElement(
      Lexer lexer,
      out MediaTypeWithQualityHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (MediaTypeWithQualityHeaderValue) null;
      List<NameValueHeaderValue> result = (List<NameValueHeaderValue>) null;
      string media;
      Token? mediaType = MediaTypeHeaderValue.TryParseMediaType(lexer, out media);
      if (!mediaType.HasValue)
      {
        t = Token.Empty;
        return false;
      }
      t = mediaType.Value;
      if ((Token.Type) t == Token.Type.SeparatorSemicolon && !NameValueHeaderValue.TryParseParameters(lexer, out result, out t))
        return false;
      parsedValue = new MediaTypeWithQualityHeaderValue();
      parsedValue.media_type = media;
      parsedValue.parameters = result;
      return true;
    }

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<MediaTypeWithQualityHeaderValue> result)
    {
      return CollectionParser.TryParse<MediaTypeWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<MediaTypeWithQualityHeaderValue>(MediaTypeWithQualityHeaderValue.TryParseElement), out result);
    }
  }
}
