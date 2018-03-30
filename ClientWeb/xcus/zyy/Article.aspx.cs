using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Model;
using BLL;
using Util;
using System.Web.UI.WebControls;

public partial class Article : System.Web.UI.Page
{
        ArticleBLL articleBLL = new ArticleBLL();
        protected ArticleConverter articleConverter = new ArticleConverter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    divContent.InnerHtml = "";
                }
                else
                {
                    int articleid =Convert.ToInt32(Request.QueryString["id"]);
                    InfoItem art = articleBLL.GetArticleByKey(articleid);
                    divContent.InnerHtml = art.Content;
                }
            }
        }
}