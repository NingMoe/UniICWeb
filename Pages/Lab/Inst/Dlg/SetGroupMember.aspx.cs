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
            UNIGROUP newGroup;
            GetHTTPObj(out newGroup);
            if (m_Request.Group.SetGroup(newGroup, out newGroup) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
        uint uManRole = 773;// (uint)ADMINLOGINRES.DWMANROLE.MAN_STALEADER + (uint)ADMINLOGINRES.DWMANROLE.MAN_DEVCHARGE + (uint)ADMINLOGINRES.DWMANROLE.MAN_ATTENDANT;
        szIdent = GetInputHtml(uManRole, CONSTHTML.option, "", "Ident");
        if (Request["op"] == "set")
        {
            bSet = true;
            PutMemberValue("dwID",Request["dwID"]);
            UNIGROUP[] group = GetGroupByID(Parse(Request["dwID"]));
            UNIGROUP setGroup=new UNIGROUP();
            if (group==null||group.Length==0)
            {
                uint uGroup = Parse(Request["uGroupID"]);
                if (uGroup == 0)
                {
                    UNIDEVICE unidev;
                    string szDevID = Request["devID"];
                    if (getDevByID(szDevID, out unidev))
                    {
                        UNIGROUP useGroup;
                        if (NewGroup(unidev.szDevName + "使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out useGroup))
                        {
                            unidev.dwUseGroupID = (uint)useGroup.dwGroupID;
                            if (m_Request.Device.Set(unidev, out unidev) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                setGroup = useGroup;
                                szGroupMember = GetGroupMemberHtml(setGroup.szMembers);
                                PutJSObj(setGroup);
                                m_Title = "修改" + "【" + setGroup.szName.ToString() + "】";
                            }
                        }
                        else
                        {
                            MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                        }
                    }
                    else
                    {
                        MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    }

                }

             
            }
            else
            {
                szGroupMember=GetGroupMemberHtml(group[0].szMembers);
                setGroup = (group[0]);
                PutJSObj(setGroup);
                m_Title = "修改" + "【" + setGroup.szName.ToString() + "】";
                
            }
        }
        else
        {
            uint? uMax = 0;
            uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;
     
            if (GetMaxValue(ref uMax, uID, "dwSN"))
            {
                UNIADMIN setValue = new UNIADMIN();
               // ViewState["szLogonName"] = setValue.szLogonName.ToString();
                PutJSObj(setValue);
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
