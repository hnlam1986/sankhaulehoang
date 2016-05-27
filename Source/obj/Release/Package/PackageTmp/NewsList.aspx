<%@ Page Title="" Language="C#" MasterPageFile="~/TGSK.Master" AutoEventWireup="true"
    CodeBehind="NewsList.aspx.cs" Inherits="TheGioiSanKhau.NewsList" %>
    <%@ Register src="ctrl/BigAds.ascx" tagname="BigAds" tagprefix="uc1" %>
<%@ Import Namespace="TheGioiSanKhau" %>
<%@ Register Src="ctrl/GroupNewsTopImage.ascx" TagName="GroupNewsTopImage" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/news_item.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 10px 0;">
    </div>
    <asp:Panel ID="PanelGroupCate" runat="server" Width="725px" style="float: left">
        <asp:Repeater ID="rpGroupCate" runat="server">
            <ItemTemplate>
                <uc1:GroupNewsTopImage ID="ListNewsTopImage1" runat="server" Width="720px" StoreProcedureName="News_GetTop6NewsByCateID"
                    PositionKey='<%#Eval("NewsCateID") %>' />
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="PanelListNews" runat="server" Width="725px" style="float: left">
        <asp:Repeater ID="rpNews" runat="server">
            <ItemTemplate>
                <div style="width: 704px">
                    <div class="mt3 clearfix">
                        <a href='/NewsDetail/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>/<%#Eval("NewsID") %>'
                            title='<%#Eval("NewsTitle")%>'>
                            <img alt='<%#Eval("NewsTitle")%>' class="img130" src='/NewsAvarta/<%#Eval("ThumnailImagePath")%>'></img></a>
                        <div class="mr1">
                            <h2 style="margin: 0!important">
                                <a class="news-title" href='/NewsDetail/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>/<%#Eval("NewsID") %>'
                                    title='<%#Eval("NewsTitle")%>'>
                                    <%#Eval("NewsTitle")%></a>
                            </h2>
                            <span >
                                <%#Eval("ShortContent")%>
                            </span>
                        </div>
                    </div>
                    <div class="line1 mt1">
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </asp:Panel>
    <uc1:BigAds ID="BigAds1" runat="server" />
</asp:Content>
