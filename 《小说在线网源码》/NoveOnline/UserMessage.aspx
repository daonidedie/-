<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMessage.aspx.cs" Inherits="UserMessage" %>

<%@ Register Assembly="PageControls" Namespace="PageControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .pageC
        {
        	font-size:12px;
        }
        
       a
{
	text-decoration: none;
}

a:link
{
	color: Black;
}

a:hover
{
	text-decoration: underline;
}

a:visited
{
	color: Black;
}

    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
           <div style="width:397px;height:50px;font-size:12px;line-height:50px;text-align:left;border:1px solid #BBB;display:table;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
                   <div style="float:left;width:100px;padding-left:5px;">小说订阅连载信息</div>
                   <div style="float:left;padding-top:12px;"><cc1:PageControl ID="PageControl1" runat="server" ControlID="repMessage" TypeName="SQLServerDAL.UsersAccess" MethodName="getUserMessage" ShowPage="2"  PageSize="5" StyleNumber="2" ControlWidth="200px" CssClass="pageC" BKColor="filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FAFAFA');"/></div>
                    
           </div>
           <div style="border:1px solid #BBB;width:397px;border-top:0px;height:180px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FAFAFA',endColorStr='#EBEBEB');">
           <asp:GridView ID="repMessage" runat="server" ShowHeader="false" GridLines="None" 
                   AutoGenerateColumns="false" onrowcommand="repMessage_RowCommand">
           <Columns>
            <asp:TemplateField>
            <ItemTemplate>
               <div style="width:397px;height:33.4px;font-size:12px;line-height:33.4px;border-bottom:1px dotted #BBB;">
                 <div style="width:260px;float:left;padding-left:5px;"><%# string.Format("{0}", Eval("MessageTitle"))%></div>
                 <div style="width:30px;float:left;margin-top:1px;"><asp:LinkButton ID="LinkButton1" runat="server" Text="删除" CommandName="delmsg" CommandArgument='<%# Eval("MessageId") %>' OnClientClick="return confirm('确定要删除该消息吗？');"></asp:LinkButton></div>
                 <div style="width:87px;float:left;padding-left:5px;"><%# string.Format("日期：{0}",Eval("SendDate")) %></div>
                 
               </div>
               </ItemTemplate>
            </asp:TemplateField>
            </Columns>
           </asp:GridView>
           </div>

            </div>
    </form>
</body>
</html>
