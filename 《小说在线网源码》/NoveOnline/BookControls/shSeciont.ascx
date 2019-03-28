<%@ Control Language="C#" AutoEventWireup="true" CodeFile="shSeciont.ascx.cs" Inherits="BookControls_shSeciont" %>

<div>
    <asp:GridView runat="server" ID="gvseex" AutoGenerateColumns="false" 
        onrowcreated="gvseex_RowCreated" GridLines="Horizontal" BorderStyle="None">
        <Columns>
        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="100px" HeaderStyle-Height="50px">
            <ItemTemplate>
                <asp:Label runat="server" ID="lbnumber"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="BookName" HeaderText="小说名称" HeaderStyle-Width="200px" ItemStyle-Height="30px"/>
            <asp:BoundField DataField="ValumeName" HeaderText="所属卷名称" HeaderStyle-Width="200px"/>
            <asp:BoundField DataField="SectionTitle" HeaderText="章节标题" HeaderStyle-Width="400px"/>
            <asp:BoundField DataField="CharNum" HeaderText="章节字数" HeaderStyle-Width="100px"/>
            <asp:BoundField DataField="ShortAddTime" HeaderText="添加时间" HeaderStyle-Width="100px"/>
        </Columns>
    
    </asp:GridView>
    
</div>