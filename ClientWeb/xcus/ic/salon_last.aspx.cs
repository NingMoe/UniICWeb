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
using System.Collections.Generic;
using UniWebLib;

public partial class Page_ : UniClientPage
{
    protected string m_szInfo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int test = 0;
        base.LoadPage();

        if (!this.Page.IsPostBack)
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i <= 6; i++)
            {
                ListItem item = (new ListItem(now.AddMonths(i * (-1)).ToString("yyyy-MM"), now.AddMonths(i * (-1)).ToString("yyyyMM")));
                ActivityDate.Items.Add(item);
                now = DateTime.Now;
            }
        }
        GetActivity();     
    }
    protected void ActivityDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetActivity();
    }
    private void GetActivity()
    {
        m_szInfo = "";
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
        UNIACTIVITYPLAN[] vrResult;
        vrParameter.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;

        uResponse = m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            int nDateNow = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
            int nCount = vrResult.Length;
            for (int i = 0; i < nCount; i++)
            {
                string szActivity = "";
                if (vrResult[i].dwActivityDate >= nDateNow)
                {
                    continue;
                }
                uint uStatue = (uint)vrResult[i].dwStatus;
                if ((uStatue & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) <= 0)
                {
                    continue;
                }
                if ((uStatue & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN) > 0)
                {
                    continue;
                }
                uint uActivityDate = uint.Parse(ActivityDate.SelectedValue);
                if (uActivityDate != (((uint)vrResult[i].dwActivityDate)/100))
                {
                    continue;
                }
                string szRepory = "";

                uint dwPublishDate = (uint)vrResult[i].dwPublishDate;
                uint dwEnrollDeadline = (uint)vrResult[i].dwEnrollDeadline;
                uint dwActivityDate = (uint)vrResult[i].dwActivityDate;
                uint dwBegIntime = (uint)vrResult[i].dwBeginTime;
                uint dwEndTime = (uint)vrResult[i].dwEndTime;
                if (!string.IsNullOrEmpty( vrResult[i].szActivityPlanURL))
                {
                    //szRepory = "<img style=\"max-width:550px;\" src =\"" + vrResult[i].szActivityPlanURL + "\" />";
                }
                m_szInfo += "<th><a href=\"salon_pre_content.aspx?type=Last&amp;id=" + vrResult[i].dwActivityPlanID.ToString() + "\"><span>" + vrResult[i].szActivityPlanName.ToString() + "</a></span></th>";
                //szActivity += "<a href=\"salon_pre_content.aspx?id=" + vrResult[i].dwActivityPlanID.ToString() + "\" class=\"title\" title=\"" + szActivityPlanNameShow + "\">预告：" + szActivityPlanName + "</a>";

                if (dwActivityDate == 20990101)
                {
                    m_szInfo += "<td>" + "待定" + "</td>";
                }
                else
                {
                    m_szInfo += "<td>" + dwActivityDate / 10000 + "-" + (dwActivityDate / 100 % 100) + "-" + (dwActivityDate % 100) + " " +
                        dwBegIntime / 100 + ":" + (dwBegIntime % 100).ToString("00") + "-" + (dwEndTime / 100) + ":" + (dwEndTime % 100).ToString("00") + "</td>";
                }
                m_szInfo += "</tr><tr class=\"detail\"><td colspan=\"2\">";
                m_szInfo += "<p><table style=\"margin:12px\">";
                m_szInfo += "<tr><td colspan=\"2\">" + szRepory + "</td></tr>";
                // m_szInfo += "<tr><td style=\"width:100px\" colspan=\"2\">" + vrResult[i].szActivityPlanName.ToString() + "</td></tr>";

                m_szInfo += "</table></p></td></tr>";
            }

        }
    }
}
