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
        if (IsPostBack)
        {
            REPAIRAPPLY setValue;
            
            GetHTTPObj(out setValue);
            setValue.dwDevID = Parse(Request["dwDevID"]);
           // setValue.dwStatus = (uint)DEVDAMAGEREC.DWSTATUS.REPARE_WAIT;
            setValue.dwDamageDate = GetDate(DateTime.Now.ToString("yyyy-MM-dd"));
            setValue.szDamageInfo = "感应器坏了";
            if (m_Request.Assert.RepareApply(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "报修失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("报修成功", "报修成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }


        if (Request["op"] == "set")
        {
           
        }
        else
        {
            m_Title = ConfigConst.GCDevName+"报修";
        }
	}
}
