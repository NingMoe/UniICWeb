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
using UniWebLib;

public partial class _Default : UniPage
{
    protected string m_Title="新建预约";
    protected string szWeeks = "";//周次
    protected string szWeek = "";//星期表单输出
    protected uint uWeek = 0;//默认星期几
    protected string szSec = "";//节次
    protected string szRoomInfo = "";//房间信息
    protected int uWeeStart = 0;
    protected uint szResvSec = 0;
    protected string szResvWeeks = "-1";//选中的预约的星期
    protected string m_TermList = "";
    protected string szRoomList = "";
    protected string szResvTime = "";
    protected uint uCouserProp = 0;
    // 
    /*1）课程属性为‘理论课中的实验’类型的一次仅能预约两节课（如1-2节，3-4节）
              2）课程属性为‘理论课中的实验’类型的并且上课节次固定值为1的一次能预约4节次（如1-4节，5-8节）
              3）课程属性为‘独立开设的实验课’类型的为固定时间段预约，如（8：00-11：00，11:15-14:15,14:30-17:30）(该时间可调整)
     * */
    protected void Page_Load(object sender, EventArgs e)
    {

        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
        string testplanid = Request["testplanid"];
        if (testplanid != null && testplanid != "")
        {
            PutMemberValue("dwTestPlanIDTemp", testplanid);
        }
       
        uint uYeartermNow = Parse(szYearTerm);
        if (termList != null)
        {
            for (int i = 0; i < termList.Length; i++)
            {

                uint uYearTermState = (uint)termList[i].dwStatus;
                if ((uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString(), true);
                }
                else
                {
                    m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                }
            }
        }
        uint uRoomID = Parse(Request["devID"]);
        uint szResvDate = Parse(Request["date"]);
        szResvDate = szResvDate + 100;
        szResvSec = Parse(Request["sec"]);
        szResvSec = szResvSec + 1;//节次需要加1
        string szWeekStr = szResvDate / 10000 + "-" + (szResvDate % 10000 / 100) + "-" + szResvDate % 100;
        uWeeStart = GetWeekFromDate(szWeekStr);

        uWeek = ((uint)DateTime.Parse(szWeekStr).DayOfWeek);
        if (uWeek == 0)
        {
            uWeek = 7;//星期天特殊处理 
        }
        //设置默认值
        {
            FULLROOMREQ vrGet = new FULLROOMREQ();
            vrGet.dwRoomID = uRoomID;
            FULLROOM[] room;
            if (m_Request.Device.FullRoomGet(vrGet, out room) == REQUESTCODE.EXECUTE_SUCCESS && room.Length > 0)
            {
                szRoomInfo = room[0].szRoomName.ToString() + "(" + room[0].dwIdleDevNum + "台)";
            }
            CLASSTIMETABLE[] classTimeTable = GetTermClasTimeTable();
            /*
            for (int i = 1; i <= classTimeTable.Length; i++)
            {
                szSec += GetInputItemHtml(CONSTHTML.option, "", szSecsList[i], i.ToString());
            }
             */
            for (int i = 0; i < szWeekDayList.Length; i++)
            {
                szWeek += GetInputItemHtml(CONSTHTML.checkBox, "szWeek", szWeekDayList[i], (i + 1).ToString());
            }

            int uWeekTotal = GetWeekTotalNow();
            for (int i = 1; i <= uWeekTotal; i++)
            {
                szWeeks += GetInputItemHtml(CONSTHTML.option, "", szWeeksList[i], (i).ToString());
            }
        }
        UNIROOM[] roomList = GetAllRoom();
        for (int k = 0; roomList != null && k < roomList.Length; k++)
        {
            szRoomList += GetInputItemHtml(CONSTHTML.radioButton, "roomID", roomList[k].szRoomName.ToString(), roomList[k].dwRoomID.ToString());
        }
        ROOMGROUPREQ vrGetRoomGroup = new ROOMGROUPREQ();
        ROOMGROUP[] roomGroupList;
        if (m_Request.Device.RoomGroupGet(vrGetRoomGroup, out roomGroupList) == REQUESTCODE.EXECUTE_SUCCESS && roomGroupList != null && roomGroupList.Length > 0)
        {
            for (int k = 0; k < roomGroupList.Length; k++)
            {
                ROOMGROUP groupTemp = roomGroupList[k];
                RGMEMBER[] rgMember = groupTemp.rgMember;
                string szRoomIDTemp = "";
                for (int m = 0; m < rgMember.Length; m++)
                {
                    szRoomIDTemp += rgMember[m].dwRoomID + ",";
                }
                szRoomList += GetInputItemHtml(CONSTHTML.radioButton, "roomID", roomGroupList[k].szRGName.ToString(), szRoomIDTemp.ToString());
            }
        }
        if (!IsPostBack)
        {
            PutMemberValue("roomID", Request["devID"]);
        }
        if (IsPostBack)
        {
            uint uResvWeekStart = Parse(Request["dwBeginWeeksSec"]);
            uint uResvWeekEnd = Parse(Request["dwEndWeeksSec"]);
            szResvWeeks = Request["szWeek"];//星期
            uint uResvSecBegin = Parse(Request["dwBeginSec"]);
            uint uResvSecEnd = Parse(Request["dwEndSec"]);
            uint uTestItemID = Parse(Request["dwTestItemID"]);
            uint uTeacher = Parse(Request["dwTeacherID"]);
            string szTeacherName = "";
            UNITERM[] termNow = GetTermNow();
            if (termNow == null || termNow.Length == 0)
            {
                MessageBox("获取学期失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);

                return;
            }
            UNIACCOUNT accTeacher;
            if (GetAccByAccno(uTeacher.ToString(), out accTeacher))
            {
                szTeacherName = accTeacher.szTrueName;
            }
            if (szResvWeeks == null || szResvWeeks == "")//星期
            {
                MessageBox("请设置设置好上课星期", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            if (uTeacher == 0)
            {
                MessageBox("请设置好实验项目", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);

                return;
            }
            if (uTestItemID == 0)
            {
                MessageBox("请设置好实验项目", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            UNITESTITEM[] testItem = GetTestItemByID(uTestItemID);
            if (testItem == null && testItem.Length == 0)
            {
                return;
            }
            //确定课程性质
            {
                UNICOURSE couse;
                if (GetCourseByID(testItem[0].dwCourseID, out couse))
                {
                    uint uProp = (uint)couse.dwCourseProperty;
                    if (((uProp & (uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_WITHTHEORY)) > 0)//理论课
                    {
                        if (couse.szMemo != null && couse.szMemo == "1")
                        {
                            uCouserProp = 2;
                        }
                        else
                        {
                            uCouserProp = 1;
                        }
                    }
                    else if (((uProp & (uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_NOTHEORY)) > 0)//实践课
                    {
                        uCouserProp = 3;
                    }
                }

            }

            UNIROOM getRoom;
            if (!GetRoomID(uRoomID.ToString(), out getRoom))
            {
                return;
            }
            ArrayList resvDevList = new ArrayList();
            string szRoomID = Request["roomID"];
            string[] szRoomIDList = szRoomID.Split(',');
            for (int w = 0; w < szRoomIDList.Length; w++)
            {
                uint uRoomTemp = Parse(szRoomIDList[w]);
                if (uRoomTemp == 0)
                {
                    continue;
                }
                UNIDEVICE[] devList = GetDevByRoomId(uRoomTemp);
                if (devList == null || devList.Length == 0)
                {
                    MessageBox(ConfigConst.GCRoomName + "下没有" + ConfigConst.GCDevName, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                int devCount = 0;

                for (int k = 0; k < devList.Length; k++)
                {
                    uint uDevKind = (uint)devList[k].dwKindID;
                    uint uRoomIDTempList = (uint)devList[k].dwRoomID;
                    bool bIsNew = true;
                    int uLocal = -1;
                    RESVDEV resvDev = new RESVDEV();
                    RESVDEV resvDevTemp = new RESVDEV();
                    for (int m = 0; m < resvDevList.Count; m++)
                    {
                        RESVDEV resvDevTempIn = (RESVDEV)resvDevList[m];
                        if (uDevKind == ((uint)resvDevTempIn.dwDevKind) && uRoomIDTempList == (uint)resvDevTempIn.dwRoomID)
                        {
                            bIsNew = false;
                            uLocal = m;
                            resvDevTemp = resvDevTempIn;
                        }
                    }
                    if (bIsNew)
                    {
                        resvDev.dwDevStart = devList[k].dwDevSN;
                        resvDev.dwDevEnd = devList[k].dwDevSN;
                        resvDev.dwDevKind = devList[k].dwKindID;
                        resvDev.szRoomNo = devList[k].szRoomNo;
                        resvDev.dwRoomID = devList[k].dwRoomID;
                        resvDev.dwDevNum = 1;
                        resvDevList.Add(resvDev);
                    }
                    else
                    {
                        uint uDevNum = (uint)resvDevTemp.dwDevNum + 1;
                        uint uDevSNTemp = (uint)devList[k].dwDevSN;
                        if (uDevSNTemp < ((uint)resvDevTemp.dwDevStart))
                        {
                            resvDevTemp.dwDevStart = uDevSNTemp;
                        }
                        if (uDevSNTemp > ((uint)resvDevTemp.dwDevEnd))
                        {
                            resvDevTemp.dwDevEnd = uDevSNTemp;
                        }
                        resvDevTemp.dwDevNum = uDevNum;
                        resvDevList[uLocal] = resvDevTemp;
                    }
                }
            }
            RESVDEV[] resvDevRes = new RESVDEV[resvDevList.Count];
            for (int m = 0; m < resvDevRes.Length; m++)
            {
                resvDevRes[m] = new RESVDEV();
                resvDevRes[m] = (RESVDEV)resvDevList[m];
            }
            uint uSuccessCount = 0;
            uint uFailCount = 0;
            for (uint i = uResvWeekStart; i <= uResvWeekEnd; i++)
            {
                string[] szWeekList = szResvWeeks.Split(',');
                for (int m = 0; m < szWeekList.Length; m++)
                {
                    UNIRESERVE setResv = new UNIRESERVE();
                    setResv.dwLabID = getRoom.dwLabID;
                    setResv.szLabName = getRoom.szLabName;
                    if (uCouserProp == 1 || uCouserProp==2)
                    {
                        setResv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING + (uint)UNIRESERVE.DWPURPOSE.USEFOR_WITHTHEORY;
                    }
                    else if (uCouserProp ==3)
                    {
                        setResv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING + (uint)UNIRESERVE.DWPURPOSE.USEFOR_NOTHEORY;
                    }
                    setResv.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
                    setResv.dwOwner = uTeacher;
                    setResv.szOwnerName = szTeacherName;
                    setResv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
                    setResv.dwMemberID = testItem[0].dwGroupID;
                    setResv.szMemberName = testItem[0].szGroupName;
                    setResv.szTestName = testItem[0].szTestName;
                    setResv.dwTestItemID = testItem[0].dwTestItemID;
                    setResv.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
                    setResv.dwYearTerm = termNow[0].dwYearTerm;
                    if (uCouserProp >= 1 && uCouserProp <= 2)
                    {
                        string szResvTime = Request["dwResvTime"];
                        uint uTeachingTime = i * 100000 + (Parse(szWeekList[m]) - 1) * 10000 + Parse(szResvTime);// uResvSecBegin * 100 + uResvSecEnd;
                        setResv.dwTeachingTime = uTeachingTime;
                    }
                    else {
                        int nResvTimeDate=(GetDateFromWeek((uint)setResv.dwYearTerm, i, Parse(szWeekList[m]) - 1));
                        string szResvTimeDate = nResvTimeDate / 10000 + "-" + (nResvTimeDate % 10000) / 100 + "-" + nResvTimeDate % 100;
                        uint BeginTime = Parse(Request["dwResvTime"])/10000;
                        uint EndTime = Parse(Request["dwResvTime"]) % 10000;
                        string szResvTimeBegin = szResvTimeDate + " " + BeginTime / 100 + ":" + BeginTime % 100;
                        string szResvTimeEnd = szResvTimeDate + " " + EndTime / 100 + ":" + EndTime % 100;
                        uint uResvBegin = Get1970Seconds(szResvTimeBegin);
                        uint uResvEnd = Get1970Seconds(szResvTimeEnd);
                        setResv.dwBeginTime = uResvBegin;
                        setResv.dwEndTime = uResvEnd;
                    }
                    setResv.ResvDev = resvDevRes;
                    REQUESTCODE uResponse = m_Request.Reserve.Set(setResv, out setResv);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Session["selectDate"] = (uint)setResv.dwPreDate - 100;
                        uSuccessCount = uSuccessCount + 1;
                    }
                    else
                    {
                        uFailCount = uFailCount + 1;
                    }
                }
                string szMessageInfo = "";
                if (uSuccessCount > 0)
                {
                    szMessageInfo = "预约成功" + uSuccessCount.ToString() + "条";
                    if (uFailCount > 0)
                    {
                        szMessageInfo += "，失败" + uFailCount.ToString() + "条";
                    }
                    MessageBox(szMessageInfo, "提示", MSGBOX.INFO, MSGBOX_ACTION.OK);
                }
                if (uFailCount > 0 && uSuccessCount == 0)
                {
                    szMessageInfo = "预约失败" + uFailCount.ToString() + "条" + "，" + m_Request.szErrMessage;
                    MessageBox(szMessageInfo, "提示", MSGBOX.INFO, MSGBOX_ACTION.NONE);
                }

            }

        }

    }
}
