<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRemmendNewsReplay.ascx.cs" Inherits="UserControls_UserRemmendNewsReplay" %>

<script type="text/javascript">
    function cst(div)
    {
        div.style.backgroundColor = "#F7F7F7";
        div.style.border = "1px solid #CCCCCC";
        //div.style.backgroundImage = "url(Images/filter2.jpg)";
        div.style.cursor = "hand";
    }

    function cst1(div)
    {
        div.style.backgroundColor = "";
        div.style.border = "1px solid #EBEBEB";
        div.style.borderBottom = "1px solid #DDD";
        //div.style.backgroundImage = "";
        div.style.cursor = "default";
    }
</script>

<div id="con">
<ul id="tags">
  <li>
  <a onclick="selectTag('tagContent0',this)" 
  href="javascript:void(0)" style="font-weight:bold;">新闻咨询</a> </li>
  <li class="selectTag"><a onclick="selectTag('tagContent1',this)" 
  href="javascript:void(0)" style="font-weight:bold;">小说推荐</a> </li>
</ul>

<div id="tagContent">
<div style="height:5px;background-image:url('Images/tab/boxbg05.gif')"></div>
<div class="tagContent" id="tagContent0">
    
    <div>
        <div style="float:left;width:670px;">
        <asp:Repeater ID="repNews" runat="server">
            <ItemTemplate>
                <div style="width:665px;margin-left:2px;margin-top:10px;height:118px;border:1px solid #EBEBEB; "  onmouseover="cst(this);" onmouseout="cst1(this);">
                    <div style="float:left;width:100px;height:118px;">
                       <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/Images/Novelnews/{0}",Eval("NewsImages")) %>' Width="95px" Height="112px" style="margin-left:3px;margin-top:3px;"/>
                    </div>
                    <div style="width:555px;float:left;margin-left:5px;" >
                        <div style="height:21px;border-bottom:1px dotted #CCCCCC;text-align:center;line-height:21px;">
                             <div style="text-align:center;width:440px;float:left;">
                                <asp:Label runat="server" Text='<%# Eval("NewsTitle").ToString().Length>23 ? Eval("NewsTitle").ToString().Substring(0,23)+"……" : Eval("NewsTitle").ToString()  %>' style="color:Black;font-size:12px;"></asp:Label>
                             </div>
                             <div style="text-align:left;width:110px;float:right;" >
                                日期：<asp:Label runat="server" Text='<%# Eval("AddTimeString") %>' style="color:Black;"></asp:Label>
                             </div>
                        </div>
                    </div>
                    <div style="width:555px;float:left;height:85px;margin-left:5px;margin-top:5px;">
                        <span style="color:Black;">相关内容：</span>
                        <div style="margin-top:5px;height:60px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label Text='<%# Eval("NewsContens").ToString().Length>173 ? Eval("NewsContens").ToString().Substring(0,173)+"……" : Eval("NewsContens").ToString()  %>' runat="server" style="color:Black;line-height:17px;"></asp:Label>
                        </div>
                    </div>
                </div>
            </ItemTemplate>

        </asp:Repeater>
        </div>
        
    </div>
    
</div>

<div class="tagContent selectTag" id="tagContent1">

       <div style="float:left;width:220px;height:252px;text-align:center;">
       <asp:Repeater ID="repRecommand" runat="server">
       <HeaderTemplate><table style="margin-left:36px;margin-top:30px;"></HeaderTemplate>
       <ItemTemplate>
        <tr>
            <td><asp:Image runat="server" id="imgbook1" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("Images"))  %>' Width="150" Height="190"/></td>
        </tr>
        <tr>
            
        </tr>
        <tr>
            <td><span style="color:Black;">[<%#Eval("TypeName")%>]</span>  <a href='<%# string.Format("BookCover.aspx?bookId={0}",Eval("BookId")) %>'><%# Eval("BookName") %></a></td>
        </tr>
       </ItemTemplate>
       <FooterTemplate></table></FooterTemplate>
       </asp:Repeater>
       </div>

      <div id="div_remout" style="float:left;width:430px;height:252px;margin-left:20px;">
       <asp:GridView ID="gridRecommand" runat="server"  AutoGenerateColumns="false" 
              BorderStyle="None" style="Color:Black;" 
              onrowdatabound="gridRecommand_RowDataBound">
        <Columns>
        <asp:BoundField DataField="TypeName"  HeaderText="小说类型" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="None" />
        <asp:TemplateField HeaderText="书本名称" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="None" >
            <ItemTemplate>
                <a href='<%# string.Format("BookCover.aspx?bookId={0}",Eval("BookId")) %>'><%# Eval("BookName") %></a>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="StateName" HeaderText="状态" HeaderStyle-Width="70px" HeaderStyle-Height="35px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="None" />
            <asp:BoundField DataField="UserName" HeaderText="作者" HeaderStyle-Width="75px" ItemStyle-Height="22px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="None" /> 
            <asp:BoundField DataField="AddTimeString" HeaderText="入站日期" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="None" />
       
        </Columns>
       </asp:GridView>
       </div>
</div>



</div>

</div>