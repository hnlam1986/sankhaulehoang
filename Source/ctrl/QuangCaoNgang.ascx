<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuangCaoNgang.ascx.cs"
    Inherits="TheGioiSanKhau.ctrl.QuangCaoNgang" %>
<div style="position: relative; width: <%=Width%>;overflow: hidden;">
    <div style="margin-top: 10px">
        <div id="<%=ID %>" class="vertical-scroll-img" style="height:<%=Height%>px;">
            <ul>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <li><a href="<%#Eval("LinkURL")%>">
                            <img style="width: 960px" src="/AdvImage/<%#Eval("AdvImagePath")%>" />
                        </a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
<script>
    $(function () {
        $('#<%=ID %>').scrollbox({
            switchItems: 1,
            distance: <%=Height%>
        });
    });
</script>