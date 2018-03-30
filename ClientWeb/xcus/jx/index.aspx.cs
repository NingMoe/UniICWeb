using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_index : UniClientPage
{
    protected string dynamicInfo = "";
    protected string infoContent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitDynInfo("notice");
    }

    private void InitDynInfo(string type)
    {
        GetNotice();
        GetNewResv();
    }

    private void GetNewResv()
    {
        RESVSHOWREQ req = new RESVSHOWREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        req.dwEndDate = req.dwBeginDate;
        req.szReqExtInfo.szOrderKey = "dwBeginTime";
        req.szReqExtInfo.szOrderMode = "ASC";
        //req.dwStatFlag =  (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        RESVSHOW[] rlt;
        if (m_Request.Reserve.GetReserveForShow(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                RESVSHOW rsv=rlt[i];
                string start = Get1970Date((int)rsv.dwBeginTime);
                string end = Get1970Date((int)rsv.dwEndTime);
                //预约对象
                string objs = rsv.szDevName;
                //RESVDEV[] resvDev = rsv.ResvDev;
                //if (resvDev != null && resvDev.Length > 0)
                //{
                //    string devName = string.Empty;
                //    for (int j = 0; j < resvDev.Length; j++)
                //    {
                //        devName = devName + resvDev[j].szDevName.ToString();
                //    }
                //    objs = devName != "" ? devName : "现场分配";
                //}
                dynamicInfo += "<li date='" + start + "' id='rsv_" + rsv.dwResvID + "'><div class='title color1'>"+objs+"&nbsp;" + Util.Converter.ResvStatusConverter(rsv.dwStatus) + "</div><div class='prop songti'><span class='glyphicon glyphicon-time'></span>&nbsp;"
                    + start.Substring(11) + " 至 " + end.Substring(5) + "</div></li>";//&nbsp;&nbsp;<span class='glyphicon glyphicon-home'></span>&nbsp;
            }
        }
    }
    private string GetNotice()
    {
        string noticeList = "";
        XmlCtrl.XmlInfo[] list = GetXmlInfoList("notice", 1);
        if (list != null && list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                string id = list[i].id;
                string title = list[i].title;
                string date = list[i].date;
                string content=list[i].content;
                if (string.IsNullOrEmpty(date)) date = "0";
                string dt = Util.Converter.StrToDate(date).ToString("yyyy年MM月dd日 HH时mm分");
                //noticeList += "<li date='" + date + "' id='" + id + "'><div class='title'>▪ " + title + "</div><div class='grey songti' style='border-bottom:1px dotted #ddd;'>" + dt + "</div></li>";
                infoContent += "<div class='msg_box'><div class='img-circle msg_img'><span class='glyphicon glyphicon-volume-up'></span></div><div><strong class='color1'>" + title + "</strong><div class='con'>"
                    + content + "</div><div class='grey text-right'>" + dt + "&nbsp;[<a class='detail' url='../a/article.aspx?back=true&type=notice&id=" + id + "' cache='#cache_con'>详细</a>]</div></div></div>";
            }
        }
        return noticeList;
    }
}