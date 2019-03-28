<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PropList.ascx.cs" Inherits="PropControls_PropList" ViewStateMode="Disabled" %>

<script type="text/javascript">

    function changeColor(div) {
        
        div.style.border = "1px solid #BBB";
        div.style.cursor = 'hand';

    }


    function changeColor1(div) {
        
        div.style.border = "1px solid #DDD";
        div.style.cursor = 'defalut';
    }

    function getInfo(pid) {
        var newdiag = new Dialog("Diag1");
        newdiag.Width = 407;
        newdiag.Height = 220;
        newdiag.Title = "道具介绍";
        newdiag.URL = "../../../getOnePropInfo.aspx?PID=" + pid;
        newdiag.ShowButtonRow = false;
        newdiag.show();
    }

</script>


<asp:Repeater runat="server" ID="repProp" 
    onitemdatabound="repProp_ItemDataBound"> 
    <ItemTemplate>
        <div style="float:left;width:210px;height:220px;margin-top:10px;border:1px solid #DDD;margin-left:14px;" onmouseover="changeColor(this);" onmouseout="changeColor1(this);">
                <div style="margin:10px;">
                    <asp:Image runat="server"  ImageUrl='<%# string.Format(@"~/Images/prop/{0}",Eval("PropImage")) %>' Width="110px" Height="110px" ToolTip='<%# Eval("PropIntroduction") %>'/>
                </div>
                <div style="margin:5px;">
                    <asp:Label Text='<%# string.Format("{0}",Eval("PropName")) %>' runat="server"></asp:Label>
                    (<a href='javascript:getInfo(<%# Eval("PropId") %>)'>道具功能</a>)
                </div>
                <div>
                    <asp:Label Text='<%# string.Format("价格：{0:f2} 书币",Eval("PropPrice")) %>' runat="server"></asp:Label>
                </div>
                <div style="margin:5px;">
                    <asp:HyperLink runat="server" ID="hypls" Text='' NavigateUrl='<%# Eval("PropId") %>'><img src="Images/addshopcart.GIF" border=0 /></asp:HyperLink>
                    
                </div>
        </div>  
    </ItemTemplate>
</asp:Repeater>
<div style="clear:both;height:50px;line-height:50px;">
    <asp:Label ID="lblPropNumber" runat="server"></asp:Label>
    <asp:Label ID="lblpageNumber" runat="server"></asp:Label>
    <asp:HyperLink Text="首页" NavigateUrl="#" runat="server" ID="FirstPage"></asp:HyperLink>
    <asp:HyperLink Text="上一页" NavigateUrl="#" runat="server" ID="PrvPage"></asp:HyperLink>
    <asp:HyperLink Text="下一页" NavigateUrl="#" runat="server" ID="NewxtPage"></asp:HyperLink>
    <asp:HyperLink Text="尾页" NavigateUrl="#" runat="server" ID="LastPage"></asp:HyperLink>
</div>
