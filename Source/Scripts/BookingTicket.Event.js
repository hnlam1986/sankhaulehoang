var BookingTicketEvent = {
    Initialize: function () {
        var bookingEvent = this;
        $(".ticket-amount").on("change", function () {
            var ticket = 0;
            var price = 0;
            $(".ticket-amount").each(function () {
                ticket = ticket + parseInt($(this).val());
                price = price + (parseInt($(this).val()) * parseInt($(this).attr("data-gia")));
            });
            $("#ticketAmount").text(ticket);
            $("#totalPrice").text(price);

        });
        //------------------------------
        $("#myModal").on('show.bs.modal', function () {
            $("#tableTicket").html(bookingEvent.GenarateTicketInfo());
            $("#dialogSL").text($("#ticketAmount").text());
            $("#dialogTT").text($("#totalPrice").text());
            $("#indicator").css("display", "none");
            $("#btnBooking").removeAttr("disabled");
        });
        
    },
    GenarateTicketInfo: function () {
        var tableTemplate = "<table class='booking-table'>"+
              "<tr class='tbl-head'>"+
                  "<td>Suất diễn</td><td>Giá vé</td><td>Số lượng</td>"+
              "</tr>"
        var detail = "";
        $(".ticket-amount").each(function () {
            if (parseInt($(this).val()) > 0) {
                var suat, gia, sl = "";
                suat = $(this).attr("data-suat");
                gia = $(this).attr("data-gia");
                sl = $(this).val();
                detail += "<tr>" +
                  "<td>"+suat+"</td><td>"+gia+"</td><td>"+sl+"</td>" +
              "</tr>"
            }
        });
        return tableTemplate + detail + "</table>";
    },
    SubmitBooking: function () {
        var valid = true;
        if ($("#txtName").val() == "") {
            $("#txtName").addClass("input-error");
            valid = false;
        } else {
            $("#txtName").removeClass("input-error");
        }

        if ($("#txtPhone").val() == "") {
            $("#txtPhone").addClass("input-error");
            valid = false;
        } else {
            $("#txtPhone").removeClass("input-error");
        }
        if (valid) {
            $("#indicator").css("display", "");
            $("#btnBooking").attr("disabled", "disabled");
            var content = $("#thongtindatve").html();
            content += '<div>Thông tin người đặt:</div>'+
                            '<div class="line-item">'+
                                '<span>Họ tên: </span>'+
                                '<span>' + $("#txtName").val() + '</span>' +
                            '</div>'+

                            '<div class="line-item">'+
                                '<span>Số điện thoại: </span>'+
                                '<span>' + $("#txtPhone").val() + '</span>' +
                            '</div>'
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=sendemail",
                dataType: 'text',
                data: { "content": content},
                success: function (data) {
                    if (data == 'True') {
                        $("#myModal").modal("hide");
                    } else {
                        var sdt = $(".sdt").text();
                        var error = "<div class='booking-unsuccess'>" +
                            "<h2>Đặt vé không thành công!</h2><br/><p>Yêu cầu đặt vé của bạn không thành công, xin vui lòng thử lại. Hoặc gọi đến số <h4>" + sdt + "</h4> để đặt vé qua điện thoại. Thành thật xin lỗi về sự bất tiện này!</p>"
                            "</div>"
                            $("#confirmMain").html(error);
                            $("#indicator").css("display", "none");
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    }
}