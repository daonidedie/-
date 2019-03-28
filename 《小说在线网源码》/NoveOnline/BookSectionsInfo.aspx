<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookSectionsInfo.aspx.cs" Inherits="BookSectionsInfo" %>
<%@ Import Namespace="Common"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>小说在线 - 章节阅读</title>
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
        function updateBodyColor(color) {
            var rnd = Math.random();
            var xmlhttp;
            if (window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            var url = "handler/setColor.aspx?Color=" + color +  "&rnd=" + rnd;
            xmlhttp.open("get", url, true);
            xmlhttp.send(null);
            xmlhttp.onreadystatechange = function () {}
            var obj = document.getElementById("change");
            obj.style.backgroundColor = color;
//            obj.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='" + color + "');";
            
            var login = document.getElementById("Div_login");
            if (login) {
                login.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='" + color + "',endColorStr='#FFFFFF');";
            }
            var userpanel = document.getElementById("Div_userPanel");
            if (userpanel) {
                userpanel.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='"+ color +"',endColorStr='#FFFFFF');";
            }
            var foot = document.getElementById("foot") 
            foot.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='" + color + "',endColorStr='#FFFFFF');";
        }

        function sectionsinfoColor(color) {
            var xmlhttp;
            var rnd = Math.random();
            if (window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }

            var url = "handler/setFontColor.aspx?FontColor=" + color + "&rnd=" + rnd;
            xmlhttp.open("get", url, true);
            xmlhttp.send(null);
            xmlhttp.onreadystatechange = function () { }
            var sectionsinfo = document.getElementById("sectionsinfo");
            sectionsinfo.style.color = color;
        }

        function sectionsinSize(size) {
            var xmlhttp;
            var rnd = Math.random();
            if (window.ActiveXObject) {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } else if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            var url = "handler/setFontColor.aspx?FontSize=" + size + "&rnd=" + rnd;
            xmlhttp.open("get", url, true);
            xmlhttp.send(null);
            xmlhttp.onreadystatechange = function () { }
            var sectionsinfo = document.getElementById("sectionsinfo");
            sectionsinfo.style.fontSize = size;
        }

        window.onload = function () {
            var Color = "<%=getColor()%>";
            var FontColor = "<%=getFont()%>";
            var FontSize = "<%=getFontSize()%>";
            updateBodyColor(Color);
            sectionsinfoColor(FontColor)
            sectionsinSize(FontSize);
        }

    </script>


    <script type="text/javascript">

        var currentpos, timer;
        function initialize() {
            timer = setInterval("scrollwindow()", 30); //设置滚动的速度  
        }
        function sc() { clearInterval(timer); }
        function scrollwindow() { window.scrollBy(0, 1); }
        document.onmousedown = sc; document.ondblclick = initialize;

    </script>
    
</head>
<body bgcolor="white">
<center>
   <div id="index">
    <form id="form1" runat="server">
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != ""  && Session["NewUser"] != null)
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

        <div id="change" style="border:1px solid #CCCCCC;margin-bottom:5px;">
        <div style="text-align:left;height:30px;padding-top:8px;margin-left:10px;">
            <div style="float:left;font-size:12px;font-weight:bold;">选择背景颜色:</div>
            <div style="background-color: #FFFFFF; border:1px solid #BBB; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#FFFFFF')"></div>
            <div style="background-color: #F4F4F4; border:1px solid #10ADC9; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#F4F4F4')"></div>
            <div style="background-color: #EBEBEB; border:1px solid #A4919E; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#EBEBEB')"></div>
            <div style="background-color: #D6D6D6; border:1px solid #BBBBBB; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#D6D6D6')"></div>
            <div style="background-color: #A7EDAC; border:1px solid #71E179; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#A7EDAC')"></div>
            <div style="background-color: #D9BEB5; border:1px solid #C79F92; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#D9BEB5')"></div>
            <div style="background-color: #BDC0F4; border:1px solid #9195EC; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#BDC0F4')"></div>
            <div style="background-color: #F5F8AD; border:1px solid #EDF37A; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#F5F8AD')"></div>       


            <div style="float:left;font-size:12px;font-weight:bold;margin-left:20px;">选择字体颜色:</div>
            <div style="background-color: #000000; border:1px solid #BBBBBB; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#000000')"></div>
            <div style="background-color: #06464F; border:1px solid #021D20; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#06464F')"></div>
            <div style="background-color: #372F35; border:1px solid #171316; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#372F35')"></div>
            <div style="background-color: #115316; border:1px solid #0A2E0D; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#115316')"></div>
            <div style="background-color: #53352B; border:1px solid #2C1C16; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#53352B')"></div>
            <div style="background-color: #2027BB; border:1px solid #131771; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#2027BB')"></div>
            <div style="background-color: #C8D112; border:1px solid #595E09; width:12px;height:12px;float:left;margin-left:5px;" onclick="sectionsinfoColor('#C8D112')"></div>

            <div style="float:left;font-size:12px;font-weight:bold;margin-left:20px;">选择字体大小:</div>
            <div style="width:150px; height:12px; float:left; margin-left:5px;">
                <a href="#" onclick="sectionsinSize('12px')">12px</a>
                <a href="#" onclick="sectionsinSize('14px')">14px</a>
                <a href="#" onclick="sectionsinSize('16px')">16px</a>
                <a href="#" onclick="sectionsinSize('20px')">20px</a>
                
            </div>
        </div>

        <div style="font-family:华文隶书;margin-top:20px;font-size:24px;">
            <div><asp:Label ID="lbBookName" runat="server"></asp:Label></div>
            <div style="font-size:12px; font-family:宋体;padding-top:5px;width:100%;margin-top:5px;padding-bottom:5px;"><asp:Label ID="lbUserName" runat="server"></asp:Label>　<asp:Label ID="AddTime" runat="server"></asp:Label></div> 
        </div>
        <div id="sectionsinfo" runat="server" style="clear:both;margin-top:20px;margin-bottom:20px;padding-left:24px;padding-right:24px;line-height:34px;text-align:left;">内容</div>

        <div style="font-size:12px;width:100%;height:30px;line-height:30px;">
                                [<asp:HyperLink ID="hlUpLeaf" runat="server" Text="上一章"></asp:HyperLink>]
                    [<asp:HyperLink ID="cataLog" runat="server" Text="回目录"></asp:HyperLink>]
                   [<asp:HyperLink ID="lbNextLeaf" runat="server" Text="下一章"></asp:HyperLink>]
        </div>

       
        </div>


        <uc:foot runat="server" ID="ftasections"/>
       </div>
       </form>
    </div>
</center>
</body>
</html>
