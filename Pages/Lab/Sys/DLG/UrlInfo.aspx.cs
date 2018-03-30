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
    protected string m_szOut = "";
    protected string m_szClassID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["GroupOpenRuleList"] = null;
        CTRLURLREQ vrParameter = new CTRLURLREQ();
        m_szClassID = Request["id"];
        vrParameter.dwClassSN = uint.Parse(Request["id"]);
        UNICTRLURL[] vrResult;
        if (Request["delID"] != null)
        {

            DelUrl(Request["delID"]);
        }
        if (m_Request.Control.GetCtrlURL(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                string strTmp;
                int nSN = i + 1;
                
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwID + "'>" + nSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szURL + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private void DelUrl(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICTRLURL delUrl = new UNICTRLURL();
        delUrl.dwID = Parse(szID);
        uResponse = m_Request.Control.DelCtrlURL(delUrl);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
