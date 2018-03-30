using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_zd_nxy_testDataRec : UniClientPage
{
    protected string testData = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            testData=GetdataList();
        }
    }
    protected string GetdataList()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TESTDATAREQ vrReq = new TESTDATAREQ();
        //
        string start = Request["start"];
        string end = Request["end"];
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end)) return "";
        uint intStartTime = ToUInt(start.Replace("-", ""));
        uint intEndTime = ToUInt(end.Replace("-", ""));
        vrReq.dwStartDate = intStartTime;
        vrReq.dwEndDate = intEndTime;
        //
        //UNIACCOUNT AccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //vrReq.dwAccNo = AccInfo.dwAccNo;
        vrReq.dwStatus = (uint)UNITESTDATA.DWSTATUS.TDSTAT_UPLOADED | (uint)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED;
        vrReq.szReqExtInfo.szOrderKey = "dwSubmitDate";
        vrReq.szReqExtInfo.szOrderMode = "DESC";
        UNITESTDATA[] vtResult;
        uResponse = m_Request.Account.TestDataGet(vrReq, out vtResult);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            string szErrMsg = "获取失败:" + m_Request.szErrMessage;
            return szErrMsg;
        }
        else
        {
            string szHtml = "";
            if (vtResult.Length == 0)
            {
                szHtml = "<tr><td colspan='10' style='padding:10px;width:100%;text-align:center;'>无上传记录,列表为空</td></tr>";
            }
            else
            {
                szHtml = "";
                for (int i = 0; i < vtResult.Length; i++)
                {
                    uint dwSubmitDate = (uint)vtResult[i].dwSubmitDate;
                    string szDate = (dwSubmitDate / 10000) + "-" + ((dwSubmitDate / 100) % 100) + "-" + (dwSubmitDate % 100) + " " + Get1970Time((uint)vtResult[i].dwSubmitTime);
                    if (i % 2 == 0)
                        szHtml += "<tr class='odd'>";
                    else
                        szHtml += "<tr>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szDisplayName.ToString() + "</td>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szTrueName + "</td>";
                    szHtml += "<td class='tbltd'>" + szDate + "</td>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szDevName.ToString() + "</td>";
                    szHtml += "<td class='tbltd'>" + ConvertSize(vtResult[i].dwFileSize) + "</td>";
                    szHtml += "<td class='tbltd status' stat='" + vtResult[i].dwStatus + "'>" + ConvertSta(vtResult[i].dwStatus) + "</td>";
                    szHtml += "<td class='tbltd'><a class='click' onclick='window.open(\"dlData.aspx?id=" + vtResult[i].dwSID.ToString() + "\");setTimeout(\"location.href=location.href;\",500);'>下载</a> | " +
                        "<a class='click' onclick='delAct(\"del_data\",\"" + vtResult[i].dwSID.ToString() + "\")'>删除</a></td>";
                    szHtml += "</tr>";
                }
            }
            return szHtml;
        }
    }


    private string ConvertSta(object p)
    {
        if ((Convert.ToInt32(p) & (int)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED) > 0)
        {
            return "<span class='green'>已下载</span>";
        }
        if ((Convert.ToInt32(p) & (int)UNITESTDATA.DWSTATUS.TDSTAT_FILEDEL) > 0)
        {
            return "<span class='red'>已删除</span>";
        }
        else
        {
            return "<span class='orange'>未下载</span>";
        }
    }
    private string ConvertSize(object p)
    {
        int k = Convert.ToInt32(p) / 1024;
        if (k == 0)
        {
            k = 1;
        }
        return k.ToString() + "K";
    }
    private string Get1970Time(uint TotalSeconds)//根据差距秒数 算出现在是时间
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("HH:mm");
    }
}