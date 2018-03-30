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
    protected string DevIn = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DEVREQ vrParameter = new DEVREQ();

        UNIDEVICE[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwDevID.ToString() + "  data-classid=" + vrResult[i].dwDevSN.ToString() + ">" + vrResult[i].szDevName + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
}
