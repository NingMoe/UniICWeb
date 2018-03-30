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
    protected string szStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelDevKind(Request["delID"]);

        }
        DEVDAMAGERECREQ vrParameter = new DEVDAMAGERECREQ();
        GetHTTPObj(out vrParameter);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            
        }
        szStatus += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        szStatus += GetInputHtmlFromXml(0, CONSTHTML.option, "", "DEVDAMAGEREC_status", true);

        vrParameter.dwStartDate = GetDate(dwStartDate.Value);
        vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        if (vrParameter.dwStatus==null||vrParameter.dwStatus == 0)
        {
            vrParameter.dwStatus = null;
        }
        DEVDAMAGEREC[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Assert.RepareRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwSID.ToString() + ">" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName + "</td>";
                UNIDEVICE dev;
                if(getDevByID(vrResult[i].dwDevID.ToString(),out dev))
                {
                    m_szOut += "<td>" +dev.szDeptName.ToString() + "</td>";
                }else
                {
                      m_szOut += "<td>" +"" + "</td>";
                }
                
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwDamageDate) + "</td>";
                m_szOut += "<td>" + vrResult[i].szDamageInfo + "</td>";
                
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwStatus, "DEVDAMAGEREC_status") + "</td>";
                m_szOut += "<td>" + (vrResult[i].szRepareInfo) + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwRepareDate) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szRepareCom) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szRepareComTel) + "</td>";
                m_szOut += "<td>" + GetFee(vrResult[i].dwRepareCost) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szManName.ToString()) + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
           
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
