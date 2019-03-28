<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorIndex.aspx.cs" Inherits="AuthorIndex" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>作者专区</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
        <div id="index">
            <form id="form1" runat="server">
            <asp:ScriptManager runat="server" ID="sc2"></asp:ScriptManager>
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != "" && Session["NewUser"] != null)
                        {
                    %>
                    <uc:LoginUserPanel ID="loginInUserPanel" runat="server" />
                    <% 
                        }
                        else
                        {
                    %>
                    <uc:userpanel ID="userpanel" runat="server" />
                    <%
                        }                     
                    %>
                </div>

                <div style="width: 948px; display: table; border: 1px solid #BBB;padding-bottom:5px;padding-top:5px;
                    float: left;filter:progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='#EBEBEB');">

                    <div style="width: 140px; float:left;margin-left:15px;display:table;background-color:White;">
                        <div style="height: 85px; border: 1px solid #CCCCCC;">
                            <asp:Image runat="server" ImageUrl="~/Images/author/allBooks.jpg" Height="80px" Width="80px" /></div>
                        <div style="height: 30px;line-height:30px;border: 1px solid #CCCCCC;border-top:0px;">
                            <asp:HyperLink Text="添加新书" runat="server" ID="hyp1" NavigateUrl="~/AuthorIndex.aspx?type=1"></asp:HyperLink>
                        </div>
                    </div>
                    <div style="width: 140px;float:left;margin-left:15px;display:table;background-color:White;">
                        <div style="height: 85px; border: 1px solid #CCCCCC;">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/author/bookEdit.jpg" Height="80px" Width="80px"/></div>
                        <div style="border: 1px solid #CCCCCC;border-top:0px;height: 30px;line-height:30px;">
                            <asp:HyperLink Text="书本管理" runat="server" ID="HyperLink1" NavigateUrl="~/AuthorIndex.aspx?type=2"></asp:HyperLink></div>
                    </div>
                    <div style="width: 140px;float:left;margin-left:15px;display:table;background-color:White;">
                        <div style="height: 85px; border: 1px solid #CCCCCC;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/author/newSecionts.jpg" Height="80px" Width="80px" /></div>
                        <div style="border: 1px solid #CCCCCC;border-top:0px;height: 30px;line-height:30px;">
                            <asp:HyperLink Text="发布新章节" runat="server" ID="HyperLink2" NavigateUrl="~/AuthorIndex.aspx?type=3"></asp:HyperLink></div>
                    </div>
                        <div style="width: 140px; float:left;margin-left:15px;display:table;background-color:White;">
                            <div style="height: 85px; border: 1px solid #CCCCCC;">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/author/shenhezhong.jpg" Height="80px" Width="80px"/></div>
                            <div style="border: 1px solid #CCCCCC;border-top:0px;height: 30px;line-height:30px;">
                                <asp:HyperLink Text="待审核章节" runat="server" ID="HyperLink3" NavigateUrl="~/AuthorIndex.aspx?type=4"></asp:HyperLink></div>
                        </div>
                        <div style="width: 140px; float:left;margin-left:15px;display:table;background-color:White;">
                            <div style="height: 85px; border: 1px solid #CCCCCC;">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/author/giveMoney.jpg" Height="80px" Width="80px" /></div>
                            <div style="border: 1px solid #CCCCCC;border-top:0px;height: 30px;line-height:30px;">
                                <asp:HyperLink Text="领取稿酬" runat="server" ID="HyperLink5" NavigateUrl="~/AuthorIndex.aspx?type=6"></asp:HyperLink></div>
                        </div>
                         <div style="width: 140px; float:left;margin-left:15px;display:table;padding-top:40px;">
                            <asp:Label id="lbtime" runat="server"></asp:Label>
                            <br /><br />祝您写作愉快！
                             
                        </div>
                    </div>
                </div>

               
                <div id="AuthorContorls" runat="server" style="width: 948px; display: table; text-align:center; border: 1px solid #CCCCCC;float: left;border-top:1px;padding-bottom:10px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FAFAFA',endColorStr='#FFFFFF');
">

                </div>

            </form>
        </div>
    </center>
</body>
</html>
