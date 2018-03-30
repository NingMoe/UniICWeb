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
        LABALLCOSTREQ vrParameter = new LABALLCOSTREQ();
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        LABALLCOST vrResult;        
       
        
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetLabAllCost(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_szOut += "<tr>";
            m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwLabNum.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwLabArea.ToString() + "</td>";
            m_szOut += "<td>" + vrResult.dwTotalCost.ToString() + "</td>";
            m_szOut += "<td>" + (vrResult.dwBuyCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwTBuyCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwKeepCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwTKeepCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwRunCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwCRunCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwBuildCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwRAndRCost) + "</td>";
            m_szOut += "<td>" + (vrResult.dwOtherCost) + "</td>";
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
