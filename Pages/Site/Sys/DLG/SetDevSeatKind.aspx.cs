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
using UniLibrary;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "修改" + ConfigConst.GCKindName;
    protected string m_KindProperty = "";
    protected string m_dwKind = "";
    protected string m_dwDevClass = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVKIND newDevKind;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        int uNew = ConfigConst.GCKindAndClass;
        if (IsPostBack)
        {
            GetHTTPObj(out newDevKind);
            UNIDEVCLS newDevClass = new UNIDEVCLS();

            if (uNew == 1)
            {
                newDevClass.dwClassID = newDevKind.dwClassID;
                newDevClass.dwKind = newDevKind.dwClassKind;
                newDevClass.szClassName = newDevKind.szKindName;
                REQUESTCODE uRes = m_Request.Device.DevClsSet(newDevClass, out newDevClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
                else
                {
                    newDevKind.dwClassID = newDevClass.dwClassID;
                    newDevKind.szClassName = newDevClass.szClassName;
                }
            }

            newDevKind.dwProperty = CharListToUint(Request["dwProperty"]);

            if (m_Request.Device.DevKindSet(newDevKind, out newDevKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改" +  ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        UNIDEVCLS[] vtDevCls;
        if (Request["dwClassKind"] != null)
        {
            vtDevCls = GetDevClsByKind(Parse(Request["dwClassKind"]));
        }
        else
        {
            vtDevCls = new UNIDEVCLS[0];
        }
        if (uNew == 0)
        {
            if (vtDevCls != null && vtDevCls.Length > 0)
            {
                for (int i = 0; i < vtDevCls.Length; i++)
                {
                    m_dwDevClass += GetInputItemHtml(CONSTHTML.option, "", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
                }

            }

        }
        m_KindProperty = GetAllInputHtml(CONSTHTML.checkBox, "dwProperty", "DevKind_dwProperty");
        m_dwKind = GetAllInputHtml(CONSTHTML.option, "", "DevClass_dwKind");
        if (Request["op"] == "set")
        {

            UNIDEVKIND devKind;
            if (GetDevKindByID(Request["id"], out devKind))
            {
                PutHTTPObj(devKind);
            }
            bSet = true;
        }
        else
        {
            m_Title = "修改" + ConfigConst.GCKindName;
        }
    }


}
