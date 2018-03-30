using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szGroupID = Request["GroupID"];
        string szMemberID = Request["MemberID"];
        string szKind=Request["KindID"];
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();
        if (Session["ClassGroupID"] != null && Session["ClassGroupID"].ToString() != "")
        {
            szGroupID = Session["ClassGroupID"].ToString();
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPMEMBER groupMember = new GROUPMEMBER();
        groupMember.dwGroupID = Parse(szGroupID);
        groupMember.dwMemberID = Parse(szMemberID);
        groupMember.dwKind = Parse(szKind);
        uResponse = m_Request.Group.DelGroupMember(groupMember);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("success");
        }
        else
        {
            Response.Write(m_Request.szErrMessage.ToString());
        }
    }
        
}