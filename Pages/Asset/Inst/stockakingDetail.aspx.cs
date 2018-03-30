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
    protected string sz_Stocking = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        STDETAILREQ vrParameter = new STDETAILREQ();
        STDETAIL[] vrResult;
        string szOp=Request["op"];
        if (szOp != null && szOp == "setNormail")
        {
            string szID = Request["delID"];
            setNormail(szID);
        }
        uint uSTID = 0;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        STOCKTAKINGREQ vrGet = new STOCKTAKINGREQ();
        vrGet.dwStartDate = 20100101;
        vrGet.dwEndDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        STOCKTAKING[] vtRes;
        if (m_Request.Assert.StockTakingGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            if (!IsPostBack)
            {
                uSTID = (uint)vtRes[0].dwSTID;
            }
            for (int i = 0; i < vtRes.Length; i++)
            {
                sz_Stocking += GetInputItemHtml(CONSTHTML.option, "", vtRes[i].szMemo, vtRes[i].dwSTID.ToString());
            }
        }
        uint uSTIDRqu=Parse(Request["dwSTID"]);
        if (uSTIDRqu != 0)
        {
            vrParameter.dwSTID = uSTIDRqu;
        }
        else
        {
            vrParameter.dwSTID = uSTID;
        }
        if (m_Request.Assert.STDetailGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-devid='"+vrResult[i].dwDevID+"' data-id=\"" + vrResult[i].dwSTID.ToString() + "\">" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUnitPrice+ "</td>";
                m_szOut += "<td>" + vrResult[i].szClassName + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLeaderName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwSTStat, "StockingDetail_Status") + "</td>";
                m_szOut += "<td>" + vrResult[i].szSTInfo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Assert);
        }
       
        PutBackValue();
    }
    private void setNormail(string szID)
    {
        STDETAILREQ vrGet=new STDETAILREQ();
        vrGet.dwSTID=Parse(szID);
        uint uDevID = Parse(Request["devid"]);
        STDETAIL[] vtRes;
        if (m_Request.Assert.STDetailGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (uDevID == ((uint)vtRes[i].dwDevID))
                {
                    STDETAIL doDetail = vtRes[i];
                    doDetail.dwSTStat = (uint)STDETAIL.DWSTSTAT.STSTAT_OK;
                    doDetail.szSTInfo = "";
                    m_Request.Assert.STDetailDo(doDetail);
                    break;
                }
            }
        }
    }
}
