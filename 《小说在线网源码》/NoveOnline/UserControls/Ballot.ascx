<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ballot.ascx.cs" Inherits="UserControls_Ballot" %>

<div id="prophots" style="margin:22px;margin-top:8px;">
<asp:GridView ID="gridHotPropss" runat="server" AutoGenerateColumns="false" ShowHeader="false" BorderStyle="None">    
    <Columns>
        <asp:TemplateField ItemStyle-CssClass="propfontbottom" >
            <ItemTemplate>
                <asp:Label runat="server" ID="hypPropName2" Text='<%# string.Format("{0}",Eval("PropName")) %>' Width="130px" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="propfontleft" >
            <ItemTemplate>
                  <asp:Label runat="server" ID="hypPropName2" Text='<%# string.Format("售价：{0:f2}",Eval("PropPrice")) %>' Width="70px" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>