<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="TheGioiSanKhau.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery.scrollbox.js"></script>
    <style>
        .scroll-img {
    border: 1px solid #FF0000;
    font-size: 0;
    height: 320px;
    overflow: hidden;
    width: 250px;
}
/*
.scroll-img ul {
    height: 600px;
    margin: 0;
    width: 700px;
}
.scroll-img ul li {
    display: inline-block;
    margin: 10px 0 10px 10px;
}
*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="demo3" class="scroll-img">
    <ul>
        <li>
            <a href="#">
                <img src="images/QCSmall.jpg"/>
            </a>
        </li>
        <li>
            <a href="#">
                <img src="images/QCSmall.jpg"/>
            </a>
        </li>
        <li>
            <a href="#">
                <img src="images/QCSmall.jpg"/>
            </a>
        </li>
        <li>
            <a href="#">
                <img src="images/QCSmall.jpg"/>
            </a>
        </li>
    </ul>
    </div>
    </form>
    <script>
        $(function() {
            $('#demo3').scrollbox({
                    switchItems: 1,
                    distance: 320
                });
        });
    </script>
</body>
</html>
