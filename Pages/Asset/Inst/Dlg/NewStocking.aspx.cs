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

        if (!IsPostBack)
        {
            //dwSTDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }
        else {
            STOCKTAKING newStocking = new STOCKTAKING();
            GetHTTPObj(out newStocking);
           // newStocking.dwSTDate = GetDate(Request["dwSTDate"]);
           // newStocking.dwLeaderID = newStocking.dwAttendantID;
            //newStocking.szLeaderName = newStocking.szAttendantName;
            newStocking.dwSTStat = (uint)STOCKTAKING.DWSTSTAT.STSTAT_DOING;
            if (m_Request.Assert.StockTakingDo(newStocking, out newStocking) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("制定盘点计划成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "制定盘点计划失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }
        m_Title = "制定盘点计划";
	}
   
}