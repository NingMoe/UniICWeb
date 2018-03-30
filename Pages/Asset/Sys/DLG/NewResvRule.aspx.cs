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
    protected string m_szIdent = "";
    protected string m_szDevCLS = "";
    protected string m_szDev ="";
    protected string m_szResvPurpose= "";
    protected string m_Priority = "";
    protected string m_Limit= "";
    protected string m_szDept = "";
    protected uint uResvFor = 2;//1devid
    protected string m_szGroup = "";
    protected string m_szDevice = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIRESVRULE newResvRule;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        UNIGROUP[] groupList = GetGroupByKind((uint)UNIGROUP.DWKIND.GROUPKIND_USER);
        m_szGroup += GetInputItemHtml(CONSTHTML.option, "","全部", "0");
        if (groupList != null && groupList.Length>0)
        {
            for (int i = 0; i < groupList.Length; i++)
            {
                m_szGroup += GetInputItemHtml(CONSTHTML.option, "", groupList[i].szName.ToString(), groupList[i].dwGroupID.ToString());
            }
        }
        UNIDEVICE[] vtDevList;
        m_szDevice += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        if (GetAllDev(out vtDevList) && vtDevList != null && vtDevList.Length > 0)
        {
            for (int i = 0; i < vtDevList.Length; i++)
            {
                m_szDevice += GetInputItemHtml(CONSTHTML.option, "", vtDevList[i].szDevName.ToString() + "(" + vtDevList[i].szLabName+","+vtDevList[i].szModel +")", vtDevList[i].dwDevID.ToString());
            }
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newResvRule);
            newResvRule.dwLatestResvTime = 24 * 60 * newResvRule.dwLatestResvTime;
            newResvRule.dwEarliestResvTime = 24 * 60 * newResvRule.dwEarliestResvTime;
            newResvRule.dwLimit = CharListToUint(Request["dwLimit"]);
            if (uResvFor == 1)
            {
                if (((uint)newResvRule.dwLimit & ((uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEV)) <= 0)
                {
                    newResvRule.dwLimit = (uint)newResvRule.dwLimit | (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEV;
                }
            }
            else if (uResvFor == 2)
            {
                if (((uint)newResvRule.dwLimit & ((uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEVKIND)) <= 0)
                {
                    newResvRule.dwLimit = (uint)newResvRule.dwLimit;
                }
            }
            if ((uint)newResvRule.dwGroupID != 0)
            {
                int nDeptID = -1;
                newResvRule.dwDeptID = 0xFFFFFFFF;
            }
            if (newResvRule.dwDevID != null && ((uint)newResvRule.dwDevID != 0))
            {
                newResvRule.dwDevKind = 0;
            }
            if (m_Request.Reserve.ResvRuleSet(newResvRule, out newResvRule) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szIdent = GetAllInputHtml(CONSTHTML.option, "", "Ident");
        m_szResvPurpose = GetAllInputHtml(CONSTHTML.radioButton, "dwResvPurpose", "ResvPurpose");
        m_Limit = GetAllInputHtml(CONSTHTML.checkBox, "dwLimit", "ResvRule_Limit");
        UNIDEVCLS[] vtDevCls = GetDevCLS(0);
        if (vtDevCls != null && vtDevCls.Length > 0)
        {
            m_szDevCLS += "<option value='" + "0" + "'>" + "全部适用" + "</option>";
            for (int i = 0; i < vtDevCls.Length; i++)
            {
                m_szDevCLS += GetInputItemHtml(CONSTHTML.option, "", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
            }
        }
        UNIDEPT[] dept = GetAllDept();
        m_szDept += "<option value='" + "0" + "'>" + "全部适用" + "</option>";
        if (dept != null)
        {
            for (int i = 0; i < dept.Length; i++)
            {
                m_szDept += GetInputItemHtml(CONSTHTML.option, "", dept[i].szName.ToString(), dept[i].dwID.ToString());
            }
        }
        m_Priority = GetAllInputHtml(CONSTHTML.option, "", "Priority");
        if (Request["op"] == "set")
        {
            bSet = true;

            RESVRULEREQ vrResvRuleReq = new RESVRULEREQ();

            vrResvRuleReq.dwRuleSN = Parse(Request["id"]);
            UNIRESVRULE[] vtResvRule;
            if (m_Request.Reserve.ResvRuleGet(vrResvRuleReq, out vtResvRule) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtResvRule.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtResvRule[0]);
                    m_Title = "修改预约规则【" + vtResvRule[0].szRuleName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建预约规则";

        }
    }
}
