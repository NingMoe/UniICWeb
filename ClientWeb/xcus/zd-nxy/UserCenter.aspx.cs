using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;

public partial class UserCenter : UniClientPage
{
    protected string resvList = "";
    protected string memList = "";
    //protected string recordList = "";
    //protected string statList = "";
    protected string applyList = "";
    protected string dataList = "";
    protected string rtList = "";
    protected string isTutor = "table-row";
    protected string accPhone = "";
    protected string accEmail = "";
    protected string tutorName = "";
    protected string tutorckSta = "";
    protected string tutorAcc = "";
    UNIACCOUNT curAcc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] == null || !IsLogined())
        {
            Response.Redirect("Default.aspx");
        }
        base.LoadPage();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        RTRESVREQ vrGet = new RTRESVREQ();
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        curAcc = vrAccInfo;
        accName.InnerText = vrAccInfo.szTrueName;
        accLgName.InnerText = vrAccInfo.szLogonName;
        accColl.InnerText = vrAccInfo.szDeptName;
        accPhone = vrAccInfo.szHandPhone;
        accEmail = vrAccInfo.szEmail;

        //获取导师
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
                            tutorckSta = "(导师已批准)";
                        }
                        else if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)
                        {
                            tutorckSta = "(导师未批准)";
                        }
                        else
                        {
                            tutorckSta = "(导师未审核)";
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


        if ((vrAccInfo.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
        {
            isTutor = "none";
            //GetMemList(vrAccInfo.dwAccNo);
        }
        //vrGet.dwMAccNo = vrAccInfo.dwAccNo;
        uint? acc = vrAccInfo.dwAccNo;
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        uint intStartTime = uint.Parse(DateTime.Now.AddMonths(-6).ToString("yyyyMMdd"));
        uint intEndTime = uint.Parse(DateTime.Now.AddMonths(6).ToString("yyyyMMdd"));
        vrGet.dwMAccNo = acc;
        vrGet.dwBeginDate = intStartTime;
        vrGet.dwEndDate = intEndTime;
        vrGet.szReqExtInfo.szOrderKey = "dwOwner";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            //int use_time = 0;
            //int prepay = 0;
            //int realpay = 0;
            //int times = 0;
            //int all_use_time = 0;
            //int all_prepay = 0;
            //int all_realpay = 0;
            //int all_times = 0;
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                string rsv = "";
                rsv += "<tr><td  style='text-align:left;'>" + resv.szDevName + "</td><td>" + resv.szOwnerName + "</td><td class='td_lab'  style='text-align:left;'><span style='font-weight:600;color:#555'>实验名称：</span>" + resv.szTestName + "<br /><span style='font-weight:600;color:#555'>仪器管理员：</span>" + resv.szManName + "</td>";
                rsv += "<td>" + resv.szHolderName + "</td>";
                string beginTime = Get1970Date((int)resv.dwBeginTime);
                string endTime = Get1970Date((int)resv.dwEndTime);
                rsv += "<td><div><span style='font-weight:600;color:#555'>开始：</span>" + beginTime + " </div><div><span style='font-weight:600;color:#555'>结束：</span>" + endTime + "</div></td>";
                if ((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) == 0)
                //{
                //    //统计
                //    use_time += (int)resv.dwRealUseTime;
                //    prepay += (int)resv.dwUseFee;
                //    realpay += (int)resv.dwRealCost;
                //    times++;
                //    all_use_time += (int)resv.dwRealUseTime;
                //    all_prepay += (int)resv.dwUseFee;
                //    all_realpay += (int)resv.dwRealCost;
                //    all_times++;
                //    if (i == vtResult.Length - 1 || resv.dwOwner != vtResult[i + 1].dwOwner)
                //    {
                //        statList += "<tr><td>" + resv.szOwnerName + "</td><td>" + times + "</td><td>" + calc((uint)use_time) + "</td><td>" + prepay / 100 + " 元</td><td>" + realpay / 100 + " 元</td></tr>";
                //        use_time = 0;
                //        prepay = 0;
                //        realpay = 0;
                //        times = 0;
                //    }
                //    //
                //    rsv += "<td>" +calc(resv.dwRealUseTime) + "</td><td>" + resv.dwUseFee / 100 + " 元</td><td>" + resv.dwRealCost / 100 + " 元</td></tr>";
                //    recordList += rsv;
                //}
                //else
                {
                    DateTime start = DateTime.Parse(beginTime);
                    rsv += "<td style='width: 120px;' class='rsv_stat' stat='" + resv.dwStatus + "'>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>";

                    string act = "";
                    if (resv.dwOwner == acc)
                    {
                        if (((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0) && (start > DateTime.Now))
                            act += "<a class='click delResv' onclick='delAct(\"del_rt_resv\",\"" + resv.dwResvID + "\");'>删除</a>";
                        if (((resv.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0) && (start < DateTime.Now))
                            act += "<a class='click' onclick='pro.j.rsv.finish(\"" + resv.dwResvID + "\");'>提前结束</a>";
                    }
                    rsv += "<td>" + act + "</td></tr>";
                    resvList += rsv;
                }
            }
            //statList += "<tr style='font-weight:bold;'><td>合计</td><td>" + all_times + "</td><td>" + calc((uint)all_use_time) + "</td><td>" + all_prepay / 100 + " 元</td><td>" + all_realpay / 100 + " 元</td></tr>";
        }
        //实验数据
        //dataList = GetdataList();
        //课题列表
        GetrtList();
        //成果列表
        GetAchi();
    }

    private void GetAchi()
    {
        REWARDRECREQ req = new REWARDRECREQ();
        if ((curAcc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
        {
            req.dwHolderID = curAcc.dwAccNo;
        }
        else
        {
            req.dwOpID = curAcc.dwAccNo;
        }
        req.dwStartDate = 0;
        req.dwEndDate = 20990909;
        req.dwReqProp = (uint)REWARDRECREQ.DWREQPROP.RRREQ_NEEDDEV;
        REWARDREC[] rlt;
        if (m_Request.Device.RewardRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                REWARDREC rec = rlt[i];
                REWARDUSEDEV[] devs = rec.UseDev;
                if (devs.Length == 0) continue;
                string tr = "<tr><td>" + rec.szRewardName + "</td>" +
                                                "<td>" + rec.szMemberNames + "</td>" +
                                                "<td>" + rec.szAuthOrg + "</td>" +
                                                "<td>" + ConvertLevel(rec.dwRewardLevel) + "</td>";
                string detail = "<td>"+rec.szOpName+"</td><td><a onclick='delAchi(" + rec.dwRewardID + ")'>删除</a> | <a href='AchiDetail.aspx?dev=" + devs[0].dwDevID + "&achi=" + rec.dwRewardID + "' target='_blank'>详细</a></td></tr>";
                if ((rec.dwRewardKind & (uint)REWARDREC.DWREWARDKIND.REKIND_PRIZE) > 0)
                    prize.InnerHtml += tr + detail;
                else if ((rec.dwRewardKind & (uint)REWARDREC.DWREWARDKIND.REKIND_THESISISSUE) > 0)
                {
                    thesis.InnerHtml += tr + "<td>" + rec.szExtInfo + "</td>" + detail;
                }
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    public string ConvertLevel(uint? level)
    {
        if (level == 1) return "SCI";
        if (level == 2) return "国家级";
        if (level == 3) return "省部级";
        if (level == 101) return "国家级";
        if (level == 102) return "省部级";
        if (level == 103) return "院校级";
        return "其它";
    }
    private void GetrtList()
    {
        //获取课题列表
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((acc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
        {
            return;//导师不显示
        }
        if (Session["TutorInfo"] == null || ((TUTORSTUDENT)Session["TutorInfo"]).dwStatus != (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)
        {
            rtList = "<td colspan='6'>未获得导师课题授权，请指定导师并审核通过。</td>";
            return;//未获得导师批准
        }
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        RESEARCHTEST[] vrResult;
        vrGet.dwHolderID = ((TUTORSTUDENT)Session["TutorInfo"]).dwTutorID;
        uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (i % 2 == 0)
                {
                    rtList += "<tr id='" + vrResult[i].dwRTID + "' class='odd'>";
                }
                else
                {
                    rtList += "<tr id='" + vrResult[i].dwRTID + "' >";
                }
                rtList += "<td style='text-align:left;' ><input type='hidden' class='courseId' value='" + vrResult[i].dwRTID + "'/>" + vrResult[i].szRTName + "</td><td >" + vrResult[i].szHolderName + "</td><td>" + ConvertToZero(vrResult[i].dwTestTimes) + "</td><td>"
                    + ConvertToZero(vrResult[i].dwTestMinutes) + "</td><td><input type='hidden' class='rtGroupId' value='" + vrResult[i].dwGroupID + "'/>" + vrResult[i].dwGroupUsers + "</td>" + GroupOperate(vrResult[i].RTMembers, acc.dwAccNo, vrResult[i].dwGroupID) + "</tr>";
            }
        }
        else
        {
            Util.MessageBox.Show(this, m_Request.szErrMsg);
            return;
        }
    }
    private string GroupOperate(RTMEMBER[] list, uint? accNo, uint? groupId)
    {
        string str = "<td class='operate'  groupid='" + groupId + "'><a class='group click' title='";
        string op = "";
        for (int i = 0; i < list.Length; i++)
        {
            str += list[i].szTrueName + ",";
            if (list[i].dwAccNo == accNo)
            {
                op = "<a class='click op quit'>退出</a></td>";
            }
        }
        if (op == "")
        {
            op = "<a class='click op join'>加入</a></td>";
        }
        if (list.Length > 0)
        {
            str = str.Substring(0, str.Length - 1);//去掉逗号
        }
        str += "'>查看成员</a>|";
        return str + op;
    }
    protected string GetdataList()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        TESTDATAREQ vrReq = new TESTDATAREQ();
        vrReq.dwStartDate = (uint)int.Parse(DateTime.Now.AddMonths(-6).ToString("yyyyMMdd"));
        vrReq.dwEndDate = (uint)int.Parse(DateTime.Now.AddMonths(1).ToString("yyyyMMdd"));
        UNIACCOUNT AccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //vrReq.dwAccNo = 256491;
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
                szHtml = "<tr><td colspan='10' style='padding:10px;width:100%;text-align:center;'>无上传记录,列表为空</td></tr>";
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
                    szHtml += "<td class='tbltd'>" + vtResult[i].szTrueName + "</td>";
                    szHtml += "<td class='tbltd'>" + szDate + "</td>";
                    szHtml += "<td class='tbltd'>" + vtResult[i].szDevName.ToString() + "</td>";
                    szHtml += "<td class='tbltd'>" + ConvertSize(vtResult[i].dwFileSize) + "</td>";
                    szHtml += "<td class='tbltd status' stat='" + vtResult[i].dwStatus + "'>" + ConvertSta(vtResult[i].dwStatus) + "</td>";
                    szHtml += "<td class='tbltd'><a class='click' onclick='window.open(\"dlData.aspx?id=" + vtResult[i].dwSID.ToString() + "\");setTimeout(\"location.href=location.href;\",500);'>下载</a> | " +
                        "<a class='click' onclick='delAct(\"del_data\",\"" + vtResult[i].dwSID.ToString() + "\")'>删除</a></td>";
                    szHtml += "</tr>";
                }
            }
            return szHtml;
        }
    }


    private string ConvertSta(object p)
    {
        if ((Convert.ToInt32(p) & (int)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED) > 0)
        {
            return "<span class='green'>已下载</span>";
        }
        if ((Convert.ToInt32(p) & (int)UNITESTDATA.DWSTATUS.TDSTAT_FILEDEL) > 0)
        {
            return "<span class='red'>已删除</span>";
        }
        else
        {
            return "<span class='orange'>未下载</span>";
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
    string ConvertToZero(object obj)
    {
        if (obj == null || obj.ToString() == "")
        {
            return "0";
        }
        else
        {
            return obj.ToString();
        }
    }
    string calc(uint? t)
    {
        string str="";
        uint? h = t / 60;
        uint? m = t % 60;
        if (h > 0) str += h + "小时";
        if(m>0)str+=m + "分钟";
        return str; 
    }
}