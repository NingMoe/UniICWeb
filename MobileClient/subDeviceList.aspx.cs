using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : PageBase
{
    protected MyString m_szOut = new MyString();
    protected string szSearchDate="";
    protected uint uDate = 0;
    protected uint uKindIDHiden = 0;
    protected override void OnLoadComplete(EventArgs e)
    {
        uint uClassKind = Parse(Request["classKind"]);
        uint uKindID = Parse(Request["KindId"]);
        uDate = Parse(Request["dwDate"]);
      
        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
        //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ALL;
        if (uClassKind != 0)
        {
            vrGet.dwClassKind = uClassKind;
        }

        if (uKindID != 0)
        {
            Session["uKindIDHiden"]=uKindID;
        }
        if (uKindID == 0 && Session["uKindIDHiden"] != null)
        {
            uKindID = Parse(Session["uKindIDHiden"].ToString());
         
        }
       
        vrGet.szKindIDs = uKindID.ToString();

        if (vrGet.szKindIDs == "" || vrGet.szKindIDs=="0")
        {
            vrGet.szKindIDs = null;
        }
      
        vrGet.szDates =  uDate.ToString();
        szSearchDate = (uDate / 10000) + "-" + (((uDate % 10000) / 100)).ToString("00") + "-" + (uDate % 100).ToString("00");
        vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
        vrGet.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
      
        vrGet.szReqExtInfo.szOrderKey = "szDevName";
        vrGet.szReqExtInfo.szOrderMode = "desc";
        DEVRESVSTAT[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].dwProperty != null)
                {
                    uint uProperty = (uint)vtRes[i].dwProperty;
                    if ((uProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV) > 0)
                    {
                        continue;
                    }
                }
               
                bool bUsable = true;  //是否可预约

                m_szOut += "<div class=\"Item\" data-kindid=\""+vtRes[i].dwKindID+"\" data-id=\"" + vtRes[i].dwDevID + "\">";
                m_szOut += "<div class=\"LHead\">"+vtRes[i].szDevName+"</div>";
                //m_szOut += "<div class=\"LContent\">"+vtRes[i].szExtInfo+"</div>";
                m_szOut += "<div class=\"LGraphics\"><span>预约状态图，</span><span class=\"enableStat\">■可预约</span>";
                uint uStartTime = (uint)vtRes[i].szOpenInfo[0].dwBegin;
                string szOpenStartTime = uStartTime / 100 + "." + uStartTime % 100;
                uint uEndTime = (uint)vtRes[i].szOpenInfo[0].dwEnd;
                string szEndTime = uEndTime / 100 + "." + uEndTime % 100;

                bool bIsOpen = true;
                DAYOPENRULE[] dayopen = vtRes[i].szOpenInfo;
                if (dayopen.Length > 0)
                {
                    if (dayopen[0].dwBegin >= dayopen[0].dwEnd)
                    {
                        bIsOpen = false;
                    }
                }
               UNIRESVRULE resv= vtRes[i].szRuleInfo;
                if (resv.dwRuleSN != null)
                {
                    uint uEarlisteResvTime=(uint)resv.dwEarliestResvTime;
                    uint uDateEarlist=uEarlisteResvTime / 1440;
                    uint uDateTemp = Parse(DateTime.Now.AddDays(uDateEarlist).ToString("yyyyMMdd"));
                    if (uDate > uDateTemp)
                    {
                        bIsOpen = false;
                    }
                }
                if (bIsOpen == false)
                {
                    szOpenStartTime = "8.00";
                    szEndTime = "21.00";
                }
                string szResvInfo = "[";
                if (vtRes[i].szResvInfo != null && vtRes[i].szResvInfo.Length > 0)
                {
                    for (int m = 0; m < vtRes[i].szResvInfo.Length; m++)
                    {
                        
                        if ((int)vtRes[i].szResvInfo[m].dwBegin < uStartTime)
                        {
                            vtRes[i].szResvInfo[m].dwBegin = uStartTime;
                        }
                        if ((int)vtRes[i].szResvInfo[m].dwEnd>uEndTime)
                        {
                            vtRes[i].szResvInfo[m].dwEnd = uEndTime+100;
                        }
                        
                        szResvInfo += ((int)vtRes[i].szResvInfo[m].dwBegin * 10000 + (int)vtRes[i].szResvInfo[m].dwEnd);
                        if ((m + 1) < (vtRes[i].szResvInfo.Length))
                        {
                           szResvInfo += ",";  
                        }
                    }
                }
                szResvInfo += "]";
                if (bIsOpen == false)
                {
                    szResvInfo = "[8002200]";
                }

                    m_szOut += "<canvas id='cav" + i.ToString() + "' data-start =\"" + szOpenStartTime + "\" data-end =\"" + szEndTime + "\" data-list=\"" + szResvInfo + "\"></canvas></div>";

                if (bUsable&&(bIsOpen!=false))
                {
                    m_szOut += "<div class=\"LBtn\"><button>预约→</button></div>";
                }
                else if(!bUsable)
                {
                    m_szOut += "<div class=\"LBtn\">已被预约满</div>";
                }
                else if (bIsOpen == false)
                {
                    m_szOut += "<div class=\"LBtn\">不开放</div>";
                }

                m_szOut += "</div>";
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
}
