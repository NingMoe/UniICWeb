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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szTitleName = "";
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
            
            DelLab(Request["delID"]);
        }
        szTitleName = GetJustNameEqual(Parse(Request["dwCodeType"]), "CodeType", false);
     
        CODINGTABLEREQ vrParameter = new CODINGTABLEREQ();
        vrParameter.dwCodeType = Parse(Request["dwCodeType"]);
        CODINGTABLE[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.System.GetCodingTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.System);
            for (int i = 0; i < vrResult.Length; i++)
            {
              
                m_szOut += "<tr>";
                m_szOut += "<td data-type=\"" + vrResult[i].dwCodeType.ToString() + "\" data-id=\"" + vrResult[i].szCodeSN.ToString() + "\">" + vrResult[i].szExtValue + "</td>";
                m_szOut += "<td>" + vrResult[i].szCodeName + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CODINGTABLE value = new CODINGTABLE();
        value.szCodeSN=(szID);
        value.dwCodeType = Parse(Request["dwCodeType"]);
        uResponse = m_Request.System.DelCodingTable(value);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
