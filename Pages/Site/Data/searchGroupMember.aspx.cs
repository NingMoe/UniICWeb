using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        uint groupID = Parse(Request["id"]);

        Response.CacheControl = "no-cache";
        UNIGROUP[] groupRes = GetGroupByID(groupID);
        if (groupRes != null && groupRes.Length > 0)
        {
            MyString szOut = new MyString();
            szOut += "[";
            GROUPMEMBER[] groupMember = groupRes[0].szMembers;
            for (int i = 0;i< groupMember.Length;i++)
            {

                szOut += "{\"id\":\"" + groupMember[i].dwMemberID + "\",\"kind\": \"" + groupMember[i].dwKind + "\",\"name\": \"" + groupMember[i].szName + "\"}";
                if (i < groupMember.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}