using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Xml;
using UniWebLib;
using UniStruct;
using Util;

public partial class DevDetail : UniClientPage
{
    uint devID;
    protected string imgUrl = "";
    protected string pagePosition = "";
    protected string CurDevId = "";
    protected string CurDevName = "";
    protected string CurDevLab = "";
    protected string CurDevDept = "";   
    protected string CurDevCps = "";
    protected string CurDevPro = "";
    protected string CurDevSta = "";
    protected string CurDevEarly = "";
    protected string CurDevLast = "";
    protected string CurDevMax = "";
    protected string CurDevMin = "";
    protected string useFee = "";
    protected string subFee = "";
    protected string sampleFee = "";
    protected string vadioFile = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Request["dev"] == null) return;
        devID = Convert.ToUInt32(Request["dev"]);
        InitDevInfo(devID);
    }
    private void InitDevInfo(uint devID)
    {
        Session["CUR_DEV"] = null;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtResult;
        vrGet.dwDevID = new UniDW(devID);
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            UNIDEVICE dev = vtResult[0];
            UNIDEVKIND kind = GetDevKind(dev.dwKindID);
            Session["CUR_DEV"] = vtResult[0];
            devNum.InnerHtml = dev.szAssertSN;
            CurDevId = dev.dwDevID.ToString();
            pagePosition = "<strong style='color:#333;'>" + dev.szCampusName + " | " + dev.szDevName + "</strong>";
            devName.InnerText = dev.szDevName;
            CurDevName = dev.szDevName;
            devModel.InnerHtml = dev.szModel;
            if (dev.dwPurchaseDate != null && dev.dwPurchaseDate > 10000000)
            {
                string str = dev.dwPurchaseDate.ToString();
                devDate.InnerHtml = str.Substring(0, 4) + "年" + str.Substring(4, 2) + "月" + str.Substring(6, 2) + "日";
            }
            ContDevExt(dev.szExtInfo);
            devProFactory.InnerHtml = ConvertStr(kind.szProducer);
            devProPlace.InnerHtml = ConvertStr(kind.dwNationCode);//must custom
            devPara.InnerHtml = ConvertStr(ViewState["szPerform"]);
            devSpecimen.InnerHtml = ConvertStr(ViewState["szSample"]);
            devFun.InnerHtml = "";//dev.szFunc;
            devCam.InnerHtml = dev.szCampusName;
            CurDevCps = dev.szCampusName;
            devCol.InnerHtml = dev.szDeptName;
            CurDevDept = dev.szDeptName;
            CurDevLab = dev.szLabName;
            CurDevPro = (dev.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 ? "l" : "r";
            devLoc.InnerHtml = dev.szLabName + " | " + dev.szRoomName;
            imgUrl = GetImg(dev.dwDevSN);
            //预约信息
            UNIRESVRULE rule = GetDevRsvRule(dev.dwDevID.ToString());
            if (rule.dwEarliestResvTime != null)
            {
                CurDevEarly = rule.dwEarliestResvTime.ToString();
            }
            if (rule.dwLatestResvTime != null)
            {
                CurDevLast = rule.dwLatestResvTime.ToString();
            }
            if (rule.dwMaxResvTime != null)
            {
                CurDevMax = rule.dwMaxResvTime.ToString();
            }
            if (rule.dwMinResvTime != null)
            {
                CurDevMin = rule.dwMinResvTime.ToString();
            }
            CurDevSta = dev.dwDevStat.ToString();
            //仪器状态
            if (Converter.GetDevStat(dev.dwDevStat))
            {
                devSta.InnerHtml = Converter.GetDevRunStat(dev.dwRunStat);
            }
            else
            {
                devSta.InnerHtml = "<span style='color:red'>仪器不可用</span>";
            }
            //获取管理员
            devMan.InnerHtml = dev.szAttendantName;
            devCon.InnerHtml = dev.szAttendantTel;

            InitFee(dev);
            InitVadio(dev);
        }
    }

    private void InitVadio(UNIDEVICE dev)
    {
        vadioFile = GetVadio(dev);
    }

    private void InitFee(UNIDEVICE dev)
    {
        //使用代检费
        GetIdentFee(dev, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER);
        GetIdentFee(dev, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER);
        //样品费
        SAMPLEINFO[] spls = dev.DevSample;
        if (spls == null) return;
        string list = "";
        for (int i = 0; i < spls.Length; i++)
        {
            SAMPLEINFO spl = spls[i];
            //if (spl.dwUnitFee1 == null)
            //    spl.dwUnitFee1 = 0;
            if (spl.dwUnitFee2 == null)
                spl.dwUnitFee2 = 0;
            if (spl.dwUnitFee3 == null)
                spl.dwUnitFee3 = 0;
            list += "<tr><td class='s-sn'>" + spl.szSampleName + "</td><td class='s-f'><span>" + ((float)spl.dwUnitFee2 / 100.00).ToString("F2") + "</span> 元/" + spl.szUnitName + "</td><td class='s-f'><span>" + ((float)spl.dwUnitFee3 / 100.00).ToString("F2") + "</span> 元/" + spl.szUnitName + "</td></tr>";
        }
        sampleFee = list;
    }
    bool GetIdentFee(UNIDEVICE dev, uint? ident)
    {
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwDevKind = dev.dwKindID;
        vrGet.dwIdent = ident;
        UNIFEE[] rlt;
        if (m_Request.Fee.Get(vrGet, out rlt)==REQUESTCODE.EXECUTE_SUCCESS&&rlt != null && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                FEEDETAIL[] fees = rlt[i].szFeeDetail;
                for (int j = 0; j < fees.Length; j++)
                {
                    FEEDETAIL fee = fees[j];
                    if (fee.dwUnitFee == null || fee.dwUnitTime == null || fee.dwUnitTime == 0)
                    {
                        fee.dwUnitFee = 0;
                        fee.dwUnitTime = 1;
                    }
                    if (fees[j].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                    {
                        useFee += "<td class='s-f'><span>" + ((float)(fee.dwUnitFee / 100.00 * 60 / fee.dwUnitTime)).ToString("F2") + "</span> 元/小时</td>";
                    }
                    else if (fees[j].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                    {
                        subFee += "<td class='s-f'><span>" + ((float)(fee.dwUnitFee / 100.00 * 60 / fee.dwUnitTime)).ToString("F2") + "</span> 元/小时</td>";
                    }
                }
            }
            return true;
        }
        return false;
    }

    private void GetDevFee(uint? devId, uint? rtId)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTDEVFEEREQ req = new RTDEVFEEREQ();
        req.dwDevID = devId;
        req.dwRTID = rtId;
        UNIFEE rlt;
        uResponse = m_Request.Fee.RTDevFeeGet(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            FEEDETAIL[] fees = rlt.szFeeDetail;
        }
    }
    private void ContDevExt(string szMemo)
    {
        //szManufacturers生产厂商
        //szNation国别
        //szLanguage操作语言
        //szPerform性能指标
        //szSample样品要求
        //主要应用: unidevice本来就有的szfun
        int uStart = -1;
        int uEnd = -1;
        string szTemp = "";
        szTemp = "{Manufacturers:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szManufacturers = "";
        if (uStart > -1 && uEnd > -1)
        {
            szManufacturers = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szManufacturers"] = szManufacturers;
        }
        szTemp = "{szNation:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szNation = "";
        if (uStart > -1 && uEnd > -1)
        {
            szNation = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szNation"] = szNation;
        }

        szTemp = "{szLanguage:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szLanguage = "";
        if (uStart > -1 && uEnd > -1)
        {
            szLanguage = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szLanguage"] = szLanguage;
        }
        szTemp = "{szPerform:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szPerform = "";
        if (uStart > -1 && uEnd > -1)
        {
            szPerform = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szPerform"] = szPerform;
        }

        szTemp = "{szSample:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szSample = "";
        if (uStart > -1 && uEnd > -1)
        {
            szSample = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szSample"] = szSample;
        }
    }
    string ConvertStr(object obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            return obj.ToString();
        }
    }
}