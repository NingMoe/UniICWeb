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
using UniStruct;

public partial class WebUserControl : UniClientModule
{
    protected string bDisplay1 = "none";
    protected string bDisplay2 = "none";
    protected string bDisplay3 = "none";
    protected string szTrueName = "";
    protected string logonName = "";
    protected string accNo = "";
    protected string phone = "";
    protected string email = "";
    protected string ident = "";
    protected string tutorSta = "5";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] != null && IsLogined())
        {
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];

            szTrueName = vrAccInfo.szTrueName;
            logonName = vrAccInfo.szLogonName;
            accNo = vrAccInfo.dwAccNo.ToString();
            phone = vrAccInfo.szHandPhone;
            email = vrAccInfo.szEmail;
            ident = vrAccInfo.dwIdent.ToString();
            tutorSta = GetTutorCheckStatus();
            if ((Convert.ToUInt32(vrAccInfo.dwIdent) & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR)>0)
            {
                bDisplay3 = "display";
            }
            else
            {
                bDisplay1 = "display";
            }
        }
        else
        {
            //bDisplay2 = "display";
        }
    }

}
