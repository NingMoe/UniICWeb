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
        CTRLSWREQ vrParameter = new CTRLSWREQ();
        m_szClassID = Request["id"];
        
        vrParameter.dwClassSN = uint.Parse(Request["id"]);
        UNICTRLSW[] vrResult;
        if (Request["delID"] != null)
        {

            DelSW(Request["delID"]);
        }
        if (m_Request.Control.GetCtrlSW(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                string strTmp;
                int nSN = i + 1;

                string szIconPath = (Int64.Parse(vrResult[i].dwMemberID.ToString())).ToString("X");
                string szHref = "<li style=\"width:32px;height:32px; background-image:url('../../../appicon/" + szIconPath + ".ico');\"></li>";

                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwID + "'>" + szHref + "</td>";
                m_szOut += "<td>" + vrResult[i].szName + "</td>";
                m_szOut += "<td>" + UintToAndCharList((vrResult[i].dwKind), "SW_KIND") + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private void DelSW(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICTRLSW delSW = new UNICTRLSW();
        delSW.dwID = Parse(szID);
        uResponse = m_Request.Control.DelCtrlSW(delSW);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
