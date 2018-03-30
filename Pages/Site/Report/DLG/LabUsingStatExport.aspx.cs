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

            REPORTREQ vrParameter = new REPORTREQ();
            LABSTAT[] vrResult;
            vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
            vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);
            vrParameter.dwPurpose = ((uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH + (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING);
            if (m_Request.Report.GetLabStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine(ConfigConst.GCLabName + "编号," + ConfigConst.GCLabName + "名称,使用人数,数目,使用次数,使用总时间");     
                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                    sbText = AppendCSVFields(sbText, vrResult[i].szLabSN.ToString());                  
                    sbText = AppendCSVFields(sbText, vrResult[i].szLabName.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwPIDNum.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwTotalNum.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwUseTimes.ToString());
                     uint uUseTime = (uint)vrResult[i].dwTotalUseTime;
                    sbText = AppendCSVFields(sbText, uUseTime / 60 + "小时" + uUseTime % 60 + "分钟");
                  
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode(ConfigConst.GCLabName+"使用率统计.csv");
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