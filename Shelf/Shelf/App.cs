// Decompiled with JetBrains decompiler
// Type: Shelf.App
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Shelf.Manager;
using Shelf.Views;
using System.CodeDom.Compiler;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shelf
{
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\App.xaml")]
  public class App : Application
  {
    public App()
    {
      this.InitializeComponent();
      GlobalMob.ButtonColor = Color.FromRgb(218, 18, 95);
      GlobalMob.TextColor = Color.White;
      this.MainPage = (Page) new NavigationPage((Page) new Login())
      {
        BarBackgroundColor = GlobalMob.ButtonColor
      };
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent() => this.LoadFromXaml<App>(typeof (App));
  }
}
