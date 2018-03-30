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
                USERSTAT[] vrResult;
                vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
                vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);
                string szKey = Request["orderkey"];
                if (szKey == null)
                {
                    vrParameter.szReqExtInfo.szOrderKey = "dwUseTime";
                    vrParameter.szReqExtInfo.szOrderMode = "desc";
                }
                else
                {
                    vrParameter.szReqExtInfo.szOrderKey = szKey;
                    vrParameter.szReqExtInfo.szOrderMode = "desc";
                }

                if (m_Request.Report.GetUserStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    System.IO.StringWriter swCSV = new System.IO.StringWriter();
                    swCSV.WriteLine("学工号,姓名,班级,学院,使用次数,使用总时间");
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                        string szClassName = "";
                        sbText = AppendCSVFields(sbText, vrResult[i].szPID.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].szTrueName.ToString());
                        UNIACCOUNT accinfo;
                        if (GetAccByAccno(vrResult[i].dwAccNo.ToString(), out accinfo))
                        {
                            szClassName = accinfo.szClassName.ToString();
                        }
                       
                        sbText = AppendCSVFields(sbText, szClassName.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].szDeptName.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].dwUseTimes.ToString());
                        uint uUseTime = (uint)vrResult[i].dwUseTime;
                        sbText = AppendCSVFields(sbText, uUseTime / 60 + "小时" + uUseTime % 60 + "分钟");
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("个人使用排行榜.csv");
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