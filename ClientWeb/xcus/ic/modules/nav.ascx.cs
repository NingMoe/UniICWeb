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

public partial class WebUserControl : UniClientModule
{
    protected string bDisplay1 = "none";
    protected string bDisplay2 = "none";
    protected string szTrueName = "";
    protected string szRes = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsClientLogin() && Session["LOGIN_ACCINFO"] != null)
        {
            UNIACCOUNT vrAccInfo;
            vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];

            szTrueName = vrAccInfo.szTrueName.ToString();
            bDisplay1 = "display";
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RESVREQ vrGet1 = new RESVREQ();
            vrGet1.dwBeginDate = uint.Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
            vrGet1.dwEndDate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
            vrGet1.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT;
            UNIRESERVE[] vtRes1;
            uResponse = m_Request.Reserve.Get(vrGet1, out vtRes1);

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes1 != null && vtRes1.Length > 0)
            {

                for (int i = 0; i < vtRes1.Length; i++)
                {
                   uint uDate = (uint)vtRes1[i].dwPreDate;
                    szRes = "(有违约)";//uDate / 10000 + "-" + (uDate % 10000) / 100 + "-" + uDate % 100 + ",";
                    break;
                }
            }
        }
        else
        {
           bDisplay2 = "display";
        }
       
    }
}
