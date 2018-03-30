using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class ClientWeb_xcus_zyy_Proxy : UniClientPage
{
    protected string rtList = "";
    protected string resvList = "";
    UNIACCOUNT acc;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Session["LOGIN_ACCINFO"] == null || !IsClientLogin())
        {
            Response.Redirect("Default.aspx");
        }
        acc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //获取项目列表
        InitRTest();
        InitRsv();
    }

    private void InitRTest()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        RESEARCHTEST[] vrResult;
        vrGet.dwLeaderID = acc.dwAccNo;
        uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                rtList += "<tr><td ><input type='hidden' class='courseId' value='" + vrResult[i].dwRTID + "'/>" +CutStrT( vrResult[i].szRTName,14) + "</td><td >" + vrResult[i].szHolderName + "</td><td >" + Util.Converter.GetRTLevel(vrResult[i].dwRTLevel) + "</td><td>" + CutStrT(vrResult[i].szFromUnit,12) + "</td><td>"
                     + CutStrT(vrResult[i].szDeptName,12) + "</td><td><input type='hidden' class='rtGroupId' value='" + vrResult[i].dwGroupID + "'/>" + vrResult[i].dwGroupUsers + "</td><td>" +
                    "<a class='click' onclick='mbManage(\"" + vrResult[i].dwRTID + "\",\"" + vrResult[i].szRTName.Replace('"', '”') + "\")'>成员管理</a></td></tr>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
            return;
        }
    }

    private void InitRsv()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        RTRESVREQ vrGet = new RTRESVREQ();
        uint? accno = acc.dwAccNo;
        vrGet.dwLeaderID = accno;
        uint intStartTime = uint.Parse(DateTime.Now.AddYears(-1).ToString("yyyyMMdd"));
        uint intEndTime = uint.Parse(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        vrGet.dwBeginDate = intStartTime;
        vrGet.dwEndDate = intEndTime;
        vrGet.dwUnNeedStat = (int)UNIRESERVE.DWSTATUS.RESVSTAT_DOING | (int)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult!=null)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                resvList += "<tr>";
                resvList += "<td  style='text-align:left;'>" + CutStrT(resv.szDevName, 10) + "</td><td>" + resv.szOwnerName + "</td><td class='td_lab'>" + CutStrT(resv.szTestName, 8) + "</td>";
                resvList += "<td class='td_course'>" + CutStrT(resv.szRTName, 8) + "</td><td>" + resv.szHolderName + "</td>";
                int begin = Convert.ToInt32(resv.dwBeginTime);
                int end = Convert.ToInt32(resv.dwEndTime);
                resvList += "<td><div><span style='font-weight:600;color:#555'>开始：</span>" + Get1970Date(begin) + " </div><div><span style='font-weight:600;color:#555'>结束：</span>" + Get1970Date(end) + "</div></td>";
                resvList += "<td class='rsv_stat' stat='" + resv.dwStatus + "' myself='" + (resv.dwOwner == accno ? "1" : "0") + "'>" + Util.Converter.RsvCheckStaConverterT(resv.dwStatus) + "</td>";
                resvList += "<td class='m_stat' stat='" + resv.dwStatus + "'>" + Util.Converter.RsvCheckStaConverterM(resv.dwStatus) + "</td>";
                string act = "";
                string fee = GetRefFee(resv);
                resvList += "<td>" + fee + "元</td>";
                if ((resv.dwStatus & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0)
                {
                    act += "[<a class='click' onclick='ckResv(\"ok\",\"" + resv.dwResvID + "\");'>审核通过</a>]<br/>[<a class='click' onclick='ckResv(\"fail\",\"" + resv.dwResvID + "\");'>拒绝通过</a>]";
                }
                else if ((resv.dwStatus & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
                {
                    act += "[<a class='click' onclick='ckResv(\"ok\",\"" + resv.dwResvID + "\");'>审核通过</a>]";
                }
                else if (((resv.dwStatus & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) && ((resv.dwStatus & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) == 0) && ((resv.dwStatus & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) == 0))
                {
                    act += "[<a class='click' onclick='ckResv(\"fail\",\"" + resv.dwResvID + "\");'>拒绝通过</a>]";
                }
                if (act == "")
                {
                    act = "无";
                }
                resvList += "<td>" + act + "</td></tr>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
}