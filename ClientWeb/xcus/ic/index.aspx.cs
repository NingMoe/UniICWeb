using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;
using System.Xml;   

public partial class Page_ : UniClientPage
{
    protected string szActivity = "";
    protected string szActivityHistory= "";
    protected string szReacher = "";
    protected string szNotice = "";
    protected string dynamicInfo = "";
    protected string infoContent = "";
    int len = 15;
    protected void Page_Load(object sender, EventArgs e)
    {        
        base.LoadPage();
        InitDynInfo("notice");//通知
        string szCurPath = Request.FilePath;
        Response.Cookies["unifoundUrl"].Value = szCurPath;
        Response.Cookies["unifoundUrl"].Expires = System.DateTime.Now.AddDays(1);
         REQUESTCODE uResponse=REQUESTCODE.EXECUTE_FAIL;
         if (!this.Page.IsPostBack)
         {            
             ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
             UNIACTIVITYPLAN[] vrResult;
             vrParameter.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
             uResponse = m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult);
             if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
             {
                 
                 int nDateNow = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                 int nCount = vrResult.Length;
                 string resvid = "";
                 for (int i = 0; i < nCount; i++)
                 {
                     resvid += vrResult[i].dwActivityPlanID.ToString() + ";";
                     uint uStatue=(uint)vrResult[i].dwStatus;
                     if ((uStatue & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)<=0)
                     {
                         continue;
                     }
                     if ((uStatue & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN) > 0)
                     {
                         //continue;
                     }
                     string szActivityPlanName = vrResult[i].szActivityPlanName.ToString();
                     string szActivityPlanNameShow = szActivityPlanName;

                     if (szActivityPlanName.Length > len)
                     {
                         szActivityPlanName = szActivityPlanName.Substring(0, len) + "...";
                     }
                     if (vrResult[i].dwActivityDate >= nDateNow)
                     {                       
                         uint dwActivityDate = (uint)vrResult[i].dwActivityDate;                        
                         szActivity += "<li>";
                         szActivity += "<a href=\"salon_pre_content.aspx?id=" + vrResult[i].dwActivityPlanID.ToString()+ "\" class=\"title\" title=\"" + szActivityPlanNameShow + "\">预告：" + szActivityPlanName + "</a>";
                         szActivity += "</li>";
                        
                     }
                     else
                     {
                         uint dwActivityDate = (uint)vrResult[i].dwActivityDate;
                         szActivityHistory += "<li>";
                         szActivityHistory += "<a href=\"salon_last_content.aspx?id=" + vrResult[i].dwActivityPlanID.ToString() + "\" class=\"title\" title=\"" + szActivityPlanNameShow + "\">回顾：" + szActivityPlanName + "</a>";
                         szActivityHistory += "</li>";
                       
                     }

                 }
                 DateTime dDatePre=DateTime.Now;
                 DateTime dDateNext = DateTime.Now.AddDays(10);
                 int nDatePre = dDatePre.Year * 10000 + dDatePre.Month * 100 + dDatePre.Day;
                 int nDateNext = dDateNext.Year * 10000 + dDateNext.Month * 100 + dDateNext.Day;
                 RESVSHOWREQ vrResvGet = new RESVSHOWREQ();
                 vrResvGet.dwBeginDate =ToUInt( nDatePre);
                 vrResvGet.dwEndDate =ToUInt( nDateNext);
                 int test = 2;
                 if (test == 1)
                 {
                     vrResvGet.dwClassKind = 8;                   
                 }
                 else if (test == 2)
                 {                   
                     vrResvGet.dwDevKind = 605;
                     vrResvGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
                     vrResvGet.dwCheckStat = 2;
                 }
                            
                 RESVSHOW[] vtReserve;
                 uResponse = m_Request.Reserve.GetReserveForShow(vrResvGet, out vtReserve);
                 szReacher = "";
                 for (int i = 0; vtReserve != null && i < vtReserve.Length; i++)
                 {
                     string szContent = vtReserve[i].szTestName.ToString();
                     string szShowContent=szContent;
                     
                     if (szContent.Length > len)
                     {
                         szContent = szContent.Substring(0, len) + "...";
                     }
                     szReacher += "<li>";
                     string szDevName = vtReserve[i].szDevName.ToString();
                     szReacher += "<span class=\"title\" title=\"" + szShowContent + "\">" + ""+ "" + (vtReserve[i].dwPreDate % 10000) / 100 + "月" + (vtReserve[i].dwPreDate % 100) + "日:" + szContent + "</span>";
                     szReacher += "</li>";                                          
                 }
                 
             }
             if (System.IO.File.Exists(Server.MapPath("./Notice.xml")) == true)
             {
                 XmlDocument xmlDoc = new XmlDocument();
                 xmlDoc.Load(Server.MapPath("./Notice.xml"));
                 XmlNodeList xmlNodeList = xmlDoc.DocumentElement.ChildNodes;
                 for (int i = 0; i < xmlNodeList.Count; i++)
                 {
                     XmlNode node = xmlNodeList[i];
                     string szValue=node.LastChild.InnerText.ToString();
                     if(szValue.Length>26)
                     {
                         szNotice += "<li title=" + szValue + ">" + szValue.Substring(0,25)+ "....</li>";
                     }
                     else{
                         szNotice += "<li>" + szValue + "</li>";
                     }
                 }
             }
         }


    }
    public string GetConfig(string cfg)
    {
        string ret = ConfigurationManager.AppSettings[cfg];
        if (ret == null) return "";
        else return ret;
    }
    private void InitDynInfo(string type)
    {
        dynamicInfo = GetNotice();
    }
    private string GetNotice()
    {
        string noticeList = "";
        XmlCtrl.XmlInfo[] list = GetXmlInfoList("notice", 3);
        if (list != null && list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                string id = list[i].id;
                string title = list[i].title;
                string date = list[i].date;
                string content = list[i].content;
                if (string.IsNullOrEmpty(date)) date = "0";
                string dt = Util.Converter.StrToDate(date).ToString("yyyy年MM月dd日 HH时mm分");
                noticeList += "<li date='" + date + "' id='" + id + "'><div class='title'>▪ " + title + "</div><div class='grey songti' style='border-bottom:1px dashed #ddd;'>" + dt + "</div></li>";

                infoContent += "<div>" + title + "：<div class='con'>" + content + "</div><span class='grey pull-right'>" + dt + "</span></div>";
            }
        }
        return noticeList;
    }
}
