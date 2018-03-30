using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_openaty : UniClientPage
{
    protected string newList = "";
    protected string oldList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPreAty();
    }
    private void InitPreAty()
    {
        ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
        UNIACTIVITYPLAN[] rlt;
        req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
        req.dwStartDate = ToUInt(DateTime.Now.AddYears(-2).ToString("yyyyMMdd"));
        req.szReqExtInfo.szOrderKey = "dwActivityDate";
        req.szReqExtInfo.szOrderMode = "DESC";
        uint now = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        if (m_Request.Reserve.GetActivityPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIACTIVITYPLAN plan = rlt[i];
                uint stat = (uint)plan.dwStatus;
                if ((stat & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
                {
                    string date = Util.Converter.UintToDateStr(plan.dwActivityDate);
                    string status = "";
                    if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                    {
                        status = "<span class='green'>|</span>";
                    }
                    else if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                    {
                        status = "<span class='red'>|</span>";
                    }
                    else
                    {
                        status = "<span class='grey'>|</span>";
                    }
                    if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED) > 0
                        && plan.dwActivityDate < now)
                    {
                        oldList += "<li><a class='item-content item-link' onclick=\"loginedLoad('../a/atydetail.aspx?id=" + plan.dwActivityPlanID + "&_t='+(new Date()).getTime())\"><div class='item-inner'><div class='item-title'>" + plan.szActivityPlanName + "</div><div class='item-after'>" + date + "</div></div></a></li>";
                    }
                    if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED) == 0
                        && plan.dwActivityDate >= now)
                    {
                        newList += "<li><a class='item-content item-link' onclick=\"loginedLoad('../a/atydetail.aspx?id=" + plan.dwActivityPlanID + "&_t='+(new Date()).getTime())\"><div class='item-inner'><div class='item-title'>" + status + plan.szActivityPlanName + "</div><div class='item-after'>" + date + "</div></div></a></li>";
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        if (newList == "")
            newList = "<li class='text-center uni_trans'>没有活动</li>";
        if (oldList == "")
            oldList = "<li class='text-center uni_trans'>没有活动</li>";
    }
}