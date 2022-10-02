// Decompiled with JetBrains decompiler
// Type: Shelf.Views.ShelfEntry
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Newtonsoft.Json;
using Shelf.Manager;
using Shelf.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\ShelfEntry.xaml")]
  public class ShelfEntry : ContentPage
  {
    private ObservableCollection<ztIOShelfTransactionDetail> list;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtShelf;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtQty;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckSuccess;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnSuccess;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstProducts;

    public Color ButtonColor => Color.FromRgb(52, 203, 201);

    public Color TextColor => Color.White;

    public ztIOShelf selectShelf { get; set; }

    public int trID { get; set; }

    public ShelfEntry()
    {
      this.InitializeComponent();
      this.Title = "Raf Girişi(Serbest)";
      this.list = new ObservableCollection<ztIOShelfTransactionDetail>();
      this.list.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
      this.lstProducts.ItemsSource = (IEnumerable) this.list;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
      if (!string.IsNullOrEmpty(this.txtShelf.Text))
        this.txtBarcode.Focus();
      else
        this.txtShelf.Focus();
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
    }

    private async void txtBarcode_Completed(object sender, EventArgs e)
    {
      ShelfEntry shelfEntry = this;
      if (string.IsNullOrEmpty(shelfEntry.txtShelf.Text))
      {
        int num = await shelfEntry.DisplayAlert("Bilgi", "Lütfen raf okutunuz", "", "Tamam") ? 1 : 0;
        shelfEntry.txtBarcode.Text = "";
        shelfEntry.txtBarcode.Focus();
      }
      else if (!GlobalMob.IsInternet())
      {
        int num = await shelfEntry.DisplayAlert("Hata", "İnternet Bağlantısı yok", "", "Tamam") ? 1 : 0;
        shelfEntry.txtBarcode.Text = "";
        shelfEntry.txtBarcode.Focus();
      }
      else
      {
        if (!string.IsNullOrEmpty(shelfEntry.txtBarcode.Text))
        {
          ztIOShelfTransactionDetail trans = JsonConvert.DeserializeObject<ztIOShelfTransactionDetail>(GlobalMob.PostJson(string.Format("TrInsert?shelfID={0}&prcTypeId={6}&wCode={1}&barcode={2}&uName={3}&trID={4}&qty={5}", (object) shelfEntry.selectShelf.ShelfID, (object) shelfEntry.selectShelf.WarehouseCode, (object) shelfEntry.txtBarcode.Text, (object) GlobalMob.User.UserName, (object) shelfEntry.trID, (object) shelfEntry.txtQty.Text, (object) 1)));
          if (trans != null)
          {
            shelfEntry.lstProducts.IsVisible = true;
            shelfEntry.trID = trans.TransactionID;
            ztIOShelfTransactionDetail transactionDetail1 = shelfEntry.list.Where<ztIOShelfTransactionDetail>((Func<ztIOShelfTransactionDetail, bool>) (x =>
            {
              if (!(x.ItemCode == trans.ItemCode) || !(x.ItemDim1Code == trans.ItemDim1Code) || !(x.ItemDim2Code == trans.ItemDim2Code) || !(x.ItemDim3Code == trans.ItemDim3Code))
                return false;
              int? itemTypeCode1 = x.ItemTypeCode;
              int? itemTypeCode2 = trans.ItemTypeCode;
              return itemTypeCode1.GetValueOrDefault() == itemTypeCode2.GetValueOrDefault() & itemTypeCode1.HasValue == itemTypeCode2.HasValue;
            })).FirstOrDefault<ztIOShelfTransactionDetail>();
            if (transactionDetail1 == null)
            {
              shelfEntry.list.Add(trans);
            }
            else
            {
              ztIOShelfTransactionDetail transactionDetail2 = trans;
              double? qty1 = transactionDetail2.Qty;
              double? qty2 = transactionDetail1.Qty;
              transactionDetail2.Qty = qty1.HasValue & qty2.HasValue ? new double?(qty1.GetValueOrDefault() + qty2.GetValueOrDefault()) : new double?();
              shelfEntry.list.Remove(transactionDetail1);
              shelfEntry.list.Add(trans);
            }
            shelfEntry.txtQty.Text = "1";
            GlobalMob.PlaySave();
          }
          else
          {
            GlobalMob.PlayError();
            int num = await shelfEntry.DisplayAlert("Bilgi", "Ürün Bulunamadı", "", "Tamam") ? 1 : 0;
          }
        }
        // ISSUE: reference to a compiler-generated method
        Device.BeginInvokeOnMainThread(new Action(shelfEntry.\u003CtxtBarcode_Completed\u003Eb__16_0));
      }
    }

    private async void txtShelf_Completed(object sender, EventArgs e)
    {
      ShelfEntry shelfEntry = this;
      shelfEntry.selectShelf = (ztIOShelf) null;
      shelfEntry.trID = 0;
      if (!string.IsNullOrEmpty(shelfEntry.txtShelf.Text) && shelfEntry.txtShelf.Text.Length == 13)
      {
        int num = await shelfEntry.DisplayAlert("Bilgi", "Raf kodu alanındasınız lütfen ürün yerine raf kodu okutunuz", "", "Tamam") ? 1 : 0;
        shelfEntry.txtShelf.Text = "";
        shelfEntry.txtShelf.Focus();
      }
      else
      {
        if (string.IsNullOrEmpty(shelfEntry.txtShelf.Text))
          return;
        string str = GlobalMob.PostJson(string.Format("GetShelf?shelfCode={0}", new object[1]
        {
          (object) shelfEntry.txtShelf.Text
        }));
        if (!string.IsNullOrEmpty(str))
        {
          shelfEntry.selectShelf = JsonConvert.DeserializeObject<ztIOShelf>(str);
          shelfEntry.stckBarcode.IsVisible = true;
          shelfEntry.stckSuccess.IsVisible = true;
          // ISSUE: reference to a compiler-generated method
          Device.BeginInvokeOnMainThread(new Action(shelfEntry.\u003CtxtShelf_Completed\u003Eb__17_0));
        }
        else
        {
          int num = await shelfEntry.DisplayAlert("Bilgi", "Hatalı Raf", "", "Tamam") ? 1 : 0;
          shelfEntry.txtShelf.Text = "";
          shelfEntry.txtShelf.Focus();
        }
      }
    }

    private void btnSuccess_Clicked(object sender, EventArgs e)
    {
      this.stckBarcode.IsVisible = false;
      this.selectShelf = (ztIOShelf) null;
      this.trID = 0;
      this.stckSuccess.IsVisible = false;
      this.list = new ObservableCollection<ztIOShelfTransactionDetail>();
      this.lstProducts.ItemsSource = (IEnumerable) this.list;
      this.txtShelf.Text = "";
      this.txtShelf.Focus();
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.ShelfEntry.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry1 = new SoftkeyboardDisabledEntry();
        StackLayout bindable1 = new StackLayout();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry2 = new SoftkeyboardDisabledEntry();
        Xamarin.Forms.Entry entry = new Xamarin.Forms.Entry();
        StackLayout stackLayout1 = new StackLayout();
        ReferenceExtension referenceExtension1 = new ReferenceExtension();
        BindingExtension bindingExtension1 = new BindingExtension();
        ReferenceExtension referenceExtension2 = new ReferenceExtension();
        BindingExtension bindingExtension2 = new BindingExtension();
        Button button = new Button();
        StackLayout stackLayout2 = new StackLayout();
        DataTemplate dataTemplate1 = new DataTemplate();
        ListView listView = new ListView();
        StackLayout bindable2 = new StackLayout();
        StackLayout bindable3 = new StackLayout();
        ShelfEntry shelfEntry = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) shelfEntry, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("Product", (object) shelfEntry);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelf", (object) softkeyboardDisabledEntry1);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckBarcode", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtBarcode", (object) softkeyboardDisabledEntry2);
        NameScope.SetNameScope((BindableObject) entry, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtQty", (object) entry);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckSuccess", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) button, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnSuccess", (object) button);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) listView, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstProducts", (object) listView);
        this.txtShelf = softkeyboardDisabledEntry1;
        this.stckBarcode = stackLayout1;
        this.txtBarcode = softkeyboardDisabledEntry2;
        this.txtQty = entry;
        this.stckSuccess = stackLayout2;
        this.btnSuccess = button;
        this.lstProducts = listView;
        bindable1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        softkeyboardDisabledEntry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Raf No Giriniz/Okutunuz");
        softkeyboardDisabledEntry1.Completed += new EventHandler(shelfEntry.txtShelf_Completed);
        softkeyboardDisabledEntry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable1.Children.Add((View) softkeyboardDisabledEntry1);
        bindable3.Children.Add((View) bindable1);
        stackLayout1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout1.SetValue(VisualElement.IsVisibleProperty, (object) false);
        softkeyboardDisabledEntry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Barkod No Giriniz/Okutunuz");
        softkeyboardDisabledEntry2.Completed += new EventHandler(shelfEntry.txtBarcode_Completed);
        softkeyboardDisabledEntry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        stackLayout1.Children.Add((View) softkeyboardDisabledEntry2);
        entry.SetValue(Xamarin.Forms.Entry.TextProperty, (object) "1");
        entry.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Miktar");
        entry.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.EndAndExpand);
        stackLayout1.Children.Add((View) entry);
        bindable3.Children.Add((View) stackLayout1);
        stackLayout2.SetValue(VisualElement.IsVisibleProperty, (object) false);
        button.SetValue(Button.TextProperty, (object) "Tamamla");
        button.SetValue(VisualElement.HeightRequestProperty, (object) 80.0);
        referenceExtension1.Name = "Product";
        ReferenceExtension referenceExtension3 = referenceExtension1;
        XamlServiceProvider xamlServiceProvider1 = new XamlServiceProvider();
        Type type1 = typeof (IProvideValueTarget);
        object[] objectAndParents1 = new object[0 + 5];
        objectAndParents1[0] = (object) bindingExtension1;
        objectAndParents1[1] = (object) button;
        objectAndParents1[2] = (object) stackLayout2;
        objectAndParents1[3] = (object) bindable3;
        objectAndParents1[4] = (object) shelfEntry;
        SimpleValueTargetProvider service1 = new SimpleValueTargetProvider(objectAndParents1, (object) typeof (BindingExtension).GetProperty("Source"));
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
        XamlTypeResolver service2 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver1, typeof (ShelfEntry).Assembly);
        xamlServiceProvider1.Add(type2, (object) service2);
        xamlServiceProvider1.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(18, 25)));
        object obj1 = referenceExtension3.ProvideValue((IServiceProvider) xamlServiceProvider1);
        bindingExtension1.Source = obj1;
        bindingExtension1.Path = "ButtonColor";
        BindingBase binding1 = ((IMarkupExtension<BindingBase>) bindingExtension1).ProvideValue((IServiceProvider) null);
        button.SetBinding(VisualElement.BackgroundColorProperty, binding1);
        referenceExtension2.Name = "Product";
        ReferenceExtension referenceExtension4 = referenceExtension2;
        XamlServiceProvider xamlServiceProvider2 = new XamlServiceProvider();
        Type type3 = typeof (IProvideValueTarget);
        object[] objectAndParents2 = new object[0 + 5];
        objectAndParents2[0] = (object) bindingExtension2;
        objectAndParents2[1] = (object) button;
        objectAndParents2[2] = (object) stackLayout2;
        objectAndParents2[3] = (object) bindable3;
        objectAndParents2[4] = (object) shelfEntry;
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
        XamlTypeResolver service4 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver2, typeof (ShelfEntry).Assembly);
        xamlServiceProvider2.Add(type4, (object) service4);
        xamlServiceProvider2.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(18, 95)));
        object obj2 = referenceExtension4.ProvideValue((IServiceProvider) xamlServiceProvider2);
        bindingExtension2.Source = obj2;
        bindingExtension2.Path = "TextColor";
        BindingBase binding2 = ((IMarkupExtension<BindingBase>) bindingExtension2).ProvideValue((IServiceProvider) null);
        button.SetBinding(Button.TextColorProperty, binding2);
        button.Clicked += new EventHandler(shelfEntry.btnSuccess_Clicked);
        button.SetValue(VisualElement.WidthRequestProperty, (object) 40.0);
        stackLayout2.Children.Add((View) button);
        bindable3.Children.Add((View) stackLayout2);
        listView.SetValue(VisualElement.IsVisibleProperty, (object) false);
        listView.SetValue(ListView.HeaderProperty, (object) "Eklenen Ürünler");
        listView.SetValue(VisualElement.HeightRequestProperty, (object) 500.0);
        DataTemplate dataTemplate2 = dataTemplate1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfEntry.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_4 xamlCdataTemplate4 = new ShelfEntry.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_4();
        object[] objArray = new object[0 + 5];
        objArray[0] = (object) dataTemplate1;
        objArray[1] = (object) listView;
        objArray[2] = (object) bindable2;
        objArray[3] = (object) bindable3;
        objArray[4] = (object) shelfEntry;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate4.parentValues = objArray;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate4.root = shelfEntry;
        // ISSUE: reference to a compiler-generated method
        Func<object> func = new Func<object>(xamlCdataTemplate4.LoadDataTemplate);
        ((IDataTemplate) dataTemplate2).LoadTemplate = func;
        listView.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate1);
        bindable2.Children.Add((View) listView);
        bindable3.Children.Add((View) bindable2);
        shelfEntry.SetValue(ContentPage.ContentProperty, (object) bindable3);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<ShelfEntry>(typeof (ShelfEntry));
      this.txtShelf = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtShelf");
      this.stckBarcode = NameScopeExtensions.FindByName<StackLayout>(this, "stckBarcode");
      this.txtBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtBarcode");
      this.txtQty = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtQty");
      this.stckSuccess = NameScopeExtensions.FindByName<StackLayout>(this, "stckSuccess");
      this.btnSuccess = NameScopeExtensions.FindByName<Button>(this, "btnSuccess");
      this.lstProducts = NameScopeExtensions.FindByName<ListView>(this, "lstProducts");
    }
  }
}
