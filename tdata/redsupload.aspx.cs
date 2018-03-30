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
using System.Threading;
using System.IO;
using System.Diagnostics;
using UniWebLib;

public partial class tdata_redsupload : UniPage
{
    protected string m_szOut = "";
    protected string m_szOutDISK = "";
    protected string m_szOutDISKSTATE = "";
    protected int m_tab = 0;
    protected string szFormID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["tabs"] != null)
        {
            m_tab = (int)Session["tabs"];
        }
        if (m_Request.m_UniDCom.SessionID == 0)
        {
            if (!string.IsNullOrEmpty((string)Request["sessionid"]))
            {
                Session["sessionid"] = Parse(Request["sessionid"]);
                Session["UniCodeSessionid"] = Request["sessionid"];
            }
            if (!string.IsNullOrEmpty((string)Request["staid"]))
            {
                Session["StationSN"] = Parse(Request["staid"]);
                Session["UniStaid"] = Request["staid"];
            }
            if (string.IsNullOrEmpty((string)Session["UniCodeSessionid"]))
            {
                Response.Redirect("Error.html");
                return;
            }
            m_Request.m_UniDCom.SessionID = Parse((string)Session["UniCodeSessionid"]);
            m_Request.m_UniDCom.StaSN = Parse((string)Session["UniStaid"]);
        }
        UNIACCOUNT accno = new UNIACCOUNT();
        if (Session["loginacc"] == null)
        {
            ACCINFOREQ recGet = new ACCINFOREQ();
            
            if (m_Request.Account.AccInfoGet(recGet, out accno) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Session["loginacc"] = accno;
            }
            else {
                Response.Write("未获取个人信息请再试");
                Response.End();
            }
        }
        szFormID = form1.ClientID;
        string szFileID = Request["delID"];
        if (szFileID != null && szFileID != "" && szFileID.IndexOf(',') == -1)
        {
            if (isExist(szFileID))
            {
                CLOUDDISK diskDel = new CLOUDDISK();
                diskDel.dwFileID = Parse(szFileID);
                if (m_Request.Account.CloudDiskDel(diskDel) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Session["tabs"] = 1;
                }
            }
        }

        TESTITEMINFOREQ testItemReq = new TESTITEMINFOREQ();
        TESTITEMINFO[] testitem;
        if (m_Request.Reserve.GetTestItemInfo(testItemReq, out testitem) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < testitem.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + testitem[i].szTeacherName + "</td>";
                m_szOut += "<td>" + testitem[i].szCourseName + "</td>";
                m_szOut += "<td>" + testitem[i].szTestName + "</td>";
                uint uState = (uint)testitem[i].dwStatus;
                uint uScore = (uint)testitem[i].dwReportScore;
                string szURL = testitem[i].szReportURL;
                if (uScore != 0)
                {
                    m_szOut += "<td>" + "实验报告已批改" + "</td>";
                    m_szOut += "<td>" + "" + "</td>";
                }
                else
                {
                    if (szURL != null && szURL != "")
                    {
                        m_szOut += "<td>" + "实验报告已提交" + "</td>";
                        string szHref = "<a class='href' href='" + ("..\\ClientWeb\\upload\\UpLoadFile\\" + testitem[i].dwTestItemID.ToString()) + "\\" + testitem[i].szReportURL + "' class='subReport'>下载</a>";
                        szHref += "___<a class='href subReport' data-id=" + testitem[i].dwTestItemID.ToString() + " >重新提交</a>";
                        m_szOut += "<td>" + szHref + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td>" + "实验报告未提交" + "</td>";
                        string szHref = "<a class='href' href='" + ("..\\ClientWeb\\upload\\UpLoadFile\\" + testitem[i].szReportFormURL.ToString()) + "' class='subReport'>下载模板</a>";
                        szHref += "___<a class='href subReport' data-id=" + testitem[i].dwTestItemID.ToString() + " >提交实验报告</a>";
                        m_szOut += "<td>" + szHref + "</td>";
                    }
                }
            }
        }

        CDISKSTATREQ vrstateGet = new CDISKSTATREQ();
        vrstateGet.dwAccNo = accno.dwAccNo;
        CDISKSTAT vtState;
        string szLogoName = "";
        if (m_Request.Account.CloudDiskStat(vrstateGet, out vtState) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            szLogoName = vtState.szPID;
            m_szOutDISKSTATE += "网络硬盘可用大小：" + vtState.dwTotalSize.ToString() + "mb；" + "已经使用：" + vtState.dwUsedSize.ToString() + "mb；文件个数：" + vtState.dwFileNum.ToString() + "个";
        }
        else
        {
            m_szOutDISKSTATE += "请刷新后重试";
            return;
        }
        //获取个人网盘
        CLOUDDISKREQ cloudreq = new CLOUDDISKREQ();
        cloudreq.dwAccNo = accno.dwAccNo;
        CLOUDDISK[] disk;
        if (m_Request.Account.CloudDiskOpen(cloudreq, out disk) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < disk.Length; i++)
            {
                m_szOutDISK += "<tr>";
                m_szOutDISK += "<td>" + disk[i].szFileName + "</td>";
                m_szOutDISK += "<td>" + disk[i].dwFileSize + "</td>";
                m_szOutDISK += "<td>" + GetDateStr(disk[i].dwSubmitDate) + "</td>";
                m_szOutDISK += "<td>" + disk[i].szMemo + "</td>";
                string szHref = "<a class='href' href='" + ("..\\ClientWeb\\upload\\UpLoadFile\\mydisk\\" + szLogoName + "\\" + disk[i].szLocation.ToString()) + "' class='subReport'>下载</a>";
                szHref += "___<a class='href del' data-id=" + disk[i].dwFileID.ToString() + " >删除</a>";
                m_szOutDISK += "<td>" + szHref + "</td>";
                m_szOutDISK += "</tr>";
            }
        }
    }
    protected bool isExist(string fileID)
    {
        CLOUDDISKREQ cloudreq = new CLOUDDISKREQ();
        CLOUDDISK[] disk;
        if (m_Request.Account.CloudDiskOpen(cloudreq, out disk) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < disk.Length; i++)
            {
                if (disk[i].dwFileID.ToString() == fileID)
                {
                    return true;
                }
            }
        }
        return false;
    }
}