<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BooksAllInfo.aspx.cs" Inherits="BooksAllInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">





<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新书一览</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>

    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <script src="Js/imagesSwitch.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />
    
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>

</head>
<body>
      <center>
        <div id="index">
            <form id="form2" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
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
                    <uc:booktype ID="BookType1" runat="server" />
                </div>
                <div id="serch">
                    <uc:Serch runat="server" id="serchCt"/>
                </div>

        
        
            <div style="border:1px solid #D6D6D6;width:210px;float:left;height:270px;">
                <div class="title">
                   <div style="background-image:url(Images/title1.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;text-indent:25px;" >
                   <uc:LabelEx ID="appointTypeId" runat="server" Value="选择类型"></uc:LabelEx>
                   </div>
                    
                </div>
                <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
                <div>
                    <uc:SectionTypeByNewBook runat="server" ID="sebkby"/>
                </div>
            </div>



            <%-- 这里放 --%>
                 <div id="ifocus">
	            <div id="ifocus_pic">
		            <div id="ifocus_piclist" style="left:0; top:0;">
			            <ul>
				           		<li><a href="#" id="l1" runat="server"><asp:Image runat="server" id="LargeImg1"/></a></li>
				                <li><a href="#" id="l2" runat="server"><asp:Image runat="server" id="LargeImg2"/></a></li>
				                <li><a href="#" id="l3" runat="server"><asp:Image runat="server" id="LargeImg3"/></a></li>
				                <li><a href="#" id="l4" runat="server"><asp:Image runat="server" id="LargeImg4"/></a></li>
			            </ul>
		            </div>
	            </div>
	            <div id="ifocus_btn">
		            <ul>
			            <li class="current"><a href="#" id="s1" runat="server"><asp:Image runat="server" id="SmallImg1" BorderStyle="None"/></a></li>
			            <li class="normal"><a href="#" id="s2" runat="server"><asp:Image runat="server" id="SmallImg2"  BorderStyle="None"/></a></li>
			            <li class="normal"><a href="#" id="s3" runat="server"><asp:Image runat="server" id="SmallImg3"  BorderStyle="None"/></a></li>
			            <li class="normal"><a href="#" id="s4" runat="server"><asp:Image runat="server" id="SmallImg4"  BorderStyle="None"/></a></li>
		            </ul>
	            </div>
                <div id="bookText" style="margin-top:10px;">
                    <asp:Repeater ID="bookTextRepeater" runat="server">
                        <ItemTemplate>
                            <div id="bookText_1" onmouseover="omsOver()" >
                               <asp:HyperLink runat="server" ID="hypo3" Text=' <%# string.Format("{0}",Eval("BookIntroduction").ToString().Length > 70 ? Eval("BookIntroduction").ToString().Substring(0, 70) + "……" : Eval("BookIntroduction").ToString()) %>'
                                NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId"))%>' Font-Underline="false"
                               ></asp:HyperLink> 
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


            <div style="width:200px;height:270px;border:1px solid #D6D6D6;float:left;">
                <div class="title">
                    <uc:LabelEx ID="appointTypeId2" runat="server"></uc:LabelEx>
                </div>
                <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
                <asp:Repeater ID="booksInfo2" runat="server">
                    <HeaderTemplate><div style="float:left;"></HeaderTemplate>
                    <FooterTemplate></div></FooterTemplate>
                    <ItemTemplate>
                        <div style="text-align:left;float:left;margin-left:15px;margin-right:10px;border-bottom:1px dotted #D6D6D6;">
                            <span style="float:left;padding-right:5px;padding-top:5px;padding-bottom:5px;">
                               <asp:HyperLink ID="hlBookName" runat="server" Text='<%# Eval("BookName") %>' ToolTip='<%# Eval("UserName") %>' Width="140px" NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'></asp:HyperLink> 
                            </span>
                            <span style="float:right;padding-top:5px;"><%# Eval("ticketNumber")%></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div style="clear:both; margin-top:5px;width:948px;height:420px;border:solid 1px #BBB;margin-bottom:5px;">
            <div class="title">
                <uc:LabelEx ID="leTypeName" runat="server"></uc:LabelEx>
            </div>
            <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
            <asp:Repeater ID="repeaterGetNewBooksCommend" runat="server">
                <ItemTemplate>
                    <div style="float:left;margin-top:7px;margin-left:9px;">
                        <div style="height:150px;border:solid 1px #BBB;padding:5px;width:135px;margin-bottom:8px;">
                            <asp:Image ID="imgs" runat="server" Width="135px" Height="150px" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("Images"))  %>' ToolTip='<%# Eval("BookIntroduction") %>' />
                        </div>
                        <span><asp:HyperLink ID="hlBookNames" runat="server" Text='<%# Eval("BookName") %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'></asp:HyperLink></span>  
                    </div>                 
                </ItemTemplate>
            </asp:Repeater> 
        </div>

        <div style="clear:both; margin-top:5px;width:948px;height:285px;border:solid 1px #BBB;margin-bottom:5px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='#FAFAFA');">
            <div class="title">
                <uc:LabelEx ID="lbMemory" runat="server"></uc:LabelEx>
            </div>
            <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
            <div style="margin-top:15px;margin-left:20px;margin-right:20px;clear:both;">
                <div style="text-align:left;margin-top:10px;margin-bottom:20px;">　　曾经，它们让我们激动、让我们彻夜等待。在一个公告下，在电脑前刷新着页面，就只为了能第一时间，第一眼看着到火热出炉的章节。如今，它们已经完结，静静的沉睡在书架之中，沉睡在记忆深处。现在，请关注此次历史回顾之旅，让我们一起重温这些经典之作。</div>
            </div>
            <div style="margin-bottom:5px;display:table;margin-left:9px;">
                <asp:Repeater ID="rpMemoryName" runat="server">
                    <ItemTemplate>
                        <div id="memoryName" style="float:left;width:145px;margin-bottom:20px;padding:5px; ">
                            <asp:Image ID="imgs" runat="server" Width="135px" Height="150px" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("Images"))  %>' ToolTip='<%# Eval("BookIntroduction") %>' /><br /><br />
                            <asp:HyperLink ID="hlName" runat="server" Text='<%# string.Format("{0}",Eval("BookName")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </form>
    </div>

    <uc:foot runat="server"  ID="ft"/>
    
</center>
</body>
</html>


