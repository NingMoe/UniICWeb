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
    protected uint uResvFor = 2;//1devid
    protected string m_szGroup = "";
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
        if (IsPostBack)
        {
            GetHTTPObj(out newResvRule);
            newResvRule.dwLatestResvTime =  60 * newResvRule.dwLatestResvTime;
            newResvRule.dwEarliestResvTime =  60 * newResvRule.dwEarliestResvTime;
            newResvRule.dwLimit = CharListToUint(Request["dwLimit"]);
            if((newResvRule.dwLimit&1073741824)>0)//需要审核
            {
                newResvRule.dwLimit = (uint)newResvRule.dwLimit;
                CHECKTYPEREQ checkTypeGet = new CHECKTYPEREQ();
                checkTypeGet.dwMainKind = (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN;
                CHECKTYPE[] vtCheckType;
                if(m_Request.Admin.CheckTypeGet(checkTypeGet, out vtCheckType)==REQUESTCODE.EXECUTE_SUCCESS&&vtCheckType!=null&&vtCheckType.Length>0)
                {
                    newResvRule.CheckTbl = new RULECHECKINFO[1];
                    newResvRule.CheckTbl[0].dwBeforeKind = 0;
                    newResvRule.CheckTbl[0].dwCheckKind = vtCheckType[0].dwCheckKind;
                    newResvRule.CheckTbl[0].dwCheckLevel = vtCheckType[0].dwCheckLevel;
                    newResvRule.CheckTbl[0].dwDeptID = vtCheckType[0].dwDeptID;
                    newResvRule.CheckTbl[0].dwMainKind = vtCheckType[0].dwMainKind;
                    newResvRule.CheckTbl[0].szCheckName = vtCheckType[0].szCheckName;
                    newResvRule.CheckTbl[0].dwProperty = (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN;
                   // newResvRule.CheckTbl[0].dwProperty = vtCheckType[0].dwProperty;
                }
               

            }
            if (uResvFor == 1)
            {
                if (((uint)newResvRule.dwLimit & ((uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEV)) <= 0)
                {
                    newResvRule.dwLimit = (uint)newResvRule.dwLimit | (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEV;
                }
            }
            else if (uResvFor==2)
            {
                if (((uint)newResvRule.dwLimit & ((uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEVKIND)) <= 0)
                {
                    newResvRule.dwLimit = (uint)newResvRule.dwLimit;
                }
            }
            newResvRule.dwResvPurpose = CharListToUint(Request["dwResvPurpose"]);
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
        m_szResvPurpose = GetAllInputHtml(CONSTHTML.checkBox, "dwResvPurpose", "ResvPurpose");
        m_Limit = GetAllInputHtml(CONSTHTML.checkBox, "dwLimit", "ResvRule_Limit");
        UNIDEVCLS[] vtDevCls = GetDevCLS(0);
        if (vtDevCls != null && vtDevCls.Length > 0)
        {
            m_szDevCLS += "<option value='" + "0" + "'>" + "全部适用" + "</option>";
            for (int i = 0; i < vtDevCls.Length; i++)
            {
                m_szDevCLS += GetInputItemHtml(CONSTHTML.option,"",vtDevCls[i].szClassName,vtDevCls[i].dwClassID.ToString());
            }
        }       
        m_Priority = GetAllInputHtml(CONSTHTML.option, "", "Priority");
        if (Request["op"] == "set")
        {
            bSet = true;

            RESVRULEREQ vrResvRuleReq = new RESVRULEREQ();

            vrResvRuleReq.dwRuleSN =Parse(Request["id"]);
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
