<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuthorVis.ascx.cs" Inherits="UserControls_AuthorVis" %>

<asp:Repeater runat="server" ID="rep1">
    <ItemTemplate>
        <div><table>        
            <tr><td rowspan="6" style="border:1px solid black;"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/test/2036355.jpg" /></td>
            </tr>
            <tr>
                <td style="border:1px solid black;">专访作者：<%# Eval("UserName") %></td>
            </tr>
            <tr>
                <td style="border:1px solid black;">出生日期：<%# Eval("UserBrithday")%></td>
            </tr>
            <tr>
                  <td style="border:1px solid black;">现居：<%# Eval("userAddress")%></td>
            </tr>
            <tr>
                <td style="border:1px solid black;">主题：<%# Eval("VisitTitle")%></td>
            </tr>
            <tr>
                <td style="border:1px solid black;">专访日期：<%# Eval("VisitDate")%></td>
            </tr>
            <tr>
                <td colspan="2" style="border:1px solid black;">专访过程：<%# Eval("Contents")%></td>
            </tr>
        </table></div>
    </ItemTemplate>
</asp:Repeater>