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
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            SAMPLEINFO newSample;
            GetHTTPObj(out newSample);
            newSample.dwUnitFee1 = newSample.dwUnitFee2;
            if (m_Request.Reserve.SetSampleInfo(newSample, out newSample) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
        if (Request["op"] == "set")
        {
            bSet = true;

            SAMPLEINFOREQ vrReq = new SAMPLEINFOREQ();
            SAMPLEINFO[] vrRes;
            if (m_Request.Reserve.GetSampleInfo(vrReq, out vrRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {                
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrRes[0]);
                    m_Title = "修改" + "【" + vrRes[0].szSampleName + "】";
                }
            }
        }
        else
        {
            uint? uMax = 0;
            uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;
     
            if (GetMaxValue(ref uMax, uID, "dwSN"))
            {
                UNIADMIN setValue = new UNIADMIN();
                
                PutJSObj(setValue);
            }
            m_Title = "新建样品";
        }
	}
    
}