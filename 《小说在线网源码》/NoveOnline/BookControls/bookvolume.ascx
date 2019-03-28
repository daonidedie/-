<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bookvolume.ascx.cs" Inherits="BookControls_bookvolume" ViewStateMode="Disabled" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

 


<div style="border:1px solid #BBB;height:30px; line-height:30px;text-align:right;">
<div style="float:left;padding-left:10px;">正在管理的是：<asp:Label runat="server" ID="lbname"></asp:Label></div>
<div style="float:right;padding-right:10px;">
<asp:HyperLink runat="server" ID="face" Text="封面设置" ></asp:HyperLink>
<asp:HyperLink runat="server" ID="vs" Text="卷章管理" ></asp:HyperLink>
</div>

</div>

<div>
 

    <div style="border:0px solid #BBB;border-top:0px;text-align:left;padding-left:10px;height:30px;line-height:30px;display:table;width:686px;padding-top:2px;margin-top:10px;">
        
       <div  style="float:left;width:600px;">添加新卷：<asp:TextBox ID="txnewVO" runat="server" Width="400px"></asp:TextBox>
        卷序：<asp:TextBox ID="txnum" runat="server" Width="24px" style="text-align:center;" Text="1"></asp:TextBox>(请输数字)</div>
        <asp:ImageButton ImageUrl="~/Images/Submit.gif" runat="server" 
            style="margin-top:1px;" onclick="Unnamed1_Click"/>
    </div>
   
    <div style="display:table;border:0px solid #BBB;width:695px;">
    <div style="height:30px;line-height:30px;text-align:left;padding-left:10px;">所有分卷：</div>
    <asp:Repeater ID="gr1" runat="server" onitemcreated="gr1_ItemCreated" 
            onitemcommand="gr1_ItemCommand">
      <HeaderTemplate>
      <table width="688px;" cellpadding="0" cellspacing="0" style="margin-left:10px;" > 
       <th style="width:488px;height:1px;border-bottom:1px solid #BBB;"></th>
       <th style="width:100px;border-bottom:1px solid #BBB;"></th>
       <th style="width:200px;border-bottom:1px solid #BBB;"></th>
      </HeaderTemplate>
      <FooterTemplate></table></FooterTemplate>
        <ItemTemplate>
            <tr style="height:30px;line-height:30px;">
                <td style="text-align:left;border-bottom:1px dotted #BBB;"><asp:Label runat="server" id="Label1" Text='<%#Eval("ValumeName") %>'></asp:Label></td>
                <td style="text-align:center;border-bottom:1px dotted #BBB;"><asp:Label runat="server" id="Label2" Text='<%# string.Format("卷序：{0}",Eval("ValumeNumber")) %>'></asp:Label></td>
                <td style="text-align:right;border-bottom:1px dotted #BBB;">
                    <asp:Image runat="server" ImageUrl="~/Images/delete.gif" Width="12px" Height="12px" />
                    <asp:LinkButton ID="lkbt"  Text="删除" runat="server" OnClientClick="return confirm('确认要删除吗？这将删除该卷包含的所有章节！')" CommandName="delvo" CommandArgument='<%#Eval("VolumeId") %>'></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Fwdw_icons_25.png" Width="12px" Height="12px" />
                    <asp:HyperLink runat="server" ID="hyp12" Text="管理该卷章节" NavigateUrl='<%# string.Format("~/AuthorIndex.aspx?type=2&ct=3&volumeId={0}&bookId={1}",Eval("VolumeId"),Eval("BookId")) %>'></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    
    </asp:Repeater>
    </div>
</div>