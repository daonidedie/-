<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BookTicket.ascx.cs" Inherits="UserControls_BookTicket" %>


<div class="title">
<div style="background-image:url(Images/title1.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;float:left;width:150px;" >书票/鲜花排行</div>
<div style="float:right; margin-right:10px;">
    <asp:LinkButton runat="server" Text="书票" ID="ticket" onclick="weeks_Click" ForeColor="Red" ></asp:LinkButton> 
    <asp:LinkButton runat="server" Text="鲜花" ID="flower" onclick="flower_Click"></asp:LinkButton> 
</div>
</div>
<div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
<asp:GridView runat="server" ID="girdBookTicket" AutoGenerateColumns="false" 
    ShowHeader="false" style="margin:6px;margin-left:12px;margin-top:6px;" GridLines="None" 
    onrowdatabound="girdBookTicket_RowDataBound">
    <Columns>
        <asp:TemplateField ItemStyle-Width="157px" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="19px"  ItemStyle-CssClass="itemsleftcss">
            <ItemTemplate>
                <asp:HyperLink runat="server" Text='<%# string.Format("[{0}]{1}",Eval("TypeName"),Eval("BookName")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>' ></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="itemsleftcss">
            <ItemTemplate>
                <asp:Label Text='<%# string.Format("数量：{0}",Eval("ticketNumber")) %>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>