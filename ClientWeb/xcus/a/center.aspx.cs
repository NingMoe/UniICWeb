using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_Default : UniClientPage
{
    protected string resvList = "";
    protected string creditrec = "";
    protected string hideTab = "";
    protected string useInfo = "";
    protected string atyList = "";
    protected bool centerActivity = false;
    UNIACCOUNT acc;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (ToUInt(GetConfig("openActivity")) > 0 && ToUInt(GetConfig("centerActivity")) == 0)
        {
            centerActivity = true;
        }
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            uint start = ToUInt(DateTime.Now.AddMonths(-3).ToString("yyyyMMdd"));
            uint end = ToUInt(DateTime.Now.AddMonths(12).ToString("yyyyMMdd"));
            if (ToUInt(GetConfig("openActivity")) > 0)
            {
                InitOpenAty(start);
            }
            InitResv(start, end);
            if (GetConfig("userCredit") == "1")
            {
                InitCreditRec();
            }
            InitUseInfo();
            hideTab = Request["hide_tab"];
        }
        else
        {
            MsgBoxH(Translate("未登录或登录超时，请重新登录"), "location.reload();");
        }
    }

    private void InitOpenAty(uint start)
    {
        ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
        req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
        req.dwOwner = acc.dwAccNo;//河南政法要求只能参看自申请的预约
        req.dwStartDate = start;
        req.szReqExtInfo.szOrderKey = "dwActivityDate";
        req.szReqExtInfo.szOrderMode = "DESC";
        UNIACTIVITYPLAN[] rlt;
        if (m_Request.Reserve.GetActivityPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIACTIVITYPLAN plan = rlt[i];
                string atyDate = Util.Converter.UintToDateStr(plan.dwActivityDate);
                string status=(plan.dwStatus&(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK)>0?"<span class='green'>已审核</span>":"<span class='orange'>未审核</span>";
                string pStart = ((int)plan.dwBeginTime / 100).ToString("00") + ":" + ((int)plan.dwBeginTime % 100).ToString("00");
                string pEnd = ((int)plan.dwEndTime / 100).ToString("00") + ":" + ((int)plan.dwEndTime % 100).ToString("00");
                DateTime _end = DateTime.Parse(atyDate + " " + pEnd);
                DateTime _start = DateTime.Parse(atyDate + " " + pStart);
                DateTime now = DateTime.Now;
                string act = "";
                if ((plan.dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_CLOSED) == 0)
                {
                    if ((plan.dwStatus & (uint)UNIACTIVITYPLAN.DWSTATUS.ACTIVITYPLANSTAT_OPENING) > 0)
                    {
                        status += " | <span class='green'>开放中</span>";
                    }
                    else
                    {
                        status += " | <span class='red'>不开放</span>";
                    }
                    if (_start > now)
                    {
                        act += "<a class='click' onclick='delOpenAty(" + plan.dwActivityPlanID + "," + plan.dwResvID + ");'>" + Translate("删除") + "</a>";
                    }
                    if (_end > now)
                    {
                        if (act != "") act += " | ";
                        act += "<a class='click' onclick='pro.d.group.manage(\"" + Translate("维护成员") + "\", {group:" + plan.dwGroupID + "},function(){uni.reload();})'>" + Translate("成员") + "</a>";
                    }
                }
                else
                {
                    status += " | <span class='grey'>已关闭</span>";
                }
                atyList += "<tr class='it'><td>" + plan.szActivityPlanName + "</td>"
    + "<td>" + plan.szHostUnit + "</td>"
    + "<td>" + plan.szContact + "</td>"
    + "<td>" + plan.szSite + "</td>"
    + "<td>" + atyDate +" "+ pStart + "-" + pEnd + "</td>"
    + "<td class='text-center'>" + status + "</td>"
    + "<td class='text-center'>" + act + "</td></tr>";
            }
        }
    }

    private void InitUseInfo()
    {
        USERCURINFOREQ req = new USERCURINFOREQ();
        req.dwAccNo = acc.dwAccNo;
        USERCURINFO rlt;
        if (m_Request.Account.GetUserCurInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.DevInfo.dwDevID != null)
        {
            string str = "";
            if ((rlt.DevInfo.dwRunStat & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0 && rlt.DevInfo.DevUse != null && rlt.DevInfo.DevUse.Length > 0)
            {
                string deadline = DateTime.Parse(Get1970Date(rlt.DevInfo.DevUse[0].dwLeaveTime)).AddSeconds((double)rlt.DevInfo.DevUse[0].dwLeaveHoldSec).ToString("HH:mm");
                //TimeSpan span = DateTime.Now.Subtract(DateTime.Parse(Get1970Date(rlt.DevInfo.DevUse[0].dwLeaveTime)));
                str = "（请于" + deadline + "前返回 ）";
            }
            useInfo = "正在使用：<span class='bold'>" + rlt.DevInfo.szDevName + "</span>&nbsp;&nbsp;状态：" + Util.Converter.GetDevRunStat((uint)rlt.DevInfo.dwRunStat) + str;
        }
    }

    private void InitCreditRec()
    {
        CREDITREC[] list = GetCreditRecByAccNo(acc.dwAccNo);
        if (list != null)
        {
            for (int i = 0; i < list.Length; i++)
            {
                CREDITREC rec = list[i];
                //处罚
                string pulish = "<span class='grey'>"+Translate("不处罚")+"</span>";
                string location = string.IsNullOrEmpty(rec.szDevName) ? Translate("未签到使用") : rec.szLabName + "," + rec.szDevName;
                if ((rec.dwUserCStat & (uint)CREDITREC.DWUSERCSTAT.USERCSTAT_VALID) > 0 && rec.dwForbidStartDate != null && rec.dwForbidStartDate != 0)
                    pulish = "<div><span class='red'>"+ Translate("禁止预约")+"：</span>" + ToDate(rec.dwForbidStartDate) + "~" + ToDate(rec.dwForbidEndDate) + "</div>";
                creditrec += "<tr class='it'><td>" + Get1970Date((int)rec.dwOccurTime) + "</td>"
                    + "<td>" + location + "</td>"
                    + "<td>" + Translate(rec.szCTName) + "/" + Translate(rec.szCreditName) + "</td>"
                    + "<td>" + (Util.Converter.GetCreditRecState(rec.dwUserCStat)) + "</td>"
                    + "<td>" + rec.dwThisUseCScore + "</td>"
                    + "<td>" + Translate(pulish) + "</td></tr>";
            }
        }
    }
    private void InitResv(uint start, uint end)
    {
        RESVREQ req = new RESVREQ();
        req.dwBeginDate = start;
        req.dwEndDate = end;
        req.szReqExtInfo.szOrderKey = "dwOccurTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        string classkind = GetConfig("openClsKind");
        if (!string.IsNullOrEmpty(classkind) && classkind != "0") req.dwClassKind = ToUInt(classkind);
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        UNIRESERVE[] rlt;
        if (m_Request.Reserve.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                //如果预约是当天且审核不通过的显示出来，如果是历史的不在当天显示。

                UNIRESERVE resv = rlt[i];
                //预约时间
                string timespan = "";
                timespan += "<div><div><span class='grey'>" + Translate("开始") + ":</span> <span class='text-primary'>" + Get1970Date((int)rlt[i].dwBeginTime).Substring(5) + "</span></div><div><span class='grey'>" + Translate("结束") + ":</span> <span class='text-primary'>" + Get1970Date((int)rlt[i].dwEndTime).Substring(5) + "</span></div></div>";
                //组成员
                string member = "<span class='grey'>" + Translate("个人预约") + "</span>";
                if ((resv.dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0)
                {
                    member = "<span class='grey'>" + Translate("组预约") + "</span>";
                    GROUPMEMBER[] mbs = GetGroup(resv.dwMemberID);
                    if (mbs != null && mbs.Length > 0)
                    {
                        string str = "";
                        for (int j = 0; j < mbs.Length; j++)
                        {
                            str += mbs[j].szName + ",";
                        }
                        member = str.Substring(0, str.Length - 1);
                    }
                }
                //预约对象
                string objs = "";
                string location = "";
                RESVDEV[] resvDev = resv.ResvDev;
                if (resvDev != null && resvDev.Length > 0)
                {
                    string devName = string.Empty;
                    for (int j = 0; j < resvDev.Length; j++)
                    {
                        if (j == 0)
                        {
                            location = resvDev[0].szLabName;
                        }
                        devName = devName + resvDev[j].szDevName.ToString();
                    }
                    objs = devName != "" ? devName : Translate("请到现场分配");
                }
                //操作
                string act = "<span class='grey'>" + Translate("无权限") + "</span>";
                if ((resv.dwPurpose & (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY) > 0)
                {
                    act = "<span class='grey'>" + Translate("开放活动") + "</span>";
                }
                else if (resv.dwOwner == acc.dwAccNo)
                {
                    act = GetAct(resv);
                }
                string szCheckFail = "";
                if ((resv.dwStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)) > 0)
                {
                    CHECKLOGREQ checkReq = new CHECKLOGREQ();
                    checkReq.dwSubjectID = (uint)resv.dwResvID;
                    checkReq.dwSubjectType = 1;
                    CHECKLOG[] checkList;

                    if (m_Request.Admin.AdminCheckLogGet(checkReq, out checkList) == REQUESTCODE.EXECUTE_SUCCESS && checkList != null && checkList.Length > 0)
                    {
                        szCheckFail = checkList[0].szCheckDetail;
                    }
                }

                //提交时间
                string occurtime = Get1970Date((int)resv.dwOccurTime);
                string reason = (resv.dwStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)) > 0 ? ("<span class='red'>审核回复：</span>"+ szCheckFail) : "";
                string szIsOver = ((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0 ? "true" : "false");
                bool bIsBnow = (int.Parse(DateTime.Now.ToString("yyyyMMdd")) > (uint)resv.dwPreDate);
                if ((resv.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0&& bIsBnow)
                {
                    szIsOver = "true";
                }

                resvList += "<tbody date='" + occurtime + "' state='" + resv.dwStatus + "' over='" + szIsOver + "' style='display:none;'><tr class='head'><td colspan='6'>"
                    + "<h3>" + resv.szTestName + "</h3><span>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</span><span class='pull-right'>"
                                + "<span class='grey'>" + occurtime + "</span></span></td></tr>"
                                + "<tr class='content'><td><div class='box'><a>" + objs + "</a><div class='grey'>" + location + "</div</div></td>"
                                    + "<td>" + resv.szOwnerName + "</td>"
                                    + "<td>" + member + "</td>"
                                    + "<td>" + timespan + "</td>"
                                    + "<td><div>" + Util.Converter.ResvStatusWithCheck(resv.dwStatus, true) + "</div><div style='font-size:12px;color:#777;'>"+reason+"</div></td>"
                                    + "<td class='text-center' style='vertical-align: middle;'>" + act + "</td></tr></tbody>";
            }
            if (rlt.Length == 0)
            {
                resvList = "<tbody><tr><td colspan='6' class='text-center'>" + Translate("没有数据") + "</td></tr></tbody>";
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }
    GROUPMEMBER[] GetGroup(uint? id)
    {
        GROUPREQ req = new GROUPREQ();
        req.dwGroupID = id;
        req.dwReqProp = (uint)GROUPREQ.DWREQPROP.GROUPREQ_NEEDDEL;
        UNIGROUP[] rlt;
        if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            return rlt[0].szMembers;
        }
        return null;
    }
    private string GetAct(UNIRESERVE rsv)
    {
        string act = "";
        DateTime end = Util.Converter.Get1970DateTime((int)rsv.dwEndTime);
        DateTime start = Util.Converter.Get1970DateTime((int)rsv.dwBeginTime);
        DateTime now = DateTime.Now;
        DateTime deadline = DateTime.Now.AddMinutes(ToUInt(GetConfig("alterDeadline")));
        uint uOpKind = ToUInt(GetConfig("centerOPGroup"));
        if (!((rsv.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0))   
        {
            if (start > deadline && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
            {
                act += "<a class='click' rsvId='" + rsv.dwResvID + "' onclick='delRsv(this);'>" + Translate("删除") + "</a>";
                RESVDEV[] resvDev = rsv.ResvDev;
                string devId = "";
                string devKind = "";
                if (resvDev.Length > 0)
                {
                    uint? sn = resvDev[0].dwDevStart;
                    if (sn != null && sn > 0 && resvDev[0].dwDevNum > 0)
                    {
                        UNIDEVICE dev = GetDevBySN(sn, resvDev[0].dwRoomID.ToString());
                        if (dev.dwDevID != null) devId = dev.dwDevID.ToString();
                    }
                    else
                        devKind = resvDev[0].dwDevKind.ToString();
                }
                if ((uOpKind & 1) > 0)
                {
                    act += " | <a rsvId='" + rsv.dwResvID + "' devId='" + devId + "' devKind='" + devKind + "' start='" + Get1970Date((int)rsv.dwBeginTime) + "' end='" + Get1970Date((int)rsv.dwEndTime) + "' onclick='alterRsv(this);'>" + Translate("修改") + "</a>";
                }
            }
            else
            {
                if (((rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))
                {
                    act = "<span class='grey'>" + Translate("删除") + "</span> | <span class='grey'>" + Translate("修改") + "</span>";
                }
            }
            if (start < now && end > now && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0 && GetConfig("showResvFinish") == "1")
            {

                act += "<br/><a class='click' onclick='pro.j.rsv.finish(\"" + rsv.dwResvID + "\");'>" + Translate("提前结束") + "</a>";
            }
            if (end > now && (rsv.dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0 && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) == 0)
            {
                    if (((rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))
                    {
                        if ((uOpKind & 2) > 0)
                        {
                            act += "<br/><button type='button' class='btn btn-info btn-xs' onclick='pro.d.group.manage(\"" + Translate("维护预约组") + "\", {width: 620, group:" + rsv.dwMemberID + "},function(){uni.reload();})'>" + Translate("成员管理") + "</button>";
                        }
                    }
            }
        }
        return act;
    }
    private string ToDate(uint? dt)
    {
        if (dt == null) return "";
        uint? y = dt / 10000;
        uint? m = (dt / 100) % 100;
        uint? d = dt % 100;
        return y + "/" + m + "/" + d;
    }
}