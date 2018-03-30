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
	protected void Page_Load(object sender, EventArgs e)
	{
        string szOP = "新建";
        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        if (IsPostBack)
        {
            ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
            vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
            string szID = Request["id"];
            UNIACTIVITYPLAN setActiviPlan=new UNIACTIVITYPLAN();
            if (szID != null && szID != "")
            {
                vrGet.szGetKey = Request["id"];
                UNIACTIVITYPLAN[] vtRes;
        
                if (m_Request.Reserve.GetActivityPlan(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
                {
                    setActiviPlan = vtRes[0];
                }
            }
          
            GetHTTPObj(out setActiviPlan);
            setActiviPlan.dwActivityDate = GetDate(Request["dwActivityDate"]);
            setActiviPlan.dwBeginTime = GetTime(Request["dwBeginTime"]);
            setActiviPlan.dwEndTime = GetTime(Request["dwEndTime"]);
            setActiviPlan.dwEnrollDeadline = GetDate(Request["dwEnrollDeadline"]);
            setActiviPlan.dwKind = (uint)UNIACTIVITYPLAN.DWKIND.ACTIVITYPLANKIND_EXPERIENCE;
          
            if (Request["op"] != "set")
            {
                UNIGROUP newGroup;
                if (NewGroup(setActiviPlan.szActivityPlanName.ToString() + "组", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out newGroup))
                {
                    setActiviPlan.dwGroupID = newGroup.dwGroupID;
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, szOP + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                setActiviPlan.dwOwner = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
            }
          
            if (m_Request.Reserve.SetActivityPlan(setActiviPlan, out setActiviPlan) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOP+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOP+"成功", szOP+"成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
    
        DEVREQ devGet = new DEVREQ();
        devGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIDEVICE[] vtDev;
        if (m_Request.Device.Get(devGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null && vtDev.Length > 0)
        {
            
            for (int i = 0; i < vtDev.Length; i++)
            {
                if (((uint)vtDev[i].dwProperty & ((uint)UNIDEVICE.DWPROPERTY.DEVPROP_ACTIVITYPLAN)) > 0)
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
                PutHTTPObj(vtRes[0]);
              
                string szBeginTime = GetTimeStr(vtRes[0].dwBeginTime);
                ViewState["dwBeginTime"] = szBeginTime;
                string szEndTime = GetTimeStr(vtRes[0].dwEndTime);
                ViewState["dwEndTime"] = szEndTime;
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
    }
}
