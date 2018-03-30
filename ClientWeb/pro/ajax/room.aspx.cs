using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_pro_ajax_room : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "get_rsv_sta")
            {
                GetRoomResvState();
            }
            else if (act == "get_rm_sta")
            {
                GetRoomState();
            }
            else if (act == "get_rm_group")
            {
                GetRoomGroup();
            }
            else if (act == "get_rm_group_sta")
            {
                GetRoomGroupState();
            }
        }
    }

    private void GetRoomGroupState()
    {
        string devNum = Request["dev_num"];
        string labid = Request["lab_id"];
        string classId = Request["class_id"];
        string kindId = Request["kind_id"];
        string purpose = Request["purpose"];
        string dt = Request["date"];
        uint date;
        List<roomResvSta> list = new List<roomResvSta>();
        if (string.IsNullOrEmpty(dt) || !uint.TryParse(dt, out date))
        {
            ErrMsg("日期错误");
            return;
        }
        string date_pre = UintToDateStr(date);
        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        RGRESVSTATREQ req = new RGRESVSTATREQ();
        req.dwDate = date;
        if (!string.IsNullOrEmpty(devNum) && devNum != "0")
            req.dwMinDevNum = ToUInt(devNum);
        RGRESVSTAT[] rlt;
        cd = m_Request.Device.GetRGResvStat(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                roomResvSta sta = new roomResvSta();
                sta.id = rlt[i].dwRGID.ToString();
                sta.name = rlt[i].szRGName + "(设备数:" + rlt[i].dwDevNum + ")";
                sta.title = rlt[i].szRGName;
                sta.roomName = rlt[i].szRGName;
                sta.devNum = rlt[i].dwDevNum;
                //教学预约
                TEACHINGRESVINFO[] tchs = rlt[i].szResvInfo;
                List<rsvInfo> rsv = new List<rsvInfo>();
                for (int j = 0; j < tchs.Length; j++)
                {
                    rsvInfo info = new rsvInfo();
                    info.testId = tchs[j].dwTestItemID;
                    info.testCard = tchs[j].dwTestCardID;
                    info.testName = tchs[j].szTestName;
                    info.planId = tchs[j].dwTestPlanID;
                    info.planName = tchs[j].szTestPlanName;
                    info.teacherId = tchs[j].dwTeacherID.ToString();
                    info.teacher = tchs[j].szTeacherName;
                    info.courseId = tchs[j].dwCourseID;
                    info.courseName = tchs[j].szCourseName;
                    info.groupId = tchs[j].dwGroupID;
                    info.groupName = tchs[j].szGroupName;
                    info.teachTime = tchs[j].dwTeachingTime;
                    info.state = tchs[j].dwResvStat;
                    rsv.Add(info);
                }
                sta.rsvs = rsv.ToArray();
                list.Add(sta);
            }
            SucRlt(list);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetRoomState()
    {
        uint classkind = ToUInt(Request["classkind"]);
        string start = Request["start"];
        string end = Request["end"];
        string date = Request["date"];
        string szRoomIDs = Request["room_id"];
        ROOMFORRESVREQ req = new ROOMFORRESVREQ();
        if (classkind != 0)
            req.dwClassKind = classkind;
        DateTime dtStart = DateTime.Parse(date + " " + start);
        if (dtStart < DateTime.Now)
        {
            start = DateTime.Now.ToString("HH:mm");
        }
        if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
        {
            req.dwBeginTime = (uint)Get1970Seconds(date + " " + start);
            req.dwEndTime = (uint)Get1970Seconds(date + " " + end);
        }
        req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        req.dwDate = ToUInt(date.Replace("-", ""));
        if (!string.IsNullOrEmpty(szRoomIDs))
        {
           // req.szRoomIDs = szRoomIDs;
        }
        //req.szReqExtInfo.szOrderKey = "szRoomName";
        //req.szReqExtInfo.szOrderMode = "ASC";
        ROOMFORRESV[] rlt;
        if (m_Request.Device.GetRoomForResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint rscMode = ToUInt(GetConfig("subSeatResourceMode"));
            ROOMREQ req2 = new ROOMREQ();
            req2.dwInClassKind = req.dwClassKind;
            if ((rscMode & 32) > 0)//二级显示
            {
                req2.szReqExtInfo.szOrderKey = "dwLabID";
                req2.szReqExtInfo.szOrderMode = "ASC";
            }
            //else
            //{
            //    req2.szReqExtInfo.szOrderKey = "dwRoomID";
            //    req2.szReqExtInfo.szOrderMode = "ASC";
            //}
          
            UNIROOM[] rms;
            if (m_Request.Device.RoomGet(req2, out rms) == REQUESTCODE.EXECUTE_SUCCESS&&rms.Length>0)//20171012修改，为了获取等过开放信息
            {
                //20171012修改，重新配置开放信息
                

                List<roomResvSta> list = new List<roomResvSta>();
                for (int j = 0; j < rms.Length; j++)
                {

                    uint? uStartHM = 2300;
                    uint? uEndHM = 0;
                    bool bSetOpenEnd = false;
                    PERIODOPENRULEREQ openRuleGet = new PERIODOPENRULEREQ();
                    openRuleGet.dwRuleSN = rms[j].dwOpenRuleSN;
                    PERIODOPENRULE[] devResvRes;
                    if (m_Request.Device.PeriodOpenRuleGet(openRuleGet, out devResvRes) == REQUESTCODE.EXECUTE_SUCCESS && devResvRes.Length > 0)
                    {
                        DateTime dtResv = DateTime.Parse(Request["date"]);
                        uint? nSearchDay = (uint?)dtResv.DayOfWeek;
                        if (nSearchDay == 0)
                        {
                            nSearchDay = 7;
                        }
                        else
                        {
                            nSearchDay = nSearchDay - 1;
                        }
                        for (int m = 0; m < devResvRes.Length; m++)
                        {
                            if (devResvRes[m].dwStartDay == nSearchDay)
                            {
                                if (devResvRes[m].DayOpenRule.Length > 1)
                                {
                                    bSetOpenEnd = true;
                                }
                                for (int n = 0; n < devResvRes[m].DayOpenRule.Length; n++)
                                {
                                    if (devResvRes[m].DayOpenRule[n].dwBegin == 0 || devResvRes[m].DayOpenRule[n].dwEnd == 0)
                                    {
                                        continue;
                                    }
                                    if (devResvRes[m].DayOpenRule[n].dwBegin < uStartHM)
                                    {

                                        uStartHM = devResvRes[m].DayOpenRule[n].dwBegin;
                                    }
                                    if (devResvRes[m].DayOpenRule[n].dwEnd > uEndHM)
                                    {

                                        uEndHM = devResvRes[m].DayOpenRule[n].dwEnd;
                                    }
                                }

                            }
                        }
                    }


                    for (int i = 0; i < rlt.Length; i++)
                    {
                        //20171012修改，重新配置开放信息
                        if (bSetOpenEnd)
                        {
                            rlt[i].dwOpenBegin = uStartHM;
                            rlt[i].dwOpenEnd = uEndHM;
                        }
                      
                        if (rms[j].dwRoomID == rlt[i].dwRoomID)
                        {
                            //20170527zy添加，管理端对房间设置不对外开放，手机端还是显示出来问题
                            if ((rms[j].dwProperty & 0x800000) > 0)//临时  0x800000=不开放
                            {
                                continue;
                            }

                            roomResvSta sta = new roomResvSta();
                            sta.roomStat = rlt[i];
                            if (sta.roomStat.dwUsableNum < 0) sta.roomStat.dwUsableNum = 0;
                            UNIROOM rm = rms[j];
                            sta.id = rm.dwRoomID.ToString();
                            sta.name = rm.szRoomName;
                            sta.labId = rm.dwLabID.ToString();
                            sta.labName = rm.szLabName;
                            list.Add(sta);
                            break;
                        }
                    }
                }
                SucRlt(list);
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetRoomGroup()
    {
        string roomId = Request["room_id"];
        string num = Request["num"];
        if (string.IsNullOrEmpty(roomId)) roomId = "0";
        if (string.IsNullOrEmpty(num)) num = "0";
        ROOMGROUP[] rlt = GetRoomGroup(ToUInt(roomId), ToUInt(num));
        if (rlt != null)
        {
            List<rmGroup> list = new List<rmGroup>();
            for (int i = 0; i < rlt.Length; i++)
            {
                rmGroup rmg = new rmGroup();
                ROOMGROUP gp = rlt[i];
                rmg.id = gp.dwRGID;
                rmg.name = gp.szRGName;
                rmg.num = gp.dwRoomNum;
                rmg.ids = "";
                rmg.devNum = 0;
                RGMEMBER[] gmb = gp.rgMember;
                if (gmb != null)
                {
                    rmGroup[] mbs = new rmGroup[gmb.Length];
                    for (int j = 0; j < gmb.Length; j++)
                    {
                        rmGroup mb = new rmGroup();
                        mb.id = gmb[j].dwRoomID;
                        mb.name = gmb[j].szRoomName;
                        mb.no = gmb[j].szRoomNo;
                        mb.num = gmb[j].dwDevNum;
                        rmg.devNum += mb.num;
                        rmg.ids += mb.id + ",";
                    }
                    rmg.mbs = mbs;
                }
                if (rmg.ids != "") rmg.ids = rmg.ids.Substring(0, rmg.ids.Length - 1);
                list.Add(rmg);
            }
            SucRlt(list);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }
    private void GetRoomResvState()
    {
        string roomId = Request["room_id"];
        string roomName = Request["room_name"];
        string devNum = Request["dev_num"];
        string labid = Request["lab_id"];
        string classId = Request["class_id"];
        string kindId = Request["kind_id"];
        string purpose = Request["purpose"];
        string dt = Request["date"];
        uint date;
        List<roomResvSta> list = new List<roomResvSta>();
        dt = dt.Replace("-","");
        if (string.IsNullOrEmpty(dt) || !uint.TryParse(dt, out date))
        {
            ErrMsg("日期错误");
            return;
        }
        string date_pre = UintToDateStr(date);
        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        ROOMRESVSTATREQ req = new ROOMRESVSTATREQ();
        req.dwDate = date;
        if (!string.IsNullOrEmpty(roomId) && roomId != "0")
        {
            req.szRoomIDs = roomId;
        }
        if (!string.IsNullOrEmpty(roomName))
            req.szRoomName = roomName;
        if (!string.IsNullOrEmpty(labid) && labid != "0")
            req.szLabIDs = labid;
        if (!string.IsNullOrEmpty(devNum) && devNum != "0")
            req.dwMinDevNum = ToUInt(devNum);
        req.dwUnNeedRoomProp = (uint)UNIROOM.DWPROPERTY.ROOMPROP_NORESV;//(uint)UNIROOM.DWPROPERTY.ROOMPROP_SUBROOM | (uint)UNIROOM.DWPROPERTY.ROOMPROP_COMBINE;
        ROOMRESVSTAT[] rlt;
        cd = m_Request.Device.GetRoomResvStat(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                roomResvSta sta = new roomResvSta();
                sta.id = rlt[i].dwRoomID.ToString();
                sta.name = rlt[i].szRoomName + "(设备数:" + rlt[i].dwDevNum + ")";
                sta.title = rlt[i].szRoomName;
                sta.roomName = rlt[i].szRoomName;
                sta.devNum = rlt[i].dwDevNum;
                //教学预约
                TEACHINGRESVINFO[] tchs = rlt[i].szResvInfo;
                List<rsvInfo> rsv = new List<rsvInfo>();
                for (int j = 0; j < tchs.Length; j++)
                {
                    rsvInfo info = new rsvInfo();
                    info.testId = tchs[j].dwTestItemID;
                    info.testCard = tchs[j].dwTestCardID;
                    info.testName = tchs[j].szTestName;
                    info.planId = tchs[j].dwTestPlanID;
                    info.planName = tchs[j].szTestPlanName;
                    info.teacherId = tchs[j].dwTeacherID.ToString();
                    info.teacher = tchs[j].szTeacherName;
                    info.courseId = tchs[j].dwCourseID;
                    info.courseName = tchs[j].szCourseName;
                    info.groupId = tchs[j].dwGroupID;
                    info.groupName = tchs[j].szGroupName;
                    info.teachTime = tchs[j].dwTeachingTime;
                    info.state = tchs[j].dwResvStat;
                    rsv.Add(info);
                }
                sta.rsvs = rsv.ToArray();
                list.Add(sta);
            }
            SucRlt(list);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    public struct roomResvSta
    {
        public string id;
        public string title;
        public string name;
        public string roomName;
        public uint? devNum;
        public string kindId;
        public string kindName;
        public string labName;
        public string labId;
        public string state;
        public string prop;
        public string openTimes;
        public rsvInfo[] rsvs;
        public ROOMFORRESV roomStat;
    }
    public struct rsvInfo
    {
        public uint? testId;		/*实验项目ID*/
        public uint? testCard;		/*实验项目卡ID*/
        public string testName;		/*实验名称*/
        public uint? planId;		/*实验计划ID*/
        public string planName;		/*实验计划名称*/
        public string teacherId;		/*教师（帐号）*/
        public string teacher;		/*教师姓名*/
        public uint? courseId;		/*课程ID*/
        public string courseName;		/*课程名称*/
        public uint? groupId;		/*上课班级*/
        public string groupName;		/*上课班级名称*/
        public uint? teachTime;		/*教学时间*/
        public uint? state;		/*预约状态*/
    }
    public struct rmGroup
    {
        public uint? id;
        public string no;
        public string ids;
        public string name;
        public uint? num;
        public uint? devNum;
        public rmGroup[] mbs;
    }
}