// Decompiled with JetBrains decompiler
// Type: Shelf.Droid.MainActivity
// Assembly: Shelf.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2ECED5B-D80F-4DDC-93D6-8A27414AADAF
// Assembly location: C:\Users\pc\Downloads\Shelf.Android.dll

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Shelf.Droid
{
  [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize, Icon = "@drawable/icon", Label = "Raf Takip", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/MainTheme")]
  public class MainActivity : FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      FormsAppCompatActivity.TabLayoutResource = 2130903106;
      FormsAppCompatActivity.ToolbarResource = 2130903107;
      base.OnCreate(bundle);
      Xamarin.Forms.Forms.Init((Context) this, bundle);
      this.LoadApplication((Xamarin.Forms.Application) new Shelf.App());
    }
  }
}
