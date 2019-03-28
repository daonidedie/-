<%@ Control Language="C#" AutoEventWireup="true" CodeFile="booksections.ascx.cs" Inherits="BookControls_booksections" %>

<div style="border:1px solid #BBB;height:30px; line-height:30px;text-align:right;width:695px">
<div style="float:left;padding-left:10px;">当前：章节管理</div>
<div style="float:right;padding-right:10px;">
<asp:HyperLink runat="server" ID="face" Text="封面设置" ></asp:HyperLink>
<asp:HyperLink runat="server" ID="vs" Text="卷章管理" ></asp:HyperLink>
</div>
</div>

<div>
    
      <div style="height:30px;line-height:30px;text-align:left;padding-left:10px;">
            <asp:Label ID="lblPropNumber" runat="server"></asp:Label>
            <asp:HyperLink Text="首页" NavigateUrl="#" runat="server" ID="FirstPage"></asp:HyperLink>
            <asp:HyperLink Text="上一页" NavigateUrl="#" runat="server" ID="PrvPage"></asp:HyperLink>
            <asp:HyperLink Text="下一页" NavigateUrl="#" runat="server" ID="NewxtPage"></asp:HyperLink>
            <asp:HyperLink Text="尾页" NavigateUrl="#" runat="server" ID="LastPage"></asp:HyperLink>
      </div>
       
      <asp:Repeater ID="gr2" runat="server" onitemcommand="gr2_ItemCommand">
      <HeaderTemplate>
      <table width="688px;" cellpadding="0" cellspacing="0" style="margin-left:10px;" > 
       <th style="width:460px;height:1px;"></th>
       <th style="width:100px;"></th>
       <th style="width:100px;"></th>
      </HeaderTemplate>
      <FooterTemplate></table></FooterTemplate>
        <ItemTemplate>
            <tr style="height:30px;line-height:30px;">
                <td style="text-align:left;border-bottom:1px dotted #BBB;"><asp:Label runat="server" id="Label1" Text='<%#Eval("SectionTitle") %>'></asp:Label></td>
                <td style="text-align:center;border-bottom:1px dotted #BBB;"><asp:Label runat="server" id="Label2" Text='<%# string.Format("字数：{0}",Eval("CharNum")) %>'></asp:Label></td>
                <td style="text-align:center;border-bottom:1px dotted #BBB;text-align:center;">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/delete.gif" Width="12px" Height="12px" />
                     <asp:LinkButton ID="LinkButton2" Text="删除该章节" runat="server" OnClientClick="return confirm('确定要删除吗？');" CommandName="delsec" CommandArgument='<%#Eval("SectiuonId") %>' ></asp:LinkButton>
                    
                </td>
            </tr>
        </ItemTemplate>
    
    </asp:Repeater>
    
</div>