// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.Parser
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Globalization;
using System.Net.Mail;

namespace System.Net.Http.Headers
{
  internal static class Parser
  {
    public static class Token
    {
      public static void Check(string s)
      {
        if (s == null)
          throw new ArgumentNullException();
        if (Lexer.IsValidToken(s))
          return;
        if (s.Length == 0)
          throw new ArgumentException();
        throw new FormatException(s);
      }

      public static bool TryCheck(string s) => s != null && Lexer.IsValidToken(s);
    }

    public static class DateTime
    {
      public static readonly Func<object, string> ToString = (Func<object, string>) (l => ((DateTimeOffset) l).ToString("r", (IFormatProvider) CultureInfo.InvariantCulture));

      public static bool TryParse(string input, out DateTimeOffset result) => Lexer.TryGetDateValue(input, out result);
    }

    public static class EmailAddress
    {
      public static bool TryParse(string input, out string result)
      {
        try
        {
          MailAddress mailAddress = new MailAddress(input);
          result = input;
          return true;
        }
        catch
        {
          result = (string) null;
          return false;
        }
      }
    }

    public static class Host
    {
      public static bool TryParse(string input, out string result)
      {
        result = input;
        return System.Uri.TryCreate("http://u@" + input + "/", UriKind.Absolute, out System.Uri _);
      }
    }

    public static class Int
    {
      public static bool TryParse(string input, out int result) => int.TryParse(input, NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out result);
    }

    public static class Long
    {
      public static bool TryParse(string input, out long result) => long.TryParse(input, NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out result);
    }

    public static class MD5
    {
      public static readonly Func<object, string> ToString = (Func<object, string>) (l => Convert.ToBase64String((byte[]) l));

      public static bool TryParse(string input, out byte[] result)
      {
        try
        {
          result = Convert.FromBase64String(input);
          return true;
        }
        catch
        {
          result = (byte[]) null;
          return false;
        }
      }
    }

    public static class TimeSpanSeconds
    {
      public static bool TryParse(string input, out TimeSpan result)
      {
        int result1;
        if (Parser.Int.TryParse(input, out result1))
        {
          result = TimeSpan.FromSeconds((double) result1);
          return true;
        }
        result = TimeSpan.Zero;
        return false;
      }
    }

    public static class Uri
    {
      public static bool TryParse(string input, out System.Uri result) => System.Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result);
    }
  }
}
