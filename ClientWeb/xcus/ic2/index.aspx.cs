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
    protected string noticeList = "";
    protected string clsSlide = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitDynInfo();
    }

    private void InitDynInfo()
    {
        GetNotice();
        GetNewResv();
        GetSlide();
    }

    private void GetSlide()
    {
        if (GetConfig("mIndexMode") != "2") return;
        string tmp = GetXmlContent("slide", "other");
            List<string> list = GetSrcFromHtml(tmp);
            for (int i = 0; i < list.Count; i++)
            {
                string src = list[i];
                clsSlide += "<div class='item" + (i == 0 ? " active" : "") + "'><img src='" + src + "' alt='...'><div class='carousel-caption'></div></div>";
            }

        //List<UNIDEVCLS> list = new List<UNIDEVCLS>();
        //uint clsKind = ToUInt(GetConfig("openClsKind"));
        //if (clsKind == 0)
        //{
        //    list.AddRange(GetDevCls(null));
        //}
        //else
        //{
        //    if ((clsKind & 1) > 0)
        //    {
        //        UNIDEVCLS[] rlt1 = GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        //        if(rlt1!=null)
        //        list.AddRange(rlt1);
        //    }
        //    if ((clsKind & 2) > 0)
        //    {
        //        UNIDEVCLS[] rlt2 = GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
        //        if (rlt2 != null)
        //        list.AddRange(rlt2);
        //    }
        //    if ((clsKind & 8) > 0)
        //    {
        //        UNIDEVCLS[] rlt3 = GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        //        if (rlt3 != null)
        //        list.AddRange(rlt3);
        //    }
        //}
        //for (int i = 0; i < list.Count; i++)
        //{
        //    string isLong = "false";
        //    string isKind = "false";
        //    DEVKINDREQ req = new DEVKINDREQ();
        //    req.szClassName = list[i].szClassName;
        //    UNIDEVKIND[] rlt;
        //    if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        //    {
        //        if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
        //        {
        //            isLong = "true";
        //        }
        //        if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
        //        {
        //            isKind = "true";
        //        }
        //    }
        //    if (list[i].szMemo != null && list[i].szMemo == "false")
        //    {
        //        continue;
        //    }
        //    string name = list[i].szClassName;
        //    string url="";
        //    //图片
        //    string tmp = GetXmlContent(list[i].dwClassID.ToString(), "slide");
        //    List<string> srcs = GetSrcFromHtml(tmp);
        //    if(srcs.Count>0)
        //    url=srcs[0];
        //    else
        //        continue;
        //    //  class=\"click_load\"
        //    clsSlide+="<div class='item "+(i==0?"active":"")+"'><a  url=\"../a/clsdetail.aspx?back=true&classKind=" + list[i].dwKind + "&isKind=" + isKind + "&isLong=" + isLong + "&classId=" + list[i].dwClassID +
        //            "&className=" + Server.UrlEncode(name) + "\"\"><img alt='" + name + "' src='" + url + "' /></a><div class='carousel-caption'>" + CutStrT(name, 20) + "</div></div>";
        //}
    }

    private void GetNewResv()
    {
        if (GetConfig("mResvDynamic")!="1")
        {
            return;
        }
        RESVSHOWREQ req = new RESVSHOWREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        req.dwEndDate = req.dwBeginDate;
        string classkind = GetConfig("openClsKind");
        if (!string.IsNullOrEmpty(classkind) && classkind != "0") req.dwClassKind = ToUInt(classkind);
        req.szReqExtInfo.szOrderKey = "dwBeginTime";
        req.szReqExtInfo.szOrderMode = "ASC";
        req.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING | (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO;
        RESVSHOW[] rlt;
        if (m_Request.Reserve.GetReserveForShow(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                RESVSHOW rsv=rlt[i];
                //if ((rsv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0) continue;//过滤过期
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
                dynamicInfo += "<li date='" + start + "' id='rsv_" + rsv.dwResvID + "'><div class='text-primary'>" + CutStrT(rsv.szTestName, 16) + "<div><div class='title color1'>" + objs + "&nbsp;" + Util.Converter.ResvStatusConverter(rsv.dwStatus) + "</div><div class='prop songti'><span class='glyphicon glyphicon-time'></span>&nbsp;"
                    + start.Substring(11) + " - " + end.Substring(5) + "</div></li>";//&nbsp;&nbsp;<span class='glyphicon glyphicon-home'></span>&nbsp;
            }
        }
    }
    private void GetNotice()
    {
        XmlCtrl.XmlInfo[] list = GetXmlInfoList("notice", 5);
        if (list != null && list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                string id = list[i].id;
                string title = list[i].title;
                string date = list[i].date;
                string content=list[i].content;
                if (string.IsNullOrEmpty(date)) date = "0";
                string dt = Util.Converter.StrToDate(date).ToString("yyyy/MM/dd HH:mm");
                if(i>0)
                    noticeList += "<li date='" + date + "' id='" + id + "'><div class='title'>▪ <a class='detail' url='../a/article.aspx?back=true&type=notice&id=" + id + "' cache='#cache_con'>" + title + "</a></div><div class='grey songti' style='border-bottom:1px dotted #ddd;'>" + dt + "</div></li>";
                else
                    infoContent += "<div class='msg_box click detail' url='../a/article.aspx?back=true&type=notice&id=" + id + "' cache='#cache_con'><div class='img-circle msg_img'><span class='glyphicon glyphicon-volume-up'></span></div><div><strong class='color1'>" + title + "</strong><div class='con songti'>"
                    + DelImgFromHtml(content) + "</div><div class='grey text-right'>" + dt + "</div></div></div>";
                
            }
        }
    }
}