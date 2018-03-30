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
using System.Collections.Specialized;

public partial class _Default : UniPage
{
    public string m_szTitle = "添加实验计划";
    public string m_szGroupID = "";
    public string m_szGroupName = "";
    public bool m_CreateOK = false;
    public string m_szOKBtnText = "下一步";
    public string m_szCancelBtnText = "关闭";
    public string m_TermText = "";
    public string m_szTesteeKind = "";
    public bool bSet = false;
    public string m_szTestItemJSData = "[]";
    protected string m_TermList = "";
    protected string szAcademicSubject = "";
    protected string szTesteeKind = "";
    protected string szNextName = "";
    protected string szStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigConst.GCscheduleMode == 2)//新建计划同时新建项目
        {
            szNextName = "完成";
        }
        else {
            szNextName = "下一步";
        }

        szStatus += GetInputHtmlFromXml(0, CONSTHTML.option, "", "planStatus", true);
        uint uTermNow = 0;
        bSet = Request["op"] == "set";
        if (bSet)
        {
            m_szTitle = "修改实验计划";
            m_szOKBtnText = "确定";
            m_szCancelBtnText = "关闭";
        }
        if (string.IsNullOrEmpty(Request["Step"]) || Request["Step"] == "0")
        {
            m_CreateOK = false;
        }
        else
        {
            m_CreateOK = true;
        }
        UNITERM[] termList = GetAllTerm();
        if (termList != null)
        {
            for (int i = 0; i < termList.Length; i++)
            {
                m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                uint uYearTermState = (uint)termList[i].dwStatus;
                if ((uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    uTermNow = (uint)termList[i].dwYearTerm;

                }
            }
        }
        szAcademicSubject = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwAcademicSubject", true);
        szTesteeKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwTesteeKind", true);
        string szID = Request["id"];

        UNITESTPLAN vrParameter = new UNITESTPLAN();
        GetHTTPObj(out vrParameter);
        PutMemberValue("dwYearTerm", uTermNow.ToString());
        vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;

       
    }
}
