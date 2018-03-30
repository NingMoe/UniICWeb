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
    protected string m_TermList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DEFAULTSTATREQ vrParameter = new DEFAULTSTATREQ();
        
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
       // vrParameter.dwForClsKind = 8;
        DEFAULTSTAT[] vtRes;
        m_Request.Report.GetDefaultStat(vrParameter, out vtRes);
        if (vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vtRes[i].szCreditName.ToString() + "</td>";//
                m_szOut += "<td>" + vtRes[i].dwResvTimes.ToString() + "</td>";//
                m_szOut += "<td>" + vtRes[i].dwDefaultTimes.ToString() + "</td>";//
                double df = (double)(vtRes[i].dwDefaultTimes / (1.0 * vtRes[i].dwResvTimes));
                m_szOut += "<td>" + (100*df).ToString("0.00") + "%</td>";//
                m_szOut += "</tr>";
            }
        }
    }
}
