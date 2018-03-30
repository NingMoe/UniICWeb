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
    protected string m_szSta = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        string szOP = "新建";

        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        if (IsPostBack)
        {
            DEVMONITOR newDCS;
            GetHTTPObj(out newDCS);
            if (m_Request.Device.DevMonitorSet(newDCS, out newDCS) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOP+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOP + "成功", szOP+"成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }

       
        if (Request["op"] == "set")
        {
            bSet = true;

            DEVMONITORREQ vrGetDCS = new DEVMONITORREQ();
            vrGetDCS.dwMonitorID = Parse(Request["dwSN"]);
            DEVMONITOR[] vrResultStation;
            if (m_Request.Device.DevMonitorGet(vrGetDCS, out vrResultStation) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrResultStation.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrResultStation[0]);
                    m_Title = "修改【" + vrResultStation[0].szMonitorName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建";
        }
	}
}
