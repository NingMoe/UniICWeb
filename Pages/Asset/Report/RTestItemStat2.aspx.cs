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
using Newtonsoft.Json;
using System.Collections.Generic;
public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    public string dwTesteeKind = "";
    public string dwAcademicSubject = "";
    public string szLab = "";
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        TESTITEMREPORTREQ vrParameter = new TESTITEMREPORTREQ();
        TESTITEMREPORT[] vrResult;
        dwTesteeKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwTesteeKind", true);
        dwAcademicSubject = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwAcademicSubject", true);
        UNILAB[] allLab = GetAllLab();
        for (int i = 0; i < allLab.Length; i++)
        {
            szLab += GetInputItemHtml(CONSTHTML.option, "", allLab[i].szLabName, allLab[i].dwLabID.ToString());
        }
        string opSub = Request["opSub"];
        if (IsPostBack && opSub == "1")
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetTestItemReport(vrParameter, out vrResult);

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    TESTITEMREPORT setValue2 = vrResult[i];
                    setValue2.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
                    m_Request.Report.SetTestItemReport(setValue2);
                }
            }
        }
        string szValue = Request["changeInfo"];
        if (IsPostBack && szValue != "" && opSub != "1")
        {
            szValue = "[" + szValue + "]";
            List<TESTITEMREPORT> devlist = JsonConvert.DeserializeObject<List<TESTITEMREPORT>>(szValue);
            for (int i = 0; i < devlist.Count; i++)
            {
                TESTITEMREPORT tempValue = devlist[i];
                TESTITEMREPORT setValue = (TESTITEMREPORT)SetEmpty0ToNull<TESTITEMREPORT>(tempValue);
                setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                setValue.dwYearTerm = uYearTerm;
                m_Request.Report.SetTestItemReport(setValue);
            }
        }

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwYearTerm = uYearTerm;
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        uResponse = m_Request.Report.GetTestItemReport(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult != null || vrResult.Length > 0))
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-dwTesteeKind='" + vrResult[i].dwTesteeKind + "'  data-id='" + vrResult[i].dwTestCardID.ToString() + "'"
                     + " data-dwAcademicSubject='" + vrResult[i].szAcademicSubjectCode.ToString() + "' data-dwLabID='" + vrResult[i].dwLabID.ToString() + "'>" 
                    + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestSN.ToString() + "</td>";
                m_szOut += "<td >" + vrResult[i].szTestName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwTestClass, "dwTestClass", false) + "</td>";
                m_szOut += "<td >" + GetJustNameEqual((uint)vrResult[i].dwTestKind, "dwTestKind", false) + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject' data-kind='select'  data-value='" + vrResult[i].szAcademicSubjectCode + "'>" + GetJustNameEqual(Parse(vrResult[i].szAcademicSubjectCode), "dwAcademicSubject", false) + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwRequirement, "dwRequirement", false) + "</td>";
                m_szOut += "<td data-type='dwTesteeKind' data-kind='select' data-value='" + vrResult[i].dwTesteeKind + "'>" + GetJustNameEqual((uint)vrResult[i].dwTesteeKind, "dwTesteeKind", false) + "</td>";
                m_szOut += "<td class='tdSet' data-kind='text' data-type='dwTesteeNum'>" + vrResult[i].dwTesteeNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupPeopleNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestHour.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabSN.ToString() + "</td>";
                m_szOut += "<td data-type='dwLabID' data-kind='select'  data-value='" + vrResult[i].dwLabID + "'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
