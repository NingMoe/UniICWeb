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
        DEVREQ vrParameter = new DEVREQ();
        UNIDEVICE[] vrResult;
        vrParameter.dwProperty = ((uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE | (uint)DEVREQ.DWREQPROP.DEVREQ_NEEDDEVUSE);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id="+vrResult[i].dwDevID.ToString()+">" + vrResult[i].dwDevSN + "</td>";
                m_szOut += "<td  class='lnkDevice' data-id='" + vrResult[i].dwDevID + "'>" + vrResult[i].szPCName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szIP.ToString() + "</td>";
                m_szOut += "<td >" + vrResult[i].szKindName + "</td>";
                m_szOut += "<td >" + vrResult[i].szModel + "</td>";
                m_szOut += "<td >" + vrResult[i].szSpecification + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "'>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "'>" + vrResult[i].szLabName + "</td>";
                if (vrResult[i].DevUse != null && vrResult[i].DevUse.Length > 0)
                {
                    m_szOut += "<td>" + vrResult[i].DevUse[0].szTrueName + "</td>";
                }
                else
                {
                    m_szOut += "<td></td>";
                }
                m_szOut += "<td>" + vrResult[i].dwDevStat + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
}
