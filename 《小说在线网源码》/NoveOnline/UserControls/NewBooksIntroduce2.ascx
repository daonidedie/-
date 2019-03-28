<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewBooksIntroduce2.ascx.cs" Inherits="UserControls_NewBooksIntroduce2" %>


<script type="text/javascript">

    function changeColor(div) {
        div.style.backgroundImage = "url(Images/filter1.jpg)";
        div.style.border = "1px solid #BBB";
        div.style.cursor = 'hand';
    }


    function changeColor1(div) {
        div.style.backgroundImage = "url(Images/filter2.jpg)";
        div.style.border = "1px solid #DDD";
        div.style.cursor = 'defalut';
    }

</script>

<div onmouseover="changeColor(this);" onmouseout="changeColor1(this);"></div>
<asp:Repeater ID="repeater" runat="server" 
    onitemdatabound="repeater_ItemDataBound">
    <ItemTemplate>
        <div id="Div1" onmouseover="changeColor(this);" onmouseout="changeColor1(this);" style="background-image:url(Images/filter2.jpg);">
            <div>
                <span style="float:left;margin-left:10px;margin-top:5px;font-size:12px;font-weight:bold;"><%# Eval("TypeName") %>小说</span>
                <span style="float:right;margin-right:10px;margin-top:6px;"><asp:HyperLink ID="BookAll" runat="server" NavigateUrl='<%# string.Format("~/BooksAllInfo.aspx?bookTypeId={0}",Eval("TypeId")) %>'>更多>></asp:HyperLink></span>
            </div>
            <asp:TextBox Visible="false" ID="typeId" runat="server" Text='<%# Eval("TypeId") %>'></asp:TextBox>
            <div style="clear:both;"></div>
            <hr style="width:90%;" />
            <asp:Repeater ID="booksInfo" runat="server">
                <ItemTemplate>
                    <div id="imags"><asp:Image ID="imgs" runat="server" Width="60px" Height="80px" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("Images"))  %>' /></div>
                    <span style="float:left;margin-left:15px;margin-right:10px;">
                        <asp:HyperLink ID="hlbookName" runat="server" Text='<%# Eval("BookName") %>' ToolTip='<%# string.Format("作者：{0}",Eval("UserName")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'>'></asp:HyperLink>
                    </span>
                    <div style="margin-left:15px;float:left;margin-right:20px;margin-top:10px;width:120px;text-align:left;"><%# string.Format("{0}",Eval("BookIntroduction").ToString().Length > 43 ? Eval("BookIntroduction").ToString().Substring(0, 43) + "……" : Eval("BookIntroduction").ToString()) %></div>    
                </ItemTemplate>
            </asp:Repeater><div style="clear:both;"></div>
            <asp:Repeater ID="booksInfo2" runat="server">
                <HeaderTemplate><div style="float:left;margin-left:10px;text-align:left;margin-top:8px;"></HeaderTemplate>
                <FooterTemplate></div></FooterTemplate>
                <ItemTemplate>
                    <div style="margin-bottom:5px;">
                        [<asp:label ID="hlBookName" runat="server" Text='<%# Eval("TypeName") %>'></asp:label>]
                        <asp:HyperLink ID="lbBookIntroduction" runat="server" Text='<%# Eval("BookName") %>' ToolTip='<%# string.Format("作者：{0}",Eval("UserName")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'>'></asp:HyperLink><br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </ItemTemplate>
</asp:Repeater>
