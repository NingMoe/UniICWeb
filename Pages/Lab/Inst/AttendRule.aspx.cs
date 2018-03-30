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

    protected void Page_Load(object sender, EventArgs e)
    {
        string szID = Request["delID"];
        if (szID != null && szID != "")
        {
            Del(szID);
        }

        ATTENDRULEREQ vrParameter = new ATTENDRULEREQ();
        vrParameter.szAttendName = Request["szAttendName"];

        ATTENDRULE[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Attendance.GetAttendRule(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-enddate=" + vrResult[i].dwEndDate.ToString() +" data-startdate=" + vrResult[i].dwStartDate.ToString()+ " data-groupid=" + vrResult[i].dwGroupID.ToString() + " data-id=" + vrResult[i].dwAttendID.ToString() + ">" + vrResult[i].szAttendName + "</td>";
                m_szOut += "<td>" + DateToStr(vrResult[i].dwStartDate) + "</td>";
                m_szOut += "<td >" + DateToStr(vrResult[i].dwEndDate) + "</td>";
                m_szOut += "<td >" + GetTimeStr(vrResult[i].dwEarlyInTime)+"~"+ GetTimeStr(vrResult[i].dwLateInTime)  + "</td>";
                m_szOut += "<td >" + GetTimeStr(vrResult[i].dwEarlyOutTime) + "~" + GetTimeStr(vrResult[i].dwLateOutTime) + "</td>";
                m_szOut += "<td >" + (vrResult[i].dwMinStayTime) + "</td>";
                ATTENDROOM[] attendRoom = vrResult[i].AttendRoom;
                string szAttendRoom = "";
                string szAttendRoomLow="";
                for (int j = 0; j < attendRoom.Length; j++)
                {
                    if(j<4)
                    {
                         szAttendRoomLow += attendRoom[j].szRoomName + "，";
                    }
                    szAttendRoom += attendRoom[j].szRoomName + "，";
                }
                szAttendRoomLow+="...";
                m_szOut += "<td title='"+szAttendRoom+"'>" + szAttendRoomLow + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Attendance);
        }
        PutBackValue();
    }
    private void Del(string szID)
    {
        ATTENDRULE rule = new ATTENDRULE();
        rule.dwAttendID = Parse(szID);
        m_Request.Attendance.DelAttendRule(rule);
    }
}
