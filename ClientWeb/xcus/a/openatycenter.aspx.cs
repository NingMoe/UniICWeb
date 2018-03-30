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
using System.Text.RegularExpressions;
using System.Reflection;
using UniWebLib;

public partial class ClientWeb_xcus_all_openaty_center : UniClientPage
{
    protected string preAty = "";
    protected string szPageNation = "";
    private uint pageRows = 10;
    protected string url="";
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
        IsLogined();
        InitPreAty();
        //if (IsLogined())
        //    InitPreAty();
        //else
        //    MsgBoxH("未登录或登录超时，请重新登录", "location.reload();");
    }
    private void InitPreAty()
    {
        uint pageNeed = 0;
        if (Session["activityPage"] != null)
        {
            pageNeed = uint.Parse(Session["activityPage"].ToString());
        }
        ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
        UNIACTIVITYPLAN[] vrResult;
        vrParameter.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
        //显示类型 预告或回顾
        string type =   Request["type"];
        if (type == "pre")
        {
            //pageNeed = 0;
            vrParameter.dwStartDate = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
            vrParameter.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
            vrParameter.szReqExtInfo.szOrderMode = "ASC";
        }
        else if (type == "old")
        {
            vrParameter.dwStartDate = ToUInt(DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"));
           // vrParameter.dwStatus = (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED;
            vrParameter.szReqExtInfo.szOrderMode = "DESC";
            
        }
        else
        {
            return;
        }
      //  vrParameter.szReqExtInfo.dwStartLine = pageNeed * pageRows;
      //  vrParameter.szReqExtInfo.dwNeedLines = pageRows;
      
        uint now = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        vrParameter.szReqExtInfo.szOrderKey = "dwActivityDate";

        if (m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<UNIACTIVITYPLAN> actList = new List<UNIACTIVITYPLAN>();
            int count = 0;
            int countTotal = 0;

            for (uint i = 0; i < vrResult.Length; i++)
            {
                uint uStatueTemp = (uint)vrResult[i].dwStatus;
                if (type == "old")
                {
                    if ((uStatueTemp & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0
                        && vrResult[i].dwActivityDate < now)
                    {
                        actList.Add(vrResult[i]);
                        countTotal = countTotal + 1;
                    }
                    else
                        continue;
                }
                else
                {
                    if ((uStatueTemp & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                    {
                        actList.Add(vrResult[i]);
                        countTotal = countTotal + 1;
                    }
                    else if ((uStatueTemp & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                    {
                        actList.Add(vrResult[i]);
                        countTotal = countTotal + 1;
                    }
                    else
                        continue;
                }
            }

            vrResult = actList.ToArray();
            actList = null;
            actList = new List<UNIACTIVITYPLAN>();
            for (uint i = pageNeed * pageRows; count < pageRows && i < vrResult.Length; i++)
            {
                uint uStatueTemp = (uint)vrResult[i].dwStatus;
                if (type == "old")
                {//过滤非过期
                    if ((uStatueTemp & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0
                        && vrResult[i].dwActivityDate < now)
                    {
                        actList.Add(vrResult[i]);
                        count = count + 1;
                    }
                    else
                        continue;
                }
                else
                {
                    if ((uStatueTemp & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                    {
                        actList.Add(vrResult[i]);
                        count = count + 1;
                    }
                    else if ((uStatueTemp & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                    {
                        actList.Add(vrResult[i]);
                        count = count + 1;
                    }
                    else
                        continue;
                }
            }
       
            
            vrResult = actList.ToArray();

            uint nTotalLine = 0;
            PRModule ActionModule = new PRModule();
            REQEXTINFO ext;
            ActionModule = m_Request.Reserve;
            if (ActionModule.UTPeekDetail(out ext))
            {
                nTotalLine = (uint)ext.dwTotolLines;
            }
            int pages = (int)nTotalLine / (int)pageRows;
            if (pages > 10)
            {
                pages = 10;
            }
            if (vrResult.Length % pageRows > 0)
            {
                pages = pages + 1;
            }
            int upage = (int)(countTotal / pageRows);
            if ((countTotal / pageRows) > 0)
            {
                upage = upage + 1;
            }
            for (int i =1; i<=upage; i++)
            {
                if ((i) == (pageNeed+1))
                {
                    szPageNation = szPageNation + " <span class=\"active\">" + (i) + "</span>";
                }
                else
                {
                    szPageNation = szPageNation + "<a class=\"pageset\" href=\"javascript:;\" data-page=" + (i-1).ToString() + ">" + (i) + "</a>";
                }

            }
            for (int i = 0; i < vrResult.Length; i++)
            {
                string szRepory = "";
                uint uStatue = (uint)vrResult[i].dwStatus;
                bool isChooseSeat = (vrResult[i].dwCheckRequirment & (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_SUPPORTSEAT) > 0;
                uint dwPublishDate = (uint)vrResult[i].dwPublishDate;
                uint dwEnrollDeadline = (uint)vrResult[i].dwEnrollDeadline;
                uint dwActivityDate = (uint)vrResult[i].dwActivityDate;
                string startDate = Util.Converter.UintToDateStr(vrResult[i].dwActivityDate);
                string szResvTime = ((int)vrResult[i].dwBeginTime / 100) + ":" + ((int)vrResult[i].dwBeginTime % 100).ToString("00") + "-" + ((int)vrResult[i].dwEndTime / 100) + ":" + ((int)vrResult[i].dwEndTime % 100).ToString("00");
                if (!string.IsNullOrEmpty(vrResult[i].szActivityPlanURL))
                {
                    szRepory = "<img style=\"max-width:740px;\" src =\"" + GetClientUrl() + "upload/UpLoadFile/" + vrResult[i].szActivityPlanURL + "\" />";
                }
                string status = "";
                if (type == "old")
                {//过滤非过期
                    if ((uStatue & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0
                        && vrResult[i].dwActivityDate < now)
                        status = "<span class='grey'>【已关闭】</span>";
                    else
                        continue;
                }
                else
                {
                    if ((uStatue & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                        status = "<span class='green'>【开放中】</span>";
                    else if ((uStatue & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                        status = "<span class='red'>【不开放】</span>";
                    else
                        continue;
                }
                preAty += "<h3>" + status + vrResult[i].szActivityPlanName + "<span class='pull-right'>" + startDate + "</span></h3><div class='aty_detail'><div><div>";
                preAty += "<h2>" + vrResult[i].szActivityPlanName.ToString() + "</h2>";
                if (dwActivityDate == 20990101)//时间待定
                {
                    preAty += "<h4 class='aty_date'>" + "待定" + "</h4>";
                }
                else
                {
                    preAty += "<h4 class='aty_date'><span class='glyphicon glyphicon-time'></span>&nbsp;" + startDate + " " + szResvTime + "</h4>";
                }
                preAty += "</div><div class=\"detail\">";
                preAty += "<table style=\"margin:12px;width:100%;\"><tbody>";
                preAty += "<tr class='aty_img'><td colspan=\"2\">" + szRepory + "</td></tr></tbody><tbody class='aty_detail_list'>";
                preAty += "<tr><td style=\"width:100px\">活动主题</td><td>" + vrResult[i].szActivityPlanName.ToString()+ "</td></tr>";
                preAty += "<tr><td>主办单位</td><td>" + vrResult[i].szHostUnit.ToString() + "</td></tr>";
                preAty += "<tr><td>承办单位</td><td>" + vrResult[i].szOrganizer.ToString() + "</td></tr>";
                preAty += "<tr><td>主讲人</td><td>" + vrResult[i].szPresenter.ToString() + "</td></tr>";
                preAty += "<tr><td>参与者要求</td><td class='user_require'>" + vrResult[i].szDesiredUser.ToString() + "</td></tr>";
                preAty += "<tr><td>活动介绍</td><td class='aty_intro'>" + vrResult[i].szIntroInfo + "</td></tr>";
                preAty += "<tr><td>联系人</td><td>" + vrResult[i].szContact.ToString() + "</td></tr>";
                preAty += "<tr><td>联系人电话</td><td>" + vrResult[i].szHandPhone.ToString() + "</td></tr>";
                preAty += "<tr><td>限制报名人数</td><td>" + vrResult[i].dwMaxUsers.ToString() + "</td></tr>";
                preAty += "<tr><td>已申请人数</td><td>" + vrResult[i].dwEnrollUsers.ToString() + "</td></tr>";
                if (dwActivityDate != 20990101)
                {
                    if ((uStatue & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                        preAty += "<tr><td>申请加入截止日期</td><td class='deadline red'>" + Util.Converter.UintToDateStr(dwEnrollDeadline) + " (不含当日)</td></tr>";
                    else if ((uStatue & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_UNOPEN) > 0)
                        preAty += "<tr><td>申请加入截止日期</td><td class='deadline red'>不开放</td></tr>";
                    else
                        preAty += "<tr><td>申请加入截止日期</td><td class='deadline red'>已结束</td></tr>";

                    preAty += "<tr><td>活动安排时间</td><td>" + startDate + "  " + szResvTime + "</td></tr>";
                }
                else
                {
                    preAty += "<tr><td>申请加入截止日期</td><td class='deadline red'>待定</td></tr>";
                    preAty += "<tr><td>活动安排时间</td><td>待定</td></tr>";
                }
                preAty += "<tr><td>主办地点</td><td>" + vrResult[i].szSite.ToString() + "</td></tr></tbody>";
                preAty += "<tbody class='aty_act'><tr><td colspan='2'><div class='line'></div>";
                if (dwActivityDate != 20990101)
                {
                    if (((uint)vrResult[i].dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) != 0)
                    {
                        if (((uint)vrResult[i].dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_ENROLLED) == 0)
                        {
                            if (dwEnrollDeadline > ToUInt(DateTime.Now.ToString("yyyyMMdd")))
                                preAty += "<button style='button' class=\"btn_mb btn btn-warning\" purpose=\"" + (isChooseSeat ? "seat" : "in") + "\" roomid=\"" + vrResult[i].dwRoomID + "\" atyid=\"" + vrResult[i].dwActivityPlanID.ToString() + "\" >加入活动</button></td></tr>";
                            else
                                preAty += "<span class='red'>已截止加入</span>";
                        }
                        else
                        {
                            preAty += "<div class='btn-group' role='group'><button style='button' class=\"btn_mb btn btn-warning\" purpose=\"out\" atyid=\"" + vrResult[i].dwActivityPlanID.ToString() + "\" >退出活动</button>";
                            if (isChooseSeat)
                            {
                                preAty += "<button style='button' class=\"btn_mb btn btn-info\"  purpose=\"seat\" roomid=\"" + vrResult[i].dwRoomID + "\" atyid=\"" + vrResult[i].dwActivityPlanID.ToString() + "\">选择座位</button>";
                            }
                            preAty += "</div></td></tr>";
                        }
                    }
                }
                preAty += "</tbody></table></div></div></div>";
            }
        }
        else
        {
            preAty = m_Request.szErrMessage;
        }
        if (preAty == "")
        {
            preAty = "没有活动";
        }
    }
}
