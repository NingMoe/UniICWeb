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
using System.Collections.Generic;
using UniWebLib;

public partial class Page_Reserve : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        base.LoadPage();
		
        if (Request["act"] == "room")
        {

        }
        else if (Request["act"] == "acc")
        {

        }
        else if (Request["act"] == "cancel")
        {

        }
        else if (Request["act"] == "timespan")
        {

        }
        else if (Request["act"] == "divres")
        {

        }
        else if (Request["act"] == "divresinfo")
        {

        }
        else if (Request["act"] == "setgroup")
        {

        }
        else if (Request["act"] == "deletemember")
        {

        }
    }
}
