// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpMessageHandler
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public abstract class HttpMessageHandler : IDisposable
  {
    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
    }

    protected internal abstract Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken);
  }
}
