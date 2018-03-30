using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_option : UniClientPage
{
    protected bool islogin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            UNIACCOUNT acc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (acc.szLogonName.ToLower() == "staadmin001")
                islogin = true;
            else
            {
                common.ClearLogin();
                MsgBoxH("非超级管理员不可登录");
            }
        }
    }
}