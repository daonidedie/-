<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VisitAuthor.ascx.cs" Inherits="UserControls_VisitAuthor" %>

<asp:Repeater runat="server" ID="repVisitAuthor">
    <ItemTemplate>
        <div style="float:left;width:150px;height:156px;text-align:left;padding-top:10px;border-bottom:1px dotted #BBB;padding-left:10px;margin-left:20px;">
            <div><asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/Images/AuthorFace/{0}",Eval("UsetImage"))  %>' Width="120px" Height="130px" /></div>
            <div style="margin-top:5px;padding-left:12px;">作家：<%# Eval("UserName")%></div>
               
        </div>
        <div style="float:left;width:495px;height:30px;border-bottom:1px dotted #BBB;">
            <div>
                <div style="float:left;width:390px;height:30px;line-height:30px;text-align:center;">
                  <%# Eval("VisitTitle")%>
                </div>
                <div style="float:left;width:85px;height:30px;line-height:30px;text-align:center;">
                <%# Eval("VisitDateString")%>
                </div>
            </div>
        </div>
        <div style="float:left;width:495px;border-bottom:1px dotted #BBB;">
            <div style="height:95px;margin-top:10px;">
                <div style="line-height:17px;">&nbsp;&nbsp;&nbsp;&nbsp;<%#  Eval("Contents").ToString().Length > 258? Eval("Contents").ToString().Substring(0,258)+"……" : Eval("Contents").ToString() %></div>
            </div>
            <div style="height:30px;text-align:right;padding-right:10px;line-height:30px;">
              
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
