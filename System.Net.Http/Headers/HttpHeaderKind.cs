﻿// Decompiled with JetBrains decompiler
// Type: System.Net.Http.Headers.HttpHeaderKind
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

namespace System.Net.Http.Headers
{
  [Flags]
  internal enum HttpHeaderKind
  {
    None = 0,
    Request = 1,
    Response = 2,
    Content = 4,
  }
}
