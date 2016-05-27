<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TheGioiSanKhau.admin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 30px;border: 1px solid;margin:100px auto;width: 226px">
    <table>
    <tr>
        <td>User name:</td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Password: </td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Log on" onclick="Button1_OnClick" /></td>
    </tr>
    </table>
        <asp:Label ID="lblMsg" runat="server" Text="Dang nhap that bai!" ForeColor="Red" Visible="false"></asp:Label>
</div>
    </form>
</body>
</html>
