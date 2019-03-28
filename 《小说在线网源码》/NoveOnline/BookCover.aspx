<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookCover.aspx.cs" Inherits="BookCover" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>小说封面</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>

    <script type="text/javascript">


        //得到不同的随机验证码
        function changPicValid() {
            var img = document.getElementById("img1");
            img.src = img.src + "?rnd=" + Math.random();
            return false;
        }



        //获取当前session中的验证码
        function getSessionString() {
           
            var tx = document.getElementById("txValid");
            if (tx.value == "") {
                 alert("请输入验证码！");
                 return false;
             }

             var oEditor = FCKeditorAPI.GetInstance("fckPL");

             if (oEditor.GetXHTML(true) == "") {
                 alert("评论内容不能为空！");
                 return false;
             }
        }


    </script>




</head>

<body>
<center>
    <form id="form1" runat="server"> 
    <asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
    <div id="index">
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
        </div>

        <div style="float:left;">
            <div style="border:1px solid #D6D6D6;width:720px;height:360px;background-color:#F4F4F4;">
                <div style="float:left;margin-top:20px;margin-left:20px;margin-bottom:3px;">
                    <div id="bookImg" style="border:1px solid #D6D6D6;width:160px;height:190px;">
                        <asp:Image ID="ibookImg" runat="server"  Width="160px" Height="190px" />
                    </div>
                    <div id="bookButton" style="width:160px;height:130px;">
                        <div style="margin-top:20px;"><asp:ImageButton ID="ibBookRead" runat="server" ImageUrl="~/Images/clickbook.jpg" /></div>
                        <div style="margin-top:10px;">
                                <asp:ImageButton runat="server" ID="readingbook" 
                                    ImageUrl="~/Images/b_nydiyuebg.gif" onclick="readingbook_Click" />
                                </div>
                        <div style="margin-top:10px;">
                                <asp:ImageButton runat="server" ID="bookshelf" 
                                    ImageUrl="~/Images/b_nybutt02.gif" onclick="bookshelf_Click"/>
                        </div>
                        
                    </div>
                </div>
                <div style="float:left;margin-top:20px;margin-left:10px;margin-bottom:20px;height:321px;width:503px;border:1px solid #D6D6D6;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FFFFFF');">
                    <div style="height:28px;font-size:28px;padding-top:10px;font-weight:bold;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FFFFFF');">
                        <asp:Label ID="hlBookName" runat="server"></asp:Label>
                    </div>
                    <div style="font-size:12px;border-bottom:1px solid #D6D6D6;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FFFFFF',endColorStr='#EBEBEB');">
                        <table style="width:100%;">
                            <tr>
                                <td style="height:25px;">作者：<asp:Label ID="lbAuthor" ForeColor="Red" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table style="width:95%;margin-left:10px;">
                            <tr>
                                <td style="width:65px;font-size:12px;font-weight:bold;">图书分类:</td>
                                <td runat="server" id="bookType" style="width:40px;text-align:left;"></td>
                                <td style="width:50px;font-size:12px;font-weight:bold;">点击量:</td>
                                <td style="width:35px;text-align:left;" id="chickAll" runat="server"></td>
                                <td style="width:65px;font-size:12px;font-weight:bold;">更新时间:</td>
                                <td style="width:65px;text-align:left;" id="updateTime" runat="server"></td>
                                <td style="font-size:12px;font-weight:bold;width:64px;">小说状态:</td>
                                <td id="stateName" runat="server" style="width:40px;"></td>
                            </tr>
                        </table>
                    </div>
                    
                    <div style="height:110px;text-align:left;padding:20px 10px 20px 20px;display:table;line-height:18px;" id="contents" runat="server"></div>
                    
                    <div style="height:84px;text-align:left;padding-left:10px;">
                        <div style="height:60px;width:300px;font-size:14px;font-weight:bold;float:left;">
                            <div style="margin-left:10px;">
                               <div style="height:25px;">最新章节：<asp:HyperLink ID="SectionTitle" runat="server" 
                                       Font-Bold="False" Font-Size="12px">[SectionTitle]</asp:HyperLink></div>
                               <div style="width:120px;float:left;height:25px;">小说搜索关键字：</div><div style="font-size:12px;float:left;font-weight:normal;" id="keyName" runat="server" Font-Bold="false"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
  
        <div style="float:left;margin-left:5px;margin-bottom:5px;">
         
            <div style="border:1px solid #D6D6D6;width:221px;display:table;padding-bottom:5px;height:355px;background-color:#F1F1F1;">
                <div class="title" style="font-weight:bold;border-bottom:1px solid #D6D6D6;">获赠消息</div>
                <div style="margin-left:5px;margin-top:5px;">
                    <div style="border:1px solid #D6D6D6;width:210px;height:157px;">
                    <div> <asp:Image runat="server" ImageUrl="~/Images/prop/shupiao1.jpg" /></div>
                        <div style="font-size:12px;margin-top:5px;">获得书票：<asp:Label ID="lbTicketPlace" runat="server"></asp:Label>
                        (<asp:Label ID="lbTicketAmount" runat="server"></asp:Label>)
                        </div>
                        <div style="height:18px;margin-top:15px;">
                            <asp:ImageButton ID="hlTicket" runat="server" 
                                ImageUrl="~/Images/newinfo_butt02.gif" onclick="hlTicket_Click"></asp:ImageButton></div>
                    </div>
                </div>
                <div style="float:left;margin-left:5px;margin-top:5px;">
                    <div style="border:1px solid #D6D6D6;width:210px;height:157px;">
                    <div> <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/prop/xianhua1.jpg" Width="70px" Height="70px"/></div>
                        <div style="font-size:12px;margin-top:15px;">获赠鲜花：<asp:Label ID="lbFlowerPlace" runat="server"></asp:Label>
                        (<asp:Label ID="lbFlowerAmout" runat="server"></asp:Label>)
                        </div>
                        <div style="height:18px;margin-top:15px;">
                            <asp:ImageButton ID="hlFlower" runat="server" 
                                ImageUrl="~/Images/nvfloaeer.gif" onclick="hlFlower_Click" style="margin-top:3px;"></asp:ImageButton></div>
                    </div>
                </div>
            </div>

        </div>

                <div style="clear:both;border:1px solid #D6D6D6;width:948px;display:table;padding-bottom:10px;margin-bottom:5px;">
                <div class="title" style="font-weight:bold;font-size:14px;border-bottom:1px solid #D6D6D6;" id="replayTitle" runat="server"></div>
                <asp:Repeater ID="BookReplayInfo" runat="server" 
                    onitemdatabound="BookReplayInfo_ItemDataBound">
                    <ItemTemplate>
                        <div style="clear:both;border-bottom:1px dotted #D6D6D6;width:946px;display:table;padding-bottom:5px;">
                            <div style="margin-top:10px;float:left;width:180px;padding-bottom:5px;">
                                <asp:Image ID="userImg" runat="server" ImageUrl='<%# string.Format("~/Images/AuthorFace/{0}",Eval("UsetImage")) %>' Height="120px" Width="120px" /><br /><br />
                                <asp:Label ID="lbUserTypeName" runat="server" Text='<%# string.Format("{0}",Eval("UserTypeName")) %>' Font-Bold="True"></asp:Label>
                            </div>
                            <div style="margin-top:10px;float:left;height:80px;margin-left:10px;display:table;padding-bottom:10px;">
                                <div style="color:#41803C;width:755px;text-align:left;border-bottom:1px dotted #EBEBEB;height:30px;line-height:30px;">
                                    <asp:Image ID="titleImg" runat="server" ImageUrl="~/Images/Button/2011-08-18_02-02-53.png" />
                                    <asp:Label ID="lbUserName" runat="server" Text='<%# string.Format("{0}",Eval("UserName")) %>' Font-Bold="True" ForeColor="#DA3527"></asp:Label>
                                    <asp:Label ID="lbTime" runat="server" Text='<%# string.Format("　{0}",Eval("ReplayTime")) %>'></asp:Label>
                                </div>
                                <div style="width:755px;text-align:left;padding-top:10px;line-height:20px;"><%# Eval("ReplayContext")%></div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
           <FCKeditorV2:FCKeditor ID="fckPL" runat="server" Width="950px" Height="300px"></FCKeditorV2:FCKeditor>
           
           
                       <div style="clear:both; height:50px;line-height:50px;padding-left:35%;padding-top:5px;border:1px solid #D6D6D6;border-top:0px;border-bottom:5px;margin-bottom:5px;border-bottom:1px solid #D6D6D6;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='1',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
                            <div style="float:left;width:100px;">
                                <asp:ImageButton ID="issuBmit" runat="server" 
                                    ImageUrl="~/Images/sendMessage.png" style="margin-top:5px;" 
                                    OnClientClick="return getSessionString();" Height="36px" 
                                    onclick="issuBmit_Click" />
                            </div>
                            <div style="float:left;width:110px;text-align:left;margin-left:20px;">
                                <asp:Label ID="Label1" Text="验证码：" runat="server"></asp:Label>
                                <asp:TextBox ID="txValid" runat="server" style="width:50px;border:1px solid Gray;text-align:center;" MaxLength="4"></asp:TextBox>
                            </div>
                            <div style="float:left;width:100px;margin-top:13px;"><img src="handler/HandlerPicValid.ashx" id="img1" alt="" />  
                            </div>
                            <div style="float:left;width:38px;"> <asp:LinkButton ID="LinkButton1" runat="server" Text="换一张" OnClientClick="return changPicValid();"></asp:LinkButton></div>
                            
                       </div>


              <uc:foot runat="server" ID="ftcover" /> 
    </div>
    </form>
    </center>
</body>
</html>
