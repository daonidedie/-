<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionRepDisplay.ascx.cs" Inherits="UserControls_SectionRepDisplay" %>
<asp:Repeater ID="rep1" runat="server">
    <ItemTemplate>
    <div style="float:left;width:455px;border:1px solid #BBB; text-align:left;margin-left:10px;margin-top:10px;"><table>
            <tr style="margin:auto;">
            <td rowspan="5"><img src="Images/test/1264634.jpg"/ alt="书本封面"></td>
            <td>书名：<%# Eval("BookName")%></td>
            </tr>
            <tr><td colspan="2">类型：<%# Eval("TypeName") %></td></tr>
            <tr><td colspan="2">作者：<%# Eval("UserName")%></td></tr>
            <tr><td colspan="2">更新日期：<%# Eval("ShortAddTime")%></td></tr>
            <tr><td colspan="2">章节简介：<%# Eval("Contents")%></td></tr>
            </table></div>
    </ItemTemplate>
</asp:Repeater>
