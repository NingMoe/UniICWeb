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
        LABSUMMARY2REQ vrParameter = new LABSUMMARY2REQ();
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        LABSUMMARY2 vrResult;               
        vrParameter.dwReportStat = (uint)(uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        if (IsPostBack)
        {
            string szValue = Request["changeInfo"];
            if (IsPostBack && szValue != "")
            {
                szValue = "[" + szValue + "]";
                List<LABSUMMARY2> devlist = JsonConvert.DeserializeObject<List<LABSUMMARY2>>(szValue);
                for (int i = 0; i < devlist.Count; i++)
                {
                    LABSUMMARY2 tempValue = devlist[i];
                    LABSUMMARY2 setValue = (LABSUMMARY2)SetEmpty0ToNull<LABSUMMARY2>(tempValue);
                    setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                    setValue.dwYearTerm = uYearTerm;
                    uResponse = m_Request.Report.SetLabSummary2(setValue);
                }
            }
        }

        uResponse=m_Request.Report.GetLabSummary2(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS&&vrResult.dwLabNum!=null)
        {
                m_szOut += "<tr>";
                m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" +"单位名称" + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwLabNum'>" + vrResult.dwLabNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwLabArea'>" + vrResult.dwLabArea.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwDevNum'>" + vrResult.dwDevNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwDevMoney'>" + ToUint(vrResult.dwDevMoney) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwBigDevNum'>" + vrResult.dwBigDevNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwBigMoney'>" + ToUint(vrResult.dwBigMoney) + "</td>";            
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
