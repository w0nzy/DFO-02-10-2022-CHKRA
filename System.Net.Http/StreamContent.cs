// Decompiled with JetBrains decompiler
// Type: System.Net.Http.StreamContent
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public class StreamContent : HttpContent
  {
    private readonly Stream content;
    private readonly int bufferSize;
    private readonly CancellationToken cancellationToken;
    private readonly long startPosition;
    private bool contentCopied;

    public StreamContent(Stream content)
      : this(content, 16384)
    {
    }

    public StreamContent(Stream content, int bufferSize)
    {
      if (content == null)
        throw new ArgumentNullException(nameof (content));
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize));
      this.content = content;
      this.bufferSize = bufferSize;
      if (!content.CanSeek)
        return;
      this.startPosition = content.Position;
    }

    internal StreamContent(Stream content, CancellationToken cancellationToken)
      : this(content)
    {
      this.cancellationToken = cancellationToken;
    }

    protected override Task<Stream> CreateContentReadStreamAsync() => Task.FromResult<Stream>(this.content);

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.content.Dispose();
      base.Dispose(disposing);
    }

    protected internal override Task SerializeToStreamAsync(
      Stream stream,
      TransportContext context)
    {
      if (this.contentCopied)
      {
        if (!this.content.CanSeek)
          throw new InvalidOperationException("The stream was already consumed. It cannot be read again.");
        this.content.Seek(this.startPosition, SeekOrigin.Begin);
      }
      else
        this.contentCopied = true;
      return this.content.CopyToAsync(stream, this.bufferSize, this.cancellationToken);
    }

    protected internal override bool TryComputeLength(out long length)
    {
      if (!this.content.CanSeek)
      {
        length = 0L;
        return false;
      }
      length = this.content.Length - this.startPosition;
      return true;
    }
  }
}
