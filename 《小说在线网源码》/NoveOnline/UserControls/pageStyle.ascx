<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pageStyle.ascx.cs" Inherits="UserControls_pageStyle" %>

   <script type="text/javascript">
       function updateBodyColor(color) {


           var login = document.getElementById("Div_login");
           if (login) {
               login.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='" + color + "');";
           }

           var userpanel = document.getElementById("Div_userPanel");
           if (userpanel) {
               userpanel.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='" + color + "');";
           }

           var foot = document.getElementById("foot")
           foot.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='" + color + "',endColorStr='#FFFFFF');";

           var cs = document.getElementById("jstitle");
           cs.style.filter = "progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='" + color + "',endColorStr='#FFFFFF');";

           userStyle = color;

       }
       function sectionsinfoColor(color) {
           var sectionsinfo = document.getElementById("sectionsinfo");
           sectionsinfo.style.color = color;
       }
       function sectionsinSize(size) {
           var sectionsinfo = document.getElementById("sectionsinfo");
           sectionsinfo.style.fontSize = size;
       }

       var currentpos, timer;
       function initialize() {
           timer = setInterval("scrollwindow()", 100); //设置滚动的速度  
       }
       function sc() { clearInterval(timer); }
       function scrollwindow() { window.scrollBy(0, 1); }
       document.onmousedown = sc; document.ondblclick = initialize;

    </script>

    
<div id="change" style="border:1px solid #CCCCCC;margin-bottom:5px;border-top:0px;">
        <div style="text-align:left;height:20px;padding-top:8px;margin-left:5px;">
            <div style="float:left;font-size:12px;font-weight:bold;">页面风格:</div>
            <div style="background-color: #F4F4F4; border:1px solid #10ADC9; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#F4F4F4')"></div>
            <div style="background-color: #EBEBEB; border:1px solid #A4919E; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#EBEBEB')"></div>
            <div style="background-color: #A7EDAC; border:1px solid #71E179; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#A7EDAC')"></div>
            <div style="background-color: #D9BEB5; border:1px solid #C79F92; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#D9BEB5')"></div>
            <div style="background-color: #BDC0F4; border:1px solid #9195EC; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#BDC0F4')"></div>
            <div style="background-color: #F5F8AD; border:1px solid #EDF37A; width:12px;height:12px;float:left;margin-left:5px;" onclick="updateBodyColor('#F5F8AD')"></div>
        </div>
</div>