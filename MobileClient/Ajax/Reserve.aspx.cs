using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        string szMsg = "";
        string szPreDate = Request["resvDate"];
        szPreDate = szPreDate.Replace("-", "");
        string szTestName = Request["szTestName"];
        string szDevID = Request["devid"];
        string szStartTime = Request["beginTime"];
        string szEndTime = Request["endTime"];
        if (szDevID == null || szDevID == "")
        {
            Response.Write("预约失败,未选择预约对象");
            return;
        }
        if (szPreDate == null || szPreDate == "" || szStartTime == null || szStartTime == "" || szEndTime == null || szEndTime == "")
        {
            Response.Write("预约失败,为选择时间");
            return;
        }

        uint uPreDate = Parse(szPreDate);

        szPreDate = (uPreDate / 10000 + "-" + (uPreDate % 10000) / 100 + "-" + uPreDate % 100);
        
        szStartTime = (uint.Parse(szStartTime) / 100 + ":" + (uint.Parse(szStartTime) % 100).ToString("00"));
        szEndTime = (uint.Parse(szEndTime) / 100 + ":" + (uint.Parse(szEndTime) % 100).ToString("00"));
        UNIDEVICE dev;
        m_Request.m_UniDCom.StaSN = 1;
        if (!getDevByID(szDevID, out dev))
        {
            szMsg = m_Request.szErrMessage;
            return;
        }
        UNIRESERVE setValue = new UNIRESERVE();
        UNIACCOUNT accno = ((ADMINLOGINRES)HttpContext.Current.Session["LoginRes"]).AccInfo;
        setValue.szTestName = szTestName;
       setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
       setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
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

        if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            szMsg = "预约成功";
        }
        else
        {
            szMsg = m_Request.szErrMessage;
        }
        Response.Write(szMsg);
    }
   
}