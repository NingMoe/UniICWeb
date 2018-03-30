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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        string szResvID = Request["resvid"];
        BILLREQ vrParameter = new BILLREQ();
        if (!IsPostBack)
        {
            vrParameter.dwStartDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
        GetHTTPObj(out vrParameter);
        if (szResvID != null && szResvID != "")
        {
            vrParameter.dwGetType = (uint)BILLREQ.DWGETTYPE.BILLGET_BYRESVID;
            vrParameter.szGetKey = szResvID;
        }
        string szLogonName = Request["dwPID"];
        if (szLogonName != null && szLogonName != "")
        {
            UNIACCOUNT accno;
            if (GetAccByLogonName(szLogonName, out accno))
            {
                vrParameter.dwGetType = (uint)BILLREQ.DWGETTYPE.BILLGET_BYACCNO;
                vrParameter.szGetKey = accno.dwAccNo.ToString();
            }
        }
        UNIBILL[] vrResult;          
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Fee.BillGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id="+vrResult[i].dwSID.ToString()+">" + vrResult[i].szPID + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + vrResult[i].szKindName + "(" + vrResult[i].szModel+ vrResult[i].szSpecification+ ")" + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwBeginTime, "MM-dd HH:mm") + "至" + Get1970Date(vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + GetMinToStr(vrResult[i].dwFeeTime) + "</td>";
                m_szOut += "<td>" + ((float)(((uint)vrResult[i].dwUnitFee * 1.0) / (100 * vrResult[i].dwUnitTime))).ToString("0.00") + "元/分钟</td>";
                m_szOut += "<td>" + GetFee(vrResult[i].dwCostMoney) + "</td>";
             //   m_szOut += "<td>" + vrResult[i].dwFeeType + "</td>";
                m_szOut += "<td>" +Get1970Date((uint)vrResult[i].dwBillTime) + "</td>";       
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
    private void DelDevKind(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDREQ vrGet = new DEVKINDREQ();
        UNIDEVKIND[] vtDevKind;
        vrGet.dwKindID = ToUint(szID);
        uResponse = m_Request.Device.DevKindGet(vrGet, out vtDevKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtDevKind == null || vtDevKind.Length == 0)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
            return;
        }
        {//同时删除devclass
            UNIDEVCLS devClass = new UNIDEVCLS();
            devClass.dwClassID = vtDevKind[0].dwClassID;
            uResponse = m_Request.Device.DevClsDel(devClass);
        }
        UNIDEVKIND devKind = new UNIDEVKIND();
        devKind = vtDevKind[0];
        uResponse = m_Request.Device.DevKindDel(devKind);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
