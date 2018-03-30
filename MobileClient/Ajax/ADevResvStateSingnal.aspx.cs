using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    public class DevResvStateAjax {
        public uint devID;
        public string DevName;
        public int[] ResvList;
        public int LeftTime;
        public int OpenTime;
        public string szMemo;
    }
    public class DevResvStateSingalAjax
    {
        public uint devID;
        public string DevName;
        public string ResvList;
        public string szResvTime;
        public string szPreResvTime;
        public string personNumLimit;
        public int OpenTime;
        public int cancelTime;
        public string szMemo;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uClassKind = Parse(Request["classKind"]);
        uint uDate = Parse(Request["dwDate"]);
        uint uDevID = Parse(Request["devID"]);
        uint uKindID = Parse(Request["kindID"]);
        if (m_Request == null)
        {

            Response.Write("{\"error\":\"\"}");
            return;
        }
        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
        //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ALL;
        if (uClassKind != 0)
        {
            vrGet.dwClassKind = uClassKind;
        }
        if (uDevID != 0)
        {
            //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_DEVID;
            vrGet.dwDevID = uDevID;
        }
        if (uKindID != 0)
        {
            vrGet.szKindIDs = uKindID.ToString();
        }
        vrGet.szDates = uDate.ToString();
        vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrGet.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        vrGet.szReqExtInfo.szOrderKey = "szDevName";
        vrGet.szReqExtInfo.szOrderMode = "desc";
        DEVRESVSTAT[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        
        if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS&&vtRes!=null)
        {
            if (uDevID != 0)
            {
                DevResvStateSingalAjax temp = new DevResvStateSingalAjax();
                temp.OpenTime = ((int)vtRes[0].szOpenInfo[0].dwBegin * 10000 + (int)vtRes[0].szOpenInfo[0].dwEnd);
                temp.personNumLimit = vtRes[0].dwMinUsers + "人到" + vtRes[0].dwMaxUsers+ "人";
                string szResvInfo = "";
                if (vtRes[0].szResvInfo != null && vtRes[0].szResvInfo.Length > 0)
                {
                    DEVRESVTIME[] devResvTime = vtRes[0].szResvInfo;
                    for (int m = 0; m < devResvTime.Length; m++)
                    {
                        szResvInfo += ((int)devResvTime[m].dwBegin * 10000 + (int)devResvTime[m].dwEnd).ToString();
                        if (m < (devResvTime.Length - 1))
                        {
                            szResvInfo += ",";
                        }
                    }
                }
                szResvInfo += "";
                temp.ResvList = szResvInfo;
                temp.DevName = vtRes[0].szDevName;
                string szResv = "";
                temp.szResvTime=GetHourStr((uint)vtRes[0].szRuleInfo.dwMinResvTime) + "到" + GetHourStr((uint)vtRes[0].szRuleInfo.dwMaxResvTime);
                temp.szPreResvTime = GetHourStr((uint)vtRes[0].szRuleInfo.dwLatestResvTime) + "到" + GetHourStr((uint)vtRes[0].szRuleInfo.dwEarliestResvTime);
                if (((uint)vtRes[0].szRuleInfo.dwCancelTime) == 0)
                {
                    temp.cancelTime = (int)vtRes[0].szRuleInfo.dwCancelTime;
                }
                else
                {
                    temp.cancelTime = 0;
                }
                string szRes = JsonConvert.SerializeObject(temp);
                Response.Write(szRes);
                return;
            }
           
        }
        else {
            Response.Write("{\"error\":\"" + m_Request.szErrMessage + "\"}");
        }


    }
    public string GetHourStr(uint uMin)
    {
        if (uMin < 60)
        {
            return uMin + "分钟";
        }
        else if (uMin < (60 * 24) && uMin>=60)
        {
            if ((uMin % 60) == 0)
            {
                return uMin / 60 + "小时";
            }
            return uMin / 60 + "小时" + uMin % 60 + "分钟";
        }
        else if (uMin >= (60 * 24))
        {
            return GetTimeForSecond(uMin*60);
        }
        return "";
    }
}