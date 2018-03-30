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
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTUSEDETAILREQ vrParameter = new RTUSEDETAILREQ();

        RTUSEDETAIL[] vrResult;    
        vrParameter.dwDevID = DateToUint(Request["dwDevID"]);
        vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
        vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);
        uResponse = m_Request.Report.GetRTUseDetail(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {           
            System.IO.StringWriter swCSV = new System.IO.StringWriter();
            swCSV.WriteLine(" 时间,使用人,"+ConfigConst.GCTutorName+",总金额,分析测试费,开放基金,劳务费");
            for (int i = 0; i < vrResult.Length; i++)
            {
                System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                sbText = AppendCSVFields(sbText, Get1970Date(vrResult[i].dwBeginTime, "yyyy-MM-dd"));
                sbText = AppendCSVFields(sbText, vrResult[i].szOwnerName);
                sbText = AppendCSVFields(sbText, vrResult[i].szHolderName.ToString());
                sbText = AppendCSVFields(sbText, vrResult[i].dwRealCost.ToString());                              
                sbText = AppendCSVFields(sbText, vrResult[i].dwDevUseFee.ToString()); //协助             
                sbText = AppendCSVFields(sbText, vrResult[i].dwSampleFee.ToString()); ////使用
                sbText = AppendCSVFields(sbText, vrResult[i].dwEntrustFee.ToString()); //样品费             
                //sbText = AppendCSVFields(sbText, vrResult[i]..ToString()); //代建费        

                sbText.Remove(sbText.Length - 1, 1);

                //写datatable的一行
                swCSV.WriteLine(sbText.ToString());
            }
            DownloadFile(Response, swCSV.GetStringBuilder(), "devUseFeeDetail.csv");
            swCSV.Close();
            Response.End();
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
                strResHeader = "inline; filename=" + strFileName;
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