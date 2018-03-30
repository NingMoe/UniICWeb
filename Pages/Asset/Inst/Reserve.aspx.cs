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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        RTRESVREQ vrParameter = new RTRESVREQ();

        vrParameter.dwUnNeedStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
        string szCheckStat = Request["dwCheckStat"];
        if (szCheckStat == null)
        {
           szCheckStat = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK).ToString();
        }
        if (szCheckStat != null && szCheckStat != "")
        {
            vrParameter.dwCheckStat = CharListToUint(szCheckStat);
        }
        if (vrParameter.dwCheckStat == null)
        {
           // vrParameter.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK; 
        }
        else
        {
            if (((uint)vrParameter.dwCheckStat==(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK))
            {
                vrParameter.dwCheckStat = (uint)vrParameter.dwCheckStat;
                vrParameter.dwUnNeedStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK + (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
            }
        }
        RTRESV[] vrResult;       
       // GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetRTResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            int count = 0;
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {
               
                uint uState = (uint)vrResult[i].dwStatus;
                if (vrParameter.dwCheckStat!=null&&((uint)vrParameter.dwCheckStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)
                {
                    if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNPAID)>0)
                    {
                        continue;
                    }
                }
                if ((szCheckStat == "8" || szCheckStat == "1024" || szCheckStat == "512") && uTimeNow >= (uint)vrResult[i].dwEndTime)
                {
                    continue;
                }
                count=count+1;
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\""+vrResult[i].dwResvID.ToString()+"\">" + vrResult[i].szTestName.ToString() + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwOwner.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szOwnerName.ToString() + "</a></td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwLeaderID.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szLeaderName.ToString() + "</a></td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString()+ "</td>";
              
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustName(GetState(uState), "Reserve_Status") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                string szOp = "";
                uState = GetState(uState);
                if ((!(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)) || !(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))))
                {
                    szOp = "'OPTD OPTD" + uState + "'";
                }
                else
                {
                    szOp = "";
                }
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();
    }
    private uint GetState(uint? uState)
    {
        if ((uState & 2) == 0 && (uState & 4) == 0)
        {
            return ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO);
        }
        else if (((uState & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) && (uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNPAID) > 0)
        {
            return ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNPAID);
        }
        else if ((!((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)) && ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_PAID) > 0) && (!((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)))
        {
            return ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO);
        }
        else if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
        {
            return ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING);
        }
        else if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNRECEIVE) > 0)
        {
            string szCheckStat = Request["dwCheckStat"];
            if (szCheckStat != "1048576")
            {
                return (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNRECEIVE;
            }
            else
            {
                return (uint)UNIRESERVE.DWSTATUS.RESVSTAT_SETTLED;
            }
        }
        else if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_RECEIVED) > 0)
        {
            return (uint)UNIRESERVE.DWSTATUS.RESVSTAT_RECEIVED;
        }
        else if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)
        {
            return (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE;
        }
        else if ((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_SETTLED) > 0)
        {
            return (uint)UNIRESERVE.DWSTATUS.RESVSTAT_SETTLED;
        }
       
        return 0;
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNILAB lab = new UNILAB();
        lab.dwLabID=Parse(szID);
        uResponse=m_Request.Device.LabDel(lab);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
