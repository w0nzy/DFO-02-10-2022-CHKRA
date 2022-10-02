// Decompiled with JetBrains decompiler
// Type: Accordion.DefaultTemplate
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Shelf.Manager;
using Shelf.Views;
using System;
using Xamarin.Forms;

namespace Accordion
{
  public class DefaultTemplate : AbsoluteLayout
  {
    public DefaultTemplate()
    {
      this.Padding = new Thickness(5.0, 2.0, 5.0, 2.0);
      this.HeightRequest = 100.0;
      new Label()
      {
        HorizontalTextAlignment = TextAlignment.Start
      }.HorizontalOptions = LayoutOptions.StartAndExpand;
      new Label() { HorizontalTextAlignment = TextAlignment.End }.HorizontalOptions = LayoutOptions.End;
      Button button = new Button();
      button.HorizontalOptions = LayoutOptions.Fill;
      button.FontSize = 20.0;
      button.BackgroundColor = Color.FromRgb(83, 186, 157);
      button.TextColor = Color.White;
      Button self = button;
      self.Clicked += new EventHandler(this.btn_Clicked);
      this.Children.Add((View) self, new Rectangle(0.0, 0.5, 1.0, 1.0), AbsoluteLayoutFlags.All);
      self.SetBinding(Button.TextProperty, "Title");
    }

    public async void btn_Clicked(object sender, EventArgs e)
    {
      DefaultTemplate defaultTemplate = this;
      if (!GlobalMob.IsInternet())
      {
        int num = await Application.Current.MainPage.DisplayAlert("Hata", "İnternet Bağlantısı yok", "", "Tamam") ? 1 : 0;
      }
      else
      {
        Button button = (Button) sender;
        ContentPage contentPage = new ContentPage();
        string text = button.Text;
        if (!(text == "Ürün Topla"))
        {
          if (!(text == "Sepet Okut"))
          {
            if (!(text == "Raf Girişi(Serbest)"))
            {
              if (!(text == "Raf Sayım"))
              {
                if (text == "Raflar Arası Transfer")
                  contentPage = (ContentPage) new Picking();
              }
              else
                contentPage = (ContentPage) new Picking();
            }
            else
              contentPage = (ContentPage) new ShelfEntry();
          }
          else
            contentPage = (ContentPage) new Basket();
        }
        else
          contentPage = (ContentPage) new Picking();
        await defaultTemplate.Navigation.PushAsync((Page) contentPage);
      }
    }
  }
}
