<%@ Page Title="" Language="C#" MasterPageFile="~/TGSK.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="TheGioiSanKhau._default" %>

<%@ Register Src="ctrl/SlideShow.ascx" TagName="SlideShow" TagPrefix="uc1" %>
<%@ Register Src="ctrl/ListNewsLink.ascx" TagName="ListNewsLink" TagPrefix="uc2" %>
<%@ Register Src="ctrl/ListNewsTopImage.ascx" TagName="ListNewsTopImage" TagPrefix="uc3" %>
<%@ Register Src="ctrl/QuangCao.ascx" TagName="QuangCao" TagPrefix="uc4" %>
<%@ Register Src="ctrl/HotPhoto.ascx" TagName="HotPhoto" TagPrefix="uc5" %>
<%@ Register Src="ctrl/HotVideo.ascx" TagName="HotVideo" TagPrefix="uc6" %>
<%@ Register Src="ctrl/CongNgheCuoi.ascx" TagName="CongNgheCuoi" TagPrefix="uc7" %>
<%@ Register Src="ctrl/QuangCaoLon.ascx" TagName="QuangCaoLon" TagPrefix="uc8" %>
<%@ Register Src="ctrl/QuangCaoNgang.ascx" TagName="QuangCaoNgang" TagPrefix="uc9" %>
<%@ Register Src="ctrl/Programs.ascx" TagPrefix="uc1" TagName="Programs" %>
<%@ Register Src="~/ctrl/QuangCaoNgang.ascx" TagPrefix="uc1" TagName="QuangCaoNgang" %>
<%@ Register Src="~/ctrl/LichDien.ascx" TagPrefix="uc1" TagName="LichDien" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Programs runat="server" id="Programs" />
    <uc1:QuangCaoNgang runat="server" ID="QuangCaoNgangTren" StoreProcedureGetData="Adv_GetAnhNgangTren" Width="100%" Height="360"/>
    <uc1:LichDien runat="server" id="LichDien" />
    <uc1:QuangCaoNgang runat="server" ID="QuangCaoNgangDuoi" StoreProcedureGetData="Adv_GetAnhNgangDuoi" Width="100%" Height="360"/>
</asp:Content>
