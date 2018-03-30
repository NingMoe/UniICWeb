using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Login : UniClientPage
{
    protected string role = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string sys = Request["sys"];
        if (sys=="teach"&&IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_TEACHINGLAB))//教学系统 重新登录
        {
            //ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if ((acc.dwIdent & 536870912) > 0)
                Response.Redirect("TestPlan.aspx");
            else
                Response.Redirect("TestItem.aspx");
        }
        role = Request["role"];
    }
}