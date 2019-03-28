<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionControl.ascx.cs" Inherits="UserControls_SectionControl" %>

<asp:GridView ID="gv1" runat="server" AutoGenerateColumns="false" DataKeyNames="SectiuonId" onrowcreated="gv1_RowCreated" GridLines="None" 
>
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate><div style="width:748px;margin-right:0px;margin-left:10px;"><div style="height:30px;line-height:30px;">
                <div style="width:40px;float:left;text-align:center;">序号</div><div style="width:40px;float:left;text-align:center;">类型</div>
                <div style="width:556px;float:left;text-align:center;">更新章节</div>
                <div style="width:100px;float:left;text-align:center;">更新时间</div></div></HeaderTemplate>
                <ItemTemplate>
                       <div style="width:736px;height:24px;line-height:24px; margin-right:0px;border-bottom:1px dotted #BBB;margin-left:10px;">
                            <div style="float:left;"><asp:Label ID="lbl" runat="server" Width="40"></asp:Label></div>
                            <div style="width:40px; float:left;"><a href="#"><%# Eval("TypeName") %></a></div>
                            <div style="width:506px;float:left;"><a target="_blank" href='<%# string.Format("BookSectionsInfo.aspx?BookId={0}&&SectiuonId={1}&&inspect=0",Eval("BookId"),Eval("SectiuonId")) %>'><%# Eval("BookName") %>/<%# Eval("ValumeName")%>/<%# Eval("SectionTitle")%></a></div>
                           <div style="width:100px;float:right;"><a href="#" ><%# Eval("ShortAddTime")%></a></div>
                       </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:TemplateField>
        </Columns>
</asp:GridView>

