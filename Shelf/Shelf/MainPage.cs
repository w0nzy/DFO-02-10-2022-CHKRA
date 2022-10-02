// Decompiled with JetBrains decompiler
// Type: Shelf.MainPage
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Shelf.Manager;
using Shelf.Models;
using Shelf.Views;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Shelf
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\MainPage.xaml")]
  public class MainPage : ContentPage
  {
    private List<MenuModelItem> menuList;
    private bool _canClose = true;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout picking;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout basket;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout shelfentry;

    public Color ButtonColor => Color.FromRgb(218, 18, 95);

    public Color TextColor => Color.White;

    public MainPage()
    {
      this.InitializeComponent();
      this.BackgroundColor = Color.GhostWhite;
      this.Title = "Raf Takip Sistemi";
      this.menuList = new List<MenuModelItem>();
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "product.png",
        Title = "ÜRÜN TOPLA",
        ColorCode = "#030A35",
        MenuId = 1
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "basket.png",
        Title = "SEPET OKUT",
        ColorCode = "#152897",
        MenuId = 2
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "shelf.png",
        Title = "RAF GİRİŞİ (İRSALİYELİ)",
        ColorCode = "#34CBC9",
        MenuId = 3
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "shelf.png",
        Title = "RAF GİRİŞİ (SERBEST)",
        ColorCode = "#34CBC9",
        MenuId = 4
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "shelfcounting.png",
        Title = "RAF SAYIM",
        ColorCode = "#152897",
        MenuId = 5
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "shelfoutput.png",
        Title = "RAF ÇIKIŞ",
        ColorCode = "#8E5198",
        MenuId = 6
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "search.png",
        Title = "RAF BUL",
        ColorCode = "#5D30F3",
        MenuId = 7
      });
      this.menuList.Add(new MenuModelItem()
      {
        ImageName = "exit.png",
        Title = "ÇIKIŞ",
        ColorCode = "#000000",
        MenuId = 8
      });
      if (GlobalMob.User != null)
      {
        GlobalMob.User.MenuIds = ",1,2,3,4,5,6,7,8,";
        string menuIds = GlobalMob.User.MenuIds;
        this.menuList = this.menuList.Where<MenuModelItem>((Func<MenuModelItem, bool>) (x => menuIds.Contains("," + (object) x.MenuId + ","))).ToList<MenuModelItem>();
      }
      int num1 = 2;
      int num2 = this.menuList.Count<MenuModelItem>() / num1;
      Grid grid = new Grid();
      grid.BackgroundColor = Color.Black;
      grid.Margin = (Thickness) 10.0;
      for (int index = 0; index < num1; ++index)
        grid.ColumnDefinitions.Add(new ColumnDefinition()
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
      for (int index = 0; index < num2; ++index)
        grid.RowDefinitions.Add(new RowDefinition()
        {
          Height = new GridLength(1.0, GridUnitType.Star)
        });
      int left = 0;
      int top = 0;
      int num3 = 0;
      foreach (MenuModelItem menu in this.menuList)
      {
        ++num3;
        StackLayout bindable = this.AddMenuItem(menu.ImageName, menu.Title, menu.ColorCode, menu.MenuId);
        grid.Children.Add((View) bindable, left, top);
        if (num3 == this.menuList.Count<MenuModelItem>() && left < num1)
          Grid.SetColumnSpan((BindableObject) bindable, num1 - left);
        if (left == num1 - 1)
        {
          ++top;
          left = 0;
        }
        else
          ++left;
      }
      this.Content = (View) grid;
    }

    private StackLayout AddMenuItem(
      string image,
      string title,
      string colorCode,
      int menuId)
    {
      StackLayout stackLayout1 = new StackLayout();
      stackLayout1.BackgroundColor = Color.White;
      stackLayout1.VerticalOptions = LayoutOptions.FillAndExpand;
      StackLayout stackLayout2 = new StackLayout();
      stackLayout2.VerticalOptions = LayoutOptions.CenterAndExpand;
      stackLayout2.HorizontalOptions = LayoutOptions.CenterAndExpand;
      Frame frame = new Frame();
      frame.BackgroundColor = Color.FromHex(colorCode);
      frame.HasShadow = false;
      frame.CornerRadius = 50f;
      frame.HeightRequest = 50.0;
      frame.WidthRequest = 50.0;
      frame.HorizontalOptions = LayoutOptions.CenterAndExpand;
      frame.VerticalOptions = LayoutOptions.Center;
      StackLayout stackLayout3 = new StackLayout();
      stackLayout3.VerticalOptions = LayoutOptions.Center;
      Image image1 = new Image();
      image1.Source = (ImageSource) image;
      image1.HorizontalOptions = LayoutOptions.Center;
      image1.VerticalOptions = LayoutOptions.Start;
      stackLayout3.Children.Add((View) image1);
      frame.Content = (View) stackLayout3;
      Label label = new Label();
      label.Text = title;
      label.Margin = (Thickness) 0.0;
      label.TextColor = Color.Black;
      label.VerticalOptions = LayoutOptions.Center;
      label.HorizontalOptions = LayoutOptions.Center;
      label.FontAttributes = FontAttributes.Bold;
      label.HorizontalTextAlignment = TextAlignment.Start;
      label.VerticalTextAlignment = TextAlignment.End;
      stackLayout2.Children.Add((View) frame);
      stackLayout2.Children.Add((View) label);
      stackLayout1.Children.Add((View) stackLayout2);
      TapGestureRecognizer gestureRecognizer = new TapGestureRecognizer();
      gestureRecognizer.CommandParameter = (object) menuId;
      gestureRecognizer.Tapped += (EventHandler) ((s, e) => this.OnTapped(s, e));
      stackLayout1.GestureRecognizers.Add((IGestureRecognizer) gestureRecognizer);
      return stackLayout1;
    }

    protected override bool OnBackButtonPressed()
    {
      if (this._canClose)
        this.ShowExitDialog();
      return this._canClose;
    }

    private async void ShowExitDialog()
    {
      MainPage mainPage = this;
      if (!await mainPage.DisplayAlert("Çıkış?", "Programdan çıkmak istiyor musunuz?", "Evet", "Hayır"))
        return;
      GlobalMob.Exit();
      mainPage._canClose = false;
      mainPage.OnBackButtonPressed();
    }

    public async void BackButtonPressed(bool isExit)
    {
      isExit = await this.DisplayAlert("Çıkış?", "Programdan çıkmak istiyor musunuz?", "Evet", "Hayır");
      if (!isExit)
        return;
      GlobalMob.Exit();
    }

    private async void OnTapped(object sender, EventArgs e)
    {
      MainPage mainPage = this;
      StackLayout stck = (StackLayout) sender;
      stck.Opacity = 0.9;
      string str = Convert.ToString(((TappedEventArgs) e).Parameter);
      Page page = (Page) null;
      switch (str)
      {
        case "1":
          page = (Page) new Picking();
          break;
        case "2":
          page = (Page) new Basket();
          break;
        case "3":
          page = (Page) new ShelfEntry2();
          break;
        case "4":
          page = (Page) new ShelfEntry();
          break;
        case "5":
          page = (Page) new ShelfCounting();
          break;
        case "6":
          page = (Page) new ShelfOutput();
          break;
        case "7":
          page = (Page) new ShelfSearch();
          break;
        case "8":
          if (await mainPage.DisplayAlert("Çıkış?", "Programdan çıkmak istiyor musunuz?", "Evet", "Hayır"))
          {
            GlobalMob.Exit();
            return;
          }
          stck.Opacity = 1.0;
          return;
      }
      if (page == null)
        stck.Opacity = 1.0;
      else if (!GlobalMob.IsInternet())
      {
        stck.Opacity = 1.0;
        int num = await mainPage.DisplayAlert("Hata", "İnternet Bağlantısı yok", "", "Tamam") ? 1 : 0;
      }
      else
      {
        await mainPage.Navigation.PushAsync(page);
        stck.Opacity = 1.0;
      }
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.MainPage.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        RowDefinition bindable1 = new RowDefinition();
        RowDefinition bindable2 = new RowDefinition();
        RowDefinition bindable3 = new RowDefinition();
        ColumnDefinition bindable4 = new ColumnDefinition();
        ColumnDefinition bindable5 = new ColumnDefinition();
        TapGestureRecognizer bindable6 = new TapGestureRecognizer();
        BoxView bindable7 = new BoxView();
        Image bindable8 = new Image();
        Label bindable9 = new Label();
        StackLayout stackLayout1 = new StackLayout();
        TapGestureRecognizer bindable10 = new TapGestureRecognizer();
        BoxView bindable11 = new BoxView();
        Image bindable12 = new Image();
        Label bindable13 = new Label();
        StackLayout stackLayout2 = new StackLayout();
        TapGestureRecognizer bindable14 = new TapGestureRecognizer();
        BoxView bindable15 = new BoxView();
        Image bindable16 = new Image();
        Label bindable17 = new Label();
        StackLayout stackLayout3 = new StackLayout();
        TapGestureRecognizer bindable18 = new TapGestureRecognizer();
        BoxView bindable19 = new BoxView();
        Image bindable20 = new Image();
        Label bindable21 = new Label();
        StackLayout bindable22 = new StackLayout();
        TapGestureRecognizer bindable23 = new TapGestureRecognizer();
        BoxView bindable24 = new BoxView();
        Image bindable25 = new Image();
        Label bindable26 = new Label();
        StackLayout bindable27 = new StackLayout();
        TapGestureRecognizer bindable28 = new TapGestureRecognizer();
        BoxView bindable29 = new BoxView();
        Image bindable30 = new Image();
        Label bindable31 = new Label();
        StackLayout bindable32 = new StackLayout();
        Grid bindable33 = new Grid();
        MainPage mainPage = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) mainPage, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("mainPage", (object) mainPage);
        NameScope.SetNameScope((BindableObject) bindable33, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable4, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable5, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("picking", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) bindable6, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable7, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable8, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable9, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("basket", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) bindable10, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable11, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable12, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable13, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) stackLayout3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("shelfentry", (object) stackLayout3);
        NameScope.SetNameScope((BindableObject) bindable14, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable15, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable16, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable17, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable22, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable18, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable19, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable20, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable21, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable27, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable23, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable24, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable25, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable26, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable32, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable28, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable29, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable30, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable31, (INameScope) nameScope);
        this.picking = stackLayout1;
        this.basket = stackLayout2;
        this.shelfentry = stackLayout3;
        bindable33.SetValue(VisualElement.BackgroundColorProperty, (object) Color.Black);
        bindable33.SetValue(View.MarginProperty, (object) new Thickness(10.0));
        bindable1.SetValue(RowDefinition.HeightProperty, new GridLengthTypeConverter().ConvertFromInvariantString("*"));
        bindable33.RowDefinitions.Add(bindable1);
        bindable2.SetValue(RowDefinition.HeightProperty, new GridLengthTypeConverter().ConvertFromInvariantString("*"));
        bindable33.RowDefinitions.Add(bindable2);
        bindable3.SetValue(RowDefinition.HeightProperty, new GridLengthTypeConverter().ConvertFromInvariantString("*"));
        bindable33.RowDefinitions.Add(bindable3);
        bindable4.SetValue(ColumnDefinition.WidthProperty, new GridLengthTypeConverter().ConvertFromInvariantString("*"));
        bindable33.ColumnDefinitions.Add(bindable4);
        bindable5.SetValue(ColumnDefinition.WidthProperty, new GridLengthTypeConverter().ConvertFromInvariantString("*"));
        bindable33.ColumnDefinitions.Add(bindable5);
        stackLayout1.SetValue(Grid.ColumnProperty, (object) 0);
        stackLayout1.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout1.SetValue(Grid.RowProperty, (object) 0);
        stackLayout1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable6.Tapped += new EventHandler(mainPage.OnTapped);
        bindable6.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "1");
        stackLayout1.GestureRecognizers.Add((IGestureRecognizer) bindable6);
        bindable7.SetValue(Grid.RowProperty, (object) 0);
        bindable7.SetValue(Grid.ColumnProperty, (object) 0);
        bindable7.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable7.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout1.Children.Add((View) bindable7);
        bindable8.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("product.png"));
        bindable8.SetValue(Grid.ColumnProperty, (object) 0);
        bindable8.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable8.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable8.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.Children.Add((View) bindable8);
        bindable9.SetValue(Label.TextProperty, (object) "ÜRÜN TOPLA");
        bindable9.SetValue(Grid.RowProperty, (object) 0);
        bindable9.SetValue(Grid.ColumnProperty, (object) 0);
        bindable9.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable9.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable9.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable9.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable9.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable9.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable9.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        stackLayout1.Children.Add((View) bindable9);
        bindable33.Children.Add((View) stackLayout1);
        stackLayout2.SetValue(Grid.RowProperty, (object) 0);
        stackLayout2.SetValue(Grid.ColumnProperty, (object) 1);
        stackLayout2.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout2.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable10.Tapped += new EventHandler(mainPage.OnTapped);
        bindable10.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "2");
        stackLayout2.GestureRecognizers.Add((IGestureRecognizer) bindable10);
        bindable11.SetValue(Grid.RowProperty, (object) 0);
        bindable11.SetValue(Grid.ColumnProperty, (object) 1);
        bindable11.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable11.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout2.Children.Add((View) bindable11);
        bindable12.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("basket.png"));
        bindable12.SetValue(Grid.RowProperty, (object) 0);
        bindable12.SetValue(Grid.ColumnProperty, (object) 1);
        bindable12.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable12.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable12.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout2.Children.Add((View) bindable12);
        bindable13.SetValue(Label.TextProperty, (object) "SEPET OKUT");
        bindable13.SetValue(Grid.RowProperty, (object) 0);
        bindable13.SetValue(Grid.ColumnProperty, (object) 1);
        bindable13.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable13.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable13.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable13.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable13.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable13.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable13.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        stackLayout2.Children.Add((View) bindable13);
        bindable33.Children.Add((View) stackLayout2);
        stackLayout3.SetValue(Grid.RowProperty, (object) 1);
        stackLayout3.SetValue(Grid.ColumnProperty, (object) 0);
        stackLayout3.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout3.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable14.Tapped += new EventHandler(mainPage.OnTapped);
        bindable14.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "3");
        stackLayout3.GestureRecognizers.Add((IGestureRecognizer) bindable14);
        bindable15.SetValue(Grid.RowProperty, (object) 1);
        bindable15.SetValue(Grid.ColumnProperty, (object) 0);
        bindable15.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable15.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        stackLayout3.Children.Add((View) bindable15);
        bindable16.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("shelf.png"));
        bindable16.SetValue(Grid.RowProperty, (object) 1);
        bindable16.SetValue(Grid.ColumnProperty, (object) 0);
        bindable16.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable16.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable16.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout3.Children.Add((View) bindable16);
        bindable17.SetValue(Label.TextProperty, (object) "RAF GİRİŞİ (İRSALİYELİ)");
        bindable17.SetValue(Grid.RowProperty, (object) 1);
        bindable17.SetValue(Grid.ColumnProperty, (object) 0);
        bindable17.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable17.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable17.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable17.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable17.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable17.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable17.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        stackLayout3.Children.Add((View) bindable17);
        bindable33.Children.Add((View) stackLayout3);
        bindable22.SetValue(Grid.RowProperty, (object) 1);
        bindable22.SetValue(Grid.ColumnProperty, (object) 1);
        bindable22.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable22.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable18.Tapped += new EventHandler(mainPage.OnTapped);
        bindable18.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "4");
        bindable22.GestureRecognizers.Add((IGestureRecognizer) bindable18);
        bindable19.SetValue(Grid.RowProperty, (object) 1);
        bindable19.SetValue(Grid.ColumnProperty, (object) 1);
        bindable19.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable19.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable22.Children.Add((View) bindable19);
        bindable20.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("shelf.png"));
        bindable20.SetValue(Grid.RowProperty, (object) 1);
        bindable20.SetValue(Grid.ColumnProperty, (object) 1);
        bindable20.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable20.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable20.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable22.Children.Add((View) bindable20);
        bindable21.SetValue(Label.TextProperty, (object) "RAF GİRİŞİ (SERBEST)");
        bindable21.SetValue(Grid.RowProperty, (object) 1);
        bindable21.SetValue(Grid.ColumnProperty, (object) 1);
        bindable21.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable21.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable21.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable21.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable21.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable21.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable21.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable22.Children.Add((View) bindable21);
        bindable33.Children.Add((View) bindable22);
        bindable27.SetValue(Grid.RowProperty, (object) 2);
        bindable27.SetValue(Grid.ColumnProperty, (object) 0);
        bindable27.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable27.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable23.Tapped += new EventHandler(mainPage.OnTapped);
        bindable23.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "5");
        bindable27.GestureRecognizers.Add((IGestureRecognizer) bindable23);
        bindable24.SetValue(Grid.RowProperty, (object) 2);
        bindable24.SetValue(Grid.ColumnProperty, (object) 0);
        bindable24.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable24.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable27.Children.Add((View) bindable24);
        bindable25.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("shelfcounting.png"));
        bindable25.SetValue(Grid.RowProperty, (object) 2);
        bindable25.SetValue(Grid.ColumnProperty, (object) 0);
        bindable25.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable25.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable25.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable27.Children.Add((View) bindable25);
        bindable26.SetValue(Label.TextProperty, (object) "RAF SAYIM");
        bindable26.SetValue(Grid.RowProperty, (object) 2);
        bindable26.SetValue(Grid.ColumnProperty, (object) 0);
        bindable26.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable26.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable26.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable26.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable26.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable26.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable26.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable27.Children.Add((View) bindable26);
        bindable33.Children.Add((View) bindable27);
        bindable32.SetValue(Grid.RowProperty, (object) 2);
        bindable32.SetValue(Grid.ColumnProperty, (object) 1);
        bindable32.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable32.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable28.Tapped += new EventHandler(mainPage.OnTapped);
        bindable28.SetValue(TapGestureRecognizer.CommandParameterProperty, (object) "6");
        bindable32.GestureRecognizers.Add((IGestureRecognizer) bindable28);
        bindable29.SetValue(Grid.RowProperty, (object) 2);
        bindable29.SetValue(Grid.ColumnProperty, (object) 1);
        bindable29.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable29.SetValue(VisualElement.BackgroundColorProperty, (object) Color.White);
        bindable32.Children.Add((View) bindable29);
        bindable30.SetValue(Image.SourceProperty, new ImageSourceConverter().ConvertFromInvariantString("shelfoutput.png"));
        bindable30.SetValue(Grid.RowProperty, (object) 2);
        bindable30.SetValue(Grid.ColumnProperty, (object) 1);
        bindable30.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable30.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable30.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable32.Children.Add((View) bindable30);
        bindable31.SetValue(Label.TextProperty, (object) "RAF ÇIKIŞ");
        bindable31.SetValue(Grid.RowProperty, (object) 2);
        bindable31.SetValue(Grid.ColumnProperty, (object) 1);
        bindable31.SetValue(View.MarginProperty, (object) new Thickness(0.0));
        bindable31.SetValue(Label.TextColorProperty, (object) Color.Black);
        bindable31.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        bindable31.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable31.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        bindable31.SetValue(Label.HorizontalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable31.SetValue(Label.VerticalTextAlignmentProperty, (object) TextAlignment.Center);
        bindable32.Children.Add((View) bindable31);
        bindable33.Children.Add((View) bindable32);
        mainPage.SetValue(ContentPage.ContentProperty, (object) bindable33);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<MainPage>(typeof (MainPage));
      this.picking = NameScopeExtensions.FindByName<StackLayout>(this, "picking");
      this.basket = NameScopeExtensions.FindByName<StackLayout>(this, "basket");
      this.shelfentry = NameScopeExtensions.FindByName<StackLayout>(this, "shelfentry");
    }
  }
}
