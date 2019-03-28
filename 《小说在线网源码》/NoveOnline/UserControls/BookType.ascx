<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BookType.ascx.cs" Inherits="UserControls_BookType" %>

<asp:Repeater ID="RebookType" runat="server">
<HeaderTemplate><ul style="margin:0px;"></HeaderTemplate>
<ItemTemplate>
    <li style="width:72px;float:left;list-style-type:none;">
         <span style="margin-right:9px;">|</span><a href='<%# string.Format("Books.aspx?rnd=1&state=0&booktype={0}&charnum=0",Eval("TypeId")) %>' style="color:black;"><%# Eval("TypeName")%>小说</a>
    </li>
    
</ItemTemplate>
<FooterTemplate></ul>|</FooterTemplate>
</asp:Repeater>