﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : Page
{
    public string szErrMsg = "出错啦!";

    protected void Page_Load(object sender, EventArgs e)
	{
        Response.CacheControl = "no-cache";

        string szType = Request["type"];
        if (szType == "1")
        {
            szErrMsg = "出错啦，请在微信内打开";
        }
        else
        {
            szErrMsg = "";
        }
	}
}