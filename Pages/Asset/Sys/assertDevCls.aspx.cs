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
            DelDevCls(Request["delID"]);
        }
        DEVCLSREQ vrParameter = new DEVCLSREQ();
        string szKind = Request["kind"];
        if (szKind != null && szKind != "")
        {
            vrParameter.dwKind = Parse(szKind);
        }
        UNIDEVCLS[] vrResult;          
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.DevClsGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id="+vrResult[i].dwClassID.ToString()+">" + vrResult[i].szClassSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szClassName + "</td>";            
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";            
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
    private void DelDevCls(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        UNIDEVCLS[] vtDevKind;
        vrGet.dwClassID = ToUint(szID);
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtDevKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtDevKind == null || vtDevKind.Length == 0)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
            return;
        }
        UNIDEVCLS devKind = new UNIDEVCLS();
        devKind = vtDevKind[0];
        uResponse = m_Request.Device.DevClsDel(devKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
