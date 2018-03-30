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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();    
    protected string m_szLab= "";

    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TESTITEMREPORTREQ vrParameter = new TESTITEMREPORTREQ();
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        TESTITEMREPORT[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwYearTerm = uYearTerm;
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        uResponse = m_Request.Report.GetTestItemReport(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult != null || vrResult.Length > 0))
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='"+ vrResult[i].dwTestCardID.ToString()+"'>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestSN.ToString() + "</td>";
                m_szOut += "<td >" + vrResult[i].szTestName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwTestClass, "dwTestClass") + "</td>";
                m_szOut += "<td data-type='dwTestKind'>" + GetJustNameEqual((uint)vrResult[i].dwTestKind, "dwTestKind") + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject'>" + GetJustNameEqual(Parse(vrResult[i].szAcademicSubjectCode), "dwAcademicSubject") + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwRequirement, "dwRequirement") + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwTesteeKind, "dwTesteeKind") + "</td>";
                m_szOut += "<td  data-type='dwTesteeNum'>" + vrResult[i].dwTesteeNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupPeopleNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestHour.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabSN.ToString() + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
