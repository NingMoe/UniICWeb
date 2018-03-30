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
    protected string m_KindProperty = "";
    protected string m_dwClsKind= "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVCLS newDevCls;

      //  m_dwClsKind = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwKind", "DevClass_dwKind", true);

        if (IsPostBack)
        {
            GetHTTPObj(out newDevCls);
            string isLease = Request["isLease"];
            if (isLease != null && isLease == "1")
            {
                newDevCls.dwKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
            }
            if (m_Request.Device.DevClsSet(newDevCls, out newDevCls) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCClassName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCClassName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCClassName;
        }
    }

}
