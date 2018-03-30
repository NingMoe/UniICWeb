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
            ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
            UNIACTIVITYPLAN[] vrResult;
            vrParameter.szReqExtInfo.dwStartLine = 1;
            vrParameter.szReqExtInfo.dwNeedLines = 100000;
            string szDate = Request["vDate"];
            uint uDate = (GetDate(szDate));
            uint dwStatue1 = Parse(Request["dwStatue1"]);
            uint dwStatue2 = Parse(Request["dwStatue2"]);
            if (uDate!=0)
            {
                vrParameter.dwStartDate = uDate;
            }
            uint uState = Parse(Request["dwStatue1"]) + Parse(Request["dwStatue2"]);
            if (uState != 0)
            {
                vrParameter.dwStatus = uState;
            }
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            vrParameter.szReqExtInfo.dwNeedLines = 100000;
            vrParameter.szReqExtInfo.szOrderKey = "dwActivityDate";
            vrParameter.szReqExtInfo.szOrderMode = "asc";
            if (m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("活动名称,空间名称,主讲人,联系人,电话,手机,活动人数,使用人数,活动时间,状态,主办单位");
                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                    UNIDEVICE devset;
                    string szDevName = "";
                    if (getDevByID(vrResult[i].dwDevID.ToString(), out devset))
                    {
                        szDevName = devset.szDevName;
                    }
                    string activityplan = vrResult[i].szActivityPlanName;
                   
                    UNIRESERVE setResv;
                    if (GetResvByID(vrResult[i].dwResvID.ToString(), out setResv))
                    {

                    }
                    string szValue = "\"" + ((char)(9)).ToString() + activityplan + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + szDevName + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szPresenter.ToString() + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szContact.ToString() + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szTel + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szHandPhone + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].dwEnrollUsers + "\"";
                    sbText = AppendCSVFields(sbText, szValue);
                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].dwRealUsers + "\"";

                    sbText = AppendCSVFields(sbText, szValue);

                    string szDateTime = GetDateStr(vrResult[i].dwActivityDate) + " " + GetTimeStr(vrResult[i].dwBeginTime) + "至" + GetTimeStr(vrResult[i].dwEndTime);
                    szValue = "\"" + ((char)(9)).ToString() + szDateTime + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    uint uStatue = (uint)vrResult[i].dwStatus;
                    if ((uStatue & 2) > 0 && (uStatue & 4) > 0)
                    {
                        vrResult[i].dwStatus = uStatue - 4;
                    }
                    if (uStatue == 3)
                    {
                        vrResult[i].dwStatus = 4;
                    }
                    szValue = "\"" + ((char)(9)).ToString() + GetJustName(vrResult[i].dwStatus, "activity_status") + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szHostUnit + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());



                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "ativityplan.csv");
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("活动安排.csv");
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