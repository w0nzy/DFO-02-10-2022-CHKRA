// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.TryParseListDelegate`1
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
  internal delegate bool TryParseListDelegate<T>(
    string value,
    int minimalCount,
    out List<T> result);
}
