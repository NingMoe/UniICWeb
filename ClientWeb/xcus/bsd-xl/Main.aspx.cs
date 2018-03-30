using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_Main : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] == null || !IsClientLogin())
        {
            Response.Redirect("Login.aspx");
        }
    }
}