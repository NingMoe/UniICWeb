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
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ACCREQ vrParameter = new ACCREQ();
        UNIACCOUNT[] vrResult;
        GetHTTPObj(out vrParameter);
        vrParameter.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_RTLEADER;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (Request["delID"] != null)
        {
            UNIACCOUNT accno = new UNIACCOUNT();
            if(GetAccByAccno(Request["delID"],out accno))
            {
                EXTIDENTACC delAccount = new EXTIDENTACC();
                delAccount.dwAccNo = accno.dwAccNo;
                delAccount.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
                m_Request.Account.ExtIdentAccDel(delAccount);
            }
            
        }
        if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwAccNo.ToString()+"'>" + vrResult[i].szLogonName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwSex, "ACCNO_sex") + "</td>";
                m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwIdent,"ACCNO_IDENT") + "</td>";
                m_szOut += "<td>" +vrResult[i].szClassName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vrResult[i].szEmail + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Account);
        }
        PutBackValue();
    }
   
}
