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

    protected void Page_Load(object sender, EventArgs e)
    {
        {
            REPORTREQ vrParameter = new REPORTREQ();
            USERSTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (!IsPostBack)
            {
                dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            }

            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);            
            if (m_Request.Report.GetUserStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                UpdatePageCtrl(m_Request.Report);
                for (int i = 0; i < vrResult.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-id=" + vrResult[i].dwAccNo.ToString() + ">" + vrResult[i].szPID + "</td>";
                    UNIACCOUNT accinfo;
                   
                    m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                     if(GetAccByAccno(vrResult[i].dwAccNo.ToString(),out accinfo))
                    {
                        m_szOut += "<td>" + accinfo.szClassName.ToString() + "</td>";
                     }
                     else
                     {
                          m_szOut += "<td></td>";
                     }
                    m_szOut += "<td>" + vrResult[i].szDeptName.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";
                    uint uUseTime = (uint)vrResult[i].dwUseTime;
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
