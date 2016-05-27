var JSUtilities = {
    TrimByPixel: function (item, width) {
        var str = $(item).text();
        var spn = $('<span style="visibility:hidden"></span>').text(str).appendTo('body');
        spn.css("font-size", $(item).css("font-size"));
        spn.css("font-weight", $(item).css("font-weight"));
        var txt = str;
        while (spn.width() > width) {

            txt = JSUtilities.TrimOneWord(txt);
            spn.text(txt + "...");
        }
        txt = spn.text();
        document.body.removeChild(spn.get(0));
        return txt;
    },
    TrimByHeightPixel: function (item, height) {
        var str = $(item).text();
//        var spn = $('<span style="visibility:hidden"></span>').text(str).appendTo('body');
//        spn.css("font-size", $(item).css("font-size"));
//        spn.css("font-weight", $(item).css("font-weight"));
        var txt = str;
        while ($(item).height() > height) {

            txt = JSUtilities.TrimOneWord(txt);
            $(item).text(txt + "...");
        }

    },
    TrimOneWord: function (str) {
        var txt = str;
        var last = txt.lastIndexOf(" ");
        return txt.substring(0, last);
    }
}