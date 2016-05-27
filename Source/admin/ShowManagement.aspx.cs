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
    public partial class ShowManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Show_GetAllShow", null);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='custom-table' id='tblAdv'>");
            sb.Append("<thead><tr>");
            sb.Append(@"<td>Anh</td>
                        <td>Chuong trinh</td>
                        <td>Ngay dien</td>
                        <td>Gio</td>
                        <td>Gia ve</td>
                        <td>Link</td>
                        <td></td>");
            sb.Append("</tr></thead><tbody>");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string id = dr["ShowID"].ToString();
                    string imgPath = dr["AdvImagePath"].ToString();
                    string name = dr["ShowName"].ToString();
                    string gio = dr["GioDien"].ToString().Replace(";", "<br/>");
                    string gia = dr["GiaVe"].ToString().Replace(";", "<br/>");
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    //string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleShowItem();
                    sb.AppendLine(string.Format(template, imgPath, fromdate, url, id,name,gio, gia));

                }

            }
            sb.Append("</tbody></table>");
            divAdv.InnerHtml = sb.ToString();
        }
    }
}