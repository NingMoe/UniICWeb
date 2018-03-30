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
        uint ukind = Parse(Request["dwKind"]);
        if (ukind == 0)
        {
            ukind = (uint)UNIGROUP.DWKIND.GROUPKIND_OPENRULE;
        }
        UNIGROUP[] groupRes = GetGroupByKind(ukind);
        if (groupRes!=null&&groupRes.Length>0)
        {
            for (int i = 0; i < groupRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + groupRes[i].dwGroupID.ToString() + "\">" + groupRes[i].szName.ToString() + "</td>";
                string szMember =GetGroupMember(groupRes[i].szMembers);
                if (szMember.Length > 80)
                {
                    szMember = szMember.Substring(0,80)+".....";
                }
                m_szOut += "<td>" + szMember + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();        
    }
    private void DelResvRule(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIRESVRULE delRule = new UNIRESVRULE();
        delRule.dwRuleSN = Parse(szID);
        uResponse=m_Request.Reserve.ResvRuleDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
