<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="UserControls_header" %>
<div id="header">
    <div class="demo">
         <script type="text/javascript">
             var focus_width = 947;//图片宽
             var focus_height = 70//图片高
             var text_height = 0//设置显示文字标题高度,最佳为20（如果不显示标题值设为0即可）
             var swf_height = focus_height + text_height - 1;
             var swf_width = focus_width - 20;
             var pics = 'Images/logo/20110801125125.jpg|Images/logo/20110801125212.jpg|Images/logo/20110801131606.jpg'
             var links = '#|#|#'
             var texts = '无|无|无'

             document.write('<object ID="focus_flash" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
             document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="flash/adplay.swf"><param name="quality" value="high"><param name="bgcolor" value="#6E0E02">');
             document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
             document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
             document.write('<embed ID="focus_flash" src="flash/adplay.swf" wmode="opaque" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#6E0E02" quality="high" width="' + focus_width + '" height="' + swf_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>');



             //联系我们
             function ConnectUs() {
                 var diag = new Dialog("Diag");
                 diag.Width = 300;
                 diag.Height = 175;
                 diag.Title = "联系我们";
                 diag.URL = "../../../us.aspx?"
                 diag.ShowButtonRow = false;
                 diag.show();
                 return false;
             }
         </script>
    </div>
   
    <div id="menu2">
        <ul>
            <li><asp:HyperLink runat="server" Text="首页" NavigateUrl="~/Default.aspx"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="站内书库" NavigateUrl="~/Books.aspx?rnd=1&state=0&booktype=0&charnum=0"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="新书推荐" NavigateUrl="~/BooksAllInfo.aspx?bookTypeId=1"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="章节排行" NavigateUrl="~/BookRanking.aspx"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="最新章节" NavigateUrl="~/NewSections.aspx"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="道具商城" NavigateUrl="~/PropIndex.aspx"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" Text="作者专区" NavigateUrl="~/AuthorIndex.aspx" Target="_blank"></asp:HyperLink></li>
            <li><asp:HyperLink runat="server" ID="hypus" NavigateUrl="/us" Text="联系我们"  onclick="return ConnectUs();"></asp:HyperLink></li>
        </ul>
        <div class="cls">
        </div>
    </div>
</div>
