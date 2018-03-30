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
    protected string szKindStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        USERECREQ vrParameter = new USERECREQ();
        string szPID = Request["dwPID"];
        DEVUSEREC[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
            //vrParameter.dwGetType = (uint)REPORTREQ.DWGETTYPE.USERECGET_BYDEVID;
            vrParameter.dwDevID = Parse(szKey);
        }
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwBeginTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        UNIDEVKIND[] RoomKind = GetDevKindByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        szKindStr += GetInputItemHtml(CONSTHTML.radioButton, "dwDevKind", "全部", "0", true);
        if (RoomKind != null && RoomKind.Length > 0)
        {

            for (int i = 0; i < RoomKind.Length; i++)
            {
                szKindStr += GetInputItemHtml(CONSTHTML.radioButton, "dwDevKind", RoomKind[i].szKindName.ToString(), RoomKind[i].dwKindID.ToString());
            }
        }
        uint uKindID = Parse(Request["dwDevKind"]);
        if (uKindID != null && uKindID != 0)
        {
            vrParameter.dwDevKind = uKindID;
        }
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIACCOUNT account = new UNIACCOUNT();
        if (szPID != null && szPID !=""&& GetAccByLogonName(szPID, out account))
        {
            vrParameter.dwAccNo = account.dwAccNo;
        }
       
        if (m_Request.Report.GetUseRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";                
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szKindName+ "</td>";                                                                
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "</td>";                
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwBeginTime, "MM-dd HH:mm") + "-" + Get1970Date(vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + GetMinToStr(vrResult[i].dwUseTime) + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
