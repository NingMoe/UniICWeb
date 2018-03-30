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
             DOORCARDRECREQ vrParameter = new DOORCARDRECREQ();
        
        string szPID = Request["dwPID"];
        if (szPID != null && szPID != "")
        {
            UNIACCOUNT accno;
            if(GetAccByLogonName(szPID,out accno))
            {
                vrParameter.dwAccNo = accno.dwAccNo;
            }
        }
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
            vrParameter.dwGetType = (uint)DOORCARDRECREQ.DWGETTYPE.DOORCARDRECGET_BYROOMID;
            vrParameter.szGetKey =(szKey);
        }
        vrParameter.dwStartTime = Get1970Seconds(Server.UrlDecode(Request["startdate"])); //DateToUint(Request["startdate"]);
        vrParameter.dwEndTime= Get1970Seconds(Server.UrlDecode(Request["enddate"])); //DateToUint(Request["enddate"]);
        vrParameter.dwCardMode = ((uint)DOORCARDREQ.DWCARDMODE.DOORCARD_IN);
        DOORCARDREC[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.szReqExtInfo.dwNeedLines = 100000;
        vrParameter.szReqExtInfo.dwStartLine = 0;
       if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwCardTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        
        if (m_Request.Report.GetDoorCardRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("编号,姓名（学工号）,部门,空间名称,刷卡时间,说明");
                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    string szValue = "\"" + ((char)(9)).ToString() + vrResult[i].dwSID.ToString() + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szDeptName.ToString() + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szRoomName.ToString() + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + Get1970Date((uint)vrResult[i].dwCardTime) + "\"";
                    sbText = AppendCSVFields(sbText, szValue);

                    szValue = "\"" + ((char)(9)).ToString() + vrResult[i].szMemo + "\"";
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("刷卡记录.csv");
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