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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_kind = "";
    public int nIsAdminSup = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
        uint uManRole = (uint)adminAcc.dwManRole;
        if ((uManRole & ((uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER + (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION + (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER)) > 0)
        {
            nIsAdminSup = 1;
        }

        if (Request["delID"] != null)
        {
            DelCompay(Request["delID"]);
        }
        m_kind += GetInputItemHtml(CONSTHTML.option,"","全部","0");
        m_kind += GetInputHtmlFromXml(0, CONSTHTML.option, "", "companyKind", true);
        COMPANYREQ vrParameter = new COMPANYREQ();
        GetHTTPObj(out vrParameter);
        if (vrParameter.dwComKind == 0)
        {
            vrParameter.dwComKind = null;
        }
        UNICOMPANY[] vrResult;
        //vrParameter.dwComKind = 1;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Assert.GetCompany(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id="+vrResult[i].dwComID.ToString()+">" + vrResult[i].szComName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwComKind, "companyKind")+"</td>";    
                m_szOut += "<td>" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vrResult[i].szTel + "</td>";            
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Assert);
        }

        PutBackValue();
    }
    private void DelCompay(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICOMPANY setValue = new UNICOMPANY();
        setValue.dwComID = Parse(szID);
        uResponse = m_Request.Assert.DelCompany(setValue);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
