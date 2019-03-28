<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserPanel.ascx.cs" Inherits="UserControls_UserPanel" %>

<div class="title" id="Div_login">
    <asp:HyperLink runat="server" ID="loginin" NavigateUrl="../UserLogin" Text="登陆" onclick="return login();" ></asp:HyperLink> |
    <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/UserRegister.aspx" Text="注册"></asp:HyperLink> |
    <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="../HA" Text="收藏本站" onclick="return AddFavorite(window.location,document.title)" ></asp:HyperLink> |
    <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="../Index" Text="设为首页" onclick="return SetHome(this,window.location)" ></asp:HyperLink> |
    <asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="/AdminLogin" Text="管理入口"  onclick="return Adminlogin();" style="margin-right:10px;"></asp:HyperLink>
</div>


