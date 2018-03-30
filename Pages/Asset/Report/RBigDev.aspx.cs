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
    protected string m_szDev = "";
    protected string m_szLab= "";
    protected string m_szRoom= "";
    protected string m_szKind= "";
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        BIGDEVREQ vrParameter = new BIGDEVREQ();
        vrParameter.dwUnitPrice = 100000;
        BIGDEV[] vrResult;
       
        if (IsPostBack)
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetBigDev(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult != null || vrResult.Length > 0))
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    BIGDEV setBigDev = new BIGDEV();
                    setBigDev = vrResult[i];
                    setBigDev.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    setBigDev.dwYearTerm = uYearTerm;
                    uResponse = m_Request.Report.SetBigDev(setBigDev);
                }
            }
        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetBigDev(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
        {
             vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetBigDev(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
            {
                vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
                vrParameter.dwYearTerm = uYearTerm;
                uResponse = m_Request.Report.GetBigDev(vrParameter, out vrResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
                {
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        BIGDEV setBigDev = new BIGDEV();
                        setBigDev = vrResult[i];
                        setBigDev.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                        uResponse = m_Request.Report.SetBigDev(setBigDev);
                    }
                }
            }
        }

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetBigDev(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwDevID.ToString()+ ">" + ConfigConst.GCSchoolCode.ToString() + "</td>"; 
                m_szOut += "<td>" + vrResult[i].szAssertSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szClassSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" +(uint)vrResult[i].dwUnitPrice + "</td>";
                m_szOut += "<td>" + vrResult[i].szModel.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szSpecification.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult[i].dwTUseTime) + "</td>";
                m_szOut += "<td>" + ((uint)vrResult[i].dwRUseTime) + "</td>";
                m_szOut += "<td>" + ((uint)vrResult[i].dwSUseTime) + "</td>";
                m_szOut += "<td>" +  ((uint)vrResult[i].dwOUseTime) + "</td>";
                m_szOut += "<td>" + vrResult[i].dwSampleNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseStudents.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseTeachers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseOthers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwRItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwSItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwNReward.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPReward.ToString() + "</td>"; 
                m_szOut += "<td>" + vrResult[i].dwTPatent.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwSPatent.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwThreeIndex.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwKernelJournal.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szAttendantName.ToString() + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }      
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        string szlab = Request["szLab"];
        string szDevKind = Request["szDevKind"];
        string szRoom = Request["szRoom"];
        if (szlab != null && szlab != "")
        {
            PutMemberValue2("szLab", szlab);
        }
        if (szDevKind != null && szDevKind != "")
        {
            PutMemberValue2("szDevKind", szDevKind);
        }
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
    }
}
