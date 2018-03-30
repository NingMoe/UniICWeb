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
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request["delID"] != null)
        {
            
            DelFee(Request["delID"]);
        }
        CREDITTYPEREQ vrParameter = new CREDITTYPEREQ();
        CREDITTYPE[] vrResult;
        
        if (m_Request.System.CreditTypeGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwCTSN.ToString() + "\">" + vrResult[i].szCTName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwForClsKind, "DevClass_dwKind") + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwUsePurpose, "ResvPurpose") + "</td>";
                uint uScoreCycle = (uint)vrResult[i].dwScoreCycle;
                if (uScoreCycle == 1)
                {
                    m_szOut += "<td>每年</td>";
                }
                else {
                    m_szOut += "<td>每学期</td>";
                }
                m_szOut += "<td>" + vrResult[i].dwMaxScore + "</td>";
                m_szOut += "<td>" + vrResult[i].dwForbidUseTime + "</td>";
              
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Fee);
        }
         
        PutBackValue();
    }
    private void DelFee(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CREDITTYPEREQ vrGet = new CREDITTYPEREQ();
        vrGet.dwCTSN = Parse(Request["dwID"]);
        CREDITTYPE[] vtRes;
        uResponse = m_Request.System.CreditTypeGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
