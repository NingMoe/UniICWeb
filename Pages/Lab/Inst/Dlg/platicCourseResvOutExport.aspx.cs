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
            TEACHINGRESVRECREQ vrParameter = new TEACHINGRESVRECREQ();
            GetHTTPObj(out vrParameter);
            vrParameter.dwStartDate = DateToUint(Request["dwStartDate"]);
            vrParameter.dwEndDate = DateToUint(Request["dwEndDate"]);
            TEACHINGRESVREC[] vrResult;
           // GetPageCtrlValue(out vrParameter.szReqExtInfo);
            System.IO.StringWriter swCSV = new System.IO.StringWriter();
            swCSV.WriteLine("课程名,教师,房间,班级,应到人数,实到人数,上课时间");
            if (m_Request.Report.GetTeachingResvRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();

                    sbText = AppendCSVFields(sbText, vrResult[i].szCourseName);
                    sbText = AppendCSVFields(sbText, vrResult[i].szTeacherName);
                    sbText = AppendCSVFields(sbText, vrResult[i].szLabName);
                    sbText = AppendCSVFields(sbText, vrResult[i].szGroupName);
                    sbText = AppendCSVFields(sbText, vrResult[i].dwGroupUsers.ToString());

                    USERSPERMINUTE[] vtUserPerMinte = vrResult[i].UsersPerMinute;

                    uint uTotal = (uint)vrResult[i].dwGroupUsers;
                    bool bIs = false;
                    string szFloatRate = "";
                    string szFTemMin = "";
                    string szLessTemPeople = "";
                    if (vtUserPerMinte.Length > 10)
                    {
                        uint u = 0;
                        uint.TryParse(vtUserPerMinte[9].dwUsers.ToString(), out u);
                        vrResult[i].dwAttendUsers = u;
                        if (uTotal == 0)
                        {
                            uTotal = 1;
                        }
                        szFTemMin = (((uint)(u) / (uTotal * 1.0)) * 100).ToString(".0");
                    }
                    int k = 0;
                    bool bIsEnd = true;
                    for (k = (vtUserPerMinte.Length - 1); k > 0; k--)
                    {
                        if (bIsEnd && vtUserPerMinte[k].dwUsers != null && ((uint)vtUserPerMinte[k].dwUsers < 10))
                        {


                        }
                        else
                        {
                            k = k + 1;
                            bIsEnd = false;
                            break;
                        }
                    }
                    if (k == 0)
                    {
                        szLessTemPeople = "0";
                    }
                    else
                    {
                        szLessTemPeople = (vtUserPerMinte.Length - k).ToString();
                    }
                    vrResult[i].szCourseCode = "";
                    string szInfo = "";
                    if (!bIs)
                    {
                        szInfo = ("上课后10分钟到课率" + szFTemMin + "%;<br />下课前" + szLessTemPeople + "分钟人数少于10");
                    }

                    sbText = AppendCSVFields(sbText, vrResult[i].dwAttendUsers.ToString());
                    sbText = AppendCSVFields(sbText, GetTeachingTime((uint)vrResult[i].dwTeachingTime));
                    sbText = AppendCSVFields(sbText, GetTeachingTime((uint)vrResult[i].dwTeachingTime));
                    sbText = AppendCSVFields(sbText, (szFTemMin + "%")); 
                    //去掉尾部的逗号
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("课程考勤情况.csv");
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