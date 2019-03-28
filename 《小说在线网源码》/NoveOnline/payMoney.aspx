<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payMoney.aspx.cs" Inherits="payMoney" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线冲值</title>

    <script type="text/javascript">
    function datavalied() {
        var keyCode = event.keyCode;
        if ((keyCode < 48 || keyCode > 57) && keyCode != 8 && keyCode != 13 && keyCode != 9) {
            event.returnValue = false;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width:270px;font-size:12px;color:Red;height:30px;line-height:30px;border:1px solid #BBB;background-color:#EBEBEB;padding-left:10px;">注：冲值应该由银行提供接口，该页作为测试页。</div>
    <div style="text-align:left; width:280px;font-size:12px;height:150px;border:1px solid #BBB;background-color:#F4F4F4;border-top:0px;padding-top:20px;">
        <div style="text-indent:45px;height:18px;">用户：<asp:Label runat="server" ID="uname"></asp:Label></div>
        <div style="text-indent:45px;margin-top:10px;">金额：<asp:TextBox ID="txMoney" runat="server" onkeydown="datavalied();"></asp:TextBox></div>
        <div style="text-indent:120px;margin-top:30px;"><asp:ImageButton ID="ImageButton1" 
                runat="server" ImageUrl="~/Images/Submit.gif" onclick="ImageButton1_Click" /></div>
    </div>
    </form>
</body>
</html>
