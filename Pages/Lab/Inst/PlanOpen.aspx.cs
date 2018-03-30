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
    protected string szStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
        string szcourseid = Request["courseid"];
        string szOP=Request["op"];
        if (szOP != null && szOP != "")
        {
            Del(Request["delID"]);
        }
        szStatus = GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        szStatus+=GetInputHtmlFromXml(0, CONSTHTML.option, "", "planStatus", true);
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
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;

        string szLogonName=Request["pid"];
        if (szLogonName != null && szLogonName != "")
        {
            UNIACCOUNT accnoInfo;
            if (GetAccByAccno(szLogonName, out accnoInfo))
            {
                vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
                vrParameter.szGetKey = accnoInfo.dwAccNo.ToString();
                PutMemberValue("pid", szLogonName);
                PutMemberValue("pidHidden", szLogonName);
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
        uint uStatue = Parse(Request["dwStatus"]);
        if (uStatue == 0)
        {
            vrParameter.dwStatus = null;
        }
        vrParameter.dwYearTerm = uYeartermNow;
        vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
        if (m_Request.Reserve.GetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {
            UpdatePageCtrl(m_Request.Reserve);
            UNIACCOUNT accnoTemp;
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr data-groupid='" + vrResult[i].dwGroupID.ToString() + "' data-id='" + vrResult[i].dwTestPlanID + "'>";
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
                m_szOut += "<td>" + vrResult[i].dwMaxUsers + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupUsers + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwEnrollDeadline) + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwStatus, "planStatus") + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestHour + "</td>";
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
    private void Del(string szID)
    {
        TESTPLANREQ vrGet = new TESTPLANREQ();
        vrGet.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
        vrGet.szGetKey = szID;
        UNITESTPLAN[] vtRes;
        if (m_Request.Reserve.GetTestPlan(vrGet, out vtRes)==REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            m_Request.Reserve.DelTestPlan(vtRes[0]);
        }
    }
}
