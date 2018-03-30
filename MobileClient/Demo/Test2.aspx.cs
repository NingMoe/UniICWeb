﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : PageBase
{
    protected string szMsg = "";
    protected string szFormID = "null";
    protected string szLabID = "";
    protected string szDrag = "false";
    protected string dwBeginTime = "";
    protected string dwEndTime = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        szLabID=Request["szLabID"];
        dwBeginTime = Request["dwBeginTime"];
        dwEndTime = Request["dwEndTime"]; 
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES currentUser = (ADMINLOGINRES)Session["LoginResult"];
            if (((uint)currentUser.AccInfo.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0)
            {
                szDrag = "true";
            }
        }
    }
}
