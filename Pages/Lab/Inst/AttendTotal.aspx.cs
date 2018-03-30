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

public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szAttendRule = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ATTENDRULEREQ ruleGet = new ATTENDRULEREQ();
        ATTENDRULE[] ruleRes;
       
        ATTENDSTATREQ vrParameter = new ATTENDSTATREQ();
        GetHTTPObj(out vrParameter);
        string szAttend=Request["attendid"];
        if (m_Request.Attendance.GetAttendRule(ruleGet, out ruleRes) == REQUESTCODE.EXECUTE_SUCCESS && ruleRes != null && ruleRes.Length > 0)
        {
            //szAttendRule += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < ruleRes.Length; i++)
            {
                szAttendRule += GetInputItemHtml(CONSTHTML.option, "", ruleRes[i].szAttendName, ruleRes[i].dwAttendID.ToString());
            }
            if (szAttend == null || szAttend == "")
            {
                vrParameter.dwAttendID = ruleRes[0].dwAttendID;
            }
        }
        if (ruleGet.dwAttendID == null)
        {
            uint uAttend=Parse(szAttend);
            if(uAttend!=0)
            {
                vrParameter.dwAttendID = uAttend;
            }
        }
        if (!IsPostBack)
        {
           
        }
        ATTENDSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Attendance.GetAttendStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwAccNo.ToString() + ">" + vrResult[i].szPID + "</td>";
                m_szOut += "<td>" + (vrResult[i].szTrueName) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwTotalTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwAttendTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwAbsentTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwLateTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwLeaveTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwLLTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwUseLessTimes) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwLeaveNoCardTimes) + "</td>";    
                m_szOut += "<td >" + (vrResult[i].dwTotalMin) + "</td>";
                
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Attendance);
        }
        PutBackValue();
    }
}
