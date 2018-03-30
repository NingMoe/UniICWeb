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
using System.IO;
using System.Text;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szRoom="";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            RESVREQ vrParameter = new RESVREQ();
            uint uCheckStat =Parse(Request["dwCheckStat"]);

            uint uDevID = Parse(Request["szGetKey"]);
            if (uDevID != 0)
            {
                vrParameter.dwDevID = uDevID;
            }
            string szPID = Request["dwPID"];
            if (szPID != null && szPID != "")
            {
                UNIACCOUNT accInfo;
                if (GetAccByLogonName(szPID, out accInfo))
                {

                    vrParameter.dwOwner = accInfo.dwAccNo;
                }
            }
            uint uKindID = Parse(Request["dwDevKind"]);
            if (uKindID != 0)
            {
                vrParameter.dwDevKind = uKindID;
            }

            CODINGTABLEREQ codeReq = new CODINGTABLEREQ();
            codeReq.dwCodeType = (uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND;
            CODINGTABLE[] vtCodeRes;
            m_Request.System.GetCodingTable(codeReq, out vtCodeRes);


            vrParameter.dwBeginDate = GetDate(Request["dwStartDate"]);
            vrParameter.dwEndDate = GetDate(Request["dwEndDate"]);
            vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            if (!(uCheckStat == 0))
            {
                vrParameter.dwCheckStat = (uCheckStat);
                vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
            }
            if (uCheckStat == 0)
            {
                vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
            }
            if (vrParameter.dwCheckStat != null && (((uint)vrParameter.dwCheckStat) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
            {
                vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            }
            if (vrParameter.dwCheckStat != null && (((uint)vrParameter.dwCheckStat) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT) > 0)
            {
                vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            }
            vrParameter.dwStatFlag = (uint)vrParameter.dwStatFlag + 8;
            UNIRESERVE[] vrResult;
             if (m_Request.Reserve.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("预约号,申请人,学/工号,手机,邮箱,部门,身份,主题,空间名称,区域,状态,提交时间,申请时间,预约类型,预约人,使用人数,申请说明");

                
                uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                for (int i = 0; i < vrResult.Length; i++)
                {
                    string szKind = "";
                    if (vrResult[i].dwKind != null && vrResult[i].dwKind != 0)
                    {
                        if (vtCodeRes != null && vtCodeRes.Length > 0)
                        {
                            for (int m = 0; m < vtCodeRes.Length; m++)
                            {
                                if (vtCodeRes[m].szCodeSN == vrResult[i].dwKind.ToString())
                                {
                                    szKind = vtCodeRes[m].szCodeName;
                                    break;
                                }
                            }
                        }
                    }
                    uint uState = (uint)vrResult[i].dwStatus;
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    sbText = AppendCSVFields(sbText, vrResult[i].dwResvID.ToString());
                    UNIACCOUNT account;
                    string szTrueName = "";
                    if (vrResult.Length > 50)
                    {
                        if (vrResult[i].dwOwner != null && GetAccByAccno(vrResult[i].dwOwner.ToString(), out account, true))
                        {
                            szTrueName = (account.szTrueName);
                            AppendCSVFields(sbText, szTrueName);
                            AppendCSVFields(sbText, account.szLogonName);
                            AppendCSVFields(sbText, account.szHandPhone);
                            AppendCSVFields(sbText, account.szEmail);
                            AppendCSVFields(sbText, account.szDeptName);
                            uint uIdent = (uint)account.dwIdent;
                            if ((uIdent & 256) > 0)
                            {
                                AppendCSVFields(sbText, "学生");
                            }
                            else if ((uIdent & 512) > 0)
                            {
                                AppendCSVFields(sbText, "教师");
                            }
                            else {
                                AppendCSVFields(sbText, "");
                            }

                        }
                        else
                        {
                            AppendCSVFields(sbText, vrResult[i].szOwnerName.ToString());
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                        }
                    }
                    else
                    {
                        if (vrResult[i].dwOwner != null && GetAccByAccno(vrResult[i].dwOwner.ToString(), out account))
                        {
                            szTrueName = (account.szTrueName);
                            AppendCSVFields(sbText, szTrueName);
                            AppendCSVFields(sbText, account.szLogonName);
                            AppendCSVFields(sbText, account.szHandPhone);
                            AppendCSVFields(sbText, account.szEmail);
                            AppendCSVFields(sbText, account.szDeptName);
                            uint uIdent = (uint)account.dwIdent;
                            if ((uIdent & 256) > 0)
                            {
                                AppendCSVFields(sbText, "学生");
                            }
                            else if ((uIdent & 512) > 0)
                            {
                                AppendCSVFields(sbText, "教师");
                            }
                            else
                            {
                                AppendCSVFields(sbText, "");
                            }
                        }
                        else
                        {
                            AppendCSVFields(sbText, vrResult[i].szOwnerName.ToString());
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                            AppendCSVFields(sbText, "");
                        }
                    }
                    AppendCSVFields(sbText, vrResult[i].szTestName);
                    AppendCSVFields(sbText, vrResult[i].ResvDev[0].szDevName);
                    AppendCSVFields(sbText, vrResult[i].szLabName.ToString());

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
                    AppendCSVFields(sbText, GetJustName((uResvState), "Reserve_Status"));
                    AppendCSVFields(sbText, Get1970Date((uint)vrResult[i].dwOccurTime, "yyyy-MM-dd HH:mm"));
                    AppendCSVFields(sbText, Get1970Date((uint)vrResult[i].dwBeginTime, "yyyy-MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "yyyy-MM-dd HH:mm"));
                    AppendCSVFields(sbText,szKind);
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
                                AppendCSVFields(sbText, szGroupMemberAll);
                            }
                            else
                            {
                                AppendCSVFields(sbText, vrResult[i].szOwnerName);
                            }
                        }
                        else
                        {

                            AppendCSVFields(sbText, vrResult[i].szOwnerName);
                        }

                    }
                    else
                    {
                        AppendCSVFields(sbText, vrResult[i].szOwnerName);
                    }
                    string szMemo = vrResult[i].szMemo;
                    if (szMemo.Length > 6)
                    {
                        szMemo = szMemo.Substring(0, 6) + "。。";
                    }
                    AppendCSVFields(sbText, vrResult[i].dwRealUsers.ToString());
                    AppendCSVFields(sbText, vrResult[i].szMemo.ToString());
                    sbText.Remove(sbText.Length - 1, 1);
                    swCSV.WriteLine(sbText.ToString());
 
                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "RuleDaySum.csv");
                swCSV.Close();
                Response.End();
            }
        }
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }
    public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("预约记录.csv");
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}