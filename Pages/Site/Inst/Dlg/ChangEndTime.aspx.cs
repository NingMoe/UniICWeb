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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string szId = Request["id"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESVREQ vrGet = new RTRESVREQ();
            vrGet.dwResvID = (uint.Parse(szId));
            RTRESV[] vtRes;
            uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes.Length>0)
            {
                dwEndTime.Text = Get1970Date((uint)vtRes[0].dwEndTime);
            }
        }

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        string szId = Request["id"];
        RESVENDTIME vrGet =new RESVENDTIME();
        vrGet.dwResvID = (uint.Parse(szId));
        vrGet.dwEndTime = (Get1970Seconds(dwEndTime.Text));

        uResponse = m_Request.Reserve.ResvChgEndTime(vrGet, out vrGet);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox("调整结束时间成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        }
        else
        {
            MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
        }
    }
}
