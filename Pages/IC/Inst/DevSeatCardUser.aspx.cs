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
    protected string m_szRooms = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIROOM[] roomList = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        if (roomList != null && roomList.Length > 0)
        {
            m_szRooms += GetInputItemHtml(CONSTHTML.option, "", "全部","0");
            for (int i = 0; i < roomList.Length; i++)
            {
                m_szRooms += GetInputItemHtml(CONSTHTML.option, "", roomList[i].szRoomName, roomList[i].szRoomNo.ToString());
            }
        }

        RESVREQ vrParameter = new RESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        string szKey = Request["szGetKey"];        
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        string szroomnos = Request["roomnos"];
        if (szroomnos != null && szroomnos != ""&& szroomnos!="0")
        {
            vrParameter.szRoomNos = szroomnos;
        }
        string szlogonName = Request["szlogonName"];
        if (szlogonName != null && szlogonName != "")
        {
            UNIACCOUNT acc = new UNIACCOUNT();
            if (GetAccByLogonName(szlogonName, out acc))
            {
                vrParameter.dwAccNo = acc.dwAccNo;
            }

        }

        UNIRESERVE[] vrResult;
        string szID = Request["ID"];
        if (szID != null && szID != "")
        {
            Del(szID);
        }
        string szOpDo = Request["op"];
        if (szOpDo == "stopLeavl")
        {
            string resvID = Request["stopAllID"];
            Stop(resvID);
        }
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwCheckStat = Parse(szCheckStat);
        }
        if (szKey != null && szKey != "")
        {
           // vrParameter.dwGetType = (uint)RESVREQ.DWGETTYPE.RESVGET_BYDEVID;
            vrParameter.dwDevID =Parse(szKey);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        if (m_Request.Reserve.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Reserve);           
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {                             
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                string szTrueName = "";
                UNIACCOUNT account;
                if (vrResult[i].dwOwner != null&&GetAccByAccno(vrResult[i].dwOwner.ToString(),out account))
                {
                    szTrueName = (account.szTrueName)+ account.szLogonName;
                }
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwOwner.ToString() + "' title='查看个人信息'><a href=\"#\">" + szTrueName+ "</a></td>";                
                m_szOut += "<td>" + vrResult[i].ResvDev[0].szDevName+ "</td>";
                m_szOut += "<td>" + vrResult[i].ResvDev[0].szRoomName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustName((vrResult[i].dwStatus), "Reserve_Status") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                string szOp = "OPTD";               
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            }
            
        }
        PutBackValue();
    }
    protected void Del(string szID)
    {
        string[] szResvList = szID.Split(',');
        for (int i = 0; i < szResvList.Length; i++)
        {
            string szIDTemp = szResvList[i];
            UNIRESERVE setResv = new UNIRESERVE();
            if (GetResvByID(szIDTemp, out setResv))
            {
                DateTime dt = DateTime.Now.AddSeconds(30);
                setResv.dwEndTime = Get1970Seconds(dt.ToString("yyyy-MM-dd HH:mm"));
                //m_Request.Reserve.Set(setResv, out setResv);
                m_Request.Reserve.ResvEarlyEnd(setResv);
            }
        }
    }
    protected void Stop(string szResvID)
    {
        string[] szResvList = szResvID.Split(',');
        for (int i = 0; i < szResvList.Length; i++)
        {
            UNIRESERVE resv;
            if (GetResvByID(szResvList[i], out resv))
            {
                DEVMANUSE devuser = new DEVMANUSE();
                devuser.dwMode = (uint)DEVMANUSE.DWMODE.MANMODE_LEAVE;
                devuser.dwLabID = resv.dwLabID;
                DEVREQ devReq=new DEVREQ();
                devReq.szLabIDs = resv.dwLabID.ToString();
                devReq.dwDevSN = resv.ResvDev[0].dwDevStart;
                devReq.szRoomIDs = resv.ResvDev[0].dwRoomID.ToString();
                devReq.szKindIDs = resv.ResvDev[0].dwDevKind.ToString();
                UNIDEVICE[] devList;
                if (m_Request.Device.Get(devReq, out devList) == REQUESTCODE.EXECUTE_SUCCESS && devList != null && devList.Length > 0)
                {
                    devuser.dwDevID = devList[0].dwDevID;
                    devuser.dwResvID = Parse(szResvList[i]);
                    REQUESTCODE ures = m_Request.Device.DevManUse(devuser);
                }
            }
        }
    }
}
