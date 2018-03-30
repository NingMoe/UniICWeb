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
            {
                REPORTREQ vrParameter = new REPORTREQ();
                vrParameter.szReqExtInfo.dwNeedLines = 1000000;
                vrParameter.szReqExtInfo.dwStartLine = 0;
                DEPTSTAT[] vrResult;
                vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
                vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);

                if (m_Request.Report.GetDeptStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    System.IO.StringWriter swCSV = new System.IO.StringWriter();
                    swCSV.WriteLine("名称,学院,使用人数,使用人次,使用总时间,平均在馆时间");
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                        sbText = AppendCSVFields(sbText, vrResult[i].szDeptName.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].dwTotalUsers.ToString());

                        sbText = AppendCSVFields(sbText, vrResult[i].dwPIDNum.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].dwUseTimes.ToString());
                        uint uUseTime = (uint)vrResult[i].dwTotalUseTime;
                        sbText = AppendCSVFields(sbText,(uUseTime / 60 + "小时" + uUseTime % 60 + "分钟"));
                        sbText = AppendCSVFields(sbText, ((uUseTime / ((uint)vrResult[i].dwTotalUsers + 1.0)).ToString(".00")));
                         
                        //去掉尾部的逗号
                        sbText.Remove(sbText.Length - 1, 1);

                        //写datatable的一行
                        swCSV.WriteLine(sbText.ToString());
                    }
                    DownloadFile(Response, swCSV.GetStringBuilder(), "RuleDaySum.csv");
                    swCSV.Close();
                    Response.End();
                }
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("部门统计.csv");
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