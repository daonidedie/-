<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Books.aspx.cs" Inherits="Books" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>本站书库</title>
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
                
                <div id="allbook" style="margin-bottom:5px;">
                    <div class="title">
                         <div style="background-image:url(Images/title1.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;text-indent:25px;" >书本筛选</div>
                    </div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>

                    <uc:allbook  runat="server" ID="allbooks"/>
                </div>

                <uc:foot  runat="server" ID="booksfoot"/>

                </div>
                </form>
                </div>
</center>
</body>
</html>