using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;

namespace TheGioiSanKhau.admin
{
    public partial class ConfigDisplayNewsCate : System.Web.UI.Page
    {
        private void LoadLeafCategory()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetAllParentNode", null);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList1.Items.Add(new ListItem(text, id, true));
                    DropDownList2.Items.Add(new ListItem(text, id, true));
                    DropDownList3.Items.Add(new ListItem(text, id, true));
                    DropDownList4.Items.Add(new ListItem(text, id, true));
                    DropDownList5.Items.Add(new ListItem(text, id, true));
                }
            }

        }
        private void LoadSettingHomePage()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Config_LoadSettingHomePage");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string name = dataRow["VariableName"].ToString();
                    string value = dataRow["Value"].ToString();
                    switch (name)
                    {
                        case "Home_NewsTopImage_1":
                            ListItem selectedListItem1 = DropDownList1.Items.FindByValue(value);
                            if (selectedListItem1 != null)
                            {
                                selectedListItem1.Selected = true;
                            }
                            break;
                        case "Home_NewsTopImage_2":
                            ListItem selectedListItem2 = DropDownList2.Items.FindByValue(value);
                            if (selectedListItem2 != null)
                            {
                                selectedListItem2.Selected = true;
                            }
                            break;
                        case "Home_NewsTopImage_3":
                            ListItem selectedListItem3 = DropDownList3.Items.FindByValue(value);
                            if (selectedListItem3 != null)
                            {
                                selectedListItem3.Selected = true;
                            }
                            break;
                        case "Home_NewsTopImage_4":
                            ListItem selectedListItem4 = DropDownList4.Items.FindByValue(value);
                            if (selectedListItem4 != null)
                            {
                                selectedListItem4.Selected = true;
                            }
                            break;
                        case "Home_CongNgheCuoi":
                            ListItem selectedListItem5 = DropDownList5.Items.FindByValue(value);
                            if (selectedListItem5 != null)
                            {
                                selectedListItem5.Selected = true;
                            }
                            LoadCNCSubMenu(value);
                            break;
                        case "Home_CongNgheCuoi_SubMenu_1":
                            ListItem selectedListItem6 = DropDownList6.Items.FindByValue(value);
                            if (selectedListItem6 != null)
                            {
                                selectedListItem6.Selected = true;
                            }
                            break;
                        case "Home_CongNgheCuoi_SubMenu_2":
                            ListItem selectedListItem7 = DropDownList7.Items.FindByValue(value);
                            if (selectedListItem7 != null)
                            {
                                selectedListItem7.Selected = true;
                            }
                            break;
                    }
                    
                }
            }
        }
        private void LoadCNCSubMenu(string value)
        {
            SqlParameter value1 = new SqlParameter("ParentNodeID", value);
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetCategoryByParentNodeID", value1);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DropDownList6.Items.Clear();
                DropDownList7.Items.Clear();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList6.Items.Add(new ListItem(text, id, true));
                    DropDownList7.Items.Add(new ListItem(text, id, true));

                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            LoadLeafCategory();
            LoadSettingHomePage();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlParameter value1 = new SqlParameter("Value1",DropDownList1.SelectedValue);
            SqlParameter value2 = new SqlParameter("Value2",DropDownList2.SelectedValue);
            SqlParameter value3 = new SqlParameter("Value3",DropDownList3.SelectedValue);
            SqlParameter value4 = new SqlParameter("Value4",DropDownList4.SelectedValue);
            SqlParameter value5 = new SqlParameter("Value5", DropDownList5.SelectedValue);
            SqlParameter value6 = new SqlParameter("Value6", DropDownList6.SelectedValue);
            SqlParameter value7 = new SqlParameter("Value7", DropDownList7.SelectedValue);
            bool res = DataAccessLayer.ExcuteNoneQuery("Config_SaveSettingHomePage", value1, value2, value3, value4, value5, value6, value7);
        }

        protected void DropDownList5_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter value1 = new SqlParameter("ParentNodeID", DropDownList5.SelectedValue);
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetCategoryByParentNodeID", value1);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                 DropDownList6.Items.Clear();
                 DropDownList7.Items.Clear();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList6.Items.Add(new ListItem(text, id, true));
                    DropDownList7.Items.Add(new ListItem(text, id, true));
                    
                }
            }
        }
    }
}