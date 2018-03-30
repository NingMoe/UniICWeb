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
                dwTotalLine = (uint)(n);
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
        REQUESTCODE uResponse=REQUESTCODE.EXECUTE_FAIL;
        System.IO.StringWriter swCSV = new System.IO.StringWriter();
        bool bAllRight=true;
        uint count = 0;
        uint uCountFali = 0;

        while ((strline = mysr.ReadLine()) != null)
        {
            count = count + 1;
            string[] szList = strline.Split(',');
           
            string szDevName = szList[0];
            string szStartDate = szList[1];
            string szEndDate = szList[2];
            string szSelectWeek = szList[3];
            string szResvTime = szList[4];

            string szLogonNames = szList[5];
            string[] szOwnerList = szLogonNames.ToString().Split(';');
            UNIDEVICE[] devList;

            devList = getDevByName(szDevName);
            if (szDevName == "" || devList == null || devList.Length == 0 || devList.Length > 1)
            {
                uCountFali = uCountFali + 1;
                continue;
            }
            DateTime startDate = DateTime.Parse(szStartDate);
            DateTime endDate = DateTime.Parse(szEndDate);
            TimeSpan span = endDate - startDate;
            while (span.Days > -1)
            {
                int uweek = (int)startDate.DayOfWeek;
                if (uweek == 0)
                {
                    uweek = 7;
                }
                if (szSelectWeek == null || szSelectWeek == "" || szSelectWeek.IndexOf(uweek.ToString()) > -1)
                {
                    string[] resvTimesList = szResvTime.Split(';');
                    if (resvTimesList == null || resvTimesList.Length == 0)
                    {
                        return;
                    }
                    for (int m = 0; m < resvTimesList.Length; m++)
                    {
                        UNIRESERVE setValue = new UNIRESERVE();
                        UNIGROUP resvGroup;
                        if (NewGroup("管理员新建预约", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out resvGroup))
                        {
                            for (int i = 0; i < szOwnerList.Length; i++)
                            {
                                UNIACCOUNT acc = new UNIACCOUNT();
                                if (GetAccByLogonName(szOwnerList[i], out acc))
                                {
                                    AddGroupMember(resvGroup.dwGroupID, acc.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                                }
                            }


                            setValue.dwMemberID = resvGroup.dwGroupID;
                            setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;

                            UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                            setValue.dwOwner = vrAccInfo.dwAccNo;
                            setValue.szOwnerName = vrAccInfo.szTrueName;
                            setValue.szTestName = Request["szMemo"];

                            UNIDEVICE dev = new UNIDEVICE();
                            dev = devList[0];
                            setValue.ResvDev = new RESVDEV[1];
                            setValue.ResvDev[0].dwDevEnd = dev.dwDevSN;
                            setValue.ResvDev[0].dwDevStart = dev.dwDevSN;
                            setValue.ResvDev[0].dwDevNum = 1;
                            setValue.ResvDev[0].dwDevKind = dev.dwKindID;
                            setValue.ResvDev[0].szRoomNo = dev.szRoomNo;
                            setValue.ResvDev[0].szDevName = dev.szDevName;
                            setValue.dwLabID = dev.dwLabID;
                            setValue.szLabName = dev.szLabName;


                            setValue.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
                            setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;


                            string[] szResvTimeListIn = resvTimesList[m].Split('-');
                            if (szResvTimeListIn == null || szResvTimeListIn.Length < 2)
                            {
                                continue;
                            }
                            string szStartTime = szResvTimeListIn[0];
                            string szEndTime = szResvTimeListIn[1];
                            string szStartTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szStartTime;
                            string szEndTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szEndTime;

                            setValue.dwBeginTime = Get1970Seconds(szStartTimeTemp);
                            setValue.dwEndTime = Get1970Seconds(szEndTimeTemp);


                            if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                count = count + 1;
                            }
                            else
                            {
                                uCountFali = uCountFali + 1;
                            }
                            setValue.dwResvID = null;
                        }
                    }

                }
                startDate = startDate.AddDays(1);
                span = endDate - startDate;
            }
        }
        if (!bAllRight)
        {
            DownloadFile(Response, swCSV.GetStringBuilder(), "resarchTestImport.csv");
        }
        nImported = count;
        nFailed = uCountFali;
        tWrite.Close();
    }
    public UNIACCOUNT[] GetAccByTrueName(string szTrueName)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.szTrueName = (szTrueName);
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;
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
    public UNIDEVICE[] getDevByName(string szName)
    {
        DEVREQ vrGet = new DEVREQ();
        vrGet.szSearchKey = szName;
        UNIDEVICE[] vtDev;
        REQUESTCODE uResponse = m_Request.Device.Get(vrGet, out vtDev);
        return vtDev;
    }
}