<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="Login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Js/Dialog.js" type="text/javascript"></script>

    <script type="text/javascript">

        function changeFocus(obj) {

            
            var txUserName = document.getElementById("loginUserName");
            var txPassword = document.getElementById("loginPassword");
            var btLogin = document.getElementById("enterLogin");

            if (event.keyCode == 9) {
                if (obj.id == "loginUserName") {
                    txPassword.focus();
                    return;
                }
                else if (obj.id == "loginPassword") {
                    btLogin.focus();
                    return;
                }
                else if (obj.id == "enterLogin") {
                    txUserName.focus();
                    return;
                }
            }
        }
        

    </script>
</head>
<body style="font-size:12px;">
    <form id="form1" runat="server" defaultfocus="loginUserName">
    <div>
        <div style="margin-left:35px;margin-top:35px;">
           帐号：<asp:TextBox runat="server" ID="loginUserName" onkeydown="changeFocus(this);"  style="width:143px;"></asp:TextBox>
        </div>
        <div style="margin-top:5px;margin-left:35px;">
           密码：<asp:TextBox runat="server" ID="loginPassword" TextMode="Password" style="width:143px;" onkeydown="changeFocus(this);"></asp:TextBox>
        </div>
        <div style="margin-top:10px;margin-left:170px;">
          <asp:ImageButton runat="server"  ID="enterLogin" ImageUrl="~/Images/login.gif" 
                onclick="enterLogin_Click" onkeydown="changeFocus(this);" />
        </div>
        <div style="margin-top:10px;margin-left:35px;">
            <asp:Label Text="提示：帐号密码错误！" id="errlb" runat="server" Visible="false"></asp:Label>
        </div>
    </div>

    </form>
</body>
</html>