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
    protected string szActivity = "";
    protected string szActivityHistory = "";
    protected string szReacher = "";
    int len = 15;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        string szCurPath = Request.FilePath;
        Response.Cookies["unifoundUrl"].Value = szCurPath;
        Response.Cookies["unifoundUrl"].Expires = System.DateTime.Now.AddDays(1);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (!this.Page.IsPostBack)
        {

            ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
            UNIACTIVITYPLAN[] vrResult;
            vrParameter.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
            uResponse = m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {

                int nDateNow = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int nCount = vrResult.Length;
                string resvid = "";
                for (int i = 0; i < nCount; i++)
                {
                    resvid += vrResult[i].dwActivityPlanID.ToString() + ";";
                    uint uStatue = (uint)vrResult[i].dwStatus;
                    if ((uStatue & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) <= 0)
                    {
                        continue;
                    }
                    if ((uStatue & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN) > 0)
                    {
                        //continue;
                    }
                    string szActivityPlanName = vrResult[i].szActivityPlanName.ToString();
                    string szActivityPlanNameShow = szActivityPlanName;

                    //if (szActivityPlanName.Length > len)
                    //{
                    //    szActivityPlanName = szActivityPlanName.Substring(0, len) + "...";
                    //}
                    string szRepory = "";
                    uint dwPublishDate = (uint)vrResult[i].dwPublishDate;
                    uint dwEnrollDeadline = (uint)vrResult[i].dwEnrollDeadline;
                    uint dwActivityDate = (uint)vrResult[i].dwActivityDate;
                    uint dwBegIntime = (uint)vrResult[i].dwBeginTime;
                    uint dwEndTime = (uint)vrResult[i].dwEndTime;

                    if (vrResult[i].dwActivityDate >= nDateNow)
                    {
                        szActivity += "<th><a href=\"salon_pre_content.aspx?type=Last&amp;id=" + vrResult[i].dwActivityPlanID.ToString() + "\"><span>" + vrResult[i].szActivityPlanName.ToString() + "</a></span></th>";
                        if (dwActivityDate == 20990101)
                        {
                            szActivity += "<td>" + "待定" + "</td>";
                        }
                        else
                        {
                            szActivity += "<td>" + dwActivityDate / 10000 + "-" + (dwActivityDate / 100 % 100) + "-" + (dwActivityDate % 100) + " " +
                                dwBegIntime / 100 + ":" + (dwBegIntime % 100).ToString("00") + "-" + (dwEndTime / 100) + ":" + (dwEndTime % 100).ToString("00") + "</td>";
                        }
                        szActivity += "</tr><tr class=\"detail\"><td colspan=\"2\">";
                        szActivity += "<p><table style=\"margin:12px\">";
                        szActivity += "<tr><td colspan=\"2\">" + szRepory + "</td></tr>";
                        szActivity += "</table></p></td></tr>";

                    }
                    else
                    {

                        szActivityHistory += "<th><a href=\"salon_pre_content.aspx?type=Last&amp;id=" + vrResult[i].dwActivityPlanID.ToString() + "\"><span>" + vrResult[i].szActivityPlanName.ToString() + "</a></span></th>";
                        if (dwActivityDate == 20990101)
                        {
                            szActivityHistory += "<td>" + "待定" + "</td>";
                        }
                        else
                        {
                            szActivityHistory += "<td>" + dwActivityDate / 10000 + "-" + (dwActivityDate / 100 % 100) + "-" + (dwActivityDate % 100) + " " +
                                dwBegIntime / 100 + ":" + (dwBegIntime % 100).ToString("00") + "-" + (dwEndTime / 100) + ":" + (dwEndTime % 100).ToString("00") + "</td>";
                        }
                        szActivityHistory += "</tr><tr class=\"detail\"><td colspan=\"2\">";
                        szActivityHistory += "<p><table style=\"margin:12px\">";
                        szActivityHistory += "<tr><td colspan=\"2\">" + szRepory + "</td></tr>";
                        szActivityHistory += "</table></p></td></tr>";

                    }

                }
                DateTime dDatePre = DateTime.Now.AddDays(-3);
                DateTime dDateNext = DateTime.Now.AddDays(3);
                int nDatePre = dDatePre.Year * 10000 + dDatePre.Month * 100 + dDatePre.Day;
                int nDateNext = dDateNext.Year * 10000 + dDateNext.Month * 100 + dDateNext.Day;
                RESVSHOWREQ vrResvGet = new RESVSHOWREQ();
                vrResvGet.dwBeginDate = (uint)nDatePre;
                vrResvGet.dwEndDate = (uint)nDateNext;
                int test = 2;
                if (test == 1)
                {
                    vrResvGet.dwClassKind = 8;
                }
                else if (test == 2)
                {
                    vrResvGet.dwDevKind = 605;
                    vrResvGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
                    vrResvGet.dwCheckStat = 2;
                }

                RESVSHOW[] vtReserve;
                uResponse = m_Request.Reserve.GetReserveForShow(vrResvGet, out vtReserve);
                szReacher = "";
                for (int i = 0; vtReserve != null && i < vtReserve.Length; i++)
                {
                    string szContent = vtReserve[i].szTestName.ToString();
                    string szShowContent = szContent;                    
                    szReacher += "<li>";
                    string szDevName = vtReserve[i].szDevName.ToString();
                    szReacher += "<span class=\"title\" title=\"" + szShowContent + "\">" + "" + "" + (vtReserve[i].dwPreDate % 10000) / 100 + "月" + (vtReserve[i].dwPreDate % 100) + "日:" + szContent + "</span>";
                    szReacher += "</li>";
                }

            }
        }
       
        
    }
}
