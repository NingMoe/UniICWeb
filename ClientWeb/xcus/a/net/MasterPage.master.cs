using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_net_MasterPage : UniClientMaster
{
    protected bool isLogin = false;
    protected string trueName = "";
    protected string logonName = "";
    protected string userDept = "";
    protected uint mHeadBg = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        mHeadBg = common.ToUInt(GetConfig("mHeadBg"));
        string proTab=GetConfig("proTab");
        if (proTab == "" || proTab == "0")
        {
            Response.Redirect("~/ClientWeb/update.aspx");
        }
        if (Session["LOGIN_ACCINFO"] != null && IsClientLogin())
        {
            isLogin = true;
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            trueName = acc.szTrueName;
            logonName = acc.szLogonName;
            userDept = acc.szDeptName;
        }
    }
}
