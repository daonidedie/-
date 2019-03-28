<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserHaveProp.aspx.cs" Inherits="UserHaveProp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        
        body
        {
            font-size:12px;	
            background-color:#F4F4F4;
        }
    </style>

     
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="../Js/Dialog.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">

    
    <div style="width:542px;height:50px;line-height:50px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FFFFFF');border:1px solid #BBB;">
    <asp:Label ID="lblUserProp" runat="server"></asp:Label>    
    <asp:HyperLink Text="首页" NavigateUrl="#" runat="server" ID="FirstPage" ForeColor="Black" Font-Underline="false"></asp:HyperLink>
    <asp:HyperLink Text="上一页" NavigateUrl="#" runat="server" ID="PrvPage" ForeColor="Black" Font-Underline="false"></asp:HyperLink>
    <asp:HyperLink Text="下一页" NavigateUrl="#" runat="server" ID="NewxtPage" ForeColor="Black" Font-Underline="false"></asp:HyperLink>
    <asp:HyperLink Text="尾页" NavigateUrl="#" runat="server" ID="LastPage" ForeColor="Black" Font-Underline="false"></asp:HyperLink> 
    </div>
    <div style="width:542px; height:330px; filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='#EBEBEB');border:1px solid #BBB;display:table;border-top:0px;">
            <asp:Repeater  runat="server" ID="repHaveProp" 
                onitemcommand="repHaveProp_ItemCommand" >
            <ItemTemplate>
            <div style="float:left;width:165px;height:150px;border:1px solid #BBB;margin-left:10px;margin-top:8px;background-color:White;">
            <div style="text-align:center;"><asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format(@"~/Images/prop/{0}",Eval("PropImage")) %>' Width="105" Height="105" /></div>
             <div style="text-align:center;margin-top:5px;">
             <asp:Label runat="server" ID="sdfasdf" Text='<%#  string.Format("{0}",Eval("PropName")) %>'></asp:Label>
             <asp:Label Text='<%#  string.Format("数量：{0}",Eval("userPropNumber")) %>' runat="server" ID="lblPropNumber"></asp:Label>
              <br />
              <div style="height:5px;"></div>
               &nbsp;&nbsp;<uc:LinkButtonEx ID="delbtn" runat="server" Text="使用道具" Value='<%# Eval("PropId") %>' Font-Underline="false" ForeColor="Black"  CommandName="useProp"  CommandArgument='<%# Eval("PropId") %>' OnClientClick='return confirm("确定要使用该道具吗？");'></uc:LinkButtonEx>
             </div>  
            </div>
                
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
