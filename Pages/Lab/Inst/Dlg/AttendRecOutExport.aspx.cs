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
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            ATTENDRECREQ vrParameter = new ATTENDRECREQ();

            GetHTTPObj(out vrParameter);
            if (vrParameter.dwStartDate == 0)
            {
                vrParameter.dwStartDate = null;
            }
            if (vrParameter.dwEndDate == 0)
            {
                vrParameter.dwEndDate = null;
            }
            //  if(vrParameter.dwStartDate=)
            uint uAttend = Parse(Request["attendid"]);
            if (uAttend != 0)
            {
                vrParameter.dwAttendID = uAttend;
            }

            ATTENDREC[] vrResult;
            uResponse = m_Request.Attendance.GetAttendRec(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("学工号,姓名,考勤规则,出勤日期,考勤房间,进入时间,退出时间,最近一次进入时间,停留时间(分钟),刷卡次数,状态");
                for (int i = 0; i < vrResult.Length; i++)
                {

                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    sbText = AppendCSVFields(sbText, vrResult[i].szPID);
                    sbText = AppendCSVFields(sbText, vrResult[i].szTrueName);
                    sbText = AppendCSVFields(sbText, vrResult[i].szAttendName.ToString());
                    sbText = AppendCSVFields(sbText, GetDateStr(vrResult[i].dwAttendDate));
                    sbText = AppendCSVFields(sbText, (vrResult[i].szRoomName));            
                    sbText = AppendCSVFields(sbText, Get1970Date(vrResult[i].dwInTime)); 
                    sbText = AppendCSVFields(sbText, Get1970Date(vrResult[i].dwOutTime));              
                    sbText = AppendCSVFields(sbText, Get1970Date(vrResult[i].dwLatestInTime));         
                    sbText = AppendCSVFields(sbText, (vrResult[i].dwStayMin).ToString()); 
                    sbText = AppendCSVFields(sbText, (vrResult[i].dwCardTimes).ToString());  
                    sbText = AppendCSVFields(sbText, GetJustName(vrResult[i].dwAttendStat, "attendstatus"));     
                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());
                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "attendrec.csv");
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