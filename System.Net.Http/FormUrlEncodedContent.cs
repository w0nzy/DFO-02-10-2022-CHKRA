// Decompiled with JetBrains decompiler
// Type: System.Net.Http.FormUrlEncodedContent
// Assembly: System.Net.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: F33FD98C-5F10-4C88-86D7-C66C4E35D4E3
// Assembly location: C:\Users\pc\Downloads\System.Net.Http.dll

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
  public class FormUrlEncodedContent : ByteArrayContent
  {
    public FormUrlEncodedContent(
      IEnumerable<KeyValuePair<string, string>> nameValueCollection)
      : base(FormUrlEncodedContent.EncodeContent(nameValueCollection))
    {
      this.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    }

    private static byte[] EncodeContent(
      IEnumerable<KeyValuePair<string, string>> nameValueCollection)
    {
      if (nameValueCollection == null)
        throw new ArgumentNullException(nameof (nameValueCollection));
      List<byte> byteList = new List<byte>();
      foreach (KeyValuePair<string, string> nameValue in nameValueCollection)
      {
        if (byteList.Count != 0)
          byteList.Add((byte) 38);
        byte[] collection1 = FormUrlEncodedContent.SerializeValue(nameValue.Key);
        if (collection1 != null)
          byteList.AddRange((IEnumerable<byte>) collection1);
        byteList.Add((byte) 61);
        byte[] collection2 = FormUrlEncodedContent.SerializeValue(nameValue.Value);
        if (collection2 != null)
          byteList.AddRange((IEnumerable<byte>) collection2);
      }
      return byteList.ToArray();
    }

    private static byte[] SerializeValue(string value)
    {
      if (value == null)
        return (byte[]) null;
      value = System.Uri.EscapeDataString(value).Replace("%20", "+");
      return Encoding.ASCII.GetBytes(value);
    }
  }
}
