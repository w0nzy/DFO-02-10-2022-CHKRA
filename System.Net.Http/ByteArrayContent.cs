// Decompiled with JetBrains decompiler
// Type: System.Net.Http.ByteArrayContent
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public class ByteArrayContent : HttpContent
  {
    private readonly byte[] content;
    private readonly int offset;
    private readonly int count;

    public ByteArrayContent(byte[] content)
    {
      this.content = content != null ? content : throw new ArgumentNullException(nameof (content));
      this.count = content.Length;
    }

    protected override Task<Stream> CreateContentReadStreamAsync() => Task.FromResult<Stream>((Stream) new MemoryStream(this.content, this.offset, this.count));

    protected internal override Task SerializeToStreamAsync(
      Stream stream,
      TransportContext context)
    {
      return stream.WriteAsync(this.content, this.offset, this.count);
    }

    protected internal override bool TryComputeLength(out long length)
    {
      length = (long) this.count;
      return true;
    }
  }
}
