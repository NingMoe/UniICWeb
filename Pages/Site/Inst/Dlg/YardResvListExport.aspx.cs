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
            YARDRESVREQ vrParameter = new YARDRESVREQ();
            string szCheckStat = Request["dwCheckStat"];
            string szKey = Request["szGetKey"];
            string szPID = Request["dwPID"];
            string szDelID = Request["delID"];
        
          
            if (szPID != null && szPID != "")
            {
                UNIACCOUNT accno;
                if (GetAccByLogonName(szPID, out accno))
                {
                    vrParameter.dwApplicantID = accno.dwAccNo;
                }
            }
            if (szKey != null && szKey != "")
            {

                vrParameter.dwDevID = Parse(szKey);

            }
            string szBuildingIDs = Request["szBuildingIDs"];
            if (szBuildingIDs != "0")
            {
                vrParameter.szBuildingIDs = szBuildingIDs;
            }
          
            uint uBeginDate = GetDate(Request["dwStartDate"]);
            uint uEndDate = GetDate(Request["dwEndDate"]);
            vrParameter.dwBeginDate = uBeginDate;
            vrParameter.dwEndDate = uEndDate;

            //  vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
            {
                vrParameter.dwCheckStat = Parse(szCheckStat);

            }
            if (szCheckStat != null && szCheckStat == "4")
            {
                vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL;
                vrParameter.dwCheckStat = null;
            }
            YARDRESV[] vrResult;
           // GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (m_Request.Reserve.GetYardResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("预约号,申请人姓名,资源名称,状态,提交时间,申请时间,时段,位置,申请说明");
              
                uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                for (int i = 0; i < vrResult.Length; i++)
                {
                       System.Text.StringBuilder sbText = new System.Text.StringBuilder();


                    uint uState = (uint)vrResult[i].dwStatus;
                    sbText = AppendCSVFields(sbText, vrResult[i].dwResvID.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szApplicantName.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szDevName);

                    if (((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0) && (((uState & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0)))
                    {
                        vrResult[i].dwStatus = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
                    }
                    sbText = AppendCSVFields(sbText, GetJustName((vrResult[i].dwStatus), "Reserve_Status"));
                    sbText = AppendCSVFields(sbText, Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm"));
                    sbText = AppendCSVFields(sbText, Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm"));
                    string szOp = "";
                    uint nHour = Parse(Get1970Date((uint)vrResult[i].dwBeginTime, "HHmm"));
                    string szHour="";
                    if (nHour <1300)
                    {
                        szHour = "上午";
                    }
                    else if (nHour >= 1300 && nHour <1800)
                    {
                        szHour = "下午";
                    }
                    else if (nHour>=1800)
                    {
                        szHour = "晚上";
                    }
                    sbText = AppendCSVFields(sbText, szHour);
                 
                    if ((!(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)) || !(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))))
                    {
                        szOp = "'OPTD OPTD" + uState + "'";
                    }
                    else
                    {
                        szOp = "";
                    }
                    sbText = AppendCSVFields(sbText, vrResult[i].szBuildingName);
                    string szDetail = vrResult[i].szMemo;
                    if (szDetail.Length > 10)
                    {
                        szDetail = szDetail.Substring(0, 10) + "...";
                    }
                    sbText = AppendCSVFields(sbText, vrResult[i].szMemo);
                    //去掉尾部的逗号
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("预约信息.csv");
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
    private YARDRESV[] GetYardResvByGroupID(uint uGroupID)
    {
        YARDRESVREQ vrGet = new YARDRESVREQ();
        vrGet.dwResvGroupID = uGroupID;
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;

    }
    private bool GetCheckType(uint uID, out CHECKTYPE setValue)
    {
        setValue = new CHECKTYPE();
        CHECKTYPEREQ vrGet = new CHECKTYPEREQ();
        vrGet.dwCheckKind = uID;
        CHECKTYPE[] vtCheck;
        if (m_Request.Admin.CheckTypeGet(vrGet, out vtCheck) == REQUESTCODE.EXECUTE_SUCCESS && vtCheck != null && vtCheck.Length > 0)
        {
            setValue = vtCheck[0];
            return true;
        }
        return false;
    }
}