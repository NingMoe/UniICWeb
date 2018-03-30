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

public partial class Sub_Device : UniPage
{
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request["op"]=="del"&&(!string.IsNullOrEmpty(Request["delID"])))
        {
            DelGroup(Parse(Request["delID"]));
        }
        GROUPREQ req = new GROUPREQ();
        req.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_MAN;
        GetPageCtrlValue(out req.szReqExtInfo);
        UNIGROUP[] groupRes ;
        if (m_Request.Group.GetGroup(req, out groupRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (groupRes != null && groupRes.Length > 0)
            {
                for (int i = 0; i < groupRes.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-mangroupid=\"" + groupRes[i].dwGroupID.ToString() + "\" data-id=\"" + groupRes[i].dwGroupID.ToString() + "\">" + groupRes[i].szName.ToString() + "</td>";
                    string szMember = GetGroupMember(groupRes[i].szMembers);
                    if (szMember.Length > 80)
                    {
                        szMember = szMember.Substring(0, 80) + ".....";
                    }
                    m_szOut += "<td>" + szMember + "</td>";
                    m_szOut += "<td><div class='OPTD'></div></td>";
                    m_szOut += "</tr>";
                }
                UpdatePageCtrl(m_Request.Group);
            }
        }
        PutBackValue();        
    }
    private void DelResvRule(string szID)
    {
       
    }
}
