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

public partial class _Default : UniWebLib.UniPage
{
    protected string szDevName = "";
    protected string szLabName = "";
    protected string szTestName = "";
    protected string szRTName = "";
    protected string szTutorName = "";
    protected string szOwnerName = "";
    protected string dwProperty = "";
    protected string szConsumables = "";
    protected string szTurtorTel = "";
    protected string szOwneTel = "";
    protected string dwEstimatedTime = "";
    protected string szResvTime = "";
    protected string szManGroupName = "";
    protected string szGroupStudent = "";
    protected string szResvInfo = "";
    protected string szResvTotalTime = "";
    protected string szTotalHtml = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uTotalSum = 0;
        if (!this.Page.IsPostBack)
        {
            string szId = Request["id"];

            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            REQUESTCODE uResponse2 = REQUESTCODE.EXECUTE_FAIL;
            RTRESVBILLREQ vrGet = new RTRESVBILLREQ();
            vrGet.dwResvID = (uint.Parse(szId));
            RTRESVBILL vtRes = new RTRESVBILL();
            uResponse = m_Request.Reserve.GetRTResvBill(vrGet, out vtRes);
           
            RTRESVREQ rtResvGet = new RTRESVREQ();
            rtResvGet.dwResvID = Parse(szId);
            RTRESV[] vtRtresv;
            uResponse2 = m_Request.Reserve.GetRTResv(rtResvGet, out vtRtresv);        

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS&&uResponse2 == REQUESTCODE.EXECUTE_SUCCESS && vtRtresv!=null&& vtRtresv.Length>0)
            {
                RTBILL[] vtRtBill  = vtRes.BillInfo;                
                DEVFARREQ devFarGet = new DEVFARREQ();
                devFarGet.dwDevID = vtRtresv[0].dwDevID;
                DEVFAR[] vtDevFarRes;
                uResponse = m_Request.Device.DevFARGet(devFarGet, out vtDevFarRes);
                GetUniFee(vtRtBill, vtDevFarRes);
            }
        }
       
    }
    public void GetUniFee(RTBILL[] vtFeeDetail, DEVFAR[] vtDevFar)
    {
       
        uint uTestSum = 0;
        uint uOpenSum = 0;
        uint uServiceSum = 0;
        uint uSum = 0;
        for (int i = 0; i < vtFeeDetail.Length; i++)
        {
            uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;
            uint uRealConst = (uint)vtFeeDetail[i].dwRealCost;
            int m = 0;
            uint uTestRate = 0;
            uint uOpenRate = 0;
            uint uServiceRate = 0;
            uint uTemp = 0;//两个分单价的合计
            for (m = 0; vtDevFar!=null&&m < vtDevFar.Length; m++)
            {
                if (uFeeType == (uint)vtDevFar[m].dwFeeType)
                {
                    uTestRate = (uint)vtDevFar[m].dwTestRate;
                    uOpenRate = (uint)vtDevFar[m].dwOpenFundRate;
                    uServiceRate = (uint)vtDevFar[m].dwServiceRate;
                }
            }
            string szTotal = uRealConst / 100 + "." + uRealConst % 100 + "元";
            double dUse=ChinaRound(((uRealConst * 1.0 * uTestRate) / 10000), 2);
            double dOpen=ChinaRound(((uRealConst * 1.0 * uOpenRate) / 10000), 2);
            string szuseRate = dUse.ToString() + "元(" + uTestRate.ToString() + "%)";
            string szOpenRate = dOpen.ToString() + "元(" + uOpenRate.ToString() + "%)";
            uTemp = uRealConst-(uint)(dUse * 100) - (uint)(dOpen * 100);
            string szSerRate = uTemp / 100 + "." + uTemp % 100 + "元(" + uServiceRate.ToString() + "%)"; ;
            if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
            {
                uSum = uSum + uRealConst;
                devUseTotal.Text = szTotal;
                useUseRate.Text = szuseRate;
                uTestSum = uTestSum + (uRealConst * uTestRate);
                useOpenRate.Text = szOpenRate;
                uOpenSum = uOpenSum + (uRealConst * uOpenRate);
                useServiceRate.Text = szSerRate;
                uServiceSum = uServiceSum + uTemp;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                uSum = uSum + uRealConst;
                sampleTotal.Text = szTotal;
                sampleTestRate.Text = szuseRate;
                uTestSum = uTestSum + (uRealConst * uTestRate);
                sampleOpenRate.Text = szOpenRate;
                uOpenSum = uOpenSum + (uRealConst * uOpenRate);
                sampleServiceRate.Text = szSerRate;
                uServiceSum = uServiceSum + uTemp;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                uSum = uSum + uRealConst;
                conTotal.Text = szTotal;
                conTestRate.Text = szuseRate;
                uTestSum = uTestSum + (uRealConst * uTestRate);
                conOpenRate.Text = szOpenRate;
                uOpenSum = uOpenSum + (uRealConst * uOpenRate);
                conServiceRate.Text = szSerRate;
                uServiceSum = uServiceSum + uTemp;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST))
            {
                uSum = uSum + uRealConst;
                entTotal.Text = szTotal;
                entTestRate.Text = szuseRate;
                uTestSum = uTestSum + (uRealConst * uTestRate);
                entOpenRate.Text = szOpenRate;
                uOpenSum = uOpenSum + (uRealConst * uOpenRate);
                entServiceRate.Text = szSerRate;
                uServiceSum = uServiceSum + uTemp;
            }
        }
        double dUstTotal=ChinaRound(((uTestSum * 1.0) / 10000),2);
        double dTestTotal= ChinaRound(((uOpenSum * 1.0) / 10000),2);
        double dSum=ChinaRound(((uSum * 1.0) / 100), 2);
        UseTotal.Text = dUstTotal.ToString() + "元";
        TestTotal.Text = dTestTotal.ToString() + "元";
        ServicesTotal.Text = (dSum -dTestTotal- dUstTotal).ToString() + "元";
        Total.Text =dSum.ToString() + "元";
        szTotalHtml =dSum.ToString() + "元";
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        RTBILLRECEIVE setValue = new RTBILLRECEIVE();
        double fTemp = 0;
        int nTemp=3;
        double.TryParse(UseTotal.Text.Replace("元", ""), out fTemp);
        fTemp=ChinaRound(fTemp, nTemp)*100;
        setValue.dwTestFee = uint.Parse(fTemp.ToString());

        double.TryParse(TestTotal.Text.Replace("元", ""), out fTemp);
        fTemp = ChinaRound(fTemp, nTemp) * 100;
        setValue.dwOpenFundFee = uint.Parse(fTemp.ToString());

        double.TryParse(ServicesTotal.Text.Replace("元", ""), out fTemp);
        fTemp = ChinaRound(fTemp, nTemp) * 100;
        setValue.dwServiceFee = uint.Parse(fTemp.ToString());

        double.TryParse(Total.Text.Replace("元", ""), out fTemp);
        fTemp = ChinaRound(fTemp, nTemp) * 100;
        setValue.dwTotalCost = uint.Parse(fTemp.ToString());

        setValue.dwReceiveDate = Parse(DateTime.Now.ToString("yyyMMdd"));
        setValue.dwResvID = Parse(Request["id"]);
      
        
        string szId = Request["id"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_SUCCESS;
        uResponse = m_Request.Reserve.RTBillReceive(setValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox("入账成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        }
        else
        {
            MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.OK);
        }
    }
}
