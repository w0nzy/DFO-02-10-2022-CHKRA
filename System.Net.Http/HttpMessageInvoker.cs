// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpMessageInvoker
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public class HttpMessageInvoker : IDisposable
  {
    private HttpMessageHandler handler;
    private readonly bool disposeHandler;

    public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
    {
      this.handler = handler != null ? handler : throw new ArgumentNullException(nameof (handler));
      this.disposeHandler = disposeHandler;
    }

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || !this.disposeHandler || this.handler == null)
        return;
      this.handler.Dispose();
      this.handler = (HttpMessageHandler) null;
    }

    public virtual Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      return this.handler.SendAsync(request, cancellationToken);
    }
  }
}
