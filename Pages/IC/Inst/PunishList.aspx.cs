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
        MYCREDITSCOREREQ vrParameter = new MYCREDITSCOREREQ();        
        string szKey = Request["szGetKey"];
        if (szKey == null||szKey=="")
        {
            return;
        }
        vrParameter.dwAccNo = Parse(szKey);
        MYCREDITSCORE[] vrResult;       
       // GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.System.MyCreditScoreGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {            
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {               
                m_szOut += "<tr>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwUsePurpose, "ResvPurpose") + "</td>";
                int uMax = (int)vrResult[i].dwMaxScore;
                int nLeft = (int)vrResult[i].dwLeftCScore;
                if (uMax < nLeft)
                {
                    nLeft= uMax;
                }
                m_szOut += "<td>" + uMax.ToString() + "</td>";
               
                m_szOut += "<td>" + nLeft + "</td>";
                string szInfo = "";
                if (vrResult[i].dwLeftCScore == 0)
                {
                    szInfo = GetDateStr(vrResult[i].dwForbidStartDate) + "-" + GetDateStr(vrResult[i].dwForbidEndDate);
                }
                m_szOut += "<td>" + szInfo + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "</tr>";
            }
            
        }
        PutBackValue();
    }    
}
