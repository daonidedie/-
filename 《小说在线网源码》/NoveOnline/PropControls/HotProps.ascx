<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HotProps.ascx.cs" Inherits="PropControls_HotProps" %>

<asp:GridView ID="gridHotProps" runat="server" AutoGenerateColumns="false" 
    ShowHeader="false" BorderStyle="None" 
    onrowdatabound="gridHotProps_RowDataBound">    
    <Columns>
        <asp:TemplateField ItemStyle-CssClass="fontbottom" >
            <ItemTemplate>
                <asp:Label runat="server" ID="hypPropName" style="text-align:left;" Text='<%# string.Format("{0}(售价{1:f2}元)",Eval("PropName"),Eval("PropPrice")) %>' Width="150px" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField ItemStyle-CssClass="fontleft" >
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="hypls1" Text='点击购买'  NavigateUrl='<%# Eval("PropId") %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>