// Decompiled with JetBrains decompiler
// Type: Shelf.Views.ShelfSearch
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
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XFNoSoftKeyboadEntryControl;

namespace Shelf.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\ShelfSearch.xaml")]
  public class ShelfSearch : ContentPage
  {
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckForm;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckShelfList;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstShelfList;

    public Color ButtonColor => Color.FromRgb(93, 48, 243);

    public Color TextColor => Color.White;

    public ShelfSearch()
    {
      this.InitializeComponent();
      this.Title = "Raf Bul";
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
      this.BarcodeFocus(200);
    }

    private void TxtBarcode_Completed(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtBarcode.Text))
        return;
      List<pIOGetShelfFromBarcodeReturnModel> barcodeReturnModelList = JsonConvert.DeserializeObject<List<pIOGetShelfFromBarcodeReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfFromBarcode?barcode={0}", new object[1]
      {
        (object) this.txtBarcode.Text
      })));
      this.lstShelfList.ItemsSource = (IEnumerable) barcodeReturnModelList;
      this.stckShelfList.IsVisible = true;
      if (barcodeReturnModelList == null || barcodeReturnModelList.Count == 0)
      {
        this.stckShelfList.IsVisible = false;
        GlobalMob.PlayError();
        this.DisplayAlert("Hata", "Raf Bulunamadı", "", "Tamam");
      }
      this.BarcodeFocus(200);
    }

    private void BarcodeFocus(int time) => Device.BeginInvokeOnMainThread((Action) (async () =>
    {
      await Task.Delay(time);
      this.txtBarcode.Text = "";
      this.txtBarcode?.Focus();
    }));

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.ShelfSearch.xaml") != null)
        this.__InitComponentRuntime();
      else if (Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider != null && Xamarin.Forms.Xaml.Internals.XamlLoader.XamlFileProvider(this.GetType()) != null)
      {
        this.__InitComponentRuntime();
      }
      else
      {
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry = new SoftkeyboardDisabledEntry();
        StackLayout bindable1 = new StackLayout();
        StackLayout stackLayout1 = new StackLayout();
        BindingExtension bindingExtension = new BindingExtension();
        DataTemplate dataTemplate1 = new DataTemplate();
        ListView listView = new ListView();
        StackLayout stackLayout2 = new StackLayout();
        StackLayout bindable2 = new StackLayout();
        ShelfSearch shelfSearch = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) shelfSearch, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("shelfSearch", (object) shelfSearch);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckForm", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtBarcode", (object) softkeyboardDisabledEntry);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelfList", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) listView, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfList", (object) listView);
        this.stckForm = stackLayout1;
        this.txtBarcode = softkeyboardDisabledEntry;
        this.stckShelfList = stackLayout2;
        this.lstShelfList = listView;
        stackLayout1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout1.SetValue(StackLayout.SpacingProperty, (object) 20.0);
        bindable1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        softkeyboardDisabledEntry.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        softkeyboardDisabledEntry.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Barkod Okutunuz");
        softkeyboardDisabledEntry.Completed += new EventHandler(shelfSearch.TxtBarcode_Completed);
        bindable1.Children.Add((View) softkeyboardDisabledEntry);
        stackLayout1.Children.Add((View) bindable1);
        bindable2.Children.Add((View) stackLayout1);
        stackLayout2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        listView.SetValue(ListView.RowHeightProperty, (object) 80);
        bindingExtension.Path = ".";
        BindingBase binding = ((IMarkupExtension<BindingBase>) bindingExtension).ProvideValue((IServiceProvider) null);
        listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding);
        listView.SetValue(ListView.SeparatorVisibilityProperty, (object) SeparatorVisibility.None);
        DataTemplate dataTemplate2 = dataTemplate1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfSearch.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_11 xamlCdataTemplate11 = new ShelfSearch.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_11();
        object[] objArray = new object[0 + 5];
        objArray[0] = (object) dataTemplate1;
        objArray[1] = (object) listView;
        objArray[2] = (object) stackLayout2;
        objArray[3] = (object) bindable2;
        objArray[4] = (object) shelfSearch;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate11.parentValues = objArray;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate11.root = shelfSearch;
        // ISSUE: reference to a compiler-generated method
        Func<object> func = new Func<object>(xamlCdataTemplate11.LoadDataTemplate);
        ((IDataTemplate) dataTemplate2).LoadTemplate = func;
        listView.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate1);
        stackLayout2.Children.Add((View) listView);
        bindable2.Children.Add((View) stackLayout2);
        shelfSearch.SetValue(ContentPage.ContentProperty, (object) bindable2);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<ShelfSearch>(typeof (ShelfSearch));
      this.stckForm = NameScopeExtensions.FindByName<StackLayout>(this, "stckForm");
      this.txtBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtBarcode");
      this.stckShelfList = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelfList");
      this.lstShelfList = NameScopeExtensions.FindByName<ListView>(this, "lstShelfList");
    }
  }
}
