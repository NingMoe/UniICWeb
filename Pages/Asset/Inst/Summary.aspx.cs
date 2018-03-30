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
using UniWebLib;

public partial class Sub_Summary : UniPage
{
    protected string m_szResv = "";
    protected string m_szResvNumTotal = "0";
    protected string m_szDevUse= "";
    protected string m_szDevUseNumTotal = "0";
    protected int nIsAdminSup = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LABCTR) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LAB) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM) > 0)
            {
                nIsAdminSup = 0;
            }
        }
        REQUESTCODE uResponse=REQUESTCODE.EXECUTE_FAIL;
        RTRESVREQ vrParameter = new RTRESVREQ();
        string szCheckStat = Request["dwCheckStat"];
       // vrParameter.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
        RTRESV[] vrResult;
        uint uNone = 0;
        uint uUncost = 0;
        uint uUnipaid = 0;
        uint uUnRecice = 0;
        uResponse = m_Request.Reserve.GetRTResv(vrParameter, out vrResult);
        if ( uResponse== REQUESTCODE.EXECUTE_SUCCESS&&vrResult!=null&&vrResult.Length>0)
        {
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uResvStatus = (uint)vrResult[i].dwStatus;
                if ((((uint)vrResult[i].dwStatus) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNRECEIVE) > 0)
                {
                    uUnRecice = uUnRecice + 1;
                }
                if ((uResvStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)== 0)
                {
                    continue;
                }

                if (((uResvStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO) == 0) && (uResvStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK + (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) == 0)
                {
                    if (uTimeNow >= (uint)vrResult[i].dwEndTime)
                    {
                        continue;
                    }

                    uNone = uNone+1;
                }
                else if ((((uint)vrResult[i].dwStatus) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNPAID) > 0)
                {
                    uUnipaid = uUnipaid+1;
                }
                else if ((((uint)vrResult[i].dwStatus) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)
                {
                    uUncost = uUncost+1;
                }
               
            }
            m_szResvNumTotal = vrResult.Length.ToString();
        }
        m_szResv += "<p data-value=" + uNone + ">未审核：" + uNone + "台</p>";
        m_szResv += "<p data-value=" + uUncost + ">未结算：" + uUncost + "台</p>";
      //  m_szResv += "<p data-value=" + uUncost + ">未入账：" + uUnRecice + "台</p>";

        DEVREQ vrParameterDev = new DEVREQ();
        vrParameterDev.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RUNNING + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE;
        UNIDEVICE[] vrResultDev;
        uint uRunning=0;
        uint uDevInuse=0;
        uint uDevResv=0;
        uResponse = m_Request.Device.Get(vrParameterDev, out vrResultDev);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResultDev != null && vrResultDev.Length>0)
        {
            for (int i = 0; i < vrResultDev.Length; i++)
            {
                if ((((uint)vrResultDev[i].dwRunStat) & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                {
                    uDevInuse = uDevInuse + 1;
                    uRunning = uRunning + 1;
                }
                else if ((((uint)vrResultDev[i].dwRunStat) & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE) > 0)
                 {
                     uDevResv = uDevResv + 1;
                 }
                else if ((((uint)vrResultDev[i].dwRunStat) & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RUNNING) > 0)
                {
                    uRunning = uRunning + 1;
                }                              
            }
            m_szDevUseNumTotal = vrResultDev.Length.ToString();
        }
        m_szDevUse += "<p data-value=" + uDevInuse + ">使用中：" + uDevInuse + "台</p>";
        m_szDevUse += "<p data-value=" + uRunning + ">开机中：" + uRunning + "台</p>";        
        m_szDevUse += "<p data-value=" + uDevResv + ">被预约：" + uDevResv + "台</p>";
    }
}
