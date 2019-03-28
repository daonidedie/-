
//用户登陆
function login() {
    var diag = new Dialog("Diag");
    diag.Width = 275;
    diag.Height = 150;
    diag.Title = "用户登陆"
    diag.URL = "../../../UserLogin.aspx";
    diag.ShowButtonRow = false;
    diag.show();

    return false;
}

//管理登陆
function Adminlogin() {
    var diag = new Dialog("Diag");
    diag.Width = 275;
    diag.Height = 150;
    diag.Title = "管理员登陆";
    diag.URL = "../../../Admin/AdminLogin.aspx";
    diag.ShowButtonRow = false;
    diag.show();
    return false;
}


//判断用户是否登陆
function Islogin(pid, carid) {

    var rnd = Math.random();
    var url = "handler/HandlerUserLogin.ashx?rnd="+rnd; //加随机数，防止AJAX只响应一次

    var xmlhttp;
    if (window.ActiveXObject) {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    } else if (window.XMLHttpRequest) {
        xmlhttp = new XMLHttpRequest();
    }

    xmlhttp.open("get", url, true);
    xmlhttp.send(null);

    xmlhttp.onreadystatechange = function () {

        if (xmlhttp.readystate == 4 && xmlhttp.status == 200) {


            var xmldom = xmlhttp.responseText;
            if (xmldom != 1) {
                alert("你尚未登陆，请先登陆再购买！");
            }

            if (xmldom == 1) {
                addtoShopCart(pid, carid);
            }
        }
    }

    return false;

}


//往购物车添加物品
function addtoShopCart(pid,cartid) {

    var rnd = Math.random();
    var xmlhttp;
    var url = "handler/HandlerAddProp.ashx?propId=" + pid + "&cartId=" + cartid+"&rnd=" + rnd;

    if (window.ActiveXObject) {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    } else if (window.XMLHttpRequest) {
        xmlhttp = new XMLHttpRequest();
    }

    xmlhttp.open("get", url, true);
    xmlhttp.send(null);

    xmlhttp.onreadystatechange = function () {

        if (xmlhttp.readystate == 4 && xmlhttp.status == 200) {
            var xmltext = xmlhttp.responseText;

            if (xmltext == 1) {
                alert("商品成功加入购物车");
            }

            if (xmltext == -1) {
                alert("您的购物车已满，请查看购物车！");
            }
        }
    }
}


//连载消息
function UserMessage(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 415;
    diag.Height = 250;
    diag.Title = "订阅连载消息";
    diag.URL = "../../../UserMessage.aspx?userId=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//书架
function UserBookShelf(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 573;
    diag.Height = 440;
    diag.Title = "我的书架";
    diag.URL = "../../../UserBookShelf.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//订阅小说
function UserReadingBook(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 700;
    diag.Height = 400;
    diag.Title = "订阅小说";
    diag.URL = "../../../UserReadingBook.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//购物车
function UserShoppingCart(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 560;
    diag.Height = 380;
    diag.Title = "购物车";
    diag.URL = "../../../ShopCart.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}


//好友列表
function UserFriend(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 700;
    diag.Height = 400;
    diag.Title = "我的好友";
    diag.URL = "../../../UserFriend.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//我的道具
function UserHaveProp(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 560;
    diag.Height = 400;
    diag.Title = "我的道具";
    diag.URL = "../../../UserHaveProp.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//个人资料
function UserInformation(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 700;
    diag.Height = 400;
    diag.Title = "个人资料信息";
    diag.URL = "../../../UserInformation.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//修改密码
function UserChangePWD(userId) {
    var diag = new Dialog("Diag");
    diag.Width = 300;
    diag.Height = 200;
    diag.Title = "密码修改";
    diag.URL = "../../../UserChangePWD.aspx?UID=" + userId;
    diag.ShowButtonRow = false;
    diag.show();
}

//冲值
function UserPayMoney(UserId) {
    var diag = new Dialog("Diag");
    diag.Width = 298;
    diag.Height = 220;
    diag.Title = "书币冲值";
    diag.URL = "../../../payMoney.aspx";
    diag.ShowButtonRow = false;
    diag.show();
}


//加入收藏 
/**   
 *    
 * @param {} sURL 收藏链接地址   
 * @param {} sTitle 收藏标题   
 */   
function AddFavorite(sURL, sTitle) {   
    try {   
        window.external.addFavorite(sURL, sTitle);   
    } catch (e) {   
        try {   
            window.sidebar.addPanel(sTitle, sURL, "");   
        } catch (e) {   
            alert("加入收藏失败，请使用Ctrl+D进行添加");   
        }
    }

    return false;  
}   
/**     
 *    
 * @param {} obj 当前对象，一般是使用this引用。   
 * @param {} vrl 主页URL   
 */   
function SetHome(obj, vrl) {   
    try {   
        obj.style.behavior = 'url(#default#homepage)';   
        obj.setHomePage(vrl);   
    } catch (e) {   
        if (window.netscape) {   
            try {   
                netscape.security.PrivilegeManager   
                        .enablePrivilege("UniversalXPConnect");   
            } catch (e) {   
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");   
            }   
            var prefs = Components.classes['@mozilla.org/preferences-service;1']   
                    .getService(Components.interfaces.nsIPrefBranch);   
            prefs.setCharPref('browser.startup.homepage', vrl);   
        }
    }
    return false;   
}
