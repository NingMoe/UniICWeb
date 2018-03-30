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

    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        STAFFINFOREQ vrParameter = new STAFFINFOREQ();
        STAFFINFO[] vrResult;
        if (IsPostBack)
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    STAFFINFO temp = vrResult[i];
                    STAFFINFO setValueObj = new STAFFINFO();
                    setValueObj = temp;
                    setValueObj.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    setValueObj.dwYearTerm = uYearTerm;
                    m_Request.Report.SetStaffInfo(setValueObj);
                }
            }
        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
        {
            vrParameter.dwYearTerm = uYearTerm;
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
            uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
            {
                vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
                vrParameter.dwYearTerm = uYearTerm;
                uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
                {
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        STAFFINFO temp = vrResult[i];
                        STAFFINFO setValueObj = new STAFFINFO();
                        setValueObj = temp;
                        setValueObj.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                        setValueObj.dwYearTerm = uYearTerm;
                        m_Request.Report.SetStaffInfo(setValueObj);
                    }
                }
            }
        }


        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szPID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwLabID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName.ToString() + "</td>";
                uint uSex = (uint)vrResult[i].dwSex;
                if (uSex == 1)
                {
                    m_szOut += "<td>" + "男" + "</td>";
                }
                else if (uSex == 2)
                {
                    m_szOut += "<td>" + "女" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + "" + "</td>";
                }
                m_szOut += "<td>" + GetDateStr((uint)vrResult[i].dwBirthDate) + "</td>";
                m_szOut += "<td wAcademicSubject' data-kind='select'>" + GetJustNameEqual(Parse(vrResult[i].szAcademicSubjectCode), "dwAcademicSubject", false) + "</td>";
                m_szOut += "<td  data-type='dwAcademicSubject' data-kind='select'>" + GetJustNameEqual(vrResult[i].dwProfessionalTitle, "dwProfessionalTitle", false) + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject' data-kind='select'>" + GetJustNameEqual(vrResult[i].dwEducation, "dwEducation", false) + "</td>";
                m_szOut += "<td  data-type='dwAcademicSubject' data-kind='select'>" + GetJustNameEqual(vrResult[i].dwExpertType, "dwExpertType", false) + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject' data-kind='select'>" + vrResult[i].dwInlandUduTime + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject' data-kind='select'>" + vrResult[i].dwInlandOtherTime + "</td>";
                m_szOut += "<td  data-type='dwAcademicSubject' data-kind='select'>" + vrResult[i].dwAbroadUduTime + "</td>";
                m_szOut += "<td data-type='dwAcademicSubject' data-kind='select'>" + vrResult[i].dwAbroadOtherTime + "</td>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
