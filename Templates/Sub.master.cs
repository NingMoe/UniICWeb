using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Templates_Default : UniMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (
            Request.UrlReferrer == null
            || Request.UrlReferrer.AbsoluteUri.ToLower().IndexOf("main.aspx") < 0
            // || Request.HttpMethod.ToUpper() != "POST"
            )
        {
            if (File.Exists(Server.MapPath("main.aspx")))
            {
                Response.Redirect("main.aspx");
            }else if (File.Exists(Server.MapPath("../main.aspx")))
            {
                Response.Redirect("../main.aspx");
            }
        }
    }
}
