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
        GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
        vrGet.dwGroupID = uGroupID;
        GROUPMEMDETAIL[] vtGroup;
        if (m_Request.Group.GetGroupMemDetail(vrGet, out vtGroup) == REQUESTCODE.EXECUTE_SUCCESS && vtGroup != null && vtGroup.Length > 0)
        {

            if (vtGroup != null && vtGroup.Length > 0)
            {
                for (int i = 0; i < vtGroup.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td>" + vtGroup[i].szPID + "</td>";
                    m_szOut += "<td>" + vtGroup[i].szTrueName + "</td>";
                    m_szOut += "<td>" + vtGroup[i].szDeptName + "</td>";
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
