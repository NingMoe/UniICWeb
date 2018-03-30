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
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        LABSUMMARYREQ vrParameter = new LABSUMMARYREQ();
        uint uYearTerm = 20131401;

        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        LABSUMMARY vrResult;                
      
        if (IsPostBack)
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetLabSummary(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                LABSUMMARY setValue = vrResult;
                setValue.dwYearTerm = uYearTerm;
                setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                uResponse = m_Request.Report.SetLabSummary(setValue);
            }
        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetLabSummary(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwYearTerm == null)
        {

            vrParameter.dwYearTerm = uYearTerm;
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
            uResponse = m_Request.Report.GetLabSummary(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwYearTerm == null)
            {
                vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
                vrParameter.dwYearTerm = uYearTerm;
                uResponse = m_Request.Report.GetLabSummary(vrParameter, out vrResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    LABSUMMARY setValue = vrResult;
                    setValue.dwYearTerm = uYearTerm;
                    setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    uResponse = m_Request.Report.SetLabSummary(setValue);
                }
            }

        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetLabSummary(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_szOut += "<tr>";
            m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
            m_szOut += "<td>" + "单位名称" + "</td>";
            m_szOut += "<td>" + vrResult.dwLabNum.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwLabArea.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwDevNum.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwDevMoney + "</td>";
            m_szOut += "<td>" + vrResult.dwBigDevNum.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwBigMoney + "</td>";
            m_szOut += "<td>" + vrResult.dwTItemNum.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwTUseTime.ToString() + "</td>";
            uint uTimeTotal = ToUint(vrResult.dwDUseTime) + ToUint(vrResult.dwMUseTime) + ToUint(vrResult.dwUUseTime) + ToUint(vrResult.dwJUseTime);
            m_szOut += "<td>" + uTimeTotal + "</td>";
            m_szOut += "<td>" + (vrResult.dwDUseTime.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwMUseTime.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwUUseTime.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwJUseTime.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwRItemNum.ToString()) + "</td>";
            uint uStaffTotal = ToUint(vrResult.dwHTStaff) + ToUint(vrResult.dwMTStaff) + ToUint(vrResult.dwHSStaff) + ToUint(vrResult.dwMSStaff) + ToUint(vrResult.dwOtherStaff) + ToUint(vrResult.dwPartTimeStaff);
            m_szOut += "<td>" + uStaffTotal + "</td>";
            m_szOut += "<td>" + (vrResult.dwHTStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwMTStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwHSStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwMSStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwOtherStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwPartTimeStaff.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwPaperNum.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwTReward.ToString()) + "</td>";
            m_szOut += "<td>" + (vrResult.dwSReward.ToString()) + "</td>";
            m_szOut += "</tr>";

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
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
    }
}
