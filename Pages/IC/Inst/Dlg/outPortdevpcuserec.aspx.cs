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
            USERECREQ vrParameter = new USERECREQ();
            string szPID = Request["dwPID"];
            DEVUSEREC[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);

            vrParameter.dwStartDate = DateToUint(Request["startdate"]);
            vrParameter.dwEndDate = DateToUint(Request["enddate"]);
            string szKey = Request["szGetKey"];
            if (szKey != null && szKey != "")
            {
                vrParameter.dwDevID = Parse(szKey);
            }
            if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
            {
                vrParameter.szReqExtInfo.szOrderKey = "dwBeginTime";
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
            vrParameter.dwClassKind = ((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
            UNIACCOUNT account = new UNIACCOUNT();
            if (szPID != "" && szPID != null && GetAccByLogonName(szPID, out account))
            {
                vrParameter.dwAccNo = account.dwAccNo;
            }
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            vrParameter.szReqExtInfo.dwNeedLines = 100000;
            vrParameter.szReqExtInfo.dwStartLine = 0;

            if (m_Request.Report.GetUseRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
               
                    System.IO.StringWriter swCSV = new System.IO.StringWriter();
                    swCSV.WriteLine("设备名称,类型名称,型号,规格,所属区域,使用人,部门,使用时长");
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                        string szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szDevName.ToString() + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szKindName + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szModel.ToString() + "\"";
                        sbText = AppendCSVFields(sbText, szValue);
                        szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szSpecification.ToString() + "\"";
                        sbText = AppendCSVFields(sbText, szValue);
                        szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szLabName.ToString() + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        szValue = "\"" + ((char)(9)).ToString() + (vrResult[i].szDeptName) + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        szValue = "\"" + ((char)(9)).ToString() + Get1970Date(vrResult[i].dwBeginTime, "MM-dd HH:mm") + "-" + Get1970Date(vrResult[i].dwEndTime, "MM-dd HH:mm") + "\"";
                        sbText = AppendCSVFields(sbText, szValue);


                        szValue = "\"" + ((char)(9)).ToString() + GetMinToStr(vrResult[i].dwUseTime) + "\"";
                        sbText = AppendCSVFields(sbText, szValue);

                        sbText.Remove(sbText.Length - 1, 1);

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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("电子阅览室使用记录.csv");
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