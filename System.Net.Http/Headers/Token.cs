// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.Token
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

namespace System.Net.Http.Headers
{
  internal struct Token
  {
    public static readonly Token Empty = new Token(Token.Type.Token, 0, 0);
    private readonly Token.Type type;

    public Token(Token.Type type, int startPosition, int endPosition)
      : this()
    {
      this.type = type;
      this.StartPosition = startPosition;
      this.EndPosition = endPosition;
    }

    public int StartPosition { get; private set; }

    public int EndPosition { get; private set; }

    public Token.Type Kind => this.type;

    public static implicit operator Token.Type(Token token) => token.type;

    public override string ToString() => this.type.ToString();

    public enum Type
    {
      Error,
      End,
      Token,
      QuotedString,
      SeparatorEqual,
      SeparatorSemicolon,
      SeparatorSlash,
      SeparatorDash,
      SeparatorComma,
      OpenParens,
    }
  }
}
