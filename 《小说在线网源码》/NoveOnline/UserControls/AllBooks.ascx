<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllBooks.ascx.cs" Inherits="UserControls_AllBooks" %>

<script type="text/javascript">

    function datavalied() {
        var keyCode = event.keyCode;
        if ((keyCode < 48 || keyCode > 57) && keyCode != 8 && keyCode != 13 && keyCode != 9) {
            event.returnValue = false;
        }
    }


</script>

<div id="bookselect">
    <div style="text-align: left; margin-top: 5px; height: 30px; line-height: 30px; padding-left: 5px;">
        <span style="font-weight: bold; color: Black;">小说状态：</span>
        <uc:LinkButtonEx runat="server" Value="state=0" ID="s1" Text="全部" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="state=1" ID="s2" Text="连载中" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="state=2" ID="s3" Text="已完本" OnClick="s1_Click"></uc:LinkButtonEx>
    </div>
    <div style="text-align: left; height: 30px; line-height: 30px; padding-left: 5px;">
        <span style="font-weight: bold; color: Black;">小说字数：</span>
        <uc:LinkButtonEx runat="server" Value="charnum=0" ID="c1" Text="全部" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="charnum=1" ID="c2" Text="30万以下" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="charnum=2" ID="c3" Text="30万-50万" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="charnum=3" ID="c4" Text="50万-100万" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="charnum=4" ID="c5" Text="100万-200万" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="charnum=5" ID="c6" Text="200万以上" OnClick="s1_Click"></uc:LinkButtonEx>
    </div>
    <div style="text-align: left;height: 30px; line-height: 30px;
        padding-left: 5px;">
        <span style="font-weight: bold; color: Black;">小说类型：</span>
        <uc:LinkButtonEx runat="server" Value="booktype=0" ID="t1" Text="全部" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=1" ID="t2" Text="玄幻" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=2" ID="t3" Text="奇幻" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=3" ID="t4" Text="武侠" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=4" ID="t5" Text="仙侠" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=5" ID="t6" Text="都市" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=6" ID="t7" Text="言情" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=7" ID="t8" Text="历史" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=8" ID="t9" Text="军事" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=9" ID="t10" Text="游戏" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=10" ID="t11" Text="科幻" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=11" ID="t12" Text="灵异" OnClick="s1_Click"></uc:LinkButtonEx>
        <uc:LinkButtonEx runat="server" Value="booktype=12" ID="t13" Text="竞技" OnClick="s1_Click"></uc:LinkButtonEx>
    </div>
</div>

<div>

<div style="width:948px; background-color:#F4F4F4;height:40px;line-height:40px;border:1px solid #CCCCCC;text-align:left;text-indent:7px;"  runat="server">
        
        <asp:Label ID="lblcount" runat="server"></asp:Label>
        <asp:HyperLink Text="首页" runat="server" ID="LinkFirst"></asp:HyperLink>
        <asp:HyperLink Text="上一页" runat="server" ID="LinkPrece"></asp:HyperLink>
        <asp:HyperLink Text="下一页" runat="server" ID="LinkNext"></asp:HyperLink>
        <asp:HyperLink Text="末页" runat="server" ID="LinkLast"></asp:HyperLink>
        <span> 第 </span><asp:TextBox runat="server" ID="TxpageIndex" style="width:50px;text-align:center;" onkeydown="datavalied();"></asp:TextBox><span> 页 </span> 
        <asp:LinkButton Text="跳转" runat="server" ID="linkgo" onclick="linkgo_Click"></asp:LinkButton>

</div>

<div style="border:1px solid #CCCCCC;border-top:0px;">
    <asp:GridView runat="server" ID="gvboos" AutoGenerateColumns="false" onrowcreated="gvboos_RowCreated" GridLines="Horizontal" BorderStyle="None">
        <Columns>
            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="70" HeaderStyle-Height="40" ItemStyle-Height="30">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lbrownum"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="小说类型" HeaderStyle-Width="90">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("TypeName") %>' runat="server" ID="lbname"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="小说书名/最后章节名称" HeaderStyle-Width="500">
                <ItemTemplate>
                    <asp:HyperLink Text='<%# string.Format("{0} / {1}", Eval("BookName"),Eval("LastSection")) %>' NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'
                        runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="小说字数" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="lballsum" Text='<%# Eval("AllSumClick") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="作者名称" HeaderStyle-Width="110">
                <ItemTemplate>
                    <asp:Label ID="lbusername" Text='<%# Eval("UserName") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="写做状态" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="lbwriter" Text='<%# Eval("StateName") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加时间" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="lbaddtime" Text='<%# Eval("AddTimeString") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    </div>
</div>
