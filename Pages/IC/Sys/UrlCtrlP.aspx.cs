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
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["GroupOpenRuleList"] = null;
        CTRLCLASSREQ vrParameter = new CTRLCLASSREQ();

        vrParameter.dwCtrlKind = (uint)(UNICTRLCLASS.DWCTRLKIND.CTRLKIND_URL);
        UNICTRLCLASS[] vrResult;
        if (Request["delID"] != null)
        {

            DelUrlClass(Request["delID"]);
        }
        if (m_Request.Control.GetCtrlClass(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if ((vrResult[i].dwCtrlMode & (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_PERMIT) != (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_PERMIT)
                {
                    continue;
                }
                m_szOut += "<tr>";
                m_szOut += "<td  class='lnkUrlClass1' data-id=\"" + vrResult[i].dwCtrlSN + "\">" + vrResult[i].szCtrlName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((vrResult[i].dwForAges), "CtrlClass_dwForAges") + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();        
    }
    private void DelUrlClass(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICTRLCLASS delClass = new UNICTRLCLASS();
        delClass.dwCtrlSN = Parse(szID);
        uResponse = m_Request.Control.DelCtrlClass(delClass);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
