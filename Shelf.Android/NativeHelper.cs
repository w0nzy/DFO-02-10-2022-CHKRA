// Decompiled with JetBrains decompiler
// Type: NativeHelper
// Assembly: Shelf.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2ECED5B-D80F-4DDC-93D6-8A27414AADAF
// Assembly location: C:\Users\pc\Downloads\Shelf.Android.dll

using Android.OS;

public class NativeHelper : INativeHelper
{
  public void CloseApp() => Process.KillProcess(Process.MyPid());

  public string GetSerialNumber() => Build.Serial;
}
