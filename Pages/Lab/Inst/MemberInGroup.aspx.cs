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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        GROUPREQ vrParameter = new GROUPREQ();
        string szPID = Request["szLogonName"];
        GetHTTPObj(out vrParameter);
        if (szPID != null && szPID != "")
        {
            UNIACCOUNT outAccno = new UNIACCOUNT();
            if (GetAccByLogonName(szPID, out outAccno,true))
            {
                vrParameter.dwAccNo = outAccno.dwAccNo;
            }
        }
        vrParameter.dwKind = Parse(Request["dwKind"]);
        UNIGROUP[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Group.GetGroup(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].dwGroupID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwDeadLine.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo.ToString() + "</td>";

        
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Group);
        }

        PutBackValue();
    }
}
