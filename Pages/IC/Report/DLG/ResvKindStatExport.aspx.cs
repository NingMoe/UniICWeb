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
                RESVKINDSTATREQ vrParameter = new RESVKINDSTATREQ();
                uint uKind = Parse(Request["Kind"]);
                CODINGTABLE[] vtCode=null;
                if (uKind == 1)
                {
                    vtCode = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND);
                    vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
                }
                else if(uKind==2)
                {
                    vtCode = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_ACTIVITYKIND);
                    vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
                }
                vrParameter.szReqExtInfo.dwNeedLines = 1000000;
                vrParameter.szReqExtInfo.dwStartLine = 0;
                RESVKINDSTAT[] vrResult;
                vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
                vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);

                if (m_Request.Report.GetResvKindStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    System.IO.StringWriter swCSV = new System.IO.StringWriter();
                    swCSV.WriteLine("类型,预约次数,预约总时间");
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                        sbText = AppendCSVFields(sbText, GetCode(vtCode, (uint)vrResult[i].dwKind));
                        sbText = AppendCSVFields(sbText, vrResult[i].dwResvTimes.ToString());

                        sbText = AppendCSVFields(sbText, vrResult[i].dwResvMinutes.ToString());
                     
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
    protected string GetCode(CODINGTABLE[] vtRes, uint uKind)
    {
        string szRes = "";
        if (vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (Parse(vtRes[i].szCodeSN.ToString()) == uKind)
                {
                    return vtRes[i].szCodeName.ToString();
                }
            }
        }
        return szRes;
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("类型统计.csv");
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