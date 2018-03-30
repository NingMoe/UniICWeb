using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientWeb_Modules_AtcMenu : System.Web.UI.UserControl
{
    protected string gName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["g"]))
        {
            Response.Redirect("Default.aspx");
        }
        string g = Request["g"].ToString();
        if (g=="1")
        {
            gName = "平台介绍";
            cList.InnerHtml = "<li><a href='ArticleList.aspx?g=1&c=1'>平台简介</a></li><li><a href='ArticleList.aspx?g=1&c=2'>联系我们</a></li>";
        }
        else if (g == "2")
        {
            gName = "新闻中心";
            cList.InnerHtml = "<li><a href='ArticleList.aspx?g=2&c=1'>新闻动态</a></li><li><a href='ArticleList.aspx?g=2&c=2'>通知公告</a></li>";
        }
        else if (g == "3")
        {
            gName = "规章制度";
        }
        else if (g == "4")
        {
            gName = "经验交流";
        }
        else if (g == "5")
        {
            gName = "下载中心";
        }
    }
}