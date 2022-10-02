// Decompiled with JetBrains decompiler
// Type: Shelf.Views.ShelfEntry2
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
  [XamlFilePath("C:\\Shelf\\ShelfMobile\\Shelf\\Shelf\\Shelf\\Views\\ShelfEntry2.xaml")]
  public class ShelfEntry2 : ContentPage
  {
    private List<pIOShelfPurchaseOrderDetailReturnModel> shelfOrderDetail;
    private string selectedShelfCode;
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
    private StackLayout stckShelf;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtShelf;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnShelfBack;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnShelfNext;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtShelfBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnShelf;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private StackLayout stckBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private SoftkeyboardDisabledEntry txtBarcode;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Xamarin.Forms.Entry txtQty;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnPickOrder;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Button btnShelfOrderSuccess;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private ListView lstShelfDetail;
    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private Label lblListHeader;

    public Color ButtonColor => Color.FromRgb(52, 203, 201);

    public Color TextColor => Color.White;

    public ShelfEntry2()
    {
      this.InitializeComponent();
      this.Title = "Raf Girişi";
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = this.ButtonColor;
      List<pIOUserShelfPurchaseOrdersReturnModel> ordersReturnModelList = JsonConvert.DeserializeObject<List<pIOUserShelfPurchaseOrdersReturnModel>>(GlobalMob.PostJson(string.Format("GetUserShelfPurchaseOrders?userID={0}", new object[1]
      {
        (object) GlobalMob.User.ShelfUserID
      })));
      this.lstShelfOrder.IsVisible = ordersReturnModelList.Count > 0;
      this.stckEmptyMessage.IsVisible = ordersReturnModelList.Count == 0;
      if (this.stckEmptyMessage.IsVisible)
        this.stckContent.VerticalOptions = LayoutOptions.Center;
      this.lstShelfOrder.BindingContext = (object) ordersReturnModelList;
      this.lstShelfOrder.ItemSelected += (EventHandler<SelectedItemChangedEventArgs>) ((sender, e) =>
      {
        object selectedItem = ((ListView) sender).SelectedItem;
        if (selectedItem == null)
          return;
        this.Title = "Raf Giriş(İrsaliyeli)";
        pIOUserShelfPurchaseOrdersReturnModel ordersReturnModel = (pIOUserShelfPurchaseOrdersReturnModel) selectedItem;
        this.txtShelfOrderNumber.Text = "";
        this.txtShelfOrderNumber.Text = ordersReturnModel.PurchaseOrderNumber.Replace("S", "");
        this.stckShelfOrderList.IsVisible = false;
        this.stckForm.IsVisible = true;
        this.GetShelfDetail();
      });
      this.lstShelfDetail.ItemSelected += (EventHandler<SelectedItemChangedEventArgs>) ((sender, e) =>
      {
        object selectedItem = ((ListView) sender).SelectedItem;
        if (selectedItem == null)
          return;
        this.DisplayAlert("Bilgi", ((pIOShelfPurchaseOrderDetailReturnModel) selectedItem).ItemDescription, "", "Tamam");
        this.lstShelfDetail.SelectedItem = (object) null;
      });
    }

    private void GetShelfDetail()
    {
      this.shelfOrderDetail = new List<pIOShelfPurchaseOrderDetailReturnModel>();
      Device.BeginInvokeOnMainThread((Action) (() =>
      {
        this.shelfOrderDetail = JsonConvert.DeserializeObject<List<pIOShelfPurchaseOrderDetailReturnModel>>(GlobalMob.PostJson(string.Format("GetShelfPurchaseOrderDetail?purchaseOrderNumber=S{0}", new object[1]
        {
          (object) this.txtShelfOrderNumber.Text
        })));
        this.lstShelfDetail.ItemsSource = (IEnumerable) null;
        if (this.shelfOrderDetail.Count > 0)
        {
          this.lblListHeader.Text = "Raf Adı : " + this.shelfOrderDetail[0].ShelfCode;
          this.txtShelf.Text = this.shelfOrderDetail[0].ShelfCode;
          this.selectedShelfCode = this.txtShelf.Text;
          this.txtShelfBarcode.Text = "";
          this.stckBarcode.IsVisible = true;
          this.stckShelf.IsVisible = true;
          this.btnShelfOrderSuccess.IsVisible = true;
          this.NextBackButtonEnabled();
          this.txtShelfBarcode.Focus();
        }
        else
          this.btnShelfOrderSuccess.IsVisible = true;
      }));
    }

    private void FillListView()
    {
      List<pIOShelfPurchaseOrderDetailReturnModel> list = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.ShelfCode == this.txtShelfBarcode.Text)).ToList<pIOShelfPurchaseOrderDetailReturnModel>();
      this.lstShelfDetail.ItemsSource = (IEnumerable) null;
      this.lblListHeader.Text = "";
      this.selectedShelfCode = this.txtShelfBarcode.Text;
      this.txtShelf.Text = this.selectedShelfCode;
      if (list.Count > 0)
      {
        this.lstShelfDetail.IsVisible = true;
        this.lstShelfDetail.ItemsSource = (IEnumerable) list;
        this.lblListHeader.Text = "Raf Adı : " + this.txtShelf.Text;
      }
      else
        this.txtShelfBarcode.Text = "";
    }

    private async void GetBarcode()
    {
      ShelfEntry2 shelfEntry2 = this;
      string barcode = shelfEntry2.txtBarcode.Text;
      if (string.IsNullOrEmpty(barcode))
        return;
      pIOShelfPurchaseOrderDetailReturnModel detailReturnModel = shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.Barcode.Contains(barcode) && x.ShelfCode == this.selectedShelfCode && x.OrderQty != x.AllocatingQty)).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>();
      if (detailReturnModel != null)
      {
        int int32_1 = Convert.ToInt32(shelfEntry2.txtQty.Text);
        int purchaseOrderDetailId = detailReturnModel.PurchaseOrderDetailID;
        if (int32_1 <= 0)
          return;
        int int32_2 = Convert.ToInt32(GlobalMob.PostJson(string.Format("UpdateAlcShelfPurchaseOrderDetail?shelfPurchaseOrderDetailID={0}&qty={1}&barcode={2}", new object[3]
        {
          (object) purchaseOrderDetailId,
          (object) int32_1,
          (object) barcode
        })));
        if (int32_2 > 0)
        {
          detailReturnModel.AllocatingQty += (double) int32_2;
          shelfEntry2.shelfOrderDetail.Select<pIOShelfPurchaseOrderDetailReturnModel, pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, pIOShelfPurchaseOrderDetailReturnModel>) (c =>
          {
            c.LastReadBarcode = false;
            return c;
          })).ToList<pIOShelfPurchaseOrderDetailReturnModel>();
          detailReturnModel.LastReadBarcode = true;
          shelfEntry2.shelfOrderDetail = shelfEntry2.shelfOrderDetail.OrderByDescending<pIOShelfPurchaseOrderDetailReturnModel, bool>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.LastReadBarcode)).ToList<pIOShelfPurchaseOrderDetailReturnModel>();
          shelfEntry2.FillListView();
          shelfEntry2.txtQty.Text = "1";
          shelfEntry2.txtBarcode.Text = "";
          Device.BeginInvokeOnMainThread((Action) (async () =>
          {
            GlobalMob.PlaySave();
            await Task.Delay(250);
            this.txtBarcode?.Focus();
          }));
        }
        else if (int32_2 == -1)
        {
          GlobalMob.PlayError();
          int num = await shelfEntry2.DisplayAlert("Bilgi", "Hata Oluştu", "", "Tamam") ? 1 : 0;
          shelfEntry2.txtBarcode.Text = "";
          shelfEntry2.txtBarcode.Focus();
        }
        else
        {
          GlobalMob.PlayError();
          int num = await shelfEntry2.DisplayAlert("Bilgi", "Miktar Yetersiz", "", "Tamam") ? 1 : 0;
          shelfEntry2.txtBarcode.Text = "";
          shelfEntry2.txtBarcode.Focus();
        }
      }
      else
      {
        int num1 = shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.Barcode.Contains(barcode) && x.ShelfCode == this.selectedShelfCode && x.AllocatingQty == x.OrderQty)).Any<pIOShelfPurchaseOrderDetailReturnModel>() ? 1 : 0;
        GlobalMob.PlayError();
        string message = num1 != 0 ? "Sipariş miktarı tamamlandı" : "Ürün bulunamadı";
        int num2 = await shelfEntry2.DisplayAlert("Bilgi", message, "", "Tamam") ? 1 : 0;
        shelfEntry2.txtBarcode.Text = "";
        shelfEntry2.txtBarcode.Focus();
      }
    }

    private void btnPickOrder_Clicked(object sender, EventArgs e) => this.GetBarcode();

    private async void btnShelf_Clicked(object sender, EventArgs e)
    {
      ShelfEntry2 shelfEntry2 = this;
      // ISSUE: reference to a compiler-generated method
      if (shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>(new Func<pIOShelfPurchaseOrderDetailReturnModel, bool>(shelfEntry2.\u003CbtnShelf_Clicked\u003Eb__12_0)).Count<pIOShelfPurchaseOrderDetailReturnModel>() > 0)
      {
        // ISSUE: reference to a compiler-generated method
        if (shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>(new Func<pIOShelfPurchaseOrderDetailReturnModel, bool>(shelfEntry2.\u003CbtnShelf_Clicked\u003Eb__12_1)).ToList<pIOShelfPurchaseOrderDetailReturnModel>().Count > 0 && shelfEntry2.selectedShelfCode != shelfEntry2.txtShelfBarcode.Text)
        {
          if (!await shelfEntry2.DisplayAlert("Devam?", "Rafdaki ürünler tamamlanmadı.Devam etmek istiyor musunuz?", "Evet", "Hayır"))
            return;
          shelfEntry2.FillListView();
        }
        else
          shelfEntry2.FillListView();
      }
      else
      {
        int num = await shelfEntry2.DisplayAlert("Bilgi", "Raf bulunamadı", "", "Tamam") ? 1 : 0;
        shelfEntry2.txtShelfBarcode.Text = "";
        shelfEntry2.txtShelfBarcode.Focus();
      }
    }

    private void NextBackButtonEnabled()
    {
      pIOShelfPurchaseOrderDetailReturnModel item = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.ShelfCode == this.selectedShelfCode)).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>();
      this.btnShelfBack.IsEnabled = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x =>
      {
        int? sortOrder1 = x.SortOrder;
        int? sortOrder2 = item.SortOrder;
        return sortOrder1.GetValueOrDefault() < sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
      })).Any<pIOShelfPurchaseOrderDetailReturnModel>();
      this.btnShelfNext.IsEnabled = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x =>
      {
        int? sortOrder3 = x.SortOrder;
        int? sortOrder4 = item.SortOrder;
        return sortOrder3.GetValueOrDefault() > sortOrder4.GetValueOrDefault() & sortOrder3.HasValue & sortOrder4.HasValue;
      })).Any<pIOShelfPurchaseOrderDetailReturnModel>();
    }

    private async void btnShelfNext_Clicked(object sender, EventArgs e)
    {
      ShelfEntry2 shelfEntry2 = this;
      if (shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.ShelfCode == this.selectedShelfCode && x.OrderQty - x.AllocatingQty > 0.0)).ToList<pIOShelfPurchaseOrderDetailReturnModel>().Count > 0 && shelfEntry2.selectedShelfCode != shelfEntry2.txtShelfBarcode.Text)
      {
        if (!await shelfEntry2.DisplayAlert("Devam?", "Rafdaki ürünler tamamlanmadı.Devam etmek istiyor musunuz?", "Evet", "Hayır"))
          return;
      }
      pIOShelfPurchaseOrderDetailReturnModel item = shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.ShelfCode == this.selectedShelfCode)).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>();
      if (item != null)
      {
        pIOShelfPurchaseOrderDetailReturnModel detailReturnModel = shelfEntry2.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x =>
        {
          int? sortOrder1 = x.SortOrder;
          int? sortOrder2 = item.SortOrder;
          return sortOrder1.GetValueOrDefault() > sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
        })).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>();
        shelfEntry2.txtShelf.Text = detailReturnModel.ShelfCode;
        shelfEntry2.selectedShelfCode = shelfEntry2.txtShelf.Text;
        shelfEntry2.txtShelfBarcode.Text = "";
        shelfEntry2.txtShelfBarcode.Focus();
      }
      shelfEntry2.NextBackButtonEnabled();
    }

    private void btnShelfBack_Clicked(object sender, EventArgs e)
    {
      pIOShelfPurchaseOrderDetailReturnModel item = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x => x.ShelfCode == this.selectedShelfCode)).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>();
      if (item != null)
      {
        this.txtShelf.Text = this.shelfOrderDetail.Where<pIOShelfPurchaseOrderDetailReturnModel>((Func<pIOShelfPurchaseOrderDetailReturnModel, bool>) (x =>
        {
          int? sortOrder1 = x.SortOrder;
          int? sortOrder2 = item.SortOrder;
          return sortOrder1.GetValueOrDefault() < sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
        })).OrderByDescending<pIOShelfPurchaseOrderDetailReturnModel, int?>((Func<pIOShelfPurchaseOrderDetailReturnModel, int?>) (x => x.SortOrder)).FirstOrDefault<pIOShelfPurchaseOrderDetailReturnModel>().ShelfCode;
        this.selectedShelfCode = this.txtShelf.Text;
      }
      this.NextBackButtonEnabled();
    }

    private async void txtShelfBarcode_Completed(object sender, EventArgs e)
    {
      ShelfEntry2 shelfEntry2 = this;
      if (shelfEntry2.selectedShelfCode != shelfEntry2.txtShelfBarcode.Text)
      {
        int num = await shelfEntry2.DisplayAlert("Bilgi", "Hatalı Raf Kodu", "", "Tamam") ? 1 : 0;
        shelfEntry2.txtShelfBarcode.Text = "";
        shelfEntry2.txtShelfBarcode.Focus();
      }
      else
        shelfEntry2.FillListView();
      // ISSUE: reference to a compiler-generated method
      Device.BeginInvokeOnMainThread(new Action(shelfEntry2.\u003CtxtShelfBarcode_Completed\u003Eb__16_0));
    }

    private void txtBarcode_Completed(object sender, EventArgs e) => this.btnPickOrder_Clicked((object) null, (EventArgs) null);

    private async void btnShelfOrderSuccess_Clicked(object sender, EventArgs e)
    {
      ShelfEntry2 shelfEntry2 = this;
      if (!string.IsNullOrEmpty(shelfEntry2.txtShelfOrderNumber.Text))
      {
        if (!string.IsNullOrEmpty(JsonConvert.DeserializeObject<string>(GlobalMob.PostJson(string.Format("ShelfOrderPurchaseCompleted?shelfPurchaseOrderNumber={0}&isCompleted=false", new object[1]
        {
          (object) shelfEntry2.txtShelfOrderNumber.Text
        })))))
        {
          if (await shelfEntry2.DisplayAlert("Devam?", "Ürünler tamamlanmadı.Devam etmek istiyor musunuz?", "Evet", "Hayır"))
          {
            GlobalMob.PostJson(string.Format("ShelfOrderCompleted?shelfOrderNumber={0}&isCompleted=true", new object[1]
            {
              (object) shelfEntry2.txtShelfOrderNumber.Text
            }));
            int num = await shelfEntry2.DisplayAlert("Bilgi", "Raf Emri Tamamlandı", "", "Tamam") ? 1 : 0;
            Page page = await shelfEntry2.Navigation.PopAsync();
          }
          else
            shelfEntry2.GetShelfDetail();
        }
        else
        {
          GlobalMob.PostJson(string.Format("ShelfOrderPurchaseCompleted?shelfPurchaseOrderNumber={0}&isCompleted=true", new object[1]
          {
            (object) shelfEntry2.txtShelfOrderNumber.Text
          }));
          int num = await shelfEntry2.DisplayAlert("Bilgi", "Raf Emri Tamamlandı", "", "Tamam") ? 1 : 0;
          Page page = await shelfEntry2.Navigation.PopAsync();
        }
      }
      else
      {
        int num1 = await shelfEntry2.DisplayAlert("Bilgi", "Lütfen raf emri seçiniz", "", "Tamam") ? 1 : 0;
      }
    }

    [GeneratedCode("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
    private void InitializeComponent()
    {
      if (ResourceLoader.ResourceProvider != null && ResourceLoader.ResourceProvider("Shelf.Views.ShelfEntry2.xaml") != null)
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
        Xamarin.Forms.Entry entry2 = new Xamarin.Forms.Entry();
        ReferenceExtension referenceExtension1 = new ReferenceExtension();
        BindingExtension bindingExtension2 = new BindingExtension();
        ReferenceExtension referenceExtension2 = new ReferenceExtension();
        BindingExtension bindingExtension3 = new BindingExtension();
        Button button1 = new Button();
        ReferenceExtension referenceExtension3 = new ReferenceExtension();
        BindingExtension bindingExtension4 = new BindingExtension();
        ReferenceExtension referenceExtension4 = new ReferenceExtension();
        BindingExtension bindingExtension5 = new BindingExtension();
        Button button2 = new Button();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry1 = new SoftkeyboardDisabledEntry();
        ReferenceExtension referenceExtension5 = new ReferenceExtension();
        BindingExtension bindingExtension6 = new BindingExtension();
        ReferenceExtension referenceExtension6 = new ReferenceExtension();
        BindingExtension bindingExtension7 = new BindingExtension();
        Button button3 = new Button();
        StackLayout stackLayout3 = new StackLayout();
        SoftkeyboardDisabledEntry softkeyboardDisabledEntry2 = new SoftkeyboardDisabledEntry();
        Xamarin.Forms.Entry entry3 = new Xamarin.Forms.Entry();
        StackLayout stackLayout4 = new StackLayout();
        ReferenceExtension referenceExtension7 = new ReferenceExtension();
        BindingExtension bindingExtension8 = new BindingExtension();
        ReferenceExtension referenceExtension8 = new ReferenceExtension();
        BindingExtension bindingExtension9 = new BindingExtension();
        Button button4 = new Button();
        ReferenceExtension referenceExtension9 = new ReferenceExtension();
        BindingExtension bindingExtension10 = new BindingExtension();
        ReferenceExtension referenceExtension10 = new ReferenceExtension();
        BindingExtension bindingExtension11 = new BindingExtension();
        Button button5 = new Button();
        BindingExtension bindingExtension12 = new BindingExtension();
        Label label1 = new Label();
        StackLayout bindable3 = new StackLayout();
        DataTemplate dataTemplate2 = new DataTemplate();
        ListView listView2 = new ListView();
        StackLayout stackLayout5 = new StackLayout();
        StackLayout stackLayout6 = new StackLayout();
        ShelfEntry2 shelfEntry2 = this;
        NameScope nameScope = new NameScope();
        NameScope.SetNameScope((BindableObject) shelfEntry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("shelfentry", (object) shelfEntry2);
        NameScope.SetNameScope((BindableObject) stackLayout6, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckContent", (object) stackLayout6);
        NameScope.SetNameScope((BindableObject) stackLayout2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelfOrderList", (object) stackLayout2);
        NameScope.SetNameScope((BindableObject) stackLayout1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckEmptyMessage", (object) stackLayout1);
        NameScope.SetNameScope((BindableObject) bindable1, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) listView1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfOrder", (object) listView1);
        NameScope.SetNameScope((BindableObject) stackLayout5, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckForm", (object) stackLayout5);
        NameScope.SetNameScope((BindableObject) bindable2, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) entry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelfOrderNumber", (object) entry1);
        NameScope.SetNameScope((BindableObject) stackLayout3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckShelf", (object) stackLayout3);
        NameScope.SetNameScope((BindableObject) entry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelf", (object) entry2);
        NameScope.SetNameScope((BindableObject) button1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnShelfBack", (object) button1);
        NameScope.SetNameScope((BindableObject) button2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnShelfNext", (object) button2);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtShelfBarcode", (object) softkeyboardDisabledEntry1);
        NameScope.SetNameScope((BindableObject) button3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnShelf", (object) button3);
        NameScope.SetNameScope((BindableObject) stackLayout4, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("stckBarcode", (object) stackLayout4);
        NameScope.SetNameScope((BindableObject) softkeyboardDisabledEntry2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtBarcode", (object) softkeyboardDisabledEntry2);
        NameScope.SetNameScope((BindableObject) entry3, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("txtQty", (object) entry3);
        NameScope.SetNameScope((BindableObject) button4, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnPickOrder", (object) button4);
        NameScope.SetNameScope((BindableObject) button5, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("btnShelfOrderSuccess", (object) button5);
        NameScope.SetNameScope((BindableObject) listView2, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lstShelfDetail", (object) listView2);
        NameScope.SetNameScope((BindableObject) bindable3, (INameScope) nameScope);
        NameScope.SetNameScope((BindableObject) label1, (INameScope) nameScope);
        ((INameScope) nameScope).RegisterName("lblListHeader", (object) label1);
        this.stckContent = stackLayout6;
        this.stckShelfOrderList = stackLayout2;
        this.stckEmptyMessage = stackLayout1;
        this.lstShelfOrder = listView1;
        this.stckForm = stackLayout5;
        this.txtShelfOrderNumber = entry1;
        this.stckShelf = stackLayout3;
        this.txtShelf = entry2;
        this.btnShelfBack = button1;
        this.btnShelfNext = button2;
        this.txtShelfBarcode = softkeyboardDisabledEntry1;
        this.btnShelf = button3;
        this.stckBarcode = stackLayout4;
        this.txtBarcode = softkeyboardDisabledEntry2;
        this.txtQty = entry3;
        this.btnPickOrder = button4;
        this.btnShelfOrderSuccess = button5;
        this.lstShelfDetail = listView2;
        this.lblListHeader = label1;
        stackLayout6.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout6.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        stackLayout2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout2.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout1.SetValue(VisualElement.IsVisibleProperty, (object) false);
        stackLayout1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.Center);
        stackLayout1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Center);
        bindable1.SetValue(Label.TextProperty, (object) "Bekleyen Raf Emri Bulunmamaktadır.");
        bindable1.SetValue(View.VerticalOptionsProperty, (object) LayoutOptions.CenterAndExpand);
        bindable1.SetValue(Label.FontAttributesProperty, (object) FontAttributes.Bold);
        Label label2 = bindable1;
        BindableProperty fontSizeProperty = Label.FontSizeProperty;
        FontSizeConverter fontSizeConverter = new FontSizeConverter();
        XamlServiceProvider xamlServiceProvider1 = new XamlServiceProvider();
        Type type1 = typeof (IProvideValueTarget);
        object[] objectAndParents1 = new object[0 + 5];
        objectAndParents1[0] = (object) bindable1;
        objectAndParents1[1] = (object) stackLayout1;
        objectAndParents1[2] = (object) stackLayout2;
        objectAndParents1[3] = (object) stackLayout6;
        objectAndParents1[4] = (object) shelfEntry2;
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
        XamlTypeResolver service2 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver1, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider1.Add(type2, (object) service2);
        xamlServiceProvider1.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(12, 128)));
        object obj1 = ((IExtendedTypeConverter) fontSizeConverter).ConvertFromInvariantString("Medium", (IServiceProvider) xamlServiceProvider1);
        label2.SetValue(fontSizeProperty, obj1);
        stackLayout1.Children.Add((View) bindable1);
        stackLayout2.Children.Add((View) stackLayout1);
        bindingExtension1.Path = ".";
        BindingBase binding1 = ((IMarkupExtension<BindingBase>) bindingExtension1).ProvideValue((IServiceProvider) null);
        listView1.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding1);
        DataTemplate dataTemplate3 = dataTemplate1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfEntry2.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_5 xamlCdataTemplate5 = new ShelfEntry2.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_5();
        object[] objArray1 = new object[0 + 5];
        objArray1[0] = (object) dataTemplate1;
        objArray1[1] = (object) listView1;
        objArray1[2] = (object) stackLayout2;
        objArray1[3] = (object) stackLayout6;
        objArray1[4] = (object) shelfEntry2;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate5.parentValues = objArray1;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate5.root = shelfEntry2;
        // ISSUE: reference to a compiler-generated method
        Func<object> func1 = new Func<object>(xamlCdataTemplate5.LoadDataTemplate);
        ((IDataTemplate) dataTemplate3).LoadTemplate = func1;
        listView1.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate1);
        stackLayout2.Children.Add((View) listView1);
        stackLayout6.Children.Add((View) stackLayout2);
        stackLayout5.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Vertical);
        stackLayout5.SetValue(StackLayout.SpacingProperty, (object) 20.0);
        stackLayout5.SetValue(VisualElement.IsVisibleProperty, (object) false);
        stackLayout5.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable2.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        entry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Raf Emri Numarası Giriniz");
        entry1.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        entry1.SetValue(VisualElement.InputTransparentProperty, (object) true);
        bindable2.Children.Add((View) entry1);
        stackLayout5.Children.Add((View) bindable2);
        stackLayout3.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout3.SetValue(VisualElement.IsVisibleProperty, (object) false);
        entry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "");
        entry2.SetValue(VisualElement.InputTransparentProperty, (object) true);
        entry2.SetValue(VisualElement.WidthRequestProperty, (object) 100.0);
        entry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.Start);
        stackLayout3.Children.Add((View) entry2);
        button1.SetValue(Button.TextProperty, (object) "<");
        referenceExtension1.Name = "shelfentry";
        ReferenceExtension referenceExtension11 = referenceExtension1;
        XamlServiceProvider xamlServiceProvider2 = new XamlServiceProvider();
        Type type3 = typeof (IProvideValueTarget);
        object[] objectAndParents2 = new object[0 + 6];
        objectAndParents2[0] = (object) bindingExtension2;
        objectAndParents2[1] = (object) button1;
        objectAndParents2[2] = (object) stackLayout3;
        objectAndParents2[3] = (object) stackLayout5;
        objectAndParents2[4] = (object) stackLayout6;
        objectAndParents2[5] = (object) shelfEntry2;
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
        XamlTypeResolver service4 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver2, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider2.Add(type4, (object) service4);
        xamlServiceProvider2.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(39, 25)));
        object obj2 = referenceExtension11.ProvideValue((IServiceProvider) xamlServiceProvider2);
        bindingExtension2.Source = obj2;
        bindingExtension2.Path = "ButtonColor";
        BindingBase binding2 = ((IMarkupExtension<BindingBase>) bindingExtension2).ProvideValue((IServiceProvider) null);
        button1.SetBinding(VisualElement.BackgroundColorProperty, binding2);
        referenceExtension2.Name = "shelfentry";
        ReferenceExtension referenceExtension12 = referenceExtension2;
        XamlServiceProvider xamlServiceProvider3 = new XamlServiceProvider();
        Type type5 = typeof (IProvideValueTarget);
        object[] objectAndParents3 = new object[0 + 6];
        objectAndParents3[0] = (object) bindingExtension3;
        objectAndParents3[1] = (object) button1;
        objectAndParents3[2] = (object) stackLayout3;
        objectAndParents3[3] = (object) stackLayout5;
        objectAndParents3[4] = (object) stackLayout6;
        objectAndParents3[5] = (object) shelfEntry2;
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
        XamlTypeResolver service6 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver3, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider3.Add(type6, (object) service6);
        xamlServiceProvider3.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(39, 98)));
        object obj3 = referenceExtension12.ProvideValue((IServiceProvider) xamlServiceProvider3);
        bindingExtension3.Source = obj3;
        bindingExtension3.Path = "TextColor";
        BindingBase binding3 = ((IMarkupExtension<BindingBase>) bindingExtension3).ProvideValue((IServiceProvider) null);
        button1.SetBinding(Button.TextColorProperty, binding3);
        button1.Clicked += new EventHandler(shelfEntry2.btnShelfBack_Clicked);
        button1.SetValue(VisualElement.WidthRequestProperty, (object) 40.0);
        button1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.StartAndExpand);
        stackLayout3.Children.Add((View) button1);
        button2.SetValue(Button.TextProperty, (object) ">");
        referenceExtension3.Name = "shelfentry";
        ReferenceExtension referenceExtension13 = referenceExtension3;
        XamlServiceProvider xamlServiceProvider4 = new XamlServiceProvider();
        Type type7 = typeof (IProvideValueTarget);
        object[] objectAndParents4 = new object[0 + 6];
        objectAndParents4[0] = (object) bindingExtension4;
        objectAndParents4[1] = (object) button2;
        objectAndParents4[2] = (object) stackLayout3;
        objectAndParents4[3] = (object) stackLayout5;
        objectAndParents4[4] = (object) stackLayout6;
        objectAndParents4[5] = (object) shelfEntry2;
        SimpleValueTargetProvider service7 = new SimpleValueTargetProvider(objectAndParents4, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider4.Add(type7, (object) service7);
        xamlServiceProvider4.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type8 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver4 = new XmlNamespaceResolver();
        namespaceResolver4.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver4.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver4.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service8 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver4, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider4.Add(type8, (object) service8);
        xamlServiceProvider4.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(42, 25)));
        object obj4 = referenceExtension13.ProvideValue((IServiceProvider) xamlServiceProvider4);
        bindingExtension4.Source = obj4;
        bindingExtension4.Path = "ButtonColor";
        BindingBase binding4 = ((IMarkupExtension<BindingBase>) bindingExtension4).ProvideValue((IServiceProvider) null);
        button2.SetBinding(VisualElement.BackgroundColorProperty, binding4);
        referenceExtension4.Name = "shelfentry";
        ReferenceExtension referenceExtension14 = referenceExtension4;
        XamlServiceProvider xamlServiceProvider5 = new XamlServiceProvider();
        Type type9 = typeof (IProvideValueTarget);
        object[] objectAndParents5 = new object[0 + 6];
        objectAndParents5[0] = (object) bindingExtension5;
        objectAndParents5[1] = (object) button2;
        objectAndParents5[2] = (object) stackLayout3;
        objectAndParents5[3] = (object) stackLayout5;
        objectAndParents5[4] = (object) stackLayout6;
        objectAndParents5[5] = (object) shelfEntry2;
        SimpleValueTargetProvider service9 = new SimpleValueTargetProvider(objectAndParents5, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider5.Add(type9, (object) service9);
        xamlServiceProvider5.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type10 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver5 = new XmlNamespaceResolver();
        namespaceResolver5.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver5.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver5.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service10 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver5, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider5.Add(type10, (object) service10);
        xamlServiceProvider5.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(42, 98)));
        object obj5 = referenceExtension14.ProvideValue((IServiceProvider) xamlServiceProvider5);
        bindingExtension5.Source = obj5;
        bindingExtension5.Path = "TextColor";
        BindingBase binding5 = ((IMarkupExtension<BindingBase>) bindingExtension5).ProvideValue((IServiceProvider) null);
        button2.SetBinding(Button.TextColorProperty, binding5);
        button2.Clicked += new EventHandler(shelfEntry2.btnShelfNext_Clicked);
        button2.SetValue(VisualElement.WidthRequestProperty, (object) 40.0);
        button2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.StartAndExpand);
        stackLayout3.Children.Add((View) button2);
        softkeyboardDisabledEntry1.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Raf No Okutunuz");
        softkeyboardDisabledEntry1.SetValue(VisualElement.WidthRequestProperty, (object) 200.0);
        softkeyboardDisabledEntry1.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        softkeyboardDisabledEntry1.Completed += new EventHandler(shelfEntry2.txtShelfBarcode_Completed);
        stackLayout3.Children.Add((View) softkeyboardDisabledEntry1);
        button3.SetValue(Button.TextProperty, (object) "...");
        button3.Clicked += new EventHandler(shelfEntry2.btnShelf_Clicked);
        referenceExtension5.Name = "shelfentry";
        ReferenceExtension referenceExtension15 = referenceExtension5;
        XamlServiceProvider xamlServiceProvider6 = new XamlServiceProvider();
        Type type11 = typeof (IProvideValueTarget);
        object[] objectAndParents6 = new object[0 + 6];
        objectAndParents6[0] = (object) bindingExtension6;
        objectAndParents6[1] = (object) button3;
        objectAndParents6[2] = (object) stackLayout3;
        objectAndParents6[3] = (object) stackLayout5;
        objectAndParents6[4] = (object) stackLayout6;
        objectAndParents6[5] = (object) shelfEntry2;
        SimpleValueTargetProvider service11 = new SimpleValueTargetProvider(objectAndParents6, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider6.Add(type11, (object) service11);
        xamlServiceProvider6.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type12 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver6 = new XmlNamespaceResolver();
        namespaceResolver6.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver6.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver6.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service12 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver6, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider6.Add(type12, (object) service12);
        xamlServiceProvider6.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(46, 25)));
        object obj6 = referenceExtension15.ProvideValue((IServiceProvider) xamlServiceProvider6);
        bindingExtension6.Source = obj6;
        bindingExtension6.Path = "ButtonColor";
        BindingBase binding6 = ((IMarkupExtension<BindingBase>) bindingExtension6).ProvideValue((IServiceProvider) null);
        button3.SetBinding(VisualElement.BackgroundColorProperty, binding6);
        referenceExtension6.Name = "shelfentry";
        ReferenceExtension referenceExtension16 = referenceExtension6;
        XamlServiceProvider xamlServiceProvider7 = new XamlServiceProvider();
        Type type13 = typeof (IProvideValueTarget);
        object[] objectAndParents7 = new object[0 + 6];
        objectAndParents7[0] = (object) bindingExtension7;
        objectAndParents7[1] = (object) button3;
        objectAndParents7[2] = (object) stackLayout3;
        objectAndParents7[3] = (object) stackLayout5;
        objectAndParents7[4] = (object) stackLayout6;
        objectAndParents7[5] = (object) shelfEntry2;
        SimpleValueTargetProvider service13 = new SimpleValueTargetProvider(objectAndParents7, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider7.Add(type13, (object) service13);
        xamlServiceProvider7.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type14 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver7 = new XmlNamespaceResolver();
        namespaceResolver7.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver7.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver7.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service14 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver7, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider7.Add(type14, (object) service14);
        xamlServiceProvider7.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(46, 98)));
        object obj7 = referenceExtension16.ProvideValue((IServiceProvider) xamlServiceProvider7);
        bindingExtension7.Source = obj7;
        bindingExtension7.Path = "TextColor";
        BindingBase binding7 = ((IMarkupExtension<BindingBase>) bindingExtension7).ProvideValue((IServiceProvider) null);
        button3.SetBinding(Button.TextColorProperty, binding7);
        button3.SetValue(VisualElement.IsVisibleProperty, (object) false);
        button3.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.EndAndExpand);
        stackLayout3.Children.Add((View) button3);
        stackLayout5.Children.Add((View) stackLayout3);
        stackLayout4.SetValue(StackLayout.OrientationProperty, (object) StackOrientation.Horizontal);
        stackLayout4.SetValue(VisualElement.IsVisibleProperty, (object) false);
        softkeyboardDisabledEntry2.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Barkod No Giriniz/Okutunuz");
        softkeyboardDisabledEntry2.SetValue(VisualElement.IsVisibleProperty, (object) true);
        softkeyboardDisabledEntry2.Completed += new EventHandler(shelfEntry2.txtBarcode_Completed);
        softkeyboardDisabledEntry2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        stackLayout4.Children.Add((View) softkeyboardDisabledEntry2);
        entry3.SetValue(Xamarin.Forms.Entry.TextProperty, (object) "1");
        entry3.SetValue(Xamarin.Forms.Entry.PlaceholderProperty, (object) "Miktar");
        entry3.SetValue(InputView.KeyboardProperty, new KeyboardTypeConverter().ConvertFromInvariantString("Numeric"));
        entry3.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.EndAndExpand);
        stackLayout4.Children.Add((View) entry3);
        stackLayout5.Children.Add((View) stackLayout4);
        button4.SetValue(Button.TextProperty, (object) "Ekle/Okut");
        referenceExtension7.Name = "shelfentry";
        ReferenceExtension referenceExtension17 = referenceExtension7;
        XamlServiceProvider xamlServiceProvider8 = new XamlServiceProvider();
        Type type15 = typeof (IProvideValueTarget);
        object[] objectAndParents8 = new object[0 + 5];
        objectAndParents8[0] = (object) bindingExtension8;
        objectAndParents8[1] = (object) button4;
        objectAndParents8[2] = (object) stackLayout5;
        objectAndParents8[3] = (object) stackLayout6;
        objectAndParents8[4] = (object) shelfEntry2;
        SimpleValueTargetProvider service15 = new SimpleValueTargetProvider(objectAndParents8, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider8.Add(type15, (object) service15);
        xamlServiceProvider8.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type16 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver8 = new XmlNamespaceResolver();
        namespaceResolver8.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver8.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver8.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service16 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver8, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider8.Add(type16, (object) service16);
        xamlServiceProvider8.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(54, 21)));
        object obj8 = referenceExtension17.ProvideValue((IServiceProvider) xamlServiceProvider8);
        bindingExtension8.Source = obj8;
        bindingExtension8.Path = "ButtonColor";
        BindingBase binding8 = ((IMarkupExtension<BindingBase>) bindingExtension8).ProvideValue((IServiceProvider) null);
        button4.SetBinding(VisualElement.BackgroundColorProperty, binding8);
        referenceExtension8.Name = "shelfentry";
        ReferenceExtension referenceExtension18 = referenceExtension8;
        XamlServiceProvider xamlServiceProvider9 = new XamlServiceProvider();
        Type type17 = typeof (IProvideValueTarget);
        object[] objectAndParents9 = new object[0 + 5];
        objectAndParents9[0] = (object) bindingExtension9;
        objectAndParents9[1] = (object) button4;
        objectAndParents9[2] = (object) stackLayout5;
        objectAndParents9[3] = (object) stackLayout6;
        objectAndParents9[4] = (object) shelfEntry2;
        SimpleValueTargetProvider service17 = new SimpleValueTargetProvider(objectAndParents9, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider9.Add(type17, (object) service17);
        xamlServiceProvider9.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type18 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver9 = new XmlNamespaceResolver();
        namespaceResolver9.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver9.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver9.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service18 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver9, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider9.Add(type18, (object) service18);
        xamlServiceProvider9.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(54, 94)));
        object obj9 = referenceExtension18.ProvideValue((IServiceProvider) xamlServiceProvider9);
        bindingExtension9.Source = obj9;
        bindingExtension9.Path = "TextColor";
        BindingBase binding9 = ((IMarkupExtension<BindingBase>) bindingExtension9).ProvideValue((IServiceProvider) null);
        button4.SetBinding(Button.TextColorProperty, binding9);
        button4.SetValue(VisualElement.IsVisibleProperty, (object) false);
        button4.Clicked += new EventHandler(shelfEntry2.btnPickOrder_Clicked);
        stackLayout5.Children.Add((View) button4);
        button5.SetValue(Button.TextProperty, (object) "Tamamla");
        button5.Clicked += new EventHandler(shelfEntry2.btnShelfOrderSuccess_Clicked);
        referenceExtension9.Name = "shelfentry";
        ReferenceExtension referenceExtension19 = referenceExtension9;
        XamlServiceProvider xamlServiceProvider10 = new XamlServiceProvider();
        Type type19 = typeof (IProvideValueTarget);
        object[] objectAndParents10 = new object[0 + 5];
        objectAndParents10[0] = (object) bindingExtension10;
        objectAndParents10[1] = (object) button5;
        objectAndParents10[2] = (object) stackLayout5;
        objectAndParents10[3] = (object) stackLayout6;
        objectAndParents10[4] = (object) shelfEntry2;
        SimpleValueTargetProvider service19 = new SimpleValueTargetProvider(objectAndParents10, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider10.Add(type19, (object) service19);
        xamlServiceProvider10.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type20 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver10 = new XmlNamespaceResolver();
        namespaceResolver10.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver10.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver10.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service20 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver10, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider10.Add(type20, (object) service20);
        xamlServiceProvider10.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(57, 21)));
        object obj10 = referenceExtension19.ProvideValue((IServiceProvider) xamlServiceProvider10);
        bindingExtension10.Source = obj10;
        bindingExtension10.Path = "ButtonColor";
        BindingBase binding10 = ((IMarkupExtension<BindingBase>) bindingExtension10).ProvideValue((IServiceProvider) null);
        button5.SetBinding(VisualElement.BackgroundColorProperty, binding10);
        referenceExtension10.Name = "shelfentry";
        ReferenceExtension referenceExtension20 = referenceExtension10;
        XamlServiceProvider xamlServiceProvider11 = new XamlServiceProvider();
        Type type21 = typeof (IProvideValueTarget);
        object[] objectAndParents11 = new object[0 + 5];
        objectAndParents11[0] = (object) bindingExtension11;
        objectAndParents11[1] = (object) button5;
        objectAndParents11[2] = (object) stackLayout5;
        objectAndParents11[3] = (object) stackLayout6;
        objectAndParents11[4] = (object) shelfEntry2;
        SimpleValueTargetProvider service21 = new SimpleValueTargetProvider(objectAndParents11, (object) typeof (BindingExtension).GetProperty("Source"));
        xamlServiceProvider11.Add(type21, (object) service21);
        xamlServiceProvider11.Add(typeof (INameScopeProvider), (object) new NameScopeProvider()
        {
          NameScope = (INameScope) nameScope
        });
        Type type22 = typeof (IXamlTypeResolver);
        XmlNamespaceResolver namespaceResolver11 = new XmlNamespaceResolver();
        namespaceResolver11.Add("", "http://xamarin.com/schemas/2014/forms");
        namespaceResolver11.Add("x", "http://schemas.microsoft.com/winfx/2009/xaml");
        namespaceResolver11.Add("local", "clr-namespace:XFNoSoftKeyboadEntryControl");
        XamlTypeResolver service22 = new XamlTypeResolver((IXmlNamespaceResolver) namespaceResolver11, typeof (ShelfEntry2).Assembly);
        xamlServiceProvider11.Add(type22, (object) service22);
        xamlServiceProvider11.Add(typeof (IXmlLineInfoProvider), (object) new XmlLineInfoProvider((IXmlLineInfo) new XmlLineInfo(57, 94)));
        object obj11 = referenceExtension20.ProvideValue((IServiceProvider) xamlServiceProvider11);
        bindingExtension11.Source = obj11;
        bindingExtension11.Path = "TextColor";
        BindingBase binding11 = ((IMarkupExtension<BindingBase>) bindingExtension11).ProvideValue((IServiceProvider) null);
        button5.SetBinding(Button.TextColorProperty, binding11);
        button5.SetValue(VisualElement.IsVisibleProperty, (object) false);
        stackLayout5.Children.Add((View) button5);
        bindingExtension12.Path = ".";
        BindingBase binding12 = ((IMarkupExtension<BindingBase>) bindingExtension12).ProvideValue((IServiceProvider) null);
        listView2.SetBinding(ItemsView<Cell>.ItemsSourceProperty, binding12);
        listView2.SetValue(VisualElement.IsVisibleProperty, (object) false);
        listView2.SetValue(VisualElement.HeightRequestProperty, (object) 500.0);
        listView2.SetValue(View.HorizontalOptionsProperty, (object) LayoutOptions.FillAndExpand);
        bindable3.SetValue(Layout.PaddingProperty, (object) new Thickness(10.0, 5.0, 0.0, 5.0));
        bindable3.SetValue(VisualElement.BackgroundColorProperty, (object) Color.AliceBlue);
        label1.SetValue(Label.TextProperty, (object) "Raf Detayları");
        label1.SetValue(Label.FontProperty, new FontTypeConverter().ConvertFromInvariantString("Bold,20"));
        bindable3.Children.Add((View) label1);
        listView2.SetValue(ListView.HeaderProperty, (object) bindable3);
        DataTemplate dataTemplate4 = dataTemplate2;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ShelfEntry2.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_6 xamlCdataTemplate6 = new ShelfEntry2.\u003CInitializeComponent\u003E_anonXamlCDataTemplate_6();
        object[] objArray2 = new object[0 + 5];
        objArray2[0] = (object) dataTemplate2;
        objArray2[1] = (object) listView2;
        objArray2[2] = (object) stackLayout5;
        objArray2[3] = (object) stackLayout6;
        objArray2[4] = (object) shelfEntry2;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate6.parentValues = objArray2;
        // ISSUE: reference to a compiler-generated field
        xamlCdataTemplate6.root = shelfEntry2;
        // ISSUE: reference to a compiler-generated method
        Func<object> func2 = new Func<object>(xamlCdataTemplate6.LoadDataTemplate);
        ((IDataTemplate) dataTemplate4).LoadTemplate = func2;
        listView2.SetValue(ItemsView<Cell>.ItemTemplateProperty, (object) dataTemplate2);
        stackLayout5.Children.Add((View) listView2);
        stackLayout6.Children.Add((View) stackLayout5);
        shelfEntry2.SetValue(ContentPage.ContentProperty, (object) stackLayout6);
      }
    }

    private void __InitComponentRuntime()
    {
      this.LoadFromXaml<ShelfEntry2>(typeof (ShelfEntry2));
      this.stckContent = NameScopeExtensions.FindByName<StackLayout>(this, "stckContent");
      this.stckShelfOrderList = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelfOrderList");
      this.stckEmptyMessage = NameScopeExtensions.FindByName<StackLayout>(this, "stckEmptyMessage");
      this.lstShelfOrder = NameScopeExtensions.FindByName<ListView>(this, "lstShelfOrder");
      this.stckForm = NameScopeExtensions.FindByName<StackLayout>(this, "stckForm");
      this.txtShelfOrderNumber = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtShelfOrderNumber");
      this.stckShelf = NameScopeExtensions.FindByName<StackLayout>(this, "stckShelf");
      this.txtShelf = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtShelf");
      this.btnShelfBack = NameScopeExtensions.FindByName<Button>(this, "btnShelfBack");
      this.btnShelfNext = NameScopeExtensions.FindByName<Button>(this, "btnShelfNext");
      this.txtShelfBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtShelfBarcode");
      this.btnShelf = NameScopeExtensions.FindByName<Button>(this, "btnShelf");
      this.stckBarcode = NameScopeExtensions.FindByName<StackLayout>(this, "stckBarcode");
      this.txtBarcode = NameScopeExtensions.FindByName<SoftkeyboardDisabledEntry>(this, "txtBarcode");
      this.txtQty = NameScopeExtensions.FindByName<Xamarin.Forms.Entry>(this, "txtQty");
      this.btnPickOrder = NameScopeExtensions.FindByName<Button>(this, "btnPickOrder");
      this.btnShelfOrderSuccess = NameScopeExtensions.FindByName<Button>(this, "btnShelfOrderSuccess");
      this.lstShelfDetail = NameScopeExtensions.FindByName<ListView>(this, "lstShelfDetail");
      this.lblListHeader = NameScopeExtensions.FindByName<Label>(this, "lblListHeader");
    }
  }
}
