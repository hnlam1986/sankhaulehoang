﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true" CodeBehind="ShowManagement.aspx.cs" Inherits="TheGioiSanKhau.admin.ShowManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/AdvertisingManagement.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <input type="button" id="btnSubmitTop" class="NewsSubmitButton btn btn-success btn-sm" value="THEM SHOW DIEN" onclick="var popup = window.open('EditShow.aspx?action=new', 'EditShowWindow', 'toolbar=no, scrollbars=yes, resizable=yes, width=550px, height=350px'); popup.focus();" /></div>
    <input type="hidden" id="hidNewAdvId" />
    <a href="javascript:void(0);" onclick="var value=$('#hidNewAdvId').val(); AdvertisingManagementEvent.AddNewShow(value);" id="advNewRefresh" style="display: none">Refresh</a>
    <div id="divAdv" runat="server"></div>
    <div>
        <input type="button" id="Button1" class="NewsSubmitButton btn btn-success btn-sm" value="THEM SHOW DIEN" onclick="var popup = window.open('EditShow.aspx?action=new', 'EditShowWindow', 'toolbar=no, scrollbars=yes, resizable=yes, width=550px, height=350px'); popup.focus();" /></div>
</asp:Content>
