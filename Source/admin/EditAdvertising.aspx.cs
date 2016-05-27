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
    public partial class EditAdvertising : System.Web.UI.Page
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
                 else if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"]=="true")
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
            DisplayFrom.Value =DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value = DateTime.Parse(dtEnd.Text);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;
           

            SqlParameter Banner = new SqlParameter("Banner", SqlDbType.Bit);
            Banner.Value = false;
            SqlParameter Tren = new SqlParameter("Tren", SqlDbType.Bit);
            Tren.Value = false;
            SqlParameter HaiBen = new SqlParameter("Haiben", SqlDbType.Bit);
            HaiBen.Value = false;
            SqlParameter Duoi = new SqlParameter("Duoi", SqlDbType.Bit);
            Duoi.Value = false;

            if(rdBanner.Checked)
            {
                Banner.Value = true;
            }else if(rdTren.Checked)
            {
                Tren.Value = true;
            }else if (rdFloat.Checked)
            {
                HaiBen.Value = true;
            }else if(rdDuoi.Checked)
            {
                Duoi.Value = true;
            }
            

            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Direction = ParameterDirection.Output;
            string res = DataAccessLayer.ExcuteNoneQueryHasOutput("Adv_InsertAdv", "AdvID", AdvImagePath, DisplayFrom, DisplayTo,
                                                       LinkURL, AdvID,HaiBen,Banner, Tren, Duoi);
            if (!string.IsNullOrEmpty(res))
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    fuLogo.SaveAs(Server.MapPath("/AdvImage/" + guid));
                }
                Response.Redirect("EditAdvertising.aspx?action=edit&reload=true&id=" + res,
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
            DisplayFrom.Value =DateTime.Parse(dtStart.Text);
            SqlParameter DisplayTo = new SqlParameter("DisplayTo", SqlDbType.DateTime);
            DisplayTo.Value =DateTime.Parse( dtEnd.Text);
            SqlParameter LinkURL = new SqlParameter("LinkURL", SqlDbType.VarChar);
            LinkURL.Value = txtNavigation.Text;


            SqlParameter Banner = new SqlParameter("Banner", SqlDbType.Bit);
            Banner.Value = false;
            SqlParameter Tren = new SqlParameter("Tren", SqlDbType.Bit);
            Tren.Value = false;
            SqlParameter HaiBen = new SqlParameter("Haiben", SqlDbType.Bit);
            HaiBen.Value = false;
            SqlParameter Duoi = new SqlParameter("Duoi", SqlDbType.Bit);
            Duoi.Value = false;
            
            if (rdBanner.Checked)
            {
                Banner.Value = true;
            }
            else if (rdTren.Checked)
            {
                Tren.Value = true;
            }
            else if (rdFloat.Checked)
            {
                HaiBen.Value = true;
            }
            else if (rdDuoi.Checked)
            {
                Duoi.Value = true;
            }
           

            bool res = DataAccessLayer.ExcuteNoneQuery("Adv_UpdateAdv", AdvID, AdvImagePath, DisplayFrom,
                                                       DisplayTo,
                                                       LinkURL, HaiBen,Tren, Duoi, Banner);
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
                Response.Redirect("EditAdvertising.aspx?action=edit&reload=false&id=" + id,
                             false);
                
            }
        }

        private void LoadAdvertising()
        {
            string id = Request.QueryString["id"];
            SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
            AdvID.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string imgPath = dr["AdvImagePath"].ToString();
                string fromdate = dr["DisplayFrom"].ToString();
                string todate = dr["DisplayTo"].ToString();
                string url = dr["LinkURL"].ToString();
                bool tren = (bool)dr["TrangChuTren"];
                bool duoi = (bool)dr["TrangChuDuoi"];
                bool banner = (bool)dr["SlideShow"];
                bool haiben = (bool)dr["TwoFloat"];
                hdImage.Value = imgPath;
                dtStart.Text =DateTime.Parse(fromdate).ToString("yyyy/MM/dd");
                dtEnd.Text = DateTime.Parse(todate).ToString("yyyy/MM/dd");
                txtNavigation.Text = url;
                rdBanner.Checked = banner;
                rdTren.Checked = tren;
                rdDuoi.Checked = duoi;
                rdFloat.Checked = haiben;
            }
        }
    }
}