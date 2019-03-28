<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClickCount.ascx.cs" Inherits="UserControls_ClickCount" %>


<div class="title">
<div style="float:left;width:100px; background-image:url(Images/title.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;" >点击排行</div>
<div style="float:left;padding-left:10px;">
</div>
<div style="float:right; margin-right:10px;">
    <asp:LinkButton runat="server" Text="总" ID="all" onclick="all_Click" ForeColor="Red"></asp:LinkButton> 
    <asp:LinkButton runat="server" Text="月" ID="month" onclick="month_Click"></asp:LinkButton> 
    <asp:LinkButton runat="server" Text="周" ID="weeks" onclick="weeks_Click"></asp:LinkButton> 
</div>
</div>
<div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
<div id="MainBody" style="text-align:left;margin-left:5px;">

    <asp:Repeater ID="repeaterBooksCountQuantitys" runat="server">
        <HeaderTemplate>
            <table style="width:210px;margin-top:10px;margin-left:15px;" cellspacing="0">
        </HeaderTemplate>
        <ItemTemplate>
            <tr onmouseover="this.style.backgroundColor='#D6D6D6';this.style.cursor='hand'" onmouseout="this.style.backgroundColor=''">
                <td style="height:21px;border-bottom:1px dotted #CCCCCC;"><asp:HyperLink ID="hyperLink" runat="server" Text='<%# string.Format("[{0}]{1}",Eval("TypeName"),Eval("BookName")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'>'></asp:HyperLink></td>
                <td style="text-align:left;width:65px;border-bottom:1px dotted #CCCCCC;"><asp:Label ID="label" runat="server" Text='<%# string.Format("点击：{0}",Eval("Weeks")) %>'></asp:Label></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div style="text-align:right;padding-right:20px;margin-top:13px;">
        <asp:HyperLink runat="server" ID="hypClickTop" Text="更多>>" NavigateUrl="~/BookRanking.aspx"></asp:HyperLink>
    </div>
</div>
