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
        TEACHINGRESVREQ vrParameter = new TEACHINGRESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
         
        }
        string szResvID = Request["delID"];
        if (!IsPostBack && (szResvID == null || szResvID==""))
        {
            vrParameter.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(6).ToString("yyyy-MM-dd"));

            dwBeginDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd");
        }
        if (dwBeginDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwBeginDate = GetDate(dwBeginDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
     
        if (szResvID != null && szResvID != "")
        {
            UNIRESERVE delResv = new UNIRESERVE();
            delResv.dwResvID = Parse(szResvID);
            m_Request.Reserve.Del(delResv);
         
        }
       
        //vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
      //  vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwResvStat = Parse(szCheckStat);
        }
        string szRoomNo = Request["szRoomNo"];
        if (!string.IsNullOrEmpty(szRoomNo))
        {
            vrParameter.szRoomNo = szRoomNo;
        }
        string szPid = Request["dwPID"];
        if (szPid != null && szPid != "")
        {
            UNIACCOUNT accno;
            if (GetAccByLogonName((szPid.ToString().Trim()), out accno,true))
            {
                vrParameter.dwTeacherID = accno.dwAccNo;
            }
        }
           TEACHINGRESV[] vrResult;
       // vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetTeachingResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            UNIROOM[] allRoom = GetAllRoom();
            for (int i = 0; i < vrResult.Length; i++)
            {
                UpdatePageCtrl(m_Request.Reserve);
                uint uState = (uint)vrResult[i].dwResvStat;                               
                m_szOut += "<tr>";
                m_szOut += "<td data-groupid='" + vrResult[i].dwGroupID.ToString() + "' data-resvDate='" + vrResult[i].dwPreDate.ToString() + "' data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestName + "</td>";  
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwTeacherID.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szTeacherName.ToString() + "</a></td>";
                UNIROOM[] roomList;
                RESVDEV[] resvdev=vrResult[i].ResvDev;
                string szRoomName = "";
                for (int k = 0; k < resvdev.Length; k++)
                {
                    string szRoomNameTemp=getRoomName(allRoom, (uint)vrResult[i].dwLabID, resvdev[k].szRoomNo,szRoomName);
                    szRoomName=szRoomName+szRoomNameTemp;
                        /*
                    roomList = GetRoomByNO(resvdev[k].szRoomNo, vrResult[i].dwLabID);
                    if (roomList != null && roomList.Length > 0)
                    {
                        if (szRoomName.IndexOf(roomList[0].szRoomName) < 0)
                        {
                            szRoomName += roomList[0].szRoomName + ",";
                        }
                    }
                         * */
                }
                
                if (szRoomName.EndsWith(","))
                {
                    szRoomName = szRoomName.Substring(0,szRoomName.Length-1);
                }
                m_szOut += "<td>" + szRoomName + "</td>";
                m_szOut += "<td class='classgroup' data-groupid='"+vrResult[i].dwGroupID.ToString()+"'>" + vrResult[i].szGroupName + "</td>";  
                m_szOut += "<td>" + GetJustName((vrResult[i].dwResvStat), "Reserve_Status") + "</td>";
                //m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";

                TEACHINGRESV setValue = vrResult[i];
                string szBeginTime = Get1970Date(setValue.dwBeginTime, "yyyy-MM-dd HH:mm");
                string szEndtime =  Get1970Date(setValue.dwEndTime, "HH:mm");
                string szTeachTime = GetTeachingTime((uint)setValue.dwTeachingTime);
                m_szOut += "<td title='" + szBeginTime +"到"+szEndtime+ "'>" + szTeachTime + "</td>";
                string szOp = "";
                m_szOut += "<td>" + szBeginTime + "到" + szEndtime + "</td>";
                if ((!(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)) || !(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))))
                {
                    if ((uint)setValue.dwTeachingTime <99999)
                    {
                        szOp = "'OPTD OPTDDel'";
                    }
                    else
                    {
                        szOp = "'OPTD OPTD" + uState + "'";
                        
                    }
                }
                else
                {
                    szOp = "";
                }
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            } 
        }
        PutBackValue();
    }
    protected string getRoomName(UNIROOM[] roomList, uint uLabID, string szRoomNo, string szRoomName)
    {
        string szRoomNameTemp = "";
        for (int i = 0; i < roomList.Length; i++)
        {
            if (roomList[i].dwLabID == uLabID && (szRoomNo == roomList[i].szRoomNo))
            {
                szRoomNameTemp = roomList[i].szRoomName;
                if (szRoomName.IndexOf(roomList[i].szRoomName) < 0)
                {
                    return roomList[i].szRoomName + ",";
                }
            }
        }
        return "";
    }
}
