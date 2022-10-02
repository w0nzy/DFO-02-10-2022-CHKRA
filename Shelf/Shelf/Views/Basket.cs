// Decompiled with JetBrains decompiler
// Type: Shelf.Views.Basket
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Newtonsoft.Json;
using Shelf.Manager;
using Shelf.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Internals;
using XFNoSoftKeyboadEntryControl;

namespace Shelf.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\Basket.xaml")]
  public class Basket : ContentPage
  {
    private List<pIOShelfOrderDetailBasketReturnModel> shelfOrderDetail;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckContent;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckShelfOrderList;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckEmptyMessage;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstShelfOrder;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckForm;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtShelfOrderNumber;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtQty;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnShelfOrderApproved;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstBasket;

    public Color ButtonColor => Color.FromRgb(21, 40, 151);

    public Color TextColor => Color.White;

    public Basket()
    {
      this.InitializeComponent();
      this.Title = "Raf Emirleri";
      this.lstBasket.BackgroundColor = Color.DarkGray;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
      List<pIOUserShelfOrdersReturnModel> ordersReturnModelList1 = JsonConvert.DeserializeObject<List<pIOUserShelfOrdersReturnModel>>(GlobalMob.PostJson(string.Format("GetUserShelfOrdersBasket?userID={0}", new object[1]
      {
        (object) GlobalMob.User.ShelfUserID
      })));
      List<pIOUserShelfOrdersReturnModel> ordersReturnModelList2 = ordersReturnModelList1 == null ? new List<pIOUserShelfOrdersReturnModel>() : ordersReturnModelList1;
      this.lstShelfOrder.IsVisible = ordersReturnModelList2.Count > 0;
      this.stckEmptyMessage.IsVisible = ordersReturnModelList2.Count == 0;
      if (this.stckEmptyMessage.IsVisible)
        this.stckContent.VerticalOptions = LayoutOptions.Center;
      this.lstShelfOrder.BindingContext = (object) ordersReturnModelList2;
      this.lstShelfOrder.ItemSelected += (EventHandler<SelectedItemChangedEventArgs>) ((sender, e) =>
      {
        object selectedItem = ((ListView) sender).SelectedItem;
        if (selectedItem == null)
          return;
        this.Title = "Sepet Okut";
        pIOUserShelfOrdersReturnModel ordersReturnModel = (pIOUserShelfOrdersReturnModel) selectedItem;
        this.txtShelfOrderNumber.Text = "";
        this.txtShelfOrderNumber.Text = ordersReturnModel.ShelfOrderNumber.Replace("S", "");
        this.stckShelfOrderList.IsVisible = false;
        this.stckForm.IsVisible = true;
        this.FillListView();
        this.txtBarcode.Focus();
      });
    }

    private void FillListView(bool isBarcode = false)
    {
      this.shelfOrderDetail = JsonConvert.DeserializeObject<List<pIOShelfOrderDetailBasketReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfOrderDetailBasket?shelfOrderNumber=S{0}", new object[1]
      {
        (object) this.txtShelfOrderNumber.Text
      })));
      this.lstBasket.ItemsSource = (IEnumerable) null;
      List<pIOShelfOrderDetailBasketReturnModel> source = this.GroupList();
      if (isBarcode)
      {
        pIOShelfOrderDetailBasketReturnModel basketReturnModel = source.Where<pIOShelfOrderDetailBasketReturnModel>((Func<pIOShelfOrderDetailBasketReturnModel, bool>) (x => x.Barcode.Contains(this.txtBarcode.Text))).FirstOrDefault<pIOShelfOrderDetailBasketReturnModel>();
        if (basketReturnModel != null)
          basketReturnModel.IsFirst = true;
      }
      this.lstBasket.ItemsSource = (IEnumerable) source.OrderBy<pIOShelfOrderDetailBasketReturnModel, bool>((Func<pIOShelfOrderDetailBasketReturnModel, bool>) (x => x.IsFirst)).ToList<pIOShelfOrderDetailBasketReturnModel>();
      this.stckBarcode.IsVisible = true;
      this.lstBasket.IsVisible = true;
      this.btnShelfOrderApproved.IsVisible = true;
    }

    private List<pIOShelfOrderDetailBasketReturnModel> GroupList() => this.shelfOrderDetail.GroupBy(c => new
    {
      ItemCode = c.ItemCode,
      ItemDim1Code = c.ItemDim1Code,
      ItemDim2Code = c.ItemDim2Code,
      Barcode = c.Barcode,
      ColorCode = c.ColorCode,
      ItemDescription = c.ItemDescription
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType0<string, string, string, string, string, string>, pIOShelfOrderDetailBasketReturnModel>, pIOShelfOrderDetailBasketReturnModel>(gcs => new pIOShelfOrderDetailBasketReturnModel()
    {
      ItemCode = gcs.Key.ItemCode,
      ItemDim1Code = gcs.Key.ItemDim1Code,
      ItemDim2Code = gcs.Key.ItemDim2Code,
      ColorCode = gcs.Key.ColorCode,
      Barcode = gcs.Key.Barcode,
      ItemDescription = gcs.Key.ItemDescription,
      ApproveQty = gcs.Sum<pIOShelfOrderDetailBasketReturnModel>((Func<pIOShelfOrderDetailBasketReturnModel, double>) (c => c.ApproveQty)),
      PickingQty = gcs.Sum<pIOShelfOrderDetailBasketReturnModel>((Func<pIOShelfOrderDetailBasketReturnModel, double>) (c => c.PickingQty)),
      IsFirst = gcs.Max<pIOShelfOrderDetailBasketReturnModel, bool>((Func<pIOShelfOrderDetailBasketReturnModel, bool>) (c => c.IsFirst))
    }).ToList<pIOShelfOrderDetailBasketReturnModel>();

    private async void txtBarcode_Completed(object sender, EventArgs e)
    {
      Basket basket = this;
      if (string.IsNullOrEmpty(basket.txtBarcode.Text))
        return;
      // ISSUE: reference to a compiler-generated method
      List<pIOShelfOrderDetailBasketReturnModel> list = basket.shelfOrderDetail.Where<pIOShelfOrderDetailBasketReturnModel>(new Func<pIOShelfOrderDetailBasketReturnModel, bool>(basket.\u003CtxtBarcode_Completed\u003Eb__9_0)).ToList<pIOShelfOrderDetailBasketReturnModel>();
      if (list.Count<pIOShelfOrderDetailBasketReturnModel>() == 0)
      {
        GlobalMob.PlayError();
        int num = await basket.DisplayAlert("Bilgi", "Miktar Yetersiz", "", "Tamam") ? 1 : 0;
        basket.txtBarcode.Text = "";
        basket.txtBarcode.Focus();
      }
      else
      {
        List<ztIOShelfOrderDetail> shelfOrderDetailList = JsonConvert.DeserializeObject<List<ztIOShelfOrderDetail>>(GlobalMob.PostJson(string.Format("UpdateApproveShelfOrderDetail?shelfOrderDetailIDs={0}&pickQty={1}&barcode={2}", new object[3]
        {
          (object) string.Join<int>(",", (IEnumerable<int>) list.Select<pIOShelfOrderDetailBasketReturnModel, int>((Func<pIOShelfOrderDetailBasketReturnModel, int>) (x => x.ShelfOrderDetailID)).ToArray<int>()),
          (object) basket.txtQty.Text,
          (object) basket.txtBarcode.Text
        })));
        if (shelfOrderDetailList == null)
        {
          GlobalMob.PlayError();
          int num = await basket.DisplayAlert("Bilgi", "Hata Oluştu", "", "Tamam") ? 1 : 0;
          basket.txtBarcode.Text = "";
          basket.txtBarcode.Focus();
        }
        else if (shelfOrderDetailList.Count == 0)
        {
          GlobalMob.PlayError();
          int num = await basket.DisplayAlert("Bilgi", "Miktar Yetersiz", "", "Tamam") ? 1 : 0;
          basket.txtBarcode.Text = "";
          basket.txtBarcode.Focus();
        }
        else
        {
          foreach (ztIOShelfOrderDetail shelfOrderDetail in shelfOrderDetailList)
          {
            ztIOShelfOrderDetail returnItem = shelfOrderDetail;
            pIOShelfOrderDetailBasketReturnModel basketReturnModel = basket.shelfOrderDetail.Where<pIOShelfOrderDetailBasketReturnModel>((Func<pIOShelfOrderDetailBasketReturnModel, bool>) (x => x.ShelfOrderDetailID == returnItem.ShelfOrderDetailID)).FirstOrDefault<pIOShelfOrderDetailBasketReturnModel>();
            basketReturnModel.ApproveQty = returnItem.ApproveQty;
            if (basketReturnModel.PickingQty == basketReturnModel.ApproveQty)
              basket.shelfOrderDetail.Remove(basketReturnModel);
          }
          basket.FillListView(true);
          basket.txtBarcode.Text = "";
          basket.txtQty.Text = "1";
          // ISSUE: reference to a compiler-generated method
          Device.BeginInvokeOnMainThread(new Action(basket.\u003CtxtBarcode_Completed\u003Eb__9_2));
        }
      }
    }

    private async void btnShelfOrderApproved_Clicked(object sender, EventArgs e)
    {
      Basket basket = this;
      if (!string.IsNullOrEmpty(basket.txtShelfOrderNumber.Text))
      {
        if (!string.IsNullOrEmpty(GlobalMob.PostJson(string.Format("ShelfOrderApproveCompleted?shelfOrderNumber={0}&isCompleted=false", new object[1]
        {
          (object) basket.txtShelfOrderNumber.Text
        })).Replace("\"", "")))
        {
          if (await basket.DisplayAlert("Devam?", "Ürünler tamamlanmadı.Devam etmek istiyor musunuz?", "Evet", "Hayır"))
          {
            GlobalMob.PostJson(string.Format("ShelfOrderApproveCompleted?shelfOrderNumber={0}&isCompleted=true", new object[1]
            {
              (object) basket.txtShelfOrderNumber.Text
            }));
            int num = await basket.DisplayAlert("Bilgi", "Sepet Sayımı Tamamlandı", "", "Tamam") ? 1 : 0;
            Page page = await basket.Navigation.PopAsync();
          }
          else
            basket.FillListView();
        }
        else
        {
          GlobalMob.PostJson(string.Format("ShelfOrderApproveCompleted?shelfOrderNumber={0}&isCompleted=true", new object[1]
          {
            (object) basket.txtShelfOrderNumber.Text
          }));
          int num = await basket.DisplayAlert("Bilgi", "Sepet Sayımı Tamamlandı", "", "Tamam") ? 1 : 0;
          Page page = await basket.Navigation.PopAsync();
        }
      }
      else
      {
        int num1 = await basket.DisplayAlert("Bilgi", "Lütfen raf emri seçiniz", "", "Tamam") ? 1 : 0;
      }
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.Basket.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        Label bindable1 = new Label();
        StackLayout stackLayout1 = new StackLayout();
        BindingExtension bindingExtension1 = new BindingExtension();
        DataTemplate dataTemplate1 = new DataTemplate();
        ListView listView1 = new ListView();
        StackLayout stackLayout2 = new StackLayout();
        Xamarin.Forms.Entry entry1 = new Xamarin.Forms.Entry();
        StackLayout bindable2 = new StackLayout();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry = new SoftkeyboardDisabledEntry();
        Xamarin.Forms.Entry entry2 = new Xamarin.Forms.Entry();
        StackLayout stackLayout3 = new StackLayout();
        ReferenceExtension referenceExtension1 = new ReferenceExtension();
        BindingExtension bindingExtension2 = new BindingExtension();
        ReferenceExtension referenceExtension2 = new ReferenceExtension();
        BindingExtension bindingExtension3 = new BindingExtension();
        Button button = new Button();
        StackLayout bindable3 = new StackLayout();
        DataTemplate dataTemplate2 = new DataTemplate();
        ListView listView2 = new ListView();
        StackLayout bindable4 = new StackLayout();
        StackLayout stackLayout4 = new StackLayout();
        StackLayout stackLayout5 = new StackLayout();
        Basket basket = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) basket, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("basket", (object) basket);
        NameScope.SetNameScope((BindableObject) stackLayout5, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckContent", (object) stackLayout5);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelfOrderList", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckEmptyMessage", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) listView1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfOrder", (object) listView1);
        NameScope.SetNameScope((BindableObject) stackLayout4, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckForm", (object) stackLayout4);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) entry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelfOrderNumber", (object) entry1);
        NameScope.SetNameScope((BindableObject) stackLayout3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckBarcode", (object) stackLayout3);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtBarcode", (object) softkeyboardDisabledEntry);
        NameScope.SetNameScope((BindableObject) entry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtQty", (object) entry2);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) button, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnShelfOrderApproved", (object) button);
        NameScope.SetNameScope((BindableObject) bindable4, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) listView2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstBasket", (object) listView2);
        this.stckContent = stackLayout5;
        this.stckShelfOrderList = stackLayout2;
        this.stckEmptyMessage = stackLayout1;
        this.lstShelfOrder = listView1;
        this.stckForm = stackLayout4;
        this.txtShelfOrderNumber = entry1;
        this.stckBarcode = stackLayout3;
        this.txtBarcode = softkeyboardDisabledEntry;
        this.txtQty = entry2;
        this.btnShelfOrderApproved = button;
        this.lstBasket = listView2;
        stackLayout5.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout2.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout1.SetValue(VisualElement.IsVisibleProperty, (object) false);
        stackLayout1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable1.SetValue(Label.TextProperty, (object) "Bekleyen Raf Emri Bulunmamaktadır.");
        bindable1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.CenterAndExpand);
        bindable1.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        Label label = bindable1;
        BindableProperty fontSizeProperty = Label.FontSizeProperty;
        FontSizeConverter fontSizeConverter = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider1 = new XamlServiceProvider();
        Type type1 = typeof (IProvideValueTarget);
        object[] objectAndParents1 = new object[0 + 5];
        objectAndParents1[0] = (object) bindable1;
        objectAndParents1[1] = (object) stackLayout1;
        objectAndParents1[2] = (object) stackLayout2;
        objectAndParents1[3] = (object) stackLayout5;
        objectAndParents1[4] = (object) basket;
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
        namespaceResolver1.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service2 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver1, typeof (Basket).Assembly);
        xamlServiceProvider1.Add(type2, (object) service2);
        xamlServiceProvider1.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(12, 128)));
        object obj1 = ((IExtendedTypeConverter) fontSizeConverter).ConvertFromInvariantString("Medium", (IServiceProvider) xamlServiceProvider1);
        label.SetValue(fontSizeProperty, obj1);
        stackLayout1.Children.Add((View) bindable1);
        stackLayout2.Children.Add((View) stackLayout1);
        bindingExtension1.Path = ".";
        BindingBase binding1 = ((IMarkupExtension<BindingBase>) bindingExtension1).ProvideValue((IServiceProvider) null);
        listView1.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding1);
        DataTemplate dataTemplate3 = dataTemplate1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        Basket.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_2 xamlCdataTemplate2 = new Basket.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_2();
        object[] objArray1 = new object[0 + 5];
        objArray1[0] = (object) dataTemplate1;
        objArray1[1] = (object) listView1;
        objArray1[2] = (object) stackLayout2;
        objArray1[3] = (object) stackLayout5;
        objArray1[4] = (object) basket;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate2.parentValues = objArray1;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate2.root = basket;
        // ISSUE: reference to a compiler-generated method
        Func<object> func1 = new Func<object>(xamlCdataTemplate2.LoadDataTemplate);
        ((IDataTemplate) dataTemplate3).LoadTemplate = func1;
        listView1.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate1);
        stackLayout2.Children.Add((View) listView1);
        stackLayout5.Children.Add((View) stackLayout2);
        stackLayout4.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout4.SetValue(StackLayout.SpacingProperty, (object) 20.0);
        stackLayout4.SetValue(VisualElement.IsVisibleProperty, (object) false);
        bindable2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        entry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Raf Emri Numarası Giriniz");
        entry1.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable2.Children.Add((View) entry1);
        stackLayout4.Children.Add((View) bindable2);
        stackLayout3.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout3.SetValue(VisualElement.IsVisibleProperty, (object) false);
        softkeyboardDisabledEntry.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Barkod No Giriniz/Okutunuz");
        softkeyboardDisabledEntry.Completed += new EventHandler(basket.txtBarcode_Completed);
        softkeyboardDisabledEntry.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        stackLayout3.Children.Add((View) softkeyboardDisabledEntry);
        entry2.SetValue(Xamarin.Forms.Entry.TextProperty, (object) "1");
        entry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Miktar");
        entry2.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.EndAndExpand);
        stackLayout3.Children.Add((View) entry2);
        stackLayout4.Children.Add((View) stackLayout3);
        button.SetValue(Button.TextProperty, (object) "Tamamla");
        referenceExtension1.Name = "basket";
        ReferenceExtension referenceExtension3 = referenceExtension1;
        XamlServiceProvider xamlServiceProvider2 = new XamlServiceProvider();
        Type type3 = typeof (IProvideValueTarget);
        object[] objectAndParents2 = new object[0 + 6];
        objectAndParents2[0] = (object) bindingExtension2;
        objectAndParents2[1] = (object) button;
        objectAndParents2[2] = (object) bindable3;
        objectAndParents2[3] = (object) stackLayout4;
        objectAndParents2[4] = (object) stackLayout5;
        objectAndParents2[5] = (object) basket;
        SimpleValueTargetProvider service3 = new SimpleValueTargetProvider(objectAndParents2, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider2.Add(type3, (object) service3);
        xamlServiceProvider2.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type4 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver2 = new XmlNamespaceResolver();
        namespaceResolver2.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver2.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver2.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service4 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver2, typeof (Basket).Assembly);
        xamlServiceProvider2.Add(type4, (object) service4);
        xamlServiceProvider2.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(41, 25)));
        object obj2 = referenceExtension3.ProvideValue((IServiceProvider) xamlServiceProvider2);
        bindingExtension2.Source = obj2;
        bindingExtension2.Path = "ButtonColor";
        BindingBase binding2 = ((IMarkupExtension<BindingBase>) bindingExtension2).ProvideValue((IServiceProvider) null);
        button.SetBinding(VisualElement.BackgroundColorProperty, binding2);
        referenceExtension2.Name = "basket";
        ReferenceExtension referenceExtension4 = referenceExtension2;
        XamlServiceProvider xamlServiceProvider3 = new XamlServiceProvider();
        Type type5 = typeof (IProvideValueTarget);
        object[] objectAndParents3 = new object[0 + 6];
        objectAndParents3[0] = (object) bindingExtension3;
        objectAndParents3[1] = (object) button;
        objectAndParents3[2] = (object) bindable3;
        objectAndParents3[3] = (object) stackLayout4;
        objectAndParents3[4] = (object) stackLayout5;
        objectAndParents3[5] = (object) basket;
        SimpleValueTargetProvider service5 = new SimpleValueTargetProvider(objectAndParents3, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider3.Add(type5, (object) service5);
        xamlServiceProvider3.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type6 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver3 = new XmlNamespaceResolver();
        namespaceResolver3.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver3.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver3.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service6 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver3, typeof (Basket).Assembly);
        xamlServiceProvider3.Add(type6, (object) service6);
        xamlServiceProvider3.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(41, 94)));
        object obj3 = referenceExtension4.ProvideValue((IServiceProvider) xamlServiceProvider3);
        bindingExtension3.Source = obj3;
        bindingExtension3.Path = "TextColor";
        BindingBase binding3 = ((IMarkupExtension<BindingBase>) bindingExtension3).ProvideValue((IServiceProvider) null);
        button.SetBinding(Button.TextColorProperty, binding3);
        button.Clicked += new EventHandler(basket.btnShelfOrderApproved_Clicked);
        button.SetValue(VisualElement.IsVisibleProperty, (object) false);
        bindable3.Children.Add((View) button);
        stackLayout4.Children.Add((View) bindable3);
        listView2.SetValue(VisualElement.IsVisibleProperty, (object) false);
        listView2.SetValue(ListView.HeaderProperty, (object) "Sepetteki Ürünler");
        listView2.SetValue(VisualElement.HeightRequestProperty, (object) 500.0);
        DataTemplate dataTemplate4 = dataTemplate2;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        Basket.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_3 xamlCdataTemplate3 = new Basket.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_3();
        object[] objArray2 = new object[0 + 6];
        objArray2[0] = (object) dataTemplate2;
        objArray2[1] = (object) listView2;
        objArray2[2] = (object) bindable4;
        objArray2[3] = (object) stackLayout4;
        objArray2[4] = (object) stackLayout5;
        objArray2[5] = (object) basket;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate3.parentValues = objArray2;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate3.root = basket;
        // ISSUE: reference to a compiler-generated method
        Func<object> func2 = new Func<object>(xamlCdataTemplate3.LoadDataTemplate);
        ((IDataTemplate) dataTemplate4).LoadTemplate = func2;
        listView2.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate2);
        bindable4.Children.Add((View) listView2);
        stackLayout4.Children.Add((View) bindable4);
        stackLayout5.Children.Add((View) stackLayout4);
        basket.SetValue(ContentPage.ContentProperty, (object) stackLayout5);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<Basket>(typeof (Basket));
      this.stckContent = NameScopeExtensions.FindByName<StackLayout>(this, "stckContent");
      this.stckShelfOrderList = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelfOrderList");
      this.stckEmptyMessage = NameScopeExtensions.FindByName<StackLayout>(this, "stckEmptyMessage");
      this.lstShelfOrder = NameScopeExtensions.FindByName<ListView>(this, "lstShelfOrder");
      this.stckForm = NameScopeExtensions.FindByName<StackLayout>(this, "stckForm");
      this.txtShelfOrderNumber = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtShelfOrderNumber");
      this.stckBarcode = NameScopeExtensions.FindByName<StackLayout>(this, "stckBarcode");
      this.txtBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtBarcode");
      this.txtQty = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtQty");
      this.btnShelfOrderApproved = NameScopeExtensions.FindByName<Button>(this, "btnShelfOrderApproved");
      this.lstBasket = NameScopeExtensions.FindByName<ListView>(this, "lstBasket");
    }
  }
}
