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

public partial class _Default : UniPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        string szGetType = Request["getType"];
        string szID = Request["id"];
        if (szGetType == null && szID != null && szID!="")
        {
            vrGet.dwAccNo = Parse(szID);
        }
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            vtRes[0].szQQ = GetIdent((uint)vtRes[0].dwIdent);
            PutJSObj(vtRes[0]);
        }
	}
}
