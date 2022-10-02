// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.Lexer
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Globalization;

namespace System.Net.Http.Headers
{
  internal class Lexer
  {
    private static readonly bool[] token_chars = new bool[(int) sbyte.MaxValue]
    {
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      true,
      false,
      true,
      true,
      true,
      true,
      true,
      false,
      false,
      true,
      true,
      false,
      true,
      true,
      false,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      false,
      false,
      false,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      false,
      true,
      false,
      true
    };
    private static readonly int last_token_char = Lexer.token_chars.Length;
    private static readonly string[] dt_formats = new string[5]
    {
      "r",
      "dddd, dd'-'MMM'-'yy HH:mm:ss 'GMT'",
      "ddd MMM d HH:mm:ss yyyy",
      "d MMM yy H:m:s",
      "ddd, d MMM yyyy H:m:s zzz"
    };
    private readonly string s;
    private int pos;

    public Lexer(string stream) => this.s = stream;

    public int Position
    {
      get => this.pos;
      set => this.pos = value;
    }

    public string GetStringValue(Token token) => this.s.Substring(token.StartPosition, token.EndPosition - token.StartPosition);

    public string GetStringValue(Token start, Token end) => this.s.Substring(start.StartPosition, end.EndPosition - start.StartPosition);

    public string GetQuotedStringValue(Token start) => this.s.Substring(start.StartPosition + 1, start.EndPosition - start.StartPosition - 2);

    public string GetRemainingStringValue(int position) => position <= this.s.Length ? this.s.Substring(position) : (string) null;

    public bool IsStarStringValue(Token token) => token.EndPosition - token.StartPosition == 1 && this.s[token.StartPosition] == '*';

    public bool TryGetNumericValue(Token token, out int value) => int.TryParse(this.GetStringValue(token), NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out value);

    public bool TryGetNumericValue(Token token, out long value) => long.TryParse(this.GetStringValue(token), NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out value);

    public TimeSpan? TryGetTimeSpanValue(Token token)
    {
      int num;
      return this.TryGetNumericValue(token, out num) ? new TimeSpan?(TimeSpan.FromSeconds((double) num)) : new TimeSpan?();
    }

    public bool TryGetDateValue(Token token, out DateTimeOffset value) => Lexer.TryGetDateValue((Token.Type) token == Token.Type.QuotedString ? this.s.Substring(token.StartPosition + 1, token.EndPosition - token.StartPosition - 2) : this.GetStringValue(token), out value);

    public static bool TryGetDateValue(string text, out DateTimeOffset value) => DateTimeOffset.TryParseExact(text, Lexer.dt_formats, (IFormatProvider) DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out value);

    public bool TryGetDoubleValue(Token token, out double value) => double.TryParse(this.GetStringValue(token), NumberStyles.AllowDecimalPoint, (IFormatProvider) CultureInfo.InvariantCulture, out value);

    public static bool IsValidToken(string input)
    {
      int index;
      for (index = 0; index < input.Length; ++index)
      {
        if (!Lexer.IsValidCharacter(input[index]))
          return false;
      }
      return index > 0;
    }

    public static bool IsValidCharacter(char input) => (int) input < Lexer.last_token_char && Lexer.token_chars[(int) input];

    public void EatChar() => ++this.pos;

    public int PeekChar() => this.pos >= this.s.Length ? -1 : (int) this.s[this.pos];

    public bool ScanCommentOptional(out string value, out Token readToken)
    {
      readToken = this.Scan();
      if ((Token.Type) readToken != Token.Type.OpenParens)
      {
        value = (string) null;
        return false;
      }
      int num = 1;
      while (this.pos < this.s.Length)
      {
        char ch = this.s[this.pos];
        switch (ch)
        {
          case '(':
            ++num;
            ++this.pos;
            continue;
          case ')':
            ++this.pos;
            if (--num <= 0)
            {
              int startPosition = readToken.StartPosition;
              value = this.s.Substring(startPosition, this.pos - startPosition);
              return true;
            }
            continue;
          default:
            if (ch >= ' ' && ch <= '~')
            {
              ++this.pos;
              continue;
            }
            goto label_10;
        }
      }
label_10:
      value = (string) null;
      return false;
    }

    public Token Scan(bool recognizeDash = false)
    {
      int startPosition = this.pos;
      if (this.s == null)
        return new Token(Token.Type.Error, 0, 0);
      Token.Type type;
      if (this.pos >= this.s.Length)
      {
        type = Token.Type.End;
      }
      else
      {
        type = Token.Type.Error;
        char index1;
        do
        {
          index1 = this.s[this.pos++];
          switch (index1)
          {
            case '\t':
            case ' ':
              continue;
            case '"':
              goto label_14;
            case '(':
              goto label_20;
            case ',':
              goto label_13;
            case '-':
              goto label_11;
            case '/':
              goto label_10;
            case ';':
              goto label_9;
            case '=':
              goto label_8;
            default:
              goto label_21;
          }
        }
        while (this.pos != this.s.Length);
        type = Token.Type.End;
        goto label_26;
label_8:
        type = Token.Type.SeparatorEqual;
        goto label_26;
label_9:
        type = Token.Type.SeparatorSemicolon;
        goto label_26;
label_10:
        type = Token.Type.SeparatorSlash;
        goto label_26;
label_11:
        if (recognizeDash)
        {
          type = Token.Type.SeparatorDash;
          goto label_26;
        }
        else
          goto label_21;
label_13:
        type = Token.Type.SeparatorComma;
        goto label_26;
label_14:
        startPosition = this.pos - 1;
        while (this.pos < this.s.Length)
        {
          switch (this.s[this.pos++])
          {
            case '"':
              type = Token.Type.QuotedString;
              goto label_26;
            case '\\':
              if (this.pos + 1 < this.s.Length)
              {
                ++this.pos;
                continue;
              }
              goto label_26;
            default:
              continue;
          }
        }
        goto label_26;
label_20:
        startPosition = this.pos - 1;
        type = Token.Type.OpenParens;
        goto label_26;
label_21:
        if ((int) index1 < Lexer.last_token_char && Lexer.token_chars[(int) index1])
        {
          startPosition = this.pos - 1;
          type = Token.Type.Token;
          for (; this.pos < this.s.Length; ++this.pos)
          {
            char index2 = this.s[this.pos];
            if ((int) index2 >= Lexer.last_token_char || !Lexer.token_chars[(int) index2])
              break;
          }
        }
      }
label_26:
      return new Token(type, startPosition, this.pos);
    }
  }
}
