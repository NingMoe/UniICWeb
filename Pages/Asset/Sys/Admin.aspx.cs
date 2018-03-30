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
        ADMINREQ vrParameter = new ADMINREQ();        
        UNIADMIN[] vrResult;        
        if (Request["delID"] != null)
        {

            DelAdmin(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Admin.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwAccNo.ToString() + "\">" + vrResult[i].szLogonName + "</td>";
                m_szOut += "<td>" +(vrResult[i].szTrueName) + "</td>";
                m_szOut += "<td>" +GetJustNameEqual((vrResult[i].dwManRole), "Admin_ManRole") + "</td>";
                m_szOut += "<td>" + vrResult[i].szHandPhone + "~" + vrResult[i].szTel + "~" + vrResult[i].szEmail + "</td>";            
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                string szDivOPTD = "OPTD";
                if (((uint)vrResult[i].dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER) > 0)
                {
                    szDivOPTD = "";
                }
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Admin);
        }
        PutBackValue();        
    }
    private void DelAdmin(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIADMIN delAdmin = new UNIADMIN();
        delAdmin.dwAccNo = Parse(szID);
        uResponse = m_Request.Admin.Del(delAdmin);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
