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
           
            TESTITEMSTATREQ vrParameter = new TESTITEMSTATREQ();
            TESTITEMSTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            GetHTTPObj(out vrParameter);
            vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
            vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);
                if (m_Request.Report.GetTestItemStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {

                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("课程编号,课程名称,课程属性,实验项目名称,每组人数,学时,教师,班级,班级人数,实验室,设备数");

                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                    
                    sbText = AppendCSVFields(sbText, vrResult[i].szCourseCode.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szCourseName.ToString());
                    sbText = AppendCSVFields(sbText, GetJustName(vrResult[i].dwCourseProperty, "Course_Property", false));
                    sbText = AppendCSVFields(sbText, vrResult[i].szTestName);
                    sbText = AppendCSVFields(sbText, vrResult[i].dwGroupPeopleNum.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwTestHour.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szTeacherName.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szGroupName.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwGroupUsers.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].szLabName.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwDevNum.ToString());

                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());

                }
            

                DownloadFile(Response, swCSV.GetStringBuilder(), "teachplan.csv");
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("实验室课程项目统计.csv");
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