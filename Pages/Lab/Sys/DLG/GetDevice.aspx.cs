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
    protected string m_szDoorCtrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DEVREQ vrDevReq = new DEVREQ();
        vrDevReq.dwDevID = Parse(Request["id"]);
        UNIDEVICE[] vtDev;
        if (m_Request.Device.Get(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
        }
        else
        {
            if (vtDev.Length == 0)
            {
                MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                PutJSObj(vtDev[0]);
                if (vtDev[0].dwCtrlMode == 1)
                {
                    m_szDoorCtrl = "登录认证";
                }
                else if (vtDev[0].dwCtrlMode == 2)
                {
                    m_szDoorCtrl = "刷卡认证";
                }
                else if (vtDev[0].dwCtrlMode == 3)
                {
                    m_szDoorCtrl = "人工管理";
                }
            }
            m_Title = "查看" + ConfigConst.GCDevName;

        }
    }
}
