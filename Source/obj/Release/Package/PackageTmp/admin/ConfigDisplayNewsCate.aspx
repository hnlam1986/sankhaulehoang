<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="ConfigDisplayNewsCate.aspx.cs" Inherits="TheGioiSanKhau.admin.ConfigDisplayNewsCate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin: 30px auto; text-align: center; width: 305px">
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Height="50px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="150px" Height="50px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width="150px" Height="50px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList4" runat="server" Width="150px" Height="50px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server" Width="200px" Height="52px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList5_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList6" runat="server" Width="100px" Height="25px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList7" runat="server" Width="100px" Height="25px" style="margin-top: 2px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
    </div>
</asp:Content>
