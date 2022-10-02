// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.NameValueWithParametersHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class NameValueWithParametersHeaderValue : NameValueHeaderValue, ICloneable
  {
    private List<NameValueHeaderValue> parameters;

    protected NameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue source)
      : base((NameValueHeaderValue) source)
    {
      if (source.parameters == null)
        return;
      foreach (NameValueHeaderValue parameter in source.parameters)
        this.Parameters.Add(parameter);
    }

    private NameValueWithParametersHeaderValue()
    {
    }

    public ICollection<NameValueHeaderValue> Parameters => (ICollection<NameValueHeaderValue>) this.parameters ?? (ICollection<NameValueHeaderValue>) (this.parameters = new List<NameValueHeaderValue>());

    object ICloneable.Clone() => (object) new NameValueWithParametersHeaderValue(this);

    public override bool Equals(object obj) => obj is NameValueWithParametersHeaderValue parametersHeaderValue && base.Equals(obj) && parametersHeaderValue.parameters.SequenceEqual<NameValueHeaderValue>(this.parameters);

    public override int GetHashCode() => base.GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>((ICollection<NameValueHeaderValue>) this.parameters);

    public override string ToString() => this.parameters == null || this.parameters.Count == 0 ? base.ToString() : base.ToString() + this.parameters.ToString<NameValueHeaderValue>();

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<NameValueWithParametersHeaderValue> result)
    {
      return CollectionParser.TryParse<NameValueWithParametersHeaderValue>(input, minimalCount, new ElementTryParser<NameValueWithParametersHeaderValue>(NameValueWithParametersHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out NameValueWithParametersHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (NameValueWithParametersHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      ref NameValueWithParametersHeaderValue local = ref parsedValue;
      NameValueWithParametersHeaderValue parametersHeaderValue = new NameValueWithParametersHeaderValue();
      parametersHeaderValue.Name = lexer.GetStringValue(t);
      local = parametersHeaderValue;
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorEqual)
      {
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.Token && (Token.Type) t != Token.Type.QuotedString)
          return false;
        parsedValue.value = lexer.GetStringValue(t);
        t = lexer.Scan();
      }
      if ((Token.Type) t == Token.Type.SeparatorSemicolon)
      {
        List<NameValueHeaderValue> result;
        if (!NameValueHeaderValue.TryParseParameters(lexer, out result, out t))
          return false;
        parsedValue.parameters = result;
      }
      return true;
    }
  }
}
