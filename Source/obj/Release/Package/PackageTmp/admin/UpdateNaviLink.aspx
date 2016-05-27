<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateNaviLink.aspx.cs" Inherits="TheGioiSanKhau.admin.UpdateNaviLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Chuyen den: 
        <asp:TextBox ID="txtchuyen" runat="server" Width="345px"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_OnClick"/>
    </div>
    
    </form>
</body>
</html>
