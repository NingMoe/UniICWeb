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
    protected string m_szCtrlMode = "";
    protected string m_szLab = "";
    protected string m_szManager = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szPorperty = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        UNIHOLIDAY newHoliday;
       
        if (IsPostBack)
        {
            GetHTTPObj(out newHoliday);
            newHoliday.dwStartDay = GetDate(Request["dwStartDay"]);
            newHoliday.dwEndDay = GetDate(Request["dwEndDay"]);
            if (m_Request.Admin.HolidDaySet(newHoliday) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置节假日失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置节假日成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);                             
                return;
            }
        }                    
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        if (Request["op"] == "set")
        {
            bSet = true;

            HOLIDAYREQ vrDevReq = new HOLIDAYREQ();
            vrDevReq.szName = (Request["id"]);
            UNIHOLIDAY[] vtDev;
            if (m_Request.Admin.HolidDayGet(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
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
                   
                }
            }
            m_Title = "修改节假日";
        }
        else
        {
            m_Title = "新建节假日";

        }                      
	}  
}
