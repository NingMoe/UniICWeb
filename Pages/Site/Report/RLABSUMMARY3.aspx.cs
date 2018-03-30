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
        LABSUMMARY vrResult;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        if (IsPostBack)
        {
            string szValue = Request["changeInfo"];
            if (IsPostBack && szValue != "")
            {
                szValue = "[" + szValue + "]";
                List<LABSUMMARY> devlist = JsonConvert.DeserializeObject<List<LABSUMMARY>>(szValue);
                for (int i = 0; i < devlist.Count; i++)
                {
                    LABSUMMARY tempValue = devlist[i];
                    LABSUMMARY setValue = (LABSUMMARY)SetEmpty0ToNull<LABSUMMARY>(tempValue);
                    setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    setValue.dwYearTerm = uYearTerm;
                    uResponse = m_Request.Report.SetLabSummary(setValue);
                }
            }
        }
        
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetLabSummary(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwLabNum != null)
        {
            m_szOut += "<tr>";
            m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
            m_szOut += "<td>" + "单位名称" + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwLabNum'>" + vrResult.dwLabNum.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwLabArea'>" + vrResult.dwLabArea.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwDevNum'>" + vrResult.dwDevNum.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwDevMoney'>" + vrResult.dwDevMoney + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwBigDevNum'>" + vrResult.dwBigDevNum.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwBigMoney'>" + vrResult.dwBigMoney + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTItemNum'>" + vrResult.dwTItemNum.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTUseTime'>" + vrResult.dwTUseTime.ToString() + "</td>";
            uint uTimeTotal = ToUint(vrResult.dwDUseTime) + ToUint(vrResult.dwMUseTime) + ToUint(vrResult.dwUUseTime) + ToUint(vrResult.dwJUseTime);
            m_szOut += "<td>" + uTimeTotal + "</td>";
            m_szOut += "<td  class='tdSet' data-type='dwDUseTime'>" + (vrResult.dwDUseTime.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwMUseTime'>" + (vrResult.dwMUseTime.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwUUseTime'>" + (vrResult.dwUUseTime.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwJUseTime'>" + (vrResult.dwJUseTime.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwRItemNum'>" + (vrResult.dwRItemNum.ToString()) + "</td>";
            uint uStaffTotal = ToUint(vrResult.dwHTStaff) + ToUint(vrResult.dwMTStaff) + ToUint(vrResult.dwHSStaff) + ToUint(vrResult.dwMSStaff) + ToUint(vrResult.dwOtherStaff) + ToUint(vrResult.dwPartTimeStaff);
            m_szOut += "<td>" + uStaffTotal + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwHTStaff'>" + (vrResult.dwHTStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwMTStaff'>" + (vrResult.dwMTStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwHSStaff'>" + (vrResult.dwHSStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwMSStaff'>" + (vrResult.dwMSStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwOtherStaff'>" + (vrResult.dwOtherStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwPartTimeStaff'>" + (vrResult.dwPartTimeStaff.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwPaperNum'>" + (vrResult.dwPaperNum.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTReward'>" + (vrResult.dwTReward.ToString()) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwSReward'>" + (vrResult.dwSReward.ToString()) + "</td>";
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
