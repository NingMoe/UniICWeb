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
using System.Xml;
using System.Text;


public partial class Page_Search : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ADMINCHECK check = new ADMINCHECK();
        ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
        vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        vrGet.szGetKey = Request["id"];
        uint uStatue = Parse(Request["statue"]);
        string szInfo = Request["szCheckInfo"];
        UNIACTIVITYPLAN[] vtRes;
        if (m_Request.Reserve.GetActivityPlan(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            UNIACTIVITYPLAN setActiviPlan;
            setActiviPlan = vtRes[0];
            if (setActiviPlan.dwActivityPlanID != null && (uint)setActiviPlan.dwActivityPlanID > 0)
            {
                check.dwSubjectID = setActiviPlan.dwActivityPlanID;
                check.dwSubjectType = (uint)ADMINCHECK.DWSUBJECTTYPE.CHECK_ACTIVITYPLAN;
                UNIRESERVE setResv;
                if (GetResvByID(setActiviPlan.dwResvID.ToString(), out setResv))
                {
                    check.dwApplicantID = setResv.dwOwner;
                    check.szApplicantName = setResv.szOwnerName;
                    check.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
                    check.szCheckDetail = szInfo;
                    if (m_Request.Admin.AdminCheck(check) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Response.Write(m_Request.szErrMessage.ToString());
                    }
                    else
                    {
                        vtRes[0].dwStatus = uStatue | (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
                        m_Request.Reserve.SetActivityPlan(vtRes[0], out vtRes[0]);
                        setActiviPlan.dwStatus = 1;// (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
                        m_Request.Reserve.SetActivityPlan(setActiviPlan, out setActiviPlan);
                        Response.Write("success");
                        return;
                    }
                  

                }
                else
                {
                    Response.Write("找不到预约信息");
                }
            }
            else
            {
                Response.Write("找不到活动信息");
            }
        }
        else {
            Response.Write("找不到审核信息");
        }
    }

   
}
