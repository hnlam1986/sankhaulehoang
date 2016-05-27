using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau
{
    public partial class TGSK : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetFloatAdv");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string img1 = "";
                string link1 = "";
                string img2 = "";
                string link2 = "";
                
                img1 = ds.Tables[0].Rows[0]["AdvImagePath"].ToString();
                link1 = ds.Tables[0].Rows[0]["LinkURL"].ToString();
                ImageButtonleft.ImageUrl = "/AdvImage/" + img1;
                ImageButtonleft.PostBackUrl = link1;
                qcLeft.Visible = true;
                if (ds.Tables[0].Rows.Count >= 2)
                {
                    img2 = ds.Tables[0].Rows[1]["AdvImagePath"].ToString();
                    link2 = ds.Tables[0].Rows[1]["LinkURL"].ToString();
                    
                    ImageButtonRight.ImageUrl = "/AdvImage/" + img2;
                    ImageButtonRight.PostBackUrl = link2;
                    qcRight.Visible = true;
                }

            }
        }
    }
}