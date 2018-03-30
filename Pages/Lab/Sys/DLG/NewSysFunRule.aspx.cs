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
    protected string m_SFSN = "";
    protected string m_szLab = "";
    protected string m_szAuthType = "";
    protected string m_szAuthModule = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        SYSFUNCRULE setValue;
        string szop1 = "新建";
        if (Request["op"] == "set")
        {
            szop1 = "修改";
        }
        if (IsPostBack)
        {
            GetHTTPObj(out setValue);
            setValue.dwScopeID = setValue.dwLabID;
            SYSFUNCREQ vrFunGet1 = new SYSFUNCREQ();
            vrFunGet1.dwSFSN= setValue.dwSFSN;
            SYSFUNC[] vtFunRes1;
            if (m_Request.System.SysFuncGet(vrFunGet1, out vtFunRes1) == REQUESTCODE.EXECUTE_SUCCESS && vtFunRes1 != null)
            {
                setValue.szSFName = vtFunRes1[0].szSFName;
            }
            if (m_Request.System.SysFuncRuleSet(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szop1+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szop1+"成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szAuthType = GetInputHtmlFromXml(0, CONSTHTML.option, "", "AuthType", true);

        m_szAuthModule = GetInputHtmlFromXml(0, CONSTHTML.option, "", "AuthModule", true);
        UNILAB[] vtLab=GetAllLab();
        for (int i = 0; i < vtLab.Length; i++)
        {
            m_szLab += GetInputItemHtml(CONSTHTML.option, "", vtLab[i].szLabName, vtLab[i].dwLabID.ToString());
        }
        SYSFUNCREQ vrFunGet = new SYSFUNCREQ();
        SYSFUNC[] vtFunRes;
       
        if (m_Request.System.SysFuncGet(vrFunGet, out vtFunRes) == REQUESTCODE.EXECUTE_SUCCESS && vtFunRes!=null)
        {
            for (int i = 0; i < vtFunRes.Length; i++)
            {
                m_SFSN += GetInputItemHtml(CONSTHTML.option, "", vtFunRes[i].szSFName, vtFunRes[i].dwSFSN.ToString());
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            SYSFUNCRULEREQ vrParameter = new SYSFUNCRULEREQ();
            vrParameter.dwSFRuleID = Parse(Request["id"]);
            SYSFUNCRULE[] vrResult;
            if (m_Request.System.SysFuncRuleGet(vrParameter, out vrResult) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrResult.Length==0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrResult[0]);
                    m_Title = "修改【" + vrResult[0].szSFRuleName+ "】";
                }
            }
        }
        else
        {
            m_Title = "新建课程";

        }
    }
}
