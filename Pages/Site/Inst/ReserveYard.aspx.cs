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
    protected string szBuilding = "";
    protected string szCamp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        UNIBUILDING[] vtBuilding=getAllBuilding();
        szBuilding=GetInputItemHtml(CONSTHTML.option,"","全部","0");
        if(vtBuilding!=null)
        {
            for (int i = 0; i < vtBuilding.Length; i++)
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName, vtBuilding[i].dwBuildingID.ToString());
            }
        }
        UNICAMPUS[] vtCamp = GetAllCampus();
        szCamp += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < vtCamp.Length; i++)
        {
            szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
        }
      

        YARDRESVREQ vrParameter = new YARDRESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        //string szKey = Request["szGetKey"];
        string szPID = Request["dwPID"];
        string szDelID = Request["delID"];
        
        if (szDelID != null && szDelID != "")
        {
            DelResv(szDelID);
        }
        if (!IsPostBack)
        {
            vrParameter.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        }
        if (szPID != null && szPID != "")
        {
            UNIACCOUNT accno;
            if (GetAccByLogonName(szPID,out accno))
            {
                vrParameter.dwApplicantID = accno.dwAccNo;
            }
        }
        /*
        if (szKey != null && szKey != "")
        {
            roomid
            vrParameter.dwDevID = Parse(szKey);
            
        }*/
        string szRoomIDs = Request["roomid"];
        if (szRoomIDs!=null&&szRoomIDs != "0"&&szRoomIDs!="")
        {
            vrParameter.szRoomIDs = szRoomIDs;
        }
        string szBuildingIDs = Request["szBuildingIDs"];
        if (szBuildingIDs != "0")
        {
            vrParameter.szBuildingIDs = szBuildingIDs;
        }
        string szCampusIDs = Request["szCampusIDs"];
        if (szCampusIDs != "0")
        {
            vrParameter.szCampusIDs = szCampusIDs;
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwBeginDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
      //  vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwCheckStat = Parse(szCheckStat);
           
        }
        if (szCheckStat != null && szCheckStat == "32")
        {
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL;
            vrParameter.dwCheckStat = null;
        }
        YARDRESV[] vrResult;       
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetYardResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwStatus;                               
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwApplicantID.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szApplicantName.ToString() + "</a></td>";                
                m_szOut += "<td>" + vrResult[i].szDevName+ "</td>";
                
                if (((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0) && (((uState & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0)))
                {
                    vrResult[i].dwStatus = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
                }
                m_szOut += "<td>" + GetJustName((vrResult[i].dwStatus), "Reserve_Status") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                string szOp = "";

               

                if ((!(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)) || !(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))))
                {
                    szOp = "'OPTD OPTD" + uState + "'";
                }
                else
                {
                    szOp = "";
                }
                m_szOut += "<td>" + vrResult[i].szBuildingName + "</td>"; 
                string szDetail = vrResult[i].szMemo;
                if (szDetail.Length > 10)
                {
                    szDetail = szDetail.Substring(0, 10) + "...";
                }
                m_szOut += "<td title='" + vrResult[i].szMemo + "'>" + szDetail + "</td>";
                szOp = "OPTD";
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();
    }
    protected void DelResv(string delID)
    {
        YARDRESVREQ vrGet = new YARDRESVREQ();
        vrGet.dwResvID = Parse(delID);
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes)==REQUESTCODE.EXECUTE_SUCCESS&&vtRes!=null&&vtRes.Length>0)
        {
            YARDRESV delResv = new YARDRESV();
            delResv = vtRes[0];
            m_Request.Reserve.DelYardResv(delResv);
        }
       
    }
}
