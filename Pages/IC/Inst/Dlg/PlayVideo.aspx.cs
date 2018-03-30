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
                ip.Value = doorCtrl[0].szDCSIP.ToString();
                ctrlSN.Value = doorCtrl[0].dwCtrlSN.ToString();
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
