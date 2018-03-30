using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szGroupID = Request["GroupID"];
        string szGroupName=Request["GroupName"];
        string szMemberID = Request["MemberID"];
        string szKind=Request["KindID"];
        string szName = Request["Name"];
        string szStartDate = Request["dwStartDate"];
        string szEndDate = Request["dwEndDate"];
        string szType=Request["type"];
        if (szType!=null&&szType.ToLower() == "logonname")
        {
            UNIACCOUNT GetAccount = new UNIACCOUNT();
            if(GetAccByLogonName(szMemberID, out GetAccount))
            {
                szMemberID=GetAccount.dwAccNo.ToString();
                szName = GetAccount.szTrueName.ToString();
            }
        }
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();
        if (Session["ClassGroupID"]==null&&(szGroupID == null || szGroupID == "" || szGroupID == "0"))
        {
            UNIGROUP newGroup = new UNIGROUP();
            newGroup.szName = szGroupName;
            newGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
            if (m_Request.Group.SetGroup(newGroup, out newGroup) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                szGroupID = newGroup.dwGroupID.ToString();
                Session["ClassGroupID"] = szGroupID;
            }
        }
        if (szGroupID == null && Session["ClassGroupID"] != null && Session["ClassGroupID"].ToString() != "")
        {
            szGroupID = Session["ClassGroupID"].ToString();
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPMEMBER groupMember = new GROUPMEMBER();
        groupMember.dwGroupID = Parse(szGroupID);
        groupMember.dwMemberID = Parse(szMemberID);
        groupMember.dwKind = Parse(szKind);
        if (((uint)groupMember.dwKind) == (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS)
        {
            if (IsInClassGroupMember(groupMember.dwGroupID, groupMember.dwMemberID, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS))
            {
                Response.Write("已添加");
                return;
            }

        }
        else if (((uint)groupMember.dwKind)== (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL)
        {
            if (IsInClassGroupMember(groupMember.dwGroupID, groupMember.dwMemberID, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL))
            {
                Response.Write("已添加");
                return;
            }
        }
        
        if (szStartDate != null && szStartDate != "")
        {
            groupMember.dwBeginDate = GetDate(szStartDate);
        }
        if (szEndDate != null && szEndDate != "")
        {
            groupMember.dwEndDate = GetDate(szEndDate);
        }
        groupMember.szName = szName;
        uResponse = m_Request.Group.SetGroupMember(groupMember);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("success");
        }
        else
        {
            Response.Write(m_Request.szErrMessage);
        }
    }
        
}