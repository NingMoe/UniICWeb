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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string szDevList = "";
    protected string szFile = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            ADMINCHECK check = new ADMINCHECK();
            UNIACTIVITYPLAN setActiviPlan;
            GetHTTPObj(out setActiviPlan);
            if (setActiviPlan.dwActivityPlanID != null && (uint)setActiviPlan.dwActivityPlanID > 0)
            {
                check.dwSubjectID = setActiviPlan.dwActivityPlanID;
                check.dwSubjectType = (uint)ADMINCHECK.DWSUBJECTTYPE.CHECK_ACTIVITYPLAN;

                UNIRESERVE setResv;
                if (GetResvByID(setActiviPlan.dwResvID.ToString(), out setResv))
                {
                    check.dwApplicantID = setResv.dwOwner;
                    check.szApplicantName = setResv.szOwnerName;
                    check.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;

                    if (m_Request.Admin.AdminCheck(check) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        
                        MessageBox(m_Request.szErrMessage, "审核失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    }
                    else
                    {
                        ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
                        vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
                        vrGet.szGetKey = setActiviPlan.dwActivityPlanID.ToString();
                        UNIACTIVITYPLAN[] vtRes;
                        if (m_Request.Reserve.GetActivityPlan(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
                        {
                            vtRes[0].dwStatus = setActiviPlan.dwStatus | (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
                            m_Request.Reserve.SetActivityPlan(vtRes[0], out vtRes[0]);
                            MessageBox("审核成功", "审核成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                            return;
                        }
                       
                    }
                }
            }
        }
    
        DEVREQ devGet = new DEVREQ();
        devGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIDEVICE[] vtDev;
        if (m_Request.Device.Get(devGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null && vtDev.Length > 0)
        {
            
            for (int i = 0; i < vtDev.Length; i++)
            {
                if (((uint)vtDev[i].dwProperty & ((uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN)) > 0)
                {
                szDevList += GetInputItemHtml(CONSTHTML.option, "", vtDev[i].szDevName, vtDev[i].dwDevID.ToString());
                }
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;
            ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
            vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
            vrGet.szGetKey = Request["id"];
            UNIACTIVITYPLAN[] vtRes;
            if (m_Request.Reserve.GetActivityPlan(vrGet, out vtRes)==REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                
                szFile = vtRes[0].szApplicationURL.ToString();
                if ((vtRes[0].dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED) > 0)
                {
                    vtRes[0].dwStatus = (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED;
                }
                else if ((vtRes[0].dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                {
                    vtRes[0].dwStatus = (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING;
                }
                else if ((vtRes[0].dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                {
                    vtRes[0].dwStatus = (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN;
                }
              
                Logger.trace("活动活动成果" + vtRes[0].szActivityPlanName); 
                UNIDEVICE setDev;
                if (vtRes[0].dwDevID!=null&&getDevByID(vtRes[0].dwDevID.ToString(), out setDev))
                {
                    PutMemberValue("devName", setDev.szDevName.ToString());
                }
                string szBeginTime = GetTimeStr(vtRes[0].dwBeginTime);
                ViewState["dwBeginTime"] = szBeginTime;
                string szEndTime = GetTimeStr(vtRes[0].dwEndTime);
                ViewState["dwEndTime"] = szEndTime;

                string szEnrollDeadline = GetDateStr(vtRes[0].dwEnrollDeadline);
                ViewState["szEnrollDeadline"] = szEnrollDeadline;
                string szActivityDate = GetDateStr(vtRes[0].dwActivityDate);
                ViewState["szActivityDate"] = szActivityDate;
                //vtRes[0].szIntroInfo = "";
                PutHTTPObj(vtRes[0]);
            }
        }
        else
        {
           
        }
	}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (ViewState["dwBeginTime"] != null)
        {
            PutMemberValue("dwBeginTime", ViewState["dwBeginTime"].ToString());
        }
        if (ViewState["dwEndTime"] != null)
        {
            PutMemberValue("dwEndTime", ViewState["dwEndTime"].ToString());
        }
        if (ViewState["szActivityDate"] != null)
        {
            PutMemberValue("dwActivityDate", ViewState["szActivityDate"].ToString());
        }
        if (ViewState["szEnrollDeadline"] != null)
        {
            PutMemberValue("dwEnrollDeadline", ViewState["szEnrollDeadline"].ToString());
        }
    }
}
