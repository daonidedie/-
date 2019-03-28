<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSection.aspx.cs" Inherits="author_EditSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
        书卷名称：<asp:TextBox runat="server" ID="tx1" Text=""></asp:TextBox><br />
        书卷顺序：<asp:TextBox runat="server" ID="TextBox1" Text=""></asp:TextBox>
        <asp:LinkButton ID="LinkButton1" Text="确认修改" runat="server"></asp:LinkButton>
    </div>
    </div>
    </form>
</body>
</html>
