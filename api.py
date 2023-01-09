import os
import string
import random
import hashlib
import time
import json
from flask import Flask
from flask import request
app = Flask(__name__)
nums = {"S2024":1,"S2022":0,"S2023":2,"S2025":3,"S2026":4,"S2027":5,"S2030":6,"S2089":7,"S2090":8}
def create_guid():
    random_byte = hashlib.sha256(os.urandom(32)).hexdigest()
    guid = random_byte[0:8] + "-" + random_byte[8:12] + "-" + random_byte[12:16] + "-" + random_byte[16:20] +"-"+ random_byte[20:32]
    return guid
data = [{
        "ShelfOrderID":1002,
        "ShelfOrderNumber":"S2023",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1003,
        "ShelfOrderNumber":"S2025",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1004,
        "ShelfOrderNumber":"S2026",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1005,
        "ShelfOrderNumber":"S2027",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1006,
        "ShelfOrderNumber":"S2030",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1007,
        "ShelfOrderNumber":"S2089",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    },
    {
        "ShelfOrderID":1008,
        "ShelfOrderNumber":"S2090",
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    }]
orders = {}
descs = ["IPHONE 14 PRO MAX PRO","1 TANKER BIRA","KANDIL YAĞI","1 TANE SCOTTISH FOLD KEDİ","1 DEMET GÜL "]
data_to_json = []
generate_char = lambda:str(random.randint(1,3))+string.ascii_uppercase[random.randint(0,3)]
def random_items(i: int) -> list:
    magic_num = random.randint(1,15)
    final_data = []
    for i in range(1,magic_num):
        raf_kodu = "%s-%s-%s-%s" % (generate_char(),generate_char(),generate_char(),generate_char())
        final_data.append({"ItemDescription":random.choice(descs),"ItemCode":random.choice(["KAZAK","ÇORAP","FARE","KUMANDA"]),"ColorCode":random.choice(["WHITE","BLUE","RED","BLACK"]),"ItemDim1Code":"ABC","ItemDim2Code":"DEF","OrderQty":220.0,
            "ItemCodeLong":raf_kodu,
            "RowColorCode":"WHİTE",
            "IsFirst":True,
            "ItemDim1CodeStr":"1B",
            "PickingQtyStr":"Miktar: 10",
            "ApproveQtyStr":"Top Mik. :15",
            "ApproveQty":0,
            "PickingQty":0.0,
            "ShelfOrderDetailID":199,
            "ShelfOrderID":i,
            "Barcode":raf_kodu,
            "ShelfCode":raf_kodu,
            "ShelfName":raf_kodu
        })
    return final_data
for i in range(1,random.randint(1,39)):
    data_to_json.append({
        "ShelfOrderID":i,
        "ShelfOrderNumber":"S%s" % (i),
        "WareHouseCode":"ALPEREN",
        "ShelfOrderDate":"2019-01-06T17:16:40",
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S%s" % (i),
        "CreatedDate":"2019-01-06T17:16:40",
        "CreatedUserName":"Alperen",
        "UpdatedDate":"2019-01-06T17:16:40",
        "UpdatedUserName":"DEFA"
    })
    for _ in range(1,random.randint(1,100)):
        orders[str(i)]=random_items(i)

dss = {
        "1000":[
            {
                "ItemDescription":"SERIFE",
                "ItemCode":"SERIFE",
                "ColorCode":"SERIFE",
                "ItemDim1Code":"SERIFE",
                "ItemDim2Code":"SERIFE",
                "OrderQty":220.2,
                "ItemCodeLong":"SERIFE-SERIFE-SERIFE-SERIFE",
                "RowColorCode":"ALPERNE",
                "IsFirst":False,
                "ItemDim1CodeStr":"1B",
                "PickingQtyStr":"Miktar: 10",
                "ApproveQtyStr":"Top Mik. :15",
                "ApproveQty":0.0,
                "PickingQty":0.0,
                "ShelfOrderDetailID":199,
                "ShelfOrderID":2024,
                "Barcode":"ABDEF1234123",
                "ShelfCode":"1B-C1-2-3",
                "ShelfName":"Babet"

            },
            {
                "ItemDescription":"SERIFE",
                "ItemCode":"40X60 KOLI KALMAMIS",
                "ColorCode":"BEYAZ",
                "ItemDim1Code":"1B",
                "ItemDim2Code":"C1",
                "OrderQty":220.2,
                "ItemCodeLong":"DC-1B-C1-C1",
                "RowColorCode":"ALPERNE",
                "IsFirst":False,
                "ItemDim1CodeStr":"1B",
                "PickingQtyStr":"Miktar: 10",
                "ApproveQtyStr":"Top Mik. :15",
                "ApproveQty":15.0,
                "PickingQty":10.0,
                "ShelfOrderDetailID":199,
                "ShelfOrderID":2024,
                "Barcode":"ABDEF1234123",
                "ShelfCode":"1B-C1-2-3",
                "ShelfName":"Babet"

            },
            {
                "ItemDescription":"Alperen",
                "ItemCode":"12334",
                "ColorCode":"FA4A4B1",
                "ItemDim1Code":"1B",
                "ItemDim2Code":"C1",
                "OrderQty":220.2,
                "ItemCodeLong":"DC-1B-C1-C1",
                "RowColorCode":"ALPERNE",
                "IsFirst":True,
                "ItemDim1CodeStr":"1B",
                "PickingQtyStr":"Miktar: 10",
                "ApproveQtyStr":"Top Mik. :15",
                "ApproveQty":15.0,
                "PickingQty":10.0,
                "ShelfOrderDetailID":199,
                "ShelfOrderID":2024,
                "Barcode":"ABDEF1234123",
                "ShelfCode":"1B-C1-2-4",
                "ShelfName":"Babet"

            }
        ],
        "None":[
            {
                "ItemDescription":"-",
                "ItemCode":"-",
                "ColorCode":"Unknown",
                "ItemDim1Code":"-",
                "ItemDim2Code":"-",
                "OrderQty":0.0,
                "ItemCodeLong":"---",
                "RowColorCode":"000000",
                "IsFirst":True,
                "ItemDim1CodeStr":"1B",
                "PickingQtyStr":"Miktar: 10",
                "ApproveQtyStr":"Top Mik. :15",
                "ApproveQty":15.0,
                "PickingQty":10.0,
                "ShelfOrderDetailID":199,
                "ShelfOrderID":0000,
                "Barcode":"ABDEF1234123",
                "ShelfCode":"ALPEREN",
                "ShelfName":"Babet"

            }
        ]
    }
def get_data_from_barcode(order: str):
    for key in orders.keys():
        data = orders[key][0]["ItemCode"]
        if data == order:
            return orders[key][0]
            break
"""
namespace Shelf.Models
{
  public class ztIOShelfUser
  {
    public int ShelfUserID { get; set; }

    public string UserName { get; set; }

    public string FirstLastName { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsPickingUser { get; set; }

    public bool IsReceivingUser { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedUserName { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedUserName { get; set; }

    public string MenuIds { get; set; }
  }
}
"""
@app.route("/ShelfWebApi/Get")
def exec_it():
    return json.dumps({"Succes":True})
is_inited = False
def create_guid():
    random_byte = hashlib.sha256(os.urandom(32)).hexdigest()
    guid = random_byte[0:8] + "-" + random_byte[8:12] + "-" + random_byte[12:16] + "-" + random_byte[16:20] +"-"+ random_byte[20:32]
    return guid
print(create_guid(),len(create_guid()))
def get_data_from_order_id(num: int):
    list_ = data_to_json[0]
@app.route("/",methods = ["GET","POST"])
def index():
    b = ""
    b+="<head><title>Hi User</title></head>"
    b+="<body>"
    b+="<ul type='circle'>"
    b+="<li>Eger ki urllere girmek istiyorsan bulundugun url'e sifre girmen gerekli ornegin /login?password=123 gibi</li>"
    b+="</ul>"
    b+="</body>"
    return b
@app.route("/ShelfWebApi/GetItems")
def k():
    """using System;

    namespace OrderManagement.Models
    {
    public class pIOGetOrderManagementItemInfoReturnModel
    {
    public string ItemCode { get; set; }

    public string ItemDescription { get; set; }

    public string ColorCode { get; set; }

    public string ItemDim1Code { get; set; }

    public string ItemDim2Code { get; set; }

    public string ItemDim3Code { get; set; }

    public string CurrAccCode { get; set; }

    public string CurrAccDescription { get; set; }

    public Decimal? Price { get; set; }

    public double? PriceTL { get; set; }

    public double? PriceUSD { get; set; }

    public double? PriceEUR { get; set; }

    public string CurrencyCode { get; set; }

    public string PriceGroupCode { get; set; }

    public double? AvailableInventoryQty1 { get; set; }

    public string PriceCurrency => this.Price.ToString() + " " + this.CurrencyCode;

    public string PriceCurrencyTL => this.PriceTL.ToString() + " TL";

    public string PriceCurrencyUSD => this.PriceUSD.ToString() + " USD";

    public string PriceCurrencyEUR => this.PriceEUR.ToString() + " EUR";

    public string Qty => "Miktar : " + this.AvailableInventoryQty1.ToString();

    public double? PriceForBusiness { get; set; }

    public double? ExchangeRate { get; set; }

    public string CurrAccCurrencyCode { get; set; }

    public byte? CurrAccExchangeTypeCode { get; set; }

    public double? PriceVI { get; set; }

    public int BarcodeQty { get; set; }

    public double? USDRate { get; set; }

    public double? EURRate { get; set; }
    }
    }"""
    _data_to_json = [{
        "ItemCode":"DICK5110",
        "ItemDescription":"Hello",
        "ColorCode":"Alperen123",
        "ItemDim1Code":"DENEME1234",
        "ItemDim2Code":"TEST1234",
        "ItemDim3Code":"ALPERN1232",
        "CurrAccCode":"Alperen",
        "CurrAccDescription":"Alperen1234",
        "Price":34.5,
        "PriceTL":34.4,
        "PriceUSD":34.4,
        "PriceEUR":34.4,
        "CurrencyCode":"alperen_",
        "PriceGroupCode":"alperen123",
        "AvailableInventoryQty1":4.4,
        "PriceCurrency":"alperen123",
        "PriceCurrencyTL":"telt",
        "PriceCurrencyUSD":"telp123",
        "PriceCurrencyEUR":"alpds",
        "Qty":"alperne12312",
        "PriceForBusiness":4.4,
        "ExchangeRate":1.1,
        "CurrAccExchangeTypeCode":65,
        "PriceVI":4.4,
        "BarcodeQty":50,
        "USDRate":1000.0,
        "EURRate":500.4
    }]
    order_d = request.args.get("itemCode")
    return json.dumps(_data_to_json)
@app.route("/login",methods = ["GET","POST"])
def login_():
    global is_inited
    pass_ = request.args.get("password",type=str)
    real_password = "Qw0nzy123"
    if pass_ == real_password:
        is_inited = True
        return "Kabul Edildi"
    else:
        return "Sifre yanlis"
@app.route("/ShelfWebApi/GetShelfTypes")
def ret_it():
    """
    amespace Shelf.Models
{
  public class pIOUserShelfOrdersReturnModel
  {
    public int ShelfOrderID { get; set; }

    public string ShelfOrderNumber { get; set; }

    public string WarehouseCode { get; set; }

    public string CustomerName { get; set; }

    public DateTime? ShelfOrderDate { get; set; }

    public int CurrAccTypeCode { get; set; }

    public string CurrAccCode { get; set; }

    public Guid? SubCurrAccID { get; set; }

    public int? PickingTypeID { get; set; }

    public int AssignedUserID { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsApproved { get; set; }

    public string ShippingNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedUserName { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedUserName { get; set; }

    public byte ShelfOrderType { get; set; }

    public string ActiveShelfOrderNumber { get; set; }

    public bool? PickAndSort { get; set; }

    public string ShelfOrderTypeStr
    {
      get
      {
        if (this.ShelfOrderType == (byte) 1)
          return "Sevk Emri";
        return this.ShelfOrderType != (byte) 2 ? "Çoklu" : "Tekli";
      }
    }

    public bool? IsOrderCompletionPassword { get; set; }

    public bool? PickPackageApprove { get; set; }

    public string FileName { get; set; }

    public int? PrinterBrandID { get; set; }

    public bool? TurkishCharacterReplace { get; set; }

    public string ReportName { get; set; }

    public bool NetworkPrinter { get; set; }

    public bool IsReadShelfCode { get; set; }
    }
    }
    """
    return json.dumps({"hello":1})
@app.route("/ShelfWebApi/GetUserShelfOrdersForPivot")
def get_pivots():
    global orders
    return json.dumps(list(orders.keys()))
@app.route("/ShelfWebApi",methods = ["GET","POST"])
def ShelfWebApi():
    data_to_login = [{
        "ShelfUserID":1,
        "UserName":"T10",
        "Password":".",
        "FirstLastName":"abcdef",
        "IsAdmin":"true",
        "IsPickingUser":True,
        "IsReceivingUser":True,
        "IsBlocked":False,
        "CreatedDate":time.ctime(),
        "CreatedUserName":"T10",
        "UpdatedDate":time.ctime(),
        "UpdatedUserName":"abcdef",
        "MenuIds":"menu"
    }]
    return json.dumps(data_to_login)
@app.route("/ShelfWebApi/Login",methods = ["GET","POST"])
def index_that():
    username = "T10"
    passid = "."
    cli_user = request.args.get("userName",type = str)
    cli_pass = request.args.get("password")
    if not cli_pass == passid or not username == cli_user:
        return json.dumps({"ShelfUserID":1})
    else:
        data_to_login = [{
            "ShelfUserID":1,
            "UserName":"T10",
            "Password":".",
            "FirstLastName":"abcdef",
            "IsAdmin":"true",
            "IsPickingUser":True,
            "IsReceivingUser":True,
            "IsBlocked":False,
            "CreatedDate":"2019-01-06T17:16:40",
            "CreatedUserName":"T10",
            "UpdatedDate":"2019-01-06T17:16:40",
            "UpdatedUserName":"abcdef",
            "MenuIds":"menu"
        }]
        return json.dumps(data_to_login)
def get_order_number(number=None,del_=None) -> list or dict:
    global orders
    global nums
    global data_to_json
    """
    public string ItemDescription { get; set; }

    public string ItemCode { get; set; }

    public string ColorCode { get; set; }

    public string ItemDim1Code { get; set; }

    public string ItemDim2Code { get; set; }

    public double OrderQty { get; set; }

    public string ItemCodeLong => this.ItemCode + "-" + this.ColorCode + "-" + this.ItemDim1Code + (!string.IsNullOrEmpty(this.ItemDim2Code) ? "-" + this.ItemDim2Code : "");

    public string RowColorCode
    {
      get
      {
        if (this.IsFirst)
          return "DeepSkyBlue";
        return this.ApproveQty != this.PickingQty ? "White" : "Gray";
      }
    }

    public bool IsFirst { get; set; }

    public string ItemDim1CodeStr => !string.IsNullOrEmpty(this.ItemDim1Code) ? "Beden : " + this.ItemDim1Code : "";

    public string PickingQtyStr => this.PickingQty > 0.0 ? "Miktar : " + Convert.ToString(this.PickingQty) : "";

    public string ApproveQtyStr => this.ApproveQty > 0.0 ? "Top. Mik. : " + Convert.ToString(this.ApproveQty) : "";

    public double PickingQty { get; set; }

    public double ApproveQty { get; set; }

    public int ShelfOrderDetailID { get; set; }

    public int ShelfOrderID { get; set; }

    public string Barcode { get; set; }

    public string ShelfCode { get; set; }

    public string ShelfName { get; set; }
  }
  """
    if not del_:
        try:
            return orders[number]
        except:
            return orders["None"]
    else:
        is_succes = False
        for num in range(len(data_to_json)):
            sevk_no = data_to_json[num]["ShelfOrderNumber"]
            if is_succes:
                break
            if sevk_no == del_:
                is_succes = True
                del data_to_json[num]
        if is_succes:
            return json.dumps(del_)
        return json.dumps("fault")
@app.route("/ShelfWebApi/ShelfOrderCompleted")
def g():
    s = request.args.get("shelfOrderNumber")
    return get_order_number(del_=s)
@app.route("/ShelfWebApi/GetUserShelfOrders",methods = ["GET","POST"])
def pack_that():
    list_ = data_to_json
    return json.dumps(list_)
@app.route("/OrderWebApi/Login",methods = ["GET","POST"])
def send_it():
    return json.dumps({"OrderManagementUserID":1})
@app.route("/OrderWebApi/Get",methods = ["GET","POST"])
def r():
    return json.dumps({"AttributeCode":"1","AttributeDescription":"2"})
@app.route("/OrderWebApi/GetATAttribute",methods = ["GET","POST"])
def r_():
    return json.dumps({"AttributeCode":["1"],"AttributeDescription":["2"]})

def s():
    ord_ = request.args.get("shelfOrderNumber")
    return get_order_number(del_=ord_)
@app.route("/ShelfWebApi/GetPendingJobs")
def get():
    return json.dumps([{"OrderCount":len(data_to_json)}])
@app.route("/ShelfWebApi/GetShelfOrderDetailFromID")
def return_it():
    order = request.args.get("shelfOrderID")
    """
     public int ShelfOrderID { get; set; }

    public string ShelfOrderNumber { get; set; }

    public string WarehouseCode { get; set; }

    public DateTime? ShelfOrderDate { get; set; }

    public int CurrAccTypeCode { get; set; }

    public string CurrAccCode { get; set; }

    public Guid? SubCurrAccID { get; set; }

    public int AssignedUserID { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsApproved { get; set; }

    public string ShippingNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedUserName { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedUserName { get; set; }
    """
    data_to_pack = [
        {
            "ShelfOrderID":1,
            "ShelfOrderNumber":"S2022",
            "WareHouseCode":"ALPEREN",
            "ShelfOrderDate":"2019-01-06T17:16:40",
            "CurrAccTypeCode":4,
            "CurrAccCode":"ALPEREN",
            "SubCurrAccID":create_guid(),
            "AssignerUserID":5,
            "isCompleted":False,
            "isApproved":True,
            "ShippingNumber":"S1024",
            "CreatedDate":"2019-01-06T17:16:40",
            "CreatedUserName":"Alperen",
            "UpdatedDate":"2019-01-06T17:16:40",
            "UpdatedUserName":"Alperen"
        }
    ]
    return get_order_number(order)
app.debug = True
app.run("192.168.1.124",80)
