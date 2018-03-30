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

public partial class Sub_Plan : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected uint m_TermList = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_TermList = GetTermList();

        TESTPLANREQ vrParameter = new TESTPLANREQ();
        UNITESTPLAN[] vrResult;
        GetHTTPObj(out vrParameter);

        vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
        vrParameter.szGetKey = "";
        vrParameter.dwYearTerm = 20131401;// GetTerm(Request["dwYearTerm"]);
        vrParameter.szReqExtInfo.dwNeedLines = 10;

        if (m_Request.Reserve.GetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr data-id='" + vrResult[i].dwTestPlanID + "'>";
                m_szOut += "<td>" + vrResult[i].dwTestPlanID + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestPlanName + "</td>";
                TranTerm(ref vrResult[i].dwYearTerm);
                if (vrResult[i].dwYearTerm == 1)
                {
                    m_szOut += "<td>上学期</td>";
                }
                else if (vrResult[i].dwYearTerm == 2)
                {
                    m_szOut += "<td>下学期</td>";
                }
                else
                {
                    m_szOut += "<td>本学期</td>";
                }
                m_szOut += "<td>" + vrResult[i].szTeacherName + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalTestHour + "</td>";
                m_szOut += "<td>" + GetTestPlanStatus((uint)vrResult[i].dwStatus) + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }
        vrParameter.dwYearTerm = GetDefaultTerm(Request["dwYearTerm"]);

        PutJSObj(vrParameter);
    }

    string GetTestPlanStatus(uint dwStatus)
    {
        if ((dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN) != 0)
        {
            return "未开放";
        }
        else if ((dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_OPENING) != 0)
        {
            return "开放中";
        }
        else if ((dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_CLOSED) != 0)
        {
            return "已关闭";
        }
        else
        {
            return "未开放";
        }
    }

    string GetTestPlanKind(uint dwKind)
    {
        if ((dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING) != 0)
        {
            return "教学统一安排";
        }
        else if ((dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN) != 0)
        {
            return "开放实验";
        }
        else if ((dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHFORSELF) != 0)
        {
            return "教学自主安排";
        }
        else
        {
            return "教学统一安排";
        }
    }
}
