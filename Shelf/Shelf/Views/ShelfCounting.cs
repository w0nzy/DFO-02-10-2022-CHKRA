// Decompiled with JetBrains decompiler
// Type: Shelf.Views.ShelfCounting
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
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Internals;
using XFNoSoftKeyboadEntryControl;

namespace Shelf.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\ShelfCounting.xaml")]
  public class ShelfCounting : ContentPage
  {
    public string CountingHeaderText = "asdas";
    private pIOGetShelfCountingReturnModel selectedCounting;
    private List<pIOGetShelfCountingDetailReturnModel> shelfCountingDetailList;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckShelfCounting;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstShelfCounting;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckForm;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private DatePicker dtShelfCounting;
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
    private StackLayout stckShelfCountingDetailList;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstShelfCountingDetail;

    public Color ButtonColor => Color.FromRgb(21, 40, 151);

    public Color TextColor => Color.White;

    public ShelfCounting()
    {
      this.InitializeComponent();
      this.Title = "Raf Sayım";
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
      this.txtShelf.Focus();
    }

    private void txtShelf_Completed(object sender, EventArgs e)
    {
      List<pIOGetShelfCountingReturnModel> countingReturnModelList = JsonConvert.DeserializeObject<List<pIOGetShelfCountingReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfCounting?date={0}", new object[1]
      {
        (object) this.dtShelfCounting.Date.ToString("dd/MM/yyyy")
      })));
      if (countingReturnModelList.Count == 1)
      {
        this.selectedCounting = countingReturnModelList[0];
        this.stckShelfCounting.IsVisible = false;
        this.stckForm.IsVisible = true;
        this.stckShelfCountingDetailList.IsVisible = true;
        this.shelfCountingDetailList = JsonConvert.DeserializeObject<List<pIOGetShelfCountingDetailReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfCountingDetail?countingId={0}&shelfCode={1}", new object[2]
        {
          (object) this.selectedCounting.CountingID,
          (object) this.txtShelf.Text
        })));
        this.FillListview();
      }
      else if (countingReturnModelList.Count > 1)
      {
        this.stckShelfCounting.IsVisible = true;
        this.stckForm.IsVisible = false;
        this.Title = "Raf Sayım Emirleri";
        this.lstShelfCounting.ItemsSource = (IEnumerable) countingReturnModelList;
        this.lstShelfCounting.ItemSelected += new EventHandler<SelectedItemChangedEventArgs>(this.LstShelfCounting_ItemSelected);
      }
      else
      {
        GlobalMob.PlayError();
        this.DisplayAlert("Hata", "Raf Bulunamadı", "", "Tamam");
        this.txtShelf.Text = "";
        this.txtShelf.Focus();
        return;
      }
      this.stckBarcode.IsVisible = true;
      this.stckSuccess.IsVisible = true;
      this.BarcodeFocus(500);
    }

    private void LstShelfCounting_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      object selectedItem = ((ListView) sender).SelectedItem;
      if (selectedItem == null)
        return;
      this.Title = "Raf Sayım";
      this.selectedCounting = (pIOGetShelfCountingReturnModel) selectedItem;
      this.stckShelfCounting.IsVisible = false;
      this.stckForm.IsVisible = true;
      this.shelfCountingDetailList = JsonConvert.DeserializeObject<List<pIOGetShelfCountingDetailReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfCountingDetail?countingId={0}&shelfCode={1}", new object[2]
      {
        (object) this.selectedCounting.CountingID,
        (object) this.txtShelf.Text
      })));
      this.FillListview();
    }

    private void txtBarcode_Completed(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(this.txtBarcode.Text))
      {
        ztIOShelfCountingDetail shelfCountingDetail1 = new ztIOShelfCountingDetail()
        {
          CountingID = this.selectedCounting.CountingID,
          CreatedUserName = GlobalMob.User.UserName
        };
        shelfCountingDetail1.UpdatedUserName = shelfCountingDetail1.CreatedUserName;
        shelfCountingDetail1.Qty = (double) Convert.ToInt32(this.txtQty.Text);
        shelfCountingDetail1.ShelfCode = this.txtShelf.Text;
        shelfCountingDetail1.UsedBarcode = this.txtBarcode.Text;
        ztIOShelfCountingDetail shelfCountingDetail2 = JsonConvert.DeserializeObject<ztIOShelfCountingDetail>(GlobalMob.PostJson("SaveShelfCounting", new Dictionary<string, string>()
        {
          {
            "detail",
            JsonConvert.SerializeObject((object) shelfCountingDetail1)
          },
          {
            "date",
            this.dtShelfCounting.Date.ToString("dd.MM.yyyy")
          }
        }).Result);
        if (shelfCountingDetail2 == null)
        {
          this.DisplayAlert("Bilgi", "Hata Oluştu", "", "Tamam");
          GlobalMob.PlayError();
          this.BarcodeFocus(100);
          return;
        }
        if (shelfCountingDetail2.CountingDetailID <= 0)
        {
          this.DisplayAlert("Bilgi", "Ürün Bulunamadı", "", "Tamam");
          GlobalMob.PlayError();
          this.BarcodeFocus(100);
          return;
        }
        if (this.selectedCounting.CountingID <= 0)
          this.selectedCounting = new pIOGetShelfCountingReturnModel()
          {
            CountingID = shelfCountingDetail2.CountingID
          };
        this.shelfCountingDetailList = this.shelfCountingDetailList.Select<pIOGetShelfCountingDetailReturnModel, pIOGetShelfCountingDetailReturnModel>((Func<pIOGetShelfCountingDetailReturnModel, pIOGetShelfCountingDetailReturnModel>) (c =>
        {
          c.LastReadBarcode = false;
          return c;
        })).ToList<pIOGetShelfCountingDetailReturnModel>();
        this.shelfCountingDetailList.Add(new pIOGetShelfCountingDetailReturnModel()
        {
          CountingDetailID = shelfCountingDetail2.CountingDetailID,
          CountingID = shelfCountingDetail2.CountingID,
          Qty = shelfCountingDetail2.Qty,
          ShelfCode = this.txtShelf.Text,
          UsedBarcode = this.txtBarcode.Text.Trim(),
          LastReadBarcode = true
        });
        GlobalMob.PlaySave();
        this.FillListview();
        this.txtQty.Text = "1";
      }
      this.BarcodeFocus(100);
    }

    private void FillListview()
    {
      IEnumerable<pIOGetShelfCountingDetailReturnModel> source = this.shelfCountingDetailList.GroupBy(c => new
      {
        UsedBarcode = c.UsedBarcode
      }).Select<IGrouping<\u003C\u003Ef__AnonymousType1<string>, pIOGetShelfCountingDetailReturnModel>, pIOGetShelfCountingDetailReturnModel>(gcs => new pIOGetShelfCountingDetailReturnModel()
      {
        UsedBarcode = gcs.Key.UsedBarcode,
        Qty = gcs.Sum<pIOGetShelfCountingDetailReturnModel>((Func<pIOGetShelfCountingDetailReturnModel, double>) (x => x.Qty)),
        childList = gcs.ToList<pIOGetShelfCountingDetailReturnModel>()
      });
      this.lstShelfCountingDetail.ItemsSource = (IEnumerable) null;
      this.lstShelfCountingDetail.ItemsSource = (IEnumerable) source.OrderByDescending<pIOGetShelfCountingDetailReturnModel, bool>((Func<pIOGetShelfCountingDetailReturnModel, bool>) (x => x.LastReadBarcode)).ToList<pIOGetShelfCountingDetailReturnModel>();
      this.CountingHeaderText = source.Count<pIOGetShelfCountingDetailReturnModel>() > 0 ? "Toplam Miktar : " + (object) Convert.ToInt32(this.shelfCountingDetailList.Sum<pIOGetShelfCountingDetailReturnModel>((Func<pIOGetShelfCountingDetailReturnModel, double>) (x => x.Qty))) : "";
      this.lstShelfCountingDetail.Header = (object) this.CountingHeaderText;
    }

    private void btnDelete_Clicked(object sender, EventArgs e)
    {
      string barcode = Convert.ToString(((Button) sender).CommandParameter);
      bool boolean = Convert.ToBoolean(GlobalMob.PostJson(string.Format("DeleteShelfCountingDetails?ids={0}", new object[1]
      {
        (object) string.Join<int>(",", (IEnumerable<int>) this.shelfCountingDetailList.Where<pIOGetShelfCountingDetailReturnModel>((Func<pIOGetShelfCountingDetailReturnModel, bool>) (x => x.UsedBarcode == barcode)).ToList<pIOGetShelfCountingDetailReturnModel>().Select<pIOGetShelfCountingDetailReturnModel, int>((Func<pIOGetShelfCountingDetailReturnModel, int>) (x => x.CountingDetailID)).ToArray<int>())
      })));
      this.DisplayAlert("Bilgi", boolean ? "Ürün Silindi" : "Hata Oluştu", "", "Tamam");
      if (boolean)
        this.shelfCountingDetailList.RemoveAll((Predicate<pIOGetShelfCountingDetailReturnModel>) (x => x.UsedBarcode == barcode));
      this.FillListview();
      this.BarcodeFocus(250);
    }

    private async void BarcodeFocus(int time) => Device.BeginInvokeOnMainThread((Action) (async () =>
    {
      await Task.Delay(time);
      this.txtBarcode.Text = "";
      this.txtBarcode?.Focus();
    }));

    private void btnSuccess_Clicked(object sender, EventArgs e)
    {
      this.stckShelfCountingDetailList.IsVisible = false;
      this.stckShelfCounting.IsVisible = false;
      this.stckForm.IsVisible = true;
      this.shelfCountingDetailList = new List<pIOGetShelfCountingDetailReturnModel>();
      this.FillListview();
      this.stckBarcode.IsVisible = false;
      this.stckSuccess.IsVisible = false;
      this.txtShelf.Text = "";
      this.txtShelf.Focus();
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.ShelfCounting.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        BindingExtension bindingExtension1 = new BindingExtension();
        DataTemplate dataTemplate1 = new DataTemplate();
        ListView listView1 = new ListView();
        StackLayout stackLayout1 = new StackLayout();
        DatePicker datePicker = new DatePicker();
        StackLayout bindable1 = new StackLayout();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry1 = new SoftkeyboardDisabledEntry();
        StackLayout bindable2 = new StackLayout();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry2 = new SoftkeyboardDisabledEntry();
        Xamarin.Forms.Entry entry = new Xamarin.Forms.Entry();
        StackLayout stackLayout2 = new StackLayout();
        ReferenceExtension referenceExtension1 = new ReferenceExtension();
        BindingExtension bindingExtension2 = new BindingExtension();
        ReferenceExtension referenceExtension2 = new ReferenceExtension();
        BindingExtension bindingExtension3 = new BindingExtension();
        Button button = new Button();
        StackLayout stackLayout3 = new StackLayout();
        BindingExtension bindingExtension4 = new BindingExtension();
        BindingExtension bindingExtension5 = new BindingExtension();
        DataTemplate dataTemplate2 = new DataTemplate();
        DataTemplate dataTemplate3 = new DataTemplate();
        ListView listView2 = new ListView();
        StackLayout stackLayout4 = new StackLayout();
        StackLayout stackLayout5 = new StackLayout();
        StackLayout bindable3 = new StackLayout();
        ShelfCounting shelfCounting = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) shelfCounting, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("shelfCounting", (object) shelfCounting);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelfCounting", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) listView1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfCounting", (object) listView1);
        NameScope.SetNameScope((BindableObject) stackLayout5, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckForm", (object) stackLayout5);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) datePicker, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("dtShelfCounting", (object) datePicker);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelf", (object) softkeyboardDisabledEntry1);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckBarcode", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtBarcode", (object) softkeyboardDisabledEntry2);
        NameScope.SetNameScope((BindableObject) entry, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtQty", (object) entry);
        NameScope.SetNameScope((BindableObject) stackLayout3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckSuccess", (object) stackLayout3);
        NameScope.SetNameScope((BindableObject) button, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnSuccess", (object) button);
        NameScope.SetNameScope((BindableObject) stackLayout4, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelfCountingDetailList", (object) stackLayout4);
        NameScope.SetNameScope((BindableObject) listView2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfCountingDetail", (object) listView2);
        this.stckShelfCounting = stackLayout1;
        this.lstShelfCounting = listView1;
        this.stckForm = stackLayout5;
        this.dtShelfCounting = datePicker;
        this.txtShelf = softkeyboardDisabledEntry1;
        this.stckBarcode = stackLayout2;
        this.txtBarcode = softkeyboardDisabledEntry2;
        this.txtQty = entry;
        this.stckSuccess = stackLayout3;
        this.btnSuccess = button;
        this.stckShelfCountingDetailList = stackLayout4;
        this.lstShelfCountingDetail = listView2;
        stackLayout1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.SetValue(VisualElement.IsVisibleProperty, (object) false);
        bindingExtension1.Path = ".";
        BindingBase binding1 = ((IMarkupExtension<BindingBase>) bindingExtension1).ProvideValue((IServiceProvider) null);
        listView1.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding1);
        DataTemplate dataTemplate4 = dataTemplate1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_8 xamlCdataTemplate8 = new ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_8();
        object[] objArray1 = new object[0 + 5];
        objArray1[0] = (object) dataTemplate1;
        objArray1[1] = (object) listView1;
        objArray1[2] = (object) stackLayout1;
        objArray1[3] = (object) bindable3;
        objArray1[4] = (object) shelfCounting;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate8.parentValues = objArray1;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate8.root = shelfCounting;
        // ISSUE: reference to a compiler-generated method
        Func<object> func1 = new Func<object>(xamlCdataTemplate8.LoadDataTemplate);
        ((IDataTemplate) dataTemplate4).LoadTemplate = func1;
        listView1.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate1);
        stackLayout1.Children.Add((View) listView1);
        bindable3.Children.Add((View) stackLayout1);
        stackLayout5.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout5.SetValue(StackLayout.SpacingProperty, (object) 20.0);
        bindable1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        datePicker.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable1.Children.Add((View) datePicker);
        stackLayout5.Children.Add((View) bindable1);
        bindable2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        softkeyboardDisabledEntry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        softkeyboardDisabledEntry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Raf No Okutunuz");
        softkeyboardDisabledEntry1.Completed += new EventHandler(shelfCounting.txtShelf_Completed);
        bindable2.Children.Add((View) softkeyboardDisabledEntry1);
        stackLayout5.Children.Add((View) bindable2);
        stackLayout2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout2.SetValue(VisualElement.IsVisibleProperty, (object) false);
        softkeyboardDisabledEntry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Barkod No Okutunuz");
        softkeyboardDisabledEntry2.Completed += new EventHandler(shelfCounting.txtBarcode_Completed);
        softkeyboardDisabledEntry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        stackLayout2.Children.Add((View) softkeyboardDisabledEntry2);
        entry.SetValue(Xamarin.Forms.Entry.TextProperty, (object) "1");
        entry.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Miktar");
        entry.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.EndAndExpand);
        stackLayout2.Children.Add((View) entry);
        stackLayout5.Children.Add((View) stackLayout2);
        stackLayout3.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout3.SetValue(VisualElement.IsVisibleProperty, (object) false);
        button.SetValue(Button.TextProperty, (object) "Tamamla");
        referenceExtension1.Name = "shelfCounting";
        ReferenceExtension referenceExtension3 = referenceExtension1;
        XamlServiceProvider xamlServiceProvider1 = new XamlServiceProvider();
        Type type1 = typeof (IProvideValueTarget);
        object[] objectAndParents1 = new object[0 + 6];
        objectAndParents1[0] = (object) bindingExtension2;
        objectAndParents1[1] = (object) button;
        objectAndParents1[2] = (object) stackLayout3;
        objectAndParents1[3] = (object) stackLayout5;
        objectAndParents1[4] = (object) bindable3;
        objectAndParents1[5] = (object) shelfCounting;
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
        XamlTypeResolver service2 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver1, typeof (ShelfCounting).Assembly);
        xamlServiceProvider1.Add(type2, (object) service2);
        xamlServiceProvider1.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(38, 64)));
        object obj1 = referenceExtension3.ProvideValue((IServiceProvider) xamlServiceProvider1);
        bindingExtension2.Source = obj1;
        bindingExtension2.Path = "ButtonColor";
        BindingBase binding2 = ((IMarkupExtension<BindingBase>) bindingExtension2).ProvideValue((IServiceProvider) null);
        button.SetBinding(VisualElement.BackgroundColorProperty, binding2);
        referenceExtension2.Name = "shelfCounting";
        ReferenceExtension referenceExtension4 = referenceExtension2;
        XamlServiceProvider xamlServiceProvider2 = new XamlServiceProvider();
        Type type3 = typeof (IProvideValueTarget);
        object[] objectAndParents2 = new object[0 + 6];
        objectAndParents2[0] = (object) bindingExtension3;
        objectAndParents2[1] = (object) button;
        objectAndParents2[2] = (object) stackLayout3;
        objectAndParents2[3] = (object) stackLayout5;
        objectAndParents2[4] = (object) bindable3;
        objectAndParents2[5] = (object) shelfCounting;
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
        XamlTypeResolver service4 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver2, typeof (ShelfCounting).Assembly);
        xamlServiceProvider2.Add(type4, (object) service4);
        xamlServiceProvider2.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(38, 140)));
        object obj2 = referenceExtension4.ProvideValue((IServiceProvider) xamlServiceProvider2);
        bindingExtension3.Source = obj2;
        bindingExtension3.Path = "TextColor";
        BindingBase binding3 = ((IMarkupExtension<BindingBase>) bindingExtension3).ProvideValue((IServiceProvider) null);
        button.SetBinding(Button.TextColorProperty, binding3);
        button.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        button.Clicked += new EventHandler(shelfCounting.btnSuccess_Clicked);
        stackLayout3.Children.Add((View) button);
        stackLayout5.Children.Add((View) stackLayout3);
        stackLayout4.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        bindingExtension4.Path = ".";
        BindingBase binding4 = ((IMarkupExtension<BindingBase>) bindingExtension4).ProvideValue((IServiceProvider) null);
        listView2.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding4);
        bindingExtension5.Path = "CountingHeaderText";
        BindingBase binding5 = ((IMarkupExtension<BindingBase>) bindingExtension5).ProvideValue((IServiceProvider) null);
        listView2.SetBinding(ListView.HeaderProperty, binding5);
        DataTemplate dataTemplate5 = dataTemplate2;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_9 xamlCdataTemplate9 = new ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_9();
        object[] objArray2 = new object[0 + 6];
        objArray2[0] = (object) dataTemplate2;
        objArray2[1] = (object) listView2;
        objArray2[2] = (object) stackLayout4;
        objArray2[3] = (object) stackLayout5;
        objArray2[4] = (object) bindable3;
        objArray2[5] = (object) shelfCounting;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate9.parentValues = objArray2;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate9.root = shelfCounting;
        // ISSUE: reference to a compiler-generated method
        Func<object> func2 = new Func<object>(xamlCdataTemplate9.LoadDataTemplate);
        ((IDataTemplate) dataTemplate5).LoadTemplate = func2;
        listView2.SetValue(ListView.HeaderTemplateProperty, (object) dataTemplate2);
        DataTemplate dataTemplate6 = dataTemplate3;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_10 xamlCdataTemplate10 = new ShelfCounting.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_10();
        object[] objArray3 = new object[0 + 6];
        objArray3[0] = (object) dataTemplate3;
        objArray3[1] = (object) listView2;
        objArray3[2] = (object) stackLayout4;
        objArray3[3] = (object) stackLayout5;
        objArray3[4] = (object) bindable3;
        objArray3[5] = (object) shelfCounting;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate10.parentValues = objArray3;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate10.root = shelfCounting;
        // ISSUE: reference to a compiler-generated method
        Func<object> func3 = new Func<object>(xamlCdataTemplate10.LoadDataTemplate);
        ((IDataTemplate) dataTemplate6).LoadTemplate = func3;
        listView2.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate3);
        stackLayout4.Children.Add((View) listView2);
        stackLayout5.Children.Add((View) stackLayout4);
        bindable3.Children.Add((View) stackLayout5);
        shelfCounting.SetValue(ContentPage.ContentProperty, (object) bindable3);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<ShelfCounting>(typeof (ShelfCounting));
      this.stckShelfCounting = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelfCounting");
      this.lstShelfCounting = NameScopeExtensions.FindByName<ListView>(this, "lstShelfCounting");
      this.stckForm = NameScopeExtensions.FindByName<StackLayout>(this, "stckForm");
      this.dtShelfCounting = NameScopeExtensions.FindByName<DatePicker>(this, "dtShelfCounting");
      this.txtShelf = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtShelf");
      this.stckBarcode = NameScopeExtensions.FindByName<StackLayout>(this, "stckBarcode");
      this.txtBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtBarcode");
      this.txtQty = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtQty");
      this.stckSuccess = NameScopeExtensions.FindByName<StackLayout>(this, "stckSuccess");
      this.btnSuccess = NameScopeExtensions.FindByName<Button>(this, "btnSuccess");
      this.stckShelfCountingDetailList = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelfCountingDetailList");
      this.lstShelfCountingDetail = NameScopeExtensions.FindByName<ListView>(this, "lstShelfCountingDetail");
    }
  }
}
