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
            string szcourseid = Request["courseid"];
            string szOP = Request["op"];
            TESTPLANREQ vrParameter = new TESTPLANREQ();
            string szYearTerm = Request["dwYearTerm"];
            uint uYeartermNow = Parse(szYearTerm);

            UNITESTPLAN[] vrResult;
            GetHTTPObj(out vrParameter);
            vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
            string szLogonName = Request["pid"];
            if (szLogonName != null && szLogonName != "")
            {
                UNIACCOUNT accnoInfo;
                if (GetAccByAccno(szLogonName, out accnoInfo))
                {
                    vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
                    vrParameter.szGetKey = accnoInfo.dwAccNo.ToString();
                    PutMemberValue("pid", szLogonName);
                    PutMemberValue("pidHidden", szLogonName);
                }
            }
            if (szcourseid != null && szcourseid != "")
            {
                vrParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYCOURSEID;
                vrParameter.szGetKey = szcourseid;
                string szCourseName = Request["courseName"];
                PutMemberValue("courseid", szcourseid);
                PutMemberValue("courseName", szCourseName);
            }
            vrParameter.dwYearTerm = uYeartermNow;
            vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
            if (m_Request.Reserve.GetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("计划名称,学期,教师,班级,课程,项目名称,上课时间,上课房间");
               
                UNIACCOUNT accnoTemp;
                for (int i = 0; i < vrResult.Length; i++)
                {
                    TESTITEMREQ testitemreq = new TESTITEMREQ();
                    testitemreq.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
                    testitemreq.szGetKey = vrResult[i].dwTestPlanID.ToString();
                    UNITESTITEM[] testitemRes;
                    if (m_Request.Reserve.GetTestItem(testitemreq, out testitemRes) == REQUESTCODE.EXECUTE_SUCCESS && testitemRes != null && testitemRes.Length > 0)
                    {
                        for (int j = 0; j < testitemRes.Length; j++)
                        {
                            string szTestName = testitemRes[j].szTestName;
                          
                            UNIRESERVE[] resvInfo=testitemRes[j].ResvInfo;
                            if (resvInfo != null && resvInfo.Length > 0)
                            {
                                for (int k = 0; k < resvInfo.Length; k++)
                                {
                                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                                    sbText = AppendCSVFields(sbText, vrResult[i].szTestPlanName);
                                    sbText = AppendCSVFields(sbText, GetTermText(vrResult[i].dwYearTerm));
                                    string szTecahcname = "";
                                    if (GetAccByAccno(vrResult[i].dwTeacherID.ToString(), out accnoTemp))
                                    {
                                        szTecahcname = vrResult[i].szTeacherName + "(" + accnoTemp.szLogonName + ")";
                                    }
                                    else
                                    {
                                        szTecahcname = vrResult[i].szTeacherName;
                                    }
                                    sbText = AppendCSVFields(sbText, szTecahcname);
                                    sbText = AppendCSVFields(sbText, vrResult[i].szGroupName.ToString());
                                    sbText = AppendCSVFields(sbText, vrResult[i].szCourseName.ToString());

                                    sbText = AppendCSVFields(sbText, szTestName);
                                    string szResvTime =GetTeachingTime((uint)resvInfo[k].dwTeachingTime);
                                    sbText = AppendCSVFields(sbText, szResvTime);

                                    string szRoomInfo = "";
                                    RESVDEV[] resvdevInfo = resvInfo[k].ResvDev;
                                    if (resvInfo != null && resvInfo.Length > 0)
                                    {
                                        for (int w = 0; w < resvdevInfo.Length; w++)
                                        {
                                            szRoomInfo += resvdevInfo[w].szRoomName + ";";
                                        }
                                    }
                                    sbText = AppendCSVFields(sbText, szRoomInfo);
                                    sbText.Remove(sbText.Length - 1, 1);
                                    swCSV.WriteLine(sbText.ToString());
                                }
                            }
                            else {
                                System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                                sbText = AppendCSVFields(sbText, vrResult[i].szTestPlanName);
                                sbText = AppendCSVFields(sbText, GetTermText(vrResult[i].dwYearTerm));
                                string szTecahcname = "";
                                if (GetAccByAccno(vrResult[i].dwTeacherID.ToString(), out accnoTemp))
                                {
                                    szTecahcname = vrResult[i].szTeacherName + "(" + accnoTemp.szLogonName + ")";
                                }
                                else
                                {
                                    szTecahcname = vrResult[i].szTeacherName;
                                }
                                sbText = AppendCSVFields(sbText, szTecahcname);
                                sbText = AppendCSVFields(sbText, vrResult[i].szGroupName.ToString());
                                sbText = AppendCSVFields(sbText, vrResult[i].szCourseName.ToString());

                                sbText = AppendCSVFields(sbText, szTestName);
                                sbText = AppendCSVFields(sbText, "");
                                sbText = AppendCSVFields(sbText, "");
                            }
                        }
                    }
                    else
                    {
                        System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                        sbText = AppendCSVFields(sbText, vrResult[i].szTestPlanName);
                        sbText = AppendCSVFields(sbText, GetTermText(vrResult[i].dwYearTerm));
                        string szTecahcname = "";
                        if (GetAccByAccno(vrResult[i].dwTeacherID.ToString(), out accnoTemp))
                        {
                            szTecahcname = vrResult[i].szTeacherName + "(" + accnoTemp.szLogonName + ")";
                        }
                        else
                        {
                            szTecahcname = vrResult[i].szTeacherName;
                        }
                        sbText = AppendCSVFields(sbText, szTecahcname);
                        sbText = AppendCSVFields(sbText, vrResult[i].szGroupName.ToString());
                        sbText = AppendCSVFields(sbText, vrResult[i].szCourseName.ToString());

                        sbText = AppendCSVFields(sbText, "");
                        sbText = AppendCSVFields(sbText, "");
                        sbText = AppendCSVFields(sbText, "");

                        sbText.Remove(sbText.Length - 1, 1);

                        //写datatable的一行
                        swCSV.WriteLine(sbText.ToString());
                    }
                   
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("排课情况.csv");
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