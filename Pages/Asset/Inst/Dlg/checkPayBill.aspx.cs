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
    protected bool bSet = false;
    protected string m_Title = "结算";
    protected uint payType = (uint)UNIBILL.DWUSABLEPAYKIND.PAYKIND_DIRECTCASH;
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
    protected string szUseTotalTime = "";
    protected string dwComsubleProperty = "";
    protected string szResvTimeTotal = "";
    protected string szSampleInfo = "";
    protected string szSampleInfoTitle = "";
    protected uint uSampleFeeTotal = 0;
    private uint uSampleLine = 1;//样品显示条数
    protected string szSampleLine = "";//样品显示条数
    protected string szPurpose = ""; 
	protected void Page_Load(object sender, EventArgs e)
    {
        uint uTotalSum = 0;
        szSampleLine = uSampleLine.ToString();
        if (!this.Page.IsPostBack)
        {
            string szId = Request["id"];
            string szHiddenSampleList = "";
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESVREQ vrGetResv = new RTRESVREQ();
            vrGetResv.dwResvID = (uint.Parse(szId));
            RTRESV[] vtRtResv;
            uResponse = m_Request.Reserve.GetRTResv(vrGetResv, out vtRtResv);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRtResv != null && vtRtResv.Length>0)
            {
                lblYuShou.Text = ((uint)vtRtResv[0].dwPrepayment/100).ToString();
                szUseTotalTime = "0分钟";//((uint)vtRtResv[0].dwRealUseTime)+"分钟";
                devID.Value = vtRtResv[0].dwDevID.ToString();
            }
            RTRESVBILLREQ vrGetbil = new RTRESVBILLREQ();
            vrGetbil.dwResvID = (uint.Parse(szId));
            RTRESVBILL vtResbill = new RTRESVBILL();
            uResponse = m_Request.Reserve.GetRTResvBill(vrGetbil, out vtResbill);
            RTBILL[] vtRtBill;
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                vtRtBill = vtResbill.BillInfo;
                GetUniFee(vtRtBill, out uTotalSum);
            }

            lblSum.Text = uTotalSum / 100 +"." +uTotalSum%100;
            uint uFeeReal = uTotalSum - 0;// (uint)vtRtResv[0].dwPrepayment;
            txtSum.Text = (uFeeReal / 100 + "." + uFeeReal % 100).ToString();

            RTRESVREQ vrGet = new RTRESVREQ();
            vrGet.dwResvID = Parse(szId);
            RTRESV[] vtRes;
            uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtRes);
            uint uMin = 0;
            uint uFeesn = 0;
            bool bCheck = false;//管理员是否陪同
            bool bIsSzum = false;//是否需要试剂
            bool bPei = false;
            uint uSumbule = 0;
            uint uSampleMoney = 0;//样品费
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                szDevName = vtRes[0].szDevName.ToString();
                szLabName = vtRes[0].szLabName.ToString();
                szTestName = vtRes[0].szTestName.ToString();
                imgPic.ImageUrl = "pic/" + vtRes[0].dwDevID + ".jpg";
                szRTName = vtRes[0].szRTName.ToString();
                szTutorName = vtRes[0].szLeaderName.ToString();
                szOwnerName = vtRes[0].szOwnerName.ToString();
              
                RESEARCHTEST research;
                if(GetResearchTestByID(out research,vtRes[0].dwRTID.ToString()))
                {
                    string szFounds = research.szFundsNo;
                    string[] szFoundslist = szFounds.Split(',');
                    for (int i = (szFoundslist.Length - 1); i > 0; i--)
                    {
                        if (szFoundslist[i] != "")
                        {
                            szFundsNo.Value = szFoundslist[i];
                            break;
                        }
                    }
                }
                uint uPurpose = (uint)vtRes[0].dwPurpose;
                if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_DEPT) > 0)
                {
                    szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_DEPT, "Fee_Ident");
                    kind.Value = "1";
                }
                else if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_INNER) > 0)
                {
                    szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER, "Fee_Ident");
                    kind.Value = "2";
                }
                else if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_OUTSIDE) > 0)
                {
                    szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER, "Fee_Ident");
                    kind.Value = "3";
                }

                if (vtRes[0].ResvSample != null && vtRes[0].ResvSample.Length > 0)
                {
                    int i = 0;
                    szSampleInfo = "<table style='width:100%' id='tblSamleList'>";
                    szSampleInfoTitle = "<table style='width:100%' id='tblSampleAddList'>";
                    for (i = 0; i < vtRes[0].ResvSample.Length; i++)
                    {
                        uSampleFeeTotal += (uint)(vtRes[0].ResvSample[i].dwUnitFee * vtRes[0].ResvSample[i].dwSampleNum);
                        szHiddenSampleList += vtRes[0].ResvSample[i].dwSampleSN + "," + vtRes[0].ResvSample[i].szSampleName + "," + (vtRes[0].ResvSample[i].dwUnitFee / 100) + "." + (vtRes[0].ResvSample[i].dwUnitFee % 100) + "," + vtRes[0].ResvSample[i].dwSampleNum + ";";
                        uint uTempSample = (uint)vtRes[0].ResvSample[i].dwUnitFee * (uint)vtRes[0].ResvSample[i].dwSampleNum;
                        if (i <= uSampleLine)
                        {
                            szSampleInfo += "<tr>";
                            szSampleInfo += "<td style='height:20px' data-id='" + vtRes[0].ResvSample[i].dwSampleSN.ToString() + "'>" + vtRes[0].ResvSample[i].szSampleName + "</td>";
                            szSampleInfo += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwUnitFee / 100 + "." + vtRes[0].ResvSample[i].dwUnitFee % 100 + "元/份</td>";
                            szSampleInfo += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwSampleNum + "份</td>";
                            szSampleInfo += "</tr>";
                        }


                        szSampleInfoTitle += "<tr>";
                        szSampleInfoTitle += "<td style='height:20px' data-num='" + vtRes[0].ResvSample[i].dwSampleNum.ToString() + "' data-uintFee='" + vtRes[0].ResvSample[i].dwUnitFee / 100 + "." + vtRes[0].ResvSample[i].dwUnitFee%100 + "' data-name='" + vtRes[0].ResvSample[i].szSampleName.ToString() + "' data-id='" + vtRes[0].ResvSample[i].dwSampleSN.ToString() + "'>" + vtRes[0].ResvSample[i].szSampleName + "</td>";
                        szSampleInfoTitle += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwUnitFee / 100 + "." + vtRes[0].ResvSample[i].dwUnitFee % 100 + "元/份</td>";

                        szSampleInfoTitle += "<td style='height:20px'>" +"<input class='setSampleNum' type='text' style='width:20px' value='"+vtRes[0].ResvSample[i].dwSampleNum + "' />份</td>";
                        szSampleInfoTitle += "<td><a class='delSample' style='width:25px' href='#' title='删除'><img style='width:25px;height:25px;' src='../../../../themes/iconpage/del.png'/></a></td>";
                        szSampleInfoTitle += "</tr>";

                        uSampleMoney += uTempSample;
                    }
                    hiddenSampleList.Value = szHiddenSampleList;
                    idTotalFee.InnerText=   (uSampleFeeTotal / 100 + "." + uSampleFeeTotal % 100).ToString();
                    lblSampleTotalReal.Text = idTotalFee.InnerText;
                    szSampleInfoTitle =szSampleInfoTitle + "</table>";
                    //if (i > uSampleLine)
                    {
                        szSampleInfo += "<tr>";
                        szSampleInfo += "<td colspan='4' style='height:20px'>" + "<a id='aSampleFee' href='#'>点击更过测试内容</a>" + "</td>";
                        szSampleInfo += "</tr>";
                    }

                    szSampleInfo += "</table>";
                }

                uFeesn = (uint)vtRes[0].dwFeeSN;
                szResvInfo = vtRes[0].szMemo.ToString();
                szManGroupName = vtRes[0].szManName.ToString();
                szResvTime = Get1970Date((uint)vtRes[0].dwBeginTime, "MM-dd HH:mm") + "至" + Get1970Date((uint)vtRes[0].dwEndTime, "MM-dd HH:mm");
                szResvTimeTotal = GetTimeForSecond((((uint)vtRes[0].dwEndTime - (uint)vtRes[0].dwBeginTime)));
                lblszResvTime.InnerText = szResvTime;
                dwEstimatedTime = GetTime((uint)vtRes[0].dwEstimatedTime);
                uMin = (uint)vtRes[0].dwEndTime - (uint)vtRes[0].dwBeginTime + 1;
                szResvTotalTime = (uMin / 86400).ToString();               
                dwBegin.Value = Get1970Date((uint)vtRes[0].dwBeginTime, "yyyy-MM-dd HH:mm");
                dwEnd.Value = Get1970Date((uint)vtRes[0].dwEndTime, "yyyy-MM-dd HH:mm");
                szGroupStudent = GetGroupMemberName((uint)vtRes[0].dwGroupID);
                UNIACCOUNT setTur;
                GetAccByAccno(vtRes[0].dwLeaderID.ToString(), out setTur);
                if (setTur.dwAccNo != null)
                {
                    szTurtorTel = setTur.szHandPhone.ToString() + ";" + setTur.szEmail.ToString();
                }
                UNIACCOUNT setOwen;
                GetAccByAccno(vtRes[0].dwOwner.ToString(), out setOwen);

                if (setTur.dwAccNo != null)
                {
                    szOwneTel = setOwen.szHandPhone.ToString() + ";" + setOwen.szEmail.ToString();
                }
                if ((((uint)vtRes[0].dwProperty) & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_MANDO)) > 0)
                {
                    dwProperty = "是";
                    bCheck = true;
                }
                else
                {
                    dwProperty = "否";
                    bCheck = false;
                }
                if ((((uint)vtRes[0].dwProperty) & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFCONSUMABLE)) > 0)
                {
                    dwComsubleProperty = "是";
                }
                else
                {
                    dwComsubleProperty = "否";
                }
                //TODO
                /*
                if (vtRes[0].dwSampleNum.ToString() != "")
                {
                    szConsumables = vtRes[0].dwSampleNum.ToString();
                    bIsSzum = true;
                }*/
                if (vtRes[0].szManName.ToString() != "")
                {
                    bPei = true;
                }

            }
        }
    }

    private string GetTime(uint Value)
    {
        string szValue = "";
        uint dwValue = Value;
        if (dwValue >= 1440)
        {
            szValue = (dwValue / 1440) + "天";
            dwValue = dwValue % 1440;

        }
        if (dwValue >= 60)
        {
            szValue += (dwValue / 60) + "小时";
            dwValue = dwValue % 60;
        }
        if (dwValue < 60 && dwValue > 0)
        {
            szValue += dwValue + "分钟";
        }
        return szValue;
    }
    public void GetUniFee(RTBILL[] vtFeeDetail, out uint uTotalSum)
    {

        uTotalSum = 0;
        for (int i = 0; i < vtFeeDetail.Length; i++)
        {
            uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;
            uint uConst = (uint)vtFeeDetail[i].dwReceivableCost;
           
            uint uFeeUint = (uint)vtFeeDetail[i].dwUnitFee;
            string szFeeUint = ChinaRound((uFeeUint * 60 * 1.0 / (100 * 1440)), 2).ToString();

            string uTotal = uConst / 100 + "." + uConst % 100;
            if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
            {
                lblUseDevFee.Text = szFeeUint;
                lblUseDevTotal.Text = uTotal.ToString();
                lblUseDevTotalReal.Text = uTotal.ToString();
                divUseDev.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_OCCUPY))
            {
                lblOccupy.Text = szFeeUint;
                lblOccupyTotal.Text = uTotal.ToString();
                lblOccupyTotalReal.Text = uTotal.ToString();
                divOccupy.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ASSIST))
            {
                lblASSIS.Text = szFeeUint;
                lblASSISTotal.Text = uTotal.ToString();
                lblASSISTotalReal.Text = uTotal.ToString();
                divASSIST.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT))
            {
                lblTIMEOUT.Text = szFeeUint;
                lblTIMEOUTTotal.Text = uTotal.ToString();
                lblTIMEOUTTotalReal.Text = uTotal.ToString();
                divTIMEOUT.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                lblCONSUMABLE.Text = "";// szFeeUint;
                lblCONSUMABLETotal.Text = uTotal.ToString();
                lblCONSUMABLETotalReal.Text = uTotal.ToString();
                divCONSUMABLE.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
            {
                lblSample.Text = ((uint)vtFeeDetail[i].dwUnitFee / 100).ToString(".00");
                lblSampleTotal.Text = uTotal.ToString();
                lblSampleTotalReal.Text = uTotal.ToString();
                divSample.Style.Clear();
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_RESVDEV))
            {
                lblRESVDEV.Text = szFeeUint;
                lblRESVDEVTotal.Text = uTotal.ToString();
                lblRESVDEVTotalReal.Text = uTotal.ToString();
                divRESVDEV.Style.Clear();

            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST))
            {
                lblENTRUST.Text = ((uint)vtFeeDetail[i].dwUnitFee / 100).ToString(".0");
                lblENTRUSTTotal.Text = uTotal.ToString();
                lblENTRUSTTotalReal.Text = uTotal.ToString();
                divENTRUST.Style.Clear();
            }
            uTotalSum += uConst;
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        string szId = Request["id"];
        uint uTotal = 0;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTBILLSETTLE setValue = new RTBILLSETTLE();
        setValue.dwResvID = (uint.Parse(szId));
        RTBILL[] vtRTBill = GetUniFeeVT(out uTotal);
        if (vtRTBill != null)
        {
            setValue.BillInfo = vtRTBill;
        }
        uint uP = 0;
        setValue.dwPayKind = (payType);
        setValue.dwTotalCost = (uTotal);
        setValue.ResvSample = SetRTResvSampleFee(szId);
        setValue.szFundsNo = szFundsNo.Value;
        uResponse = m_Request.Reserve.RTBillSettle(setValue);
  
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox("结算成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            billResearve(szId);
        }
        else
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.OK);
        }
    }
    private void billResearve(string szId)
    {
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

        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && uResponse2 == REQUESTCODE.EXECUTE_SUCCESS && vtRtresv != null && vtRtresv.Length > 0)
        {
            RTBILL[] vtRtBill = vtRes.BillInfo;
            DEVFARREQ devFarGet = new DEVFARREQ();
            devFarGet.dwDevID = vtRtresv[0].dwDevID;
            DEVFAR[] vtDevFarRes;
            uResponse = m_Request.Device.DevFARGet(devFarGet, out vtDevFarRes);
            GetUniFee(vtRtBill, vtDevFarRes);
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
            uint uTemp1 = 0;
            uint uTemp2 = 0;
            for (m = 0; vtDevFar != null && m < vtDevFar.Length; m++)
            {
                if (uFeeType == (uint)vtDevFar[m].dwFeeType)
                {
                    uTestRate = (uint)vtDevFar[m].dwTestRate;
                    uOpenRate = (uint)vtDevFar[m].dwOpenFundRate;
                    uServiceRate = (uint)vtDevFar[m].dwServiceRate;
                }
            }
            if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV) && uRealConst != 0)
            {
                uSum = uSum + uRealConst;
                uTemp1 = (uint)ChinaRound((double)(uRealConst * uTestRate * 1.0) / 100, 2);
                uTemp2 = (uint)ChinaRound((double)(uRealConst * uOpenRate * 1.0) / 100, 2);
                uTestSum = uTestSum + uTemp1;
                uOpenSum = uOpenSum + uTemp2;
                uServiceSum = uServiceSum + uRealConst - uTemp1 - uTemp2;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE) && uRealConst != 0)
            {
                uSum = uSum + uRealConst;
                uTemp1 = (uint)ChinaRound((double)(uRealConst * uTestRate * 1.0) / 100, 2);
                uTemp2 = (uint)ChinaRound((double)(uRealConst * uOpenRate * 1.0) / 100, 2);
                uTestSum = uTestSum + uTemp1;
                uOpenSum = uOpenSum + uTemp2;
                uServiceSum = uServiceSum + uRealConst - uTemp1 - uTemp2;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE) && uRealConst != 0)
            {
                uSum = uSum + uRealConst;
                uTemp1 = (uint)ChinaRound((double)(uRealConst * uTestRate * 1.0) / 100, 2);
                uTemp2 = (uint)ChinaRound((double)(uRealConst * uOpenRate * 1.0) / 100, 2);
                uTestSum = uTestSum + uTemp1;
                uOpenSum = uOpenSum + uTemp2;
                uServiceSum = uServiceSum + uRealConst - uTemp1 - uTemp2;
            }
            else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST) && uRealConst != 0)
            {
                uSum = uSum + uRealConst;
                uTemp1 = (uint)ChinaRound((double)(uRealConst * uTestRate * 1.0) / 100, 2);
                uTemp2 = (uint)ChinaRound((double)(uRealConst * uOpenRate * 1.0) / 100, 2);
                uTestSum = uTestSum + uTemp1;
                uOpenSum = uOpenSum + uTemp2;
                uServiceSum = uServiceSum + uRealConst - uTemp1 - uTemp2;
            }
        }
        RTBILLRECEIVE setValue = new RTBILLRECEIVE();
        setValue.dwTotalCost = uSum;
        setValue.dwOpenFundFee = uOpenSum;
        setValue.dwTestFee = uTestSum;
        setValue.dwResvID = Parse(Request["id"]);
        setValue.dwReceiveDate = GetDate(DateTime.Now.ToString("yyyy-MM-dd"));
        setValue.dwServiceFee = (uSum - uOpenSum - uTestSum);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_SUCCESS;
        uResponse = m_Request.Reserve.RTBillReceive(setValue);
    }
    private RESVSAMPLE[] SetRTResvSampleFee(string szResvID)
    {
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwResvID =Parse(szResvID);
        string szFeeList = hiddenSampleList.Value;
        RTRESV[] vtRes;
        if (m_Request.Reserve.GetRTResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            string[] szSampleList = szFeeList.Split(';');
            vtRes[0].ResvSample = new RESVSAMPLE[szSampleList.Length-1];
            for (int i = 0; i < szSampleList.Length-1; i++)
            {
                RESVSAMPLE temp=new RESVSAMPLE();

                if (szSampleList[i] != "")
                {
                    string[] sampleListTemp=szSampleList[i].Split(',');
                    temp.dwResvID=vtRes[0].dwResvID;
                    temp.dwSampleSN=Parse(sampleListTemp[0]);
                    temp.dwSampleNum = Parse(sampleListTemp[3]);
                    vtRes[0].ResvSample[i] = temp;
                }
            }
            if (vtRes[0].ResvSample == null || vtRes[0].ResvSample.Length == 0)
            {
                return null;
            }
            return vtRes[0].ResvSample;
        }
        return null;
    }
    public RTBILL[] GetUniFeeVT(out uint uTotal)
    {
        string szId = Request["id"];
        uTotal = 0;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTRESVBILLREQ vrGet = new RTRESVBILLREQ();
        vrGet.dwResvID = (uint.Parse(szId));
        RTRESVBILL vtRes = new RTRESVBILL();
        uResponse = m_Request.Reserve.GetRTResvBill(vrGet, out vtRes);

        ArrayList list = new ArrayList();
       
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            RTBILL[] vtFeeDetail = vtRes.BillInfo;
            for (int i = 0; vtFeeDetail != null && i < vtFeeDetail.Length; i++)
            {
                RTBILL billTem = new RTBILL();
                billTem = vtFeeDetail[i];
                uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;
                uint uFee = ((uint)vtFeeDetail[i].dwUnitFee/100);
                billTem.dwFeeType = (uFeeType);
                billTem.dwUnitFee = (uFee);
                billTem.dwPayKind = (payType);
                billTem.dwResvID = (uint.Parse(szId));
                double uReal = 0;
                if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
                {
                    double.TryParse(lblUseDevTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_OCCUPY))
                {
                    double.TryParse(lblOccupyTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ASSIST))
                {
                    double.TryParse(lblASSISTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT))
                {
                    double.TryParse(lblTIMEOUTTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
                {
                    double.TryParse(lblCONSUMABLETotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
                {
                    double.TryParse(lblSampleTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_RESVDEV))
                {
                    double.TryParse(lblRESVDEVTotalReal.Text.ToString(), out uReal);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST))
                {
                    double.TryParse(lblENTRUSTTotalReal.Text.ToString(), out uReal);
                }
                uint uTemp =(uint)(uReal * 100);
                uTotal = uTotal + uTemp;
                billTem.dwRealCost = (uTemp);
                list.Add(billTem);
            }
        }

        RTBILL[] vtFeeDetailRes = new RTBILL[list.Count];
        for(int i=0;i<list.Count;i++)
        {
            vtFeeDetailRes[i] = new RTBILL();
            vtFeeDetailRes[i] = (RTBILL)list[i];
        }
        return vtFeeDetailRes;
    }
}
