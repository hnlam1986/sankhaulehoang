using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheGioiSanKhau.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            string username = ConfigurationManager.AppSettings["username"];
            string pass = ConfigurationManager.AppSettings["pass"];
            if (txtUserName.Text.ToLower() == username && txtPassword.Text == pass)
            {
                Session["tgsk_logon"] = true;
                Response.Redirect("/admin/NewsCategoryManagement.aspx");
                lblMsg.Visible = false;
            }
            else
            {
                lblMsg.Visible = true;
            }
        }
    }
}