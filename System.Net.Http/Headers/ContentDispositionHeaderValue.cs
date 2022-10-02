// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.ContentDispositionHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class ContentDispositionHeaderValue : ICloneable
  {
    private string dispositionType;
    private List<NameValueHeaderValue> parameters;

    private ContentDispositionHeaderValue()
    {
    }

    protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
    {
      this.dispositionType = source != null ? source.dispositionType : throw new ArgumentNullException(nameof (source));
      if (source.parameters == null)
        return;
      foreach (NameValueHeaderValue parameter in source.parameters)
        this.Parameters.Add(new NameValueHeaderValue(parameter));
    }

    public ICollection<NameValueHeaderValue> Parameters => (ICollection<NameValueHeaderValue>) this.parameters ?? (ICollection<NameValueHeaderValue>) (this.parameters = new List<NameValueHeaderValue>());

    object ICloneable.Clone() => (object) new ContentDispositionHeaderValue(this);

    public override bool Equals(object obj) => obj is ContentDispositionHeaderValue dispositionHeaderValue && string.Equals(dispositionHeaderValue.dispositionType, this.dispositionType, StringComparison.OrdinalIgnoreCase) && dispositionHeaderValue.parameters.SequenceEqual<NameValueHeaderValue>(this.parameters);

    public override int GetHashCode() => this.dispositionType.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>((ICollection<NameValueHeaderValue>) this.parameters);

    public override string ToString() => this.dispositionType + this.parameters.ToString<NameValueHeaderValue>();

    public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
    {
      parsedValue = (ContentDispositionHeaderValue) null;
      Lexer lexer = new Lexer(input);
      Token token = lexer.Scan();
      if (token.Kind != Token.Type.Token)
        return false;
      List<NameValueHeaderValue> result = (List<NameValueHeaderValue>) null;
      string stringValue = lexer.GetStringValue(token);
      Token t = lexer.Scan();
      switch (t.Kind)
      {
        case Token.Type.End:
          parsedValue = new ContentDispositionHeaderValue()
          {
            dispositionType = stringValue,
            parameters = result
          };
          return true;
        case Token.Type.SeparatorSemicolon:
          if (!NameValueHeaderValue.TryParseParameters(lexer, out result, out t) || (Token.Type) t != Token.Type.End)
            return false;
          goto case Token.Type.End;
        default:
          return false;
      }
    }
  }
}
