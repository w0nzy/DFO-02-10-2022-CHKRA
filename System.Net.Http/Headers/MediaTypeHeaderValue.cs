// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.MediaTypeHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class MediaTypeHeaderValue : ICloneable
  {
    internal List<NameValueHeaderValue> parameters;
    internal string media_type;

    public MediaTypeHeaderValue(string mediaType) => this.MediaType = mediaType;

    protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
    {
      this.media_type = source != null ? source.media_type : throw new ArgumentNullException(nameof (source));
      if (source.parameters == null)
        return;
      foreach (NameValueHeaderValue parameter in source.parameters)
        this.Parameters.Add(new NameValueHeaderValue(parameter));
    }

    internal MediaTypeHeaderValue()
    {
    }

    public string CharSet
    {
      get
      {
        if (this.parameters == null)
          return (string) null;
        return this.parameters.Find((Predicate<NameValueHeaderValue>) (l => string.Equals(l.Name, "charset", StringComparison.OrdinalIgnoreCase)))?.Value;
      }
    }

    public string MediaType
    {
      set
      {
        string media;
        Token? nullable = value != null ? MediaTypeHeaderValue.TryParseMediaType(new Lexer(value), out media) : throw new ArgumentNullException(nameof (MediaType));
        if (!nullable.HasValue || nullable.Value.Kind != Token.Type.End)
          throw new FormatException();
        this.media_type = media;
      }
    }

    public ICollection<NameValueHeaderValue> Parameters => (ICollection<NameValueHeaderValue>) this.parameters ?? (ICollection<NameValueHeaderValue>) (this.parameters = new List<NameValueHeaderValue>());

    object ICloneable.Clone() => (object) new MediaTypeHeaderValue(this);

    public override bool Equals(object obj) => obj is MediaTypeHeaderValue mediaTypeHeaderValue && string.Equals(mediaTypeHeaderValue.media_type, this.media_type, StringComparison.OrdinalIgnoreCase) && mediaTypeHeaderValue.parameters.SequenceEqual<NameValueHeaderValue>(this.parameters);

    public override int GetHashCode() => this.media_type.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>((ICollection<NameValueHeaderValue>) this.parameters);

    public override string ToString() => this.parameters == null ? this.media_type : this.media_type + this.parameters.ToString<NameValueHeaderValue>();

    public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
    {
      parsedValue = (MediaTypeHeaderValue) null;
      Lexer lexer = new Lexer(input);
      List<NameValueHeaderValue> result = (List<NameValueHeaderValue>) null;
      string media;
      Token? mediaType = MediaTypeHeaderValue.TryParseMediaType(lexer, out media);
      if (!mediaType.HasValue)
        return false;
      switch (mediaType.Value.Kind)
      {
        case Token.Type.End:
          parsedValue = new MediaTypeHeaderValue()
          {
            media_type = media,
            parameters = result
          };
          return true;
        case Token.Type.SeparatorSemicolon:
          Token t;
          if (!NameValueHeaderValue.TryParseParameters(lexer, out result, out t) || (Token.Type) t != Token.Type.End)
            return false;
          goto case Token.Type.End;
        default:
          return false;
      }
    }

    internal static Token? TryParseMediaType(Lexer lexer, out string media)
    {
      media = (string) null;
      Token token1 = lexer.Scan();
      if ((Token.Type) token1 != Token.Type.Token)
        return new Token?();
      if ((Token.Type) lexer.Scan() != Token.Type.SeparatorSlash)
        return new Token?();
      Token token2 = lexer.Scan();
      if ((Token.Type) token2 != Token.Type.Token)
        return new Token?();
      media = lexer.GetStringValue(token1) + "/" + lexer.GetStringValue(token2);
      return new Token?(lexer.Scan());
    }
  }
}
