<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditShow.aspx.cs" Inherits="TheGioiSanKhau.admin.EditShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Style/admin.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
<link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/jquery.js"></script>
<script type="text/javascript" src="Scripts/jquery.form-validator.min.js"></script>
<script type="text/javascript" src="Scripts/jquery.datetimepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div><asp:Button runat="server" ID="btnSubmitTop"  
            CssClass="NewsSubmitButton btn btn-success btn-sm" Text="ĐĂNG TIN" 
             onclick="btnSubmitTop_Click" /></div>
    <div>
    <table>
    <tbody>
        <thead>
            <tr>
                <td>Chon poster</td>
                <td>
                    <asp:FileUpload ID="fuLogo" runat="server" /></td>
            </tr>
            <tr>
                <td>Ten Chuong Trinh</td>
                <td><asp:TextBox ID="txtName" runat="server" data-validation="required" data-validation-error-msg="Nhập vào tên chương trình"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Ngay dien</td>
                <td>
                    <asp:TextBox ID="dtStart" runat="server" ClientIDMode="Static" data-validation="required" data-validation-error-msg="Nhập vào ngày diễn"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td>Link lien ket</td>
                <td>
                    <asp:TextBox ID="txtNavigation" runat="server" data-validation="url" data-validation-error-msg="Nhập vào link lien ket. VD: http://google.com.vn"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Vi Tri</td>
                <td>
                    <asp:RadioButton ID="rdCaiLuong" runat="server"  GroupName="position" Text="Cải Lương"/>
                    <asp:RadioButton ID="rdKichNoi" runat="server" Checked="true" GroupName="position" Text="Kịch Nói"/>
                    <asp:RadioButton ID="rdCaNhac" runat="server" GroupName="position" Text="Ca Nhạc"/>
                   </td> 
            </tr>
            <tr>
                <td>Suat dien</td>
                <td><asp:TextBox ID="txtSuatDien" runat="server" data-validation="required" data-validation-error-msg="Nhập vào 1 hoặc nhiều suất diễn cách nhau bằng dấu chấm phẩy. VD: 13:00;19:00"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Gia Ve</td>
                <td><asp:TextBox ID="txtGia" runat="server" data-validation="required" data-validation-error-msg="Nhập vào 1 hoặc nhiều giá vé cách nhau bằng dấu chấm phẩy. VD: 150,000VND;200,000VND"></asp:TextBox></td>
            </tr>
            <tr>
                <td>SDT</td>
                <td><asp:TextBox ID="txtSDT" runat="server" data-validation="required" data-validation-error-msg="Nhập vào SDT"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Show len muc Chuong Trinh</td>
                <td><asp:CheckBox ID="chkShow" runat="server"></asp:CheckBox></td>
            </tr>
        </thead>
    </tbody>
    </table>
    </div>
    <div><asp:Button runat="server" ID="Button1"  
            CssClass="NewsSubmitButton btn btn-success btn-sm" Text="ĐĂNG TIN" 
             onclick="btnSubmitTop_Click" /></div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#dtStart').datetimepicker({
                closeOnDateSelect: true,
                format: 'Y/m/d',
                onShow: function (ct) {
                    this.setOptions({
                        maxDate: $('#dtEnd').val() ? $('#dtEnd').val() : false
                    });
                },
                timepicker: false
            });
            $('#dtEnd').datetimepicker({
                closeOnDateSelect: true,
                format: 'Y/m/d',
                onShow: function (ct) {
                    this.setOptions({
                        minDate: $('#dtStart').val() ? $('#dtStart').val() : false
                    });
                },
                timepicker: false
            });
        });
        $(document).ready(function () {
            $.validate({
                validateOnBlur: false, // disable validation when input looses focus
                errorMessagePosition: 'top', // Instead of 'element' which is default
                scrollToTopOnError: false // Set this property to true if you have a long form
            });
        });
    </script>
    <asp:HiddenField runat="server" ID="hdImage"/>
    </form>
</body>
</html>
