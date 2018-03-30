using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Report : UniClientPage
{
    string testId;
    UNITESTPLAN plan;
    UNIACCOUNT acc;
    protected string report_list = "";
    protected string planLinkList = "";
    protected string curLink = "0,0";
    protected string curTest = "";
    UNIRESVREC[] recs;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ClientRedirect("Login.aspx"))
        {
            testId = Request["test_id"];
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (!string.IsNullOrEmpty(testId) && acc.dwAccNo != null)
            {
                InitReport();
                InitLink();
            }
            else
                MsgBox("未指定实验项目", "location.href='Default.aspx'");
        }
    }

    private void InitResvRec()
    {
        if (plan.dwTestPlanID == null) return;
        UNITERM term = GetTerm(plan.dwYearTerm);
        if (term.dwYearTerm == null) return;
        RESVRECREQ req = new RESVRECREQ();
        req.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYTESTITEMID;
        req.szGetKey = testId;
        req.dwStartDate = term.dwBeginDate;
        req.dwEndDate = term.dwEndDate;
        UNIRESVREC[] rlt;
        if (m_Request.Report.ResvRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            recs = rlt;
        }
    }
    private string GetAttend(uint? accno)
    {
        string ret = "";
        if (recs != null)
        {
            int need_num = 0;//应到
            int sign_num = 0;//已到
            int leave_num = 0;//请假
            int unsign_num = 0;//未到
            int late_num = 0;//迟到
            int early_num = 0;//早退
            for (int i = 0; i < recs.Length; i++)
            {
                UNIRESVREC rec = recs[i];
                if (accno == rec.dwAccNo)
                {
                    need_num++;
                    if (IsStat(rec.dwStatus, (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_SIGNED | (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_ATTEND))
                        sign_num++;
                    if (IsStat(rec.dwStatus, (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_UNSIGN | (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_ABSENT))
                        unsign_num++;
                    if (IsStat(rec.dwStatus, (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_SICK) || IsStat(rec.dwStatus, (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_PRIVATE))
                        leave_num++;
                }
            }
            ret = "应到:<code>" + need_num + "</code> 实到:<code>" + sign_num + "</code> 请假:<code>" + leave_num + "</code>";
        }
        return ret;

    }
    private void InitLink()
    {
        if (plan.dwTestPlanID == null) return;
        TESTPLANREQ req = new TESTPLANREQ();
        req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
        req.szGetKey = acc.dwAccNo.ToString();
        req.dwYearTerm = plan.dwYearTerm;
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            planLinkList = "{";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNITESTPLAN tp = rlt[i];
                planLinkList += (i == 0 ? "'" : ",'") + tp.dwTestPlanID + "':'" + tp.szTestPlanName + "'";
            }
            planLinkList += "}";
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    private void InitReport()
    {
        UNITESTITEM test = GetTestItemByID(testId);
        if (test.dwTestItemID == null)
        {
            MsgBox("未找到实验");
            return;
        }
        else
        {
            curLink = test.dwTestPlanID + "," + testId;
            plan = GetTestPlanByID(test.dwTestPlanID.ToString());
            InitResvRec();
            curTest = test.szTestName;
        }
        TESTITEMINFOREQ req = new TESTITEMINFOREQ();
        req.dwTeacherID = acc.dwAccNo;
        req.dwTestItemID = ToUInt(testId);
        TESTITEMINFO[] rlt;
        if (m_Request.Reserve.GetTestItemInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                TESTITEMINFO info = rlt[i];
                string url = info.szReportURL == "" ? "<span class='orange'>未提交实验报告</span>" : "<a href='" + Page.ResolveClientUrl("~/ClientWeb/") + "upload/UpLoadFile/" + info.dwTestItemID + "/" + info.szReportURL + "'>下载实验报告 <span class='glyphicon glyphicon-download'></span></a>";
                uint? score = info.dwReportScore;
                string szScore = (score == null || score == 0) ? "<span class='orange'>未评分</span>" : "<span class='text-primary'>" + score + " 分</span>";
                report_list += "<tr><td>" + (i + 1) + "</td><td>" + info.szTrueName + "(" + info.szPID + ")</td><td>" + url + "</td><td>" + szScore + "</td>" +
                    "<td>" + GetAttend(info.dwAccNo) + "</td>" +
                            "<td><button type='button' class='btn btn-info btn-xs' onclick='correct(\"" + info.dwSID + "\",\"" + info.dwAccNo + "\",\"" + info.dwReportScore + "\",\"" + info.szReportMarkInfo + "\");'>评分  <span class='glyphicon glyphicon-edit'></span></button></td></tr>";
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
}