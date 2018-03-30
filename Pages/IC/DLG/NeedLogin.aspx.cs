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
    protected string m_szRoom="";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "机器免登录";
        if (!IsPostBack)
        {   
            UNIROOM[] roomList = GetAllRoom();
            if (roomList != null && roomList.Length > 0)
            {
                for (int i = 0; i < roomList.Length; i++)
                {
                    string szCheck = "";
                    if (i == 0)
                    {
                        szCheck = " checked=\"true\"";
                    }
                    m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"radio\" name=\"" + "roomID" + "\" value=\"" + roomList[i].dwRoomID.ToString() + "\" /> " + roomList[i].szRoomName + "</label>";
                }
                UNIDEVICE[] devList = GetDevByRoomId(roomList[0].dwRoomID);
                if (devList != null && devList.Length > 0)
                {
                    for (int i = 0; i < devList.Length; i++)
                    {
                        m_szDev += "<label><input class=\"enum\" type=\"checkbox\" name=\"" + "devID" + "\" value=\"" + devList[i].dwDevID.ToString() + "\" /> " + devList[i].szDevName + "</label>,";
                      
                    }
                }
            }
        
        }
        else if (Request["op"] == "set")
        {
            string szDevID=Request["selectID"];
            string[] szDevIDList=szDevID.Split(',');
            if(szDevIDList.Length==0)
            {
                return;
            }
            for (int i = 0; i < szDevIDList.Length; i++)
            {
                DEVCTRLINFO devCtrl = new DEVCTRLINFO();
                uint uDevID = Parse(szDevIDList[i]);
                if (uDevID == 0)
                {
                    continue;
                }
                devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_NEEDLOGON;
                devCtrl.dwDevID = uDevID;
                m_Request.Device.DevCtrl(devCtrl, out devCtrl);
                MessageBox("需登录命令发送成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
            }
            return;
        }
        
    }
}