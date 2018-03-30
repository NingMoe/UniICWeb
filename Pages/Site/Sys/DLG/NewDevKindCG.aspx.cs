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
    protected string m_dwDevClass = "";
    protected string m_szOP = "新建";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVKIND newDevKind;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (Request["op"] != null && Request["op"] == "set")
        {
            m_szOP = "修改";
        }
        int uNew = ConfigConst.GCKindAndClass;
        if (IsPostBack)
        {
            GetHTTPObj(out newDevKind);
            UNIDEVCLS newDevClass = new UNIDEVCLS();

            if (uNew == 1)
            {
                /*
                newDevClass.dwKind = newDevKind.dwClassKind;
                newDevClass.szClassName = newDevKind.szKindName;
                REQUESTCODE uRes = m_Request.Device.DevClsSet(newDevClass, out newDevClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_szOP + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);                  
                }
                else
                {
                    newDevKind.dwClassID = newDevClass.dwClassID;
                    newDevKind.szClassName = newDevClass.szClassName;
                }
                 * */
            }

            UNIDEVCLS[] vtDevclass = GetAllDevCls();
            if (vtDevclass != null && vtDevclass.Length > 0)
            {
                newDevKind.dwClassID = vtDevclass[0].dwClassID;
            }
            newDevKind.dwProperty = CharListToUint(Request["dwProperty"]);
            newDevKind.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
            if (m_Request.Device.DevKindSet(newDevKind, out newDevKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, m_szOP + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(m_szOP + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        UNIDEVCLS[] vtDevCls;
        if(Request["dwClassKind"]!=null)
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
            UNIDEVKIND setKind;
            GetDevKindByID((Request["id"]), out setKind);
            if (setKind.dwKindID == null)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
               
                {
                    PutJSObj(setKind);
                    m_Title = "修改【" + setKind.szKindName + "】";
                }
            }
        }
        else
        {
            m_Title = m_szOP + ConfigConst.GCKindName;
        }
    }

}
