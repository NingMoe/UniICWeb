using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientWeb_xcus_bsd_xl_ArticleList : System.Web.UI.Page
{
    protected string artTitle = "内容浏览";
    protected string iframeUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string title = Request["title"];
        string id = Request["id"];
        string type = Request["type"];
        if (title != null) artTitle = Server.UrlDecode(title);
        if (!string.IsNullOrEmpty(id)&&!string.IsNullOrEmpty(type))
        {
            iframeUrl = "Article.aspx?id="+id+"&type="+type;
        }
    }
}