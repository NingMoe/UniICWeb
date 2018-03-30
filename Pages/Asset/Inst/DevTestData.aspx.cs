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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        TESTDATAREQ vrParameter = new TESTDATAREQ();
        UNITESTDATA[] vrResult;
        string szPID = Request["dwPID"];
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {            
            vrParameter.dwDevID = Parse(szKey);
        }
        UNIACCOUNT account = new UNIACCOUNT();
        if (szPID!=null&&GetAccByLogonName(szPID, out account))
        {
            vrParameter.dwAccNo = account.dwAccNo;
        }
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwSubmitTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        if (m_Request.Account.TestDataGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-displayname=" + vrResult[i].szDisplayName.ToString() + " data-id=" + vrResult[i].dwSID.ToString() + ">" + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "</td>"; 
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";                
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwSubmitTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + vrResult[i].szDisplayName.ToString() + "</td>";
                m_szOut += "<td>" +GetFileSizeString((double)vrResult[i].dwFileSize) + "</td>";                
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Account);
        }

        PutBackValue();
    }
}
