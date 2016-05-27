using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;
using TheGioiSanKhau.entity;

namespace TheGioiSanKhau.ctrl
{
    public partial class LichDien : System.Web.UI.UserControl
    {
        private string TimeTableHeadTemplate = @"
                                                <table width='100%'>
                                                    <thead>
                                                        <tr>
                                                            <th>NGÀY</th>
                                                            <th>GIỜ</th>
                                                            <th>TÊN CHƯƠNG TRÌNH</th>
                                                            <th>THỂ LOẠI</th>
                                                            <th>GIÁ VÉ</th>
                                                            <th>LIÊN HỆ ĐẶT VÉ</th>
                                                            <th>ĐẶT VÉ ONLINE</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        {0}
                                                    </tbody>
                                                </table>
                                                    ";
        private string TimeTableRowTemplate = @"
                                                <tr>
                                                    <td>{0}</td>
                                                    <td>{1}</td>
                                                    <td>{2}</td>
                                                    <td>{3}</td>
                                                    <td>{4}</td>
                                                    <td>{5}</td>
                                                    <td><a href='{6}'>Đặt vé</a></td>
                                                </tr>
                                                ";
        private string TimeTableRowTemplateNotBooking = @"
                                                <tr class='show-da-dien'>
                                                    <td>{0}</td>
                                                    <td>{1}</td>
                                                    <td>{2}</td>
                                                    <td>{3}</td>
                                                    <td>{4}</td>
                                                    <td>{5}</td>
                                                    <td>Đã diễn</td>
                                                </tr>
                                                ";
        protected void Page_Load(object sender, EventArgs e)
        {
            List<TimeTable> lst = new List<TimeTable>();
            DataSet ds = DataAccessLayer.ExecuteDataSet("Show_GetAvailableShow");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string fromdate = dr["DisplayFrom"].ToString();
                    string todate = dr["DisplayTo"].ToString();
                    string url = dr["LinkURL"].ToString();
                    string id = dr["ShowId"].ToString();
                    bool cailuong = (bool)dr["CaiLuong"];
                    bool kichnoi = (bool)dr["KichNoi"];
                    bool canhac = (bool)dr["CaNhac"];
                    string theloai = "Cải Lương";
                    if (kichnoi)
                    {
                        theloai = "Kịch Nói";
                    }
                    else if (canhac)
                    {
                        theloai = "Ca Nhạc";
                    }
                    else if (cailuong)
                    {
                        theloai = "Cải Lương";
                    }
                    string suat = dr["GioDien"].ToString();
                    string gia = dr["GiaVe"].ToString();
                    string sdt = dr["SDT"].ToString();
                    string name = dr["ShowName"].ToString();
                    string[] shows = suat.Split(';');

                    foreach (string item in shows)
                    {
                        TimeTable show = new TimeTable();
                        show.DenNgay = Convert.ToDateTime(todate);
                        show.TuNgay = Convert.ToDateTime(fromdate);
                        show.GiaVe = gia;
                        show.SDT = sdt;
                        show.ShowName = name;
                        show.Link = url;
                        show.ShowId = id;
                        show.SuatDien = Convert.ToDateTime(item);
                        show.TheLoai = theloai;
                        show.NgayDien = Convert.ToDateTime(fromdate);

                        lst.Add(show);

                    }

                }
            }
            lst = lst.OrderBy(o => o.NgayDien).ToList();
            string rows = "";
            foreach (TimeTable item in lst)
            {
                rows += string.Format(item.CanBooking ? TimeTableRowTemplate : TimeTableRowTemplateNotBooking,
                    item.NgayDien.ToString("dd/MM/yyyy"),
                    item.SuatDien.ToString("HH:mm"),
                    item.ShowName,
                    item.TheLoai,
                    item.GiaVe.Replace(";", "<br/>"),
                    item.SDT,
                    item.Link + "/Book/" + item.ShowId);
            }
            string full = string.Format(TimeTableHeadTemplate, rows);
            gridShowTime.InnerHtml = full;
        }


    }
}