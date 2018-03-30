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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szYearTerm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
           Del(Request["delID"]);
        }
        UNITERM[] vtTerm = GetAllTerm();
        uint uEndDate = 0;
       
        uEndDate = Parse(Request["dwDeadLine"]);
        
        if (vtTerm != null)
        {
            for (int i = 0; i < vtTerm.Length; i++)
            {
                if ((vtTerm[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    if (uEndDate == 0)
                    {
                        uEndDate = (uint)vtTerm[i].dwEndDate;
                    }
                    szYearTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwEndDate.ToString(), true);
                }
                else
                {
                    szYearTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwEndDate.ToString());
                }
            }
        }

        GROUPREQ vrParameter = new GROUPREQ();
        vrParameter.dwMaxDeadLine = uEndDate;
        vrParameter.dwMinDeadLine = uEndDate;
        vrParameter.szName = Request["szname"];
        UNIGROUP[] vrResult;
        vrParameter.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Group.GetGroup(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwGroupID.ToString() + ">" + vrResult[i].szName + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo.ToString()+ "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Group);
        }
        PutBackValue();
    }
    private void Del(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIGROUP group = new UNIGROUP();
        group.dwGroupID = Parse(szID);
        uResponse=m_Request.Group.DelGroup(group);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
