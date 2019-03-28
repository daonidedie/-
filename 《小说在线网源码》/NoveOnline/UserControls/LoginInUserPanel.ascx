<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginInUserPanel.ascx.cs" Inherits="UserControls_LoginInUserPanel" %>


<div id="Div_userPanel" >
    <asp:Repeater runat="server" ID="g1">
        <HeaderTemplate><table></HeaderTemplate>
        <ItemTemplate>
            <tr>
            <td>欢迎回来：<asp:Label runat="server" ID="lbUserName" Text='<%# string.Format("{0}({1})", Eval("UserName"),Eval("UserTypeName")) %>'></asp:Label></td>
            <td>您的IP：<asp:Label runat="server" ID="Label2" Text='<%#Eval("IpAddress")%>'></asp:Label></td>
            <td>
                  | <a href='javascript:UserShoppingCart(<%# Eval("UserId") %>)'>
                        购物车</a> |
            </td>
            <td><a href='javascript:UserMessage(<%# Eval("UserId") %>)'>订阅连载消息</a> |</td>
            <td><a href='javascript:UserBookShelf(<%# Eval("UserId") %>)'>我的书架</a> |</td>
            <td><a href='javascript:UserHaveProp(<%# Eval("UserId") %>)'>道具&书币</a> |</td>
            <td><a href='javascript:UserChangePWD(<%# Eval("UserId") %>)'>修改密码</a> |</td>
            <td><a href='javascript:UserPayMoney(<%# Eval("UserId") %>)'>书币冲值</a> |</td>
        </ItemTemplate>
        <FooterTemplate>
        <td><asp:LinkButton ID="exit" runat="server" Text="退出登陆" onclick="exit_Click"></asp:LinkButton></td></tr>
        </table>
        </FooterTemplate>
    </asp:Repeater>
    
</div>