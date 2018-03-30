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
    protected string szKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        ASSERTLOGREQ vrParameter = new ASSERTLOGREQ();
        ASSERTLOG[] vrResult;
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        szKind = GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        szKind += GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwOpKind", true);
       
        if (vrParameter.dwOpKind == 0)
        {
            vrParameter.dwOpKind = null;
        }
        if (vrParameter.szReqExtInfo.szOrderKey == null)
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwOpTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        if (m_Request.Assert.AssertLogGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Assert);
            for (int i = 0; i < vrResult.Length; i++)
            {

                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwOpKind, "dwOpKind") +"</td>";
                m_szOut += "<td>" + vrResult[i].szOpDetail + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwOpTime) + "</td>";
                m_szOut += "<td>" + vrResult[i].szOperatorName + "</td>";
                m_szOut += "</tr>";
            }
           
        }

        PutBackValue();
    }
}
