// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HttpHeaderValueCollection`1
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  public sealed class HttpHeaderValueCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    where T : class
  {
    private readonly List<T> list;
    private readonly HttpHeaders headers;
    private readonly HeaderInfo headerInfo;
    private List<string> invalidValues;

    internal HttpHeaderValueCollection(HttpHeaders headers, HeaderInfo headerInfo)
    {
      this.list = new List<T>();
      this.headers = headers;
      this.headerInfo = headerInfo;
    }

    public int Count => this.list.Count;

    internal List<string> InvalidValues => this.invalidValues;

    public bool IsReadOnly => false;

    public void Add(T item) => this.list.Add(item);

    internal void AddRange(List<T> values) => this.list.AddRange((IEnumerable<T>) values);

    internal void AddInvalidValue(string invalidValue)
    {
      if (this.invalidValues == null)
        this.invalidValues = new List<string>();
      this.invalidValues.Add(invalidValue);
    }

    public void Clear()
    {
      this.list.Clear();
      this.invalidValues = (List<string>) null;
    }

    public bool Contains(T item) => this.list.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

    public bool Remove(T item) => this.list.Remove(item);

    public override string ToString()
    {
      string str = string.Join<T>(this.headerInfo.Separator, (IEnumerable<T>) this.list);
      if (this.invalidValues != null)
        str += string.Join(this.headerInfo.Separator, (IEnumerable<string>) this.invalidValues);
      return str;
    }

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    internal T Find(Predicate<T> predicate) => this.list.Find(predicate);
  }
}
