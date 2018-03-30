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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        TEACHINGRESVREQ vrParameter = new TEACHINGRESVREQ();
        TEACHINGRESV[] vrResult;
        vrParameter.dwBeginDate =Parse(DateTime.Now.ToString("yyyyMMdd"));
        vrParameter.dwEndDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwResvStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        if (m_Request.Reserve.GetTeachingResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Reserve);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName + "</td>";
                RESVDEV[] resvDev = vrResult[i].ResvDev;
                string szRoomName = "";
                for (int k = 0; resvDev != null && k < resvDev.Length; k++)
                {
                    szRoomName += resvDev[k].szRoomName + ",";
                }
                m_szOut += "<td>" + szRoomName + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupUsers + "</td>";
                m_szOut += "<td>" + vrResult[i].dwCurUsers + "</td>";
                m_szOut += "<td>" + GetTeachingTime((uint)vrResult[i].dwTeachingTime) + "</td>";
                // m_szOut += "<td>" + vrResult[i].szTestName + "</td>";
                m_szOut += "</tr>";
            }

        }

        PutBackValue();
    }
    private void DelRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIROOM room = new UNIROOM();
        room.dwRoomID = Parse(szID);
        uResponse = m_Request.Device.RoomDel(room);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
