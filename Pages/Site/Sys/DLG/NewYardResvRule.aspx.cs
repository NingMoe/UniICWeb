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
    protected string m_level = "";
    protected string m_szDevKind = "";

    protected string m_szIdent = "";
    protected string m_szDevCLS = "";
    protected string m_szDev = "";
    protected string m_szResvPurpose = "";
    protected string m_Priority = "";
    protected string m_Limit = "";
    protected string m_szDept = "";
    protected uint uResvFor = 2;//1devid
    protected string m_szGroup = "";
    protected string m_szDevice = "";
    protected string m_szYardActivity = "";

    protected string m_szExtCheckType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        string szOp = "新建";
        if (Request["op"] == "set")
        {
            szOp = "修改";
        }
        if (IsPostBack)
        {
            {
                UNIRESVRULE newResvRule;
                GetHTTPObj(out newResvRule);
                newResvRule.dwResvPurpose = Parse(Request["dwResvPurpose2"]);
                newResvRule.dwLatestResvTime = 24 * 60 * newResvRule.dwLatestResvTime;
                newResvRule.dwEarliestResvTime = 24 * 60 * newResvRule.dwEarliestResvTime;
                newResvRule.dwLimit = (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NOCONFLICTCHECK;// CharListToUint(Request["dwLimit"]);
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
                string ExtCheckType = Request["ExtCheckType"];
                if (ExtCheckType == null)
                {
                    ExtCheckType = "";
                }
                string[] checkTypeList = ExtCheckType.Split(',');
                int uLen = checkTypeList.Length;
                RULECHECKINFO[] checkInfo = new RULECHECKINFO[uLen];
                string szNoCheck=Request["noChencType"];
                szNoCheck = "," + szNoCheck + ",";
                for (int i = 0; i < uLen; i++)
                {
                    uint uCheckKind = CharListToUint(Request["ExtCheckType" + checkTypeList[i]]);
                    checkInfo[i].dwCheckKind = Parse(checkTypeList[i]);
                    checkInfo[i].dwBeforeKind = uCheckKind;
                    if (szNoCheck.IndexOf(","+checkTypeList[i]+",") > -1)
                    {
                        checkInfo[i].dwProperty = (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_SUB;
                    }
                    else {
                        checkInfo[i].dwProperty = (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN;
                    }
                }
                newResvRule.CheckTbl = null;
                newResvRule.dwPriority = 2;
                if (m_Request.Reserve.ResvRuleSet(newResvRule, out newResvRule) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    return;
                }
                newResvRule.CheckTbl = checkInfo;
                newResvRule.dwLimit = 0;
                newResvRule.dwPriority = Parse(Request["dwPriority"]);

                if (m_Request.Reserve.ResvRuleSet(newResvRule, out newResvRule) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    return;
                }
                MessageBox(szOp + "规则配置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
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
        UNIDEVKIND[] devKind = GetAllDevKind();
        if (devKind != null)
        {
            for (int i = 0; i < devKind.Length; i++)
            {
                m_szDevKind += GetInputItemHtml(CONSTHTML.checkBox, "szUsableKindIDs", devKind[i].szKindName, devKind[i].dwKindID.ToString());
            }
        }
        YARDACTIVITYREQ activityReq = new YARDACTIVITYREQ();
        YARDACTIVITY[] vtYardAcitivty;
        m_szYardActivity += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        if (m_Request.Reserve.GetYardActivity(activityReq, out vtYardAcitivty) == REQUESTCODE.EXECUTE_SUCCESS && vtYardAcitivty != null && vtYardAcitivty.Length > 0)
        {
            for (int i = 0; i < vtYardAcitivty.Length; i++)
            {
                m_szYardActivity += GetInputItemHtml(CONSTHTML.option, "", vtYardAcitivty[i].szActivityName, vtYardAcitivty[i].dwActivitySN.ToString());
            }
        }
        m_Property = GetInputHtmlFromXml(0, CONSTHTML.radioButton, "dwProperty", "CheckType_Property", true);
        m_level = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Yard_ActivityLevel", true);

        CHECKTYPEREQ checkTypeReq = new CHECKTYPEREQ();
        CHECKTYPE[] vtCheckType;
        if (m_Request.Admin.CheckTypeGet(checkTypeReq, out vtCheckType) == REQUESTCODE.EXECUTE_SUCCESS && vtCheckType != null && vtCheckType.Length > 0)
        {
            m_szExtCheckType += "<tr><td class='td' style='text-align:center;height:20px;' colspan='1'>需审核部门</td><td class='td'>是否审核</td><td style='text-align:center;height:20px' colspan='2' class='td'>所依赖审核部门</td></tr>";
            for (int i = 0; i < vtCheckType.Length; i++)
            {

                m_szExtCheckType += "<tr><td class='td' colspan='1' style='text-align:center;height:20px;width:180px;'>";
                m_szExtCheckType += GetInputItemHtml(CONSTHTML.checkBox, "ExtCheckType", vtCheckType[i].szCheckName, vtCheckType[i].dwCheckKind.ToString());
                m_szExtCheckType += "<td class='td' colspan='1' style='width:58px' >" + GetInputItemHtml(CONSTHTML.checkBox, "noChencType", "不审核", vtCheckType[i].dwCheckKind.ToString()) + "</td>";
                m_szExtCheckType += "</td>";
                m_szExtCheckType += "<td class='td' colspan='2' style='text-align:left;height:20px'>";
                for (int j = 0; j < vtCheckType.Length; j++)
                {
                  
                    if (vtCheckType[j].dwCheckKind != vtCheckType[i].dwCheckKind)
                    {
                        m_szExtCheckType += GetInputItemHtml(CONSTHTML.checkBox, "ExtCheckType" + vtCheckType[i].dwCheckKind, vtCheckType[j].szCheckName, vtCheckType[j].dwCheckKind.ToString());
                    }
                  
                }
                m_szExtCheckType += "</td>";
                m_szExtCheckType += "</tr>";
            }
        }
        if (Request["extValue"] != "")
        {
            PutMemberValue("dwExtValue", Request["extValue"]);
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            RESVRULEREQ vrResvRuleReq = new RESVRULEREQ();
            string szResvRuleID = Request["dwID"];
            string szDevID = Request["devID"];

            if (szResvRuleID != null && szResvRuleID != "")
            {
                vrResvRuleReq.dwRuleSN = Parse(szResvRuleID);
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
                    string szCheckKind="";
                    string szNoCheckKind = "";
                    if(vtResvRule[0].CheckTbl!=null)
                    {
                        for(int i=0;i<vtResvRule[0].CheckTbl.Length;i++)
                        {
                            szCheckKind+=vtResvRule[0].CheckTbl[i].dwCheckKind+",";
                            if ((vtResvRule[0].CheckTbl[i].dwProperty & (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_SUB) > 0)
                            {
                                szNoCheckKind += vtResvRule[0].CheckTbl[i].dwCheckKind + ",";
                            }
                            PutMemberValue("ExtCheckType" + vtResvRule[0].CheckTbl[i].dwCheckKind, vtResvRule[0].CheckTbl[i].dwBeforeKind.ToString());
                        }
                    }
                    PutMemberValue("ExtCheckType", szCheckKind);
                    PutMemberValue("noChencType", szNoCheckKind);
                    m_Title = "修改规则配置【" + vtResvRule[0].szRuleName + "】";
                    ViewState["dwLatestResvTime"] = vtResvRule[0].dwLatestResvTime.ToString();
                    ViewState["dwEarliestResvTime"] = vtResvRule[0].dwEarliestResvTime.ToString();
                    if (uResvFor == 2)
                    {
                        UNIDEVKIND devKind2;

                        if (vtResvRule[0].dwDevKind != null && GetDevKindByID(vtResvRule[0].dwDevKind.ToString(), out devKind2))
                        {
                            ViewState["kindName"] = devKind2.szKindName.ToString();
                        }
                        else
                        {
                            ViewState["kindName"] = "全部";
                        }
                    }
                }
            }
        }
        else
        {
            m_Title = szOp + "规则配置";

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
        if (ViewState["kindName"] != null)
        {
            PutMemberValue("szDevKindName", ViewState["kindName"].ToString());
        }
    }
}
