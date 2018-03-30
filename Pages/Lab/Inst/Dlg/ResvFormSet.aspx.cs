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
    protected string m_Title = "修改预约";
    protected string szWeeks = "";//周次
    protected string szWeek = "";//星期表单输出
    protected uint uWeek = 0;//默认星期几
    protected string szSec = "";//节次
    protected string szRoomInfo = "";//房间信息
    protected int uWeeStart = 0;
    protected uint szResvSec = 0;
    protected string szResvWeeks = "-1";//选中的预约的星期
    protected string szRoomList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIROOM[] roomList = GetAllRoom();
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                szRoomList += GetInputItemHtml(CONSTHTML.option, "resvRoom", roomList[i].szRoomName, roomList[i].dwRoomID.ToString());
            }
        }
        CLASSTIMETABLE[] classTimeTable = GetTermClasTimeTable();
        for (int i = 1; i <= classTimeTable.Length; i++)
        {
            szSec += GetInputItemHtml(CONSTHTML.option, "", szSecsList[i], i.ToString());
        }
        for (int i = 0; i < szWeekDayList.Length; i++)
        {
            szWeek += GetInputItemHtml(CONSTHTML.option, "", szWeekDayList[i], (i).ToString());
        }
        int uWeekTotal = GetWeekTotalNow();
        for (int i = 1; i <= uWeekTotal; i++)
        {
            szWeeks += GetInputItemHtml(CONSTHTML.option, "", szWeeksList[i], (i).ToString());
        }

        if (IsPostBack)
        {
            UNIRESERVE setVaue = new UNIRESERVE();
            string szResvID=Request["dwResvID"];
            if (GetResvByID(szResvID, out setVaue))
            {

                uint uResvWeekStart = Parse(Request["dwBeginWeeksSec"]);
                uint uResvWeekEnd = Parse(Request["dwEndWeeksSec"]);
                szResvWeeks = Request["szWeek"];//星期
                uint uResvSecBegin = Parse(Request["dwBeginSec"]);
                uint uResvSecEnd = Parse(Request["dwEndSec"]);

                uint uWeeks = Parse(Request["dwWeeks"]);
                uint uWeek = Parse(Request["szWeek"]);
                uint uBeginSec = Parse(Request["dwBeginSec"]);
                uint uEndSec = Parse(Request["dwEndSec"]);
                uint uTeachingTime = uWeeks * 100000 + (uWeek) * 10000 + uBeginSec * 100 + uEndSec;

                uint uRoomID = Parse(Request["dwRoom"]);

                UNIROOM getRoom;
                if (!GetRoomID(uRoomID.ToString(), out getRoom))
                {
                    return;
                }
                ArrayList resvDevList = new ArrayList();
                UNIDEVICE[] devList = GetDevByRoomId(uRoomID);
                if (devList == null || devList.Length == 0)
                {
                    MessageBox(ConfigConst.GCRoomName + "下没有" + ConfigConst.GCDevName, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
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

                setVaue.dwTeachingTime = uTeachingTime;
                setVaue.ResvDev = resvDevRes;
                setVaue.dwBeginTime = null;
                setVaue.dwEndTime = null;
                REQUESTCODE uResponse = m_Request.Reserve.Set(setVaue, out setVaue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改成功", "", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    return;
                }
            }
            else {

                MessageBox("获取不到预约信息", "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                return;
            }


        }
        if (Request["op"] == "set")
        {
             UNIRESERVE setValue = new UNIRESERVE();
            if(GetResvByID(Request["id"],out setValue)!=true)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                uint uTeachingTime = (uint)setValue.dwTeachingTime;//05030102
                uint uResvWeeks = 0;
                uint uResvWeek = 0;
                uint uResvBeginSec = 0;
                uint uResvEndSec = 0;
                GetTeachingTimeDetail(uTeachingTime, out uResvWeeks, out uResvWeek, out uResvBeginSec, out uResvEndSec);
                uint uTeachAccno = (uint)setValue.dwOwner;
                string szTeachName = setValue.szOwnerName;
                string testPlanName = setValue.szTestName;
                PutMemberValue("dwWeeks", uResvWeeks.ToString());
                UNIROOM[] roomListTemp = GetRoomByNO(setValue.ResvDev[0].szRoomNo, setValue.dwLabID);
                if (roomListTemp != null && roomListTemp.Length > 0)
                {
                    PutMemberValue("dwRoom", roomListTemp[0].dwRoomID.ToString());
                }
                PutMemberValue("szWeekHiden", (uResvWeek).ToString());
                PutMemberValue("dwBeginSecHiden", uResvBeginSec.ToString());
                PutMemberValue("dwEndSecHiden", uResvEndSec.ToString());
                PutMemberValue("szTeacherName", szTeachName.ToString());
                PutMemberValue("szTestPlanName", testPlanName.ToString());
                PutMemberValue("dwResvID", setValue.dwResvID.ToString());
            }
        }
        else
        {
            m_Title = "修改预约";

        }
    }

}
