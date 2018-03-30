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
    protected string m_szRoom = "";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szType = Request["type"];
        string szLabid = Request["labid"];
        if (szType == "11")
        {
            m_Title = "远程开机";
        }
        else if (szType == "12")
        {
            m_Title = "远程关机";
        }
        else if (szType == "13")
        {
            m_Title = "远程重启";
        }
        else if (szType == "52")
        {
            m_Title = "免登录";
        }
        else if (szType == "51")
        {
            m_Title = "需要登录";
        }
        else if (szType == "45")
        {
            m_Title = "U盘锁定";
        }
        else if (szType == "41")
        {
            m_Title = "屏幕锁定";
        }
        else if (szType == "42")
        {
            m_Title = "屏幕解锁";
        }
        else if (szType == "46")
        {
            m_Title = "U盘解锁";
        }
        else if (szType == "43")
        {
            m_Title = "光驱禁用";
        }
        else if (szType == "44")
        {
            m_Title = "光驱解禁";
        }
        else if (szType == "23")
        {
            m_Title = "卸载客户端";
        }

        if (!IsPostBack)
        {
            UNIROOM[] roomList =GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
            if (roomList != null && roomList.Length > 0)
            {
                for (int i = 0; i < roomList.Length; i++)
                {
                    string szCheck = "";
                    if (i == 0)
                    {
                        szCheck = " checked=\"true\"";
                    }
                    m_szRoom += "<input class=\"enum\"" + szCheck + " type=\"radio\" name=\"" + "roomID" + "\" id='" + roomList[i].dwRoomID.ToString() + "' /> <label for=\"" + roomList[i].dwRoomID.ToString() + "\">" + roomList[i].szRoomName + "</label>";
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
            string szDevID = Request["selectID"];
            string[] szDevIDList = szDevID.Split(',');
            if (szDevIDList.Length == 0)
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
                devCtrl.dwCmd = Parse(szType);
                devCtrl.dwDevID = uDevID;
                UNIDEVICE devSet = new UNIDEVICE();
                if (getDevByID(uDevID.ToString(), out devSet))
                {
                    devCtrl.dwLabID = devSet.dwLabID;
                    m_Request.Device.DevCtrl(devCtrl, out devCtrl);
                    MessageBox(m_Title + "发送成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
                }
            }
        }
        return;
    }

}
