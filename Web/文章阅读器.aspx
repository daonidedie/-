<%@ Page Language="C#" AutoEventWireup="true" CodeFile="文章阅读器.aspx.cs" Inherits="文章阅读器" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    body{ font-size:9pt;  }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <contenttemplate><div>
                请输入文章名称：<asp:TextBox runat="server" ID="BookName">百炼成仙</asp:TextBox>
                <asp:Button runat="server" Text="查看" OnClick="Unnamed1_Click" />
            </div>
        </contenttemplate>
        
    </div>   
    <asp:UpdatePanel ID="statePanel" runat="server">
        <ContentTemplate>
         
           <asp:Timer ID="timer" runat="server" Interval="10000" OnTick="timer_Tick">
            </asp:Timer>
           
            <asp:DataList ID="DataList1" runat="server" CellPadding="4" CellSpacing="3" 
                ForeColor="#333333" RepeatColumns="4" ShowFooter="False" 
        ShowHeader="False" BorderColor="Fuchsia" BorderWidth="1px" GridLines="Both" 
        RepeatDirection="Horizontal">

                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#E3EAEB" />

                <ItemTemplate>
                 <a href="<%#(Eval("ResultUrl") == null ? "#" : (Eval("ResultUrl") as Uri).ToString())%>" target="_blank" > <%#(Eval("OriginUrl") == null ? "#" : (Eval("OriginUrl") as Skybot.Collections.Analyse.ListPageContentUrl).Title)%></a>
                </ItemTemplate>

                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />

            </asp:DataList>
            <asp:Label ID="stateText" runat="server" Text="当前状态"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
