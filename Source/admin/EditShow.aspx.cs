using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau.admin
{
    public partial class EditShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "edit")
            {
                LoadAdvertising();
                string id = Request.QueryString["id"];
                if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"] == "false")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "TheGioiSanKhauUpdateAdv",
                           "$(window.opener.document.getElementById('refresh_adv_" + id + "')).get(0).click();", true);
                }
                else if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"] == "true")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "TheGioiSanKhauUpdateAdv",
                           "$(window.opener.document.getElementById('hidNewAdvId')).val('" + id + "');" +
                           "$(window.opener.document.getElementById('advNewRefresh')).get(0).click();", true);
                }
            }
        }

        protected void btnSubmitTop_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "new")
                {
                    InsertAdvertising();
                }
                else
                {
                    if (Request.QueryString["id"] != null)
                    {
                        UpdateAdvertising();
                    }
                }

            }
        }

        private void InsertAdvertising()
        {
            SqlParameter AdvImagePath = new SqlParameter("AdvImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }
            AdvImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter DisplayFrom = new SqlParameter("DisplayFrom", SqlDbType.DateTime);
            DisplayFrom.Value = DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value = DateTime.Parse(dtStart.Text).AddHours(23).AddMinutes(59);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;

           

            SqlParameter CaiLuong = new SqlParameter("CaiLuong", SqlDbType.Bit);
            CaiLuong.Value = false;
            SqlParameter KichNoi = new SqlParameter("KichNoi", SqlDbType.Bit);
            KichNoi.Value = false;
            SqlParameter CaNhac = new SqlParameter("CaNhac", SqlDbType.Bit);
            CaNhac.Value = false;

            SqlParameter SuatDien = new SqlParameter("SuatDien", SqlDbType.VarChar);
            SuatDien.Value = txtSuatDien.Text;
            SqlParameter GiaVe = new SqlParameter("GiaVe", SqlDbType.VarChar);
            GiaVe.Value = txtGia.Text;
            SqlParameter SDT = new SqlParameter("SDT", SqlDbType.VarChar);
            SDT.Value = txtSDT.Text;
            SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
            Name.Value = txtName.Text;
            SqlParameter SlideShow = new SqlParameter("SlideShow", SqlDbType.Bit);
            SlideShow.Value = chkShow.Checked;
            
            
            if (rdCaiLuong.Checked)
            {
                CaiLuong.Value = true;
            }
            else if (rdKichNoi.Checked)
            {
                KichNoi.Value = true;
            }
            else if (rdCaNhac.Checked)
            {
                CaNhac.Value = true;
            }


            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Direction = ParameterDirection.Output;
            string res = DataAccessLayer.ExcuteNoneQueryHasOutput("Show_InsertShow", "AdvID", AdvImagePath, DisplayFrom, DisplayTo,
                                                       LinkURL, AdvID, CaiLuong, KichNoi, CaNhac, SuatDien, GiaVe, SDT, Name, SlideShow);
            if (!string.IsNullOrEmpty(res))
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    fuLogo.SaveAs(Server.MapPath("/AdvImage/" + guid));
                }
                Response.Redirect("EditShow.aspx?action=edit&reload=true&id=" + res,
                             false);
            }
        }

        private void UpdateAdvertising()
        {
            string id = Request.QueryString["id"];
            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Value = id;
            SqlParameter AdvImagePath = new SqlParameter("AdvImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }
            AdvImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter DisplayFrom = new SqlParameter("DisplayFrom", SqlDbType.DateTime);
            DisplayFrom.Value = DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value = DateTime.Parse(dtStart.Text).AddHours(23).AddMinutes(59);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;


            SqlParameter CaiLuong = new SqlParameter("CaiLuong", SqlDbType.Bit);
            CaiLuong.Value = false;
            SqlParameter KichNoi = new SqlParameter("KichNoi", SqlDbType.Bit);
            KichNoi.Value = false;
            SqlParameter CaNhac = new SqlParameter("CaNhac", SqlDbType.Bit);
            CaNhac.Value = false;

            SqlParameter SuatDien = new SqlParameter("SuatDien", SqlDbType.VarChar);
            SuatDien.Value = txtSuatDien.Text;
            SqlParameter GiaVe = new SqlParameter("GiaVe", SqlDbType.VarChar);
            GiaVe.Value = txtGia.Text;
            SqlParameter SDT = new SqlParameter("SDT", SqlDbType.VarChar);
            SDT.Value = txtSDT.Text;
            SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
            Name.Value = txtName.Text;
            SqlParameter SlideShow = new SqlParameter("SlideShow", SqlDbType.Bit);
            SlideShow.Value = chkShow.Checked;

            if (rdCaiLuong.Checked)
            {
                CaiLuong.Value = true;
            }
            else if (rdKichNoi.Checked)
            {
                KichNoi.Value = true;
            }
            else if (rdCaNhac.Checked)
            {
                CaNhac.Value = true;
            }

            bool res = DataAccessLayer.ExcuteNoneQuery("Show_UpdateShow", AdvID, AdvImagePath, DisplayFrom,
                                                       DisplayTo,
                                                       LinkURL, CaNhac, CaiLuong, KichNoi, SuatDien, GiaVe, SDT, Name, SlideShow);
            if (res)
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {

                    fuLogo.SaveAs(Server.MapPath("/AdvImage/" + guid));
                    if (File.Exists(Server.MapPath("/AdvImage/" + hdImage.Value)))
                    {
                        File.Delete(Server.MapPath("/AdvImage/" + hdImage.Value));
                    }
                    hdImage.Value = fuLogo.FileName;
                }
                Response.Redirect("EditShow.aspx?action=edit&reload=false&id=" + id,
                             false);

            }
        }

        private void LoadAdvertising()
        {
            string id = Request.QueryString["id"];
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
                bool show = (bool)dr["SlideShow"];
                string suat = dr["GioDien"].ToString();
                string gia = dr["GiaVe"].ToString();
                string sdt = dr["SDT"].ToString();
                string name = dr["ShowName"].ToString();
                
                hdImage.Value = imgPath;
                dtStart.Text = DateTime.Parse(fromdate).ToString("yyyy/MM/dd");
                txtNavigation.Text = url;
               
                rdCaiLuong.Checked = cailuong;
                rdKichNoi.Checked = kichnoi;
                rdCaNhac.Checked = canhac;
                txtSuatDien.Text = suat;
                txtGia.Text = gia;
                txtSDT.Text = sdt;
                txtName.Text = name;
                chkShow.Checked = show;
            }
        }
    }
}