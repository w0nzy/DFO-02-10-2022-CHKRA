// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HttpHeaders
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Http.Headers
{
  public abstract class HttpHeaders : 
    IEnumerable<KeyValuePair<string, IEnumerable<string>>>,
    IEnumerable
  {
    private static readonly Dictionary<string, HeaderInfo> known_headers;
    private readonly Dictionary<string, HttpHeaders.HeaderBucket> headers;
    private readonly HttpHeaderKind HeaderKind;
    internal bool? connectionclose;
    internal bool? transferEncodingChunked;

    static HttpHeaders()
    {
      HeaderInfo[] headerInfoArray = new HeaderInfo[48]
      {
        HeaderInfo.CreateMulti<MediaTypeWithQualityHeaderValue>("Accept", new TryParseListDelegate<MediaTypeWithQualityHeaderValue>(MediaTypeWithQualityHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Charset", new TryParseListDelegate<StringWithQualityHeaderValue>(StringWithQualityHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Encoding", new TryParseListDelegate<StringWithQualityHeaderValue>(StringWithQualityHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Language", new TryParseListDelegate<StringWithQualityHeaderValue>(StringWithQualityHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<string>("Accept-Ranges", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateSingle<TimeSpan>("Age", new TryParseDelegate<TimeSpan>(Parser.TimeSpanSeconds.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<string>("Allow", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Content, 0),
        HeaderInfo.CreateSingle<AuthenticationHeaderValue>("Authorization", new TryParseDelegate<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<CacheControlHeaderValue>("Cache-Control", new TryParseDelegate<CacheControlHeaderValue>(CacheControlHeaderValue.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<string>("Connection", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateSingle<ContentDispositionHeaderValue>("Content-Disposition", new TryParseDelegate<ContentDispositionHeaderValue>(ContentDispositionHeaderValue.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateMulti<string>("Content-Encoding", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateMulti<string>("Content-Language", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<long>("Content-Length", new TryParseDelegate<long>(Parser.Long.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<System.Uri>("Content-Location", new TryParseDelegate<System.Uri>(Parser.Uri.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<byte[]>("Content-MD5", new TryParseDelegate<byte[]>(Parser.MD5.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<ContentRangeHeaderValue>("Content-Range", new TryParseDelegate<ContentRangeHeaderValue>(ContentRangeHeaderValue.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<MediaTypeHeaderValue>("Content-Type", new TryParseDelegate<MediaTypeHeaderValue>(MediaTypeHeaderValue.TryParse), HttpHeaderKind.Content),
        HeaderInfo.CreateSingle<DateTimeOffset>("Date", new TryParseDelegate<DateTimeOffset>(Parser.DateTime.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response, Parser.DateTime.ToString),
        HeaderInfo.CreateSingle<EntityTagHeaderValue>("ETag", new TryParseDelegate<EntityTagHeaderValue>(EntityTagHeaderValue.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<NameValueWithParametersHeaderValue>("Expect", new TryParseListDelegate<NameValueWithParametersHeaderValue>(NameValueWithParametersHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<DateTimeOffset>("Expires", new TryParseDelegate<DateTimeOffset>(Parser.DateTime.TryParse), HttpHeaderKind.Content, Parser.DateTime.ToString),
        HeaderInfo.CreateSingle<string>("From", new TryParseDelegate<string>(Parser.EmailAddress.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<string>("Host", new TryParseDelegate<string>(Parser.Host.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<EntityTagHeaderValue>("If-Match", new TryParseListDelegate<EntityTagHeaderValue>(EntityTagHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<DateTimeOffset>("If-Modified-Since", new TryParseDelegate<DateTimeOffset>(Parser.DateTime.TryParse), HttpHeaderKind.Request, Parser.DateTime.ToString),
        HeaderInfo.CreateMulti<EntityTagHeaderValue>("If-None-Match", new TryParseListDelegate<EntityTagHeaderValue>(EntityTagHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<RangeConditionHeaderValue>("If-Range", new TryParseDelegate<RangeConditionHeaderValue>(RangeConditionHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<DateTimeOffset>("If-Unmodified-Since", new TryParseDelegate<DateTimeOffset>(Parser.DateTime.TryParse), HttpHeaderKind.Request, Parser.DateTime.ToString),
        HeaderInfo.CreateSingle<DateTimeOffset>("Last-Modified", new TryParseDelegate<DateTimeOffset>(Parser.DateTime.TryParse), HttpHeaderKind.Content, Parser.DateTime.ToString),
        HeaderInfo.CreateSingle<System.Uri>("Location", new TryParseDelegate<System.Uri>(Parser.Uri.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateSingle<int>("Max-Forwards", new TryParseDelegate<int>(Parser.Int.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateMulti<NameValueHeaderValue>("Pragma", new TryParseListDelegate<NameValueHeaderValue>(NameValueHeaderValue.TryParsePragma), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<AuthenticationHeaderValue>("Proxy-Authenticate", new TryParseListDelegate<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateSingle<AuthenticationHeaderValue>("Proxy-Authorization", new TryParseDelegate<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<RangeHeaderValue>("Range", new TryParseDelegate<RangeHeaderValue>(RangeHeaderValue.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<System.Uri>("Referer", new TryParseDelegate<System.Uri>(Parser.Uri.TryParse), HttpHeaderKind.Request),
        HeaderInfo.CreateSingle<RetryConditionHeaderValue>("Retry-After", new TryParseDelegate<RetryConditionHeaderValue>(RetryConditionHeaderValue.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<ProductInfoHeaderValue>("Server", new TryParseListDelegate<ProductInfoHeaderValue>(ProductInfoHeaderValue.TryParse), HttpHeaderKind.Response, separator: " "),
        HeaderInfo.CreateMulti<TransferCodingWithQualityHeaderValue>("TE", new TryParseListDelegate<TransferCodingWithQualityHeaderValue>(TransferCodingWithQualityHeaderValue.TryParse), HttpHeaderKind.Request, 0),
        HeaderInfo.CreateMulti<string>("Trailer", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<TransferCodingHeaderValue>("Transfer-Encoding", new TryParseListDelegate<TransferCodingHeaderValue>(TransferCodingHeaderValue.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<ProductHeaderValue>("Upgrade", new TryParseListDelegate<ProductHeaderValue>(ProductHeaderValue.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<ProductInfoHeaderValue>("User-Agent", new TryParseListDelegate<ProductInfoHeaderValue>(ProductInfoHeaderValue.TryParse), HttpHeaderKind.Request, separator: " "),
        HeaderInfo.CreateMulti<string>("Vary", new TryParseListDelegate<string>(CollectionParser.TryParse), HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<ViaHeaderValue>("Via", new TryParseListDelegate<ViaHeaderValue>(ViaHeaderValue.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<WarningHeaderValue>("Warning", new TryParseListDelegate<WarningHeaderValue>(WarningHeaderValue.TryParse), HttpHeaderKind.Request | HttpHeaderKind.Response),
        HeaderInfo.CreateMulti<AuthenticationHeaderValue>("WWW-Authenticate", new TryParseListDelegate<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParse), HttpHeaderKind.Response)
      };
      HttpHeaders.known_headers = new Dictionary<string, HeaderInfo>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (HeaderInfo headerInfo in headerInfoArray)
        HttpHeaders.known_headers.Add(headerInfo.Name, headerInfo);
    }

    protected HttpHeaders() => this.headers = new Dictionary<string, HttpHeaders.HeaderBucket>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    internal HttpHeaders(HttpHeaderKind headerKind)
      : this()
    {
      this.HeaderKind = headerKind;
    }

    private bool AddInternal(
      string name,
      IEnumerable<string> values,
      HeaderInfo headerInfo,
      bool ignoreInvalid)
    {
      HttpHeaders.HeaderBucket headerBucket;
      this.headers.TryGetValue(name, out headerBucket);
      bool flag1 = true;
      foreach (string str in values)
      {
        bool flag2 = headerBucket == null;
        if (headerInfo != null)
        {
          object result;
          if (!headerInfo.TryParse(str, out result))
          {
            if (!ignoreInvalid)
              throw new FormatException(string.Format("Could not parse value for header '{0}'", (object) name));
            flag1 = false;
            continue;
          }
          if (headerInfo.AllowsMany)
          {
            if (headerBucket == null)
              headerBucket = new HttpHeaders.HeaderBucket(headerInfo.CreateCollection(this), headerInfo.CustomToString);
            headerInfo.AddToCollection(headerBucket.Parsed, result);
          }
          else
          {
            if (headerBucket != null)
              throw new FormatException();
            headerBucket = new HttpHeaders.HeaderBucket(result, headerInfo.CustomToString);
          }
        }
        else
        {
          if (headerBucket == null)
            headerBucket = new HttpHeaders.HeaderBucket((object) null, (Func<object, string>) null);
          headerBucket.Values.Add(str ?? string.Empty);
        }
        if (flag2)
          this.headers.Add(name, headerBucket);
      }
      return flag1;
    }

    public bool TryAddWithoutValidation(string name, IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      if (!this.TryCheckName(name, out HeaderInfo _))
        return false;
      this.AddInternal(name, values, (HeaderInfo) null, true);
      return true;
    }

    private HeaderInfo CheckName(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException(nameof (name));
      Parser.Token.Check(name);
      HeaderInfo headerInfo;
      if (!HttpHeaders.known_headers.TryGetValue(name, out headerInfo) || (headerInfo.HeaderKind & this.HeaderKind) != HttpHeaderKind.None)
        return headerInfo;
      if (this.HeaderKind != HttpHeaderKind.None && ((this.HeaderKind | headerInfo.HeaderKind) & HttpHeaderKind.Content) != HttpHeaderKind.None)
        throw new InvalidOperationException(name);
      return (HeaderInfo) null;
    }

    private bool TryCheckName(string name, out HeaderInfo headerInfo)
    {
      if (!Parser.Token.TryCheck(name))
      {
        headerInfo = (HeaderInfo) null;
        return false;
      }
      return !HttpHeaders.known_headers.TryGetValue(name, out headerInfo) || (headerInfo.HeaderKind & this.HeaderKind) != HttpHeaderKind.None || this.HeaderKind == HttpHeaderKind.None || ((this.HeaderKind | headerInfo.HeaderKind) & HttpHeaderKind.Content) == HttpHeaderKind.None;
    }

    public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
    {
      foreach (KeyValuePair<string, HttpHeaders.HeaderBucket> header1 in this.headers)
      {
        HttpHeaders.HeaderBucket header2 = this.headers[header1.Key];
        HeaderInfo headerInfo;
        HttpHeaders.known_headers.TryGetValue(header1.Key, out headerInfo);
        List<string> allHeaderValues = this.GetAllHeaderValues(header2, headerInfo);
        if (allHeaderValues != null)
          yield return new KeyValuePair<string, IEnumerable<string>>(header1.Key, (IEnumerable<string>) allHeaderValues);
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public bool Remove(string name)
    {
      this.CheckName(name);
      return this.headers.Remove(name);
    }

    internal static string GetSingleHeaderString(string key, IEnumerable<string> values)
    {
      string str1 = ",";
      HeaderInfo headerInfo;
      if (HttpHeaders.known_headers.TryGetValue(key, out headerInfo) && headerInfo.AllowsMany)
        str1 = headerInfo.Separator;
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = true;
      foreach (string str2 in values)
      {
        if (!flag)
        {
          stringBuilder.Append(str1);
          if (str1 != " ")
            stringBuilder.Append(" ");
        }
        stringBuilder.Append(str2);
        flag = false;
      }
      return flag ? (string) null : stringBuilder.ToString();
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in this)
      {
        stringBuilder.Append(keyValuePair.Key);
        stringBuilder.Append(": ");
        stringBuilder.Append(HttpHeaders.GetSingleHeaderString(keyValuePair.Key, keyValuePair.Value));
        stringBuilder.Append("\r\n");
      }
      return stringBuilder.ToString();
    }

    internal void AddOrRemove<T>(string name, T value, Func<object, string> converter = null) where T : class
    {
      if ((object) value == null)
        this.Remove(name);
      else
        this.SetValue<T>(name, value, converter);
    }

    private List<string> GetAllHeaderValues(
      HttpHeaders.HeaderBucket bucket,
      HeaderInfo headerInfo)
    {
      List<string> allHeaderValues = (List<string>) null;
      if (headerInfo != null && headerInfo.AllowsMany)
        allHeaderValues = headerInfo.ToStringCollection(bucket.Parsed);
      else if (bucket.Parsed != null)
      {
        string str = bucket.ParsedToString();
        if (!string.IsNullOrEmpty(str))
        {
          allHeaderValues = new List<string>();
          allHeaderValues.Add(str);
        }
      }
      if (bucket.HasStringValues)
      {
        if (allHeaderValues == null)
          allHeaderValues = new List<string>();
        allHeaderValues.AddRange((IEnumerable<string>) bucket.Values);
      }
      return allHeaderValues;
    }

    internal static HttpHeaderKind GetKnownHeaderKind(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException(nameof (name));
      HeaderInfo headerInfo;
      return HttpHeaders.known_headers.TryGetValue(name, out headerInfo) ? headerInfo.HeaderKind : HttpHeaderKind.None;
    }

    internal T GetValue<T>(string name)
    {
      HttpHeaders.HeaderBucket headerBucket;
      if (!this.headers.TryGetValue(name, out headerBucket))
        return default (T);
      if (headerBucket.HasStringValues)
      {
        object result;
        if (!HttpHeaders.known_headers[name].TryParse(headerBucket.Values[0], out result))
          return !(typeof (T) == typeof (string)) ? default (T) : (T) headerBucket.Values[0];
        headerBucket.Parsed = result;
        headerBucket.Values = (List<string>) null;
      }
      return (T) headerBucket.Parsed;
    }

    internal HttpHeaderValueCollection<T> GetValues<T>(string name) where T : class
    {
      HttpHeaders.HeaderBucket headerBucket;
      if (!this.headers.TryGetValue(name, out headerBucket))
      {
        HeaderInfo knownHeader = HttpHeaders.known_headers[name];
        headerBucket = new HttpHeaders.HeaderBucket((object) new HttpHeaderValueCollection<T>(this, knownHeader), knownHeader.CustomToString);
        this.headers.Add(name, headerBucket);
      }
      HttpHeaderValueCollection<T> collection = (HttpHeaderValueCollection<T>) headerBucket.Parsed;
      if (headerBucket.HasStringValues)
      {
        HeaderInfo knownHeader = HttpHeaders.known_headers[name];
        if (collection == null)
          headerBucket.Parsed = (object) (collection = new HttpHeaderValueCollection<T>(this, knownHeader));
        for (int index = 0; index < headerBucket.Values.Count; ++index)
        {
          string invalidValue = headerBucket.Values[index];
          object result;
          if (!knownHeader.TryParse(invalidValue, out result))
            collection.AddInvalidValue(invalidValue);
          else
            knownHeader.AddToCollection((object) collection, result);
        }
        headerBucket.Values.Clear();
      }
      return collection;
    }

    internal void SetValue<T>(string name, T value, Func<object, string> toStringConverter = null) => this.headers[name] = new HttpHeaders.HeaderBucket((object) value, toStringConverter);

    private class HeaderBucket
    {
      public object Parsed;
      private List<string> values;
      public readonly Func<object, string> CustomToString;

      public HeaderBucket(object parsed, Func<object, string> converter)
      {
        this.Parsed = parsed;
        this.CustomToString = converter;
      }

      public bool HasStringValues => this.values != null && this.values.Count > 0;

      public List<string> Values
      {
        get => this.values ?? (this.values = new List<string>());
        set => this.values = value;
      }

      public string ParsedToString()
      {
        if (this.Parsed == null)
          return (string) null;
        return this.CustomToString != null ? this.CustomToString(this.Parsed) : this.Parsed.ToString();
      }
    }
  }
}
