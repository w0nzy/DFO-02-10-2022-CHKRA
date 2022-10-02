// Decompiled with JetBrains decompiler
// Type: System.Net.Http.HttpContent
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
  public abstract class HttpContent : IDisposable
  {
    private HttpContent.FixedMemoryStream buffer;
    private Stream stream;
    private bool disposed;
    private HttpContentHeaders headers;

    public HttpContentHeaders Headers => this.headers ?? (this.headers = new HttpContentHeaders(this));

    internal long? LoadedBufferLength => this.buffer != null ? new long?(this.buffer.Length) : new long?();

    public Task CopyToAsync(Stream stream) => this.CopyToAsync(stream, (TransportContext) null);

    public Task CopyToAsync(Stream stream, TransportContext context)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      return this.buffer != null ? this.buffer.CopyToAsync(stream) : this.SerializeToStreamAsync(stream, context);
    }

    protected virtual async Task<Stream> CreateContentReadStreamAsync()
    {
      await this.LoadIntoBufferAsync().ConfigureAwait(false);
      return (Stream) this.buffer;
    }

    private static HttpContent.FixedMemoryStream CreateFixedMemoryStream(
      long maxBufferSize)
    {
      return new HttpContent.FixedMemoryStream(maxBufferSize);
    }

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.disposed)
        return;
      this.disposed = true;
      if (this.buffer == null)
        return;
      this.buffer.Dispose();
    }

    public Task LoadIntoBufferAsync() => this.LoadIntoBufferAsync((long) int.MaxValue);

    public async Task LoadIntoBufferAsync(long maxBufferSize)
    {
      HttpContent httpContent = this;
      if (httpContent.disposed)
        throw new ObjectDisposedException(httpContent.GetType().ToString());
      if (httpContent.buffer != null)
        return;
      httpContent.buffer = HttpContent.CreateFixedMemoryStream(maxBufferSize);
      await httpContent.SerializeToStreamAsync((Stream) httpContent.buffer, (TransportContext) null).ConfigureAwait(false);
      httpContent.buffer.Seek(0L, SeekOrigin.Begin);
    }

    public async Task<Stream> ReadAsStreamAsync()
    {
      HttpContent httpContent = this;
      if (httpContent.disposed)
        throw new ObjectDisposedException(httpContent.GetType().ToString());
      if (httpContent.buffer != null)
        return (Stream) new MemoryStream(httpContent.buffer.GetBuffer(), 0, (int) httpContent.buffer.Length, false);
      if (httpContent.stream == null)
      {
        Stream stream = await httpContent.CreateContentReadStreamAsync().ConfigureAwait(false);
        httpContent.stream = stream;
      }
      return httpContent.stream;
    }

    public async Task<string> ReadAsStringAsync()
    {
      await this.LoadIntoBufferAsync().ConfigureAwait(false);
      if (this.buffer.Length == 0L)
        return string.Empty;
      byte[] buffer = this.buffer.GetBuffer();
      int length = (int) this.buffer.Length;
      int preambleLength = 0;
      Encoding encoding;
      if (this.headers != null && this.headers.ContentType != null && this.headers.ContentType.CharSet != null)
      {
        encoding = Encoding.GetEncoding(this.headers.ContentType.CharSet);
        preambleLength = HttpContent.StartsWith(buffer, length, encoding.GetPreamble());
      }
      else
        encoding = HttpContent.GetEncodingFromBuffer(buffer, length, ref preambleLength) ?? Encoding.UTF8;
      return encoding.GetString(buffer, preambleLength, length - preambleLength);
    }

    private static Encoding GetEncodingFromBuffer(
      byte[] buffer,
      int length,
      ref int preambleLength)
    {
      Encoding[] encodingArray = new Encoding[3]
      {
        Encoding.UTF8,
        Encoding.UTF32,
        Encoding.Unicode
      };
      foreach (Encoding encodingFromBuffer in encodingArray)
      {
        if ((preambleLength = HttpContent.StartsWith(buffer, length, encodingFromBuffer.GetPreamble())) != 0)
          return encodingFromBuffer;
      }
      return (Encoding) null;
    }

    private static int StartsWith(byte[] array, int length, byte[] value)
    {
      if (length < value.Length)
        return 0;
      for (int index = 0; index < value.Length; ++index)
      {
        if ((int) array[index] != (int) value[index])
          return 0;
      }
      return value.Length;
    }

    protected internal abstract Task SerializeToStreamAsync(
      Stream stream,
      TransportContext context);

    protected internal abstract bool TryComputeLength(out long length);

    private sealed class FixedMemoryStream : MemoryStream
    {
      private readonly long maxSize;

      public FixedMemoryStream(long maxSize) => this.maxSize = maxSize;

      private void CheckOverflow(int count)
      {
        if (this.Length + (long) count > this.maxSize)
          throw new HttpRequestException(string.Format("Cannot write more bytes to the buffer than the configured maximum buffer size: {0}", (object) this.maxSize));
      }

      public override void WriteByte(byte value)
      {
        this.CheckOverflow(1);
        base.WriteByte(value);
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
        this.CheckOverflow(count);
        base.Write(buffer, offset, count);
      }
    }
  }
}
