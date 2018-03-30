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
    protected string m_szMonthIn = "";
    protected string m_szMonthOut="";
    protected override void OnLoadComplete(EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["delID"]))
        {
            UNIRESERVE delItem = new UNIRESERVE();
            delItem.dwResvID = ToUint(Request["delID"]);
            m_Request.m_UniDCom.StaSN = 1;
            REQUESTCODE delResult = m_Request.Reserve.Del(delItem);
            if (delResult == REQUESTCODE.EXECUTE_SUCCESS)
            {
            }
        }
        string szDate = DateTime.Now.ToString("yyyyMMdd");
        m_szMonthIn = DateTime.Now.ToString("yyyyMMdd")  + DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
        m_szMonthOut = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd") + DateTime.Now.AddYears(-10).ToString("yyyyMMdd");
        RESVREQ vrGet = new RESVREQ();
        string szCheckStatue=Request["resvStatus"];
        uint uCheckStatue = CharListToUint(szCheckStatue);
        if(uCheckStatue!=0)
        {
            vrGet.dwCheckStat = uCheckStatue;
        }
        else
        {
            //vrGet.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO;
        }
        string resvDate=Request["resvDate"];
        if (resvDate != null && resvDate != "")
        {
            uint uEndDate = 0; 
            uint uStartDate = 0;
            if (resvDate.Length > 15)
            {
                 uEndDate =  Parse(resvDate.Substring(0, 8));
                 uStartDate = Parse(resvDate.Substring(8, 8));
            }
            vrGet.dwBeginDate = uStartDate;
            vrGet.dwEndDate = uEndDate;
        }
        else {
            vrGet.dwBeginDate = Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
            vrGet.dwEndDate = Parse(DateTime.Now.AddDays(7).ToString("yyyyMMdd"));
        }
        string szResvDate = Request["resvDate"];
        
        //
        vrGet.szReqExtInfo.szOrderKey = "dwBeginTime";
        vrGet.szReqExtInfo.szOrderMode = "desc";
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
        UNIRESERVE[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Reserve.Get(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vtRes != null && vtRes.Length > 0)
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    string szDevName="";
                    if (vtRes[i].ResvDev != null && vtRes[i].ResvDev.Length > 0)
                    {
                        szDevName = vtRes[i].ResvDev[0].szDevName.ToString();
                    }
                    m_szOut += "<tr data-id=\"" + vtRes[i].dwResvID + "\"><td  class=\"valname\">" + Get1970Date((uint)vtRes[i].dwBeginTime) + "到" + Get1970Date((uint)vtRes[i].dwEndTime, "HH:mm") + "</td>";
                    m_szOut += "<td class=\"valname\">" + szDevName + "</td>";
                    string szStatue = "";
                    uint uStatue = (uint)vtRes[i].dwStatus;
                    if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                    {
                        szStatue += "生效中，";
                    }
                    if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                    {
                        szStatue += "预约成功，";
                    }
                    if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                    {
                        szStatue += "预约已结束，";
                    }
                    if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT) > 0)
                    {
                        szStatue += "违约，";
                    }
                    if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_SIGNED) > 0)
                    {
                        szStatue += "已签到，";
                    }
                    m_szOut += "<td class=\"valname\">" + szStatue + "</td>";
                    m_szOut += "<td  class=\"valname\"><button class=\"btnCancel\">删除</button></td></tr>";
                }
            }
            else
            {
                m_szOut += "<tr><td colspan=\"3\"><center>无符合条件记录</center></td></tr>";
            }
        }
        else
        {
            m_szOut += "<tr><td colspan=\"3\"><center>获取预约记录失败，" + m_Request.szErrMessage + "</center></td></tr>";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
}
