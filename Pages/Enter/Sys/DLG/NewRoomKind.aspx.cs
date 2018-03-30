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
    protected string m_dwDevClass= "";
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
           
            if(uNew==1)
            {
                newDevClass.dwKind = newDevKind.dwClassKind;
                newDevClass.szClassName = newDevKind.szKindName;
                string szIsOpen = Request["isOpen"];
                if (szIsOpen != null && szIsOpen == "1")
                {
                    newDevClass.szMemo = "false";
                }
                else
                {
                    newDevClass.szMemo = "";
                }
                if (((uint)newDevKind.dwMinUsers) > 1)
                {
                    newDevClass.dwKind = (uint)UNIDEVCLS.DWKIND.CLSCOMMONS_MULTIPLE;
                }
                REQUESTCODE uRes = m_Request.Device.DevClsSet(newDevClass, out newDevClass);
               
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("新建" +ConfigConst.GCSysKindRoom+ ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);                    
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
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCSysKindRoom + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCSysKindRoom + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        UNIDEVCLS[] vtDevCls = GetDevClsByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
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
            bSet = true;
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCSysKindRoom + ConfigConst.GCKindName;
        }
    }

}
