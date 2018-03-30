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
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTFADETAILREQ vrParameter = new RTFADETAILREQ();

        RTFADETAIL[] vrResult;
        string szDevID = "";
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");            
        }
        UNIDEVICE[] dev;
        if (GetAllDev(out dev) == true)
        {
            //m_szDev += "<option value='" + "0" + "'>" + "全部" + "</option>";
            szDevID = dev[0].dwDevID.ToString();
            for (int i = 0; i < dev.Length; i++)
            {
                m_szDev += "<option value='" + dev[i].dwDevID.ToString() + "'>" + dev[i].szDevName + "</option>";
            }
        }

        if (Request["dwDevID"] != null && Request["dwDevID"] != "" && Request["dwDevID"] != "0")
        {
            vrParameter.dwDevID = ToUint(Request["dwDevID"]);
        }
        else
        {
            vrParameter.dwDevID = ToUint(szDevID);
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);    
        uResponse=m_Request.Report.GetRTFADetail(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" +Get1970Date(vrResult[i].dwBeginTime,"yyyy-MM-dd") + "</td>";
                m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                m_szOut += "<td>" + vrResult[i].szHolderName.ToString() + "</td>";
                m_szOut += "<td>" + GetFee(vrResult[i].dwTotalFee) + "</td>";
                m_szOut += "<td>" + GetFee(vrResult[i].dwTestFee) + "</td>";//
                m_szOut += "<td>" + GetFee(vrResult[i].dwOpenFundFee) + "</td>";//
                m_szOut += "<td>" + GetFee(vrResult[i].dwServiceFee) + "</td>"; //                             
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }
        
        PutBackValue();
    }  
}
