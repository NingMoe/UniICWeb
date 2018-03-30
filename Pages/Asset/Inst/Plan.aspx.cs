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
    protected string m_TermList = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
        string szcourseid = Request["courseid"];
        uint uYeartermNow=Parse(szYearTerm);
        if (termList != null)
        {
            for (int i = 0; i < termList.Length; i++)
            {
                m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                uint uYearTermState = (uint)termList[i].dwStatus;
                if (szYearTerm == null && (uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    uYeartermNow=(uint)termList[i].dwYearTerm;
                }
            }
        }
        

        TESTPLANREQ vrParameter = new TESTPLANREQ();
        UNITESTPLAN[] vrResult;
        GetHTTPObj(out vrParameter);
        vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
        string szLogonName=Request["pid"];
        if (szLogonName != null && szLogonName != "")
        {
            UNIACCOUNT accnoInfo;
            if (GetAccByLogonName(szLogonName, out accnoInfo))
            {
                vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
                vrParameter.szGetKey = accnoInfo.dwAccNo.ToString();
                PutMemberValue("pid", szLogonName);
            }
        }
        if (szcourseid != null && szcourseid != "")
        {
            vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYCOURSEID;
            vrParameter.szGetKey = szcourseid;
            string szCourseName = Request["courseName"];
            PutMemberValue("courseid", szcourseid);
            PutMemberValue("courseName", szCourseName);
        }
        vrParameter.dwYearTerm = uYeartermNow;
        
        if (m_Request.Reserve.GetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {
            UpdatePageCtrl(m_Request.Reserve);
            UNIACCOUNT accnoTemp;
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr data-id='" + vrResult[i].dwTestPlanID + "'>";
                m_szOut += "<td>" + vrResult[i].dwTestPlanID + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestPlanName + "</td>";
                m_szOut += "<td>" +GetTermText(vrResult[i].dwYearTerm) + "</td>";
                if(GetAccByAccno(vrResult[i].dwTeacherID.ToString(),out accnoTemp))
                {
                    m_szOut += "<td>" + vrResult[i].szTeacherName + "(" + accnoTemp.szLogonName + ")" + "</td>";
                }
                else{
                    m_szOut += "<td>" + vrResult[i].szTeacherName + "</td>";
                }
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestNum + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalTestHour + "</td>";
                m_szOut += "<td>" + vrResult[i].dwResvTestHour + "</td>";
                m_szOut += "<td>" + vrResult[i].dwDoneTestHour + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
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
