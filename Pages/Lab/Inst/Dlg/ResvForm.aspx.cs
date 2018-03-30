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
    protected void Page_Load(object sender, EventArgs e)
    {

        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
		string testplanid = Request["testplanid"];
		if(testplanid!=null&&testplanid!="")
		{
			PutMemberValue("dwTestPlanIDTemp",testplanid);
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
            for (int i = 1; i <= classTimeTable.Length; i++)
            {
                szSec += GetInputItemHtml(CONSTHTML.option, "", szSecsList[i], i.ToString());
            }
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
            UNIROOM getRoom;
            if (!GetRoomID(uRoomID.ToString(), out getRoom))
            {
                return;
            }
            ArrayList resvDevList = new ArrayList();
            UNIDEVICE[] devList = GetDevByRoomId(uRoomID);
            if (devList == null || devList.Length == 0)
            {
                MessageBox(ConfigConst.GCRoomName+"下没有"+ConfigConst.GCDevName, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            int devCount = 0;
           
            for (int k = 0; k < devList.Length; k++)
            {
                uint uDevKind = (uint)devList[k].dwKindID;
                bool bIsNew = true;
                int uLocal = -1;
                RESVDEV resvDev = new RESVDEV();
                RESVDEV resvDevTemp = new RESVDEV();
                for (int m = 0; m < resvDevList.Count; m++)
                {
                    RESVDEV resvDevTempIn = (RESVDEV)resvDevList[m];
                    if (uDevKind == ((uint)resvDevTempIn.dwDevKind))
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
                    setResv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
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
                    uint uTeachingTime = i * 100000 + (Parse(szWeekList[m])-1) * 10000 + uResvSecBegin * 100 + uResvSecEnd;
                    setResv.dwTeachingTime = uTeachingTime;
                    setResv.ResvDev = resvDevRes;
                    REQUESTCODE uResponse = m_Request.Reserve.Set(setResv, out setResv);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Session["selectDate"] = (uint)setResv.dwPreDate - 100;
                        uSuccessCount = uSuccessCount + 1;
                    }
                    else {
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
                if (uFailCount > 0 && uSuccessCount==0)
                {
                    szMessageInfo = "预约失败" + uFailCount.ToString() + "条"+"，"+m_Request.szErrMessage;
                    MessageBox(szMessageInfo, "提示", MSGBOX.INFO, MSGBOX_ACTION.NONE);
                }
                
            }
           
        }

    }
}
