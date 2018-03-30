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
    protected void Page_Load(object sender, EventArgs e)
    {
        //注意：需要在App_Code/UniPage.cs文件中ImportProcess函数里实现导入功能

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
            string[] szList = strline.Split(',');
            if (szList.Length < 8)
            {
                continue;
            }
            string szSLogonName = szList[0];
            string szSTrueName = szList[1];
            string szSHandPhone = szList[2];
            string szSEmail = szList[3];

            string szTLogonName = szList[4];
            string szTTrueName = szList[5];
            string szTHandPhone = szList[6];
            string szTEmail = szList[7];

            UNIACCOUNT studentAcc = new UNIACCOUNT();
            UNIACCOUNT teacherAcc = new UNIACCOUNT();
            if (GetAccByLogonName(szSLogonName.Trim(), out studentAcc) && GetAccByLogonName(szTLogonName.Trim(), out teacherAcc))
            {
                int uAuto = ConfigConst.GCTurtorReacher;
                if (uAuto == 1)
                {
                    TUTORREQ tutorReq = new TUTORREQ();
                    tutorReq.dwTutorID = teacherAcc.dwAccNo;
                    UNITUTOR[] vtTutor;
                    if (m_Request.Account.TutorGet(tutorReq, out vtTutor) == REQUESTCODE.EXECUTE_SUCCESS && vtTutor != null && vtTutor.Length > 0)
                    {

                    }
                    else
                    {
                        EXTIDENTACC setTutor = new EXTIDENTACC();
                        setTutor.dwAccNo = teacherAcc.dwAccNo;
                        setTutor.szTrueName = teacherAcc.szTrueName;
                        RESEARCHTEST setResarch = new RESEARCHTEST();
                        setResarch.szRTName = teacherAcc.szTrueName;
                        setResarch.dwRTKind = (uint)RESEARCHTEST.DWRTKIND.RTKIND_RTASK;
                        UNIGROUP setGroup = new UNIGROUP();
                        setGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
                        setGroup.szName = teacherAcc.szTrueName +ConfigConst.GCTutorName+ "组";

                        if (m_Request.Group.SetGroup(setGroup, out setGroup) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            setResarch.dwGroupID = setGroup.dwGroupID;
                        }
                        setResarch.szRTName = setTutor.szTrueName + ConfigConst.GCReachTestName;
                        setResarch.dwLeaderID = setTutor.dwAccNo;
                        setResarch.szLeaderName = setTutor.szTrueName;
                        if (m_Request.Reserve.SetResearchTest(setResarch, out setResarch) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            m_Request.Account.ExtIdentAccSet(setTutor);
                        }
                    }
                }
                else
                {
                    TUTORREQ tutorReq = new TUTORREQ();
                    tutorReq.dwTutorID = teacherAcc.dwAccNo;
                    UNITUTOR[] vtTutor;
                    if (m_Request.Account.TutorGet(tutorReq, out vtTutor) == REQUESTCODE.EXECUTE_SUCCESS && vtTutor != null && vtTutor.Length > 0)
                    {

                    }
                    else
                    {
                        EXTIDENTACC setTutor = new EXTIDENTACC();
                        setTutor.dwAccNo = teacherAcc.dwAccNo;
                        setTutor.szTrueName = teacherAcc.szTrueName;
                        RESEARCHTEST setResarch = new RESEARCHTEST();
                        setResarch.szRTName = teacherAcc.szTrueName;
                        setResarch.dwRTKind = (uint)RESEARCHTEST.DWRTKIND.RTKIND_RTASK;
                        setResarch.szRTName = setTutor.szTrueName + ConfigConst.GCReachTestName;
                        setResarch.dwLeaderID = setTutor.dwAccNo;
                        setResarch.szLeaderName = setTutor.szTrueName;
                        if (m_Request.Reserve.SetResearchTest(setResarch, out setResarch) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            m_Request.Account.ExtIdentAccSet(setTutor);
                        }
                    }
                }
                TUTORSTUDENT turtorStudent = new TUTORSTUDENT();
                turtorStudent.dwTutorID = teacherAcc.dwAccNo;
                turtorStudent.szTutorName = teacherAcc.szTrueName;
                turtorStudent.dwAccNo = studentAcc.dwAccNo;
                turtorStudent.dwStatus = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK);
                turtorStudent.szPID = studentAcc.szLogonName;
                turtorStudent.szTrueName = studentAcc.szTrueName;

                m_Request.Account.TutorStudentSet(turtorStudent);
                nImported = nImported+1;
            }
        }

        tWrite.Close();
    }
}