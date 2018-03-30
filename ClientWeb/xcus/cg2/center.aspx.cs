using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_Default : UniClientPage
{
    protected string resvList = "";
    protected string delResvList = "";
    protected string detailList = "";
    protected string feedback="";
    protected string delList = "";
    protected string timeDetail = "";
    protected string secondList;
    protected string atyList;
    protected UNIACCOUNT acc;
    protected string svcArray = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            
            DateTime start = DateTime.Now.AddMonths(-3);
            DateTime end = DateTime.Now.AddMonths(12);
            InitYardResv(ToUInt(start.ToString("yyyyMMdd")), ToUInt(end.ToString("yyyyMMdd")));
            InitDelYardResv();
            InitFeedback();
            GetAtyList();
            GetSecondList();
        }
        else
        {
            MsgBoxH("未登录或登录超时，请重新登录", "location.reload();");
        }
    }

    private void InitFeedback()
    {
        USERFEEDBACKREQ req = new USERFEEDBACKREQ();
        req.dwAccNo = acc.dwAccNo;
        req.szReqExtInfo.szOrderKey = "dwOccurTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-60).ToString("yyyyMMdd"));//5年内
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        USERFEEDBACK[] rlt;
        if (m_Request.Admin.GetUserFeedback(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                USERFEEDBACK fb=rlt[i];
                YARDRESV resv = GetYardResvById(fb.dwResvID);
                string sc = "★★★★★";
                if (resv.dwResvID == null) continue;

                string timespan = resv.szCycRule;
                if (string.IsNullOrEmpty(timespan))//非周期预约
                {
                    timespan = Get1970Date((int)resv.dwBeginTime) + "至" + Get1970Date((int)resv.dwEndTime).Substring(11);
                }
                feedback += "<tr><td><ul><li class='title'><span class='text-primary'>" + resv.szResvName + "</span><span class='score'>" + sc.Substring(0, (int)fb.dwScore) + "</span><span class='grey pull-right'>" + Get1970Date((int)fb.dwOccurTime) + "</span>"
                    + "</li><li class='feedback'><p>" + fb.szIntroInfo + "</p></li>"+(fb.szReplyInfo==""?"":"<li class='replay'><div class='grey'>"+fb.szAnswerer+" &nbsp;回复：</div><p>"+fb.szReplyInfo+"</p><div class='grey'>"+Util.Converter.UintToDateStr(fb.dwReplyDate)+"</div></li>")
                    + "<li class='detail'><span class='pull-right'>场地：<span class='text-primary'>" + fb.szDevName + "</span>活动时间： <span class='grey'>" +timespan+ "</span></span></li></ul></td></tr>";
            }
        }
        else
            MsgBoxH(m_Request.szErrMsg);
    }
    private void InitYardResv(uint start, uint end)
    {
        YARDRESVREQ req = new YARDRESVREQ();
        req.dwBeginDate = start;
        req.dwEndDate = end;
        req.dwApplicantID = acc.dwAccNo;
        req.szReqExtInfo.szOrderKey = "dwResvGroupID DESC,OccurTime";
        req.szReqExtInfo.szOrderMode = "ASC";
        //req.dwReqProp = (uint)YARDRESVREQ.DWREQPROP.YARDREQ_ONLYMAINRESV;
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        YARDRESV[] rlt;
        if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            CHECKTYPE[] mantypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN);
            CHECKTYPE[] dicttypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
            CHECKTYPE[] servtypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
            //获取服务列表
            for (int i = 0; i < servtypes.Length; i++)
            {
                svcArray +=servtypes[i].dwCheckKind+",";
            }
            if (svcArray.Length > 0) svcArray = svcArray.Substring(0, svcArray.Length - 1);
            //
            string delList = "";
            for (int i = 0; i < rlt.Length;i++)
            {
                YARDRESV resv = rlt[i];
                string groupId = resv.dwResvGroupID.ToString();
                bool over = (resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0;
                //预约明细
                if (i == 0 || rlt[i - 1].dwResvGroupID != resv.dwResvGroupID)
                {
                    delList = resv.dwResvID.ToString();
                    timeDetail += (i == 0 ? "" : "</tbody></table></div>") + "<div id='timedetail_" + groupId + "'><table class='timedetail_info state_info uni_sort_tbl'><thead><tr><th class='sort_asc'>时间</th></tr></thead><tbody>";
                    //timeDetail += (i == 0 ? "" : "</tbody></table></div>") + "<div id='timedetail_" + groupId + "'><table class='timedetail_info state_info uni_sort_tbl'><thead><tr><th>预约号</th><th class='sort_asc'>时间</th><th>状态</th><th class='no_sort'>操作</th></tr></thead><tbody>";
                }
                else
                {
                    delList += "," + resv.dwResvID;
                }
                timeDetail += "<tr class='tdetail_tr_" + resv.dwResvID + "'><td>" + Get1970Date((int)resv.dwBeginTime) + "至" + Get1970Date((int)resv.dwEndTime).Substring(11) + "</td></tr>" + (i == rlt.Length - 1 ? "</tbody></table></div>" : "");
//                timeDetail += "<tr class='tdetail_tr_"+resv.dwResvID+"'><td>" + resv.dwResvID + "</td><td>" + Get1970Date((int)resv.dwBeginTime) + "至" + Get1970Date((int)resv.dwEndTime).Substring(11) + "</td><td>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>" +
//"<td class='text-center resv_act'>" + GetResvAct(resv) + "</td></tr>" + (i == rlt.Length - 1 ? "</tbody></table></div>" : "");
                if (i < rlt.Length - 1 && rlt[i + 1].dwResvGroupID == resv.dwResvGroupID)//非同组最后一个
                {
                    continue;
                }
                string timespan = resv.szCycRule;
                bool isCycle = true;
                if (i == 0 || rlt[i - 1].dwResvGroupID != resv.dwResvGroupID)//非周期预约
                {
                    isCycle = false;
                    timespan = Get1970Date((int)resv.dwBeginTime) + "至" + Get1970Date((int)resv.dwEndTime).Substring(11);
                }
                string occurtime = Get1970Date((int)resv.dwOccurTime);
                //申请模版
                string applyTbl = "apply";
                //申请模版
                if ((resv.dwSecurityLevel & 0x20000000) > 0)//会议模版
                {
                    applyTbl = "apply_hy";
                }

                string man="";
                string dict="";
                string service="";
                bool noCheck = true;//未审核
                if (over)
                {
                    man = "<span class='grey'>见【审核详情】</span>";
                    dict = "<span class='grey'>见【审核详情】</span>";
                    service = "<span class='grey'>见【审核详情】</span>";
                }
                else
                {
                    string msg;
                    YARDRESVCHECKINFO[] ckInfo = GetResvCheckInfo(resv.dwResvID.ToString(), out msg);
                    for (int j = 0; j < ckInfo.Length; j++)
                    {
                        YARDRESVCHECKINFO info = ckInfo[j];
                        if (noCheck && (info.dwCheckStat & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) noCheck = false;
                        CHECKTYPE mantype = IsInAarray(info.dwCheckKind, mantypes);
                        CHECKTYPE dicttype = IsInAarray(info.dwCheckKind, dicttypes);
                        CHECKTYPE servtype = IsInAarray(info.dwCheckKind, servtypes);
                        if (mantype.dwCheckKind != null)
                            man = resv.szDeptName + "<br/>" + Util.Converter.GetCheckState(info.dwCheckStat);
                        else if (dicttype.dwCheckKind != null)
                        {
                            string name = dicttype.szDeptName;
                            if (string.IsNullOrEmpty(name.Trim())) name = acc.szDeptName;
                            //if (dicttype.dwDeptID != null || dicttype.dwDeptID != 0) name = acc.szDeptName;
                            dict = name + "<br/>" + Util.Converter.GetCheckState(info.dwCheckStat);
                        }
                        else if (servtype.dwCheckKind != null)
                            service += info.szCheckName + "<br/>";//"(" + Util.Converter.GetCheckState(info.dwCheckStat) + ")<br/>"
                    }
                }
                resvList += "<tbody date='" + occurtime + "' over='" + (over?"true":"false") + "' style='display:none;'>"+
                    "<tr class='head'><td colspan='6'><h3>" + resv.szResvName + "<small>" + ((resv.dwProperty & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN) > 0 ? "(不公开)" : "(公开)") + "</small></h3>"
                                + "<span>状态：<strong class='green'>" + Util.Converter.ResvStatusWithCheck(resv.dwStatus) + "</strong> </span><span class='pull-right'>"
                                + "<span class='grey'>单号:" + groupId + " </span><a class='activity_detail' resv_id='" + groupId + "'>详情</a></span></td></tr>"
                                + "<tr class='content'><td><div class='box'><span class='load_apply_detail'>" + resv.szDevName + "</span><br/><a target='_blank' href='applydetail.aspx?resv_id=" + groupId + "' >详细</a> | <a onclick='copyApply(\"" + groupId + "\",\""+resv.dwActivitySN+"\",\""+applyTbl+".aspx\")'>复制申请</a></div></td>"
                                    + "<td>" +  dict + "</td>"
                                    + "<td>" + man + "</td>"
                                    + "<td>" + timespan + "<button type='button' class='btn btn-info btn-xs time_detail "+(isCycle?"":"hidden")+"' resv_id='" + groupId + "' no_check='"+noCheck+"'><<时间明细&nbsp;</button></td>"
                                    + "<td>服务：<br /><span class='part'>"+service+"</span></td>"
                                    + "<td class='text-center'><div style='margin: 5px auto;'>" + GetAct(resv,delList,noCheck) + "</div><div>"
                                    + "<button type='button' class='btn btn-info btn-xs check_state' resv_id='" + resv.dwResvID + "'>&nbsp;审核详情&nbsp;</button></div></td></tr></tbody>";
                detailList += "<div id='detail_" + groupId + "'><table class='detail_info'>"
                                  + "<tr><td>活动场景</td><td>" + resv.szActivityName + "</td></tr>"
                                            + "<tr><td>组织方</td><td>" + resv.szOrganization + "</td></tr>"
                                            + "<tr><td>人数规模</td><td>" + resv.dwMaxAttendance + "人 </td></tr>"
                                            + "<tr><td>校区</td><td>" + resv.szCampusName + "</td></tr>"
                                            + "<tr><td>申请时间</td><td>" + occurtime + "</td></tr>"
                                            + "<tr><td>是否盈利</td><td>"+((resv.dwProperty&(uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT)>0?"营利":"非营利")+"</td></tr></table></div>";
            }
            if (rlt.Length == 0)
            {
                resvList = "<tbody><tr><td colspan='6' class='text-center'>没有数据</td></tr></tbody>";
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
        //删除明细
    //    req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL;
    //    req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"));
    //    if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
    //    {
    //        for (int i = 0; i < rlt.Length; i++)
    //        {
    //            YARDRESV resv = rlt[i];
    //            delList += "<tr class='it'><td>" + resv.dwResvID + "</td><td>" + resv.szResvName + "</td><td>" + resv.szDevName + "</td>" +
    //"<td>" + resv.szDeptName + "</td><td class='text-center'>" + Get1970Date((int)resv.dwBeginTime).Substring(5) + "至" + Get1970Date((int)resv.dwEndTime).Substring(5) + "</td>" +
    //"<td class='text-center'>" + Get1970Date((int)resv.dwOccurTime) + "</td></tr>";
    //        }
    //        if (rlt.Length == 0)
    //        {
    //            resvList = "<tbody><tr><td colspan='6' class='text-center'>没有数据</td></tr></tbody>";
    //        }
    //    }
    //    else
    //    {
    //        MsgBoxH(m_Request.szErrMsg);
    //    }
    }
    private CHECKTYPE IsInAarray(uint? type, CHECKTYPE[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (type == arr[i].dwCheckKind)
                return arr[i];
        }
        return new CHECKTYPE();
    }

    private void InitDelYardResv()
    {
        YARDRESVREQ req = new YARDRESVREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-24).ToString("yyyyMMdd"));//2年
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        req.dwApplicantID = acc.dwAccNo;
        req.szReqExtInfo.szOrderKey = "dwOccurTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL;
        YARDRESV[] rlt;
        if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                YARDRESV resv = rlt[i];
                if (!IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO))
                    continue;
                string id = resv.dwResvID.ToString();
                string timespan = rlt[i].szCycRule;
                string occurtime = Get1970Date((int)resv.dwOccurTime);
                string cause = resv.szHostUnit;
                if (cause == "") cause = "主动删除";
                delResvList += "<tr class='it'><td>" + resv.dwResvGroupID + "</td><td>" + resv.szResvName + "</td><td>" + resv.szDevName + "</td><td class='text-center'>" + occurtime + "</td>" +
                    "<td class='text-center'>" + Get1970Date((int)resv.dwBeginTime) + "-" + Get1970Date((int)resv.dwEndTime).Substring(11) + "</td>" +
                    "<td>"+cause+"</td></tr>";
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }
    private string GetService(YARDRESV activity){
        string checkService = "";
        CHECKTYPE[] rlt;
        rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
        for (int k = 0; k < rlt.Length; k++)
        {
            checkService+=rlt[k].szCheckName + ((k<rlt.Length-1)?",":"");
        }
        return checkService;
    }
    private string GetAct(YARDRESV resv,string delList,bool noCheck)
    {
        string rlt = " | <a>评价</a>";
        if (IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO))
        {
            rlt = "<a onclick='delGResv(\"" + delList + "\",\"" + resv.szResvName + "\",this)'>撤销</a>";
        }
        else
        {
            rlt = "<span class='grey'>撤销</span>";
        }
        if (IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE))
        {
            if (IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_NOFEED))
                rlt += " | <a class='click' onclick='feedback(\"" + resv.dwResvID + "\",\"" + resv.dwDevID + "\",\"" + resv.szResvName + "\")'>评价</a>";
            else
            {
                rlt += " | <span class='orange'>已评</span>";
            }
        }
        else
            rlt += " | <span class='grey'>评价</span>";
        return rlt;
    }
    private string GetResvAct(YARDRESV rsv)
    {
        string act = "";
        DateTime end = Util.Converter.Get1970DateTime((int)rsv.dwEndTime);
        DateTime start = Util.Converter.Get1970DateTime((int)rsv.dwBeginTime);
        DateTime now = DateTime.Now;
        if (start > now && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
        {
            act += "<a class='click' rsvId='" + rsv.dwResvID + "' onclick='delRsv(this);'>" + Translate("删除") + "</a>";
            act += " | <a rsvId='" + rsv.dwResvID + "' devId='" + rsv.dwDevID + "' devKind='" + rsv.dwDevKind + "' start='" + Get1970Date((int)rsv.dwBeginTime) + "' end='" + Get1970Date((int)rsv.dwEndTime) + "' onclick='alterRsv(this);'>" + Translate("修改") + "</a>";
        }
        else
            act = "<span class='grey'>" + Translate("删除") + "</span> | <span class='grey'>" + Translate("修改") + "</span>";
        if (start < now && end > now && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0 && GetConfig("showResvFinish") == "1")
        {
            act += "<br/><a class='click' onclick='pro.j.rsv.finish(\"" + rsv.dwResvID + "\");'>" + Translate("提前结束") + "</a>";
        }
        return act;
    }

    private void GetAtyList()
    {
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        YARDACTIVITY[] rlt;
        REQUESTCODE cd = m_Request.Reserve.GetYardActivity(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (rlt[i].dwSecurityLevel == 1) continue;//不开放
                if ((rlt[i].dwSecurityLevel & 0x20000000) > 0)//会议模版
                {
                    continue;
                }
                atyList += "<option value='" + rlt[i].dwActivitySN + "'>" + rlt[i].szActivityName + "</option>";
            }
        }

    }

    private void GetSecondList()
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        THIRDRESVREQ req = new THIRDRESVREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"));
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(12).ToString("yyyyMMdd"));
        req.szPID = acc.szPID;
        req.szReqExtInfo.szOrderKey = "dwResvDate";
        req.szReqExtInfo.szOrderMode = "DESC";
        THIRDRESV[] rlt;
        if (m_Request.Reserve.GetThirdResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                THIRDRESV resv = rlt[i];
                string status = "";
                string act = "";
                if (string.IsNullOrEmpty(resv.szAssertSN) || resv.dwResvID == 0)
                {
                    status = "<span class='orange'>未预约</span>";
                    act = "<a class='click second_act' third_id=" + resv.dwThirdResvID + ">预约</a>";
                }
                else
                {
                    status = "<span class='green'>已预约</span>";
                    DEVREQ dReq = new DEVREQ();
                    dReq.dwDevSN = ToUInt(resv.szAssertSN);
                    UNIDEVICE[] dRlt;
                    if (m_Request.Device.Get(dReq, out dRlt) == REQUESTCODE.EXECUTE_SUCCESS && dRlt.Length > 0)
                    {
                        act = dRlt[0].szDevName;
                    }
                    act += "<br/><a class='click second_act' third_id='" + resv.dwThirdResvID + "' resv_id='" + resv.dwResvID + "'>重新预约</a>";
                }
                string date = "未定";
                if (resv.dwResvDate != null && resv.dwStartHM != null)
                {
                    date = Util.Converter.UintToDateStr(resv.dwResvDate) + " " +
                        ((int)resv.dwStartHM / 100).ToString("00") + ":" + ((int)resv.dwStartHM % 100).ToString("00") + "-"
                        + ((int)resv.dwEndHM / 100).ToString("00") + ":" + ((int)resv.dwEndHM % 100).ToString("00");
                }
                secondList += "<tr class='it'><td>" + resv.dwThirdResvID + "</td><td>" + resv.szResvTitle + "</td><td>" + resv.szOrganization + "</td>" +
                    "<td>" + resv.szOrganiger + "</td><td>" + date + "</td><td class='text-center'>" + status + "</td><td class='text-center'>" + act + "</td></tr>";
            }
        }
    }
}