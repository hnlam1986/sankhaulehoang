<%@ Page Title="" Language="C#" MasterPageFile="~/TGSK.Master" AutoEventWireup="true"
    CodeBehind="NewsDetail.aspx.cs" Inherits="TheGioiSanKhau.NewsDetail" %>

<%@ Register Src="ctrl/ListNewsLink.ascx" TagName="ListNewsLink" TagPrefix="uc2" %>
<%@ Register Src="ctrl/BigAds.ascx" TagName="BigAds" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/admin/Scripts/jquery.form-validator.min.js"></script>
    <script type="text/javascript" src="/Scripts/BookingTicket.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt10">

        <div id="news_content" class="mt15" style="width: 718px;">
            <div id="dvTitle" runat="server" class="news-title">
            </div>
            
            <div class="bookTicketFrame" id="bookTicketFrame" runat="server">
            </div>
            <div id="thanhToanve" runat="server">
                <div><span>Số lượng vé: </span><span id="ticketAmount"></span></div>
                <div><span>Thanh toán: </span><span id="totalPrice"></span></div>
                <div>
                    <input type="button" value="ĐẶT VÉ" data-toggle="modal" data-target="#myModal" />
                </div>
                <div>Hoặc gọi đến số <h2><label id="lbsdt" runat="server" class="sdt"></label></h2> để đặt vé qua điện thoại.</div>
            </div>
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">XÁC THỰC ĐẶT VÉ</h4>
                        </div>
                        <div class="modal-body" id="confirmMain">
                            <div id="thongtindatve">
                                <div>Thông tin vé:</div>
                                <div id="tableTicket"></div>
                                <div>
                                    <div><span>Số lượng vé: </span><span id="dialogSL"></span></div>
                                    <div><span>Thanh toán: </span><span id="dialogTT"></span></div>
                                </div>
                            </div>
                            <div>Thông tin người đặt:</div>
                            <div class="line-item">
                                <span>Họ tên: </span>
                                <input type="text" id="txtName" data-validation="required" data-validation-error-msg="Nhập vào họ tên" />
                            </div>

                            <div class="line-item">
                                <span>Số điện thoại: </span>
                                <input type="text" id="txtPhone" data-validation="required" data-validation-error-msg="Nhập vào số điện thoại" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <img src="/img/loading.gif" style="display: none" id="indicator" />
                            <button type="button" class="btn btn-default" onclick="BookingTicketEvent.SubmitBooking();" id="btnBooking">ĐẶT VÉ</button>
                        </div>
                    </div>

                </div>
            </div>
            <div id="shortContent" runat="server" style="font-weight: bold">
            </div>
            <div id="content" runat="server">
            </div>
            <%--<div id="source" runat="server"></div>--%>
        </div>

        <script>
            $(document).ready(function () {
                $("span.cut-title").each(function () {
                    var trim = JSUtilities.TrimByPixel(this, 320);
                    $(this).text(trim);
                });

                //-------------------------
                BookingTicketEvent.Initialize();
            });
        </script>
    </div>
</asp:Content>
