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
using Newtonsoft.Json;
using System.Collections.Generic;
using UniWebLib;

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_szDev = "";
    protected string m_szLab= "";
    protected string m_szRoom= "";
    protected string m_szKind= "";
    public bool bLeader = false;
    public class devListSet
    {
        public uint dwYearTerm;
        public uint dwDevID;
        public uint dwStatCode;
        public string szDeptSN;
        public uint dwLabID;
    };
   
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVLISTREQ vrParameter = new DEVLISTREQ();
        DEVLIST[] vrResult;
        {
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetDevList(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-labid='" + vrResult[i].dwLabID.ToString() + "' data-YearTerm='" + vrResult[i].dwYearTerm.ToString() + "' data-id='" + vrResult[i].dwDevID.ToString() + "'>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szAssertSN.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szClassSN.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szModel.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szSpecification.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwComeFrom.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwNationCode.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwUnitPrice.ToString() + "</td>";
                    m_szOut += "<td>" + GetDateStr((uint)vrResult[i].dwPurchaseDate) + "</td>";
                    m_szOut += "<td  class='tdSet1' data-type='dwStatCode'>" + vrResult[i].dwStatCode + "</td>";
                    uint uDevPropery = (uint)vrResult[i].dwComeFrom;
                    if ((uDevPropery & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_FORTEACHING) > 0)
                    {
                        m_szOut += "<td>" + "调整" + "</td>";
                    }
                    else if ((uDevPropery & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_FORRESEARCH) > 0)
                    {
                        m_szOut += "<td>" + "调整" + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td>" + "" + "</td>";
                    }
                    m_szOut += "<td  class='tdSet1' data-type='szDeptSN'>" + vrResult[i].szDeptSN.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].szDeptName.ToString() + "</td>";
                    m_szOut += "</tr>";
                }
                UpdatePageCtrl(m_Request.Report);
            }
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
