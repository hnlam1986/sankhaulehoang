using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNet.FriendlyUrls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau
{
    public static class Utilities
    {
        public static string GetTempleAdvItem(bool isRefresh = false)
        {
            string template = "<td>" +
                        "<img src='/AdvImage/{0}' style='max-width:100px'/>" +
                        "</td>" +
                        "<td>{1}</td>" +
                        "<td>{2}</td>" +
                        "<td><a href=\"javascript:void(0);\" onclick=\"window.open('{3}');\">{3}</a></td>" +
                        "<td>" +
                        "<button type=\"button\" class=\"btn btn-warning btn-sm\" onclick=\"var popup = window.open('EditAdvertising.aspx?action=edit&id={4}','EditAdvWindow','toolbar=no, scrollbars=yes, resizable=yes, width=450px, height=450px');popup.focus();\">Edit</button>" +
                        "<button type=\"button\" class=\"btn btn-danger btn-sm\" onclick=\"AdvertisingManagementEvent.DeleteAdvItem('{4}','/AdvImage/{0}')\" style='margin-left:5px;'>Del</button>" +
                        "<img src='img/throbber.gif' id='adv_indicator_{4}' style='display:none'/>" +
                        "<a href=\"javascript:void(0);\" onclick=\"AdvertisingManagementEvent.RefreshAdvItem('{4}')\" id='refresh_adv_{4}' style='display:none'>Refresh</a>" +
                        "</td>";
            if(!isRefresh)
            {
                
                 template =    "<tr id='adv_{4}'>" + template+ "</tr>";
            }
            return template;
        }
        public static string GetTempleShowItem(bool isRefresh = false)
        {
            string template = "<td>" +
                        "<img src='/AdvImage/{0}' style='max-width:100px'/>" +
                        "</td>" +
                        "<td>{4}</td>" +
                        "<td>{1}</td>" +
                        "<td>{5}</td>" +
                        "<td>{6}</td>" +
                        "<td><a href=\"javascript:void(0);\" onclick=\"window.open('{2}');\">{2}</a></td>" +
                        "<td>" +
                        "<button type=\"button\" class=\"btn btn-warning btn-sm\" onclick=\"var popup = window.open('EditShow.aspx?action=edit&id={3}','EditShowWindow','toolbar=no, scrollbars=yes, resizable=yes, width=450px, height=450px');popup.focus();\">Edit</button>" +
                        "<button type=\"button\" class=\"btn btn-danger btn-sm\" onclick=\"AdvertisingManagementEvent.DeleteShowItem('{3}','/AdvImage/{0}')\" style='margin-left:5px;'>Del</button>" +
                        "<img src='img/throbber.gif' id='adv_indicator_{3}' style='display:none'/>" +
                        "<a href=\"javascript:void(0);\" onclick=\"AdvertisingManagementEvent.RefreshShowItem('{3}')\" id='refresh_adv_{3}' style='display:none'>Refresh</a>" +
                        "</td>";
            if (!isRefresh)
            {

                template = "<tr id='adv_{3}'>" + template + "</tr>";
            }
            return template;
        }
        public static string GetFriendlyUrl(HttpRequest request,string page,params object[] id)
        {
            string link = FriendlyUrl.Href(page, id);
            return link;
        }
        public static string ConvertUnicodeToAscii(string uText)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = unicode.GetBytes(uText);
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            char[] un_safe = ("$&+,/:;=?@<>#%{}|^~[]`.\"'").ToCharArray();
            char[] filter = (from item in asciiChars where !un_safe.Contains(item) select item).ToArray();
            string asciiString = new string(filter).Replace(" ", "-");
            return asciiString;
        }
        public static void WriteLog(string logDes)
        {
            SqlParameter param = new SqlParameter("LogDes",logDes);
            DataAccessLayer.ExcuteNoneQuery("Logs_InsertLog", param);
        }
    }
}