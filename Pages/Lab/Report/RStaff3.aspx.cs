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
    protected string dwAcademicSubject="";
    protected string dwProfessionalTitle="";
    protected string dwEducation = "";
    protected string dwExpertType = "";
    public string szLab = "";
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        STAFFINFOREQ vrParameter = new STAFFINFOREQ();
        STAFFINFO[] vrResult;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetStaffInfo(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwAccNo.ToString()+ "'>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szPID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwLabID + "</td>";
                m_szOut += "<td class='tdSet' data-value=" + vrResult[i].dwLabID + " data-type='dwlabid' data-kind='select'>" + vrResult[i].szLabName + "</td>";
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
                m_szOut += "<td class='tdSet' data-type='dwAcademicSubject'  data-value=" + vrResult[i].szAcademicSubjectCode + " data-kind='select'>" + GetJustNameEqual(Parse(vrResult[i].szAcademicSubjectCode), "dwAcademicSubject", false) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwProfessionalTitle'  data-value=" + vrResult[i].dwProfessionalTitle + " data-kind='select'>" + GetJustNameEqual(vrResult[i].dwProfessionalTitle, "dwProfessionalTitle", false) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwEducation' data-value=" + vrResult[i].dwEducation + " data-kind='select'>" + GetJustNameEqual(vrResult[i].dwEducation, "dwEducation", false) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwExpertType' data-value=" + vrResult[i].dwExpertType + " data-kind='select'>" + GetJustNameEqual(vrResult[i].dwExpertType, "dwExpertType", false) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwInlandUduTime' data-kind='text'>" + vrResult[i].dwInlandUduTime + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwInlandOtherTime' data-kind='text'>" + vrResult[i].dwInlandOtherTime + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwAbroadUduTime' data-kind='text'>" + vrResult[i].dwAbroadUduTime + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwAbroadOtherTime' data-kind='text'>" + vrResult[i].dwAbroadOtherTime + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
