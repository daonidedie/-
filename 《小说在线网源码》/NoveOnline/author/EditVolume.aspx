<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditVolume.aspx.cs" Inherits="author_EditVolume" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        书卷名称：<asp:TextBox runat="server" ID="tx1" Text=""></asp:TextBox><br />
        书卷顺序：<asp:TextBox runat="server" ID="TextBox1" Text=""></asp:TextBox>
        <asp:LinkButton Text="确认修改" runat="server" onclick="Unnamed1_Click"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
