using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UniWebLib;
using System.Xml;

public partial class ClientWeb_pro_ajax_reserve : UniClientAjax
{
    UNIDEVICE curDev;
    string myTutor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "del_rt_resv")
            {
                if (IsLoginReady())
                {
                    REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                    uint id = Convert.ToUInt32(Request["id"]);
                    RTRESV vrRTResv = new RTRESV();
                    vrRTResv.dwResvID = id;
                    uResponse = m_Request.Reserve.DelRTResv(vrRTResv);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        SucMsg();
                    }
                    else
                    {
                        ErrMsg(m_Request.szErrMsg);
                    }
                }
            }
            else if (act == "set_resv")
            {
                if (IsLoginReady())
                {
                    SetResv();
                }
            }
            else if (act == "set_rtrsv")
            {
                if (IsLoginReady())
                {
                    if (GetDev(out curDev))
                    {
                        SetRTResv();
                    }
                }
            }
            else if (act == "set_yard_rsv")
            {
                if (IsLoginReady())
                {
                    if (GetDev(out curDev))
                    {
                        SetYardResv();
                    }
                }
            }
            else if (act == "set_tch_rsv")
            {
                if (IsLoginReady())
                {
                    SetTeachResv();
                }
            }
            else if (act == "quick_resv")
            {
                if (IsLoginReady())
                {
                    QuickResv();
                }
            }
            else if (act == "del_resv")
            {
                string rsvId = Request["id"];
                string type = Request["resv_type"];
                if (string.IsNullOrEmpty(rsvId))
                {
                    ErrMsg("参数有误！");
                    return;
                }
                string[] list = rsvId.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] == "") continue;
                    string ret = DelReserve(list[i]);
                    if (ret != "ok")
                    {
                        ErrMsg(ret);
                        return;
                    }
                }
                SucMsg();
            }
            else if (act == "decide_resv")
            {
                if (IsLoginReady())
                    DecideResv();
            }
            else if (act == "ck_rtrsv")
            {
                if (IsLoginReady())
                {
                    CkRTRsv();
                }
            }
            else if (act == "get_dev_rtrsv_list")
            {
                if (GetDev(out curDev))
                {
                    GetDevRTRsvList();
                }
            }
            else if (act == "get_dev_yard_rsv")
            {
                GetDevYardRsv();
            }
            else if (act == "get_yardrsv_checkinfo")
            {
                if (IsLoginReady())
                {
                    GetYardRsvCheckInfo();
                }
            }
            else if (act == "get_resv_list")
            {
                GetResvList();
            }
            else if (act == "get_my_resv")
            {
                if (IsLoginReady())
                {
                    GetUserResv();
                }
            }
            else if (act == "get_user_rtrsv_list")
            {
                GetUserRsvList();
            }
            else if (act == "get_dev_rtrsv_fm")
            {
                if (IsLoginReady())
                {
                    if (GetDev(out curDev))
                    {
                        string rtOpt = GetRTestes();
                        string manOpt = GetManInfo();
                        string devStatus = "null";
                        if (!string.IsNullOrEmpty(Request["date"]))
                        {
                            uint dt = DateToUint(Request["date"]);
                            devStatus = GetDevSta(dt);
                        }
                        SucRlt("{\"rtOpt\":\"" + rtOpt + "\",\"manOpt\":\"" + manOpt + "\",\"devStatus\":" + devStatus + ",\"myTutor\":\"" + myTutor + "\"}");
                    }
                }
            }
            else if (act == "get_dev_rtrsv_fee")
            {
                if (IsLoginReady())
                {
                    if (GetDev(out curDev))
                    {
                        string splOpt = GetSpl();
                        //费用
                        string fee = GetFee();
                        SucRlt("{\"splOpt\":\"" + splOpt + "\"," + fee + "}");
                    }
                }
            }
            else if (act == "sub_dev_rtrsv_fm")
            {
                if (IsLoginReady())
                {
                    if (GetDev(out curDev))
                    {
                        RsvForm();
                    }
                }
            }
            else if (act == "set_rsv_feedback")
            {
                if (IsLoginReady())
                {
                    SetRsvFeedback();
                }
            }
            else if (act == "get_rsv_feedback")
            {
                GetRsvFeedback();
            }
            else if (act == "get_tch_resv")
            {
                GetTchResv();
            }
            else if (act == "get_plan_resv")
            {
                if (IsLoginReady())
                    GetPlanResv();
            }
            else if (act == "get_test_resv")
            {
                GetTestResv();
            }
            else if (act == "get_test_info")
            {
                if (IsLoginReady())
                    GetTestInfo();
            }
            else if (act == "get_check_type")
            {
                GetCkType();
            }
            else if (act == "set_open_aty")
            {
                if (IsLoginReady())
                    setOpenAty();
            }
            else if (act == "del_open_aty")
            {
                delOpenAty();
            }
            else if (act == "get_third_resv")
            {
                if (IsLoginReady())
                {
                    GetThirdResv();
                }
            }
            else if (act == "resv_checkin")
            {
                ResvCheckIn();
            }
            else if (act == "resv_leave")
            {
                ResvLeave();
            }
            else if (act == "resv_extend")
            {
                ResvExtend();
            }
            else if (act == "get_aty_seats")
            {
                GetAtySeats();
            }
            else if (act == "enroll_aty")
            {
                if (IsLoginReady())
                {
                    EnrollAty();
                }
            }
            else if (act == "quit_aty")
            {
                if (IsLoginReady())
                {
                    QuitAty();
                }
            }
            else
            {
                NoAct();
            }
        }
    }

    private void QuitAty()
    {
        string aty_id = Request["aty_id"];
        ACTIVITYEXIT set = new ACTIVITYEXIT();
        set.dwActivityPlanID = ToUInt(aty_id);
        set.dwAccNo = curAcc.dwAccNo;
        if (m_Request.Reserve.ExitActivity(set) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetAtySeats()
    {
        string atyId = Request["aty_id"];
        APSEATREQ req = new APSEATREQ();
        req.dwActivityPlanID = ToUInt(atyId);
        APSEAT[] rlt;
        if (m_Request.Reserve.GetAPSeat(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<seat> list = new List<seat>();
            for (int i = 0; i < rlt.Length; i++)
            {
                APSEAT seat = rlt[i];
                seat st = new seat();
                st.devId = seat.dwDevID;
                st.devSN = seat.dwDevSN;
                st.devName = seat.szDevName;
                st.atyId = seat.dwActivityPlanID;
                st.accno = seat.dwAccNo;
                st.userName = seat.szTrueName;
                st.status = seat.dwStatus;
                list.Add(st);
            }
            SucRlt(list.ToArray());
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void EnrollAty()
    {
        string atyId = Request["aty_id"];
        string devId = Request["dev_id"];
        string devSN = Request["dev_sn"];
        ACTIVITYENROLL set = new ACTIVITYENROLL();
        set.dwActivityPlanID = ToUInt(atyId);
        if (!string.IsNullOrEmpty(devId))
        {
            set.dwDevID = ToUInt(devId);
        }
        if (!string.IsNullOrEmpty(devSN))
        {
            set.dwDevSN = ToUInt(devSN);
        }
        set.dwAccNo = curAcc.dwAccNo;
        if (m_Request.Reserve.EnrollActivity(set) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void delOpenAty()
    {
        string id = Request["aty_id"];
        string resvId = Request["resv_id"];
        UNIACTIVITYPLAN set = new UNIACTIVITYPLAN();
        set.dwActivityPlanID = ToUInt(id);
        set.dwResvID = ToUInt(resvId);
        if (m_Request.Reserve.DelActivityPlan(set) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
            ErrMsg();
    }

    private void ResvExtend()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string labId = Request["lab_id"];
        string time = Request["time"];
        RESVUSERDELAYREQ req = new RESVUSERDELAYREQ();
        req.dwResvID = ToUInt(resvId);
        req.dwDevID = ToUInt(devId);
        req.dwLabID = ToUInt(labId);
        req.dwMaxDelayMin = ToUInt(time);
        RESVUSERDELAYRES rlt;
        if (m_Request.Console.ResvUserDelay(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg(rlt.szDispInfo);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void ResvLeave()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string labId = Request["lab_id"];
        string type = Request["type"];
        RESVUSERGOOUTREQ req = new RESVUSERGOOUTREQ();
        req.dwResvID = ToUInt(resvId);
        if (string.IsNullOrEmpty(devId))
        {
            DEVREQ req2 = new DEVREQ();
            req2.dwResvID = ToUInt(resvId);
            UNIDEVICE[] rlt2;
            if (m_Request.Device.Get(req2, out rlt2) == REQUESTCODE.EXECUTE_SUCCESS && rlt2.Length > 0)
            {
                req.dwDevID = rlt2[0].dwDevID;
                req.dwLabID = rlt2[0].dwLabID;
            }
            else
            {
                ErrMsg("获取预约的设备失败");
            }
        }
        else
        {
            req.dwDevID = ToUInt(devId);
            req.dwLabID = ToUInt(labId);
        }
        if (string.IsNullOrEmpty(type))
            req.dwOutType = (uint)RESVUSERGOOUTREQ.DWOUTTYPE.RESVUSEROUT_LEAVE;
        else
            req.dwOutType = ToUInt(type);
        RESVUSERGOOUTRES rlt;
        if (m_Request.Console.ResvUserGoOut(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg(rlt.szDispInfo);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void ResvCheckIn()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string labId = Request["lab_id"];
        string type = Request["type"];
        RESVUSERCOMEINREQ req = new RESVUSERCOMEINREQ();
        req.dwResvID = ToUInt(resvId);
        req.dwDevID = ToUInt(devId);
        req.dwLabID = ToUInt(labId);
        if (!string.IsNullOrEmpty(type))
            req.dwInType = ToUInt(type);
        RESVUSERCOMEINRES rlt;
        if (m_Request.Console.ResvUserComeIn(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg(rlt.szDispInfo);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void QuickResv()
    {
        uint kind = ToUInt(Request["kind_id"]);
        uint room = ToUInt(Request["room_id"]);
        string date = Request["date"];
        string range = Request["range"];
        uint useTime = ToUInt(Request["interval"]);
        uint classkind = ToUInt(Request["classkind"]);
        if (string.IsNullOrEmpty(range))
        {
            ErrMsg("未选择时间范围");
            return;
        }
        string[] rgs = range.Split('-');
        string start = date + " " + rgs[0];
        string end = date + " " + rgs[1];
        AUTORESVREQ req = new AUTORESVREQ();
        req.dwOwner = curAcc.dwAccNo;
        req.dwDevKind = kind;
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        if ((classkind & 1) > 0)//空间
        {
            req.dwPurpose += (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
        }
        else if ((classkind & 2) > 0)//阅览室
        {
            req.dwPurpose += (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
        }
        else if ((classkind & 8) > 0)//座位
        {
            req.dwPurpose += (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
        }
        if (room != 0)
        {
            req.dwRoomID = room;
        }
        DateTime dt = DateTime.Parse(start);
        if (DateTime.Now > dt)
        {
            start = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm");
        }
        req.dwPreDate = ToUInt(date.Replace("-", ""));
        req.dwEarlyBeginTime = (uint)Get1970Seconds(start);
        req.dwLateBeginTime = (uint)Util.Converter.Get1970Seconds(DateTime.Parse(end).AddMinutes(-useTime));
        req.dwUseMin = useTime;
        UNIRESERVE rlt;
        if (m_Request.Reserve.Auto(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string str_start = Get1970Date((uint)rlt.dwBeginTime);
            string str_end = Get1970Date((uint)rlt.dwEndTime);
            SucRlt("{\"dev\":\"" + rlt.ResvDev[0].szDevName + "(" + rlt.ResvDev[0].szRoomName + ")" + "\",\"time\":\"" + str_start + "-" + str_end.Substring(11) + "\"}");
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetThirdResv()
    {
        string id = Request["resv_id"];
        THIRDRESVREQ req = new THIRDRESVREQ();
        if (!string.IsNullOrEmpty(id))
            req.dwThirdResvID = ToUInt(id);
        req.szPID = curAcc.szPID;
        THIRDRESV[] rlt;
        if (m_Request.Reserve.GetThirdResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucRlt(rlt);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetUserResv()
    {
        string start = Request["start"];
        string end = Request["end"];
        string statFlag = Request["stat_flag"];
        if (string.IsNullOrEmpty(start))
        {
            start = DateTime.Now.ToString("yyyyMMdd");
        }
        if (string.IsNullOrEmpty(end))
        {
            end = DateTime.Now.AddMonths(12).ToString("yyyyMMdd");
        }
        RESVREQ req = new RESVREQ();
        req.dwBeginDate = ToUInt(start);
        req.dwEndDate = ToUInt(end);
        string classkind = GetConfig("openClsKind");
        if (!string.IsNullOrEmpty(classkind) && classkind != "0") req.dwClassKind = ToUInt(classkind);
        req.szReqExtInfo.szOrderKey = "dwBeginTime";
        req.szReqExtInfo.szOrderMode = "ASC";
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
        if (!string.IsNullOrEmpty(statFlag))
            req.dwStatFlag = ToUInt(statFlag);
        else
            req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        UNIRESERVE[] rlt;
        if (m_Request.Reserve.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<uniresv> list = new List<uniresv>();
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIRESERVE resv = rlt[i];
                uniresv rsv = new uniresv();
                if ((resv.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)//20171110章毅添加为了获取审核不通过理由
                {
                    rsv.szmemo = resv.szMemo;
                }
                //预约时间
                rsv.start = Get1970Date((int)rlt[i].dwBeginTime);
                rsv.end = Get1970Date((int)rlt[i].dwEndTime);
                rsv.timeDesc = Get1970Date((int)rlt[i].dwBeginTime, "MM/dd HH:mm") + "-" + Get1970Date((int)rlt[i].dwEndTime, (rsv.start.Substring(0, 10) == rsv.end.Substring(0, 10) ? "" : "MM/dd ") + "HH:mm");
                //组成员
                string member = "";
                if ((resv.dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0)
                {
                    GROUPMEMDETAIL[] mbs = GetMembers(resv.dwMemberID);
                    if (mbs != null && mbs.Length > 0)
                    {
                        string str = "";
                        for (int j = 0; j < mbs.Length; j++)
                        {
                            str += mbs[j].szTrueName + ",";
                        }
                        member = str.Substring(0, str.Length - 1);
                    }
                    GROUPREQ g_req = new GROUPREQ();
                    g_req.dwGroupID = resv.dwMemberID;
                    UNIGROUP[] g_rlt;
                    if (m_Request.Group.GetGroup(g_req, out g_rlt) == REQUESTCODE.EXECUTE_SUCCESS && g_rlt.Length > 0)
                    {
                        rsv.minUser = (int)g_rlt[0].dwMinUsers;
                        rsv.maxUser = (int)g_rlt[0].dwMaxUsers;
                        rsv.groupName = g_rlt[0].szName;
                    }
                }
                else
                {
                    member = Translate("个人预约");
                }
                rsv.group = (int)resv.dwMemberID;
                rsv.groupId = rsv.group + "";
                rsv.members = member;
                //预约对象
                string objs = "";
                RESVDEV[] resvDev = resv.ResvDev;
                if (resvDev != null && resvDev.Length > 0)
                {
                    string devName = string.Empty;
                    for (int j = 0; j < resvDev.Length; j++)
                    {
                        devName = devName + resvDev[j].szDevName.ToString();
                    }
                    objs = devName != "" ? devName : Translate("请到现场分配");
                    //首设备信息
                    uint? sn = resvDev[0].dwDevStart;
                    if (sn != null && sn > 0 && resvDev[0].dwDevNum > 0)
                    {
                        UNIDEVICE dev = GetDevBySN(sn, resvDev[0].dwRoomID.ToString());
                        if (dev.dwDevID != null)
                        {
                            rsv.devId = dev.dwDevID.ToString();
                        }
                    }
                    rsv.kindId = resvDev[0].dwDevKind.ToString();
                    rsv.roomId = resvDev[0].dwRoomID.ToString(); ;
                    rsv.roomName = resvDev[0].szRoomName;
                    rsv.labId = resvDev[0].dwLabID.ToString();
                    rsv.labName = resvDev[0].szLabName;
                }
                rsv.devName = objs;
                //操作
                if (resv.dwOwner == curAcc.dwAccNo)
                    rsv.actSN = GetAct(resv);
                //提交时间
                rsv.occur = Get1970Date((int)resv.dwOccurTime);
                //预约信息
                rsv.id = resv.dwResvID.ToString();
                rsv.name = resv.szTestName;
                rsv.status = resv.dwStatus;
                if ((resv.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)//20171110章毅添加为了获取审核不通过理由
                {
                    rsv.state = (Util.Converter.ResvStatusConverter(resv.dwStatus))+"<br />" + rsv.szmemo;
                }
                else {
                    rsv.state = (Util.Converter.ResvStatusConverter(resv.dwStatus));
                }
                rsv.states = (Util.Converter.ResvStatusWithCheck(resv.dwStatus, true));
                rsv.owner = resv.szOwnerName;
                rsv.ownerAccno = resv.dwOwner.ToString();
                list.Add(rsv);
            }
            SucRlt(list);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    private int GetAct(UNIRESERVE rsv)
    {
        int act = 0;//删除=1 修改=2 提前结束=4 成员管理=8
        DateTime end = Util.Converter.Get1970DateTime((int)rsv.dwEndTime);
        DateTime start = Util.Converter.Get1970DateTime((int)rsv.dwBeginTime);
        DateTime now = DateTime.Now;
        if (start > now && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
        {
            if ((rsv.dwPurpose & (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY) == 0)
                act += 3;
        }
        if (start < now && end > now && (rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0 && GetConfig("showResvFinish") == "1")
        {
            act += 4;
        }
        if (end > now && (rsv.dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0)
        {
            act += 8;
        }
        return act;
    }

    private void setOpenAty()
    {
        UNIACTIVITYPLAN para = new UNIACTIVITYPLAN();
        string atyId = Request["dwActivityPlanID"];
        if (string.IsNullOrEmpty(atyId))//创建
        {
            string dev = Request["dev_id"];
            UNIDEVICE[] devs = GetDevById(dev);
            UNIDEVICE CurDev;
            if (devs == null || devs.Length != 1)
            {
                ErrMsg("获取设备有误");
                return;
            }
            else
                CurDev = devs[0];

            para.szSite = CurDev.szLabName + "【" + CurDev.szDevName + "】";
            para.dwDevID = CurDev.dwDevID;
            para.szRoomNo = CurDev.szRoomNo;
            para.dwCheckRequirment = (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_NOAPPLY;
            para.dwKind = (uint)UNIACTIVITYPLAN.DWKIND.ACTIVITYPLANKIND_SALON;
            //组
            if (!string.IsNullOrEmpty(Request["group_id"]))
            {
                para.dwGroupID = ToUInt(Request["group_id"]);
            }
        }
        else//修改
        {
            ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
            req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
            req.szGetKey = atyId;
            UNIACTIVITYPLAN[] rlt;
            if (m_Request.Reserve.GetActivityPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                para = rlt[0];
            }
            else
            {
                ErrMsg("获取活动失败。" + m_Request.szErrMsg);
            }
        }
        if (!string.IsNullOrEmpty(Request["szActivityPlanName"]))
            para.szActivityPlanName = Request["szActivityPlanName"];
        if (!string.IsNullOrEmpty(Request["szHostUnit"]))
            para.szHostUnit = Request["szHostUnit"];
        if (!string.IsNullOrEmpty(Request["szOrganizer"]))
            para.szOrganizer = Request["szOrganizer"];
        if (!string.IsNullOrEmpty(Request["szPresenter"]))
            para.szPresenter = Request["szPresenter"];
        if (!string.IsNullOrEmpty(Request["szDesiredUser"]))
            para.szDesiredUser = Request["szDesiredUser"];
        if (!string.IsNullOrEmpty(Request["szContact"]))
            para.szContact = Request["szContact"];
        if (!string.IsNullOrEmpty(Request["szHandPhone"]))
            para.szHandPhone = Request["szHandPhone"];
        if (!string.IsNullOrEmpty(Request["szEmail"]))
            para.szEmail = Request["szEmail"];
        if (!string.IsNullOrEmpty(Request["dwKind"]))
        {
            para.dwKind = ToUInt(Request["dwKind"]);
        }
        //申请条件
        if (!string.IsNullOrEmpty(Request["dwCheckRequirment"]))
            para.dwCheckRequirment =para.dwCheckRequirment| ToUInt(Request["dwCheckRequirment"]);
        if(!string.IsNullOrEmpty(Request["require_seat"])){
            para.dwCheckRequirment = para.dwCheckRequirment | (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_SUPPORTSEAT;
        }
        if(!string.IsNullOrEmpty(Request["require_check_in"])){
            para.dwCheckRequirment = para.dwCheckRequirment | (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_ATTENDANCE;
        }
        //活动类型
        if (!string.IsNullOrEmpty(Request["dwKind"]))
            para.dwKind = ToUInt(Request["dwKind"]);
        //成员限制
        if (!string.IsNullOrEmpty(Request["dwMaxUsers"]))
            para.dwMaxUsers = ToUInt(Request["dwMaxUsers"]);
        if (!string.IsNullOrEmpty(Request["dwMinUsers"]))
            para.dwMinUsers = ToUInt(Request["dwMinUsers"]);
        //时间
        if (!string.IsNullOrEmpty(Request["dwEnrollDeadline"]))
            para.dwEnrollDeadline = ToUInt(Request["dwEnrollDeadline"].Replace("-", ""));
        if (!string.IsNullOrEmpty(Request["dwPublishDate"]))
            para.dwPublishDate = ToUInt(Request["dwPublishDate"].Replace("-", ""));
        if (!string.IsNullOrEmpty(Request["dwActivityDate"]))
            para.dwActivityDate = ToUInt(Request["dwActivityDate"].Replace("-", ""));
        if (!string.IsNullOrEmpty(Request["dwBeginTime"]))
            para.dwBeginTime = ToUInt(Request["dwBeginTime"].Replace(":", ""));
        if (!string.IsNullOrEmpty(Request["dwEndTime"]))
            para.dwEndTime = ToUInt(Request["dwEndTime"].Replace(":", ""));

        if (!string.IsNullOrEmpty(Request["szIntroInfo"]))
            para.szIntroInfo = Request["szIntroInfo"];
        if (!string.IsNullOrEmpty(Request["szMemo"]))
            para.szMemo = Request["szMemo"];

        if (!string.IsNullOrEmpty(Request["szApplicationURL"]))
            para.szApplicationURL = Request["szApplicationURL"];//申请材料
        if (!string.IsNullOrEmpty(Request["szActivityPlanURL"]))
            para.szActivityPlanURL = Request["szActivityPlanURL"];//活动海报

        if (m_Request.Reserve.SetActivityPlan(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (string.IsNullOrEmpty(atyId))
            {//新建活动 把申请人加入组
                //GROUPMEMBER setValueMember = new GROUPMEMBER();
                //setValueMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
                //setValueMember.dwMemberID = curAcc.dwAccNo;
                //setValueMember.szName = curAcc.szTrueName.ToString();
                //setValueMember.szMemo = curAcc.szLogonName.ToString() + ":" + curAcc.szTrueName.ToString();
                //setValueMember.dwGroupID = para.dwGroupID;
                //m_Request.Group.SetGroupMember(setValueMember);
                if (string.IsNullOrEmpty(Request["group_id"]))//先前未建组
                    AddMemByAccNo(para.dwGroupID.ToString(), curAcc.dwAccNo.ToString());
            }
            SucMsg();
        }
        else
        {
            if (para.dwResvID != null && para.dwResvID != 0)
            {
                UNIRESERVE resv = new UNIRESERVE();
                resv.dwResvID = para.dwResvID;
                m_Request.Reserve.Del(resv);
            }
            ErrMsg("对不起，申请失败。" + m_Request.szErrMsg);
        }
    }

    private void DecideResv()
    {
        string rsvId = Request["id"];
        if (string.IsNullOrEmpty(rsvId))
        {
            ErrMsg("参数有误！");
            return;
        }
        string[] list = rsvId.Split(',');
        UNIRESERVE para = new UNIRESERVE();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == "") continue;
            para.dwResvID = ToUInt(list[i]);
            para.dwStatus = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_FORMAL;
            para.dwOwner = curAcc.dwAccNo;
            if (m_Request.Reserve.Set(para, out para) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                ErrMsg(m_Request.szErrMsg);
                return;
            }
        }
        SucMsg();
    }

    private void GetTestInfo()
    {
        string testId = Request["test_id"];
        string planId = Request["plan_id"];
        string term = Request["term"];
        List<uniresv> list = new List<uniresv>();
        TESTITEMINFOREQ req = new TESTITEMINFOREQ();
        if (!string.IsNullOrEmpty(testId))
            req.dwTestItemID = ToUInt(testId);
        if (!string.IsNullOrEmpty(planId))
            req.dwTestPlanID = ToUInt(planId);
        if (!string.IsNullOrEmpty(term))
            req.dwYearTerm = ToUInt(term);
        req.dwAccNo = curAcc.dwAccNo;
        TESTITEMINFO[] rlt;
        if (m_Request.Reserve.GetTestItemInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                TESTITEMINFO item = rlt[i];
                UNIRESERVE[] resvs = item.ResvInfo;
                for (int j = 0; j < resvs.Length; j++)
                {
                    uniresv resv = new uniresv();
                    UNIRESERVE info = resvs[j];
                    RESVDEV[] rsvdev = info.ResvDev;
                    string rooms = GetRoomsFromResvDev(rsvdev);
                    resv.id = info.dwResvID.ToString();
                    resv.title = item.szTestName;
                    resv.detail = "房间：" + rooms + "<br/>教师：" + item.szTeacherName + "<br/>实验：" + item.szTestName + "<br/>课程：" + item.szCourseName;
                    resv.testId = item.dwTestItemID.ToString();
                    resv.testName = item.szTestName;
                    resv.owner = item.szTrueName;
                    resv.ownerAccno = item.dwAccNo.ToString();
                    resv.planId = item.dwTestPlanID.ToString();
                    resv.planName = item.szTestPlanName;
                    actResv(info, ref resv);
                    list.Add(resv);
                }
            }
            SucRlt(list);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }

    private void GetTestResv()
    {
        string testId = Request["test_id"];
        List<uniresv> list = new List<uniresv>();
        if (!string.IsNullOrEmpty(testId))
        {
            TESTITEMREQ req = new TESTITEMREQ();
            req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
            req.szGetKey = testId;
            UNITESTITEM[] rlt;
            if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (rlt.Length > 0)
                    ConvertTestResvInfo(rlt[0], ref list);
                SucRlt(list);
            }
            else
                ErrMsg(m_Request.szErrMsg);
        }
        else
            ErrMsgP();
    }

    private void GetPlanResv()
    {
        string term = Request["term"];
        string planId = Request["plan_id"];
        string ident = Request["ident"];
        List<uniresv> list = new List<uniresv>();
        if (!string.IsNullOrEmpty(term))
        {
            TESTPLANREQ req = new TESTPLANREQ();
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
            req.szGetKey = curAcc.dwAccNo.ToString();
            req.dwYearTerm = ToUInt(term);
            UNITESTPLAN[] rlt;
            if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    GetResvByPlan(rlt[i].dwTestPlanID.ToString(), ref list);
                }
                SucRlt(list);
            }
            else
                ErrMsg(m_Request.szErrMsg);
        }
        else if (!string.IsNullOrEmpty(planId))
        {
            GetResvByPlan(planId, ref list);
            SucRlt(list);
        }
        else
            ErrMsg("参数有误");
    }
    private void GetResvByPlan(string planId, ref List<uniresv> list)
    {
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        req.szGetKey = planId;
        req.dwTeacherID = curAcc.dwAccNo;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                ConvertTestResvInfo(rlt[i], ref list);
            }
        }
    }

    private void ConvertTestResvInfo(UNITESTITEM test, ref List<uniresv> list)
    {
        UNIRESERVE[] resvs = test.ResvInfo;
        for (int j = 0; j < resvs.Length; j++)
        {
            uniresv resv = new uniresv();
            UNIRESERVE info = resvs[j];
            RESVDEV[] rsvdev = info.ResvDev;
            string rooms = GetRoomsFromResvDev(rsvdev);
            resv.id = info.dwResvID.ToString();
            resv.title = test.szTestName;
            resv.detail = "房间：" + rooms + "<br/>班级：" + test.szGroupName + "<br/>实验：" + test.szTestName + "<br/>课程：" + test.szCourseName;
            resv.testId = test.dwTestItemID.ToString();
            resv.testName = test.szTestName;
            resv.owner = test.szTeacherName;
            resv.ownerAccno = test.dwTeacherID.ToString();
            resv.groupId = test.dwGroupID.ToString();
            resv.groupName = test.szGroupName;
            resv.planId = test.dwTestPlanID.ToString();
            resv.planName = test.szTestPlanName;
            actResv(info, ref resv);
            list.Add(resv);
        }
    }
    private void actResv(UNIRESERVE info, ref uniresv resv)
    {
        uint? tchl = info.dwTeachingTime;
        resv.ltch = (int)tchl;
        int start = (int)(tchl % 10000) / 100;
        int end = (int)tchl % 100;
        string[] week = { "一", "二", "三", "四", "五", "六", "日" };
        resv.name = (int)tchl / 100000 + "周【" + "星期" + week[(int)((tchl / 10000) % 10)] + "】第" + start + (start == end ? "" : ("-" + end)) + "节";
        //预约状态
        if ((info.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
        {
            resv.state = "undo";
        }
        else if ((info.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
        {
            resv.state = "doing";
        }
        else if ((info.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
        {
            resv.state = "done";
        }
        else
        {
            resv.state = "othe";
        }
        resv.allDay = false;
        resv.islong = false;
    }

    private void GetTchResv()
    {
        string start = Request["start"];
        string end = Request["end"];
        string roomId = Request["room_id"];
        string teacherAccno = Request["teacher_accno"];
        string memberAccno = Request["mb_accno"];
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            ErrMsg("参数有误");
            return;
        }
        TEACHINGRESVREQ req = new TEACHINGRESVREQ();
        req.dwBeginDate = ToUInt(start);
        req.dwEndDate = ToUInt(end);
        if (!string.IsNullOrEmpty(roomId))
            req.szRoomNo = roomId;
        if (!string.IsNullOrEmpty(roomId))
            req.dwTeacherID = ToUInt(teacherAccno);
        if (!string.IsNullOrEmpty(roomId))
            req.dwAccNo = ToUInt(memberAccno);
        TEACHINGRESV[] rlt;
        if (m_Request.Reserve.GetTeachingResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<uniresv> resvs = new List<uniresv>();
            for (int i = 0; i < rlt.Length; i++)
            {

                uniresv resv = new uniresv();
                resv.id = rlt[i].dwResvID.ToString();
                string rooms = GetRoomsFromResvDev(rlt[i].ResvDev);
                resv.roomName = rooms;
                resv.title =rooms+","+ rlt[i].szGroupName;
                resv.testId = rlt[i].dwTestItemID.ToString();
                resv.testName = rlt[i].szTestName;
                resv.owner = rlt[i].szTeacherName;
                resv.ownerAccno = rlt[i].dwTeacherID.ToString();
                resv.teacher = rlt[i].szTeacherName;
                resv.teacherAccno = rlt[i].dwTeacherID.ToString();
                resv.groupId = rlt[i].dwGroupID.ToString();
                resv.groupName = rlt[i].szGroupName;
                resv.planId = rlt[i].dwTestPlanID.ToString();
                resv.planName = rlt[i].szTestPlanName;
                resv.ltch = (int)rlt[i].dwTeachingTime;
                //预约状态
                if ((rlt[i].dwResvStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                {
                    resv.state = "undo";
                }
                else if ((rlt[i].dwResvStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                {
                    resv.state = "doing";
                }
                else if ((rlt[i].dwResvStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                {
                    resv.state = "done";
                }
                else
                {
                    resv.state = "othe";
                }
                resv.allDay = false;
                resv.islong = false;
                resvs.Add(resv);
            }
            SucRlt(resvs);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }
    private void GetRsvFeedback()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string accno = Request["accno"];
        USERFEEDBACKREQ req = new USERFEEDBACKREQ();
        if (!string.IsNullOrEmpty(resvId))
            req.dwResvID = ToUInt(resvId);
        if (!string.IsNullOrEmpty(devId))
            req.dwDevID = ToUInt(devId);
        if (!string.IsNullOrEmpty(accno))
            req.dwAccNo = ToUInt(accno);
        USERFEEDBACK[] rlt;
        if (m_Request.Admin.GetUserFeedback(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<feedback> list = new List<feedback>();
            for (int i = 0; i < rlt.Length; i++)
            {
                USERFEEDBACK fb = rlt[i];
                feedback it = new feedback();
                it.accno = fb.dwAccNo.ToString();
                it.name = fb.szTrueName;
                it.phone = fb.szHandPhone;
                it.email = fb.szEmail;
                it.dept = fb.szUserDeptName;
                it.devName = fb.szDevName;
                it.kind = (int)fb.dwFeedKind;
                it.score = (int)fb.dwScore;
                it.occurDate = (int)fb.dwOccurDate;
                it.occurTime = Get1970Date((int)fb.dwOccurTime);
                it.replyDate = (int)fb.dwReplyDate;
                it.replyTime = Get1970Date((int)fb.dwReplyTime);
                it.content = fb.szIntroInfo;
                it.reply = fb.szReplyInfo;
                it.answer = fb.szAnswerer;
                YARDRESV resv = GetYardResvById(fb.dwResvID);
                if (resv.dwResvID != null)
                {
                    it.resvName = resv.szResvName;
                    it.activity = resv.szActivityName;
                    it.resvStart = Get1970Date((int)resv.dwBeginTime);
                    it.resvEnd = Get1970Date((int)resv.dwEndTime);
                }
                list.Add(it);
            }
            SucRlt(list);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }

    private void SetRsvFeedback()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string kind = Request["kind"];
        string content = Request["con"];
        string score = Request["score"];
        USERFEEDBACK para = new USERFEEDBACK();
        para.dwAccNo = curAcc.dwAccNo;
        if (!string.IsNullOrEmpty(resvId))
            para.dwResvID = ToUInt(resvId);
        para.dwDevID = ToUInt(devId);
        para.dwFeedKind = ToUInt(kind);
        para.szIntroInfo = content;
        if (!string.IsNullOrEmpty(score))
            para.dwScore = ToUInt(score);
        if (m_Request.Admin.DoUserFeedback(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    struct feedback
    {
        public string accno;		/*帐号*/
        public string name;		/*姓名*/
        public string phone;		/*手机*/
        public string email;		/*电邮*/
        public string dept;		/*部门*/
        public int kind;		/*反馈类型*/
        public int state;		/*状态*/
        public string resvId;		/*预约ID*/
        public string devId;		/*使用设备*/
        public string devName;		/*设备名称*/
        public int score;		/*用户评分*/
        public int occurDate;		/*发生日期*/
        public string occurTime;		/*发生时间*/
        public string content;		/*申请信息*/
        public string reply;		/*回复信息*/
        public int replyDate;		/*回复日期*/
        public string replyTime;		/*回复时间*/
        public string answer;		/*回复者*/
        public string memo;		/*状态说明*/
        //预约信息
        public string resvName;
        public string resvStart;
        public string resvEnd;
        public string activity;
    }
    struct checkinfo
    {
        public string title;
        public string id;
        public uint? kind;
        public string memo;
        public string admin;
        public string state;
        public string date;
    }
    private void GetYardRsvCheckInfo()
    {
        string resvId = Request["resv_id"];
        string msg = "";
        YARDRESVCHECKINFO[] rlt = GetResvCheckInfo(resvId, out msg);
        if (msg == "ok")
        {
            List<checkinfo> list = new List<checkinfo>();
            for (int i = 0; i < rlt.Length; i++)
            {
                checkinfo ck = new checkinfo();
                ck.title = rlt[i].szCheckName;
                ck.kind = rlt[i].dwCheckKind;
                ck.id = rlt[i].dwCheckID.ToString();
                ck.admin = rlt[i].szAdminName;
                ck.memo = rlt[i].szCheckDetail;
                if (rlt[i].dwCheckDate != null && rlt[i].dwCheckDate != 0)
                    ck.date = Get1970Date((int)rlt[i].dwCheckTime);//toDate((uint)rlt[i].dwCheckDate) + " " + rlt[i].dwCheckTime / 100 + ":" + rlt[i].dwCheckTime % 100;
                ck.state = Util.Converter.GetCheckState(rlt[i].dwCheckStat);
                list.Add(ck);
            }
            SucRlt(list);
        }
        else
        {
            ErrMsg(msg);
        }
    }

    private void GetDevYardRsv()
    {
        string resvId = Request["resv_id"];
        string resvName = Request["resv_name"];
        string devId = Request["dev_id"];
        string devcls = Request["dev_cls"];
        string campus = Request["campus"];
        string atytype = Request["aty_type"];
        string buildingId = Request["building_id"];
        string onlyOpen = Request["only_open"];
        string login = Request["login"];
        string start = Request["start"];
        string end = Request["end"];
        string state = Request["state"];
        string multi = Request["multi"];
        string unNeed = Request["un_need"];//不需要的状态

        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            ErrMsg("参数有误");
            return;
        }
        start = start.Replace("-", "");
        end = end.Replace("-", "");
        YARDRESVREQ req = new YARDRESVREQ();
        if (login != null && login.ToLower() == "true")
        {
            if (IsLoginReady())
                req.dwApplicantID = curAcc.dwAccNo;
            else
                return;
        }
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        if (!string.IsNullOrEmpty(resvId))
            req.dwResvID = ToUInt(resvId);
        if (!string.IsNullOrEmpty(resvName))
            req.szResvName = resvName;
        if (!string.IsNullOrEmpty(devId))
            req.dwDevID = ToUInt(devId);
        req.dwBeginDate = ToUInt(start);
        req.dwEndDate = ToUInt(end);
        if (!string.IsNullOrEmpty(devcls) && devcls != "0")
            req.dwClassID = ToUInt(devcls);
        if (!string.IsNullOrEmpty(campus) && campus != "0")
            req.szCampusIDs = campus;
        if (!string.IsNullOrEmpty(atytype))
            req.dwActivitySN = ToUInt(atytype);
        if (!string.IsNullOrEmpty(state))
            req.dwCheckStat = ToUInt(state);
        if (!string.IsNullOrEmpty(unNeed))
            req.dwUnNeedStat = ToUInt(unNeed);
        if (!string.IsNullOrEmpty(buildingId) && buildingId != "0")
            req.szBuildingIDs = buildingId;
        if (multi == "true")
        {
            req.szReqExtInfo.szOrderKey = "dwBeginTime ASC,ResvGroupID";
            req.szReqExtInfo.szOrderMode = "DESC";
        }
        else
        {
            req.szReqExtInfo.szOrderKey = "dwBeginTime";
            req.szReqExtInfo.szOrderMode = "ASC";
        }
        if (onlyOpen == "true")
            req.dwUnNeedProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN;
        YARDRESV[] rlt;
        if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<uniresv> list = new List<uniresv>();
            for (int i = 0; i < rlt.Length; i++)
            {
                uniresv resv = new uniresv();
                YARDRESV yrsv = rlt[i];
                if (multi == "true")
                {
                    string starts = "";
                    string ends = "";
                    string states = "";
                    for (; i < rlt.Length; i++)
                    {
                        if (yrsv.dwResvID == rlt[i].dwResvID || (yrsv.dwResvGroupID != null && yrsv.dwResvGroupID == rlt[i].dwResvGroupID))
                        {
                            starts += Get1970Date((int)rlt[i].dwBeginTime) + ",";
                            ends += Get1970Date((int)rlt[i].dwEndTime) + ",";
                            states += GetResvStatus(rlt[i].dwStatus) + ",";
                        }
                        else { i--; break; }
                    }
                    if (starts != "") resv.starts = starts.Substring(0, starts.Length - 1);
                    if (ends != "") resv.ends = ends.Substring(0, ends.Length - 1);
                    if (states != "") resv.states = states.Substring(0, states.Length - 1);
                }
                resv.id = yrsv.dwResvID.ToString();
                resv.group = (int)yrsv.dwResvGroupID;
                resv.name = yrsv.szResvName;
                resv.title = yrsv.szApplicantName + ":" + yrsv.szResvName;
                resv.owner = yrsv.szApplicantName;
                resv.ownerAccno = yrsv.dwApplicantID.ToString();
                resv.dept = yrsv.szUserDeptName;
                resv.devId = yrsv.dwDevID.ToString();
                resv.devName = yrsv.szDevName;
                resv.devDept = yrsv.szDeptName;
                resv.roomName = yrsv.szRoomName;
                resv.labName = yrsv.szLabName;
                resv.campus = yrsv.szCampusName;
                resv.atyId = yrsv.dwActivitySN.ToString();
                resv.atyName = yrsv.szActivityName;
                resv.minUser = (int)yrsv.dwMinAttendance;
                resv.maxUser = (int)yrsv.dwMaxAttendance;
                resv.start = Get1970Date((int)yrsv.dwBeginTime);
                resv.end = Get1970Date((int)yrsv.dwEndTime);
                resv.org = yrsv.szOrganization;
                resv.orger = yrsv.szOrganiger;
                resv.contact = yrsv.szContact;
                resv.phone = yrsv.szHandPhone;

                //预约状态
                resv.state = GetResvStatus(yrsv.dwStatus);
                resv.allDay = false;
                resv.prop = yrsv.dwProperty.ToString();
                resv.islong = (curDev.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0;
                list.Add(resv);
            }
            SucRlt(list);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    private string GetResvStatus(uint? status)
    {
        string state = "othe";
        if ((status & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
        {
            state = "undo";
        }
        else if ((status & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
        {
            state = "doing";
        }
        else if ((status & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
        {
            state = "done";
        }
        return state;
    }

    private void SetYardResv()
    {
        string resvId = Request["resv_id"];
        string resvName = Request["resv_name"];
        string spareDevs = Request["spare_devs"];
        string purpose = Request["purpose"];
        string atyId = Request["aty_id"];
        string atyName = Request["aty_name"];
        string atyType = Request["aty_type"];
        string checkKinds = Request["ck_kind"];
        string prop = Request["prop"];
        string level = Request["level"];
        string security = Request["security"];
        string mbNum = Request["mb_num"];
        string mbMaxNum = Request["mb_max_num"];
        string mbMinNum = Request["mb_min_num"];
        string org = Request["org"];
        string orger = Request["orger"];
        string presenter = Request["presenter"];
        string intro = Request["intro"];
        string cycDesc = Request["cyc_desc"];
        string require = Request["require"];
        string contact = Request["contact"];
        string phone = Request["phone"];
        string email = Request["email"];
        string open = Request["prop_open"];
        string profit = Request["prop_profit"];
        string media = Request["prop_media"];
        string[] starts = string.IsNullOrEmpty(Request["start"]) ? new string[0] : Request["start"].Split(',');
        string[] ends = string.IsNullOrEmpty(Request["end"]) ? new string[0] : Request["end"].Split(',');
        string memo = Request["memo"];
        string upFile = Request["up_file"];
        string thirdId = Request["third_id"];

        YARDRESV para = new YARDRESV();
        if (!string.IsNullOrEmpty(resvId))
            para.dwResvID = ToUInt(resvId);
        if (!string.IsNullOrEmpty(resvName))
            para.szResvName = resvName;
        if (!string.IsNullOrEmpty(purpose))
            para.dwPurpose = ToUInt(purpose);
        if (!string.IsNullOrEmpty(atyId) && atyId != "0")
            para.dwActivitySN = ToUInt(atyId);
        if (!string.IsNullOrEmpty(atyName))
            para.szActivityName = atyName;
        if (!string.IsNullOrEmpty(checkKinds) && checkKinds != "0")
            para.dwCheckKinds = ToUInt(checkKinds);
        if (!string.IsNullOrEmpty(level) && level != "0")
            para.dwActivityLevel = ToUInt(level);
        if (!string.IsNullOrEmpty(security) && security != "0")
            para.dwSecurityLevel = ToUInt(security);
        if (!string.IsNullOrEmpty(mbNum))
        {
            string[] ns = mbNum.Split('-');
            if (ns.Length > 1)
            {
                para.dwMinAttendance = ToUInt(ns[0]);
                para.dwMaxAttendance = ToUInt(ns[1]);
            }
        }
        if (!string.IsNullOrEmpty(mbMinNum))
            para.dwMinAttendance = ToUInt(mbMinNum);
        if (!string.IsNullOrEmpty(mbMaxNum))
            para.dwMaxAttendance = ToUInt(mbMaxNum);

        para.szMemo = memo;
        para.szApplicationURL = upFile;
        //预约人
        para.dwApplicantID = curAcc.dwAccNo;
        para.szApplicantName = curAcc.szTrueName;
        para.dwUserDeptID = curAcc.dwDeptID;
        para.szUserDeptName = curAcc.szDeptName;

        //联系人
        para.szContact = contact;
        para.szHandPhone = phone;
        para.szEmail = email;
        //参与人要求
        if (!string.IsNullOrEmpty(require))
        {
            if (require[require.Length - 1] == ',')
                require = require.Substring(0, require.Length - 1);
            para.szDesiredUser = require;
        }
        //主持人
        para.szPresenter = presenter;
        //介绍
        para.szIntroInfo = intro;
        //设备
        para.dwDevID = curDev.dwDevID;
        para.szDevName = curDev.szDevName;
        para.dwDevKind = curDev.dwKindID;
        para.dwLabID = curDev.dwLabID;
        para.szLabName = curDev.szLabName;
        if (!string.IsNullOrEmpty(spareDevs))
            para.szSpareDevIDs = spareDevs;

        para.szOrganization = org;
        para.szOrganiger = orger;
        //活动类型
        para.dwKind = ToUInt(atyType);
        //属性
        if (!string.IsNullOrEmpty(prop) && prop != "0")
            para.dwProperty = ToUInt(prop);
        else
            para.dwProperty = 0;
        if (open != "true")
            para.dwProperty += (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN;
        if (profit == "true")
            para.dwProperty += (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT;
        if (media == "true")
            para.dwProperty += 0x4000000;
        if (!string.IsNullOrEmpty(thirdId))
        {
            para.dwProperty += (uint)UNIRESERVE.DWPROPERTY.RESVPROP_BYTHIRD;
            para.dwCheckTime = ToUInt(thirdId);
        }
        if (para.dwProperty == 0)
            para.dwProperty = null;
        //时间
        para.szCycRule = cycDesc;
        if (starts.Length > 0 && starts.Length == ends.Length)
        {
            para.dwBeginTime = (uint?)Get1970Seconds(starts[0]);
            para.dwEndTime = (uint?)Get1970Seconds(ends[0]);
            if (m_Request.Reserve.SetYardResv(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                List<retresv> retList = new List<retresv>();
                for (int i = 1; i < starts.Length; i++)
                {
                    if (string.IsNullOrEmpty(starts[i]) || string.IsNullOrEmpty(ends[i])) continue;
                    YARDRESV p = new YARDRESV();
                    p = para;
                    p.dwResvID = null;
                    p.dwResvGroupID = para.dwResvID;
                    p.dwBeginTime = (uint?)Get1970Seconds(starts[i]);
                    p.dwEndTime = (uint?)Get1970Seconds(ends[i]);
                    if (m_Request.Reserve.SetYardResv(p, out p) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        retresv ret = new retresv();//记录成功的预约
                        ret.ids = para.dwResvID.ToString();
                        retList.Add(ret);
                    }
                    else
                    {
                        ErrMsg(m_Request.szErrMsg);
                        //根据记录删除已成功的预约
                        DelReserve(para.dwResvID.ToString());//删主预约
                        foreach (retresv it in retList)
                            DelReserve(it.ids);
                        return;
                    }
                }
                SucRlt(para);
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
        }
    }

    private void GetResvList()
    {
        string start = Request["start"];
        string end = Request["end"];
        string devId = Request["dev_id"];
        string accno = Request["accno"];
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            ErrMsg("时区有误");
            return;
        }
        List<uniresv> resvs = new List<uniresv>();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESVREQ req = new RESVREQ();
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        req.dwBeginDate = Convert.ToUInt32(start);
        req.dwEndDate = Convert.ToUInt32(end);
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        if (!string.IsNullOrEmpty(devId))
        {
            if (!GetDev(out curDev)) return;
            req.dwDevID = ToUInt(devId);
        }
        if (accno != null)//非空即可
        {
            if (!IsLoginReady()) return;
            req.dwOwner = curAcc.dwAccNo;
        }
        UNIRESERVE[] vtResult;
        uResponse = m_Request.Reserve.Get(req, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                uniresv resv = new uniresv();
                resv.id = vtResult[i].dwResvID.ToString();
                resv.title = vtResult[i].szOwnerName;
                resv.start = Get1970Date((int)vtResult[i].dwBeginTime);
                resv.end = Get1970Date((int)vtResult[i].dwEndTime);
                resv.labId = vtResult[i].dwLabID.ToString();
                resv.labName = vtResult[i].szLabName;
                resv.ltch = (int)vtResult[i].dwTeachingTime;
                //预约状态
                if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                {
                    resv.state = "undo";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                {
                    resv.state = "doing";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                {
                    resv.state = "done";
                }
                else
                {
                    resv.state = "othe";
                }
                resv.allDay = false;
                resv.prop = vtResult[i].dwProperty.ToString();
                resv.islong = curDev.dwProperty == null ? false : (curDev.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0;
                resvs.Add(resv);
            }
            SucRlt(resvs);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }

    private void SetRTResv()
    {
        string devId = Request["dev_id"];
        string rtId = Request["rt_id"];
        string testName = Request["test_name"];
        string start = Request["start"];
        string end = Request["end"];
        string memo = Request["memo"];
        string samples = Request["samples"];
        string prop = Request["prop"];
        string upFile = Request["up_file"];
        if (string.IsNullOrEmpty(devId) || string.IsNullOrEmpty(rtId) || string.IsNullOrEmpty(testName) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            ErrMsg("参数有误");
            return;
        }
        if (DateTime.Parse(start) <= DateTime.Now)
        {
            ErrMsg("开始时间不能小于当前时间！");
            return;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTRESV setResv = new RTRESV();
        setResv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;

        if (rtId == "0")
        {
            ErrMsg("不支持的实验类型！");
            return;
        }
        setResv.dwRTID = Convert.ToUInt32(rtId);

        setResv.szTestName = testName;

        setResv.dwBeginTime = (uint?)Get1970Seconds(start);
        setResv.dwEndTime = (uint?)Get1970Seconds(end);

        setResv.dwProperty = ToUInt(prop);
        setResv.szMemo = memo;
        if (!string.IsNullOrEmpty(samples))
        {
            RESVSAMPLE[] splList = FmtSplLsit(samples, rtId);
            setResv.ResvSample = splList;
        }
        setResv.dwOwner = curAcc.dwAccNo;
        setResv.szOwnerName = curAcc.szTrueName.ToString();

        setResv.dwLabID = curDev.dwLabID;
        setResv.dwDevID = curDev.dwDevID;
        setResv.dwDevKind = curDev.dwKindID;
        setResv.dwManID = curDev.dwAttendantID;
        setResv.szManName = curDev.szAttendantName;
        setResv.dwPurpose = 2 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH;
        uResponse = m_Request.Reserve.SetRTResv(setResv, out setResv);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg("预约成功！");
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void SetResv()
    {
        string resvId = Request["resv_id"];
        string devId = Request["dev_id"];
        string labId = Request["lab_id"];
        string roomId=Request["room_id"];
        string kindId = Request["kind_id"];
        string resvKind = Request["resv_kind"];
        string testName = Request["test_name"];
        string testId = Request["test_id"];
        string term = Request["term"];
        string groupId = Request["group_id"];
        bool isAccno = Request["mb_accno"] == "true";
        string mbList = Request["mb_list"];
        string minUser = Request["min_user"];
        string maxUser = Request["max_user"];
        string prop = Request["prop"];
        string type = Request["type"];
        string start = Request["start"];
        string end = Request["end"];
        string cut = Request["cut"];
        //string date = Request["date"];
        //string startTime = Request["start_time"];
        //string endTime = Request["end_time"];
        string startDate = Request["start_date"];
        string endDate = Request["end_date"];
        string openStart = Request["open_start"];
        string openEnd = Request["open_end"];
        string memo = Request["memo"];
        string upFile = Request["up_file"];
        //if (!string.IsNullOrEmpty(date)&&!string.IsNullOrEmpty(startTime)&&!string.IsNullOrEmpty(endTime))//日期+时间模式
        //{
        //    start = date + " " + startTime;
        //    end = date + " " + endTime;
        //}
        if (cut == "true")//提前结束
        {
            end = DateTime.Now.AddMinutes(1).ToString("yyyy-MM-dd HH:mm");
        }
        if (!string.IsNullOrEmpty(openStart) && !string.IsNullOrEmpty(openEnd) && !string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))//纯日期模式
        {
            start = startDate + " " + openStart;
            //if (DateTime.Parse(start) <= DateTime.Now)
            //{
            //    start = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm");
            //}
            end = endDate + " " + openEnd;
        }
        if (DateTime.Parse(start) <= DateTime.Now)
        {
            start = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm");
        }
        string[] starts = string.IsNullOrEmpty(start) ? new string[0] : start.Split(',');
        string[] ends = string.IsNullOrEmpty(end) ? new string[0] : end.Split(',');

        UNIRESERVE resv = new UNIRESERVE();
        if (!string.IsNullOrEmpty(resvId))//修改
        {
            UNIRESERVE[] list = GetResvById(resvId);
            if (list != null && list.Length > 0)
                resv = list[0];
            else
            {
                ErrMsg("未找到预约");
                return;
            }
        }
        else//新建
        {
            if ((string.IsNullOrEmpty(devId) && string.IsNullOrEmpty(kindId)) || starts.Length == 0 || ends.Length == 0)
            {
                ErrMsg("缺少必要的参数");
                return;
            }
            //预约成员
            if (!string.IsNullOrEmpty(groupId))
            {
                resv.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
                resv.dwMemberID = uint.Parse(groupId);
                resv.szOwnerName = curAcc.szTrueName.ToString() + "group:" + groupId;
            }
            else if (ToUInt(maxUser) > 1)//组预约
            {
                uint? min = ToUInt(minUser);
                uint? max = ToUInt(maxUser);
                if (!string.IsNullOrEmpty(mbList))
                {
                    if (mbList[0] == '&')
                    { //首字符为&表示列表为accno，否则为loginname
                        isAccno = true;
                        mbList = mbList.Substring(1);//去掉&
                    }
                    int user_num = mbList.Split(',').Length;
                    if (min != 0 && user_num < min)
                    {
                        ErrMsg("组成员不足");
                        return;
                    }
                    else if (max != 0 && user_num > max)
                    {
                        ErrMsg("组成员过多");
                        return;
                    }
                }
                else
                {
                    isAccno = true;
                    mbList = curAcc.dwAccNo.ToString();
                }
                uint gid = NewGroup(testName + ":" + Translate("成员组"), mbList, min, max, isAccno);
                resv.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
                resv.dwMemberID = gid;
                resv.szOwnerName = curAcc.szTrueName + "group:" + gid.ToString();
            }
            else
            {
                resv.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
                resv.dwMemberID = curAcc.dwAccNo;
                resv.szOwnerName = curAcc.szTrueName.ToString();
            }
            if (string.IsNullOrEmpty(term))
            {
                resv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
                if (!string.IsNullOrEmpty(prop) && prop != "0")
                    resv.dwProperty = ToUInt(prop);
            }
            else//教学预约
            {
                resv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
                resv.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
                resv.dwYearTerm = ToUInt(term);
                resv.dwTestItemID = ToUInt(testId);
            }
            //设置预约设备
            RESVDEV[] resvDev = new RESVDEV[1];
            uint? clskind = 0;
            if (type != null && type == "kind")
            {
                if (string.IsNullOrEmpty(labId))
                {
                    ErrMsg("缺少实验室ID");
                    return;
                }
                UNIDEVKIND kind = GetDevKind(ToUInt(kindId));
                if (kind.dwKindID != null)
                {
                    resvDev[0] = new RESVDEV();
                    resvDev[0].dwDevKind = kind.dwKindID;
                    resvDev[0].dwDevNum = 1;
                    resvDev[0].szDevName = kind.szKindName;
                    resv.dwLabID = uint.Parse(labId);
                    clskind = kind.dwClassKind;
                    //临时方法，查找房间号
                    DEVREQ req = new DEVREQ();
                    req.szKindIDs = kindId;
                    req.szLabIDs = labId;
                    if (!string.IsNullOrEmpty(roomId))
                    {
                        req.szRoomIDs = roomId;
                    }
                    UNIDEVICE[] rlt;
                    if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
                    {
                        resvDev[0].szRoomNo = rlt[0].szRoomNo;
                        resv.szLabName = rlt[0].szLabName;
                    }
                    else
                    {
                        MsgBox("临时获取房间失败");
                        return;
                    }
                }
                else
                {
                    ErrMsg("参数有误");
                    return;
                }
            }
            else
            {
                UNIDEVICE[] temp = GetDevById(devId);
                if (temp != null && temp.Length > 0)
                {
                    UNIDEVICE dev = temp[0];
                    resvDev[0] = new RESVDEV();
                    resvDev[0].dwDevStart = dev.dwDevSN;
                    resvDev[0].dwDevEnd = dev.dwDevSN;
                    resvDev[0].dwDevKind = dev.dwKindID;
                    resvDev[0].szRoomNo = dev.szRoomNo.ToString();
                    resvDev[0].dwDevNum = 1;
                    resvDev[0].szDevName = dev.szDevName.ToString();
                    resv.szLabName = dev.szLabName.ToString();
                    resv.dwLabID = dev.dwLabID;
                    clskind = dev.dwClassKind;
                }
                else
                {
                    ErrMsg("参数有误");
                    return;
                }
            }
            //使用模式
            if ((clskind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)//外借
            {
                resv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_LEASE;
                resv.dwPurpose = resv.dwPurpose | (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN;
            }
            else
            {
                resv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
                if ((clskind & (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT) > 0)//座位
                {
                    resv.dwPurpose = resv.dwPurpose | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
                }
                else if ((clskind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) > 0)//空间
                {
                    resv.dwPurpose = resv.dwPurpose | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
                }
                else if ((clskind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER) > 0)//电子阅览室
                {
                    resv.dwPurpose = resv.dwPurpose | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
                }
            }
            resv.ResvDev = resvDev;
        }
        if (!string.IsNullOrEmpty(resvKind))
            resv.dwKind = ToUInt(resvKind);
        if (!string.IsNullOrEmpty(testName))
            resv.szTestName = testName;
        if (!string.IsNullOrEmpty(memo))
            resv.szMemo = memo;
        if ((string.IsNullOrEmpty(testName) || testName == ","||testName=="0")&& !string.IsNullOrEmpty(resvKind))
        {
            CODINGTABLEREQ req = new CODINGTABLEREQ();
            req.dwCodeType = (uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND;
            req.szCodeSN = resvKind;
            CODINGTABLE[] rlt;
            if (m_Request.System.GetCodingTable(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS&&rlt.Length>0)
            {
                resv.szTestName = rlt[0].szCodeName;
            }
        }

        if (!string.IsNullOrEmpty(upFile))
            resv.szApplicationURL = upFile;
        //时间
        string msg = "ok";
        if (!string.IsNullOrEmpty(resvId))//修改
        {
            if (starts.Length > 0) resv.dwBeginTime = (uint?)Get1970Seconds(starts[0]);
            if (ends.Length > 0) resv.dwEndTime = (uint?)Get1970Seconds(ends[0]);
            if (m_Request.Reserve.Set(resv, out resv) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                msg = m_Request.szErrMsg;
            }
        }
        else//新建
        {
            List<string> sucList = new List<string>();
            for (int i = 0; i < starts.Length; i++)
            {
                string st = starts[i];
                string ed = ends[i];
                if (string.IsNullOrEmpty(st) || string.IsNullOrEmpty(ed))
                    continue;
                Logger.Trace("新建预约-开始:" + st + " 结束:" + ed);
                resv.dwBeginTime = (uint?)Get1970Seconds(st);
                resv.dwEndTime = (uint?)Get1970Seconds(ed);
                //if (!string.IsNullOrEmpty(st))
                //{
                //    string BeginDate = ConvertStr(st);
                //    //if (DateTime.Parse(BeginDate) <= DateTime.Now)
                //    //{
                //    //    msg="开始时间不能小于当前时间！";
                //    //    DelReserve(
                //    //    break;
                //    //}
                //}
                REQUESTCODE cd = m_Request.Reserve.Set(resv, out resv);
                if (cd == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    sucList.Add(resv.dwResvID.ToString());
                    resv.dwResvID = null;
                }
                else
                {
                    //msg = GetSimilStr((st.Substring(0, 10) +(m_Request.szErrMsg)), "$最少需$人同时使用", "$At least $ person");
                    msg = Translate(st.Substring(0, 10) + (m_Request.szErrMsg));
                    //删除已成功的预约
                        foreach (string item in sucList)
                        DelReserve(item);
                    //
                    break;
                }
            }
        }
        if (msg == "ok")
            SucMsg();
        else
            ErrMsg(msg);
    }
    public string GetSimilStr(string szSer, string szXml, string szValue)
    {
        string[] szList = szXml.Split('$');
        string[] szValueList = szValue.Split('$');
        bool bResSimel = true;
        for (int i = 0; i < szList.Length; i++)
        {
            if (!(szSer.IndexOf(szList[i]) > -1))
            {
                bResSimel = false;
                return "";
            }
        }
        for (int i = 0; i < szValueList.Length; i++)
        {
            try
            {
                szSer = szSer.Replace(szList[i], szValueList[i]);
            }
            catch (Exception e)
            {
            }
        }
        return szSer;
    }
    private void SetTeachResv()
    {
        string objType = Request["obj_type"];
        string resvList = Request["resv_list"];
        string testId = Request["test_id"];
        string term = Request["term"];
        string status = Request["status"];
        string tmType = Request["tm_type"];
        string purpose = Request["purpose"];
        if (string.IsNullOrEmpty(term) || string.IsNullOrEmpty(resvList) || string.IsNullOrEmpty(testId))
        {
            ErrMsg("参数有误");
            return;
        }
        UNITESTITEM test = GetTestItemByID(testId);
        if (test.dwTestItemID == null)
        {
            ErrMsg("获取实验项目出错");
            return;
        }
        List<retresv> list = new List<retresv>();
        /*解析预约信息字符串
         【 ; 】分隔多个预约
         【 &】 分隔出预约组[0] 房间号[1] 节次时间[2]=（周次WW星期D（0开始）开始节次SS（1开始）持续节次DD）WWDSSDD  绝对时间[3]=16位=（日期8位+时间8位）*/
        string[] tmp = resvList.Split(';');
        for (int i = 0; i < tmp.Length; i++)
        {
            string rsv = tmp[i];
            if (rsv == "" || rsv.Length < 8) continue;
            retresv resv = new retresv();
            string[] idsg = rsv.Split('&');
            if (idsg.Length < 3) continue;
            string[] gs = idsg[0].Split(',');
            resv.groupId = ToUInt(gs[0]);
            if (gs.Length > 1) resv.groupName = gs[1];
            resv.ids = idsg[1];
            string ltch = idsg[2];
            resv.ltch = ltch;
            if (idsg.Length > 2)
            {
                uint tch = ToUInt(ltch.Substring(ltch.Length - 7));
                uint diff = tch % 100;
                uint sec = (tch % 10000) / 100;
                resv.tchTime = (tch / 100 * 100 + (sec + diff)) % 10000000;
                resv.diff = diff;
                if (idsg.Length == 4)
                {
                    string tm = idsg[3];
                    resv.time = tm;
                    string start = tm.Substring(8, 2) + ":" + tm.Substring(10, 2);
                    string end = tm.Substring(12, 2) + ":" + tm.Substring(14, 2);
                    string dt = toDate(ToUInt(tm.Substring(0, 8)));
                    resv.start = dt + start;
                    resv.end = dt + end;
                }
            }
            else
            {
                continue;
            }
            resv.state = 0;
            resv.memo = "未创建";
            list.Add(resv);
        }
        for (int j = 0; j < list.Count; j++)
        {
            retresv resv = list[j];
            retresv ret = new retresv();
            ret = resv;
            //新建预约            
            UNIRESERVE resvSet = new UNIRESERVE();
            resvSet.dwTestItemID = ToUInt(testId);
            resvSet.szTestName = test.szTestName;
            resvSet.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
            resvSet.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
            if (!string.IsNullOrEmpty(purpose)) resvSet.dwPurpose = ToUInt(purpose);
            else resvSet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
            resvSet.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
            if (!string.IsNullOrEmpty(status))
                resvSet.dwStatus = ToUInt(status);
            //学期
            resvSet.dwYearTerm = ToUInt(term);
            //成员
            resvSet.dwMemberID = resv.groupId;
            if (string.IsNullOrEmpty(resv.groupName))
            {
                resvSet.szMemberName = "预约组:" + resv.groupId;
            }
            else
            {
                resvSet.szMemberName = resv.groupName;
            }

            resvSet.dwOwner = curAcc.dwAccNo;
            resvSet.szOwnerName = curAcc.szTrueName;
            //设置预约设备
            int uDevNum = 0;
            UNILAB lab;
            RESVDEV[] resvDev = GetResvDev(resv.ids, out uDevNum, out lab);
            if (resvDev == null || resvDev.Length == 0)
            {
                ret.state = 2;
                ret.memo = "无可用设备或获取设备有误";
                list[j] = ret;
                continue;
            }
            resvSet.ResvDev = resvDev;
            resvSet.dwLabID = lab.dwLabID;
            resvSet.szLabName = lab.szLabName;
            uint ckClsRoomCapacity=ToUInt(GetConfig("ckClsRoomCapacity"));
            if (ckClsRoomCapacity>0 && !(IsRoomDeviceAndGroupMember((ckClsRoomCapacity&2)>0?uDevNum-1:uDevNum, (uint)resvSet.dwMemberID)))
            {
                ret.state = 2;
                ret.memo = "房间内设备数目小于班内人数，影响学生上课！";
                list[j] = ret;
                break;
            }
            //设置时间
            if (!string.IsNullOrEmpty(resv.start))
            {
                resvSet.dwBeginTime = (uint)Get1970Seconds(resv.start);
                resvSet.dwEndTime = (uint)Get1970Seconds(resv.end);
            }
            else if (resv.tchTime != null)
            {
                resvSet.dwTeachingTime = resv.tchTime;
            }
            else
            {
                continue;
            }
            if (m_Request.Reserve.Set(resvSet, out resvSet) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                ret.state = 1;
                ret.resvId = resvSet.dwResvID.ToString();
                ret.memo = "预约成功";
            }
            else
            {
                ret.state = 2;
                ret.memo = m_Request.szErrMsg;
            }
            list[j] = ret;
        }
        SucRlt(list);
    }

    private bool IsRoomDeviceAndGroupMember(int uDevNum, uint? uGroupID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        GROUPMEMDETAILREQ vrMemberReq = new GROUPMEMDETAILREQ();
        vrMemberReq.dwGroupID = uGroupID;
        GROUPMEMDETAIL[] vtRes;
        int uMemberNum = 0;
        uResponse = m_Request.Group.GetGroupMemDetail(vrMemberReq, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            uMemberNum = vtRes.Length;
        }
        if ((uDevNum - uMemberNum) < 0)
        {
            return false;
        }
        return true;
    }
    private RESVDEV[] GetResvDev(string szRoomGroup, out int uDevNum, out UNILAB lab)
    {
        uDevNum = 0;
        lab = new UNILAB();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (!string.IsNullOrEmpty(szRoomGroup))
        {
            DEVREQ vrGet = new DEVREQ();
            vrGet.szRoomIDs = szRoomGroup;
            vrGet.szReqExtInfo.szOrderKey = "dwRoomID";
            vrGet.szReqExtInfo.szOrderMode = "ASC";
            UNIDEVICE[] vtDev;
            uResponse = m_Request.Device.Get(vrGet, out vtDev);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
            {
                uDevNum = vtDev.Length;
                uint? start = 0;
                uint? end = 0;
                int len = 0;
                List<RESVDEV> vtRes = new List<RESVDEV>();
                for (int i = 0; i < vtDev.Length; i++)
                {
                    UNIDEVICE dev = vtDev[i];
                    if (i == 0)
                    {
                        lab.dwLabID = dev.dwLabID;
                        lab.szLabName = dev.szLabName;
                    }
                    if (len == 0 || dev.dwDevSN >= end)
                    {
                        end = dev.dwDevSN;
                    }
                    if (len == 0 || dev.dwDevSN <= start)
                    {
                        start = dev.dwDevSN;
                    }
                    if (i + 1 == vtDev.Length || dev.dwRoomID != vtDev[i + 1].dwRoomID || dev.dwKindID != vtDev[i + 1].dwKindID)
                    {
                        RESVDEV rdev = new RESVDEV();
                        rdev.szRoomNo = dev.szRoomNo;
                        rdev.dwDevStart = 1;//start;
                        rdev.dwDevEnd = uint.MaxValue - 1;//end;
                        rdev.dwDevNum = (uint)len + 1;
                        rdev.dwDevKind = dev.dwKindID;
                        len = -1;
                        //start = uint.MaxValue - 1;
                        //end = 0;
                        vtRes.Add(rdev);
                    }
                    len++;
                }
                return vtRes.ToArray();
            }
        }
        return null;
    }
    UNIRESERVE[] GetResvById(string id)
    {
        RESVREQ req = new RESVREQ();
        req.dwResvID = ToUInt(id);
        UNIRESERVE[] rlt;
        m_Request.Reserve.Get(req, out rlt);
        return rlt;
    }

    private void GetUserRsvList()
    {
        DateTime start = ConvertTime(Convert.ToInt64(Request["start"]));
        DateTime end = ConvertTime(Convert.ToInt64(Request["end"]));
        List<uniresv> resvs = new List<uniresv>();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwBeginDate = Convert.ToUInt32(start.ToString("yyyyMMdd"));
        vrGet.dwEndDate = Convert.ToUInt32(end.ToString("yyyyMMdd"));
        vrGet.dwResvID = 0;//获取多条样品费用，要设置为0
        vrGet.dwMAccNo = curAcc.dwAccNo;
        vrGet.dwUnNeedStat = (int)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
        {
            SucRlt(vtResult);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private string GetDevSta(uint date)
    {
        string ret = "null";

        DEVRESVSTAT[] sta = GetDevRsvSta(date.ToString(), curDev.dwDevID.ToString());
        if (sta != null && sta.Length > 0)
        {
            ret = "{";
            DAYOPENRULE[] open = sta[0].szOpenInfo;
            UNIRESVRULE rule = sta[0].szRuleInfo;
            if (open.Length > 0)
            {
                ret += "\"open\":{\"start\":\"" + open[0].dwBegin.ToString() + "\",\"end\":\"" + open[0].dwEnd.ToString() + "\"},";
            }
            ret += "\"rule\":{\"earliest\":" + rule.dwEarliestResvTime + ",\"latest\":" + rule.dwLatestResvTime + ",\"max\":" + rule.dwMaxResvTime + ",\"min\":" + rule.dwMinResvTime + "}";
            ret += "}";
        }
        return ret;
    }

    private string GetSpl()
    {
        string list = "";
        if (!string.IsNullOrEmpty(Request["rt_id"]))
        {
            RTDEVSAMPLEREQ req = new RTDEVSAMPLEREQ();
            req.dwDevID = curDev.dwDevID;
            req.dwRTID = ToUInt(Request["rt_id"]);
            RTDEVSAMPLE[] spls;
            if (m_Request.Fee.RTDevSampleGet(req, out spls) == REQUESTCODE.EXECUTE_SUCCESS && spls != null)
            {
                for (int i = 0; i < spls.Length; i++)
                {
                    RTDEVSAMPLE spl = spls[i];
                    string fee = "(免费)";
                    if (spl.dwUnitFee > 0)
                    {
                        fee = "(" + ((double)spl.dwUnitFee / 100.00).ToString("F2") + " 元/" + spl.szUnitName + ")";
                    }
                    list += "<option value='" + spl.dwSampleSN + "' unit='" + spl.szUnitName + "' fee='" + spl.dwUnitFee + "'>" + spl.szSampleName + fee + "</option>";
                }
            }
        }
        return list;
    }

    private void CkRTRsv()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uint id = Convert.ToUInt32(Request["id"]);
        RTRESVREQ req = new RTRESVREQ();
        req.dwResvID = id;
        RTRESV[] rlt;
        uResponse = m_Request.Reserve.GetRTResv(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                string ck = Request["order"];
                RTRESVCHECK set = new RTRESVCHECK();
                set.RTResv = rlt[0];
                if (ck == "ok")
                {
                    set.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
                }
                else if (ck == "fail")
                {
                    set.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
                }
                else
                {
                    ErrMsg("审核失败，缺少执行命令！");
                    return;
                }
                uResponse = m_Request.Reserve.RTResvCheck(set);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    SucMsg();
                }
                else
                {
                    ErrMsg(m_Request.szErrMsg);
                    return;
                }
            }
            else
            {
                ErrMsg("审核失败，未能获取预约！");
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void RsvForm()
    {
        string rtId = ConvertStr(Request["rtSel"]);
        string szlabName = ConvertStr(Request["labName"]);
        //string szlabMan = ConvertStr(Request["labMan"]);
        //string szlabManId = ConvertStr(Request["labManId"]);
        string BeginDate = ConvertStr(Request["beginDate"]);
        if (DateTime.Parse(BeginDate) <= DateTime.Now)
        {
            ErrMsg("开始时间不能小于当前时间！");
            return;
        }
        string EndDate = ConvertStr(Request["endDate"]);

        uint isCheck = ConvertStr(Request["check"]) == "1" ? (uint)UNIRESERVE.DWPROPERTY.RESVPROP_MANDO : (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
        uint isMat = ConvertStr(Request["selMat"]) == "1" ? (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFCONSUMABLE : 0;
        string szMemo = ConvertStr(Request["memo"]);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTRESV setResv = new RTRESV();
        setResv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;

        if (rtId == "0")//个人实验
        {
            ErrMsg("不支持的实验类型！");
            return;
        }
        setResv.dwRTID = Convert.ToUInt32(rtId);

        setResv.szTestName = szlabName;

        setResv.dwBeginTime = (uint?)Get1970Seconds(BeginDate);
        setResv.dwEndTime = (uint?)Get1970Seconds(EndDate);

        setResv.dwProperty = isCheck | isMat;
        setResv.szMemo = szMemo;
        if (!string.IsNullOrEmpty(Request["spls"]))
        {
            RESVSAMPLE[] splList = FmtSplLsit(Request["spls"], rtId);
            setResv.ResvSample = splList;
        }
        setResv.dwOwner = curAcc.dwAccNo;
        setResv.szOwnerName = curAcc.szTrueName.ToString();

        setResv.dwLabID = curDev.dwLabID;
        setResv.dwDevID = curDev.dwDevID;
        setResv.dwDevKind = curDev.dwKindID;
        setResv.dwManID = curDev.dwAttendantID;
        setResv.szManName = curDev.szAttendantName;
        uResponse = m_Request.Reserve.SetRTResv(setResv, out setResv);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg("预约成功！");
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private RESVSAMPLE[] FmtSplLsit(string p, string rtId)
    {
        string[] list = p.Split(',');
        RESVSAMPLE[] spls = new RESVSAMPLE[list.Length / 3];
        for (int i = 0; i < list.Length; i += 3)
        {
            RESVSAMPLE spl = new RESVSAMPLE();
            spl.dwResvID = ToUInt(rtId);
            spl.dwSampleSN = ToUInt(list[i]);
            spl.dwUnitFee = ToUInt(list[i + 1]);
            spl.dwSampleNum = ToUInt(list[i + 2]);
            spls[i / 3] = spl;
        }
        return spls;
    }

    private void GetDevRTRsvList()
    {
        string start = Request["start"];
        string end = Request["end"];
        List<uniresv> resvs = new List<uniresv>();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        //获取项目科研预约
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrGet.dwBeginDate = Convert.ToUInt32(start);
        vrGet.dwEndDate = Convert.ToUInt32(end);
        vrGet.dwDevID = curDev.dwDevID;
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                uniresv resv = new uniresv();
                resv.id = vtResult[i].dwResvID.ToString();
                resv.title = vtResult[i].szOwnerName;
                resv.ownerAccno = vtResult[i].dwOwner.ToString();
                resv.owner = vtResult[i].szOwnerName;
                resv.start = Get1970Date((int)vtResult[i].dwBeginTime);
                resv.end = Get1970Date((int)vtResult[i].dwEndTime);
                //预约状态
                if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                {
                    resv.state = "undo";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                {
                    resv.state = "doing";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                {
                    resv.state = "done";
                }
                else
                {
                    resv.state = "othe";
                }
                resv.allDay = false;
                resv.prop = vtResult[i].dwProperty.ToString();
                resv.islong = (curDev.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0;
                resvs.Add(resv);
            }
        }
        SucRlt(resvs);
    }
    private string ConvertDate(string p)
    {
        string y = p.Substring(0, 4);
        string m = p.Substring(4, 2);
        string d = p.Substring(6, 2);
        return y + "-" + m + "-" + d;
    }

    private long MilliTimeStamp(DateTime TheDate)
    {
        DateTime d1 = new DateTime(1970, 1, 1);
        DateTime d2 = TheDate.ToUniversalTime();
        TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

        return (long)ts.TotalMilliseconds;
    }
    private DateTime ConvertTime(long milliTime)
    {
        long timeTricks = new DateTime(1970, 1, 1).Ticks + milliTime * 10000000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
        return new DateTime(timeTricks);
    }
    private bool GetDev(out UNIDEVICE dev)
    {
        if (Session["CUR_DEV"] == null)
        {
            string devId = Request["dev_id"];
            if (!string.IsNullOrEmpty(devId))
            {
                UNIDEVICE[] devs = GetDevById(devId);
                if (devs != null && devs.Length > 0)
                {
                    dev = devs[0];
                    return true;
                }
            }
            ErrMsg("获取设备信息时出错！");
            dev = new UNIDEVICE();
            return false;
        }
        else
        {
            dev = (UNIDEVICE)Session["CUR_DEV"];
            return true;
        }
    }

    private string GetFee()
    {
        uint useFee = 0;
        uint useFeeUnit = 1;
        uint sampleFee = 0;
        uint sampleFeeUnit = 1;
        uint subFee = 0;
        uint subFeeUnit = 1;
        string feeName = "";
        if (!string.IsNullOrEmpty(Request["rt_id"]))
        {
            RTDEVFEEREQ vrGet = new RTDEVFEEREQ();
            vrGet.dwDevID = curDev.dwDevID;
            vrGet.dwRTID = ToUInt(Request["rt_id"]);
            UNIFEE vrResult;
            if (m_Request.Fee.RTDevFeeGet(vrGet, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                feeName = vrResult.szFeeName;
                FEEDETAIL[] fees = vrResult.szFeeDetail;
                for (int i = 0; i < fees.Length; i++)
                {
                    if (fees[i].dwUnitFee == null || fees[i].dwUnitTime == null)
                    {
                        fees[i].dwUnitFee = 0;
                    }
                    if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                    {
                        useFee = (uint)fees[i].dwUnitFee;
                        useFeeUnit = (uint)fees[i].dwUnitTime;
                    }
                    else if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                    {
                        sampleFee = (uint)fees[i].dwUnitFee;
                        sampleFeeUnit = (uint)fees[i].dwUnitTime;
                    }
                    else if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                    {
                        subFee = (uint)fees[i].dwUnitFee;
                        subFeeUnit = (uint)fees[i].dwUnitTime;
                    }
                }
            }
        }
        if (feeName.Length > 0)
        {
            feeName.Substring(0, feeName.Length - 1);
        }
        return "\"feeName\":\"" + feeName.Replace('"', '”') + "\",\"useFee\":\"" + useFee + "\",\"sampleFee\":\"" + sampleFee + "\",\"subFee\":\"" + subFee +
                    "\",\"useFeeUnit\":\"" + useFeeUnit + "\",\"sampleFeeUnit\":\"" + sampleFeeUnit + "\",\"subFeeUnit\":\"" + subFeeUnit + "\"";
    }
    private string GetRTestes()
    {
        string optCourse = "<option value='0'>未选择</option>";
        RESEARCHTEST[] opts = GetRTestes("", "", "", "", curAcc.dwAccNo.ToString());
        if (opts != null && opts.Length > 0)
        {
            for (int i = 0; i < opts.Length; i++)
            {
                //临时方法，查询成员状态
                RTMEMBER[] mbs = opts[i].RTMembers;
                for (int j = 0; j < mbs.Length; j++)
                {
                    if (mbs[j].dwAccNo == curAcc.dwAccNo && ((mbs[j].dwStatus & 2) > 0))
                    {
                        optCourse += "<option value='" + opts[i].dwRTID + "' tutor='" + opts[i].szHolderName + "' leader='" + opts[i].szLeaderName + "'>" + opts[i].szRTName.Replace('"', '”') + "</option>";
                    }
                }
            }
        }
        return optCourse;
    }
    private string GetManInfo()
    {
        string optMan = "";
        optMan = "<option value='" + curDev.dwAttendantID + "' phone='" + curDev.szAttendantTel + "'>" + curDev.szAttendantName + "</option>";
        return optMan;
    }
    RESEARCHTEST[] GetRTestesByTutor(string id)
    {
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        if (id != null)
        {
            vrGet.dwRTID = Convert.ToUInt32(id);
        }
        else
        {
            if ((curAcc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
            {
                vrGet.dwHolderID = curAcc.dwAccNo;
                myTutor = "(导师)";
            }
            else
            {
                TUTORREQ vrPra = new TUTORREQ();
                vrPra.dwStudentAccNo = curAcc.dwAccNo;
                UNITUTOR[] vrTutor;
                if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
                {
                    TUTORSTUDENTREQ vrStuGet = new TUTORSTUDENTREQ();
                    vrStuGet.dwTutorID = vrTutor[0].dwAccNo;
                    TUTORSTUDENT[] vrStu;
                    if (m_Request.Account.TutorStudentGet(vrStuGet, out vrStu) == REQUESTCODE.EXECUTE_SUCCESS && vrStu != null)
                    {
                        for (int i = 0; i < vrStu.Length; i++)
                        {
                            if (vrStu[i].dwAccNo == curAcc.dwAccNo && vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)
                            {
                                vrGet.dwMemberID = curAcc.dwAccNo;
                                myTutor = "&nbsp&nbsp&nbsp导师：" + vrTutor[0].szTrueName;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    myTutor = "&nbsp&nbsp&nbsp项目实验需指定导师并获批准，请到[<a href='UserCenter.aspx?tab=4'>个人信息</a>]页面查看状态。";
                    return null;//没有导师或导师未审核通过
                }
            }
        }
        RESEARCHTEST[] vtResult;
        m_Request.Reserve.GetResearchTest(vrGet, out vtResult);
        return vtResult;
    }
    private void GetCkType()
    {
        string kind = Request["ck_kind"];
        string main = Request["ck_main"];
        CHECKTYPE[] rlt = GetCheckType(ToUInt(kind), ToUInt(main));
        SucRlt(rlt);
    }
    string toDate(uint date)
    {
        uint y = date / 10000;
        uint m = (date % 10000) / 100;
        uint d = date % 100;
        return y + "-" + m.ToString("00") + "-" + d.ToString("00") + " ";
    }
    //struct unirule //未使用
    //{
    //    public bool allowLong;
    //    public string rule;
    //    public uint? limit;
    //    public uint? earliest;
    //    public uint? latest;
    //    public uint? max;
    //    public uint? min;
    //    public string[] open;
    //}
    struct uniresv
    {
        public string id;
        public int group;
        public string name;
        public string title;
        public string detail;
        public string owner;
        public string ownerAccno;
        public string dept;
        public string start;
        public string end;
        public string starts;
        public string ends;
        public string timeDesc;
        public string occur;
        public int ltch;
        public uint? status;
        public string state;
        public string states;
        public string prop;
        public bool allDay;
        public bool islong;
        public string szmemo;//20171110章毅添加为了获取审核不通过理由
        //详细
        public string devId;
        public string devName;
        public string kindId;
        public string kindName;
        public string groupId;
        public string groupName;
        public string members;
        public string roomId;
        public string roomName;
        public string labId;
        public string labName;
        public string campus;
        public string devDept;
        public string org;
        public string orger;
        public string contact;
        public string phone;
        public int minUser;
        public int maxUser;
        //活动
        public string atyId;
        public string atyName;
        //课程
        public string testId;
        public string testName;
        public string planId;
        public string planName;
        public string teacher;
        public string teacherAccno;
        //操作
        public int actSN;
    }
    struct retresv
    {
        public string ids;//多个用逗号隔开
        public string resvId;
        public uint? groupId;
        public string groupName;
        public int state;//1 成功 2 失败
        public string time;
        public uint? tchTime;
        public uint? diff;
        public string ltch;
        public string start;
        public string end;
        public string memo;
    }
    struct seat
    {
        public uint? devId;
        public uint? devSN;
        public string devName;
        public uint? atyId;
        public uint? status;
        public uint? accno;
        public string userName;
    }
}