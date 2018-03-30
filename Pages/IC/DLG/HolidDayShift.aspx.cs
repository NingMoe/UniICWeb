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
        m_Title = "调课";
        if (!IsPostBack)
        {   
        }
        else if (Request["op"] == "set")
        {
            HOLIDAYSHIFT change = new HOLIDAYSHIFT();
            GetHTTPObj(out change);
            if (m_Request.Reserve.HolidayShift(change) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("调课成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
                return;
            }
            else {
                MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                return;
            }
        }
        
    }
}