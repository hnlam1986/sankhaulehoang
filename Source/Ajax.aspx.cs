using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheGioiSanKhau.Dal;
using System.IO;
using System.Net.Mail;

namespace TheGioiSanKhau
{
    public partial class Ajax : System.Web.UI.Page
    {
        private void GetNode()
        {
            if (Request.QueryString["id"] != null)
            {
                string parentNodeId = Request.QueryString["id"];
                bool isHaveData = false;
                StringBuilder html = new StringBuilder();
                int nodeId = 0;
                if (parentNodeId != "#")
                {
                    //load root nodes
                    nodeId = int.Parse(parentNodeId);
                }
                SqlParameter paramParentNodeID = new SqlParameter("ParentNodeID", SqlDbType.Int);
                paramParentNodeID.Value = nodeId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetCategoryByParentNodeID", paramParentNodeID);
                
                html = new StringBuilder();

                html.AppendLine("<ul>");
                if (parentNodeId == "#")
                {
                    html.AppendLine("<li id=\"0\">Trang chủ<ul>");
                }
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        string id = dataRow["NewsCateID"].ToString();
                        string name = dataRow["NewsCateName"].ToString();
                        string hasChild = dataRow["HasChild"].ToString();
                        string isVisible = dataRow["IsVisible"].ToString();
                        if (hasChild == "True")
                        {
                            html.AppendLine(string.Format("<li id=\"{0}\" class=\"jstree-closed\" data-visible=\"{2}\">{1}</li>", id,
                                                          name,isVisible));
                        }
                        else
                        {
                            html.AppendLine(string.Format("<li id=\"{0}\"  data-visible=\"{2}\">{1}</li>", id, name, isVisible));
                        }
                    }
                }
                if (parentNodeId == "#")
                {
                    html.AppendLine("</ul></li>");
                }
                html.AppendLine("</ul>");

          
                Response.Write(html.ToString());

            }
        }
        private void Rename()
        {
            if (Request.Form["id"] != null && Request.Form["name"] != null)
            {
                try
                {
                    string id = Request.Form["id"];
                    string name = Request.Form["name"];
                    SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                    paramNewsCateID.Value = id;
                    SqlParameter paramNewsCateName = new SqlParameter("NewsCateName", SqlDbType.NVarChar);
                    paramNewsCateName.Value = name;
                    bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateCategoryName", paramNewsCateID, paramNewsCateName);
                    Response.Write(res);
                }
                catch (Exception ex)
                {
                    Response.Write(false);
                }
            }
        }
        private void Delete()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramNewsCateID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Category_DeleteCategory", paramNewsCateID);
                Response.Write(res);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }

        private void DeleteNews()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("NewsID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("News_DeleteNews", paramNewsID);
                if(res)
                {
                    if (File.Exists(Server.MapPath("/NewsAvarta/" + image)))
                    {
                        File.Delete(Server.MapPath("/NewsAvarta/" + image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }

        private void Create()
        {
            try
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                string isleaf = Request.Form["leaf"];
                SqlParameter paramNewsCateName = new SqlParameter("NewsCateName", SqlDbType.NVarChar);
                paramNewsCateName.Value = name;
                SqlParameter paramNewsCateParentID = new SqlParameter("NewsCateParentID", SqlDbType.Int);
                paramNewsCateParentID.Value = id;
                SqlParameter paramIsLeaf = new SqlParameter("IsLeaf", SqlDbType.Bit);
                paramIsLeaf.Value = isleaf;
                bool res = DataAccessLayer.ExcuteNoneQuery("Category_CreateNewsCategory", paramNewsCateName, paramNewsCateParentID, paramIsLeaf);
                Response.Write(res);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void GetNewsList()
        {
            if (Request.QueryString["id"] != null)
            {
                string cateId = Request.QueryString["id"];
                string isLeaf = Request.QueryString["isLeaf"];
                SqlParameter paramParentNodeID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramParentNodeID.Value = cateId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByCategoryId", paramParentNodeID);
                StringBuilder html = new StringBuilder();
                string start_template = "<table id=\"tblNews\" class=\"custom-table\">" +
                                        "<thead><tr>" +
                                        "<td>Tiêu Đề</td>" +
                                        "<td>Ngày Đăng</td>" +
                                        "<td>" + (isLeaf=="true"?
                                        ("<button class=\"btn btn-success btn-sm\"  type=\"button\"" +
                                        "onclick=\"var popup = window.open('NewsEditor.aspx?action=new&id={0}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Post a News</button>") :"")+
                                        "</td>" +
                                        "</tr></thead>";
                string item_template = "<tr id='news_{2}'>" +
                                       "<td>{0}</td>" +
                                       "<td>{1}</td>" +
                                       "<td>" +
                                       "<button class=\"btn btn-warning btn-sm\"  type=\"button\" onclick=\"var popup = window.open('NewsEditor.aspx?action=edit&id={2}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\">Edit</button>" +
                                       "<button class=\"btn btn-danger btn-sm\"  type=\"button\" style=\"margin-left:2px;\" " +
                                       "onclick=\"NewsCategoryManagementEvent.DeleteNewsItem({2},'{3}')\"" +
                                       ">Del</button>" +
                                       "<img src='img/throbber.gif' id='news_indicator_{2}' style='display:none'/>" +
                                       "</td>" +
                                       "</tr>";
                string end_template = "<tfoot>" +
                                      "<tr>" +
                                      "<td></td>" +
                                      "<td></td>" +
                                      "<td>" + (isLeaf=="true"?
                                      ("<button class=\"btn btn-success btn-sm\"  type=\"button\" " +
                                      "onclick=\"var popup = window.open('NewsEditor.aspx?action=new&id={0}','EditNewsWindow'," +
                                       "'toolbar=no, scrollbars=yes, resizable=yes, width=600, height=600'" +
                                       ");popup.focus();\"" +
                                       ">Post a News</button>"):"" )+
                                      "</td>" +
                                      "</tr></tfoot></table>";
                StringBuilder sb = new StringBuilder();
                start_template = string.Format(start_template, cateId);
                end_template = string.Format(end_template, cateId);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        string id = dataRow["NewsID"].ToString();
                        string title = dataRow["NewsTitle"].ToString();
                        string posted_date = dataRow["PostedDate"].ToString();
                        string newsImage = dataRow["ThumnailImagePath"].ToString();
                        string formatTitle = string.Format("<a href='/NewsDetail/{0}/{1}'>{2}<a>", Utilities.ConvertUnicodeToAscii(title), id, title);
                        sb.AppendLine(string.Format(item_template, formatTitle, posted_date, id, newsImage));
                    }
                }
                string result = start_template + sb.ToString() + end_template;
                Response.Write(result);
            }
        }
        private void UpdateOrder()
        {
            try
            {
                string selected = Request.Form["selected"];
                string children = Request.Form["children"];
                string is_leaf = Request.Form["is_leaf"];
                string parent_node = Request.Form["parent_node"];
                string sort_order = Request.Form["order"];
                if (string.IsNullOrEmpty(children))
                {
                    children = selected;
                }
                string[] order = children.Split(',');
                List<string> list = null;
                if (!order.Contains(selected))
                {
                    children = selected + "," + children;
                    order = children.Split(',');
                    list = order.ToList<string>();
                }
                else
                {
                    list = order.ToList<string>();
                    int old_index = list.IndexOf(selected);
                    list.RemoveAt(old_index);
                    int index_at = int.Parse(sort_order);
                    if (old_index < index_at)
                        index_at -= 1;
                    else if (index_at != 0 && index_at > list.Count - 1)
                        index_at = list.Count - 1;
                    list.Insert(index_at, selected);

                }
                int index = 0;
                foreach (string s in list)
                {
                    SqlParameter paramNewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
                    paramNewsCateID.Value = s;
                    SqlParameter paramNewsCateParentID = new SqlParameter("NewsCateParentID", SqlDbType.Int);
                    paramNewsCateParentID.Value = parent_node;
                    SqlParameter paramSortOrder = new SqlParameter("SortOrder", SqlDbType.Int);
                    paramSortOrder.Value = index;
                    SqlParameter paramIsLeaf = new SqlParameter("IsLeaf", SqlDbType.Bit);
                    paramIsLeaf.Value = is_leaf;
                    bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateOrder", paramNewsCateID,
                                                               paramNewsCateParentID, paramSortOrder, paramIsLeaf);
                    index++;
                }
                Response.Write(true);
            }
            catch(Exception)
            {
                Response.Write(false);
            }
        }
        private void DeleteAdv()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("AdvID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Adv_DeleteAdv", paramNewsID);
                if (res)
                {
                    if (File.Exists(Server.MapPath(image)))
                    {
                        File.Delete(Server.MapPath(image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void DeleteShow()
        {
            try
            {
                string id = Request.Form["id"];
                string image = Request.Form["img"];
                SqlParameter paramNewsID = new SqlParameter("AdvID", SqlDbType.Int);
                paramNewsID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Show_DeleteShow", paramNewsID);
                if (res)
                {
                    if (File.Exists(Server.MapPath(image)))
                    {
                        File.Delete(Server.MapPath(image));
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void RefreshAdv()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem(true);
                    string s = string.Format(template, imgPath, fromdate, todate, url, id);
                    Response.Write(s);
                }
            }catch(Exception ex)
            {
                Response.Write(false);
            }
        }
        private void RefreshShow()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Show_GetShowByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleShowItem(true);
                    string name = dr["ShowName"].ToString();
                    string gio = dr["GioDien"].ToString().Replace(";","<br/>");
                    string gia = dr["GiaVe"].ToString().Replace(";", "<br/>");
                    string s = string.Format(template, imgPath, fromdate, url, id,name,gio,gia);
                    Response.Write(s);
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void AddNewAdv()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAdvByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem();
                    string s = string.Format(template, imgPath, fromdate, todate, url, id);
                    Response.Write(s);
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void AddNewShow()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter AdvID = new SqlParameter("AdvID", SqlDbType.Int);
                AdvID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Show_GetShowByID", AdvID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleShowItem();
                    string name = dr["ShowName"].ToString();
                    string gio = dr["GioDien"].ToString().Replace(";", "<br/>");
                    string gia = dr["GiaVe"].ToString().Replace(";", "<br/>");
                    string s = string.Format(template, imgPath, fromdate, url, id, name, gio, gia);
                    Response.Write(s);
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void DeleteAlbum()
        {
            try
            {
                string id = Request.Form["id"];
                string folder = Request.Form["folder"];
                SqlParameter ID = new SqlParameter("ID", SqlDbType.Int);
                ID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Album_DeleteAlbum", ID);
                if (res)
                {
                    string path = Server.MapPath("/Albums/" + folder);
                    if (Directory.Exists(path))
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                        Directory.Delete(path);
                    }
                }
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void DeleteVideo()
        {
            try
            {
                string id = Request.Form["id"];
                SqlParameter ID = new SqlParameter("ID", SqlDbType.Int);
                ID.Value = id;
                bool res = DataAccessLayer.ExcuteNoneQuery("Video_DeleteVideo", ID);
                Response.Write(true);
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }
        }
        private void ChangeDisplayMenuStatus()
        {
            string id = Request.QueryString["id"];
            SqlParameter pID = new SqlParameter("ID", SqlDbType.Int);
            pID.Value = id;
            bool res = DataAccessLayer.ExcuteNoneQuery("Category_UpdateDisplayStatus", pID);
        }
        private void GetImageFromFolder()
        {
            string path = Request.Form["path"];
            string[] files = Directory.GetFiles(path);
            StringBuilder sb = new StringBuilder();
            foreach (string file in files)
            {
                string imgUrl = file.Replace("\\", "/");
                int index = imgUrl.IndexOf("UserFiles");
                imgUrl = imgUrl.Substring(index);
                imgUrl = "/" + imgUrl;
                sb.Append("<img src=\"" + imgUrl + "\" style=\"width: 100px;float:left;margin-right:10px;margin-bottom:10px\" onclick=\"apply_img('" + imgUrl + "')\"/>");
            }
            Response.Write(sb.ToString());
        }
        private void SendEmail()
        {
            try
            {
                string defaultEmail = "";
                string sendEmail = "";
                string passEmail = "";
                if (ConfigurationManager.AppSettings["receiveEmail"] != null)
                {
                    defaultEmail = ConfigurationManager.AppSettings["receiveEmail"];
                    sendEmail = ConfigurationManager.AppSettings["sendEmail"];
                    passEmail = ConfigurationManager.AppSettings["passSendEmail"];
                }
                string content = Request.Form["content"];
                MailMessage mail = new MailMessage(sendEmail, defaultEmail);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential(sendEmail, passEmail);
                client.Host = "share06.vhost.vn";
                mail.Subject = "Xác thực đặt vé Sân Khấu Lê Hoàng";
                mail.Body = content;
                mail.IsBodyHtml = true;
                client.Send(mail);
                Response.Write(true.ToString());
            }
            catch (Exception ex)
            {
                Response.Write(false.ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                switch (Request.QueryString["action"])
                {
                    case "get":
                        GetNode();
                        break;
                    case "rename":
                        Rename();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "create":
                        Create();
                        break;
                    case "getnewslist":
                        GetNewsList();
                        break;
                    case "deletenews":
                        DeleteNews();
                        break;
                    case "order":
                        UpdateOrder();
                        break;
                    case "deleteadv":
                        DeleteAdv();
                        break;
                    case "deleteshow":
                        DeleteShow();
                        break;
                    case "refreshadv":
                        RefreshAdv();
                        break;
                    case "refreshshow":
                        RefreshShow();
                        break;
                    //RefreshShow
                    case "addnewadv":
                        AddNewAdv();
                        break;
                    case "addnewshow":
                        AddNewShow();
                        break;
                    case "deletealbum":
                        DeleteAlbum();
                        break;
                    case "deletevideo":
                        DeleteVideo();
                        break;
                    case "changestatus":
                        ChangeDisplayMenuStatus();
                        break;
                    case "getimage":
                        GetImageFromFolder();
                        break;
                    case "sendemail":
                        SendEmail();
                        break;
                }
            }
        }
    }
}