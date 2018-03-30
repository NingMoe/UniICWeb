using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Attendace : UniClientPage
{
    string planId;
    string testId;
    string resvId;
    UNIACCOUNT acc;
    protected string attend_list = "";
    protected string planLinkList = "";
    protected string testLinkList = "";
    protected string curLink = "0,0,0";
    UNITESTITEM[] tests;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ClientRedirect("Login.aspx"))
        {
            planId = Request["plan_id"];
            testId = Request["test_id"];
            resvId = Request["resv_id"];
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (!string.IsNullOrEmpty(planId) && !string.IsNullOrEmpty(testId) && acc.dwAccNo != null)
            {
                InitAllTest();
                InitResvRec();
                InitLink();
            }
            else
                MsgBox("未指定实验项目", "location.href='Default.aspx'");
        }
    }

    private void InitResvRec()
    {
        string start = Request["start"];
        string end = Request["end"];
        RESVRECREQ req = new RESVRECREQ();
        req.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYID;
        req.dwStartDate = ToUInt(start);
        req.dwEndDate = ToUInt(end);
        req.szGetKey = resvId;
        UNIRESVREC[] rlt;
        if (m_Request.Report.ResvRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIRESVREC rec=rlt[i];
                attend_list += "<tr><td>"+(i+1)+"</td><td>"+rec.szTrueName+"</td><td>"+rec.szPID+"</td><td>"+rec.szDevName+"</td><td>"+Util.Converter.GetAttendState(rec.dwStatus)+"</td></tr>";
            }
        }
    }
    private void InitLink()
    {
            UNITESTPLAN plan = GetTestPlanByID(planId);
            if (plan.dwTestPlanID == null) return;
            curLink = planId + "," + testId + "," + resvId;
            TESTPLANREQ req = new TESTPLANREQ();
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
            req.szGetKey = acc.dwAccNo.ToString();
            req.dwYearTerm = plan.dwYearTerm;
            UNITESTPLAN[] rlt;
            if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                planLinkList = "{";
                testLinkList = "{";
                for (int i = 0; i < rlt.Length; i++)
                {
                    UNITESTPLAN tp = rlt[i];
                    planLinkList += (i == 0 ? "'" : ",'") + tp.dwTestPlanID + "':'" + tp.szTestPlanName + "'";
                    testLinkList += (i == 0 ? "'" : ",'") + tp.dwTestPlanID + "':" + getTestLink(tp.dwTestPlanID) + "";
                }
                planLinkList += "}";
                testLinkList += "}";
            }
            else
                MsgBox(m_Request.szErrMsg);
    }
    private string getTestLink(uint? planId)
    {
        string ret = "[";
        if (tests != null)
        {
            for (int i = 0; i < tests.Length; i++)
            {
                if (tests[i].dwTestPlanID == planId)
                {
                    ret += "{'id':" + tests[i].dwTestItemID + ",'name':'" + tests[i].szTestName + "'},";
                }
            }
        }
        if (ret.Length > 1) ret = ret.Substring(0, ret.Length - 1);
        return ret + "]";
    }
    private void InitAllTest()
    {
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYALL;
        req.dwTeacherID = acc.dwAccNo;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            tests = rlt;
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
}