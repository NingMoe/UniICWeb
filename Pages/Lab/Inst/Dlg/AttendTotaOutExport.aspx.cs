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
            ATTENDSTATREQ vrParameter = new ATTENDSTATREQ();
            GetHTTPObj(out vrParameter);
            if (Parse(Request["attendid"]) != 0)
            {
                vrParameter.dwAttendID = Parse(Request["attendid"]);
            }
            ATTENDSTAT[] vrResult;
            uResponse = m_Request.Attendance.GetAttendStat(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("学工号,姓名,需考勤次数,出勤次数,缺勤次数,迟到次数,早退次数,迟到加早退,使用时间不达标次数,离开未刷卡次数,出勤总时间");
                for (int i = 0; i < vrResult.Length; i++)
                {

                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    sbText = AppendCSVFields(sbText, vrResult[i].szPID);
                    sbText = AppendCSVFields(sbText, vrResult[i].szTrueName);
                    sbText = AppendCSVFields(sbText, vrResult[i].dwTotalTimes.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwAttendTimes.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwAbsentTimes.ToString()); //协助             
                    sbText = AppendCSVFields(sbText, vrResult[i].dwLateTimes.ToString()); ////使用
                    sbText = AppendCSVFields(sbText, vrResult[i].dwLeaveTimes.ToString()); //样品费             
                    sbText = AppendCSVFields(sbText, vrResult[i].dwLLTimes.ToString()); //代建费        
                    sbText = AppendCSVFields(sbText, vrResult[i].dwUseLessTimes.ToString()); //代建费     
                    sbText = AppendCSVFields(sbText, vrResult[i].dwLeaveNoCardTimes.ToString()); //代建费     
                    sbText = AppendCSVFields(sbText, vrResult[i].dwTotalMin.ToString()); //代建费     
                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());
                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "attendtotal.csv");
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