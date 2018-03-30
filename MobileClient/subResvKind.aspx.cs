using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;

public partial class _Default : PageBase
{
    protected LOCALUSER user;
    protected string OpenTime = "";
    protected string personNumLimit = "";
    protected string ResvList = "";
    protected string DevName = "";
    protected string szResvTime = "";
    protected string szPreResvTime = "";
    protected string cancelTime = "";
    protected string resvDateIn = "";
    protected string szStartTime = "";
    protected string szResvInfo = "";
    protected string szMsg = "";
    protected uint uClassKindTemp = 0;

    protected override void OnLoadComplete(EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        string resvSubmit = Request["resvSubmit"];
        if (resvSubmit!=null&&resvSubmit=="ok"&&!IsPostBack)
        {
            string szPreDate = Request["date"];
            uint uPreDate=Parse(szPreDate);

            szPreDate = (uPreDate / 10000 + "-" + (uPreDate % 10000) / 100 +"-"+ uPreDate % 100);
            string szStartTime = Request["dwBeginTime"];
            szStartTime = (uint.Parse(szStartTime) / 100 + ":" + uint.Parse(szStartTime) % 100);
            string szEndTime = Request["dwEndTime"];
            szEndTime = (uint.Parse(szEndTime) / 100 + ":" + uint.Parse(szEndTime) % 100);
            string szDevID = Request["devID"];
            UNIDEVICE dev;
            m_Request.m_UniDCom.StaSN = 1;
            if (!getDevByID(szDevID, out dev))
            {
                szMsg = m_Request.szErrMessage;
                return;
            }
            UNIRESERVE setValue = new UNIRESERVE();
            UNIGROUP group;
            UNIACCOUNT accno = ((ADMINLOGINRES)HttpContext.Current.Session["LoginRes"]).AccInfo;
            string szAccnoS = Request["szAccno"];

            /*
            if (!NewGroup("预约组", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out group))
            {
                Response.Write("{\"res\":0,\"error\":\"" + m_Request.szErrMessage + "\"}");
                return;
            }

            AddGroupMember(group.dwID, accno.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
            string[] szAccnoList = szAccnoS.Split(',');
            for (int i = 0; i < szAccnoList.Length; i++)
            {
                if (szAccnoList[i] != "")
                {
                    UNIACCOUNT accinfo;
                    if (GetAccByAccno(szAccnoList[i], out accinfo))
                    {
                        AddGroupMember(group.dwID, accinfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    }
                }
            }
            */
            setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
            setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
            setValue.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
            setValue.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
            setValue.dwOwner = accno.dwAccNo;
            setValue.szOwnerName = accno.szTrueName;

            setValue.dwMemberID = accno.dwAccNo;
            setValue.szMemberName = accno.szTrueName;
            setValue.dwLabID = dev.dwLabID;
            setValue.szLabName = dev.szLabName;

            setValue.ResvDev = new RESVDEV[1];
            setValue.ResvDev[0] = new RESVDEV();
            setValue.ResvDev[0].dwDevNum = 1;
            setValue.ResvDev[0].dwDevKind = dev.dwKindID;
            setValue.ResvDev[0].dwDevClsKind = dev.dwClassKind;
            setValue.ResvDev[0].dwDevStart = dev.dwDevSN;
            setValue.ResvDev[0].dwDevEnd = dev.dwDevSN;
            setValue.ResvDev[0].szRoomNo = dev.szRoomNo;
            setValue.ResvDev[0].szDevName = dev.szDevName;

            setValue.dwBeginTime = Get1970Seconds(szPreDate + " " + szStartTime);
            setValue.dwEndTime = Get1970Seconds(szPreDate + " " + szEndTime);
            string szTestName = Request["szTestName"];
            if (szTestName != null)
            {
                setValue.szTestName = szTestName;
            }
            if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                szMsg = "预约成功";
            }
            else
            {
                szMsg = m_Request.szErrMessage;
            }

        }
        LoadDefault();
    }
    public void LoadDefault()
    {
        string szDevID = Request["devID"];
        devID.Value = szDevID;

        if (user == null)
        {
            user = new LOCALUSER();
        }

        string szDate = Request["resvDate"];
        if (szDate == null)
        {
            return;
        }
        uint uDateTemp = Parse(szDate);
        szDate = uDateTemp / 10000 + "-" + (uDateTemp % 10000) / 100 + "-" + uDateTemp % 100;
        DateTime da;
        try
        {
            da = DateTime.Parse(szDate);
        }
        catch {
            return;
        }

        szDate = (da.Year * 10000 + (da.Month) * 100 + (int)da.Day).ToString();
        resvDateIn = da.ToString("yyyy-MM-dd");
        date.Value = szDate;
        user = GetUser();

        uint uClassKind = Parse(ConfigurationManager.AppSettings["MobileSysKind"]); 
        uint uDate = Parse(da.ToString("yyyyMMdd"));
        uint uDevID = Parse(Request["devID"]);

        UNIDEVICE getDev;
        m_Request.m_UniDCom.StaSN = 1;
        if (getDevByID(uDevID.ToString(), out getDev))
        {
            uClassKindTemp = (uint)getDev.dwClassKind;
        }
         
        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
     //   vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ALL;
        if (uClassKind != 0)
        {
            vrGet.dwClassKind = uClassKind;
        }
        if (uDevID != 0)
        {
          //  vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_DEVID;
            vrGet.dwDevID = uDevID;
        }

        vrGet.szDates = uDate.ToString();
        vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrGet.szDates = (szDate);
        vrGet.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        vrGet.szReqExtInfo.szOrderKey = "szDevName";
        vrGet.szReqExtInfo.szOrderMode = "desc";
        DEVRESVSTAT[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;

        if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null&&vtRes.Length>0)
        {
            //if (uDevID != 0)
            {
                /*
                for (int i = (int)vtRes[0].szOpenInfo[0].dwBegin; i <= (int)vtRes[0].szOpenInfo[0].dwEnd; i = i + 10)
                {
                    szStartTime += GetInputItemHtml(CONSTHTML.option, "", i / 100 + ":" + (i % 100).ToString("00"), i.ToString());
                }
                 * */
                for (int m = 0; m < vtRes[0].szOpenInfo.Length; m++)
                {
                    for (int i = (int)vtRes[0].szOpenInfo[m].dwBegin; i <= (int)vtRes[0].szOpenInfo[m].dwEnd; i = i + 10)
                    {
                        if ((i % 100)>60)
                        {
                            continue;
                            i = i + 100 - 60;
                        }
                        szStartTime += GetInputItemHtml(CONSTHTML.option, "", i / 100 + ":" + (i % 100).ToString("00"), i.ToString());
                    }
                }

                uint uBeginTime = (uint)vtRes[0].szOpenInfo[0].dwBegin;
                uint uEndTime = (uint)vtRes[0].szOpenInfo[0].dwEnd;
                OpenTime = "" + uBeginTime / 100 + ":" + (uBeginTime % 100).ToString("00") + "到" + uEndTime / 100 + ":" + (uEndTime % 100).ToString("00");
                personNumLimit = vtRes[0].dwMinUsers + "人到" + vtRes[0].dwMaxUsers + "人";

                if (vtRes[0].szResvInfo != null && vtRes[0].szResvInfo.Length > 0)
                {
                    DEVRESVTIME[] devResvTime = vtRes[0].szResvInfo;
                    for (int m = 0; m < devResvTime.Length; m++)
                    {
                        uint uBeginTime2 = (uint)devResvTime[m].dwBegin;
                        uint uEndTime2 = (uint)devResvTime[m].dwEnd;

                        szResvInfo += uBeginTime2 / 100 + ":" + (uBeginTime2 % 100).ToString("00") + "到" + uEndTime2 / 100 + ":" + (uEndTime2 % 100).ToString("00");
                        if (m < (devResvTime.Length - 1))
                        {
                            szResvInfo += ",";
                        }
                    }
                }
                szResvInfo += "";
                ResvList = szResvInfo;
                DevName = vtRes[0].szDevName;
                string szResv = "";
                szResvTime = GetHourStr((uint)vtRes[0].szRuleInfo.dwMinResvTime) + "到" + GetHourStr((uint)vtRes[0].szRuleInfo.dwMaxResvTime);
                szPreResvTime = GetHourStr((uint)vtRes[0].szRuleInfo.dwLatestResvTime) + "到" + GetHourStr((uint)vtRes[0].szRuleInfo.dwEarliestResvTime);
                if (vtRes[0].szRuleInfo.dwCancelTime != null && ((uint)vtRes[0].szRuleInfo.dwCancelTime).ToString() != "0")
                {
                    cancelTime = "" + vtRes[0].szRuleInfo.dwCancelTime.ToString();
                }
                else
                {
                    cancelTime = "不取消";
                }

                return;
            }
        }
    }
    public string GetHourStr(uint uMin)
    {
        if (uMin < 60)
        {
            return uMin + "分钟";
        }
        else if (uMin < (60 * 24) && uMin >= 60)
        {
            if ((uMin % 60) == 0)
            {
                return uMin / 60 + "小时";
            }
            return uMin / 60 + "小时" + uMin % 60 + "分钟";
        }
        else if (uMin >= (60 * 24))
        {
            return GetTimeForSecond(uMin * 60);
        }
        return "";
    }
}
