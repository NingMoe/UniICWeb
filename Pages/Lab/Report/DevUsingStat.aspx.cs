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
        REPORTREQ vrParameter = new REPORTREQ();
        DEVSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
           
        }
       
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
            if (vrParameter.szReqExtInfo.szOrderKey == null)
            {
                vrParameter.szReqExtInfo.szOrderKey = "dwTotalUseTime";
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
        if (m_Request.Report.GetDevStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwDevID.ToString() + ">" + vrResult[i].szDevName + "</td>";
                string szClsName= vrResult[i].szClassName;
                if(szClsName==null)
                {
                    szClsName="";
                }
                m_szOut += "<td>" + szClsName.ToString() + "</td>";
               // m_szOut += "<td class='lnkTeacher' data-id='" + vrResult[i].dwAttendantID + "'>" + vrResult[i].szAttendantName + "</td>";
                uint uUseTime=(uint)vrResult[i].dwTotalUseTime;
              
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szModel+"/"+ vrResult[i].szSpecification  + "</td>";
                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPIDNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
