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
using System.IO;
using System.Text;
public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_TermList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            RESVMODESTATREQ vrParameter = new RESVMODESTATREQ();
            RESVMODESTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (!IsPostBack)
            {
                dwStartDate.Value = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
                dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            }
            vrParameter.dwBeginDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
           
            string szKey = Request["orderkey"];
            if (szKey == null)
            {
                vrParameter.szReqExtInfo.szOrderKey = "dwUseTime";
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
            else {
                vrParameter.szReqExtInfo.szOrderKey = szKey;
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
            uint dwClassKind = Parse(Request["dwClassKind"]);
            if (dwClassKind != 0)
            {
                vrParameter.dwClassKind = dwClassKind;
            }
            if (m_Request.Report.GetResvModeStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                UpdatePageCtrl(m_Request.Report);
                for (int i = 0; i < vrResult.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-id=" + vrResult[i].dwUseMode.ToString() + ">" +GetJustName(vrResult[i].dwUseMode, "resvModel",false) + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwUsers.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwResvTimes.ToString() + "</td>";
                    uint uUseTime = (uint)vrResult[i].dwResvMinutes;
                    m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                    m_szOut += "</tr>";
                }
              
            }
        }
      
        PutBackValue();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

       
    }
   
}
