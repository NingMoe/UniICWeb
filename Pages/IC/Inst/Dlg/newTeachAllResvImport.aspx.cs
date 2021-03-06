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
    protected string szErrLines = "";
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

                Import(pagedata.szFilePath, szErrListRawFile, out nImported, out nFailed, out szErrLines);
                pagedata.dwTotalLine = nImported + nFailed;
                pagedata.dwImported = nImported;
                pagedata.dwFailed = nFailed;

                for (int i = 0; i < ErrorLines.Count; i++)
                {
                    szErrLines += ErrorLines[i].ToString();
                    if (i < ErrorLines.Count - 1)
                    {
                        szErrLines += ",";
                    }
                }
                //pagedata.szErrLines = szErrLines;
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

    void Import(string szFilePath, string szErrListRawFile, out uint nImported, out uint nFailed, out string szOutError)
    {
        szOutError = "";
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
        UNITERM[] termNow = GetTermNow();
        int nContint = 0;
        szOutError = "<table>";
        while ((strline = mysr.ReadLine()) != null)
        {
            nContint = nContint + 1;
            if (nContint == 1)
            {
                continue;
            }

            string szError = "";

            string[] szList = strline.Split(',');
            if (szList.Length < 4)
            {
                continue;
            }
            count = count + 1;
            string szRoomName = szList[0];
            string[] szResvWeeksList = szList[1].Split(';'); ;//周次列表
            string[] szWeekList = szList[2].Split(';');//星期
            string[] szSecs = szList[3].Split(';');//节次

            UNIROOM setRoom;
     
            for (uint i = 0; i < szResvWeeksList.Length; i++)//周数组循环
            {
                string szResvWeekTemp = szResvWeeksList[i];
                if (szResvWeekTemp == "")
                {
                    continue;
                }
                string[] szStartEndWeekList = szResvWeekTemp.Split('-');//
                uint uStrartWeeks = 0;
                uint uEndWeeks = 0;
                if (szStartEndWeekList.Length == 1)
                {
                    uStrartWeeks = Parse(szStartEndWeekList[0]);
                    uEndWeeks = Parse(szStartEndWeekList[0]);
                }
                else if (szStartEndWeekList.Length == 2)
                {
                    uStrartWeeks = Parse(szStartEndWeekList[0]);
                    uEndWeeks = Parse(szStartEndWeekList[1]);
                }
                else
                {
                    szOutError += "<tr><td>" + szList[1] + "(" + szResvWeekTemp + ")" + "</td><td></td><td>格式不对</td></tr>";
                    uCountFali = uCountFali + 1;
                    continue;
                }
                if (uEndWeeks < uStrartWeeks)
                {
                    szOutError += "<tr><td>" + szRoomName + "</td><td>" + szResvWeekTemp + "</td><td>开始周次大于结束周次</td></tr>";
                    uCountFali = uCountFali + 1;
                    continue;
                }
                if (uStrartWeeks == 0 || uEndWeeks == 0)
                {
                    szOutError += "<tr><td>" + szRoomName + "</td><td>" + szResvWeekTemp + "</td><td>开始周次或者结束周次格式不对</td></tr>";
                    uCountFali = uCountFali + 1;
                    continue;
                }
                for (uint k = uStrartWeeks; k <= uEndWeeks; k++)//周循环
                {
                    //k表示第几周
                  //  uint uWeeksReal = Parse(szWeekList[m - 1]);
                    for (uint m = 0; m < szWeekList.Length; m++)//m表示星期
                    {
                        uint uWeekReal = Parse(szWeekList[m]) - 1;
                        if (uWeekReal < 0 || uWeekReal > 7)
                        {
                            szOutError += "<tr><td>" + szRoomName + "</td><td>" + szList[1] + "内的第" + k + "周的星期(" + m + ")" + "</td><td>格式错误</td></tr>";
                            uCountFali = uCountFali + 1;
                            continue;
                        }
                      
                        int uResvDate = GetDateFromWeek((uint)termNow[0].dwYearTerm, k, (uWeekReal));//预约的日期
                        if (uResvDate.ToString().Length != 8)
                        {
                            szOutError += "<tr><td>" + szRoomName + "</td><td>" + szList[1] + "内的第" + k + "周的星期(" + m + ")" + "</td><td>格式错误</td></tr>";
                            uCountFali = uCountFali + 1;
                            continue;
                        }
                        for (uint n = 0; n < szSecs.Length; n++)//节次
                        {
                            int uBeginSec = -1;
                            int uEndSec = -1;
                            string[] szSecTempList = szSecs[n].Split('-');
                            if (szSecTempList.Length == 1)
                            {
                                if (szSecTempList[0] == "")
                                {
                                    continue;
                                }
                                uBeginSec = uEndSec = IntParse(szSecTempList[0]);
                            }
                            else if (szSecTempList.Length == 2)
                            {
                                if (szSecTempList[0] == "" || szSecTempList[1] == "")
                                {
                                    continue;
                                }
                                uBeginSec = IntParse(szSecTempList[0]);
                                uEndSec = IntParse(szSecTempList[1]);
                            }
                            else
                            {
                                szOutError += "<tr><td>" + szRoomName + "</td><td>" + szList[1] + "(" + szResvWeekTemp + ")" + szSecs[n] + "</td><td>格式不对</td></tr>";
                                uCountFali = uCountFali + 1;
                                continue;
                            }
                            if (uBeginSec == -1 || uEndSec == -1)
                            {
                                szOutError += "<tr><td>" + szRoomName + "</td><td>" + szList[1] + "(" + szResvWeekTemp + ")" + szSecs[n] + "</td><td>节次小于1</td></tr>";
                                uCountFali = uCountFali + 1;
                                continue;
                            }
                            if (uBeginSec < 1 || uEndSec < 1)
                            {
                                szOutError += "<tr><td>" + szRoomName + "</td><td>" + szList[1] + "(" + szResvWeekTemp + ")" + szSecs[n] + "</td><td>节次小于1</td></tr>";
                                uCountFali = uCountFali + 1;
                                continue;
                            }
                            uint uBeginTime = (uint)(termNow[0].szCTS1[uBeginSec-1].dwBeginTime);
                            uint uEndTime = (uint)(termNow[0].szCTS1[uEndSec-1].dwEndTime);
                            if (!GetRoomByName(szRoomName, out setRoom))
                            {
                                szOutError += "<tr><td>" + szRoomName + "</td><td></td><td>房间名称不存在</td></tr>";
                                uCountFali = uCountFali + 1;
                                continue;
                            }

                            //预约代码
                            string szResvDate = uResvDate / 10000 + "-" + (uResvDate % 10000) / 100 + "-" + uResvDate % 100; 
                            DateTime resvDate = DateTime.Parse(szResvDate);
                            string szBeginTimeTemp = resvDate.ToString("yyyy-MM-dd") + " " + (uBeginTime / 100).ToString() + ":" + (uBeginTime % 100).ToString();
                            uint uResvBeginTime = Get1970Seconds(szBeginTimeTemp);
                            string szEndTimeTemp = resvDate.ToString("yyyy-MM-dd") + " " + (uEndTime / 100).ToString() + ":" + (uEndTime % 100).ToString();
                            uint uResvEndTime = Get1970Seconds(szEndTimeTemp);

                            ALLUSERRESV setValue = new ALLUSERRESV();
                            setValue.dwBeginTime = uResvBeginTime;
                            setValue.dwEndTime = uResvEndTime;
                            setValue.dwLabID = setRoom.dwLabID;
                            setValue.szLabName = setRoom.szLabName;
                            setValue.szTestName = "全体人员预约";
                            setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING + (uint)UNIRESERVE.DWPURPOSE.USEFOR_ALLUSER;
                            UNIDEVICE[] devList;
                            devList = GetDevByRoomId(setRoom.dwRoomID);
                            RESVDEV[] resvDev = new RESVDEV[devList.Length];
                            for (int j = 0; j < devList.Length; j++)
                            {
                                resvDev[j].dwDevEnd = devList[j].dwDevSN;
                                resvDev[j].dwDevKind = devList[j].dwKindID;
                                resvDev[j].dwDevStart = devList[j].dwDevSN;
                                resvDev[j].dwDevNum = 1;
                                resvDev[j].szRoomNo = devList[j].szRoomNo;
                            }
                            setValue.ResvDev = resvDev;
                            if (m_Request.Reserve.AllUserResvSet(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                szOutError += "<tr><td>" + szRoomName + "</td><td>" + szBeginTimeTemp + "到" + szEndTimeTemp + "</td><td>" + m_Request.szErrMessage + "</td></tr>";
                                szError += m_Request.szErrMessage + ",";
                                uCountFali = uCountFali + 1;
                            }
                            else
                            {
                                nImported = nImported + 1;
                            }
                        }
                    }

                }
            }


        }
        szOutError += "</table>";
        if (!bAllRight)
        {
            DownloadFile(Response, swCSV.GetStringBuilder(), "resarchTestImport.csv");
        }
       // nImported = count;
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