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
    protected string m_szCheckTypeKind = "";
    protected string m_Property= "";
    protected string m_szCheckTypeMainKind = "";
    protected string m_level = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szOP = "新建";
        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        CHECKTYPE newCheckType;
       
        if (IsPostBack)
        {
            GetHTTPObj(out newCheckType);
            if(((uint)newCheckType.dwMainKind)==(uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN)
            {
                newCheckType.dwCheckLevel = (uint)UNIADMIN.DWMANLEVEL.MANLEVEL_DEPT;
            }
            else if (((uint)newCheckType.dwMainKind) == (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR)
            {
                if (newCheckType.dwDeptID != null && newCheckType.dwDeptID != 0)
                {
                    newCheckType.dwCheckLevel = (uint)UNIADMIN.DWMANLEVEL.MANLEVEL_SCHOOL;
                }
                else
                {
                    newCheckType.dwCheckLevel = (uint)UNIADMIN.DWMANLEVEL.MANLEVEL_DEPT;
                    newCheckType.szDeptName = "";
                }
            }
            else if (((uint)newCheckType.dwMainKind) == (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_PUBLICITY)
            {
                newCheckType.dwCheckLevel = (uint)UNIADMIN.DWMANLEVEL.MANLEVEL_SCHOOL;
            }
            else if (((uint)newCheckType.dwMainKind) == (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SECURITY)
            {
                newCheckType.dwCheckLevel = (uint)UNIADMIN.DWMANLEVEL.MANLEVEL_SCHOOL;
            }

            if (m_Request.Admin.CheckTypeSet(newCheckType) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOP + "审核类型失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOP + "审核类型成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szCheckTypeKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "CheckType_Kind", true);
        m_szCheckTypeMainKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "CheckType_MainKind", true); 
        m_Property = GetInputHtmlFromXml(0, CONSTHTML.radioButton, "dwProperty", "CheckType_Property", true);
        m_level = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Yard_ActivityLevel", true);
        if (Request["op"] == "set")
        {
            bSet = true;

            CHECKTYPEREQ vrGet = new CHECKTYPEREQ();
            vrGet.dwCheckKind = Parse(Request["dwID"]);
            CHECKTYPE[] vtRes;
            if (m_Request.Admin.CheckTypeGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_Title = "修改【" + vtRes[0].szCheckName + "】";
                }
            }
        }
        else
        {
            m_Title = "审核类型";

        }
    }
}
