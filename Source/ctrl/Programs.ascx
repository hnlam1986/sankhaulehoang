<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Programs.ascx.cs" Inherits="TheGioiSanKhau.ctrl.Programs" %>
<%@ Register Src="~/ctrl/SlideShow.ascx" TagPrefix="uc1" TagName="SlideShow" %>

<div class="program-main">
    <div class="program-header">
        CHƯƠNG TRÌNH
    </div>
    <div class="program-border">
        <div class="program-col">
            <div class="program-title">CẢI LƯƠNG</div>
            <div class="program-image">
                <uc1:SlideShow runat="server" ID="SlideShow" IsIncludeScript="false" StoreProcedureGetData="Show_GetAllCaiLuong" Height="435px" ShowCaption="True" CanBookTicket="True"/>
            </div>
        </div>
        <div class="program-col">
            <div class="program-title">KỊCH NÓI</div>
            <div class="program-image">
                <uc1:SlideShow runat="server" ID="SlideShow1" IsIncludeScript="false" StoreProcedureGetData="Show_GetAllKichNoi" Height="435px" ShowCaption="True" CanBookTicket="True"/>
            </div>
        </div>
        <div class="program-col">
            <div class="program-title">CA NHẠC</div>
            <div class="program-image">
                <uc1:SlideShow runat="server" ID="SlideShow2" IsIncludeScript="false" StoreProcedureGetData="Show_GetAllCaNhac" Height="435px" ShowCaption="True" CanBookTicket="True"/>
            </div>
        </div>
    </div>

</div>
