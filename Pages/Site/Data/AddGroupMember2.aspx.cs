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
        string szMemberID = Request["MemberID"];
        string szKind=Request["KindID"];
        string szName = Request["Name"];
        string szStartDate = Request["dwStartDate"];
        string szEndDate = Request["dwEndDate"];
        string szType=Request["type"];
        string szAddLogonName = Request["addlogonname"];
        if (szType!=null&&szType.ToLower() == "logonname")
        {
            UNIACCOUNT GetAccount = new UNIACCOUNT();
            if (GetAccByLogonName(szMemberID, out GetAccount,true))
            {
                if (szAddLogonName == "true")
                {
                    szName = GetAccount.szTrueName.ToString()+"("+GetAccount.szLogonName+")";
                }
                else {
                    szName = GetAccount.szTrueName.ToString();
                }
                szMemberID = GetAccount.dwAccNo.ToString();
               
            }
            else
            {
                Response.Write("个人信息有误");
                Response.End();
            }
        }
        if (szType != null && szType.ToLower() == "accno")
        {
            UNIACCOUNT GetAccount = new UNIACCOUNT();
            if (GetAccByAccno(szMemberID, out GetAccount))
            {
                if (szAddLogonName == "true")
                {
                    szName = GetAccount.szTrueName.ToString() + "(" + GetAccount.szLogonName + ")";
                }
                else
                {
                    szName = GetAccount.szTrueName.ToString();
                }
                szMemberID = GetAccount.dwAccNo.ToString();

            }
            else
            {
                Response.Write("个人信息有误");
                Response.End();
            }
        }
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();


        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPMEMBER groupMember = new GROUPMEMBER();
        groupMember.dwGroupID = Parse(szGroupID);
        groupMember.dwMemberID = Parse(szMemberID);
        groupMember.dwKind = Parse(szKind);
        if (IsExistGroupMember(groupMember.dwGroupID, groupMember.dwMemberID, groupMember.dwKind))
        {
            Response.Write("添加成员已存在");
            Response.End();
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
            Response.Write("succ" + szName);
        }
        else
        {
            Response.Write(m_Request.szErrMessage);
        }
    }
    bool IsExistGroupMember(uint? groupid, uint? memberid, uint? umemberkind)
    {
        bool bRes = false;

        GROUPREQ vrGet = new GROUPREQ();
        vrGet.dwGroupID = groupid;
        UNIGROUP[] vtGroup;
        if (m_Request.Group.GetGroup(vrGet, out vtGroup) == REQUESTCODE.EXECUTE_SUCCESS && vtGroup != null && vtGroup.Length > 0)
        {
            GROUPMEMBER[] vtMember = vtGroup[0].szMembers;
            for (int i = 0; i < vtMember.Length; i++)
            {
                if (vtMember[i].dwMemberID == memberid && vtMember[i].dwKind == umemberkind)
                {
                    return true;
                }
            }
        }

        return bRes;
    }
}