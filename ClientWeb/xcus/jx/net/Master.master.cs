using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_cg_Templates_Master : UniClientMaster
{
    protected string url;
    protected bool isTeacher=false;
    protected string mustLogin = "display:none";
    protected string mustTeach = "display:none";
    protected string trueName = "";
    protected string logonName = "未登录";
    protected string userDept = "未登录";
    private uint year=0;
    public uint Year
    {
        get { return year; }
        set { year = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        MyTerm.YearTerm = year;
        url = this.ResolveClientUrl("~/ClientWeb/");
        if (Session["LOGIN_ACCINFO"] != null && IsClientLogin())
        {
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            trueName = acc.szTrueName;
            logonName = acc.szLogonName;
            userDept = acc.szDeptName;
            ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            isTeacher = (res.dwManRole&(uint)ADMINLOGINRES.DWMANROLE.MANIDENT_TEACHER)>0;//(acc.dwIdent&(uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER) > 0;
        }
    }
}
