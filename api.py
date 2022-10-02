import os
import hashlib
import time
import json
from flask import Flask
from flask import request
app = Flask(__name__)
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
is_inited = False
def create_guid():
    random_byte = hashlib.sha256(os.urandom(32)).hexdigest()
    guid = random_byte[0:8] + "-" + random_byte[8:12] + "-" + random_byte[12:16] + "-" + random_byte[16:20] +"-"+ random_byte[20:32]
    return guid
print(create_guid(),len(create_guid()))
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

@app.route("/ShelfWebApi",methods = ["GET","POST"])
def ShelfWebApi():
    data_to_login = {
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
    }
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
        data_to_login = {
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
        }
        return json.dumps(data_to_login)

@app.route("/ShelfWebApi/GetUserShelfOrdersBasket",methods = ["GET","POST"])
def pack_that():
    data_to_json = {
        "ShelfOrderID":"1020",
        "ShelfOrderNumber":"ABCDEF",
        "WareHouseCode":"AD42E13F",
        "ShelfOrderDate":time.ctime(),
        "CurrAccTypeCode":4,
        "SubCurrAccID":create_guid(),
        "AssignedUserID":5,
        "IsCompleted":"false",
        "IsApproved":"false",
        "ShippingNumber":"S1024",
        "CreatedDate":time.ctime(),
        "CreatedUserName":"Alperen",
        "UpdatedDate":time.ctime(),
        "UpdatedUserName":"DEFA"
    }
    return json.dumps(data_to_json)
app.debug = False
app.run("192.168.1.124",80)
