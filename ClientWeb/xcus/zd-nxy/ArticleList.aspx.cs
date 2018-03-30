using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArticleList : System.Web.UI.Page
{
    protected string pagePosition = "";
    protected string iframeUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["g"]))
        {
            Response.Redirect("Default.aspx");
        }

        string g = Request["g"].ToString();
        string c = Request["c"];

        iframeUrl = "Article.aspx?g="+g;

        if (g=="1")
        {
            pagePosition = " >> <a href='ArticleList.aspx?g=1'>平台介绍</a>";
            if (!string.IsNullOrEmpty(c))
            {
                if (c=="1")
                {
                    pagePosition += " >> 平台简介";
                }
                else if (c == "2")
                {
                    pagePosition += " >> 联系我们";
                }
            }
            else
            {
                pagePosition += " >> 平台简介";
            }
        }
        else if (g == "2")
        {
            pagePosition = " >> <a href='ArticleList.aspx?g=2'>新闻中心</a>";
            if (!string.IsNullOrEmpty(c))
            {
                if (c == "1")
                {
                    pagePosition += " >> 新闻动态";
                }
                else if (c == "2")
                {
                    pagePosition += " >> 通知公告";
                }
            }
            else
            {
                pagePosition += " >> 新闻动态";
            }
        }
        else if (g == "3")
        {
            pagePosition = " >> <a href='ArticleList.aspx?g=3'>规章制度</a>";
        }
        else if (g == "4")
        {
            pagePosition = " >> <a href='ArticleList.aspx?g=4'>经验交流</a>";
        }
        else if (g == "5")
        {
            pagePosition = " >> <a href='ArticleList.aspx?g=5'>下载中心</a>";
        }

        GetArticle();
    }

    private void GetArticle()
    {
        lbTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        if (!string.IsNullOrEmpty(Request["tl"]))
        {
            lbArticleTitle.Text = HttpUtility.UrlDecode(Request["tl"].ToString());
        }
    }
}