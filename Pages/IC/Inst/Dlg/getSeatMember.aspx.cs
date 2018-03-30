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
using System.Text;
using System.Reflection;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szOut = "";
    protected string szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "查看座位分配情况";
        APSEATREQ vrGet = new APSEATREQ();
        vrGet.dwActivityPlanID = Parse(Request["id"]);
        APSEAT[] vtRes;
        
        REQUESTCODE uResponse = m_Request.Reserve.GetAPSeat(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                
                m_szOut += "<td  data-devid=\"" + vtRes[i].dwDevID+ "\" data-accno=\"" +vtRes[i].dwAccNo + "\" data-activityplanid=\"" + (vtRes[i].dwActivityPlanID.ToString()) + "\">" + vtRes[i].szDevName + "</td>";
                m_szOut += "<td>" + vtRes[i].szTrueName + "</td>";
                uint uStatue = (uint)vtRes[i].dwStatus;
                string szStatue = "";
                if ((uStatue & (uint)APSEAT.DWSTATUS.APSTAT_BOOKED) > 0)
                {
                    szStatue = "已被选定";
                }
                else if ((uStatue & (uint)APSEAT.DWSTATUS.APSTAT_IDLE) > 0)
                {
                    szStatue = "空闲";
                }
                m_szOut += "<td>" +szStatue+ "</td>";
                m_szOut += "</tr>";
            }
        //    UpdatePageCtrl(m_Request.Group);
        }
    }
}

