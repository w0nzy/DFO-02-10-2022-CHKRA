// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HeaderInfo
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  internal abstract class HeaderInfo
  {
    public bool AllowsMany;
    public readonly HttpHeaderKind HeaderKind;
    public readonly string Name;

    protected HeaderInfo(string name, HttpHeaderKind headerKind)
    {
      this.Name = name;
      this.HeaderKind = headerKind;
    }

    public static HeaderInfo CreateSingle<T>(
      string name,
      TryParseDelegate<T> parser,
      HttpHeaderKind headerKind,
      Func<object, string> toString = null)
    {
      HeaderInfo.HeaderTypeInfo<T, object> single = new HeaderInfo.HeaderTypeInfo<T, object>(name, parser, headerKind);
      single.CustomToString = toString;
      return (HeaderInfo) single;
    }

    public static HeaderInfo CreateMulti<T>(
      string name,
      TryParseListDelegate<T> elementParser,
      HttpHeaderKind headerKind,
      int minimalCount = 1,
      string separator = ", ")
      where T : class
    {
      return (HeaderInfo) new HeaderInfo.CollectionHeaderTypeInfo<T, T>(name, elementParser, headerKind, minimalCount, separator);
    }

    public object CreateCollection(HttpHeaders headers) => this.CreateCollection(headers, this);

    public Func<object, string> CustomToString { get; private set; }

    public virtual string Separator => throw new NotSupportedException();

    public abstract void AddToCollection(object collection, object value);

    protected abstract object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo);

    public abstract List<string> ToStringCollection(object collection);

    public abstract bool TryParse(string value, out object result);

    private class HeaderTypeInfo<T, U> : HeaderInfo where U : class
    {
      private readonly TryParseDelegate<T> parser;

      public HeaderTypeInfo(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind)
        : base(name, headerKind)
      {
        this.parser = parser;
      }

      public override void AddToCollection(object collection, object value)
      {
        HttpHeaderValueCollection<U> headerValueCollection = (HttpHeaderValueCollection<U>) collection;
        if (value is List<U> values)
          headerValueCollection.AddRange(values);
        else
          headerValueCollection.Add((U) value);
      }

      protected override object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo) => (object) new HttpHeaderValueCollection<U>(headers, headerInfo);

      public override List<string> ToStringCollection(object collection)
      {
        if (collection == null)
          return (List<string>) null;
        HttpHeaderValueCollection<U> headerValueCollection = (HttpHeaderValueCollection<U>) collection;
        if (headerValueCollection.Count == 0)
          return headerValueCollection.InvalidValues == null ? (List<string>) null : new List<string>((IEnumerable<string>) headerValueCollection.InvalidValues);
        List<string> stringCollection = new List<string>();
        foreach (U u in headerValueCollection)
          stringCollection.Add(u.ToString());
        if (headerValueCollection.InvalidValues != null)
          stringCollection.AddRange((IEnumerable<string>) headerValueCollection.InvalidValues);
        return stringCollection;
      }

      public override bool TryParse(string value, out object result)
      {
        T result1;
        int num = this.parser(value, out result1) ? 1 : 0;
        result = (object) result1;
        return num != 0;
      }
    }

    private class CollectionHeaderTypeInfo<T, U> : HeaderInfo.HeaderTypeInfo<T, U> where U : class
    {
      private readonly int minimalCount;
      private readonly string separator;
      private readonly TryParseListDelegate<T> parser;

      public CollectionHeaderTypeInfo(
        string name,
        TryParseListDelegate<T> parser,
        HttpHeaderKind headerKind,
        int minimalCount,
        string separator)
        : base(name, (TryParseDelegate<T>) null, headerKind)
      {
        this.parser = parser;
        this.minimalCount = minimalCount;
        this.AllowsMany = true;
        this.separator = separator;
      }

      public override string Separator => this.separator;

      public override bool TryParse(string value, out object result)
      {
        List<T> result1;
        if (!this.parser(value, this.minimalCount, out result1))
        {
          result = (object) null;
          return false;
        }
        result = (object) result1;
        return true;
      }
    }
  }
}
