<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShow.ascx.cs" Inherits="TheGioiSanKhau.ctrl.SlideShow" %>
<%@ Import Namespace="TheGioiSanKhau" %>
<%if (IsIncludeScript)
  { %>
<script type='text/javascript' src='/Scripts/jquery.min.js'></script>
<script type='text/javascript' src='/scripts/jquery.mobile.customized.min.js'></script>
<script type='text/javascript' src='/scripts/jquery.easing.1.3.js'></script>
<script type='text/javascript' src='/scripts/camera.js'></script>
<link href="/Styles/camera.css" rel="stylesheet" type="text/css" />
<%} %>
<div id="<%=ID%>" class="camera_wrap">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <%if (CanBookTicket)
              { %>
            <div data-src="/AdvImage/<%#Eval("AdvImagePath") %>" data-link="<%#Eval("LinkURL") %><%#CanBooking(Eval("DisplayFrom").ToString(), Eval("GioDien").ToString())?"/Book/"+Eval("ShowId"):"" %>">
                <%}
              else
              { %>
                <div data-src="/AdvImage/<%#Eval("AdvImagePath") %>" data-link="<%#Eval("LinkURL") %>">
                    <%} %>
                    <%if (ShowCaption)
                      { %>
                        <%#CanBooking(Eval("DisplayFrom").ToString(), Eval("GioDien").ToString())?"<div class=\"fadeIn camera_caption\"><a href=\"#\" class=\"button-book-ticket\">ĐẶT VÉ</a></div>":"<div class=\"fadeIn camera_caption\"><a href=\"#\" class=\"button-book-ticket\">ĐÃ DIỄN</a></div>" %>
                    <%} %>
                </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<script>
    jQuery(function () {

        jQuery('#<%=ID%>').camera({
            height: "<%=Height%>",//435
            fx: "<%=Effect%>",
            playPause: false,
            loader: 'none',
            navigation: false,
            time: 1000
        });
    });
</script>

