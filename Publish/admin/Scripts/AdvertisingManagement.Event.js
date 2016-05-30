﻿var AdvertisingManagementEvent = {
    DeleteAdvItem: function (id, image) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=deleteadv",
                dataType: 'text',
                data: { "id": id, img: image },
                success: function (data) {
                    if (data == 'True') {
                        $("#adv_" + id).remove();
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    DeleteShowItem: function (id, image) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=deleteshow",
                dataType: 'text',
                data: { "id": id, img: image },
                success: function (data) {
                    if (data == 'True') {
                        $("#adv_" + id).remove();
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    RefreshAdvItem: function (id) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=refreshadv",
                dataType: 'text',
                data: { "id": id },
                success: function (data) {
                    if (data != 'False') {
                        $("#adv_" + id).html(data);
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    RefreshShowItem: function (id) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=refreshshow",
                dataType: 'text',
                data: { "id": id },
                success: function (data) {
                    if (data != 'False') {
                        $("#adv_" + id).html(data);
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    AddNewAdv: function (id) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                    type: "POST",
                    url: "/ajax.aspx?action=addnewadv",
                    dataType: 'text',
                    data: { "id": id },
                    success: function(data) {
                        if (data != 'False') {
                            var html = $("#tblAdv tbody").html();
                            html = data + html;
                            $("#tblAdv tbody").get(0).innerHTML = html;
                        }
                    },
                    error: function(msg) {
                        this.result = false;
                    }
                });
        }
    },
    AddNewShow: function (id) {
        if (id) {
            $("#adv_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=addnewshow",
                dataType: 'text',
                data: { "id": id },
                success: function (data) {
                    if (data != 'False') {
                        var html = $("#tblAdv tbody").html();
                        html = data + html;
                        $("#tblAdv tbody").get(0).innerHTML = html;
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    },
    SelectedPage: function (btn, page) {
        $(".custom-table").hide();
        $(".custom-table[data-paging=" + page + "]").show();
        $(".pgGrid li a").removeClass("selected");
        $(btn).addClass("selected");
    }
}