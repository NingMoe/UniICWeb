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
        TUTORREQ vrParameter = new TUTORREQ();
        if (Request["delID"] != null)
        {
            DelTutor(Request["delID"]);
        }
        if (Request["szTrueName"] != null && Request["szTrueName"] != "")
        {
            vrParameter.szTrueName = Request["szTrueName"];
        }
        UNITUTOR[] vrResult;       
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Account.TutorGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwAccNo.ToString() + "\">" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTel+ "</td>";
                m_szOut += "<td>" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vrResult[i].szEmail + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                string szDivOPTD = "OPTD";
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Account);
        }
        PutBackValue();        
    }
    private void DelTutor(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        EXTIDENTACC delTuro = new EXTIDENTACC();
        delTuro.dwAccNo = Parse(szID);
        uResponse = m_Request.Account.ExtIdentAccDel(delTuro);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}