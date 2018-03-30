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

public partial class _Default :UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uGroupID = Parse(Request["GroupID"]);
        string szOP=Request["op"];
        string szGroupMember = Request["memberid"];
        string szMemberkind = Request["kindid"];
        if (szOP == "del")
        {
            Del(uGroupID.ToString(),szGroupMember,szMemberkind);
        }
        GROUPREQ vrGet = new GROUPREQ();
        vrGet.dwGroupID = uGroupID;
        UNIGROUP[] vtGroup;
        if (m_Request.Group.GetGroup(vrGet, out vtGroup) == REQUESTCODE.EXECUTE_SUCCESS && vtGroup != null && vtGroup.Length > 0)
        {
            GROUPMEMBER[] vtGroupMember = vtGroup[0].szMembers;
            if (vtGroupMember != null && vtGroupMember.Length > 0)
            {
                for (int i = 0; i < vtGroupMember.Length; i++)
                {
                    m_szOut += "<tr data-id='"+vtGroupMember[i].dwMemberID.ToString()+"' data-kindid='"+vtGroupMember[i].dwKind.ToString()+"'>";
                    m_szOut += "<td>" + vtGroupMember[i].szName + "</td>";
                    m_szOut += "<td>" + vtGroupMember[i].szMemo + "</td>";
                    m_szOut += "<td><div class='OPTD'></div></td>";
                    m_szOut += "</tr>";
                }
            }
        }
    }
    private void Del(string szGroupID,string szMemberID,string kind)
    {
        DelGroupMember(Parse(szGroupID), Parse(szMemberID), Parse(kind));
    }
}
