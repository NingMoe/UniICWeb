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
        LABINFOREQ vrParameter = new LABINFOREQ();

        LABINFO[] vrResult;        
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
        }
        if (IsPostBack)
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetLabInfo(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    LABINFO temp = vrResult[i];
                    temp.dwReportStat = (uint)(uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    temp.dwYearTerm = uYearTerm;
                    m_Request.Report.SetLabInfo(temp);
                }
            }
        }
        
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetLabInfo(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetLabInfo(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (vrResult == null || vrResult.Length == 0))
            {
                vrParameter.dwYearTerm = uYearTerm;
                vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
                uResponse = m_Request.Report.GetLabInfo(vrParameter, out vrResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
                {
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        LABINFO temp = vrResult[i];
                        temp.dwReportStat = (uint)(uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                        temp.dwYearTerm = uYearTerm;
                        m_Request.Report.SetLabInfo(temp);
                    }
                }
            }

        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_ORIGINAL;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetLabInfo(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>"; 
                m_szOut += "<td>" + vrResult[i].szLabSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwLabClass.ToString() + "</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwCreateDate)+ "</td>";
                m_szOut += "<td>" +"0" + "</td>";//面积
                m_szOut += "<td>" + vrResult[i].szAcademicSubjectCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTNReward.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTPReward.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTPatent.ToString() + "</td>";

                uint uStudentReword = (uint)vrResult[i].dwSNReward + (uint)vrResult[i].dwSPReward + (uint)vrResult[i].dwSPatent;
                m_szOut += "<td>" + uStudentReword.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwTThreeIndex.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwRThreeIndex.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwTKernelJournal.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwRKernelJournal.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwTestBookNum.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwPRItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwRItemNum.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwSItemNum.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwPRItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPTItemNum.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwBKThesisUsers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwZKThesisUsers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwSSThesisUsers.ToString() + "</td>";
              //  m_szOut += "<td>" + vrResult[i].dwBSThesisUsers.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwOtherItemNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseUsers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwOtherUsers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseTime.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwOtherTime.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwPartTimeUsers.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].dwTotalCost.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwConsumeCost.ToString() + "</td>";
                
                    
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
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
    }
}
