using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class checkTimeout : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        Response.Write("OK");
        return;
        try
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            string szPasswd = adminAcc.AccInfo.szPasswd;
            if (szPasswd == null || szPasswd == "" || szPasswd == "db4e4b64e6ce1e")
            {
                Response.Write("Error");
            }
            else
            {
                Response.Write("OK");
            }
        }
        catch {
            Response.Write("Error");
        }
    }
}