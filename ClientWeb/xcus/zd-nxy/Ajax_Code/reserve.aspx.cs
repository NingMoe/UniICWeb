using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UniWebLib;
using UniStruct;
using System.Xml;

public partial class DevWeb_Ajax_Code_reserve : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Session["CUR_DEV"] == null)
        {
            Response.Write("{\"ret\":0,\"msg\":\"Session已超时，刷新页面！\"}");
            return;
        }
        UNIDEVICE dev = (UNIDEVICE)Session["CUR_DEV"];
        DateTime start = ConvertTime(Convert.ToInt64(Request["start"]));
        DateTime end = ConvertTime(Convert.ToInt64(Request["end"]));
        List<dwResv> resvs = new List<dwResv>();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        //获取长期预约  不用
        //DEVLONGRESVSTATREQ vrGet = new DEVLONGRESVSTATREQ();
        //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_DEVID;
        //vrGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        //vrGet.szKey = dev.dwDevID.ToString();
        //vrGet.dwStartDate = Convert.ToUInt32(start.ToString("yyyyMMdd"));
        //vrGet.dwEndDate = Convert.ToUInt32(end.ToString("yyyyMMdd"));
        //vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH;
        //DEVLONGRESVSTAT[] vtResult;
        //获取课题科研预约
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        vrGet.dwBeginDate = Convert.ToUInt32(start.ToString("yyyyMMdd"));
        vrGet.dwEndDate = Convert.ToUInt32(end.ToString("yyyyMMdd"));
        vrGet.dwDevID = dev.dwDevID;
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                dwResv resv = new dwResv();
                resv.id = vtResult[i].dwResvID.ToString();
                resv.accno = vtResult[i].dwOwner.ToString();
                resv.start = Get1970Date((int)vtResult[i].dwBeginTime);
                resv.end = Get1970Date((int)vtResult[i].dwEndTime);
                if (resv.start == resv.end) resv.end = (DateTime.Parse(resv.end)).AddMinutes(1).ToString("yyyy-MM-dd HH:mm");
                resv.title =">> "+resv.end.Substring(11)+"（"+ vtResult[i].szOwnerName + "，导师："+vtResult[i].szHolderName+"，实验：" + vtResult[i].szTestName+"）";
                //预约状态
                if ((vtResult[i].dwStatus&(uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO)>0)
                {
                    resv.status = "undo";
                    resv.color = "#006DA3";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                {
                    resv.status = "doing";
                    resv.color = "#77A500";
                }
                else if ((vtResult[i].dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                {
                    resv.status = "done";
                    resv.color = "#5F00B8";
                }
                else
                {
                    resv.status = "othe";
                    resv.color = "#ccc";
                }
                resv.allDay = false;
                resvs.Add(resv);
            }
        }
        Response.ContentType = "application/Json";
        Response.Write(JsonConvert.SerializeObject(resvs));

    }

    private string ConvertDate(string p)
    {
        string y = p.Substring(0, 4);
        string m = p.Substring(4, 2);
        string d = p.Substring(6, 2);
        return y + "-" + m + "-" + d;
    }

    private long MilliTimeStamp(DateTime TheDate)
    {
        DateTime d1 = new DateTime(1970, 1, 1);
        DateTime d2 = TheDate.ToUniversalTime();
        TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

        return (long)ts.TotalMilliseconds;
    }
    private DateTime ConvertTime(long milliTime)
    {
        long timeTricks = new DateTime(1970, 1, 1).Ticks + milliTime * 10000000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
        return new DateTime(timeTricks);
    }
}
class dwResv
{
    public string id;
    public string title;
    public string start;
    public string end;
    public string status;
    public string color;
    public string accno;
    public bool allDay;
}