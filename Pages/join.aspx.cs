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
using Newtonsoft.Json;
public partial class _Default : UniPage
{
    protected string activityName = "";
    protected string szRes = "";
    protected bool bIsIN = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        string szop = Request["op"];
        if (string.IsNullOrEmpty(szop))
        {
            string szActivitySN = Request["sn"];
            if (!string.IsNullOrEmpty(szActivitySN))
            {
                ViewState["sn"] = szActivitySN;
                ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
                req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
                req.szGetKey = szActivitySN;
                UNIACTIVITYPLAN[] res;
                m_Request.m_UniDCom.SessionID = (uint)Session["SessionID"];
                m_Request.m_UniDCom.StaSN = 1;
                if (m_Request.Reserve.GetActivityPlan(req, out res) == REQUESTCODE.EXECUTE_SUCCESS && res != null && res.Length > 0)
                {
                    ViewState["devid"] = res[0].dwDevID;
                    ViewState["devsn"] = res[0].dwDevSN;
                    UNIACTIVITYPLAN acti = new UNIACTIVITYPLAN();
                    acti = res[0];
                    activityName = acti.szActivityPlanName;
                    if (((uint)acti.dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_ENROLLED) > 0)
                    {
                        bIsIN = true;
                    }
                }
            }
        }
        else
        {
            m_Request.m_UniDCom.SessionID = (uint)Session["SessionID"];
            m_Request.m_UniDCom.StaSN = 1;
            ADMINLOGINRES vrLogin = (ADMINLOGINRES)Session["LoginRes"];
            string atyId = ViewState["sn"].ToString();
            string devId = ViewState["devid"].ToString();
            string devSN = ViewState["devsn"].ToString();

            if (szop == "join")
            {
                ACTIVITYENROLL set = new ACTIVITYENROLL();
                set.dwActivityPlanID = uint.Parse(atyId);
                if (!string.IsNullOrEmpty(devId))
                {
                    set.dwDevID = uint.Parse(devId);
                }
                if (!string.IsNullOrEmpty(devSN))
                {
                    set.dwDevSN = uint.Parse(devSN);
                }
                set.dwAccNo = vrLogin.AccInfo.dwAccNo;

                if (m_Request.Reserve.EnrollActivity(set) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    szRes="加入成功";
                }
                else {
                    szRes = m_Request.szErrMessage;
                }
            }
            else if (szop == "out")
            {
              
                ACTIVITYEXIT set = new ACTIVITYEXIT();
                set.dwActivityPlanID = uint.Parse(atyId);
                set.dwAccNo = set.dwAccNo = vrLogin.AccInfo.dwAccNo;
                if (m_Request.Reserve.ExitActivity(set) == REQUESTCODE.EXECUTE_SUCCESS)
                {

                    szRes = "退出成功";
                }
            }
        }
       
    }


}