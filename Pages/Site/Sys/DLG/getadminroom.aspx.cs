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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string szManRole = "";
    protected string m_szRoom = "";
    protected string m_szLab = "";
    protected string m_checkLab = "";
    protected string m_adminKind = "";
    protected string m_szOut = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (!IsPostBack)
        {
            uint dwAccno =Parse(Request["dwID"]);

            if (dwAccno == 0)
            {
                return;
            }
            MANROOMREQ manRoomGet = new MANROOMREQ();
            manRoomGet.dwAccNo = dwAccno;
            manRoomGet.dwManFlag = 1;
            MANROOM[] vtResManRoom;
            if (m_Request.Admin.GetManRoom(manRoomGet, out vtResManRoom) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                
                for (int i = 0; i < vtResManRoom.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-id=\"" + vtResManRoom[i].dwRoomID.ToString() + "\">" + vtResManRoom[i].szRoomNo + "</td>";
                    m_szOut += "<td>" + vtResManRoom[i].szRoomName + "</td>";
                    m_szOut += "</tr>";
                }
            }

        }
	}
}