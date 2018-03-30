﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using UniWebLib;
using System.Text;
using System.Reflection;

public struct IMPORTSTAT
{
    Reserved reserved;
    public uint dwStep; //步骤
    public string szTitle; //标题
    public string szMessage; //提示
    public string szFileName; //上传的文件名
    public string szFilePath; //上传的文件路径
    public string szTemplateFile; //模板文件
    public string szDestName; //目标结构名
    public string szDestFieldList; //目标结构字段序列

    public uint dwTotalLine;
    public uint dwImported;
    public uint dwFailed;
    public string szErrLines;
    public string szErrListFile;
}
public partial class _Default : UniPage
{
    protected IMPORTSTAT pagedata;
    ArrayList ErrorLines;
    protected string m_szOut;

    protected string m_Title = "导入";
    protected string m_szTerm = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //注意：需要在App_Code/UniPage.cs文件中ImportProcess函数里实现导入功能

        TERMREQ vrGetTerm = new TERMREQ();
        UNITERM[] vtTerm;
        if (m_Request.Reserve.GetTerm(vrGetTerm, out vtTerm) == REQUESTCODE.EXECUTE_SUCCESS && vtTerm != null && vtTerm.Length > 0)
        {
            for (int i = 0; i < vtTerm.Length; i++)
            {
                if (((uint)vtTerm[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    m_szTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwYearTerm.ToString(), true);
                }
                else
                {
                    m_szTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwYearTerm.ToString());
                }
            }
        }
        GetHTTPObj(out pagedata);

        Response.CacheControl = "no-cache";

        //使用HTTP输入参数:
        //pagedata.szTitle = "项目卡";
        //pagedata.szTemplateFile = MyVPath+"Upload/TestCard_Template.csv";
        //pagedata.szDestName = "TESTCARD";
        //pagedata.szDestFieldList = "szTestName,szCategoryName,dwGroupPeopleNum,dwTestHour,dwTestClass,dwTestKind,dwRequirement,szConstraints,szMemo";
        //<<
        /*
        if (string.IsNullOrEmpty(pagedata.szDestName) || string.IsNullOrEmpty(pagedata.szDestFieldList) || string.IsNullOrEmpty(pagedata.szTemplateFile) || string.IsNullOrEmpty(pagedata.szTitle))
        {
            pagedata.dwStep = 4;
            pagedata.szMessage = "参数不能为空";
        }
         * */
        if (Request["Submit"] == "true")
        {
            if (pagedata.dwStep == 0)
            {
                if (Request.Files.Count > 0)
                {
                    string szTempPath = MyVPath + "Upload/Import_" + DateTime.Now.Ticks + ".csv";
                    string szTempRawPath = Server.MapPath(szTempPath);
                    Request.Files[0].SaveAs(szTempRawPath);

                    pagedata.dwStep = 1;
                    pagedata.szFileName = Request.Files[0].FileName;
                    pagedata.szMessage = "已成功上传文件:" + pagedata.szFileName;
                    pagedata.szFilePath = szTempPath;
                }
                else
                {
                    pagedata.szMessage = "没获取到上传的文件";
                }
            }

            if (pagedata.dwStep == 3)
            {
                pagedata.dwStep = 4;
            }

            if (pagedata.dwStep == 2)
            {
                pagedata.dwStep = 3;
                pagedata.szMessage = "";
                uint nImported;
                uint nFailed;
                pagedata.szErrListFile = MyVPath + "Upload/ImportError_" + DateTime.Now.Ticks + ".csv";
                string szErrListRawFile = Server.MapPath(pagedata.szErrListFile);
                Import(pagedata.szFilePath, szErrListRawFile, out nImported, out nFailed);
                pagedata.dwTotalLine = nImported + nFailed;
                pagedata.dwImported = nImported;
                pagedata.dwFailed = nFailed;
                string szErrLines = "";
                for (int i = 0; i < ErrorLines.Count; i++)
                {
                    szErrLines += ErrorLines[i].ToString();
                    if (i < ErrorLines.Count - 1)
                    {
                        szErrLines += ",";
                    }
                }
                pagedata.szErrLines = szErrLines;
            }

            if (pagedata.dwStep == 1)
            {
                pagedata.dwStep = 2;
                pagedata.szMessage = "已成功上传文件:" + pagedata.szFileName;
                m_szOut = ReadCSV(pagedata.szFilePath, 10, out pagedata.dwTotalLine);
            }
        }
        else
        {
            m_szOut = ReadCSV(pagedata.szTemplateFile, 10, out pagedata.dwTotalLine);
        }

        PutJSObj(pagedata);
    }

    string ReadCSV(string szFilePath, int nMaxLine, out uint dwTotalLine)
    {
        string szOut = "";
        dwTotalLine = 0;
        try
        {
            using (CsvReader csv = new CsvReader(new StreamReader(Server.MapPath(szFilePath), Encoding.GetEncoding("gb2312")), true))
            {
                //字段数量
                int fieldCount = csv.FieldCount;
                //标题数组
                string[] headers = csv.GetFieldHeaders();

                szOut = "<table class='tblCSV'><thead><tr>";
                for (int i = 0; i < fieldCount; i++)
                {
                    szOut += "<th>" + headers[i] + "</th>";
                }
                szOut += "</tr></thead><tbody>";

                int n = 0;
                //只进的游标读取
                while (csv.ReadNextRecord())
                {
                    if (n++ <= nMaxLine)
                    {
                        //遍历列
                        szOut += "<tr>";
                        for (int i = 0; i < fieldCount; i++)
                        {
                            szOut += "<td>" + csv[i] + "</td>";
                        }
                        szOut += "</tr>";
                    }
                }
                dwTotalLine = (uint)n;
                szOut += "</tbody>";
                if (n > nMaxLine)
                {
                    szOut += "<tfoot><tr><td class='importTblMore' colspan='" + fieldCount + "'>总" + dwTotalLine + "条，只显示前" + nMaxLine + "条</td></tr></tfoot>";
                }
                szOut += "</table>";
                if (dwTotalLine == 0)
                {
                    szOut = "";
                }
            }
        }
        catch (Exception)
        {
            dwTotalLine = 0;
            szOut = "";
        }
        return szOut;
    }

    void Import(string szFilePath, string szErrListRawFile, out uint nImported, out uint nFailed)
    {
        ErrorLines = new ArrayList();
        nImported = 0;
        nFailed = 0;
        string strline;

        StreamWriter tWrite = new StreamWriter(szErrListRawFile, false, Encoding.GetEncoding("gb2312"));
        System.IO.StreamReader mysr = new StreamReader(Server.MapPath(szFilePath), Encoding.GetEncoding("gb2312"));
        while ((strline = mysr.ReadLine()) != null)
        {
            uint uTermID = Parse(Request["dwYearTerm"]);
            string[] szList = strline.Split(',');
            string szCourName = szList[0];
            string szClassName  =(szList[1]);
            string szTeachLogonName = (szList[2]);
            UNIACCOUNT accinfoTeachar=new UNIACCOUNT();
            if (GetAccByLogonName(szTeachLogonName, out accinfoTeachar))
            {
                UNITESTPLAN setTestPlan = new UNITESTPLAN();
                COURSEREQ courseGet = new COURSEREQ();
                courseGet.szCourseName = szCourName;
                UNICOURSE[] courseRes;
                UNICOURSE setCourse = new UNICOURSE();

                if (m_Request.Reserve.GetCourse(courseGet, out courseRes) == REQUESTCODE.EXECUTE_SUCCESS && courseRes != null && courseRes.Length > 0)
                {
                    setCourse = new UNICOURSE();
                    setCourse = courseRes[0];
                }
                else
                {
                    setCourse.szCourseName = szCourName;
                    setCourse.dwTestHour = 100;
                    setCourse.szCourseCode = GetDevSN().ToString();
                    setCourse.dwCourseProperty = (uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_NOTHEORY;
                    if (m_Request.Reserve.SetCourse(setCourse, out setCourse) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        nFailed = nFailed + 1;
                        continue;
                    }
                    //新建课程
                }
                UNIGROUP setGroup = new UNIGROUP();
                UNIGROUP[] setGroupList = GetGroupByName(szClassName);
                if (setGroupList != null && setGroupList.Length > 0)
                {
                    setGroup = new UNIGROUP();
                    setGroup = setGroupList[0];
                }
                else
                {
                    //新建班级
                    UNITERM[] vtTerm= GetTermByID(uTermID);
                    if (vtTerm != null && vtTerm.Length > 0)
                    {
                        NewGroup(szClassName, (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out setGroup, (uint)vtTerm[0].dwEndDate);
                    }
                    else
                    {
                        nFailed = nFailed + 1;
                        continue;
                    }
                }

                {//获取testplan是否已经新建
                    TESTPLANREQ testPlanReq = new TESTPLANREQ();
                    testPlanReq.dwYearTerm = uTermID;
                    testPlanReq.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYNAME;
                    testPlanReq.szGetKey = accinfoTeachar.szTrueName + "_" + szCourName;
                    UNITESTPLAN[] vtTestPlan;
                    if (m_Request.Reserve.GetTestPlan(testPlanReq, out vtTestPlan) == REQUESTCODE.EXECUTE_SUCCESS && vtTestPlan != null && vtTestPlan.Length > 0)
                    {
                        nFailed = nFailed + 1;
                        continue;
                    }
                }

                setTestPlan.szTestPlanName = accinfoTeachar.szTrueName + "_" + szCourName;
                setTestPlan.dwGroupID = setGroup.dwGroupID;
                setTestPlan.dwCourseID = setCourse.dwCourseID;
                setTestPlan.dwTeacherID = accinfoTeachar.dwAccNo;
                setTestPlan.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
                setTestPlan.dwTestHour = setCourse.dwTestHour;
                setTestPlan.dwYearTerm = uTermID;
                REQUESTCODE uResponse = m_Request.Reserve.SetTestPlan(setTestPlan, out setTestPlan);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    nImported = nImported + 1;
                    //新建默认的一个实验项目
                    uint uNewItem = 1;
                    if (uNewItem == 1)
                    {
                        TESTCARD newCard = new TESTCARD();
                        newCard.dwGroupPeopleNum = 1;
                        newCard.szTestName = setTestPlan.szTestPlanName;
                        newCard.dwTestHour = setTestPlan.dwTestHour;
                        if (m_Request.Reserve.SetTestCard(newCard, out newCard) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            UNITESTITEM testItem = new UNITESTITEM();
                            testItem.dwTestPlanID = setTestPlan.dwTestPlanID;
                            testItem.dwTestCardID = newCard.dwTestCardID;
                            testItem.szTestName = setTestPlan.szTestPlanName;
                            testItem.dwMaxResvTimes = 100;
                            m_Request.Reserve.SetTestItem(testItem, out testItem);
                        }

                    }
                }
                else
                {
                    nFailed = nFailed + 1;
                }
            }
         

        }

        tWrite.Close();
    }
}