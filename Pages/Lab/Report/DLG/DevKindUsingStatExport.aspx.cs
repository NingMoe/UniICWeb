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
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {

            REPORTREQ vrParameter = new REPORTREQ();
            DEVKINDSTAT[] vrResult;
            string dwStartDate = Request["dwStartDate"];
            string dwEndDate = Request["dwEndDate"];
            vrParameter.dwStartDate = DateToUint(dwStartDate);
            vrParameter.dwEndDate = DateToUint(dwEndDate);

            vrParameter.dwPurpose = ((uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH + (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING);
            if (m_Request.Report.GetDevKindStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {

                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine(ConfigConst.GCKindName + "名称," + "个数,使用人数,使用人次数,使用总时间");
                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                    sbText = AppendCSVFields(sbText, vrResult[i].szKindName.ToString());

                    sbText = AppendCSVFields(sbText, vrResult[i].dwTotalNum.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwPIDNum.ToString());

                    sbText = AppendCSVFields(sbText, vrResult[i].dwUseTimes.ToString());
                    sbText = AppendCSVFields(sbText, GetMinToStr(vrResult[i].dwTotalUseTime));

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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode(ConfigConst.GCKindName+"使用率统计.csv");
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