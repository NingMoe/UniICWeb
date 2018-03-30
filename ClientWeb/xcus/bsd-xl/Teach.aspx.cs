using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_Teach : UniClientPage
{
    protected string testList = "";
    protected string testDetail = "";
    protected string planDetail="";
    protected string testId;
    protected UNITESTITEM curTest;
    protected UNIACCOUNT curAcc;
    protected UNITERM curTerm;

    protected string LabList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_TEACHINGLAB))
        {
            Response.Redirect("Default.aspx");
        }
        testId=Request["testId"];
        curAcc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
        InitTerm();
        GetTestList();
        LabList = GetLabHtm("opt");
    }

    private void GetTestList()
    {
        TESTPLANREQ req = new TESTPLANREQ();
        req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
        req.dwYearTerm = curTerm.dwYearTerm;
        req.szGetKey = curAcc.dwAccNo.ToString();
        req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                int testhour = 0;
                UNITESTPLAN plan = rlt[i];
                string its = GetTestItem(plan, out testhour);
                if (its == "") its = "<li><a class='text-center'>没有实验项目</a></li>";
                testList+= "<div class='accordion'><h3>" + plan.szTestPlanName + "[" + plan.dwTestHour + "/" + testhour + "]</h3>"
                   + "<div class='rt_tab'><div class='pro_list'><ul class='menu'>" + its + "</ul></div></div></div>";
            }
            //
            if (string.IsNullOrEmpty(testId))
            {
                GetTestPlanDetail(rlt);
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
    private string GetTestItem(UNITESTPLAN plan, out int testhour)
    {
        string ret = "";
        testhour = 0;
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        req.szGetKey = plan.dwTestPlanID.ToString();
        req.dwCourseID = plan.dwCourseID;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNITESTITEM it = rlt[i];
                testhour += (int)it.dwTestHour;
                ret += "<li><a href='?testId=" + it.dwTestItemID + "' test='"+it.dwTestItemID+"'><span class='ui-icon ui-icon-calculator'></span>" + it.szTestName + "["+it.dwTestHour+"/"+it.dwResvTestHour+"]</a></li>";
                //实验项目
                if (testId!=null&&it.dwTestItemID == ToUInt(testId))
                {
                    curTest = it;
                    GetTestDetail(it);
                }
            }
        }
        return ret;
    }
    private void InitTerm()
    {
        UNITERM[] rlt = InitTermList(out curTerm, Request["term"]);
    }
    private void GetTestDetail(UNITESTITEM test)
    {
        UNIRESERVE[] resvs = test.ResvInfo;
        int usehour = 0;
        for (int i = 0; i < resvs.Length; i++)
        {
            UNIRESERVE resv = resvs[i];
            uint? tchl = resv.dwTeachingTime;
            int start = (int)(tchl % 10000) / 100;
            int end = (int)tchl % 100;
            usehour +=(int)resv.dwTestHour;
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
            string time = Get1970Date((int)resv.dwBeginTime).Substring(5)+"-"+Get1970Date((int)resv.dwEndTime).Substring(11);
            testDetail += "<tr><td>" + resv.szTestName + "</td><td>" + time + "</td>" +
                                    "<td>" + rooms + "</td>" +
                                    "<td>" + resv.dwResvUsers + " 人 <span class='click' onclick='uni.msgBox(\""+member+"\",\"成员名称\")'>详细</span></td>" +
                                    "<td><span>" + resv.dwTestHour + "</span> 学时</td>" +
                                    "<td>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>" +
                                    "<td><a onclick='delResv(" + resv.dwResvID + ")'>删除</a></td></tr>";
        }
        if (testDetail == "") testDetail = "<tr><td colspan='10' style='text-align:center;'>请点击【预约实验室】预约上课</td></tr>";
    }
    private void GetTestPlanDetail(UNITESTPLAN[] plans)
    {
        for (int i = 0; i < plans.Length; i++)
        {
            UNITESTPLAN plan = plans[i];
            planDetail += "<tr><td>" + plan.dwCourseID + "</td>" +
                "<td>" + plan.szTestPlanName + "</td>" +
                                    "<td><span>" + plan.dwTestHour + "</span> 学时</td>" +
                                    "<td><span>" + plan.dwResvTestHour + "</span> 学时</td>" +
                                    "<td><a onclick='openGroup(" + plan.dwGroupID + ")'>"+plan.szGroupName+"</a></td></tr>";
        }
    }
}