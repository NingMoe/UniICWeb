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
    protected  string szKindStr="";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szDelID = Request["delID"]; 
        if (szDelID != null && szDelID != "")
        {
            string szOp = Request["op"];
            if (szOp == "beforeDone")
            {
                BeforeDone(szDelID);
            }
            else if (szOp == "resvRdit")
            {
                resvRdit(szDelID);
            }
            else
            {
                DelResv(szDelID);
            }
        }
        RESVREQ vrParameter = new RESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        
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
        UNIDEVKIND[] RoomKind = GetDevKindByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        szKindStr += GetInputItemHtml(CONSTHTML.radioButton, "dwDevKind", "全部", "0", true);    
        if (RoomKind != null && RoomKind.Length > 0)
        {
    
            for(int i=0;i<RoomKind.Length;i++)
            {
                szKindStr+=GetInputItemHtml(CONSTHTML.radioButton,"dwDevKind",RoomKind[i].szKindName.ToString(),RoomKind[i].dwKindID.ToString());
            }
        }
        if (!IsPostBack)
        {
            vrParameter.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwBeginDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwCheckStat = Parse(szCheckStat);
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        }
        if ((szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        }
        if (vrParameter.dwCheckStat!=null&&(((uint)vrParameter.dwCheckStat) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
        {
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
        }
        if (vrParameter.dwCheckStat != null && (((uint)vrParameter.dwCheckStat) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT) > 0)
        {
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
        }
        vrParameter.dwStatFlag = (uint)vrParameter.dwStatFlag + (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        UNIRESERVE[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        
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
                m_szOut += "<td>" + vrResult[i].ResvDev[0].szDevName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                string szOp = "";
                szOp = "'OPTD OPTDDel'";
                if ((uState & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)) > 0|| (uState & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)) > 0)
                {
                    szOp = "'OPTD OPTDCheckok'";
                }
                uint uResvState = (uint)vrResult[i].dwStatus;
                if ((uResvState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0 && (uResvState & (uint)(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_FAIL) > 0)
                {
                    uResvState = uResvState - (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO;
                }
                m_szOut += "<td>" + GetJustName((uResvState), "Reserve_Status") + "</td>";

                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
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
                            m_szOut += "<td title='" + szGroupMemberAll + "'>" + groupMember.Length + "人:" + szGroupMember + "</td>";
                        }
                        else
                        {
                            m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                        }
                    }
                    else
                    {
                        m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                    }

                }
                else
                {
                    m_szOut += "<td>" + vrResult[i].szOwnerName + "</td>";
                }
                string szMemo = vrResult[i].szMemo;
                if (szMemo.Length > 6)
                {
                    szMemo = szMemo.Substring(0, 6)+"。。";
                }
             
                m_szOut += "<td>" + vrResult[i].dwRealUsers.ToString() + "</td>";

                string szTestName= vrResult[i].szTestName;
                if (szTestName.Length > 6)
                {
                    szTestName = szTestName.Substring(0, 6) + "。。";
                }
                m_szOut += "<td title='" + vrResult[i].szTestName + "'>" + szTestName + "</td>";
                m_szOut += "<td title='"+vrResult[i].szMemo+"'>" + szMemo + "</td>";
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
    protected void BeforeDone(string delID)
    {
       
    }
    protected void resvRdit(string delID)
    {
       
    }
}
