using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau.admin
{
    public partial class AdvertisingManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAllImage", null);
            StringBuilder sb = new StringBuilder();
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int index = 0;
                int page_index = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (index == 0)
                    {
                        page_index++;
                        if (page_index > 1)
                        {
                            sb.Append("<table class='custom-table' id='tblAdv' data-paging='" + page_index + "' style='display:none'>");
                        }
                        else
                        {
                            sb.Append("<table class='custom-table' id='tblAdv' data-paging='" + page_index + "'>");
                        }
                        
                        sb.Append("<thead><tr>");
                        sb.Append("<td>Anh</td><td>Bat Dau</td><td>Ket Thuc</td><td>Link</td><td></td>");
                        sb.Append("</tr></thead><tbody>");
                    }
                    string id = dr["AdvID"].ToString();
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem();
                    sb.AppendLine(string.Format(template, imgPath, fromdate, todate, url, id));
                    index++;
                    if (index == 10)
                    {
                        sb.Append("</tbody></table>");
                        index = 0;
                    }
                }
                if (index < 10 && index > 0)
                {
                    sb.Append("</tbody></table>");
                    index = 0;
                }
                divAdv.InnerHtml = sb.ToString();
                divPaging.InnerHtml = Utilities.BuildPaging("pgAdv", "AdvertisingManagementEvent.SelectedPage", page_index);
            }
            
        }
    }
}