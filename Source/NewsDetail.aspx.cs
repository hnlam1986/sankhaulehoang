using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        public string NewsUrl { get; set; }
        string tableTemplete = @"
                <table class='{1}'>
                   {0}
                </table>
";
        string suatDienTemplete = @"
                    <tr>
                        <td><h3>SUẤT {0}</h3></td>
                    </tr>
                    <tr>
                        <td>{1}</td>
                    </tr>";

        string loaiVeRow = @"
<tr>
                        <td>{0} VND</td>
                        <td>{1}</td>
                    </tr>
";
        private string GenarateDDLSoLuong(string suat, string gia)
        {
            string selectTemplete = @"<select data-gia='{0}' data-suat='{1}' class='ticket-amount'>{2}</select>";
            string optionTemplete = @"<option value='{0}'>{0}</option>";
            string resOption = "";
            for(int i=0;i<21;i++){
                resOption += string.Format(optionTemplete, i);
            }
            return string.Format(selectTemplete,gia.Replace(",","") , suat, resOption);
        }
        private string BuildGiaVe(string suat,string gia)
        {
            string[] giave = gia.Split(';');
            string res = "";
            foreach (string item in giave)
            {
                res += string.Format(loaiVeRow, item, GenarateDDLSoLuong(suat,item));
            }
            return string.Format(tableTemplete, res,"ticket-amount-selection");
        }
        private string BuildSuatDien(string ngay, string suat, string gia)
        {
            string[] suatdien = suat.Split(';');
            string res = "";
            
            foreach (string item in suatdien)
            {
                DateTime NgayDien = Convert.ToDateTime(ngay);
                DateTime showtime = Convert.ToDateTime(item);
                showtime = NgayDien.AddHours(showtime.Hour).AddMinutes(showtime.Minute);
                if (showtime > DateTime.Now)
                {
                    string giaveformat = BuildGiaVe(item, gia);
                    res += string.Format(suatDienTemplete, item, giaveformat);
                }
            }
            return string.Format(tableTemplete, res, "timetable-detail");
        }
        private void LoadAShowById(string id)
        {
            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Show_GetShowByID", AdvID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string imgPath = dr["AdvImagePath"].ToString();
                string fromdate = dr["DisplayFrom"].ToString();
                string todate = dr["DisplayTo"].ToString();
                string url = dr["LinkURL"].ToString();
                bool cailuong = (bool)dr["CaiLuong"];
                bool kichnoi = (bool)dr["KichNoi"];
                bool canhac = (bool)dr["CaNhac"];
                string suat = dr["GioDien"].ToString();
                string gia = dr["GiaVe"].ToString();
                string sdt = dr["SDT"].ToString();
                string name = dr["ShowName"].ToString();
                lbsdt.InnerText = sdt;
                string datveFormat = BuildSuatDien(fromdate, suat, gia);
                bookTicketFrame.InnerHtml = datveFormat;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            NewsUrl = Request.Url.ToString();
            IList<string> lst = Request.GetFriendlyUrlSegments();
            if (IsPostBack)
                return;
            if (lst.Count>0)
            {
                string id = lst[1];
                if (lst.Count > 2)
                {
                    bool bookAction = lst[2].ToLower() == "book";
                    string showid = "0";
                    if (bookAction)
                    {
                        showid = lst[3];
                        LoadAShowById(showid);
                    }
                }
                else
                {
                    thanhToanve.Visible = false;
                }
                SqlParameter NewsId = new SqlParameter("NewsID", SqlDbType.Int);
                NewsId.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByNewID", NewsId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string NewsTitle = dr["NewsTitle"].ToString();
                    string NewsContent = dr["NewsContent"].ToString();
                    string SourceNews = dr["SourceNews"].ToString();
                    string catename = dr["NewsCateName"].ToString();
                    string postdate = dr["PostedDate"].ToString();
                    int cateid = int.Parse(dr["NewsCateID"].ToString());
                    string viewed = dr["Viewed"].ToString();
                    //NewsCateID = dr["NewsCateID"].ToString();
                    string ShortContent = dr["ShortContent"].ToString();
                    dvTitle.InnerText = NewsTitle;
                    this.Title = NewsTitle;
                    shortContent.InnerHtml = "<p>"+ShortContent+"</p>";
                    content.InnerHtml = NewsContent;
                    //source.InnerText = SourceNews;


                }
            }
        }
    }
}