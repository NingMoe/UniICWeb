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

public partial class _Default : UniPage
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
   
    protected string dwComsubleProperty = "";
    protected uint uFeeTime = 1440;//费率每小时的走 如果改成小时 改为60；目前设置上为每天，显示为每小时
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uTotalSum = 0;
        if (!this.Page.IsPostBack)
        {
            string szId = Request["id"];

            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESVBILLREQ vrGetbill = new RTRESVBILLREQ();
            vrGetbill.dwResvID = (uint.Parse(szId));
            RTRESVBILL vtResbill = new RTRESVBILL();
            uResponse = m_Request.Reserve.GetRTResvBill(vrGetbill, out vtResbill);
           
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                RTBILL[] vtRt = vtResbill.BillInfo;
                GetUniFee(vtRt, out uTotalSum);
            }
            idYshou.Text = uTotalSum / 100 + "." + uTotalSum % 100;
        }
       
    }


    public void GetUniFee(RTBILL[] vtFeeDetail, out uint uTotalSum)
    {

        uTotalSum = 0;
        for (int i = 0; i < vtFeeDetail.Length; i++)
        {
            uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;
            uint uConst=(uint)vtFeeDetail[i].dwReceivableCost;
            uTotalSum += uConst;
            
            uint uFeeUint = (uint)vtFeeDetail[i].dwUnitFee;
            string szFeeUint = ChinaRound((uFeeUint * 60 * 1.0 / (100*1440)), 2).ToString();

            string uTotal = uConst / 100 +"."+ uConst%100 + "元";
            if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
            {
                lblUseDevFee.Text = szFeeUint;
                lblUseDevTotal.Text = uTotal.ToString();
                divUseDev.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_OCCUPY))
            {
                lblOccupy.Text = szFeeUint;
                lblOccupyTotal.Text = uTotal.ToString();
                divOccupy.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ASSIST))
            {

                lblASSIS.Text = szFeeUint;
                lblASSISTotal.Text = uTotal.ToString();
                divASSIST.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT))
            {

                lblTIMEOUT.Text = szFeeUint;
                lblTIMEOUTTotal.Text = uTotal.ToString();

                divTIMEOUT.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                lblCONSUMABLE.Text = "";// szFeeUint;
                lblCONSUMABLETotal.Text = uTotal.ToString();
                divCONSUMABLE.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                lblSample.Text = "";// ((uint)vtFeeDetail[i].dwUnitFee / 100).ToString(".00");
                lblSampleTotal.Text = uTotal.ToString();
                divSample.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_RESVDEV))
            {
                lblRESVDEV.Text = szFeeUint;
                lblRESVDEVTotal.Text = uTotal.ToString();
                divRESVDEV.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST))
            {
                lblENTRUST.Text = szFeeUint;
                lblENTRUSTTotal.Text = uTotal.ToString();
                divENTRUST.Style.Clear();
            }

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        string szId = Request["id"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTPREPAY setValue = new RTPREPAY();
        setValue.dwResvID = (uint.Parse(szId));
        setValue.dwPrepayment = (uint)(double.Parse(idYshou.Text)*100);
        uResponse = m_Request.Reserve.PrepayRTResv(setValue);

        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox("预收费成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        }
        else
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.OK);
        }
    }
}
