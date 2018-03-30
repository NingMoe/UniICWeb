using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Collections;
using Newtonsoft;
public partial class searchCourse :UniPage
{
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


    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;
        string szTermID=Request["termID"];
        uint uTermID = Parse(szTermID);
          
        Response.CacheControl = "no-cache";
        UNITERM termNow;

        if(uTermID==0)
        {
            if (GetTermNow(out termNow))
            {
                uTermID = (uint)termNow.dwYearTerm;
            }
            else {
                return;
            }
        }
        TESTPLANREQ planReq=new TESTPLANREQ();
        planReq.dwYearTerm=uTermID;
        UNITESTPLAN[] vtTestPlan;
        List<uniresv> list = new List<uniresv>();
        if (m_Request.Reserve.GetTestPlan(planReq, out vtTestPlan) == REQUESTCODE.EXECUTE_SUCCESS && vtTestPlan != null && vtTestPlan.Length > 0)
        {

            for (int i = 0; i < vtTestPlan.Length; i++)
            {
                TESTITEMREQ vrGetCls = new TESTITEMREQ();

                vrGetCls.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
                vrGetCls.szGetKey = vtTestPlan[i].dwTestPlanID.ToString();
                UNITESTITEM[] vtCls;
                if (m_Request.Reserve.GetTestItem(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null && vtCls.Length > 0)
                {
                    for (int j = 0; j < vtCls.Length; j++)
                    {
                        ConvertTestResvInfo(vtCls[j], ref list);
                    }
                }
            }
        }
        
       string szRes= Newtonsoft.Json.JsonConvert.SerializeObject(list);
       Response.Write(szRes);
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
            resv.title = test.szGroupName+";"+rooms;
            // resv.detail = "房间：" + rooms + "<br/>班级：" + test.szGroupName + "<br/>实验：" + test.szTestName + "<br/>课程：" + test.szCourseName;
            resv.detail = info.dwResvID.ToString(); 
            resv.testId = test.dwTestItemID.ToString();
            resv.testName = test.szTestName;
            resv.owner = test.szTeacherName;
            resv.ownerAccno = test.dwTeacherID.ToString();
            resv.groupId = test.dwGroupID.ToString();
           // resv.groupName = test.szGroupName;
           resv.groupName= info.dwResvID.ToString();
            resv.planId = test.dwTestPlanID.ToString();
            resv.planName = test.szTestPlanName;
            actResv(info, ref resv);
            list.Add(resv);
        }
    }
    public string GetRoomsFromResvDev(RESVDEV[] rsvdev)
    {
        string rooms = "";
        for (int i = 0; i < rsvdev.Length; i++)
        {
            if (i > 0 && rsvdev[i - 1].dwRoomID == rsvdev[i].dwRoomID) continue;
            if (rooms != "") rooms += ",";
            rooms += rsvdev[i].szRoomName;
        }
        return rooms;
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

}