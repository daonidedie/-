<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PropIndex.aspx.cs" Inherits="PropIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商城</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />
    <link href="Css/Prop.css" rel="stylesheet" type="text/css" />
    <link href="Css/callboard.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        window.onload = function () {

            var isok = this.location.href;
            if (isok.indexOf('isok=1') != -1) {


                var index = isok.indexOf("userid=");
                var userID = isok.substring(index, isok.length);
                userID = userID.replace("userid=", "");
                var intUserId = parseInt(userID);

                var newdiag = new Dialog("Diag");
                newdiag.Width = 560;
                newdiag.Height = 400;
                newdiag.Title = "我的道具";
                newdiag.URL = "../../../UserHaveProp.aspx?UID=" + userID;
                newdiag.ShowButtonRow = false;
                newdiag.show();

            }
        }
        
    </script>


</head>
<body>
    <center>
        <div id="index">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != "" && Session["NewUser"] != null)
                        {
                    %>
                    <uc:LoginUserPanel ID="loginInUserPanel" runat="server" />
                    <% 
                        }
                        else
                        {
                    %>
                    <uc:userpanel ID="userpanel" runat="server" />
                    <%
                        }                     
                    %>

                </div>
                <uc:header ID="header" runat="server" />
                <div id="boostype">
                    <uc:booktype ID="BookType" runat="server" />
                </div>
                <div id="serch">
                    <uc:Serch runat="server" ID="serchCt" />
                </div>
                <%--  商城部分 --%>
                <div id="Propleft">
                    <div id="PropType">
                        <div class="title">公告</div>
                        <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')">
                        </div>
                        <uc:CallBoard runat="server" ID="callBoard"/>
                    </div>

                    <div style="height:5px;"></div>

                    <div style="background-image: url(Images/hot.png);position:absolute;z-index:20;background-repeat:no-repeat;width:59px;height:50px;"></div>
                    <div id="propTOP" style="z-index:10;">
                            <div class="title">热卖商品</div>
                            <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
                            <uc:HotProp runat="server"  ID="hotProp"/>
                     </div>
                        

                </div>

                <div id="Propright">
                    <div id="PropList">
                        <div class="title">
                            商品列表</div>
                        <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')">
                        </div>
                        <uc:PropList runat="server" ID="proplist1" />
                    </div>
                    
                </div>
            
            <div>
                <uc:foot ID="foot" runat="server" />
            </div>
            </div>
            
            </form>
        </div>

    </center>
</body>
</html>
