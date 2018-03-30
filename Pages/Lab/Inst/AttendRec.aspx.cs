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
        ATTENDRECREQ vrParameter = new ATTENDRECREQ();
        ATTENDREC[] vrResult;
     

        ATTENDRULEREQ attendreq = new ATTENDRULEREQ();
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (vrParameter.dwStartDate == 0)
        {
            vrParameter.dwStartDate = null;
        }
        if (vrParameter.dwEndDate == 0)
        {
            vrParameter.dwEndDate =null;
        }
      //  if(vrParameter.dwStartDate=)
        uint uAttend =Parse(Request["attendid"]);
       
        ATTENDRULE[] vtAttendRes;
        if (m_Request.Attendance.GetAttendRule(attendreq, out vtAttendRes) == REQUESTCODE.EXECUTE_SUCCESS && vtAttendRes != null && vtAttendRes.Length > 0)
        {
            szAttendRule += GetInputItemHtml(CONSTHTML.option, "","全部", "0");
            for (int i = 0; i < vtAttendRes.Length; i++)
            {
                szAttendRule += GetInputItemHtml(CONSTHTML.option, "", vtAttendRes[i].szAttendName, vtAttendRes[i].dwAttendID.ToString());
            }
            if (uAttend!=0)
            {
                vrParameter.dwAttendID = vtAttendRes[0].dwAttendID;
            }
        }
        if (uAttend != 0)
        {
            vrParameter.dwAttendID = uAttend;
        }
        vrParameter.szReqExtInfo.dwNeedLines = 10;
        vrParameter.szReqExtInfo.dwStartLine = 0;
        if (m_Request.Attendance.GetAttendRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwSID.ToString() + ">" + vrResult[i].szPID + "</td>";
                m_szOut += "<td>" + (vrResult[i].szTrueName) + "</td>";
                m_szOut += "<td >" + (vrResult[i].szAttendName) + "</td>";
                m_szOut += "<td >" + GetDateStr(vrResult[i].dwAttendDate) + "</td>";
                m_szOut += "<td >" + (vrResult[i].szRoomName) + "</td>";
                
                m_szOut += "<td >" +Get1970Date(vrResult[i].dwInTime) + "</td>";
                m_szOut += "<td >" + Get1970Date(vrResult[i].dwOutTime) + "</td>";
                m_szOut += "<td >" + Get1970Date(vrResult[i].dwLatestInTime) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwStayMin) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwCardTimes) + "</td>";
                m_szOut += "<td >" + GetJustName(vrResult[i].dwAttendStat, "attendstatus") + "</td>";
             //   m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Attendance);
        }
        PutBackValue();
    }
}
