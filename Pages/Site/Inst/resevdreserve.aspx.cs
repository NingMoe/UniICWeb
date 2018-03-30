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
    protected string szKindStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szDelID = Request["delID"];
        string szOpTemp = Request["op"];
        if (szDelID != null && szDelID != "")
        {
            
            if (szOpTemp == "dellist")
            {
                DelList(szDelID);
            }
            else if (szOpTemp == "resvRdit")
            {
                resvRdit(szDelID);
            }
            else
            {
                DelResv(szDelID);
            }
        }
        if (szOpTemp == "delall")
        {
            DelResvAll(Request["delAllID"]);
        }
    
        RESVREQ vrParameter = new RESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        uint uDevID = Parse(Request["szGetKey"]);
        if (uDevID != null && uDevID != 0)
        {
            vrParameter.dwDevID = uDevID;
        }
        string szPID = Request["dwPID"];
        if (szPID != null && szPID != "")
        {
            UNIACCOUNT accInfo;
            if (GetAccByLogonName(szPID, out accInfo))
            {
                //vrParameter.dwGetType = (uint)RESVREQ.DWGETTYPE.RESVGET_BYOWNER;
                vrParameter.dwOwner = accInfo.dwAccNo;
            }
        }
        uint uKindID = Parse(Request["dwDevKind"]);
        if (uKindID != null && uKindID != 0)
        {
            vrParameter.dwDevKind = uKindID;
        }
      
        if (!IsPostBack)
        {
            vrParameter.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwBeginDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
       
        UNIRESERVE[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwCheckStat = Parse(szCheckStat);
        }

        if (szCheckStat != null && szCheckStat == "4")
        {
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL;
            vrParameter.dwCheckStat = null;
        }
        if (m_Request.Reserve.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            int count = 0;
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            UpdatePageCtrl(m_Request.Reserve);
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwStatus;
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                UNIACCOUNT account;
                string szTrueName = "";
                if (vrResult[i].dwOwner != null && GetAccByAccno(vrResult[i].dwOwner.ToString(), out account))
                {
                    szTrueName = (account.szTrueName);
                    m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwOwner.ToString() + "' title='查看个人信息'><a href=\"#\">" + account.szTrueName + "(" + account.szLogonName + ")" + "</a></td>";
                }
                else
                {
                    m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwOwner.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szOwnerName.ToString() + "</a></td>";
                }
                string szDevName = "";
                if (vrResult[i].ResvDev.Length > 0)
                {
                    szDevName = vrResult[i].ResvDev[0].szDevName;
                }
                m_szOut += "<td>" + szDevName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                string szOp = "";
                if ((uState & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING)) > 0)
                {
                    if ((uState & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO)) > 0)
                    {
                        szOp = "'OPTD OPTDCheckDel'";
                    }
                    else
                    {
                        szOp = "'OPTD OPTDCheck'";
                    }
                }
                else
                {
                    szOp = "";
                    if ((uState & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO)) > 0)
                    {
                        szOp = "'OPTD OPTDDel'";
                    }
                    else
                    {
                        szOp = "'OPTD OPTDGet'";
                    }
                }
               
                uint uResvState = (uint)vrResult[i].dwStatus;
                if ((uResvState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0 && (uResvState & (uint)(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_FAIL) > 0)
                {
                    uResvState = uResvState - (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO;
                }
                m_szOut += "<td>" + GetJustName((uResvState), "Reserve_Status") + "</td>";

              //  m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                if ((((uint)vrResult[i].dwMemberKind) & ((uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP)) > 0)
                {
                    UNIGROUP[] resvGroupList = GetGroupByID((uint)vrResult[i].dwMemberID);
                    if (resvGroupList != null && resvGroupList.Length > 0)
                    {
                        UNIGROUP resvGroup = new UNIGROUP();
                        resvGroup = resvGroupList[0];
                        GROUPMEMBER[] groupMember = resvGroup.szMembers;
                        if (groupMember != null)
                        {
                            string szGroupMember = "";
                            string szGroupMemberAll = "";
                            for (int j = 0; j < groupMember.Length; j++)
                            {
                                if (j < 3)
                                {
                                    szGroupMember += groupMember[j].szName + ",";
                                }
                                szGroupMemberAll += groupMember[j].szName + ",";
                            }
                           // m_szOut += "<td title='" + szGroupMemberAll + "'>" + groupMember.Length + "人:" + szGroupMember + "</td>";
                        }
                        else
                        {
                           // m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                        }
                    }
                    else
                    {
                       // m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                    }

                }
                else
                {
                 //   m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                }
                m_szOut += "<td>" +vrResult[i].szTestName + "</td>";
                szOp = "'OPTD'";
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            }

        }
        PutBackValue();
    }
    protected void DelResv(string delID)
    {
        UNIRESERVE delResv = new UNIRESERVE();
        delResv.dwResvID = Parse(delID);
        m_Request.Reserve.Del(delResv);
    }
    protected void DelList(string delID)
    {
        //1groupid中的一个resvid
        UNIRESERVE[] vtResv = getResvByID(Parse(delID));
        UNIRESERVE[] vtResvGroup = getResvByGroupID(Parse(delID));
        if(vtResv!=null&&vtResv.Length>0)
        {
            UNIRESERVE[] vtResvGroupTem = getResvByGroupID((uint)vtResv[0].dwResvGroupID);
            delResv(vtResvGroupTem);
            UNIRESERVE[] vtResvOne = getResvByID((uint)vtResv[0].dwResvGroupID);
            delResv(vtResvOne);
        }
        delResv(vtResvGroup);
        delResv(vtResv);
      

    }
    protected void delResv(UNIRESERVE[] resvList)
    {
        if (resvList != null && resvList.Length > 0)
        {
            for (int i = 0; i < resvList.Length; i++)
            {
                m_Request.Reserve.Del(resvList[i]);
            }
        }
    }
    protected void DelResvAll(string szResvID)
    {
        string[] szList = szResvID.Split(',');
        for (int i = 0; i < szList.Length; i++)
        {
            if (szList[i] != null && szList[i] != "")
            {
                DelResv(szList[i]);
            }
        }
    }
    protected UNIRESERVE[] getResvByID(uint id)
    {
        RESVREQ resvReq = new RESVREQ();
        resvReq.dwResvGroupID = (id);
        UNIRESERVE[] vtRes;
        if (m_Request.Reserve.Get(resvReq, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
        }
        return vtRes;
    }
    protected UNIRESERVE[] getResvByGroupID(uint id)
    {
        RESVREQ resvReq = new RESVREQ();
        resvReq.dwResvGroupID = id;
        UNIRESERVE[] vtRes;
        if (m_Request.Reserve.Get(resvReq, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
        }
        return vtRes;
    }
    protected void BeforeDone(string delID)
    {

    }
    protected void resvRdit(string delID)
    {

    }
}