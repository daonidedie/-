<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小说搜索</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>

    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>

    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>

    <link href="Css/logo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        window.onload = function () {
            if (document.getElementById("lblnobook")) {

                var dv = document.getElementById("sediv");
                dv.style.border = "0px";
                dv.style.backgroundColor = "White";
                dv.style.paddingTop = "50px";

            }
        }

        function changborder(div) {
            div.style.border = "1px solid #DDD";
            div.style.backgroundColor = "#F4F4F4";
            
        }

        function defaultborder(div) {
            div.style.border = "1px dotted #BBB";
            div.style.backgroundColor = "White";
            
        }
        
    </script>

</head>
<body>
      <center>
        <div id="index">
            <form id="form1" runat="server">
           
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != "" && Session["NewUser"] != null)
                        {
                     %>
                            <uc:LoginUserPanel ID="loginInUserPanel" runat="server"/>
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


                <uc:header ID="header" runat="server" />
                <div id="boostype">
                    <uc:booktype ID="BookType" runat="server" />
                </div>
                <div id="serch">
                    <uc:Serch runat="server" id="serchCt"/>
                </div>


                <div style="border:1px solid #CCCCCC;width:948px;display:table;padding-top:5px;" id="sediv">

               <asp:Repeater runat="server" ID="gv1">
                <ItemTemplate>
                <div style="width:465px;float:left;margin-bottom:5px;margin-left:4.5px;border:1px dotted #BBB;background-color:White;" onmouseover="changborder(this);" onmouseout="defaultborder(this);">
                    <table style="width:465px;float:left;">
                    <tr>
                        <td rowspan="4" style="text-align:center; height:150px;padding-top:2px;width:148px;"><asp:Image runat="server" ID="bookImage" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("BookImage"))  %>' Width="120px" Height="150px"/></td>
                    </tr>
                    <tr>
                        <td style="height:30px;border-bottom:1px dotted #BBB;"><%# string.Format("{0}[{1}]",Eval("BookName"),Eval("TypeName")) %></td>
                        <td style="height:30px;border-bottom:1px dotted #BBB;">作者：<%# Eval("UserName")%></td>
                        <td style="height:30px;border-bottom:1px dotted #BBB;"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height:100px;text-align:left;vertical-align:top;">
                            <br />　　<%# Eval("BookIntroduction").ToString().Length > 160 ? Eval("BookIntroduction").ToString().Substring(0, 160) + "……" : Eval("BookIntroduction").ToString() %>
                        </td>
                    </tr>
                    <tr><td colspan="4" style="text-align:right;padding-right:10px;">
                        <asp:HyperLink ID="hypbllid" runat="server" NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'><img src="Images/clickbook.jpg" border=0 /></asp:HyperLink>
                    </td></tr> 
                    <tr><td colspan="4" style="text-align:right;"></td></tr>
                    </table>
                    </div>
                </ItemTemplate>
               </asp:Repeater>
               <div>
                        <div style="clear:both;width:948px; background-color:#F4F4F4;height:30px;line-height:30px;border-top:1px solid #CCCCCC;" id="pagediv" runat="server">
                        <asp:Label runat="server" ID="lblPage"></asp:Label>
                        <asp:HyperLink Text="首页" runat="server" ID="LinkFirst"></asp:HyperLink>
                        <asp:HyperLink Text="上一页" runat="server" ID="LinkPrece"></asp:HyperLink>
                        <asp:HyperLink Text="下一页" runat="server" ID="LinkNext"></asp:HyperLink>
                        <asp:HyperLink Text="末页" runat="server" ID="LinkLast"></asp:HyperLink>
                    </div>
               </div>
               </div>



               <asp:Label Text="" runat="server" ID="lblnobook" Visible="false"></asp:Label>
</div>
</form>
</div>
</center>
</body>
</html>