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
    protected string m_dwKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVKIND newDevKind;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newDevKind);
            UNIDEVCLS newDevClass = new UNIDEVCLS();
            {//先新建devClass
              
                newDevClass.dwKind = newDevKind.dwClassKind;
                newDevClass.szClassName = newDevKind.szKindName;
                REQUESTCODE uRes = m_Request.Device.DevClsSet(newDevClass, out newDevClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("新建" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
            newDevKind.dwProperty = CharListToUint(Request["dwProperty"]);
            newDevKind.dwClassID = newDevClass.dwClassID;
            newDevKind.szClassName = newDevClass.szClassName;
            if (m_Request.Device.DevKindSet(newDevKind, out newDevKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_KindProperty = GetAllInputHtml(CONSTHTML.checkBox, "dwProperty", "DevKind_dwProperty");
        m_dwKind = GetAllInputHtml(CONSTHTML.option, "", "DevClass_dwKind");
        if (Request["op"] == "set")
        {
            bSet = true;
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCKindName;
        }
    }

}
