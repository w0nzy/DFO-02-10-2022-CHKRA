// Decompiled with JetBrains decompiler
// Type: Shelf.Views.Login
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Newtonsoft.Json;
using Shelf.Helpers;
using Shelf.Manager;
using Shelf.Models;
using System;
using System.CodeDom.Compiler;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Internals;

namespace Shelf.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\Login.xaml")]
  public class Login : ContentPage
  {
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtUserName;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtPassword;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtServer;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Switch chkRememberMe;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnLogin;

    public Login()
    {
      this.InitializeComponent();
      this.btnLogin.BackgroundColor = Color.FromRgb(218, 18, 95);
      this.btnLogin.TextColor = GlobalMob.TextColor;
      this.Title = "Kullanıcı Girişi";
      this.txtServer.Text = "192.168.1.176:81";
      this.txtServer.Text = "";
      string userName = Settings.UserName;
      string password = Settings.Password;
      string server = Settings.Server;
      this.chkRememberMe.IsToggled = true;
      this.txtUserName.Text = !string.IsNullOrEmpty(userName) ? userName : "";
      this.txtPassword.Text = !string.IsNullOrEmpty(password) ? password : "";
      this.txtServer.Text = !string.IsNullOrEmpty(server) ? server : "";
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
      Login login = this;
      string serialNumber = DependencyService.Get<INativeHelper>().GetSerialNumber();
      string text1 = login.txtUserName.Text;
      string text2 = login.txtPassword.Text;
      GlobalMob.ServerName = login.txtServer.Text;
      try
      {
        ztIOShelfUser ztIoShelfUser = JsonConvert.DeserializeObject<ztIOShelfUser>(GlobalMob.PostJson(string.Format("Login?userName={0}&password={1}&serial={2}", new object[3]
        {
          (object) text1,
          (object) text2,
          (object) serialNumber
        })));
        if (ztIoShelfUser != null)
        {
          if (ztIoShelfUser.ShelfUserID != -1)
          {
            Settings.UserName = login.chkRememberMe.IsToggled ? text1 : "";
            Settings.Password = login.chkRememberMe.IsToggled ? text2 : "";
            Settings.Server = login.chkRememberMe.IsToggled ? login.txtServer.Text : "";
            GlobalMob.User = ztIoShelfUser;
            Application.Current.MainPage = (Page) new NavigationPage((Page) new MainPage())
            {
              BarBackgroundColor = GlobalMob.ButtonColor
            };
          }
          else
          {
            int num1 = await login.DisplayAlert("Lisans Hatası", "Kullanıcı sayısı lisans hakkını aşmıştır.", "", "Tamam") ? 1 : 0;
          }
        }
        else
        {
          int num2 = await login.DisplayAlert("Hata", "Kullanıcı adı veya şifre yanlış", "", "Tamam") ? 1 : 0;
        }
      }
      catch (Exception ex)
      {
        int num = await login.DisplayAlert("Hata", "Servera ulaşılamadı", "", "Tamam") ? 1 : 0;
      }
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.Login.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        Image bindable1 = new Image();
        Label bindable2 = new Label();
        StackLayout bindable3 = new StackLayout();
        Image bindable4 = new Image();
        Xamarin.Forms.Entry entry1 = new Xamarin.Forms.Entry();
        StackLayout bindable5 = new StackLayout();
        Image bindable6 = new Image();
        Xamarin.Forms.Entry entry2 = new Xamarin.Forms.Entry();
        StackLayout bindable7 = new StackLayout();
        Image bindable8 = new Image();
        Xamarin.Forms.Entry entry3 = new Xamarin.Forms.Entry();
        StackLayout bindable9 = new StackLayout();
        Label bindable10 = new Label();
        Switch @switch = new Switch();
        StackLayout bindable11 = new StackLayout();
        StackLayout bindable12 = new StackLayout();
        Button button1 = new Button();
        StackLayout bindable13 = new StackLayout();
        StackLayout bindable14 = new StackLayout();
        Label bindable15 = new Label();
        StackLayout bindable16 = new StackLayout();
        StackLayout bindable17 = new StackLayout();
        Login bindable18 = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) bindable18, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable17, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable14, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable12, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable5, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable4, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) entry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtUserName", (object) entry1);
        NameScope.SetNameScope((BindableObject) bindable7, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable6, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) entry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtPassword", (object) entry2);
        NameScope.SetNameScope((BindableObject) bindable9, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable8, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) entry3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtServer", (object) entry3);
        NameScope.SetNameScope((BindableObject) bindable11, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable10, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) @switch, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("chkRememberMe", (object) @switch);
        NameScope.SetNameScope((BindableObject) bindable13, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) button1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnLogin", (object) button1);
        NameScope.SetNameScope((BindableObject) bindable16, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable15, (INameScope) nameScope);
        this.txtUserName = entry1;
        this.txtPassword = entry2;
        this.txtServer = entry3;
        this.chkRememberMe = @switch;
        this.btnLogin = button1;
        bindable17.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable17.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable3.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Start);
        bindable1.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("lock.png"));
        bindable1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable3.Children.Add((View) bindable1);
        Label label1 = bindable2;
        BindableProperty fontSizeProperty1 = Label.FontSizeProperty;
        FontSizeConverter fontSizeConverter1 = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider1 = new XamlServiceProvider();
        Type type1 = typeof (IProvideValueTarget);
        object[] objectAndParents1 = new object[0 + 4];
        objectAndParents1[0] = (object) bindable2;
        objectAndParents1[1] = (object) bindable3;
        objectAndParents1[2] = (object) bindable17;
        objectAndParents1[3] = (object) bindable18;
        SimpleValueTargetProvider service1 = new SimpleValueTargetProvider(objectAndParents1, (object) Label.FontSizeProperty);
        xamlServiceProvider1.Add(type1, (object) service1);
        xamlServiceProvider1.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type2 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver1 = new XmlNamespaceResolver();
        namespaceResolver1.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver1.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        XamlTypeResolver service2 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver1, typeof (Login).Assembly);
        xamlServiceProvider1.Add(type2, (object) service2);
        xamlServiceProvider1.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(10, 24)));
        object obj1 = ((IExtendedTypeConverter) fontSizeConverter1).ConvertFromInvariantString("Large", (IServiceProvider) xamlServiceProvider1);
        label1.SetValue(fontSizeProperty1, obj1);
        bindable2.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable2.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable2.SetValue(Label.TextProperty, (object) "Kullanıcı Girişi");
        bindable3.Children.Add((View) bindable2);
        bindable17.Children.Add((View) bindable3);
        bindable14.SetValue(View.MarginProperty, (object) new Thickness(10.0, 0.0, 10.0, 0.0));
        bindable14.SetValue(Layout.PaddingProperty, (object) new Thickness(10.0, 0.0, 10.0, 0.0));
        bindable14.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Fill);
        bindable14.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable12.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable5.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        bindable4.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("user.png"));
        bindable4.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Start);
        bindable5.Children.Add((View) bindable4);
        entry1.SetValue(Xamarin.Forms.Entry.FontAttributesProperty, (object) FontAttributes.Bold);
        entry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Kullanıcı Adı Giriniz...");
        entry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable5.Children.Add((View) entry1);
        bindable12.Children.Add((View) bindable5);
        bindable7.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        bindable6.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("password.png"));
        bindable6.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Start);
        bindable7.Children.Add((View) bindable6);
        entry2.SetValue(Xamarin.Forms.Entry.FontAttributesProperty, (object) FontAttributes.Bold);
        entry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Şifre Giriniz...");
        entry2.SetValue(Xamarin.Forms.Entry.IsPasswordProperty, (object) true);
        entry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable7.Children.Add((View) entry2);
        bindable12.Children.Add((View) bindable7);
        bindable9.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        bindable8.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("server.png"));
        bindable8.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Start);
        bindable9.Children.Add((View) bindable8);
        entry3.SetValue(Xamarin.Forms.Entry.FontAttributesProperty, (object) FontAttributes.Bold);
        entry3.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Server Adresi Giriniz...");
        entry3.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable9.Children.Add((View) entry3);
        bindable12.Children.Add((View) bindable9);
        bindable11.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        bindable11.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable11.SetValue(View.MarginProperty, (object) new Thickness(5.0));
        bindable10.SetValue(Label.TextProperty, (object) "Beni Hatırla");
        Label label2 = bindable10;
        BindableProperty fontSizeProperty2 = Label.FontSizeProperty;
        FontSizeConverter fontSizeConverter2 = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider2 = new XamlServiceProvider();
        Type type3 = typeof (IProvideValueTarget);
        object[] objectAndParents2 = new object[0 + 6];
        objectAndParents2[0] = (object) bindable10;
        objectAndParents2[1] = (object) bindable11;
        objectAndParents2[2] = (object) bindable12;
        objectAndParents2[3] = (object) bindable14;
        objectAndParents2[4] = (object) bindable17;
        objectAndParents2[5] = (object) bindable18;
        SimpleValueTargetProvider service3 = new SimpleValueTargetProvider(objectAndParents2, (object) Label.FontSizeProperty);
        xamlServiceProvider2.Add(type3, (object) service3);
        xamlServiceProvider2.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type4 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver2 = new XmlNamespaceResolver();
        namespaceResolver2.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver2.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        XamlTypeResolver service4 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver2, typeof (Login).Assembly);
        xamlServiceProvider2.Add(type4, (object) service4);
        xamlServiceProvider2.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(31, 52)));
        object obj2 = ((IExtendedTypeConverter) fontSizeConverter2).ConvertFromInvariantString("Medium", (IServiceProvider) xamlServiceProvider2);
        label2.SetValue(fontSizeProperty2, obj2);
        bindable10.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable10.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Start);
        bindable10.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable11.Children.Add((View) bindable10);
        @switch.SetValue(VisualElement.ScaleProperty, (object) 1.0);
        @switch.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Start);
        @switch.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable11.Children.Add((View) @switch);
        bindable12.Children.Add((View) bindable11);
        bindable14.Children.Add((View) bindable12);
        bindable13.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        button1.SetValue(Button.TextProperty, (object) "Giriş");
        button1.SetValue(Button.FontAttributesProperty, (object) FontAttributes.Bold);
        button1.Clicked += new EventHandler(bindable18.btnLogin_Clicked);
        Button button2 = button1;
        BindableProperty fontSizeProperty3 = Button.FontSizeProperty;
        FontSizeConverter fontSizeConverter3 = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider3 = new XamlServiceProvider();
        Type type5 = typeof (IProvideValueTarget);
        object[] objectAndParents3 = new object[0 + 5];
        objectAndParents3[0] = (object) button1;
        objectAndParents3[1] = (object) bindable13;
        objectAndParents3[2] = (object) bindable14;
        objectAndParents3[3] = (object) bindable17;
        objectAndParents3[4] = (object) bindable18;
        SimpleValueTargetProvider service5 = new SimpleValueTargetProvider(objectAndParents3, (object) Button.FontSizeProperty);
        xamlServiceProvider3.Add(type5, (object) service5);
        xamlServiceProvider3.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type6 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver3 = new XmlNamespaceResolver();
        namespaceResolver3.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver3.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        XamlTypeResolver service6 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver3, typeof (Login).Assembly);
        xamlServiceProvider3.Add(type6, (object) service6);
        xamlServiceProvider3.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(37, 29)));
        object obj3 = ((IExtendedTypeConverter) fontSizeConverter3).ConvertFromInvariantString("Medium", (IServiceProvider) xamlServiceProvider3);
        button2.SetValue(fontSizeProperty3, obj3);
        button1.SetValue(VisualElement.BackgroundColorProperty, (object) new Color(0.85490196943283081, 0.070588238537311554, 0.37254902720451355, 1.0));
        button1.SetValue(Button.TextColorProperty, (object) Color.White);
        button1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable13.Children.Add((View) button1);
        bindable14.Children.Add((View) bindable13);
        bindable17.Children.Add((View) bindable14);
        bindable16.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.End);
        Label label3 = bindable15;
        BindableProperty fontSizeProperty4 = Label.FontSizeProperty;
        FontSizeConverter fontSizeConverter4 = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider4 = new XamlServiceProvider();
        Type type7 = typeof (IProvideValueTarget);
        object[] objectAndParents4 = new object[0 + 4];
        objectAndParents4[0] = (object) bindable15;
        objectAndParents4[1] = (object) bindable16;
        objectAndParents4[2] = (object) bindable17;
        objectAndParents4[3] = (object) bindable18;
        SimpleValueTargetProvider service7 = new SimpleValueTargetProvider(objectAndParents4, (object) Label.FontSizeProperty);
        xamlServiceProvider4.Add(type7, (object) service7);
        xamlServiceProvider4.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type8 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver4 = new XmlNamespaceResolver();
        namespaceResolver4.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver4.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        XamlTypeResolver service8 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver4, typeof (Login).Assembly);
        xamlServiceProvider4.Add(type8, (object) service8);
        xamlServiceProvider4.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(41, 24)));
        object obj4 = ((IExtendedTypeConverter) fontSizeConverter4).ConvertFromInvariantString("Small", (IServiceProvider) xamlServiceProvider4);
        label3.SetValue(fontSizeProperty4, obj4);
        bindable15.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable15.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable15.SetValue(View.MarginProperty, (object) new Thickness(50.0));
        bindable15.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable15.SetValue(Label.TextProperty, (object) "iontegra");
        bindable16.Children.Add((View) bindable15);
        bindable17.Children.Add((View) bindable16);
        bindable18.SetValue(ContentPage.ContentProperty, (object) bindable17);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<Login>(typeof (Login));
      this.txtUserName = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtUserName");
      this.txtPassword = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtPassword");
      this.txtServer = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtServer");
      this.chkRememberMe = NameScopeExtensions.FindByName<Switch>(this, "chkRememberMe");
      this.btnLogin = NameScopeExtensions.FindByName<Button>(this, "btnLogin");
    }
  }
}
