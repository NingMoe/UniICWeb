﻿using System;
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
        string szLogonName = Request["szLogonName"];
        string szName = Request["Name"];
        string szStartDate = Request["dwStartDate"];
        string szEndDate = Request["dwEndDate"];
        string szType=Request["type"];
        if (szType!=null&&szType.ToLower() == "logonname")
        {
            UNIACCOUNT GetAccount = new UNIACCOUNT();
            if(GetAccByLogonName(szMemberID, out GetAccount,true))
            {
                szMemberID=GetAccount.dwAccNo.ToString();
                szName = GetAccount.szTrueName.ToString();
            }
        }
        if (szType != null && szType.ToLower() == "accno")
        {
            UNIACCOUNT GetAccount = new UNIACCOUNT();
            if (GetAccByAccno(szMemberID, out GetAccount))
            {
                szMemberID = GetAccount.dwAccNo.ToString();
                szName = GetAccount.szTrueName.ToString();
            }
        }
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();


        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPMEMBER groupMember = new GROUPMEMBER();
        groupMember.dwGroupID = Parse(szGroupID);
        groupMember.dwMemberID = Parse(szMemberID);
        groupMember.dwKind = Parse(szKind);
        if (szStartDate != null && szStartDate != "")
        {
            groupMember.dwBeginDate = GetDate(szStartDate);
        }
        if (szEndDate != null && szEndDate != "")
        {
            groupMember.dwEndDate = GetDate(szEndDate);
        }
        groupMember.szName = szName;
        if (szLogonName != null && szLogonName != "")
        {
            groupMember.szMemo = szName + "(" + szLogonName + ")";
        }

        uResponse = m_Request.Group.SetGroupMember(groupMember);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("succ");
        }
        else
        {
            Response.Write(m_Request.szErrMessage);
        }
    }
        
}