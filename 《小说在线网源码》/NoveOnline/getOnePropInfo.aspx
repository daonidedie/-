<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getOnePropInfo.aspx.cs" Inherits="getOnePropInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
         
         body
         {
            font-size:12px;	
         }
         
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:Repeater runat="server" id="repaProp">
            <ItemTemplate>
                <div style="width:390px;">
                    <div style="width:150px;height:200px;float:left;text-align:center;">
                        <asp:Image runat="server" ImageUrl='<%# string.Format(@"~/Images/prop/{0}",Eval("PropImage")) %>'   style="margin-top:20px;margin-bottom:10px;" /><br />
                        <asp:Label Text='<%#Eval("PropName")%>' runat="server" ></asp:Label>
                    </div>
                    <div style="width:236px;height:200px;float:left;">
                        <div style="height:30px;line-height:30px;text-indent:20px;border-bottom:1px dotted #CCCCCC;">
                            <asp:Label Text='<%# string.Format("单价：{0:f0}书币",Eval("PropPrice")) %>' runat="server"></asp:Label>
                        </div>
                        <div style="height:30px;line-height:30px;text-indent:20px;">
                           
                        </div>
                        <div style="height:30px;line-height:30px;text-indent:20px;">
                            <asp:Label ID="Label1" Text="使用后功能：" runat="server"></asp:Label>
                        </div>

                        <div style="margin-top:0px;width:215px;float:left;margin-left:20px;">
                            <asp:Label Text='<%# string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}",Eval("PropIntroduction")) %>' runat="server"></asp:Label>
                        </div>
                        
                    </div>
                </div>
                
            </ItemTemplate>
         </asp:Repeater>   

    </div>
    </form>
</body>
</html>
