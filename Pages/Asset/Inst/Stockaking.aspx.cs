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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string sz_Staues = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        STOCKTAKINGREQ vrParameter = new STOCKTAKINGREQ();
        STOCKTAKING[] vrResult;
        string szOp=Request["op"];
        string szID = Request["delID"];
        if (szOp == "del")
        {
            Del(szID);
        }
        else if (szOp == "over")
        {
            SetOver(szID);
        }
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        sz_Staues = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Stocking_Status", true);
        if (vrParameter.dwSTStat!=null&&((uint)vrParameter.dwSTStat) == 0)
        {
            vrParameter.dwSTStat = null;
        }
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            vrParameter.dwStartDate = uint.Parse(DateTime.Now.AddDays(-5).ToString("yyyyMMdd"));
            vrParameter.dwEndDate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
        }
        if (m_Request.Assert.StockTakingGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-name='" + vrResult[i].szMemo+ "' data-id=\"" + vrResult[i].dwSTID.ToString() + "\">" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwSTDate) + "</a></td>";
                if (vrResult[i].szKindName.ToString() != "")
                {
                    m_szOut += "<td>" + vrResult[i].szKindName + "</a></td>";
                }
                else {
                    m_szOut += "<td>无限制</td>";
                }
                if (vrResult[i].szRoomName.ToString() != "")
                {
                    m_szOut += "<td>" + vrResult[i].szRoomName.ToString() + "</td>";
                }
                else
                {
                    m_szOut += "<td>无限制</td>";
                }
                if (vrResult[i].dwMinUnitPrice != 0 && vrResult[i].dwMaxUnitPrice != 0)
                {
                    m_szOut += "<td>" + vrResult[i].dwMinUnitPrice.ToString() + "到" + vrResult[i].dwMaxUnitPrice.ToString() + "</td>";
                }
                else if (vrResult[i].dwMinUnitPrice == 0 && vrResult[i].dwMaxUnitPrice != 0)
                {
                    m_szOut += "<td>小于" + vrResult[i].dwMaxUnitPrice + "</td>";
                }
                else if (vrResult[i].dwMinUnitPrice != 0 && vrResult[i].dwMaxUnitPrice == 0)
                {
                    m_szOut += "<td>大于" + vrResult[i].dwMinUnitPrice + "</td>";
                }
                else
                {
                    m_szOut += "<td>无限制</td>";
                }
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwSTStat, "Stocking_Status") + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwSTEndDate) + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Assert);
        }
       
        PutBackValue();
    }
    private void Del(string szID)
    {
 
    }
    private void SetOver(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        STOCKTAKINGREQ vrParameter = new STOCKTAKINGREQ();
        vrParameter.dwSTID = Parse(szID);
        STOCKTAKING[] vrResult;
        if (m_Request.Assert.StockTakingGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS&&vrResult!=null&&vrResult.Length>0)
        {
         
            STOCKTAKING setValue=vrResult[0];
            setValue.dwSTEndDate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
            setValue.dwSTStat = (uint)STOCKTAKING.DWSTSTAT.STSTAT_DONE;
            m_Request.Assert.StockTakingDo(setValue, out setValue);
            
        }
    }
}
