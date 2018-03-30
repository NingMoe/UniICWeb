using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Templates_Instance : UniMaster
{
    protected string szBodyClass = "Inst";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.AbsoluteUri.ToLower().IndexOf("/inst/main.aspx") >= 0)
        {
            szBodyClass = "Inst";
            HeadInclude.Theme = "redmond";
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/report/main.aspx") >= 0)
        {
            szBodyClass = "Report";
            HeadInclude.Theme = "cupertino";
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/sys/main.aspx") >= 0)
        {
            szBodyClass = "Sys";
            HeadInclude.Theme = "cupertino";
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/supsys/main.aspx") >= 0)
        {
            szBodyClass = "Sys";
            HeadInclude.Theme = "cupertino";
        }
    }
}
