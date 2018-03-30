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
        RESVRECREQ vrParameter = new RESVRECREQ();
        string szPID = Request["dwPID"];
        UNIRESVREC[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
            vrParameter.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYDEVID;
            vrParameter.szGetKey = (szKey);
        }
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwBeginTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
      
        UNIACCOUNT account = new UNIACCOUNT();
        if (szPID != null&& szPID!=""&& GetAccByLogonName(szPID, out account))
        {
            vrParameter.dwAccNo = account.dwAccNo;
        }
       
        if (m_Request.Report.ResvRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                
                m_szOut += "<td>" + GetScanMode(vrResult[i].dwInMode) + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwInTime) + "</td>";

                m_szOut += "<td>" + GetScanMode(vrResult[i].dwLeaveMode) + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwLeaveTime) + "</td>";


                m_szOut += "<td>" + GetScanMode(vrResult[i].dwBackMode) + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwBackTime) + "</td>";


                m_szOut += "<td>" + GetScanMode(vrResult[i].dwOutMode) + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwOutTime) + "</td>";




                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
    private string GetScanMode(uint? umode)
    {
        string res = "";
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_CONSOLE)) > 0)
        {
            return "现场预约台";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_AG)) > 0)
        {
            return "通道机";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_HP)) > 0)
        {
            return "手机";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_MONITOR)) > 0)
        {
            return "智能座位监控设备";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_AUTO)) > 0)
        {
            return "自动签到";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_ADMIN)) > 0)
        {
            return "管理员";
        }
        if ((umode & ((uint)UNIRESVREC.DWINMODE.RCMODE_DOOR)) > 0)
        {
            return "门禁刷卡";
        }
        
        return "";
    }
}
