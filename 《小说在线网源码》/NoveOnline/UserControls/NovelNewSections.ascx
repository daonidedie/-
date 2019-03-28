<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NovelNewSections.ascx.cs" Inherits="UserControls_NovelNewSections" %>
<div style="width:690px;padding-top:15px;padding-left:20px;">
    <asp:GridView ID="gv1" runat="server"
        AutoGenerateColumns="false" Width="650px"  
        ShowHeader="false"  GridLines="None" onrowdatabound="gv1_RowDataBound">
        <Columns>
        <asp:TemplateField ItemStyle-CssClass="itemsleftcss">
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="hyp1" Text='<%# string.Format("[{0}] {1}/{2}/{3}",Eval("TypeName"),Eval("BookName"),Eval("ValumeName"),Eval("SectionTitle")) %>' NavigateUrl='<%# string.Format("~/BookSectionsInfo.aspx?BookId={0}&&SectiuonId={1}&&inspect=0",Eval("BookId"),Eval("SectiuonId")) %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>    
        
           <asp:BoundField DataField="StateName" HeaderText="状态" ItemStyle-CssClass="itemsCss" HeaderStyle-Width="50px" />
           <asp:BoundField DataField="ShortAddTime" HeaderText="更新时间" ItemStyle-CssClass="itemsCss"/>    
        </Columns> 
    </asp:GridView>
     <div style="text-align:right;margin-right:40px;margin-top:13px;">
        <asp:HyperLink runat="server" ID="newSecionLink" NavigateUrl="~/NewSections.aspx" Text="更多>>"></asp:HyperLink>
     </div>
</div>