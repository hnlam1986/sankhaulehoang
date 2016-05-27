<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsLink.ascx.cs" Inherits="TheGioiSanKhau.ctrl.ListNewsLink" %>
<%@ Import Namespace="TheGioiSanKhau" %>
<div style="position: relative;height: <%=Height%>">
 <div id="titleBar" class="link-news-title-bar">
    <div class="title-frame">
        <div class="title-left bg-yellow"><%=Title %></div>
        <div class="title-arrow">
            <div class="yellow-arrow"><img src="/images/yellow-arrow.jpg" style="margin-left: -9px"/></div>
        </div>
    </div>
    <div class="title-right bg-grey"></div>
 </div>
 <div style="margin-top: 5px">
     <asp:Repeater ID="Repeater1" runat="server">
     
        <ItemTemplate>
            <a style="display: block" class="link-news-separate" href="/NewsDetail/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>/<%#Eval("NewsID") %>"><div class="bullet"></div><span class="cut-title"><%#Eval("NewsTitle") %></span></a>
        </ItemTemplate>
     </asp:Repeater>
 </div>
</div>