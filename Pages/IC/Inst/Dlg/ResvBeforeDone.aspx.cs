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
    protected string m_szSta = "";
	protected void Page_Load(object sender, EventArgs e)
	{        
        if (IsPostBack)
        {
            UNIRESERVE delResv = new UNIRESERVE();
            string szDelID = Request["delID"];
            delResv.dwResvID = Parse(szDelID);
            if (GetResvByID(szDelID, out delResv))
            {
                uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                uTimeNow = uTimeNow + 120;
                delResv.dwEndTime = uTimeNow;
                if (m_Request.Reserve.Set(delResv, out delResv) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    /*
                    string szDirt = Request["redit"];
                    if (szDirt == "1")
                    {

                    }
                    else
                    */
                    {
                        MessageBox("设置成功", "设置成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        return;
                    }
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
           
           
        }

        
        else
        {
            m_Title = "确定提前结束预约？";
        }
	}
}
