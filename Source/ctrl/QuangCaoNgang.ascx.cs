using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau.ctrl
{
    public partial class QuangCaoNgang : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet(StoreProcedureGetData);//Adv_GetAnhNgang
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }
        public string StoreProcedureGetData
        {
            get;
            set;
        }
    }
}