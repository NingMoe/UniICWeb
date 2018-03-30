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
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCHGREQ vrParameter = new DEVCHGREQ();

        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        DEVCHG vrResult;

        vrParameter.dwYearTerm = uYearTerm;
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        uResponse = m_Request.Report.GetDevChg(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwYearTerm != null)
        {
            for (int i = 0; i < 1; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult.dwBDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwBMoney) + "</td>";
                m_szOut += "<td>" + vrResult.dwBBigDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwBBigMoney) + "</td>";
                m_szOut += "<td>" + vrResult.dwIncDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwIncMoney) + "</td>";
                m_szOut += "<td>" + vrResult.dwDecDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwDecMoney) + "</td>";
                m_szOut += "<td>" + vrResult.dwEDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwEMoney) + "</td>";
                m_szOut += "<td>" + vrResult.dwEBigDevNum.ToString() + "</td>";
                m_szOut += "<td>" + ((uint)vrResult.dwEBigMoney) + "</td>";
                m_szOut += "</tr>";
            }
        }
        UpdatePageCtrl(m_Request.Report);


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
