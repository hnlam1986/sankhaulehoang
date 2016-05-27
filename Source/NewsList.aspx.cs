using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau
{
    public partial class NewsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> lst= Request.GetFriendlyUrlSegments();
            if (IsPostBack)
                return;
            if (lst.Count>=2)
            {
                string id = lst[1];
                string parent = lst[0];
                if (parent == "0")
                {
                    PanelGroupCate.Visible = true;
                    PanelListNews.Visible = false;
                    GetAllCateByParentID(id);
                }
                else
                {
                    PanelGroupCate.Visible = false;
                    PanelListNews.Visible = true;
                    GetListNews(id);
                }
            }
        }
        private void GetAllCateByParentID(string ID)
        {
            SqlParameter key = new SqlParameter("CateID", ID);
            DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetAllGroupByCateID", key);
            rpGroupCate.DataSource = ds;
            rpGroupCate.DataBind();
            
        }

        private void GetListNews(string ID)
        {
            SqlParameter cateId = new SqlParameter("NewsCateID", SqlDbType.Int);
            cateId.Value = ID;
            DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByCategoryId", cateId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rpNews.DataSource = ds;
                rpNews.DataBind();
            }
        }
    }
}