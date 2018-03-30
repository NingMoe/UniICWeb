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
    protected string m_YardActivity = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        YARDACTIVITYSTATREQ vrParameter = new YARDACTIVITYSTATREQ();
        YARDACTIVITYSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }
       
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        if (ConfigConst.GCICTypeMode == 1)
        {
            UNITERM[] termnow = GetTermByID(Parse(Request["dwYearTerm"]));
            if (termnow != null && termnow.Length > 0)
            {
                vrParameter.dwStartDate = termnow[0].dwBeginDate;
                vrParameter.dwEndDate = termnow[0].dwEndDate;
            }
            else
            {
                vrParameter.dwStartDate = null;
                vrParameter.dwEndDate = null;

            }
        }
        
        if (m_Request.Report.GetYardActivityStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Report);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwActivitySN.ToString() + ">" + vrResult[i].szActivityName + "</td>";
                
                m_szOut += "<td>" + vrResult[i].dwPIDNum.ToString() + "</td>";/*使用人数*/
                m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";/*使用人次*/
                uint uUseTime = (uint)vrResult[i].dwTotalUseTime;
                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                
                m_szOut += "</tr>";
            }

        }


        PutBackValue();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

       
    }
   
}
