<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserChangePWD.aspx.cs" Inherits="UserChangePWD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
    </style>
    <script src="Js/Dialog.js" type="text/javascript"></script>

    <script type="text/javascript">

        var oldPWD;
        function datavalid() {

            var usepwd = document.getElementById("txUsePassword");
            var acc = document.getElementById("lbacc");

            var accnumber = acc.innerText;
            var userpasswrod = usepwd.value.toString();

            var xmlhttp;
            var rnd  = Math.random();
            var url = "handler/HandlerUserPassword.ashx?acc=" + accnumber + "&PWD=" + userpasswrod + "&rnd=" + rnd;
            if(window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest;
            }

            xmlhttp.open("get", url, true);

            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readystate == 4 && xmlhttp.status == 200) {
                    xmldom = xmlhttp.responseText;
                    var flag = parseInt(xmldom);
                    if (flag == 0) {    
                        oldPWD = 1;
                    }
                    if (flag == 1) {
                        oldPWD = 0;
                        var txnewpwd1 = document.getElementById("newpwd1");
                        txnewpwd1.disabled = false;
                        txnewpwd1.focus();
                        var txnewpwd2 = document.getElementById("newpwd2");
                        txnewpwd2.disabled = false;

                    }
                }
            }

            xmlhttp.send(null);
        }

        function pwdvalid()
        {
            var txnewpwd1 = document.getElementById("newpwd1");
            var txnewpwd2 = document.getElementById("newpwd2");
            var p1 = txnewpwd1.value;
            var p2 = txnewpwd2.value;

            if (oldPWD == 1) {
                alert('原始密码错误！');
                return false;
            }

            if (p1 != p2) {
                alert('两次密码不一致');
                return false;
            }

            if (p1 == p2 && p1 != "" && p2 != "") {
                if (confirm("确认修改密码吗？")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                alert("新密码不能为空！");
                return false;
            }


        }

        function reset() {
            var txnewpwd1 = document.getElementById("newpwd1");
            var txnewpwd2 = document.getElementById("newpwd2");
            var usepwd = document.getElementById("txUsePassword");

            usepwd.value = '';
            txnewpwd1.value = '';
            txnewpwd1.disable = true;
            txnewpwd2.value = '';
            txnewpwd2.disable = true;

            return false;
        }
            
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <div style="width:250px;display:table;margin-left:30px;margin-top:20px;">
       <div style="width:230px;clear:both;height:30px;">
            <div style="float: left; text-align: right; width: 70px;height:30px;line-height:30px;">
                您的帐号：
            </div>
            <div style="float: left; text-align: left; width: 150px;height:30px;line-height:30px;">
                <asp:Label ID="lbusername" runat="server"></asp:Label>(<asp:Label ID="lbacc" runat="server"></asp:Label>)
            </div>
      </div>
 
        <div style="width:230px;clear:both;height:30px;">
            <div style="float: left; text-align: right; width: 70px;height:30px;line-height:30px;">
                原始密码：
            </div>
            <div style="float: left; text-align: left; width: 150px;margin-top:3px;">
                <asp:TextBox runat="server" ID="txUsePassword" TextMode="Password" onblur="datavalid();" ></asp:TextBox>
            </div>
        </div>

        <div style="width:230px;clear:both;height:30px;">
            <div style="float: left; text-align: right; width: 70px;height:30px;line-height:30px;">
                新的密码：
            </div>
            <div style="float: left; text-align: left; width: 150px;height:30px;line-height:30px;margin-top:3px;">
                <asp:TextBox runat="server" ID="newpwd1" TextMode="Password" Enabled="false" MaxLength="16" ></asp:TextBox>
            </div>
        </div>


        <div style="width:230px;clear:both;height:30px;">
            <div style="float: left; text-align: right; width: 70px;height:30px;line-height:30px;">
                确认密码：
            </div>
            <div style="float: left; text-align: left; width: 150px;margin-top:3px;">
                 <asp:TextBox runat="server" ID="newpwd2" TextMode="Password" Enabled="false" MaxLength="16" ></asp:TextBox>
            </div>
        </div>
   </div>
   <div style="text-align:center;width:290px;margin-top:5px;">
        <asp:ImageButton ImageUrl="~/Images/Submit.gif" runat="server" 
            OnClientClick="return pwdvalid();" onclick="Unnamed1_Click"/>
        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Reset.gif" runat="server" OnClientClick="return reset();"/>
   </div>
    </form>

    <script type="text/javascript">
        var usepwd = document.getElementById("txUsePassword");
        usepwd.focus();
    </script>
</body>
</html>
