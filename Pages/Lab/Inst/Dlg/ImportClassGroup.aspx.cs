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
    protected string szYearTerm;
    protected string m_Title = "导入";
    protected string m_szTerm = "";
    protected string szErrLinesList = "";
    public class classGroup {
        public string szGroupName;
        public uint uGroupID;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        GetHTTPObj(out pagedata);

        Response.CacheControl = "no-cache";
        UNITERM[] vtTerm = GetAllTerm();
        uint uEndDate = 0;

        uEndDate = Parse(Request["dwDeadLine"]);

        if (vtTerm != null)
        {
            for (int i = 0; i < vtTerm.Length; i++)
            {
                if ((vtTerm[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    if (uEndDate == 0)
                    {
                        uEndDate = (uint)vtTerm[i].dwEndDate;
                    }
                    szYearTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwEndDate.ToString(), true);
                }
                else
                {
                    szYearTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwEndDate.ToString());
                }
            }
        }

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
                
                Import(pagedata.szFilePath, szErrListRawFile, out nImported, out nFailed, out szErrLinesList);
                pagedata.dwTotalLine = nImported + nFailed;
                pagedata.dwImported = nImported;
                pagedata.dwFailed = nFailed;
                pagedata.szErrLines = szErrLinesList;
                /*
                for (int i = 0; i < ErrorLines.Count; i++)
                {
                    szErrLines += ErrorLines[i].ToString();
                    if (i < ErrorLines.Count - 1)
                    {
                        szErrLines += ",";
                    }
                }
                pagedata.szErrLines = szErrLines;
                 * */
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
                    szOut += "<tfoot><tr><td class='importTblMore' colspan='" + fieldCount + "'>总" + dwTotalLine + "条</td></tr></tfoot>";
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
        szOutError += "<table border='1px' class='tblCSV'>";
        ErrorLines = new ArrayList();
        nImported = 0;
        nFailed = 0;
        string strline;

        StreamWriter tWrite = new StreamWriter(szErrListRawFile, false, Encoding.GetEncoding("gb2312"));
        System.IO.StreamReader mysr = new StreamReader(Server.MapPath(szFilePath), Encoding.GetEncoding("gb2312"));
        ArrayList list = new ArrayList();
        string szEndLine = Request["dwDeadLine"];
        uint uEndLine = Parse(szEndLine);
        if (uEndLine == 0)
        {
            return;
        }
        int nCountTemp = 0;
        while ((strline = mysr.ReadLine()) != null)
        {
            if (nCountTemp == 0)
            {
                nCountTemp = nCountTemp + 1;
                continue;
            }
            string[] szList = strline.Split(',');

            string szGrouName = szList[0].Trim();
            uint uGroupID = bIsExitst(list, szGrouName, uEndLine);
            if (uGroupID == 0)
            {
                UNIGROUP newGroup;
                if (NewGroup(szGrouName, (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out newGroup, uEndLine))
                {
                    classGroup newGroupTemp = new classGroup();
                    newGroupTemp.uGroupID = (uint)newGroup.dwGroupID;
                    newGroupTemp.szGroupName = szGrouName;
                    list.Add(newGroupTemp);
                    uGroupID = (uint)newGroup.dwGroupID;
                }
            }
            else
            {
                //szOutError += "<tr><td>" + szGrouName + "</td><td>" +"已存在，不新建直接插入成员"+"</td></tr>";
            }
            string szPID = szList[1].Trim();
            uint szTrueName = Parse(szList[1]);
            UNIACCOUNT accinfo;
            if (GetAccByLogonName(szPID, out accinfo, true) && uGroupID != 0)
            {
                if (IsInClassGroupMember(uGroupID, accinfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL))
                {
                    szOutError += "<tr><td>" + szPID + "</td><td>" + "已经添加" + "</td></tr>";
                    nFailed = nFailed + 1;
                }
                else
                {
                    REQUESTCODE uResponse = AddGroupMember(uGroupID, accinfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL, accinfo.szTrueName.ToString());
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {

                        nImported = nImported + 1;
                    }
                    else
                    {
                        szOutError += "<tr><td>" + szPID + "</td><td>" + "添加失败，" + m_Request.szErrMessage.ToString() + "</td></tr>";
                        nFailed = nFailed + 1;
                    }
                }
            }
            else
            {
                szOutError += "<tr><td>" + szPID + "</td><td>" + ":不存在" + "</td></tr>";
            }

        }

        tWrite.Close();
        szOutError += "</table>";
    }
    public uint bIsExitst(ArrayList list, string szName, uint uEndLine)
    {
        for (int i = 0; i < list.Count; i++)
        {
            classGroup temp = new classGroup();
            temp = (classGroup)list[i];
            if (temp.szGroupName == szName)
            {
                return temp.uGroupID;
            }
        }
        UNIGROUP[] groupList;
        groupList= GetGroupByName(szName);
        if (groupList != null && groupList.Length > 0 && (groupList[0].szName.ToString() == szName) && (groupList[0].dwDeadLine.ToString() == uEndLine.ToString()))
        {
            return (uint)groupList[0].dwGroupID;
                
        }
        return 0;
    }
}