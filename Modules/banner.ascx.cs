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
using UniWebLib;

public partial class banner : UniControl
{
    protected string szInstImg = "2";
    protected string szReportImg = "2";
    protected string szSysImg = "2";
    protected int nCurPage = 0;
    protected string szTime = "";
    protected string szTrueName = "";
    protected string szManRole = "";
    protected uint uManRole = 0;
    protected int szUnStall = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
      

       szTime= DateTime.Now.ToString("yyyy-MM-dd");

        if (Request.Url.AbsoluteUri.ToLower().IndexOf("/inst/main.aspx") >= 0)
        {
            szInstImg = "";
            nCurPage = 1;
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/report/main.aspx") >= 0)
        {
            szReportImg = "";
            nCurPage = 2;
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/sys/main.aspx") >= 0)
        {
            szSysImg = "";
            nCurPage = 3;
        }
        else if (Request.Url.AbsoluteUri.ToLower().IndexOf("/supsys/main.aspx") >= 0)
        {
            szSysImg = "";
            nCurPage = 0;
        }

        if (Session["SessionID"] == null || Session["LoginResult"] == null)
        {
            Response.Redirect(MyVPath+"Pages/"+ConfigConst.GCSysFrame+"/LoginForm.aspx");
        }
        else
        {
            szTrueName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szTrueName;
            if ((((uint)((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwManRole) & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER) > 0)
            {
                szUnStall = 1;
            }
            else
            {
                szUnStall =0;
            }
        }
        szManRole=((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwManRole.ToString();
        uManRole = (uint)(((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwManRole);
    }
}
