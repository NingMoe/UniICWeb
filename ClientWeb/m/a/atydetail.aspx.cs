using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_openaty : UniClientPage
{
    protected string image = "";
    protected string atyDetails = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPreAty();
    }
    private void InitPreAty()
    {
        ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
        UNIACTIVITYPLAN[] rlt;
        req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        req.szGetKey = Request["id"];
        req.dwStartDate = ToUInt(DateTime.Now.AddYears(-2).ToString("yyyyMMdd"));
        req.szReqExtInfo.szOrderKey = "dwActivityDate";
        req.szReqExtInfo.szOrderMode = "DESC";
        uint now = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        if (m_Request.Reserve.GetActivityPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            UNIACTIVITYPLAN plan = rlt[0];
            uint stat = (uint)plan.dwStatus;
            if ((stat & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
            {
                string status = "";
                uint dwPublishDate = (uint)plan.dwPublishDate;
                uint dwEnrollDeadline = (uint)plan.dwEnrollDeadline;
                uint dwActivityDate = (uint)plan.dwActivityDate;
                string startDate = Util.Converter.UintToDateStr(plan.dwActivityDate);
                string szResvTime = ((int)plan.dwBeginTime / 100) + ":" + ((int)plan.dwBeginTime % 100).ToString("00") + "-" + ((int)plan.dwEndTime / 100) + ":" + ((int)plan.dwEndTime % 100).ToString("00");
                if (!string.IsNullOrEmpty(plan.szActivityPlanURL))
                {
                    image = "<img style=\"width:100%\" src =\"" + GetClientUrl() + "upload/UpLoadFile/" + plan.szActivityPlanURL + "\" />";
                }
                atyDetails += "<div class='aty_detail_title'><h2> " + status + plan.szActivityPlanName + "</h2><div class='line'></div>";
                if (dwActivityDate == 20990101)//时间待定
                {
                    atyDetails += "<h4 class='aty_date'>待定</h4>";
                }
                else
                {
                    atyDetails += "<h4 class='aty_date'> <span class='glyphicon glyphicon-time'></span>&nbsp;" + startDate + " " + szResvTime + "</h4>";
                }
                atyDetails += "</div><div class=\"detail\">";
                atyDetails += "<table style=\"width:100%;\"><tbody class='aty_detail_list'>";
                atyDetails += "<tr><td class='uni_trans' style=\"width:80px\">活动主题</td><td>" + plan.szActivityPlanName.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>主办单位</td><td>" + plan.szHostUnit.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>承办单位</td><td>" + plan.szOrganizer.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>主讲人</td><td>" + plan.szPresenter.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>参与者要求</td><td class='user_require'>" + plan.szDesiredUser.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>活动介绍</td><td class='aty_intro'>" + plan.szIntroInfo + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>联系人</td><td>" + plan.szContact.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>联系人电话</td><td>" + plan.szHandPhone.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>限制报名人数</td><td>" + plan.dwMaxUsers.ToString() + "</td></tr>";
                atyDetails += "<tr><td class='uni_trans'>已申请人数</td><td>" + plan.dwEnrollUsers.ToString() + "</td></tr>";
                if (dwActivityDate != 20990101)
                {
                    if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                        atyDetails += "<tr><td class='uni_trans'>加入截止日期</td><td class='deadline red'>" + Util.Converter.UintToDateStr(dwEnrollDeadline) + " ("+Translate("不含当日")+")</td></tr>";
                    else if ((stat & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                        atyDetails += "<tr><td class='uni_trans'>加入截止日期</td><td class='deadline red uni_trans'>不开放</td></tr>";
                    else
                        atyDetails += "<tr><td class='uni_trans'>加入截止日期</td><td class='deadline red uni_trans'>已结束</td></tr>";

                    atyDetails += "<tr><td class='uni_trans'>活动安排时间</td><td>" + startDate + "  " + szResvTime + "</td></tr>";
                }
                else
                {
                    atyDetails += "<tr><td class='uni_trans'>加入截止日期</td><td class='deadline red uni_trans'>待定</td></tr>";
                    atyDetails += "<tr><td class='uni_trans'>活动安排时间</td><td class='uni_trans'>待定</td></tr>";
                }
                atyDetails += "<tr><td class='uni_trans'>主办地点</td><td>" + plan.szSite.ToString() + "</td></tr></tbody></table>";
                atyDetails += "<div class='aty_act content-block'><p class='text-center'>";
                if (dwActivityDate != 20990101)
                {
                    if (((uint)plan.dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) != 0)
                    {
                        if (((uint)plan.dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_ENROLLED) == 0)
                        {
                            if (dwEnrollDeadline > ToUInt(DateTime.Now.ToString("yyyyMMdd")))
                                atyDetails += "<a href='#' class=\"btn_mb button button-big button-fill\" purpose=\"in\" gid=\"" + plan.dwGroupID.ToString() + "\" >加入活动</a>";
                            else
                                atyDetails += "<span class='red uni_trans'>已截止加入</span>";
                        }
                        else
                        {
                            atyDetails += "<a href='#' class=\"btn_mb button button-big button-fill\" purpose=\"out\" gid=\"" + plan.dwGroupID.ToString() + "\" >退出活动</a>";
                        }
                    }
                }
                atyDetails += "</p></div></div></div></div><div>";
            }
        }
    }
}