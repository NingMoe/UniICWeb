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

public partial class Sub_Device : UniPage
{
    protected string m_szOut = "";
    protected string m_szClassID = "";
    protected string m_szClassP= "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["GroupOpenRuleList"] = null;
        CTRLURLREQ vrParameter = new CTRLURLREQ();
        m_szClassID = Request["id"];
        if (Request["id"] != null)
        {
            vrParameter.dwClassSN = Parse(Request["id"]);
        }
        CTRLCLASSREQ vrCtrlassGet = new CTRLCLASSREQ();
        vrCtrlassGet.dwCtrlKind = (uint)(UNICTRLCLASS.DWCTRLKIND.CTRLKIND_URL);
        UNICTRLCLASS[] vtCtrlClass;
        uint uID = 0;
        if (m_Request.Control.GetCtrlClass(vrCtrlassGet, out vtCtrlClass) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtCtrlClass.Length; i++)
            {
                if ((vtCtrlClass[i].dwCtrlMode & (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_PERMIT) != (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_PERMIT)
                {
                    continue;
                }
                if (uID ==0)
                {
                    uID=(uint)vtCtrlClass[i].dwCtrlSN;
                }

                m_szClassP += GetInputItemHtml(CONSTHTML.option, "", vtCtrlClass[i].szCtrlName, vtCtrlClass[i].dwCtrlSN.ToString());
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        if (m_szClassID == null)
        {
            if (!IsPostBack)
            {
                vrParameter.dwClassSN = uID;
            }
        }
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
