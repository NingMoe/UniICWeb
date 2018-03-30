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
using System.Collections.Specialized;

public partial class _Default : UniPage
{
    public string m_szTitle = "确认要删除实验计划吗?";

	protected void Page_Load(object sender, EventArgs e)
	{
        if (Request["IsSubmit"] == "true")
        {
            UNITESTPLAN vrParameter = new UNITESTPLAN();
            vrParameter.dwTestPlanID = ToUint(Request["id"]);
            if (m_Request.Reserve.DelTestPlan(vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("删除成功", "删除成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox("删除失败,"+m_Request.szErrMessage, "删除失败", MSGBOX.ERROR, MSGBOX_ACTION.OK);
            }
        }
	}
}
