// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.ViaHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public class ViaHeaderValue : ICloneable
  {
    private ViaHeaderValue()
    {
    }

    public string Comment { get; private set; }

    public string ProtocolName { get; private set; }

    public string ProtocolVersion { get; private set; }

    public string ReceivedBy { get; private set; }

    object ICloneable.Clone() => this.MemberwiseClone();

    public override bool Equals(object obj) => obj is ViaHeaderValue viaHeaderValue && string.Equals(viaHeaderValue.Comment, this.Comment, StringComparison.Ordinal) && string.Equals(viaHeaderValue.ProtocolName, this.ProtocolName, StringComparison.OrdinalIgnoreCase) && string.Equals(viaHeaderValue.ProtocolVersion, this.ProtocolVersion, StringComparison.OrdinalIgnoreCase) && string.Equals(viaHeaderValue.ReceivedBy, this.ReceivedBy, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
    {
      int hashCode = this.ProtocolVersion.ToLowerInvariant().GetHashCode() ^ this.ReceivedBy.ToLowerInvariant().GetHashCode();
      if (!string.IsNullOrEmpty(this.ProtocolName))
        hashCode ^= this.ProtocolName.ToLowerInvariant().GetHashCode();
      if (!string.IsNullOrEmpty(this.Comment))
        hashCode ^= this.Comment.GetHashCode();
      return hashCode;
    }

    internal static bool TryParse(string input, int minimalCount, out List<ViaHeaderValue> result) => CollectionParser.TryParse<ViaHeaderValue>(input, minimalCount, new ElementTryParser<ViaHeaderValue>(ViaHeaderValue.TryParseElement), out result);

    private static bool TryParseElement(Lexer lexer, out ViaHeaderValue parsedValue, out Token t)
    {
      parsedValue = (ViaHeaderValue) null;
      t = lexer.Scan();
      if ((Token.Type) t != Token.Type.Token)
        return false;
      Token start = lexer.Scan();
      ViaHeaderValue viaHeaderValue = new ViaHeaderValue();
      if ((Token.Type) start == Token.Type.SeparatorSlash)
      {
        Token token = lexer.Scan();
        if ((Token.Type) token != Token.Type.Token)
          return false;
        viaHeaderValue.ProtocolName = lexer.GetStringValue(t);
        viaHeaderValue.ProtocolVersion = lexer.GetStringValue(token);
        start = lexer.Scan();
      }
      else
        viaHeaderValue.ProtocolVersion = lexer.GetStringValue(t);
      if ((Token.Type) start != Token.Type.Token)
        return false;
      if (lexer.PeekChar() == 58)
      {
        lexer.EatChar();
        t = lexer.Scan();
        if ((Token.Type) t != Token.Type.Token)
          return false;
      }
      else
        t = start;
      viaHeaderValue.ReceivedBy = lexer.GetStringValue(start, t);
      string str;
      if (lexer.ScanCommentOptional(out str, out t))
        t = lexer.Scan();
      viaHeaderValue.Comment = str;
      parsedValue = viaHeaderValue;
      return true;
    }

    public override string ToString()
    {
      string str1;
      if (this.ProtocolName == null)
        str1 = this.ProtocolVersion + " " + this.ReceivedBy;
      else
        str1 = this.ProtocolName + "/" + this.ProtocolVersion + " " + this.ReceivedBy;
      string str2 = str1;
      return this.Comment == null ? str2 : str2 + " " + this.Comment;
    }
  }
}
