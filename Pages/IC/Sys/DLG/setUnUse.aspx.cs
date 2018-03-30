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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string szDevID = Request["id"];
            string szText = Request["szMessageInfo"];
            if (szDevID == null || szDevID == "")
            {                
                MessageBox("设置失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            UNIDEVICE outDev = new UNIDEVICE();
            if(getDevByID(szDevID,out outDev))
            {
                outDev.dwDevStat = (uint)UNIDEVICE.DWDEVSTAT.DEVSTAT_DAMAGED;
                outDev.szExtInfo = szText;
                if (m_Request.Device.Set(outDev, out outDev) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
 
                }
            }
        }

    }
}
