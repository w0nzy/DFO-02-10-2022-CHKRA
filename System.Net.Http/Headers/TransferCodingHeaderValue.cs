// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.TransferCodingHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class TransferCodingHeaderValue : ICloneable
  {
    internal string value;
    internal List<NameValueHeaderValue> parameters;

    protected TransferCodingHeaderValue(TransferCodingHeaderValue source)
    {
      this.value = source.value;
      if (source.parameters == null)
        return;
      foreach (NameValueHeaderValue parameter in source.parameters)
        this.Parameters.Add(new NameValueHeaderValue(parameter));
    }

    internal TransferCodingHeaderValue()
    {
    }

    public ICollection<NameValueHeaderValue> Parameters => (ICollection<NameValueHeaderValue>) this.parameters ?? (ICollection<NameValueHeaderValue>) (this.parameters = new List<NameValueHeaderValue>());

    public string Value => this.value;

    object ICloneable.Clone() => (object) new TransferCodingHeaderValue(this);

    public override bool Equals(object obj) => obj is TransferCodingHeaderValue codingHeaderValue && string.Equals(this.value, codingHeaderValue.value, StringComparison.OrdinalIgnoreCase) && this.parameters.SequenceEqual<NameValueHeaderValue>(codingHeaderValue.parameters);

    public override int GetHashCode()
    {
      int hashCode = this.value.ToLowerInvariant().GetHashCode();
      if (this.parameters != null)
        hashCode ^= HashCodeCalculator.Calculate<NameValueHeaderValue>((ICollection<NameValueHeaderValue>) this.parameters);
      return hashCode;
    }

    public override string ToString() => this.value + this.parameters.ToString<NameValueHeaderValue>();

    internal static bool TryParse(
      string input,
      int minimalCount,
      out List<TransferCodingHeaderValue> result)
    {
      return CollectionParser.TryParse<TransferCodingHeaderValue>(input, minimalCount, new ElementTryParser<TransferCodingHeaderValue>(TransferCodingHeaderValue.TryParseElement), out result);
    }

    private static bool TryParseElement(
      Lexer lexer,
      out TransferCodingHeaderValue parsedValue,
      out Token t)
    {
      parsedValue = (TransferCodingHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      TransferCodingHeaderValue codingHeaderValue = new TransferCodingHeaderValue();
      codingHeaderValue.value = lexer.GetStringValue(t);
      t = lexer.Scan();
      if ((Token.Type) t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out codingHeaderValue.parameters, out t) || (Token.Type) t != Token.Type.End))
        return false;
      parsedValue = codingHeaderValue;
      return true;
    }
  }
}
