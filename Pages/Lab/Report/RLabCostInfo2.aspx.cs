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
        LABALLCOSTREQ vrParameter = new LABALLCOSTREQ();
        uint uYearTerm = 20131401;

        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        LABALLCOST vrResult;
        string opSub = Request["opSub"];
        if (IsPostBack && opSub == "1")
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetLabAllCost(vrParameter, out vrResult);

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwLabNum!=null)
            {
                LABALLCOST setValue2 = vrResult;
                setValue2.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
                m_Request.Report.SetLabAllCost(setValue2);
            }
        }
        if (IsPostBack && opSub!= "1")
        {
            string szValue = Request["changeInfo"];
            if (IsPostBack && szValue != "")
            {
                szValue = "[" + szValue + "]";
                List<LABALLCOST> devlist = JsonConvert.DeserializeObject<List<LABALLCOST>>(szValue);
                for (int i = 0; i < devlist.Count; i++)
                {
                    LABALLCOST tempValue = devlist[i];
                    LABALLCOST setValue = (LABALLCOST)SetEmpty0ToNull<LABALLCOST>(tempValue);
                    setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    setValue.dwYearTerm = uYearTerm;
                    uResponse = m_Request.Report.SetLabAllCost(setValue);
                }
            }
        }
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetLabAllCost(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwLabNum!=null)
        {
            m_szOut += "<tr>";
            m_szOut += "<td >" + ConfigConst.GCSchoolCode.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwLabNum'>" + vrResult.dwLabNum.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwLabArea'>" + vrResult.dwLabArea.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTotalCost'>" + vrResult.dwTotalCost.ToString() + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwBuyCost'>" + (vrResult.dwBuyCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTBuyCost'>" + (vrResult.dwTBuyCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwKeepCost'>" + (vrResult.dwKeepCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwTKeepCost'>" + (vrResult.dwTKeepCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwRunCost'>" + (vrResult.dwRunCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwCRunCost'>" + (vrResult.dwCRunCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwBuildCost'>" + (vrResult.dwBuildCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwRAndRCost'>" + (vrResult.dwRAndRCost) + "</td>";
            m_szOut += "<td class='tdSet' data-type='dwOtherCost'>" + (vrResult.dwOtherCost) + "</td>";
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
