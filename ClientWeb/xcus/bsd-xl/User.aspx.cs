using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_User : UniClientPage
{
    protected string rtRsvList = "";
    protected string resvList = "";
    protected string teachResv = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_LAB))
        {
            Response.Redirect("Default.aspx");
        }
        DateTime start = DateTime.Now.AddMonths(-Convert.ToInt32(SelTime.SelectedValue));
        DateTime end = DateTime.Now.AddMonths(3);
        InitRTResv(ToUInt(start.ToString("yyyyMMdd")), ToUInt(end.ToString("yyyyMMdd")));
        InitResv(ToUInt(start.ToString("yyyyMMdd")), ToUInt(end.ToString("yyyyMMdd")));
        //InitTeachResv(ToUInt(start.ToString("yyyyMMdd")), ToUInt(end.ToString("yyyyMMdd")));
    }

    private void InitTeachResv(uint start, uint end)
    {
        RESVREQ vrGet = new RESVREQ();
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        vrGet.dwAccNo = acc.dwAccNo;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        vrGet.dwBeginDate = start;
        vrGet.dwEndDate = end;
        UNIRESERVE[] vtResult;
        if(m_Request.Reserve.Get(vrGet, out vtResult)==REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                UNIRESERVE resv = vtResult[i];
                uint? tchl = resv.dwTeachingTime;
                string rooms = GetRoomsFromResvDev(resv.ResvDev);
                //组成员
                string member = "个人预约";
                if ((resv.dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0)
                {
                    GROUPMEMDETAIL[] mbs = GetMembers(resv.dwMemberID);
                    if (mbs != null && mbs.Length > 0)
                    {
                        string str = "";
                        for (int j = 0; j < mbs.Length; j++)
                        {
                            str += mbs[j].szTrueName + "，";
                        }
                        member = str.Substring(0, str.Length - 1);
                    }
                }
                string time = Get1970Date((int)resv.dwBeginTime).Substring(5) + "-" + Get1970Date((int)resv.dwEndTime).Substring(11);
                teachResv += "<tr><td>"+resv.szOwnerName+"</td><td>" + resv.szTestName + "</td><td>" + time + "</td>" +
                                        "<td>" + rooms + "</td>" +
                                        "<td>" + resv.dwResvUsers + " 人 <span class='click' onclick='uni.msgBox(\"" + member + "\",\"成员名称\")'>详细</span></td>" +
                                        "<td><span>" + resv.dwTestHour + "</span> 学时</td>" +
                                        "<td>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>" ;
            }
        }
    }

    private void InitResv(uint start, uint end)
    {
        REQUESTCODE uResponse;
        RESVREQ vrGet = new RESVREQ();
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        vrGet.dwBeginDate = start;
        vrGet.dwEndDate = end;
        UNIRESERVE[] vtResult;
        uResponse = m_Request.Reserve.Get(vrGet, out vtResult);
        {
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
            {
                for (int i = 0; i < vtResult.Length; i++)
                {
                    UNIRESERVE resv = vtResult[i];
                    RESVDEV[] resvDev = resv.ResvDev;
                    //设备
                    string devName = "";
                    if (resvDev != null && resvDev.Length > 0)
                    {
                        devName = resvDev[0].szDevName;
                    }
                    //成员
                    GROUPMEMDETAIL[] mbs = GetMembers(resv.dwMemberID);
                    string szMembers = "";
                    if (mbs != null && mbs.Length > 0)
                    {
                        string str = "";
                        for (int j = 0; j < mbs.Length; j++)
                        {
                            str += mbs[j].szTrueName + ",";
                        }
                        szMembers = str.Substring(0, str.Length - 1);
                    }
                    resvList += "<tr rsvId='" + resv.dwResvID + "' owner='" + resv.dwOwner + "'>";
                    resvList += "<td class='f-tl'>" + CutStrT(resv.szTestName, 16) + "</td>";
                    resvList += "<td class='f-tl'>" + CutStrT(devName, 16) + "</td>";
                    resvList += "<td class='f-tl'>" + szMembers + "</td>";
                    resvList += "<td>" + Get1970Date((int)resv.dwOccurTime).Substring(5) + "</td>";
                    resvList += "<td>" + Get1970Date((int)resv.dwBeginTime).Substring(5) + "</td>";
                    string endtime = Get1970Date((int)resv.dwEndTime).Substring(5);
                    string rsvStr = endtime;
                    if (IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING))
                        rsvStr = "<div class='time'><div><span>" + endtime + "</span></div><div>[<span class='click' onclick='alterRsv(this);'>修改</span>]</div></div>" +
                            "<div class='alter' style='display:none;'><div><input type='text' class='Wdate' style='width:60px;' value='" + endtime.Substring(6) + "'/></div><div><span class='click' onclick='subAlter(this);'>提交</span> | <span class='click' onclick='back(this);'>返回</span></div></div>";
                    resvList += "<td rsvId='" + resv.dwResvID + "' start='" + Get1970Date((int)resv.dwBeginTime) + "' end='" + Get1970Date((int)resv.dwEndTime) + "'>" + rsvStr + "</td>";
                    resvList += "<td>" + Util.Converter.ResvStatusWithCheck(resv.dwStatus) + "</td>";
                    string act = "无";
                    if (resv.dwOwner == acc.dwAccNo && IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO))
                    {
                        act = "[<span class='click' rsvId='" + resv.dwResvID + "' onclick='delRsv(this);'>删除</span>]";
                    }
                    resvList += "<td>" + act + "</td>";
                    resvList += "</tr>";
                }
            }
        }
    }
    private void InitRTResv(uint startDate, uint endDate)
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        RTRESVREQ vrGet = new RTRESVREQ();
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        uint? accno = vrAccInfo.dwAccNo;
        uint intStartTime = startDate;
        uint intEndTime = endDate;
        vrGet.dwMAccNo = accno;
        //vrGet.dwTutorID = acc;
        vrGet.dwBeginDate = intStartTime;
        vrGet.dwEndDate = intEndTime;
        //vrGet.dwUnNeedStat = (int)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                rtRsvList += "<tr>";
                rtRsvList += "<td class='f-tl'>" + CutStrT(resv.szRTName, 12) + "<span class='color1'  title='主持人:" + resv.szHolderName + "; 负责人:" + resv.szLeaderName + "'>.详细</span></td><td>" + resv.szOwnerName + "</td><td class='f-tl'>" + CutStrT(resv.szDevName, 12) + "</td>";
                rtRsvList += "<td class='f-tl'>" + CutStrT(resv.szTestName, 14) + "</td>";
                rtRsvList += "<td>" + Get1970Date((int)resv.dwOccurTime).Substring(5) + "</td>";
                string begin = Get1970Date(Convert.ToInt32(resv.dwBeginTime));
                rtRsvList += "<td>" + begin.Substring(5) + "</td>";
                string end = Get1970Date(Convert.ToInt32(resv.dwEndTime));
                string endtime = end.Substring(5);
                string rsvStr = endtime;
                uint usetime = 0;
                if (IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING))
                {
                    usetime = GetRTestRsvTime(resv.dwLabID, resv.dwRTID);
                    rsvStr = "<div class='time'><div><span>" + endtime + "</span></div><div>[<span class='click' onclick='alterRsv(this);'>修改</span>]</div></div>" +
                        "<div class='alter' style='display:none;'><div><input type='text' class='Wdate' style='width:60px;' value='" + endtime.Substring(6) + "' title='剩余可用时长：" + usetime + "分钟'/></div><div><span class='click' onclick='subAlter(this);'>提交</span> | <span class='click' onclick='back(this);'>返回</span></div></div>";
                }
                    rtRsvList += "<td rsvId='" + resv.dwResvID + "' start='" + Get1970Date((int)resv.dwBeginTime) + "' end='" + Get1970Date((int)resv.dwEndTime) + "' valid='" + usetime + "'>" + rsvStr + "</td>";
                string szState = Util.Converter.ResvStatusConverter(resv.dwStatus);
                rtRsvList += "<td>" + szState + "</td>";
                string act = "";
                if (((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0) && (resv.dwOwner == accno))
                {
                    act += "[<a class='click' rsvId='" + resv.dwResvID + "' onclick='delRsv(this)'>删除</a>]<br/>";
                }
                if (act == "")
                {
                    act = "无";
                }
                rtRsvList += "<td>" + act + "</td></tr>";
            }
        }
    }
    //获取剩余机时
    private uint GetRTestRsvTime(uint? labId,uint? rtId)
    {
        uint ret = 0;
        SFROLEINFO[] rlt = GetLabRTRole(labId, rtId);
        if (rlt != null && rlt.Length > 0)
        {
            ret = (uint)(rlt[0].dwPermitUseTime - rlt[0].dwUsedTime);
        }
        return ret;
    }
}