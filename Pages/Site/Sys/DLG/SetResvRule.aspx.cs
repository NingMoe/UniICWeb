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
    protected string m_szDev = "";
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
        m_szGroup += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < groupList.Length; i++)
        {
            m_szGroup += GetInputItemHtml(CONSTHTML.option, "", groupList[i].szName.ToString(), groupList[i].dwGroupID.ToString());
        }
        UNIDEVICE[] vtDevList;
        m_szDevice += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        if (GetAllDev(out vtDevList) && vtDevList != null && vtDevList.Length > 0)
        {
            for (int i = 0; i < vtDevList.Length; i++)
            {
                m_szDevice += GetInputItemHtml(CONSTHTML.option, "", vtDevList[i].szDevName.ToString(), vtDevList[i].dwDevID.ToString());
            }
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newResvRule);
            
            newResvRule.dwLatestResvTime = 24 * 60 * newResvRule.dwLatestResvTime;
            newResvRule.dwEarliestResvTime = 24 * 60 * newResvRule.dwEarliestResvTime;
            newResvRule.dwLimit = CharListToUint(Request["dwLimit"]);
            newResvRule.dwLimit = ((uint)newResvRule.dwLimit);
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
                newResvRule.dwDeptID = (uint)nDeptID;
            }
            if (newResvRule.dwDevID != null && ((uint)newResvRule.dwDevID != 0))
            {
                newResvRule.dwDevKind = 0;
            }
            if (m_Request.Reserve.ResvRuleSet(newResvRule, out newResvRule) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
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
        UNIDEVICE[] vtDev;
        if (GetAllDev(out vtDev) == true)
        {
            m_szDev += "<option value='" + "0" + "'>" + "全部" + "</option>";
            for (int i = 0; i < vtDev.Length; i++)
            {
                m_szDev += GetInputItemHtml(CONSTHTML.option, "", vtDev[i].szDevName, vtDev[i].dwDevID.ToString());
            }
        }
        m_Priority = GetAllInputHtml(CONSTHTML.option, "", "Priority");
        if (Request["op"] == "set")
        {
            bSet = true;

            RESVRULEREQ vrResvRuleReq = new RESVRULEREQ();
            string szResvRuleID = Request["dwID"];
            string szDevID=Request["devID"];

            if(szResvRuleID!=null&&szResvRuleID!="")
            {
            vrResvRuleReq.dwRuleSN =Parse(szResvRuleID);
            }
            else if (szDevID != null && szDevID != "")
            {
                vrResvRuleReq.dwDevID = Parse(szDevID);
            }
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
                    ViewState["dwLatestResvTime"] = vtResvRule[0].dwLatestResvTime.ToString();
                    ViewState["dwEarliestResvTime"] = vtResvRule[0].dwEarliestResvTime.ToString();
                    if (uResvFor == 2)
                    {
                        UNIDEVKIND devKind;

                        if (vtResvRule[0].dwDevKind != null && GetDevKindByID(vtResvRule[0].dwDevKind.ToString(), out devKind))
                        {
                            ViewState["kindName"] = devKind.szKindName.ToString();
                        }
                        else {
                            ViewState["kindName"] = "全部";
                        }
                    }
                }
            }
        }
        else
        {
            m_Title = "修改预约规则";

        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (ViewState["dwLatestResvTime"] != null && ViewState["dwEarliestResvTime"] != null)
        {
            PutMemberValue("dwLatestResvTime", (Parse(ViewState["dwLatestResvTime"].ToString()) / 1440).ToString());
            PutMemberValue("dwEarliestResvTime", (Parse(ViewState["dwEarliestResvTime"].ToString()) / 1440).ToString());
        }
        if (ViewState["kindName"] != null )
        {
            PutMemberValue("szDevKindName", ViewState["kindName"].ToString());
        }        
    }
}
