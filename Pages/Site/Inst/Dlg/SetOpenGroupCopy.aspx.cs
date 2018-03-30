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
    protected string szCamp = "";
    protected string szBuilding = "";

	protected void Page_Load(object sender, EventArgs e)
	{
        UNICAMPUS[] vtCamp = GetAllCampus();
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        szBuilding += GetInputItemHtml(CONSTHTML.option, "", "全部", "");
        for (int i = 0; i < vtBuilding.Length; i++)
        {

            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }
        if (IsPostBack)
        {
            uint uGroupid = Parse(Request["dwGroupID"]);
            if (uGroupid == 0)
            {
                MessageBox("找不到需要复制的成员", "复制失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            UNIGROUP[] setGroupList=GetGroupByID(uGroupid);
            if (setGroupList == null || setGroupList.Length == 0||setGroupList[0].szMembers==null||setGroupList[0].szMembers.Length==0)
            {
                MessageBox("复制成员数0", "复制成功", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            GROUPMEMBER[] groupMemberList = setGroupList[0].szMembers;
            string devidchk = Request["devidchk"];
            string[] openruleList = devidchk.Split(',');
            for (int k = 0; k < openruleList.Length; k++)
            {
                string szRuleSN = openruleList[k];
                GROUPOPENRULE setGroupOpenRule = new GROUPOPENRULE();

                uint uGroupIDValue = 0;
                DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
                vrGet.dwRuleSN = Parse(szRuleSN);
                DEVOPENRULE[] vtRes;
                if (!(m_Request.Device.DevOpenRuleGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0))
                {
                    continue;
                }
                else
                {
                    DEVOPENRULE setGroupRule = new DEVOPENRULE();
                    setGroupRule = vtRes[0];
                    GROUPOPENRULE[] vtGroupOpenRule = setGroupRule.GroupOpenRule;

                    if (vtGroupOpenRule != null && vtGroupOpenRule.Length > 0)
                    {
                        for (int n = 0; n < vtGroupOpenRule.Length; n++)
                        {
                            UNIGROUP group = new UNIGROUP();
                            group = vtGroupOpenRule[n].szGroup;
                            if (group.dwGroupID != null && ((uint)group.dwGroupID != 0))
                            {
                                uGroupIDValue = (uint)group.dwGroupID;
                                break;
                            }
                        }
                        if (uGroupIDValue == 0)
                        {
                            UNIGROUP setGroup = new UNIGROUP();
                            if (NewGroup(setGroupRule.szRuleName + "开放规则组", (uint)UNIGROUP.DWKIND.GROUPKIND_OPENRULE, out setGroup))
                            {
                                setGroupOpenRule = vtGroupOpenRule[0];
                                setGroupOpenRule.szGroup = new UNIGROUP();
                                setGroupOpenRule.szGroup = setGroup;
                                uGroupIDValue = (uint)setGroup.dwGroupID;
                            }
                            else {
                                MessageBox("新建白名单组失败", "复制失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                                continue;
                            }
                        }
                    }

                    DEVOPENRULE setValue = new DEVOPENRULE();
                    setValue = vtRes[0];
                    setValue.GroupOpenRule = null;
                    CHANGEGROUPOPENRULE delOpenRule = new CHANGEGROUPOPENRULE();
                    delOpenRule.dwRuleSN = setValue.dwRuleSN;
                    delOpenRule.dwOldGroupID = 0;
                    delOpenRule.dwGroupID = uGroupIDValue;
                    if (m_Request.Device.GroupOpenRuleDel(delOpenRule) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        setValue.GroupOpenRule=new GROUPOPENRULE[1];
                        setValue.GroupOpenRule[0] =setGroupOpenRule;
                        if (m_Request.Device.DevOpenRuleSet(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            for (int i = 0; i < groupMemberList.Length; i++)
                            {
                                if (uGroupIDValue != 0)
                                {
                                    AddGroupMember(uGroupIDValue, groupMemberList[i].dwMemberID, (uint)groupMemberList[i].dwKind, groupMemberList[i].szName.ToString());
                                }
                            }
                        }
                    }


                }
            }
            MessageBox("复制成功", "复制成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;


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
