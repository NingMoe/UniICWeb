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
    protected string m_Title = "";
    protected string m_KindProperty = "";
    protected string m_dwKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVKIND setDevKind;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out setDevKind);
            UNIDEVCLS setDevClass = new UNIDEVCLS();
            {//先新建devClass
                setDevClass.dwClassID = setDevKind.dwClassID;
                setDevClass.dwKind = setDevKind.dwClassKind;
                setDevClass.szClassName = setDevKind.szKindName;
                REQUESTCODE uRes = m_Request.Device.DevClsSet(setDevClass, out setDevClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage,"修改" + ConfigConst.GCKindName + "失败", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
            setDevKind.dwProperty = CharListToUint(Request["dwProperty"]);
            setDevKind.dwClassID = setDevClass.dwClassID;
            setDevKind.szClassName = setDevClass.szClassName;
            if (m_Request.Device.DevKindSet(setDevKind, out setDevKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_KindProperty = GetAllInputHtml(CONSTHTML.checkBox, "dwProperty", "DevKind_dwProperty");
        m_dwKind = GetAllInputHtml(CONSTHTML.option, "", "DevClass_dwKind");
        if (Request["op"] == "set")
        {
            UNIDEVKIND devKind;
            if(GetDevKindByID(Request["id"],out devKind))
            {
               PutHTTPObj(devKind);
            }
          }
        else
        {
            m_Title = "修改" + ConfigConst.GCKindName;
        }
    }

}
