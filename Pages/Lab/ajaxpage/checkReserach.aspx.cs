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
using UniStruct;
using System.Xml;
using System.Text;


public partial class Page_Search : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        SFROLEINFOREQ vrPar = new SFROLEINFOREQ();
        vrPar.dwApplyID = Parse(Request["ID"]);
        string szvApplyAgain = Request["vApplyAgain"];
        SFROLEINFO[] vtRes;
        uResponse = m_Request.System.SFRoleGet(vrPar, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS == vtRes.Length > 0)
        {
            if (szvApplyAgain == "1")
            {
                vtRes[0].dwStatus = (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT + (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
            }
            else
            {
                vtRes[0].dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
            }
            vtRes[0].szCheckInfo = Request["szCheckInfo"];
            uResponse = m_Request.System.SFRoleCheck(vtRes[0]);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("success");
            }
            else
            {

                Response.Write(m_Request.szErrMessage.ToString());
            }
        }
    }
   
}
