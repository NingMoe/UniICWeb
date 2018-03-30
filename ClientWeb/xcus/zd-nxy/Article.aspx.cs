using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string g = Request["g"];
        if (string.IsNullOrEmpty(g)||g!="2")
        {
            divContent.InnerHtml = "";
        }
    }
}