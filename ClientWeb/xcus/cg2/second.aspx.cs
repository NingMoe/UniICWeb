using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_second : UniClientPage
{
    protected string secondList;
    protected string atyList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsLogined())
        {
            GetAtyList();
            GetSecondList();
        }
    }

    private void GetAtyList()
    {
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        YARDACTIVITY[] rlt;
        REQUESTCODE cd = m_Request.Reserve.GetYardActivity(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if ((rlt[i].dwSecurityLevel & 0x20000000) > 0)//会议模版
                {
                    continue;
                }
                atyList+="<option value='"+rlt[i].dwActivitySN+"'>"+rlt[i].szActivityName+"</option>";
            }
        }
        
    }

    private void GetSecondList()
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                THIRDRESVREQ req = new THIRDRESVREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"));
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(12).ToString("yyyyMMdd"));
        req.szPID = acc.szPID;
        THIRDRESV[] rlt;
        if (m_Request.Reserve.GetThirdResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                THIRDRESV resv = rlt[i];
                string status ="";
                string act="";
                if (string.IsNullOrEmpty(resv.szAssertSN) || resv.dwResvID == 0)
                {
                    status = "<span class='orange'>未预约</span>";
                    act = "<a class='click second_act' third_id=" + resv.dwThirdResvID + ">预约</a>";
                }
                else
                {
                    status = "<span class='green'>已预约</span>";
                    DEVREQ dReq = new DEVREQ();
                    dReq.dwDevSN = ToUInt(resv.szAssertSN);
                    UNIDEVICE[] dRlt;
                    if (m_Request.Device.Get(dReq, out dRlt) == REQUESTCODE.EXECUTE_SUCCESS&&dRlt.Length>0)
                    {
                        act = dRlt[0].szDevName;
                    }
                    act += "<br/><a class='click second_act' third_id='" + resv.dwThirdResvID + "' resv_id='"+resv.dwResvID+"'>重新预约</a>";
                }
                string date = "未定";
                if (resv.dwResvDate != null && resv.dwStartHM != null)
                {
                    date = Util.Converter.UintToDateStr(resv.dwResvDate)+" "+
                        ((int)resv.dwStartHM / 100).ToString("00") + ":" + ((int)resv.dwStartHM % 100).ToString("00") + "-"
                        + ((int)resv.dwEndHM / 100).ToString("00") + ":" + ((int)resv.dwEndHM % 100).ToString("00");
                }
                secondList += "<tr class='it'><td>"+resv.dwThirdResvID+"</td><td>"+resv.szResvTitle+"</td><td>"+resv.szOrganization+"</td>"+
                    "<td>" + resv.szOrganiger + "</td><td>" + date + "</td><td class='text-center'>" + status + "</td><td class='text-center'>" + act + "</td></tr>";
            }
        }
    }
}