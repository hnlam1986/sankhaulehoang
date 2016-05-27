<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="NewsEditor.aspx.cs" Inherits="TheGioiSanKhau.admin.NewsEditor" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery.js"></script>
    <link href="Style/Site.css" rel="stylesheet" type="text/css" /><link href="Style/admin.css" rel="stylesheet" type="text/css" />
  <%--  <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="Style/admin.css" rel="stylesheet" type="text/css" />
<%--    <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="tinymce/tinymce.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.form-validator.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "#txtTitle.editable",
            inline: true,
            toolbar: "undo redo",
            menubar: false
        });
        tinymce.init({
            selector: "#txtShortText.editable",
            inline: true,
            toolbar: "undo redo",
            menubar: false
        });
        tinymce.init({
            selector: "div.editable",
            theme: "modern",
            filemanager_title: "File Manager",
            plugins: [
                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code fullscreen",
                "insertdatetime nonbreaking save table contextmenu directionality",
                "emoticons template paste textcolor filemanager"
            ],
            toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            toolbar2: "print preview media | forecolor backcolor emoticons",
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ]
        });
    </script>
    <style type="text/css">
        .mce-open[aria-label="Upload image"]
        {
            height: 28px !important;
        }
        body
        {
            background-color: #ffffff!important;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin: 10px;">
        <div>
        <asp:Button runat="server" ID="btnSubmitTop" CssClass="NewsSubmitButton btn btn-success btn-sm"
            Text="ĐĂNG TIN" OnClientClick="SubmitNews();" OnClick="btnSaveTin_OnClick" /></div>
    <table width="100%">
        <tr>
            <td>
                <h1>
                    Nhập vào tiêu đề</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtTitle" Width="100%" data-validation="required"
                    data-validation-error-msg="Nhập vào tiêu đề"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào phần tin vắn</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtShortText" Width="100%" data-validation="required" TextMode="MultiLine" Height="50px"
                    data-validation-error-msg="Nhập vào phần tin vắn"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Chọn ảnh đại diện</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:FileUpload runat="server" ID="fuAvarta" />
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào nguồn tin</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtSource" Width="100%" data-validation="required"
                    data-validation-error-msg="Nhập vào nguồn tin"></asp:TextBox>
            </td>
        </tr>
        <div id="divCategory" runat="server">
            <tr>
                <td>
                    <h1>
                        Thuộc danh mục tin</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCategory" Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
        </div>
        <tr>
            <td>
                <asp:CheckBox runat="server" ID="chkSlide" Text="Slide Show" CssClass="mr30"/>
                <asp:CheckBox runat="server" ID="chkHotNews" Text="Tin Hot" CssClass="mr30"/>
                <asp:CheckBox runat="server" ID="chkMostReview" Text="Tin Xem Nhiều" />
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào nội dung bài viết</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editable" style="width: 100%; height: 550px" id="divContain">
                    Nhập vào nội dung bài viết</div>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hidContent" ClientIDMode="Static" />
    <div>
        <asp:Button runat="server" ID="btnSaveTin" CssClass="NewsSubmitButton btn btn-success btn-sm"
            Text="ĐĂNG TIN" OnClientClick="SubmitNews();" OnClick="btnSaveTin_OnClick" /></div>
    <script type="text/javascript">
        function SubmitNews() {
            $("#hidContent").val($("#divContain_ifr").contents().find("body").html());
        }

        $(document).ready(function () {
            $.validate({
                validateOnBlur: false, // disable validation when input looses focus
                errorMessagePosition: 'top', // Instead of 'element' which is default
                scrollToTopOnError: false // Set this property to true if you have a long form
            });
            if ($("#hidContent").val() != "")
                $("#divContain").html($("#hidContent").val());
        });
    </script>
    <asp:HiddenField runat="server" ID="hdImage" />
    </form>
</body>
</html>
