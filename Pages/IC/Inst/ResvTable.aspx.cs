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

public partial class Sub_ResvTable : UniPage
{
    protected uint m_TermList = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_TermList = GetTermList();

        RESVREQ vrParameter = new RESVREQ();
        GetHTTPObj(out vrParameter);

        if (GetReserved(vrParameter, "dwYearTerm") == null)
        {
            SetReserved(ref vrParameter, "dwYearTerm", GetDefaultTerm(null));
        }
        PutJSObj(vrParameter);
    }
}
