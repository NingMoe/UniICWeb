using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Configuration;

public partial class UserCenter : UniClientPage
{
    protected string resvList = "";
    protected string feeList = "";
    protected string memList = "";
    protected string applyList = "";
    protected string dataList = "";
    protected string rtList = "";
    protected string TutorHide = "default";
    protected string TutorShow = "none";
    protected string accPhone = "";
    protected string accEmail = "";
    protected string tutorName = "";
    protected string tutorckSta = "";
    protected string tutorAcc = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] == null || !IsClientLogin())
        {
            Response.Redirect("Default.aspx");
        }
        base.LoadPage();

        //用户信息
        InitAcc();
        //预约列表和费用列表
        if (!IsPostBack)
        {
            DateTime start = DateTime.Now.AddYears(-1);
            DateTime end = DateTime.Now.AddMonths(3);
            iptDateStart.Value = start.ToString("yyyy-MM-dd");
            iptDateEnd.Value = end.ToString("yyyy-MM-dd");
            InitResv(ToUInt(start.ToString("yyyyMMdd")), ToUInt(end.ToString("yyyyMMdd")));
        }
        //实验数据
        dataList = GetdataList();
        //项目列表
        InitrtList("rtest");
    }
    private uint ToDate(string str)
    {
        string y = str.Substring(0, 4);
        string m = str.Substring(5, 2);
        string d = str.Substring(8, 2);
        return Convert.ToUInt32(y + m + d);
    }
    private void InitAcc()
    {
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];

        accName.InnerText = vrAccInfo.szTrueName;
        accLgName.InnerText = vrAccInfo.szLogonName;
        accColl.InnerText = vrAccInfo.szDeptName;
        accPhone = vrAccInfo.szHandPhone;
        accEmail = vrAccInfo.szEmail;
        TUTORREQ vrPra = new TUTORREQ();
        vrPra.dwStudentAccNo = vrAccInfo.dwAccNo;
        UNITUTOR[] vrTutor;
        if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
        {
            tutorName = vrTutor[0].szTrueName;
            tutorAcc = vrTutor[0].dwAccNo.ToString();
            TUTORSTUDENTREQ vrStuGet = new TUTORSTUDENTREQ();
            vrStuGet.dwTutorID = vrTutor[0].dwAccNo;
            TUTORSTUDENT[] vrStu;
            if (m_Request.Account.TutorStudentGet(vrStuGet, out vrStu) == REQUESTCODE.EXECUTE_SUCCESS && vrStu != null)
            {
                for (int i = 0; i < vrStu.Length; i++)
                {
                    if (vrStu[i].dwAccNo == vrAccInfo.dwAccNo)
                    {
                        Session["TutorInfo"] = vrStu[i];
                        if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)
                        {
                            tutorckSta += "(<span style='color:green;'>导师已确认</span>)";
                        }
                        else if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)
                        {
                            tutorckSta += "(<span style='color:red;'>导师已否认</span>)";
                        }
                        else
                        {
                            tutorckSta += "(导师未确认)";
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            tutorckSta = "未指定";
        }
    }

    private void InitResv(uint startDate, uint endDate)
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwResvID = 0;//获取多条样品费用，要设置为0
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((vrAccInfo.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
        {
            TutorHide = "none";
            TutorShow = "default";
            //GetMemList(vrAccInfo.dwAccNo);
        }
        //vrGet.dwMAccNo = vrAccInfo.dwAccNo;
        uint? acc = vrAccInfo.dwAccNo;
        //vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;uint.Parse(DateTime.Now.AddMonths(-3).ToString("yyyyMMdd"));
        uint intStartTime = startDate;
        uint intEndTime = endDate;
        vrGet.dwMAccNo = acc;
        vrGet.dwHolderID = acc;
        vrGet.dwBeginDate = intStartTime;
        vrGet.dwEndDate = intEndTime;
        vrGet.dwUnNeedStat = (int)UNIRESERVE.DWSTATUS.RESVSTAT_DONE;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                resvList += "<tr>";
                resvList += "<td>" + resv.szOwnerName + "</td><td class='tl'>" + ToNavDev(CutStrT(resv.szDevName, 20),resv.dwDevID) + "</td><td class='tl'>" + CutStrT(resv.szTestName, 14) + "</td>";
                resvList += "<td class='tl'>" + CutStrT(resv.szRTName, 12) + "<span class='click'  title='负责人:" + resv.szHolderName + "; 授权委托:" + resv.szLeaderName + "'>.详细</span></td>";
                resvList += "<td>" + Get1970Date((int)resv.dwOccurTime) + "</td>";
                int begin = Convert.ToInt32(resv.dwBeginTime);
                int end = Convert.ToInt32(resv.dwEndTime);
                resvList += "<td class='tl'><div><span style='font-weight:600;color:#274A5C'>开始: </span>" + Get1970Date(begin).Substring(5) + " </div><div><span style='font-weight:600;color:#274A5C'>结束: </span>" + Get1970Date(end).Substring(5) + "</div></td>";
                resvList += "<td class='rsv_stat' stat='" + resv.dwStatus + "' myself='" + (resv.dwOwner == acc ? "1" : "0") + "'>" + Util.Converter.RsvCheckStaConverterT(resv.dwStatus) + "</td>";
                resvList += "<td class='m_stat' stat='" + resv.dwStatus + "'>" + Util.Converter.RsvCheckStaConverterM(resv.dwStatus) + "</td>";
                string act = "";
                if (((resv.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0) && (resv.dwOwner == acc))
                {
                    act += "[<a class='click delResv' onclick='delAct(\"del_rt_resv\",\"" + resv.dwResvID + "\");'>删除</a>]<br/>";
                }
                if (TutorHide == "none")
                {
                    string fee = GetRefFee(resv);//custom
                    resvList += "<td class='tl'>" + fee + "元</td>";
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
                }
                if (act == "")
                {
                    act = "无";
                }
                resvList += "<td>" + act + "</td></tr>";
            }
        }
        //预约消费表
        if (TutorHide == "none")
        {
//ToUInt(DateTime.Now.AddYears(-1).ToString("yyyyMMdd"));ToUInt(DateTime.Now.ToString("yyyyMMdd"));
            vrGet.dwBeginDate = startDate;
            vrGet.dwEndDate = endDate;
            vrGet.dwHolderID = acc;
            vrGet.dwUnNeedStat = null;
            vrGet.dwCheckStat = (int)UNIRESERVE.DWSTATUS.RESVSTAT_SETTLED;
            uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
            {
                for (int i = 0; i < vtResult.Length; i++)
                {
                    RTRESV resv = vtResult[i];
                    feeList += "<tr>";
                    feeList += "<td  style='text-align:left;'>" + CutStrT(resv.szDevName, 10) + "</td><td>" + resv.szOwnerName + "</td><td class='td_lab'>" + CutStrT(resv.szTestName, 8) + "</td>";
                    feeList += "<td class='td_course'>" + CutStrT(resv.szRTName, 8) + "</td>";
                    int beginTime = Convert.ToInt32(resv.dwBeginTime);
                    int endTime = Convert.ToInt32(resv.dwEndTime);
                    feeList += "<td><div><span style='font-weight:600;color:#274A5C'>开始: </span>" + Get1970Date(beginTime) + " </div><div><span style='font-weight:600;color:#274A5C'>结束: </span>" + Get1970Date(endTime) + "</div></td>";
                    feeList += "<td>" + MinToHour(resv.dwRealUseTime) + "</td>";
                    feeList += "<td>已结算</td>";
                    string fee = GetRefFee(resv);//custom
                    feeList += "<td>" + fee + " 元</td>";
                    feeList += "<td>" + resv.dwRealCost / 100.00 + "元</td>";
                    feeList += "</tr>";
                }
            }
        }
    }

    //项目列表
    private void InitrtList(string type)
    {
        string msg;
        RESEARCHTEST[] vrResult;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrResult = GetrtList(type, out msg);
        if (vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                bool isMyself = false;
                if (vrResult[i].dwHolderID == acc.dwAccNo || vrResult[i].dwLeaderID == acc.dwAccNo) isMyself = true;
                int num;
                string opera = GroupOperate(vrResult[i].RTMembers, acc.dwAccNo, vrResult[i].dwRTID, vrResult[i].dwGroupID, isMyself,out num);
                rtList += "<tr id='" + vrResult[i].dwRTID + "' ><td ><input type='hidden' class='courseId' value='" + vrResult[i].dwRTID + "'/>" +CutStrT( vrResult[i].szRTName,16 )+ "</td><td >" + vrResult[i].szHolderName + "</td><td >" + vrResult[i].szLeaderName + "</td><td>" + vrResult[i].szFromUnit + "</td><td>"
                    + CutStrT(vrResult[i].szDeptName, 12) + "</td><td><input type='hidden' class='rtGroupId' value='" + vrResult[i].dwGroupID + "'/>" + num + "</td>" + opera + "</tr>";
            }
        }
        else
        {
            MsgBox(msg);
        }
    }
    private string GroupOperate(RTMEMBER[] list, uint? accNo, uint? rtId, uint? groupid,bool isMyself,out int mbNum)
    {
        mbNum = 0;
        string str = "";
        string mbList = "<td class='operate'  rtid='" + rtId + "' groupid='" + groupid + "'>"; 
        string op = "";
        bool isCk=true;
        for (int i = 0; i < list.Length; i++)
        {
            if ((list[i].dwStatus & 2) > 0)
            {
                str += list[i].szTrueName + ",";
                mbNum++;
            }
            if (list[i].dwAccNo == accNo)
            {
                op = " | <a class='click op quit'>退出</a></td>";
                if ((list[i].dwStatus & 2) == 0) isCk = false;
            }
        }
        if (op == "")
        {
            op = " | <a class='click op join'>加入</a></td>";
        }
        if (isMyself) op = "";
        if (list.Length > 0&&isCk)
        {
            str = str.Substring(0, str.Length - 1);//去掉逗号
            mbList+="<a class='group click' title='"+str+"'>查看成员</a>";
        }
        if (!isCk) { mbList += "<span style='color:grey'>等待批准</span>"; }
        return mbList + op;
    }
    protected string GetdataList()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        TESTDATAREQ vrReq = new TESTDATAREQ();
        vrReq.dwStartDate = (uint)int.Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
        vrReq.dwEndDate = (uint)int.Parse(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        UNIACCOUNT AccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrReq.dwAccNo = AccInfo.dwAccNo;
        vrReq.dwStatus = (uint)UNITESTDATA.DWSTATUS.TDSTAT_UPLOADED | (uint)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED;
        vrReq.szReqExtInfo.szOrderKey = "dwSubmitDate";
        vrReq.szReqExtInfo.szOrderMode = "DESC";
        UNITESTDATA[] vtResult;
        uResponse = m_Request.Account.TestDataGet(vrReq, out vtResult);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            string szErrMsg = "获取失败:" + m_Request.szErrMessage;
            return szErrMsg;
        }
        else
        {
            string szHtml = "";
            if (vtResult.Length == 0)
            {
                szHtml = "<tr><td colspan='6' style='padding:10px;width:100%;text-align:center;'>无上传记录,列表为空</td></tr>";
            }
            else
            {
                szHtml = "";
                for (int i = 0; i < vtResult.Length; i++)
                {
                    uint dwSubmitDate = (uint)vtResult[i].dwSubmitDate;
                    string szDate = (dwSubmitDate / 10000) + "-" + ((dwSubmitDate / 100) % 100) + "-" + (dwSubmitDate % 100) + " " + Get1970Time((uint)vtResult[i].dwSubmitTime);
                    if (i % 2 == 0)
                        szHtml += "<tr class='odd'>";
                    else
                        szHtml += "<tr>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szDisplayName.ToString() + "</td>";
                    szHtml += "<td class='tbltd'>" + szDate + "</td>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szDevName.ToString() + "</td>";
                    szHtml += "<td class='tbltd'>" + ConvertSize(vtResult[i].dwFileSize) + "</td>";
                    szHtml += "<td class='tbltd status' stat='" + vtResult[i].dwStatus + "'>" + ConvertSta(vtResult[i].dwStatus) + "</td>";
                    szHtml += "<td class='tbltd'><a class='click' onclick='window.open(\"dlData.aspx?id=" + vtResult[i].dwSID + "\");setTimeout(\"uni.tab.reload();\",500);'>下载</a>|" +
                        "<a class='click' onclick='delAct(\"del_data\",\"" + vtResult[i].dwSID.ToString() + "\")'>删除</a></td>";
                    szHtml += "</tr>";
                }
            }
            return szHtml;
        }
    }


    private string ConvertSta(object p)
    {
        if (Convert.ToInt32(p) == (int)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED)
        {
            return "<span class='green'>已下载</span>";
        }
        if (Convert.ToInt32(p) == (int)UNITESTDATA.DWSTATUS.TDSTAT_FILEDEL)
        {
            return "<span class='red'>已删除</span>";
        }
        else
        {
            return "<span class='green'>未下载</span>";
        }
    }
    private string ConvertSize(object p)
    {
        int k = Convert.ToInt32(p) / 1024;
        if (k == 0)
        {
            k = 1;
        }
        return k.ToString() + "K";
    }
    private string GetDevModel(string szDevID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        DEVREQ vrReq = new DEVREQ();
        vrReq.dwDevID = new UniDW(uint.Parse(szDevID));
        UNIDEVICE[] vtRes;
        uResponse = m_Request.Device.Get(vrReq, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes[0].szModel.ToString();
        }
        return "";
    }
    private string Get1970Time(uint TotalSeconds)//根据差距秒数 算出现在是时间
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("HH:mm");
    }
    protected void DateSubmit_Click(object sender, EventArgs e)
    {
        uint start = ToDate(iptDateStart.Value);
        uint end = ToDate(iptDateEnd.Value);
        InitResv(start, end);
    }
    string ToNavDev(string name, uint? id)
    {
        return "<a class='click' target='_blank' href='DevDetail.aspx?dev=" + id + "'>" + name + "</a>";
    }
}