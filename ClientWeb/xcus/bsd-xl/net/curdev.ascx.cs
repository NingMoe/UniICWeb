using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_xcus_bsd_xl_net_curdev : UniClientModule
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
        vrGet.dwDevID = devID;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            UNIDEVICE dev = vtResult[0];
            Session["CUR_DEV"] = vtResult[0];
            CurDevId = dev.dwDevID.ToString();
            pagePosition = "<strong style='color:#333;'>" + dev.szCampusName + " | " + dev.szDevName + "</strong>";
            CurDevName = dev.szDevName;
            if (dev.dwPurchaseDate != null && dev.dwPurchaseDate > 10000000)
            {
                string str = dev.dwPurchaseDate.ToString();
                string devDate = str.Substring(0, 4) + "年" + str.Substring(4, 2) + "月" + str.Substring(6, 2) + "日";
            }
            ContDevExt(dev.szExtInfo);
            CurDevCps = dev.szCampusName;
            CurDevDept = dev.szDeptName;
            CurDevLab = dev.szLabName;
            CurDevPro = (dev.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 ? "l" : "r";
            UniClientPage p = new UniClientPage();
            imgUrl = this.ResolveClientUrl(p.GetImg(dev.dwDevSN));
            //预约信息
            UNIRESVRULE rule = p.GetDevRsvRule(dev.dwDevID.ToString());
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
            string devSta;
            if (Converter.GetDevStat(dev.dwDevStat))
            {
                devSta = Converter.GetDevRunStat(dev.dwRunStat);
            }
            else
            {
                devSta = "<span style='color:red'>仪器不可用</span>";
            }
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