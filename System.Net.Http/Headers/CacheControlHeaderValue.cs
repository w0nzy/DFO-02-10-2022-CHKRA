// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.CacheControlHeaderValue
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
  public class CacheControlHeaderValue : ICloneable
  {
    private List<NameValueHeaderValue> extensions;
    private List<string> no_cache_headers;
    private List<string> private_headers;

    public ICollection<NameValueHeaderValue> Extensions => (ICollection<NameValueHeaderValue>) this.extensions ?? (ICollection<NameValueHeaderValue>) (this.extensions = new List<NameValueHeaderValue>());

    public TimeSpan? MaxAge { get; set; }

    public bool MaxStale { get; set; }

    public TimeSpan? MaxStaleLimit { get; set; }

    public TimeSpan? MinFresh { get; set; }

    public bool MustRevalidate { get; set; }

    public bool NoCache { get; set; }

    public ICollection<string> NoCacheHeaders => (ICollection<string>) this.no_cache_headers ?? (ICollection<string>) (this.no_cache_headers = new List<string>());

    public bool NoStore { get; set; }

    public bool NoTransform { get; set; }

    public bool OnlyIfCached { get; set; }

    public bool Private { get; set; }

    public ICollection<string> PrivateHeaders => (ICollection<string>) this.private_headers ?? (ICollection<string>) (this.private_headers = new List<string>());

    public bool ProxyRevalidate { get; set; }

    public bool Public { get; set; }

    public TimeSpan? SharedMaxAge { get; set; }

    object ICloneable.Clone()
    {
      CacheControlHeaderValue controlHeaderValue = (CacheControlHeaderValue) this.MemberwiseClone();
      if (this.extensions != null)
      {
        controlHeaderValue.extensions = new List<NameValueHeaderValue>();
        foreach (NameValueHeaderValue extension in this.extensions)
          controlHeaderValue.extensions.Add(extension);
      }
      if (this.no_cache_headers != null)
      {
        controlHeaderValue.no_cache_headers = new List<string>();
        foreach (string noCacheHeader in this.no_cache_headers)
          controlHeaderValue.no_cache_headers.Add(noCacheHeader);
      }
      if (this.private_headers != null)
      {
        controlHeaderValue.private_headers = new List<string>();
        foreach (string privateHeader in this.private_headers)
          controlHeaderValue.private_headers.Add(privateHeader);
      }
      return (object) controlHeaderValue;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is CacheControlHeaderValue controlHeaderValue))
        return false;
      TimeSpan? nullable1 = this.MaxAge;
      TimeSpan? nullable2 = controlHeaderValue.MaxAge;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && this.MaxStale == controlHeaderValue.MaxStale)
      {
        nullable2 = this.MaxStaleLimit;
        nullable1 = controlHeaderValue.MaxStaleLimit;
        if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          nullable1 = this.MinFresh;
          nullable2 = controlHeaderValue.MinFresh;
          if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && this.MustRevalidate == controlHeaderValue.MustRevalidate && this.NoCache == controlHeaderValue.NoCache && this.NoStore == controlHeaderValue.NoStore && this.NoTransform == controlHeaderValue.NoTransform && this.OnlyIfCached == controlHeaderValue.OnlyIfCached && this.Private == controlHeaderValue.Private && this.ProxyRevalidate == controlHeaderValue.ProxyRevalidate && this.Public == controlHeaderValue.Public)
          {
            nullable2 = this.SharedMaxAge;
            nullable1 = controlHeaderValue.SharedMaxAge;
            if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && this.extensions.SequenceEqual<NameValueHeaderValue>(controlHeaderValue.extensions) && this.no_cache_headers.SequenceEqual<string>(controlHeaderValue.no_cache_headers))
              return this.private_headers.SequenceEqual<string>(controlHeaderValue.private_headers);
          }
        }
      }
      return false;
    }

    public override int GetHashCode() => (((((((((((((((29 * 29 + HashCodeCalculator.Calculate<NameValueHeaderValue>((ICollection<NameValueHeaderValue>) this.extensions)) * 29 + this.MaxAge.GetHashCode()) * 29 + this.MaxStale.GetHashCode()) * 29 + this.MaxStaleLimit.GetHashCode()) * 29 + this.MinFresh.GetHashCode()) * 29 + this.MustRevalidate.GetHashCode()) * 29 + HashCodeCalculator.Calculate<string>((ICollection<string>) this.no_cache_headers)) * 29 + this.NoCache.GetHashCode()) * 29 + this.NoStore.GetHashCode()) * 29 + this.NoTransform.GetHashCode()) * 29 + this.OnlyIfCached.GetHashCode()) * 29 + this.Private.GetHashCode()) * 29 + HashCodeCalculator.Calculate<string>((ICollection<string>) this.private_headers)) * 29 + this.ProxyRevalidate.GetHashCode()) * 29 + this.Public.GetHashCode()) * 29 + this.SharedMaxAge.GetHashCode();

    public static bool TryParse(string input, out CacheControlHeaderValue parsedValue)
    {
      parsedValue = (CacheControlHeaderValue) null;
      if (input == null)
        return true;
      CacheControlHeaderValue controlHeaderValue = new CacheControlHeaderValue();
      Lexer lexer = new Lexer(input);
      Token token;
      do
      {
        token = lexer.Scan();
        if ((Token.Type) token != Token.Type.Token)
          return false;
        string stringValue1 = lexer.GetStringValue(token);
        bool flag = false;
        TimeSpan? timeSpanValue;
        switch (stringValue1)
        {
          case "max-age":
          case "min-fresh":
          case "s-maxage":
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.SeparatorEqual)
              return false;
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.Token)
              return false;
            timeSpanValue = lexer.TryGetTimeSpanValue(token);
            if (!timeSpanValue.HasValue)
              return false;
            switch (stringValue1.Length)
            {
              case 7:
                controlHeaderValue.MaxAge = timeSpanValue;
                break;
              case 8:
                controlHeaderValue.SharedMaxAge = timeSpanValue;
                break;
              default:
                controlHeaderValue.MinFresh = timeSpanValue;
                break;
            }
            break;
          case "max-stale":
            controlHeaderValue.MaxStale = true;
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.SeparatorEqual)
            {
              flag = true;
              break;
            }
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.Token)
              return false;
            timeSpanValue = lexer.TryGetTimeSpanValue(token);
            if (!timeSpanValue.HasValue)
              return false;
            controlHeaderValue.MaxStaleLimit = timeSpanValue;
            break;
          case "must-revalidate":
            controlHeaderValue.MustRevalidate = true;
            break;
          case "no-cache":
          case "private":
            if (stringValue1.Length == 7)
              controlHeaderValue.Private = true;
            else
              controlHeaderValue.NoCache = true;
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.SeparatorEqual)
            {
              flag = true;
              break;
            }
            token = lexer.Scan();
            if ((Token.Type) token != Token.Type.QuotedString)
              return false;
            foreach (string str1 in lexer.GetQuotedStringValue(token).Split(',', StringSplitOptions.None))
            {
              char[] chArray = new char[2]{ '\t', ' ' };
              string str2 = str1.Trim(chArray);
              if (stringValue1.Length == 7)
              {
                controlHeaderValue.PrivateHeaders.Add(str2);
              }
              else
              {
                controlHeaderValue.NoCache = true;
                controlHeaderValue.NoCacheHeaders.Add(str2);
              }
            }
            break;
          case "no-store":
            controlHeaderValue.NoStore = true;
            break;
          case "no-transform":
            controlHeaderValue.NoTransform = true;
            break;
          case "only-if-cached":
            controlHeaderValue.OnlyIfCached = true;
            break;
          case "proxy-revalidate":
            controlHeaderValue.ProxyRevalidate = true;
            break;
          case "public":
            controlHeaderValue.Public = true;
            break;
          default:
            string stringValue2 = lexer.GetStringValue(token);
            string str = (string) null;
            token = lexer.Scan();
            if ((Token.Type) token == Token.Type.SeparatorEqual)
            {
              token = lexer.Scan();
              switch (token.Kind)
              {
                case Token.Type.Token:
                case Token.Type.QuotedString:
                  str = lexer.GetStringValue(token);
                  break;
                default:
                  return false;
              }
            }
            else
              flag = true;
            controlHeaderValue.Extensions.Add(NameValueHeaderValue.Create(stringValue2, str));
            break;
        }
        if (!flag)
          token = lexer.Scan();
      }
      while ((Token.Type) token == Token.Type.SeparatorComma);
      if ((Token.Type) token != Token.Type.End)
        return false;
      parsedValue = controlHeaderValue;
      return true;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      if (this.NoStore)
      {
        sb.Append("no-store");
        sb.Append(", ");
      }
      if (this.NoTransform)
      {
        sb.Append("no-transform");
        sb.Append(", ");
      }
      if (this.OnlyIfCached)
      {
        sb.Append("only-if-cached");
        sb.Append(", ");
      }
      if (this.Public)
      {
        sb.Append("public");
        sb.Append(", ");
      }
      if (this.MustRevalidate)
      {
        sb.Append("must-revalidate");
        sb.Append(", ");
      }
      if (this.ProxyRevalidate)
      {
        sb.Append("proxy-revalidate");
        sb.Append(", ");
      }
      if (this.NoCache)
      {
        sb.Append("no-cache");
        if (this.no_cache_headers != null)
        {
          sb.Append("=\"");
          this.no_cache_headers.ToStringBuilder<string>(sb);
          sb.Append("\"");
        }
        sb.Append(", ");
      }
      TimeSpan? nullable;
      if (this.MaxAge.HasValue)
      {
        sb.Append("max-age=");
        StringBuilder stringBuilder = sb;
        nullable = this.MaxAge;
        string str = nullable.Value.TotalSeconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder.Append(str);
        sb.Append(", ");
      }
      nullable = this.SharedMaxAge;
      if (nullable.HasValue)
      {
        sb.Append("s-maxage=");
        StringBuilder stringBuilder = sb;
        nullable = this.SharedMaxAge;
        string str = nullable.Value.TotalSeconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder.Append(str);
        sb.Append(", ");
      }
      if (this.MaxStale)
      {
        sb.Append("max-stale");
        nullable = this.MaxStaleLimit;
        if (nullable.HasValue)
        {
          sb.Append("=");
          StringBuilder stringBuilder = sb;
          nullable = this.MaxStaleLimit;
          string str = nullable.Value.TotalSeconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          stringBuilder.Append(str);
        }
        sb.Append(", ");
      }
      nullable = this.MinFresh;
      if (nullable.HasValue)
      {
        sb.Append("min-fresh=");
        StringBuilder stringBuilder = sb;
        nullable = this.MinFresh;
        string str = nullable.Value.TotalSeconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder.Append(str);
        sb.Append(", ");
      }
      if (this.Private)
      {
        sb.Append("private");
        if (this.private_headers != null)
        {
          sb.Append("=\"");
          this.private_headers.ToStringBuilder<string>(sb);
          sb.Append("\"");
        }
        sb.Append(", ");
      }
      this.extensions.ToStringBuilder<NameValueHeaderValue>(sb);
      if (sb.Length > 2 && sb[sb.Length - 2] == ',' && sb[sb.Length - 1] == ' ')
        sb.Remove(sb.Length - 2, 2);
      return sb.ToString();
    }
  }
}
