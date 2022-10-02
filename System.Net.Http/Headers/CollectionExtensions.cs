// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.CollectionExtensions
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.Http.Headers
{
  internal static class CollectionExtensions
  {
    public static bool SequenceEqual<TSource>(this List<TSource> first, List<TSource> second)
    {
      if (first == null)
        return second == null || second.Count == 0;
      if (second != null)
        return first.SequenceEqual<TSource>((IEnumerable<TSource>) second);
      return first == null || first.Count == 0;
    }

    public static string ToString<T>(this List<T> list)
    {
      if (list == null || list.Count == 0)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < list.Count; ++index)
      {
        stringBuilder.Append("; ");
        stringBuilder.Append((object) list[index]);
      }
      return stringBuilder.ToString();
    }

    public static void ToStringBuilder<T>(this List<T> list, StringBuilder sb)
    {
      if (list == null || list.Count == 0)
        return;
      for (int index = 0; index < list.Count; ++index)
      {
        if (index > 0)
          sb.Append(", ");
        sb.Append((object) list[index]);
      }
    }
  }
}
