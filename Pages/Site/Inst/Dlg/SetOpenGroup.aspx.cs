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
    protected string m_szSta = "";
    protected string szIdent = "";
    protected string szGroupMember = "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            uint uGroup = Parse(Request["dwGroupID"]);
            GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
            vrGet.dwGroupID =uGroup;
            GROUPMEMDETAIL[] groupMemberList;
            if (m_Request.Group.GetGroupMemDetail(vrGet, out groupMemberList) == REQUESTCODE.EXECUTE_SUCCESS && groupMemberList != null && groupMemberList.Length > 0)
            {
                DEVOPENRULEREQ vrRuleGet = new DEVOPENRULEREQ();
                vrRuleGet.dwRuleSN = Parse(Request["id"]);
                DEVOPENRULE[] vtRes;
                if (m_Request.Device.DevOpenRuleGet(vrRuleGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
                {
                    DEVOPENRULE setValue = new DEVOPENRULE();
                    setValue = vtRes[0];
                    setValue.GroupOpenRule = null;
                    CHANGEGROUPOPENRULE delOpenRule = new CHANGEGROUPOPENRULE();
                    delOpenRule.dwRuleSN = setValue.dwRuleSN;
                    delOpenRule.dwOldGroupID = 0;
                    delOpenRule.dwGroupID = uGroup;

                    if (m_Request.Device.GroupOpenRuleDel(delOpenRule) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        setValue.GroupOpenRule = new GROUPOPENRULE[1];
                        setValue.GroupOpenRule[0] = new GROUPOPENRULE();
                        GROUPOPENRULE groupOpenRule = (GROUPOPENRULE)Session["groupOpenRuleSetMember"];
                        setValue.GroupOpenRule[0] = groupOpenRule;
                        if (m_Request.Device.DevOpenRuleSet(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                            return;
                        }
                        else
                        {
                            MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                            return;
                        }
                       
                    }else
                    {
                         MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                            return;
                    }
                    /*
                       
                     * */
                }
                   
                }

            else {
                 DEVOPENRULEREQ vrRuleGet = new DEVOPENRULEREQ();
                vrRuleGet.dwRuleSN = Parse(Request["id"]);
                DEVOPENRULE[] vtRes;
                if (m_Request.Device.DevOpenRuleGet(vrRuleGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
                {
                    DEVOPENRULE setValue = new DEVOPENRULE();
                    setValue = vtRes[0];
                    GROUPOPENRULE openruleNullGroup = setValue.GroupOpenRule[0];
                    setValue.GroupOpenRule = null;
                    CHANGEGROUPOPENRULE delOpenRule = new CHANGEGROUPOPENRULE();
                    delOpenRule.dwRuleSN = setValue.dwRuleSN;
                    delOpenRule.dwOldGroupID =uGroup;
                    delOpenRule.dwGroupID = 0;

                    if (m_Request.Device.GroupOpenRuleDel(delOpenRule) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        setValue.GroupOpenRule = new GROUPOPENRULE[1];
                        setValue.GroupOpenRule[0] = new GROUPOPENRULE();
                        UNIGROUP groupTemp = new UNIGROUP();
                        groupTemp.dwGroupID = 0;
                        openruleNullGroup.szGroup = groupTemp;
                        GROUPOPENRULE groupOpenRule = openruleNullGroup;
                        setValue.GroupOpenRule[0] = groupOpenRule;
                        if (m_Request.Device.DevOpenRuleSet(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                            return;
                        }
                        else
                        {
                            MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                            return;
                        }
                      
                    }
                    else
                    {
                        MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                        return;
                    }
                }
            }
          

        }
        if (Request["op"] == "set")
        {
            bSet = true;
            DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
            vrGet.dwRuleSN = Parse(Request["id"]);
            DEVOPENRULE[] vtRes;
            if (m_Request.Device.DevOpenRuleGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                DEVOPENRULE setGroupRule = new DEVOPENRULE();
                setGroupRule = vtRes[0];
                GROUPOPENRULE[] vtGroupOpenRule = setGroupRule.GroupOpenRule;
                if (vtGroupOpenRule != null && vtGroupOpenRule.Length > 0)
                {
                    bool IsGroup = false;
                    for (int i = 0; i < vtGroupOpenRule.Length; i++)
                    {
                        UNIGROUP group = new UNIGROUP();
                        group=vtGroupOpenRule[i].szGroup;
                        if (group.dwGroupID != null && ((uint)group.dwGroupID!=0))
                        {
                            PutMemberValue("dwGroupID", group.dwGroupID.ToString());
                          
                            IsGroup = true;
                            break;
                        }
                    }
                    if (!IsGroup)
                    {
                        UNIGROUP setGroup = new UNIGROUP();
                        if (NewGroup(setGroupRule.szRuleName + "开放规则组", (uint)UNIGROUP.DWKIND.GROUPKIND_OPENRULE, out setGroup))
                        {
                            GROUPOPENRULE setGroupOpenRule = new GROUPOPENRULE();
                            setGroupOpenRule= vtGroupOpenRule[0];
                            setGroupOpenRule.szGroup = new UNIGROUP();
                            setGroupOpenRule.szGroup = setGroup;
                            Session["groupOpenRuleSetMember"]=setGroupOpenRule;
                           // ViewState["groupOpenRule"] = setGroupOpenRule;
                            PutMemberValue("dwGroupID", setGroup.dwGroupID.ToString());
                        }
                    }
                }

            }
        }
        else
        {
            uint? uMax = 0;
            uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;

            if (GetMaxValue(ref uMax, uID, "dwSN"))
            {

            }
            m_Title = "新建管理员";
        }
	}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);        
        if(ViewState["szLogonName"]!=null&&ViewState["szLogonName"].ToString()!="")
        {
            PutMemberValue("szLogonNamePut", ViewState["szLogonName"].ToString());
        }
    }
    private string GetGroupMemberHtml(GROUPMEMBER[] groupMemberList)
    {
        string szRes="";
        if (groupMemberList == null || groupMemberList.Length == 0)
        {
            return szRes;
        }
        for (int i = 0; i < groupMemberList.Length; i++)
        {
            string szTemp = "<li class=\"ui-widget-content ui-corner-tr\">";
            szTemp += "<a id=\"" + groupMemberList[i].dwMemberID.ToString() + "\" kindid=\""+groupMemberList[i].dwKind.ToString()+"\" href=\"#\" title=\"删除\" class=\"ui-icon ui-icon-trash\"></a>";
            szTemp += "<label>" + groupMemberList[i].szName.ToString() + "</label></li>";
            szRes += szTemp;
        }
        return szRes;
    }

}
