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
    protected string m_szClassP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uType = Parse(Request["type"]);
        CTRLCLASSREQ vrCtrlassGet = new CTRLCLASSREQ();
        vrCtrlassGet.dwCtrlKind = (uint)(UNICTRLCLASS.DWCTRLKIND.CTRLKIND_URL);
        UNICTRLCLASS[] vtCtrlClass;
        if (m_Request.Control.GetCtrlClass(vrCtrlassGet, out vtCtrlClass) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtCtrlClass.Length; i++)
            {
                uint uMode=(uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_PERMIT;
                if (uType == 1)
                {
                    uMode = (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_FORBID;
                }
                if (((uint)vtCtrlClass[i].dwCtrlMode&uMode)!=uMode)
                {
                    continue;
                }
                m_szClassP += GetInputItemHtml(CONSTHTML.option, "", vtCtrlClass[i].szCtrlName, vtCtrlClass[i].dwCtrlSN.ToString());
            }
        }

        UNICTRLURL setUrl;
        uint? uMax = 0;
        if (IsPostBack)
        {
            GetHTTPObj(out setUrl);

           
            setUrl.dwStatus = (uint)UNICTRLURL.DWSTATUS.URLSTAT_CHECKED;

            REQUESTCODE uRes = m_Request.Control.SetCtrlURL(setUrl, out setUrl);
            if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置" + "网址" + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置" + "网址" + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
      
    }
}
