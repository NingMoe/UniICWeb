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
    protected string m_szDept = "";
    protected string m_szLabKind = "";
    protected string m_szLabClass = "";
    protected string m_Campu = "";
	protected void Page_Load(object sender, EventArgs e)
    {
         if (IsPostBack)
        {
            UNIDEPT setDept;
            GetHTTPObj(out setDept);
            if (m_Request.Account.DeptSet(setDept, out setDept) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }

      
        if (Request["op"] == "set")
        {
            bSet = true;
            DEPTREQ vrParameter = new DEPTREQ();
            vrParameter.dwID = Parse(Request["dwID"]);
            UNIDEPT[] vrResult;
            //vrParameter.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYALL;
            //vrParameter.dwKind = (uint)ConfigConst.GCDeptKind;
            if (m_Request.Account.DeptGet(vrParameter, out vrResult) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrResult.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrResult[0]);
                    m_Title = "修改" + ConfigConst.GCLabName + "" + "【" + vrResult[0].szName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCLabName;

        }
    }
}
