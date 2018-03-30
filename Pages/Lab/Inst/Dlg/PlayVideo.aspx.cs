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

public partial class _Default : UniWebLib.UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
    protected string m_szButton = "";
	protected void Page_Load(object sender, EventArgs e)
	{

        m_Title = "视频";
        if (!IsPostBack)
        {
            UNIDOORCTRL[] doorCtrl;
            string szRoomNo=Request["szRoomNO"];
            string szTime = Request["time"];
            if (GetDoorCtrl(szRoomNo, out doorCtrl))
            {
                for (int i = 0; i < doorCtrl.Length; i++)
                {
                    m_szButton += "<input type='button' time="+szTime+" ip="+doorCtrl[i].szDCSIP+" class='btn' id='" + doorCtrl[i].dwCtrlSN + "' value='"+doorCtrl[i].szCtrlModel+"' />";
                }
                ip.Value = doorCtrl[doorCtrl.Length-1].szDCSIP.ToString();
                ctrlSN.Value = doorCtrl[doorCtrl.Length-1].dwCtrlSN.ToString();
                dwDate.Value = szTime;
            }
            else
            {
                ip.Value = "127.0.0.1";
                ctrlSN.Value = "0";
                dwDate.Value = szTime;
            }
                 
        }

       
	}
    private bool GetDoorCtrl(string szRoomNo, out UNIDOORCTRL[] doorCtrl)
    {
        //-todo
        //doorCtrl = new UNIDOORCTRL();

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ROOMCTRLREQ roomGet = new ROOMCTRLREQ();
        roomGet.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_VIDEOCTRL;
        roomGet.szRoomNo = szRoomNo;
        uResponse = m_Request.Device.GetRoomCtrlInfo(roomGet, out doorCtrl);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;
    }
}
