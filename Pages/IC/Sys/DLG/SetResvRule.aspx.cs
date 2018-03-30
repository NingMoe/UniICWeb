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
    protected uint uResvFor = 2;//1devid
    protected string m_szGroup = "";
    protected string USEFORSYS = "";
    protected string USEFORSYSTime = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        uint uKind = Parse(Request["kind"]);
        uint usyskind = (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT + (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC + (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM - uKind;
        UNIRESVRULE newResvRule;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }

        USEFORSYS = GetInputHtml(usyskind, CONSTHTML.checkBox, "dwTimeLimitForPurpose", "USEFORSYS");
        USEFORSYSTime = GetInputHtml(usyskind, CONSTHTML.checkBox, "dwTimeConflictForPurpose", "USEFORSYS");
        UNIGROUP[] groupList = GetGroupByKind((uint)UNIGROUP.DWKIND.GROUPKIND_USER);
        m_szGroup += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; groupList != null && i < groupList.Length; i++)
        {
            m_szGroup += GetInputItemHtml(CONSTHTML.option, "", groupList[i].szName.ToString(), groupList[i].dwGroupID.ToString());
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newResvRule);
            newResvRule.dwTimeLimitForPurpose = CharListToUint(Request["dwTimeLimitForPurpose"]);
            newResvRule.dwTimeLimitForPurpose = newResvRule.dwTimeLimitForPurpose | uKind;

            newResvRule.dwTimeConflictForPurpose = CharListToUint(Request["dwTimeConflictForPurpose"]);
            newResvRule.dwLatestResvTime = 60 * newResvRule.dwLatestResvTime + Parse(Request["dwLatestResvTimeMin"]);
            newResvRule.dwEarliestResvTime = 60 * newResvRule.dwEarliestResvTime + Parse(Request["dwEarliestResvTimeMin"]);
            newResvRule.dwLimit = CharListToUint(Request["dwLimit"]);
            newResvRule.dwLimit = ((uint)newResvRule.dwLimit);
           
                newResvRule.dwLimit = (uint)newResvRule.dwLimit;
                CHECKTYPEREQ checkTypeGet = new CHECKTYPEREQ();
                checkTypeGet.dwMainKind = (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN;
                CHECKTYPE[] vtCheckType;
                if (m_Request.Admin.CheckTypeGet(checkTypeGet, out vtCheckType) == REQUESTCODE.EXECUTE_SUCCESS && vtCheckType != null && vtCheckType.Length > 0)
                {
                    newResvRule.CheckTbl = new RULECHECKINFO[1];
                    newResvRule.CheckTbl[0].dwBeforeKind = 0;
                    newResvRule.CheckTbl[0].dwCheckKind = vtCheckType[0].dwCheckKind;
                    newResvRule.CheckTbl[0].dwCheckLevel = vtCheckType[0].dwCheckLevel;
                    newResvRule.CheckTbl[0].dwDeptID = vtCheckType[0].dwDeptID;
                    newResvRule.CheckTbl[0].dwMainKind = vtCheckType[0].dwMainKind;
                    newResvRule.CheckTbl[0].szCheckName = vtCheckType[0].szCheckName;
                    
                    if ((newResvRule.dwLimit & 1073741824) > 0)//需要审核0x40000000
                    {
                        newResvRule.CheckTbl[0].dwProperty = (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN;
                    }
                    else
                    {
                        newResvRule.CheckTbl[0].dwProperty = 0;// (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN;
                    }
                }
          
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
            newResvRule.dwResvPurpose = CharListToUint(Request["dwResvPurpose"]);
            newResvRule.dwResvPurpose = (uint)newResvRule.dwResvPurpose| uKind;
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
       
        m_szIdent = GetAllInputHtml(CONSTHTML.option, "", "Ident");
       
        if (uKind == 0)
        {
            m_szResvPurpose = GetAllInputHtml(CONSTHTML.checkBox, "dwResvPurpose", "ResvPurpose");
        }
        else
        {
            m_szResvPurpose = GetAllInputHtml(CONSTHTML.checkBox, "dwResvPurpose", "ResvPurposeSubSys");
        }

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
                    ViewState["dwTimeLimitForPurpose"] = vtResvRule[0].dwTimeLimitForPurpose.ToString();
                    ViewState["dwTimeConflictForPurpose"] = vtResvRule[0].dwTimeConflictForPurpose.ToString();
                    
                    PutMemberValue("kind", uKind.ToString());
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
            PutMemberValue("dwLatestResvTime", (Parse(ViewState["dwLatestResvTime"].ToString()) / 60).ToString());
            PutMemberValue("dwEarliestResvTime", (Parse(ViewState["dwEarliestResvTime"].ToString()) /60).ToString());

            PutMemberValue("dwLatestResvTimeMin", (Parse(ViewState["dwLatestResvTime"].ToString())% 60).ToString());
            PutMemberValue("dwEarliestResvTimeMin", (Parse(ViewState["dwEarliestResvTime"].ToString()) % 60).ToString());
        }
        if (ViewState["kindName"] != null )
        {
            PutMemberValue("szDevKindName", ViewState["kindName"].ToString());
        }
        if (ViewState["dwTimeLimitForPurpose"] != null)
        {
            PutMemberValue("dwTimeLimitForPurpose", ViewState["dwTimeLimitForPurpose"].ToString());
        }
        if (ViewState["dwTimeConflictForPurpose"] != null)
        {
            PutMemberValue("dwTimeConflictForPurpose", ViewState["dwTimeConflictForPurpose"].ToString());
        }
    }
}
