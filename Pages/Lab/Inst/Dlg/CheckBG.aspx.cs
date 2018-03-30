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
    protected string m_Title = "审核";

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
    protected string szSampleInfo = "";
    protected string szSampleInfoTitle = "";
    protected string szPurpose = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
          
            string szId = Request["id"];
            szidh.Value = szId;
            
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESVREQ vrGet = new RTRESVREQ();
            vrGet.dwResvID = Parse(szId);
            RTRESV[] vtRes;
            uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtRes);
            uint uMin = 0;
            uint uFeesn = 0;
            bool bCheck = false;//是否代检
            bool bIsSzum = false;//是否需要耗材
            bool bPei = false;
            uint uSumbule = 0;
            uint uSampleMoney = 0;//样品费
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length>0)
            {
                szDevName = vtRes[0].szDevName.ToString();
                szLabName = vtRes[0].szLabName.ToString();                
                szTestName = vtRes[0].szTestName.ToString();
                imgPic.ImageUrl = "../../../clientweb/upload/devimg/" + vtRes[0].dwDevSN.ToString() + ".jpg";
                szRTName = vtRes[0].szRTName.ToString();
                szTutorName = vtRes[0].szLeaderName.ToString();
                szOwnerName = vtRes[0].szOwnerName.ToString();
                //TODO
                //uSumbule = ((uint)vtRes[0].dwSampleNum);
                uint uSampleLine = 5;//样品显示条数
                if (vtRes[0].ResvSample != null && vtRes[0].ResvSample.Length > 0)
                {
                    uint uPurpose=(uint)vtRes[0].dwPurpose;
                    if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_DEPT) > 0)
                    {
                        szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_DEPT,"Fee_Ident");
                    }
                    else if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_INNER) > 0)
                    {
                        szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER, "Fee_Ident");
                    }
                    else if ((uPurpose & (uint)UNIRESERVE.DWPURPOSE.USEBY_OUTSIDE) > 0)
                    {
                        szPurpose = GetJustNameEqual((uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER, "Fee_Ident");
                    }

                    int i = 0;
                    szSampleInfo = "<table style='width:100%'>";
                    for (i= 0; i < vtRes[0].ResvSample.Length; i++)
                    {
                        uint uTempSample = (uint)vtRes[0].ResvSample[i].dwUnitFee * (uint)vtRes[0].ResvSample[i].dwSampleNum;
                        if (i <= uSampleLine)
                        {
                            szSampleInfo += "<tr>";
                            szSampleInfo += "<td style='height:20px'>" + vtRes[0].ResvSample[i].szSampleName + "</td>";
                            szSampleInfo += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwUnitFee / 100 + "." + vtRes[0].ResvSample[i].dwUnitFee % 100 + "元/份</td>";

                            szSampleInfo += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwSampleNum + "份</td>";
                            szSampleInfo += "<td style='height:20px'>" + uTempSample / 100 + "." + uTempSample % 100 + "元</td>";
                            szSampleInfo += "</tr>";
                        }
                        else
                        {
                            szSampleInfoTitle += "<tr>";
                            szSampleInfoTitle += "<td style='height:20px'>" + vtRes[0].ResvSample[i].szSampleName + "</td>";
                            szSampleInfoTitle += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwUnitFee / 100 + "." + vtRes[0].ResvSample[i].dwUnitFee % 100 + "元/份</td>";

                            szSampleInfoTitle += "<td style='height:20px'>" + vtRes[0].ResvSample[i].dwSampleNum + "份</td>";
                            szSampleInfoTitle += "<td style='height:20px'>" + uTempSample / 100 + "." + uTempSample % 100 + "元</td>";
                            szSampleInfoTitle += "</tr>";
                        }
                        uSampleMoney += uTempSample;
                    }
                    szSampleInfoTitle = szSampleInfo + szSampleInfoTitle + "</table>";
                    if (i > uSampleLine)
                    {
                        szSampleInfo += "<tr>";
                        szSampleInfo += "<td colspan='4' style='height:20px'>" + "<a id='aSampleFee' href='javascript:shownSampleFee();return;'>点击显示更多测试内容</a>" + "</td>";
                        szSampleInfo += "</tr>";
                    }
                    
                    szSampleInfo += "</table>";
                }
                
               lblSampleTotal.InnerText=uSampleMoney / 100 +"."+ uSampleMoney % 100;
                uFeesn = (uint)vtRes[0].dwFeeSN;
                szResvInfo = vtRes[0].szMemo.ToString();
                szManGroupName = vtRes[0].szManName.ToString();
                szResvTime = Get1970Date((uint)vtRes[0].dwBeginTime, "yyyy-MM-dd HH:mm") + "至" + Get1970Date((uint)vtRes[0].dwEndTime, "yyyy-MM-dd HH:mm") + "；共" + GetTime((((uint)vtRes[0].dwEndTime - (uint)vtRes[0].dwBeginTime) / 60)) + "";
                lblszResvTime.InnerText = szResvTime;
                dwEstimatedTime = GetTime((uint)vtRes[0].dwEstimatedTime);
                uMin = (uint)vtRes[0].dwEndTime - (uint)vtRes[0].dwBeginTime;
                szResvTotalTime = (uMin / 86400).ToString();
                szResvT.Value = szResvTotalTime;
                dwBegin.Value = Get1970Date((uint)vtRes[0].dwBeginTime, "yyyy-MM-dd HH:mm");
                dwEnd.Value = Get1970Date((uint)vtRes[0].dwEndTime, "yyyy-MM-dd HH:mm");            
                szGroupStudent =GetGroupMemberName((uint)vtRes[0].dwGroupID);
                 UNIACCOUNT  setTur;
                GetAccByAccno(vtRes[0].dwLeaderID.ToString(),out setTur);
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
                if ((((uint)vtRes[0].dwProperty) &((uint)UNIRESERVE.DWPROPERTY.RESVPROP_MANDO)) > 0)
                {
                    dwProperty = "是";
                    bCheck = true;
                    ViewState["dwProperty"] = "true";
                }
                else
                {
                    dwProperty = "否";
                    bCheck = false;
                    ViewState["dwProperty"] = "false";
                }
                if ((((uint)vtRes[0].dwProperty) & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFCONSUMABLE)) > 0)
                {
                    dwComsubleProperty="是";
                    bIsSzum = true;
                    ViewState["dwProperty2"] = "true";
                }
                else
                {
                    dwComsubleProperty="否";
                    bIsSzum = false;
                    ViewState["dwProperty2"] = "false";
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
            GetUniFee(uFeesn.ToString(), uMin, bCheck, bIsSzum, bPei, uSumbule,uSampleMoney);
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
    public void GetUniFee(string szFeeSN,uint uMin,bool bCheck,bool bIsSzum,bool bPei,uint uSumbule,uint uSampleMoney)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        string szfeeType = "";
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN = Parse(szFeeSN);
        UNIFEE[] vtRes;
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        double uFeeTotal = 0;
        uFeeTotal = uFeeTotal + uSampleMoney / 100 + uSampleMoney % 100;
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            UNIFEE setFee = new UNIFEE();
            setFee = vtRes[0];
            FEEDETAIL[] vtFeeDetail = setFee.szFeeDetail;            
            string uTotal =GetTimeForSecond(uMin);
            for (int i = 0; i < vtFeeDetail.Length; i++)
            {
                uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;
                uint uFeeUint = (uint)vtFeeDetail[i].dwUnitFee;
                uint uFeeTime = (uint)vtFeeDetail[i].dwUnitTime;
                if (uFeeTime == 0)
                {
                    uFeeTime = 1;
                }
                double fUint60 = double.Parse(((uFeeUint * 60 * 1.0) / (100*uFeeTime)).ToString("F2"));                
                double fTotalTemp=fUint60*uMin/(60*60);

                string szFeeTemp = ChinaRound(fTotalTemp, 2).ToString();
                string szFeeUint = ((uFeeUint * 1.0) / 100 * 60 / uFeeTime).ToString("0.00"); ;
                if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
                {
                    lblUseDevTotal.InnerText = szFeeTemp;
                    hiddenUseDevTotal.Value = szFeeTemp;
                    lblUseDevFee.Text = uTotal;
                    lblUseDev.Text = szFeeUint;
                    divUseDev.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV).ToString() + ",";
                    uFeeTotal += ChinaRound(fTotalTemp, 2);
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_OCCUPY))
                {
                    lblOccupyTotal.InnerText = szFeeTemp;  
                    lblOccupyFee.Text = uTotal;
                    lblOccupy.Text = szFeeUint;
                    divOccupy.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_OCCUPY).ToString() + ",";
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ASSIST) && bPei)
                {
                    lblASSISTotal.InnerText = szFeeTemp;                    
                    lblASSISFee.Text = uTotal;
                    lblASSIS.Text = szFeeUint;
                    divASSIST.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_ASSIST).ToString() + ",";
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT))
                {
                    lblASSISTotal.InnerText = szFeeTemp; 
                    lblTIMEOUTFee.Text = uTotal;
                    lblTIMEOUT.Text = szFeeUint;
                    divTIMEOUT.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT).ToString() + ",";
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE) && !bIsSzum)
                {
                    lblCONSUMABLETotal.InnerText = ((uFeeUint*1.0)/100*uSumbule).ToString("0.0"); 
                    lblCONSUMABLEFee.Text = uSumbule.ToString();
                    lblCONSUMABLE.Text = szFeeUint;
                    divCONSUMABLE.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE).ToString() + ",";
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
                {
                    hiddenSampleTotal.Value = uSampleMoney / 100 +"."+ uSampleMoney % 100;
                    /*
                    fTotalTemp = (double)(((uFeeUint * 1.0) / 100) * uSumbule);
                    lblSampleTotal.InnerText = fTotalTemp.ToString("0.0");
                    hiddenSampleTotal.Value = fTotalTemp.ToString("0.0");
                    lblSampleFee.Text = uSumbule.ToString();
                    lblSample.Text = ((uFeeUint * 1.0) / 100).ToString("0.0");
                    
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE).ToString() + ",";
                    uFeeTotal += ChinaRound(fTotalTemp, 2);
                     * */
                    divSample.Style.Clear();
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_RESVDEV))
                {
                    lblRESVDEVTotal.InnerText = szFeeTemp; 
                    lblRESVDEVFee.Text = uTotal;
                    lblRESVDEV.Text = szFeeUint;
                    divRESVDEV.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_RESVDEV).ToString() + ",";
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST) && bCheck)
                {
                    lblENTRUSTTotal.InnerText = szFeeTemp;
                    hiddenENTRUSTTotal.Value = szFeeTemp;
                    lblENTRUSTFee.Text = uTotal;
                    lblENTRUST.Text = szFeeUint;
                    divENTRUST.Style.Clear();
                    szfeeType += ((uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST).ToString() + ",";
                    uFeeTotal += ChinaRound(fTotalTemp, 2);
                }                               
            }
        }
        feeType.Value = szfeeType;
        lblSum.InnerText = uFeeTotal.ToString("0.00"); ;
    }

    protected void btnCheckTempOK_Click(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTRESVCHECK setValue = new RTRESVCHECK();        
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwResvID = Parse(szidh.Value);
        RTRESV[] vtRes;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtRes);
        uint uFeeSN = 0;
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            uFeeSN = (uint)vtRes[0].dwFeeSN;
            vtRes[0].dwBeginTime =Get1970Seconds(dwBegin.Value);
            vtRes[0].dwEndTime = Get1970Seconds(dwEnd.Value);
            uint uTotal = 0;
            RTBILL[] vtBill = GetUniFeeVTFromHtml(uFeeSN.ToString(), out uTotal);
            setValue.BillInfo = vtBill;
            vtRes[0].dwReceivableCost = uTotal;
            setValue.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
            setValue.RTResv = vtRes[0];
            uResponse = m_Request.Reserve.RTResvCheck(setValue);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (ConfigConst.GCRTRepay == 1)
                {
                    string szId = Request["id"];
                    RTPREPAY setRTRePay = new RTPREPAY();
                    setRTRePay.dwResvID = (uint.Parse(szId));
                    setRTRePay.dwPrepayment = (0);//预收费0
                    uResponse = m_Request.Reserve.PrepayRTResv(setRTRePay);
                }
                MessageBox("审核通过", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox("审核失败:" + m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }
    }
    public RTBILL[] GetUniFeeVTFromHtml(string szFeeSN, out uint uTotal)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uTotal = 0;

        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN =(uint.Parse(szFeeSN));
        UNIFEE[] vtRes;
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        ArrayList list = new ArrayList();
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {            
            UNIFEE setFee = new UNIFEE();
            setFee = vtRes[0];
            FEEDETAIL[] vtFeeDetail = setFee.szFeeDetail;

            for (int i = 0; i < vtFeeDetail.Length; i++)
            {                
                uint uFeeType = (uint)vtFeeDetail[i].dwFeeType;                               
                RTBILL vtBillTemp = new RTBILL();
                vtBillTemp.dwFeeType = (uFeeType);
                vtBillTemp.dwResvID = Parse(szidh.Value);
                uint uReal = 0;
                double fReal = 0;
                uint dwUnitFee = (uint)vtFeeDetail[i].dwUnitFee;
                if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
                {
                    double.TryParse(hiddenUseDevTotal.Value, out fReal);                                                                
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE))
                {
                    double.TryParse(hiddenSampleTotal.Value, out fReal);                                                         
                }               
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)&& ViewState["dwProperty"]!=null&& ViewState["dwProperty"].ToString()=="true")
                {
                    double.TryParse(hiddenENTRUSTTotal.Value, out fReal);                                 
                }
                else if ((uFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE) && ViewState["dwProperty2"] != null && ViewState["dwProperty2"].ToString() == "false")//不是自带才收费
                {
                    double.TryParse(txtCONSUMABLETotal.Value, out fReal);
                }
                uReal=(uint)(fReal*100);
                vtBillTemp.dwReceivableCost = uReal;
                vtBillTemp.dwUnitFee = dwUnitFee;
                uTotal = uTotal + uReal;
                list.Add(vtBillTemp);
            }
        }
        RTBILL[] vtBill = new RTBILL[list.Count]; ;
        for (int i = 0; i < list.Count; i++)
        {
            vtBill[i] = (RTBILL)list[i];
        }
        return vtBill;
    }
}
