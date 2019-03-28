<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="UserInfoLogin"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���û�ע��</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />

    <link href="Css/UserInfoLogin.css" rel="stylesheet" type="text/css" />


    

    <script type="text/javascript">
        window.onload = function () {
            var info = document.getElementById("info");
            var a = document.getElementById("a");
            var b = document.getElementById("b");
            var y = document.getElementById("info").clientHeight;
            a.style.height = y+2;
            b.style.height = y+2;
        }

        function hiddenON() {
            var cbOK = document.getElementById("cbOK");
            var datum = document.getElementById("datum");
            var a = document.getElementById("a");
            var b = document.getElementById("b");
            if (cbOK.checked == true) {
                datum.style.visibility = 'visible';
                datum.style.height = "200px";
                var y = document.getElementById("info").clientHeight;
                a.style.height = y + 2;
                b.style.height = y + 2;
            }
            else {
                datum.style.visibility = 'hidden';
                datum.style.height = "0px";
                var y = document.getElementById("info").clientHeight;
                a.style.height = y + 2;
                b.style.height = y + 2;
            }
        }

        function acceptOK() {
            var cbOK2 = document.getElementById("cbOK2");
            var btOK = document.getElementById("btOK");
            var cbOK = document.getElementById("cbOK");

            var validUsername = document.getElementById("validUsername").innerHTML;
            var lbUserName3 = document.getElementById("lbUserName3").innerHTML;
            var lbPassword = document.getElementById("lbPassword").innerHTML;
            var lbPassword2 = document.getElementById("lbPassword2").innerHTML;

            var tbUserName = document.getElementById("tbUserName").value;
            var tbUserName2 = document.getElementById("tbUserName2").value;
            var tbUserPassword = document.getElementById("tbUserPassword").value;
            var tbUserPassword2 = document.getElementById("tbUserPassword2").value;

            if (cbOK.checked == true) {
                var lbCardNumber = document.getElementById("lbCardNumber").innerHTML;
                if (cbOK2.checked == true && validUsername == '' && lbUserName3 == '' && lbPassword == '' && lbPassword2 == '' && lbCardNumber == '' && tbUserName != '' && tbUserName != '' && tbUserPassword != '' && tbUserPassword2 != '') {
                    btOK.disabled = false;
                } else {
                    cbOK2.checked = false;
                    alert('�û���Ϣ����!����ϸȷ��');
                    btOK.disabled = true;
                }
            } else {
                if (cbOK2.checked == true && validUsername == '' && lbUserName3 == '' && lbPassword == '' && lbPassword2 == '' && tbUserName != '' && tbUserName != '' && tbUserPassword != '' && tbUserPassword2 != '') {
                    btOK.disabled = false;
                } else {
                    cbOK2.checked = false;
                    alert('�û���Ϣ����!����ϸȷ��');
                    btOK.disabled = true;
                }
            }
        }

        function datavalid() {
            var keyCode = event.keyCode;
            if (keyCode > 47 && keyCode < 58) {
                event.returnValue = true;
            } else if (keyCode > 64 && keyCode < 91) {
                event.returnValue = true;
            } else if (keyCode == 8 || keyCode == 9) {
                event.returnValue = true;
            }
            else {
                event.returnValue = false;
            }
        }

        function hasUser(tx) {
            if (tx.value.length < 6) {
                document.getElementById("validUsername").innerHTML = '&nbsp;&nbsp;�˺ų��Ȳ���';
                return;
            }
            var username = tx.value;
            var xmlhttp;
            var rnd = Math.random();
            var url = "handler/HandlerHasUser.ashx?username=" + username + "&rnd=" + rnd;
            if (window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            xmlhttp.open("get", url, true);
            xmlhttp.send(null);
            xmlhttp.onreadystatechange = function () {

                if (xmlhttp.readystate == 4 && xmlhttp.status == 200) {

                    xmlText = xmlhttp.responseText;
                    var count = parseInt(xmlText);
                    if (count == 1) {
                        document.getElementById("validUsername").innerHTML = '&nbsp;&nbsp;�˺��Ѵ���';
                    }
                    else if (tx.value.length > 5 && count == 0) {
                        document.getElementById("validUsername").innerHTML = '';
                    }
                }
            }
        }

        function hasUserName(userName) {
            var username = userName.value;
            var xmlhttp;
            var rnd = Math.random();
            var url = "handler/HandlerHasUserName.ashx?userName=" + encodeURI(username) + "&rnd=" + rnd;
            if (window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            xmlhttp.open("get", url, true);
            xmlhttp.send(null);
            xmlhttp.onreadystatechange = function () {

                if (xmlhttp.readystate == 4 && xmlhttp.status == 200) {

                    xmlText = xmlhttp.responseText;
                    var count = parseInt(xmlText);
                    if (count == 1) {
                        document.getElementById("lbUserName3").innerHTML = '&nbsp;&nbsp;�ǳ��Ѵ���';
                    }
                    else {
                        document.getElementById("lbUserName3").innerHTML = '';
                    }
                }
            }
        }

        function hasPassword(lbpassword) {
            if (lbpassword.value.length < 6)
                document.getElementById("lbPassword").innerHTML = '&nbsp;&nbsp;���볤�Ȳ���';
            else
                document.getElementById("lbPassword").innerHTML = '';
        }

        function hasPassword2(lbpassword) {
            var password = document.getElementById("tbUserPassword");
            if (lbpassword.value != password.value)
                document.getElementById("lbPassword2").innerHTML = '&nbsp;&nbsp;�����������벻һ��';
            else
                document.getElementById("lbPassword2").innerHTML = '';
        }
        
        function validateIdentityCardNumber(cardNumber) {
            if (cardNumber.value.length < 18) {
                document.getElementById("lbCardNumber").innerHTML = '&nbsp;&nbsp;���֤�������';
                return;
            }

            var a = cardNumber.value.split('');
            var b=0;
            for (var i = 0; i < a.length - 1; i++) {
                if (a[i].charCodeAt(0) < 48 || a[i].charCodeAt(0) > 57) {
                    document.getElementById("lbCardNumber").innerHTML = '&nbsp;&nbsp;���֤�������';
                    return;
                }
                b = b + 1;
            }
            if (cardNumber.value.length == 18 && b == 17)
                document.getElementById("lbCardNumber").innerHTML = '';
        }
    </script>


    

    

</head>
<body>

    <script type="text/javascript">
        var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    </script>
    <script src="Js/calendar.js" type="text/javascript"></script>

<center>
    <form id="form2" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="main">
            <div>
                <%
                    if (User.Identity.Name != "" && Session["NewUser"] != null)
                    {
                    %>
                        <uc:LoginUserPanel ID="loginInUserPanel" runat="server"/>
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
                <uc:booktype ID="BookType1" runat="server" />
            </div>
            <div id="serch">
                <uc:Serch runat="server" id="serchCt"/>
            </div>
        </div>

        

        <div style="width:948px;border:1px solid #D6D6D6;margin-bottom:5px;">
            <div class="title" style="border-bottom:1px solid #CCCCCC;">
                ���û�ע��
            </div>
            <div style="width:948px;text-align:center;height:45px;background-color:#FAFAFA;padding-top:25px;font-size:12px;color:Red;">
                ��ӭע�᱾վ��Ա��ף��������죡
            </div>
            <div id="a" style="width:100px;float:left;background-color:#FAFAFA;"></div>
            <div id="info" style="background-color:White;width:696px;float:left;padding:30px 20px 30px 30px;border:1px solid #D6D6D6;text-align:left;">
                <table id="tbUser">
                    <tr>
                        <td style="width:65px;"><asp:Label ID="lbUserName" runat="server" Text="��½�˺ţ�"></asp:Label></td>
                        <td><asp:TextBox ID="tbUserName" runat="server" MaxLength="16" onkeydown="return datavalid();"  onblur="hasUser(this);" ></asp:TextBox><asp:Label runat="server" ID="validUsername" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td><td>���ַ���������ĸ��ͷ����ֻ����a-z��26��СдӢ����ĸ��0-9��10��������ɣ����ܰ����ո��»��ߵ������ַ�������Ϊ6-16λ�ַ�֮�䡣</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lbUserName2" runat="server" Text="�û��ǳƣ�"></asp:Label></td>
                        <td><asp:TextBox ID="tbUserName2" runat="server" MaxLength="12" onblur="hasUserName(this);"></asp:TextBox><asp:Label runat="server" ID="lbUserName3" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td><td>������12λ��������2λ�����ڵ��û���������ʹ�����ġ�Ӣ�ġ����ֺ��»��ߡ�</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lbUserPassword" runat="server" Text="��½���룺"></asp:Label></td>
                        <td><asp:TextBox ID="tbUserPassword" runat="server" MaxLength="16" 
                                TextMode="Password" onkeydown="return datavalid();" onblur="hasPassword(this)"></asp:TextBox><asp:Label runat="server" ID="lbPassword" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td><td>6��16λ�ַ�����a-z�Ĳ��޴�СдӢ����ĸ��0-9��������ɣ����ܰ����ո�������ַ������벻�����˺�����ͬ��</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lbUserPassword2" runat="server" Text="ȷ�����룺"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="tbUserPassword2" runat="server"  MaxLength="16" Visible="true" TextMode="Password" onkeydown="return datavalid();" onblur="hasPassword2(this)"></asp:TextBox><asp:Label runat="server" ID="lbPassword2" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="background-color:White;width:696px;text-align:left;margin-top:10px;"><asp:CheckBox ID="cbOK" onclick="hiddenON()" runat="server" Text="���ø�������" /><br /></div>

                <div id="datum" style="height:0px;width:948px;overflow:hidden;padding-left:0px;padding-top:10px;visibility:hidden;">
                    <asp:UpdatePanel ID="u1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <table id="userInfo">
                        <tr>
                            <td><asp:Label ID="lbName" runat="server" Text="��ʵ������"></asp:Label></td>
                            <td><asp:TextBox ID="tbName" width="302px" runat="server" MaxLength="5" style="margin-left:5px;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbSex" runat="server" Text="�Ա�"></asp:Label></td>
                            <td id="tdb" style="padding:2px;"><asp:RadioButton ID="rbSexM" runat="server" GroupName="sex" Text="��" 
                                    Checked="True" /><asp:RadioButton ID="rbSexF" runat="server" GroupName="sex" Text="Ů" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbBrithday" runat="server" Text="�������ڣ�"></asp:Label></td>
                            <td style="padding:5px;"><asp:TextBox id="tbBrithday"  runat="server" Width="302px" onclick="showcalendar(event, this);" 
                             onfocus="showcalendar(event, this);if(this.value=='0000-00-00')this.value=''"/></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbIdentityCardNumber" runat="server" Text="���֤�ţ�"></asp:Label></td>
                            <td style="padding:5px;"><asp:TextBox ID="tbIdentityCardNumber" width="302px" runat="server" 
                                    MaxLength="18" onblur="validateIdentityCardNumber(this)"></asp:TextBox><asp:Label ID="lbCardNumber" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbArea" runat="server" Text="���ڵ�����"></asp:Label></td>
                            <td style="padding-left:5px;"><asp:Label ID="lbProvince" runat="server" Text="ʡ��"></asp:Label>
                                <asp:DropDownList ID="ddlProvinceId" runat="server" Width="115px" 
                                    DataTextField="province" DataValueField="id" AutoPostBack="True" 
                                    onselectedindexchanged="ddlProvinceId_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lbArea2" runat="server" Text="��/����"></asp:Label><asp:DropDownList ID="ddlArea" runat="server" Width="115px" DataTextField="city" DataValueField="id"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbAddress" runat="server" Text="��ϵ��ַ��"></asp:Label></td>
                            <td style="padding-left:5px;"><asp:TextBox ID="tbAddress" width="302px" runat="server" MaxLength="20" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbUsetImage" runat="server" Text="�ϴ�ͷ��"></asp:Label></td>
                            <td style="padding-left:5px;">
                                <asp:FileUpload ID="fuUsetImage" runat="server" style="width:308px;"/></td>
                        </tr>
                    </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>

                <div style="text-align:center;">
                    <asp:CheckBox ID="cbOK2" runat="server" onclick="acceptOK()" /><asp:HyperLink ID="hlOK" runat="server" Text="�����Ķ������ܡ���վ�û�Э�顷" NavigateUrl="~/Idal.aspx" Target="_blank"></asp:HyperLink><br />
                    <div style="margin-top:10px;">
                        <asp:Button ID="btOK" runat="server" Text="ȷ��ע��" Width="70px"  
                         Enabled="False" onclick="btOK_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btAfresh" runat="server" Text="������д"  Width="70px" />
                    </div>
                    
                </div>
            </div>
            <div id="b" style="width:100px;float:left;background-color:#FAFAFA;"></div>
            <div style="clear:both;width:948px;height:70px;background-color:#FAFAFA;"></div>
        </div>

        <uc:foot runat="server" id="regfoot"/>
        </div>
    </form>
    </center>
</body>
</html>