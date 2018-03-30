using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_zd_nxy_resvRec : UniClientPage
{
    protected string recordList = "";
    protected string statList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            RTRESVREQ vrGet = new RTRESVREQ();
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            uint? acc = vrAccInfo.dwAccNo;
            vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
            string start = Request["start"];
            string end=Request["end"];
            if(string.IsNullOrEmpty(start)||string.IsNullOrEmpty(end))return;
            uint intStartTime = ToUInt(start.Replace("-",""));
            uint intEndTime = ToUInt(end.Replace("-", ""));
            vrGet.dwMAccNo = acc;
            vrGet.dwBeginDate = intStartTime;
            vrGet.dwEndDate = intEndTime;
            vrGet.szReqExtInfo.szOrderKey = "dwOwner";
            vrGet.szReqExtInfo.szOrderMode = "DESC";
            RTRESV[] vtResult;
            if (m_Request.Reserve.GetRTResv(vrGet, out vtResult) == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
            {
                int use_time = 0;
                int prepay = 0;
                int realpay = 0;
                int times = 0;
                int all_use_time = 0;
                int all_prepay = 0;
                int all_realpay = 0;
                int all_times = 0;
                for (int i = 0; i < vtResult.Length; i++)
                {
                    RTRESV resv = vtResult[i];
                    string rsv = "";
                    rsv += "<tr><td  style='text-align:left;'>" + resv.szDevName + "</td><td>" + resv.szOwnerName + "</td><td class='td_lab'  style='text-align:left;'><span style='font-weight:600;color:#555'>实验名称：</span>" + resv.szTestName + "<br /><span style='font-weight:600;color:#555'>仪器管理员：</span>" + resv.szManName + "</td>";
                    rsv += "<td>" + resv.szHolderName + "</td>";
                    string beginTime = Get1970Date((int)resv.dwBeginTime);
                    string endTime = Get1970Date((int)resv.dwEndTime);
                    rsv += "<td><div><span style='font-weight:600;color:#555'>开始：</span>" + beginTime + " </div><div><span style='font-weight:600;color:#555'>结束：</span>" + endTime + "</div></td>";
                    if ((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                    {
                        //统计
                        use_time += (int)resv.dwRealUseTime;
                        prepay += (int)resv.dwUseFee;
                        realpay += (int)resv.dwRealCost;
                        times++;
                        all_use_time += (int)resv.dwRealUseTime;
                        all_prepay += (int)resv.dwUseFee;
                        all_realpay += (int)resv.dwRealCost;
                        all_times++;
                        if (i == vtResult.Length - 1 || resv.dwOwner != vtResult[i + 1].dwOwner)
                        {
                            statList += "<tr><td>" + resv.szOwnerName + "</td><td>" + times + "</td><td>" + calc((uint)use_time) + "</td><td>" + prepay / 100 + " 元</td><td>" + realpay / 100 + " 元</td></tr>";
                            use_time = 0;
                            prepay = 0;
                            realpay = 0;
                            times = 0;
                        }
                        //
                        rsv += "<td>" + calc(resv.dwRealUseTime) + "</td><td>" + resv.dwUseFee / 100 + " 元</td><td>" + resv.dwRealCost / 100 + " 元</td></tr>";
                        recordList += rsv;
                    }
                }
                statList += "<tr style='font-weight:bold;'><td>合计</td><td>" + all_times + "</td><td>" + calc((uint)all_use_time) + "</td><td>" + all_prepay / 100 + " 元</td><td>" + all_realpay / 100 + " 元</td></tr>";
            }
        }
    }
    string calc(uint? t)
    {
        string str = "";
        uint? h = t / 60;
        uint? m = t % 60;
        if (h > 0) str += h + "小时";
        if (m >= 0) str += m + "分钟";
        return str;
    }
}