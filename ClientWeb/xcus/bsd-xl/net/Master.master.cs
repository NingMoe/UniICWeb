using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_Templates_Master : UniClientMaster
{
    protected string url;
    protected string mustLogin = "display:none";
    protected string mustTeach = "display:none";
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
        if (Session["LOGIN_ACCINFO"] != null && IsClientLogin())
        {
            mustLogin = "";
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if ((acc.dwIdent & 512) > 0)//教师
                mustTeach = "";
        }
    }
}
