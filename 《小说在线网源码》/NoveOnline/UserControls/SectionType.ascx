<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionType.ascx.cs" Inherits="UserControls_SectionType" %>
<asp:Repeater ID="rep1" runat="server">
    <HeaderTemplate>
    <ul style="margin-top:20px;margin-left:5px;">
        <li style="list-style-type:none;height:30px;line-height:30px;"> <asp:HyperLink ID="HyperLink1" runat="server" Text="全部小说最新章节" NavigateUrl="~/NewSections.aspx"></asp:HyperLink></li>
    </HeaderTemplate>
    <ItemTemplate>
        <li style="list-style-type:none;height:30px;line-height:30px;"> <asp:HyperLink runat="server" Text='<%# string.Format("{0}小说最新章节",Eval("TypeName"))%>' NavigateUrl='<%# string.Format("~/NewSections.aspx?booktypeId={0}",Eval("TypeId")) %>'></asp:HyperLink></li>
    </ItemTemplate>
    <FooterTemplate></ul></FooterTemplate>
</asp:Repeater>