<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrMessage.aspx.cs" Inherits="ErrMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出错了</title>

</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div>
     <div style="width:500px;padding-top:15%;" id="errdiv">
        <asp:Image ID="Image1" ImageUrl="~/Images/errmsg.jpg" runat="server" />
    </div>
    <div style="width:500px;font-size:12px;">
        <asp:HyperLink Text="返回首页" runat="server" NavigateUrl="~/Default.aspx" ForeColor="Black" Font-Underline="false"></asp:HyperLink>
    </div>
    </div>
    </center>
    </form>
</body>
</html>
