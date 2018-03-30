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

    protected string m_szLab = "";
    protected string m_szRoom = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelDevKind(Request["delID"]);           
        }
        DEVKINDREQ vrParameter = new DEVKINDREQ();
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        if (Request["szKindName"] != null && Request["szKindName"] != "")
        {
            vrParameter.szKindName = Request["szKindName"].ToString().Trim();
        }
        UNIDEVKIND[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.DevKindGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwKindID.ToString() + ">" + vrResult[i].szKindName + "</td>";             
                m_szOut += "<td>" + vrResult[i].dwTotalNum + "</td>";                          
                m_szOut += "<td>" + GetJustName(vrResult[i].dwProperty, "DevKind_dwProperty") + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
    private void DelDevKind(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDREQ vrGet = new DEVKINDREQ();
        UNIDEVKIND[] vtDevKind;
        vrGet.dwKindID = ToUint(szID);
        uResponse = m_Request.Device.DevKindGet(vrGet, out vtDevKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtDevKind == null || vtDevKind.Length == 0)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
            return;
        }
        if (ConfigConst.GCKindAndClass == 1)//同时删除devclass
        {
            UNIDEVCLS devClass = new UNIDEVCLS();
            devClass.dwClassID = vtDevKind[0].dwClassID;
            uResponse = m_Request.Device.DevClsDel(devClass);
        }
        UNIDEVKIND devKind = new UNIDEVKIND();
        devKind = vtDevKind[0];
        uResponse = m_Request.Device.DevKindDel(devKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
