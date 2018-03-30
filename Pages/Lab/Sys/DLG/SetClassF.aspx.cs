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
    protected string m_dwCtrlMode = "";
    protected string m_dwForAges = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNICTRLCLASS setClass;
        uint? uMax = 0;
        uint uID = PRControl.CONTROL_BASE | PRControl.MSREQ_CTRLCLASS_SET;
        if (GetMaxValue(ref uMax, uID, "dwCtrlSN"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out setClass);
            UNICTRLCLASS setCtrlClass = new UNICTRLCLASS();
            {//先新建
                GetHTTPObj(out setCtrlClass);
                if (Request["type"] == "url")
                {
                    setCtrlClass.dwCtrlKind = (uint)UNICTRLCLASS.DWCTRLKIND.CTRLKIND_URL;
                }
                else
                {
                    setCtrlClass.dwCtrlKind = (uint)UNICTRLCLASS.DWCTRLKIND.CTRLKIND_SW;
                }
              
                REQUESTCODE uRes = m_Request.Control.SetCtrlClass(setCtrlClass, out setCtrlClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "设置" + "黑名单" + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("设置" + "黑名单" + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
      
        }
        m_dwForAges = GetAllInputHtml(CONSTHTML.option, "", "CtrlClass_dwForAges");
        m_dwCtrlMode = GetAllInputHtml(CONSTHTML.option, "", "CtrlClass_dwCtrlMode");
        if (Request["op"] == "set")
        {
            UNICTRLCLASS CtrlClass;
            if (GetUrlCtrlClassByID(Request["id"], out CtrlClass))
            {
                PutHTTPObj(CtrlClass);
            }
        }
        else
        {
            m_Title = "设置" + "黑名单";
        }
    }

    public bool GetUrlCtrlClassByID(string szID, out UNICTRLCLASS Class)
    {
        Class = new UNICTRLCLASS();
        CTRLCLASSREQ vrClassGet = new CTRLCLASSREQ();
        vrClassGet.dwCtrlSN = ToUint(szID);
        vrClassGet.dwCtrlKind = (uint)UNICTRLCLASS.DWCTRLKIND.CTRLKIND_SW;
        UNICTRLCLASS[] vtClass;
        REQUESTCODE uRes = m_Request.Control.GetCtrlClass(vrClassGet, out vtClass);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtClass != null && vtClass.Length > 0)
        {
            Class = vtClass[0];
            return true;
        }
        return false;
    }
}
