using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public partial class DevWeb_Ajax_Code_resvForm : UniClientPage
{
    string myTutor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        string act = Request["act"];
        if (act == "form")
        {
            string rtId = ConvertStr(Request["rtSel"]);
            string szlabName = ConvertStr(Request["labName"]);
            //string szlabMan = ConvertStr(Request["labMan"]);
            //string szlabManId = ConvertStr(Request["labManId"]);
            string BeginDate = ConvertStr(Request["beginDate"]);
            if (DateTime.Parse(BeginDate) <= DateTime.Now)
            {
                //BeginDate = DateTime.Now.AddMinutes(1).ToString();
                Response.Write("{\"ret\":0,\"msg\":\"开始时间不得早于当前时间\"}");
                return;
            }
            string EndDate = ConvertStr(Request["endDate"]);
            //EndDate = DateTime.Parse(EndDate).AddDays(1).AddSeconds(-1).ToString();

            uint isCheck = ConvertStr(Request["check"]) == "1" ? (uint)UNIRESERVE.DWPROPERTY.RESVPROP_MANDO : (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
            uint isMat = ConvertStr(Request["selMat"]) == "1" ? (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFCONSUMABLE : 0;
            string szMemo = ConvertStr(Request["memo"]);
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESV setResv = new RTRESV();
            setResv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;

            if (rtId == "" || rtId == "0")//个人实验
            {
                Response.Write("{\"ret\":0,\"msg\":\"系统只支持有课题的实验\"}");
                return;//暂不考虑个人实验
            }
            setResv.dwRTID = Convert.ToUInt32(rtId);

            setResv.szTestName = szlabName;

            setResv.dwBeginTime = (uint?)Get1970Seconds(BeginDate);
            setResv.dwEndTime = (uint?)Get1970Seconds(EndDate);

            setResv.dwProperty = isCheck | isMat;
            setResv.szMemo = szMemo;
            if (!string.IsNullOrEmpty(Request["sample"]))//样本
            {
                //setResv.dwSampleNum = Convert.ToUInt32(Request["sample"]);
            }
            else
            {
                //setResv.dwSampleNum = 0;
            }
            UNIACCOUNT setAcc = new UNIACCOUNT();
            if (Session["LOGIN_ACCINFO"] != null && Session["CUR_DEV"] != null)
            {
                setAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                setResv.dwOwner = setAcc.dwAccNo;
                setResv.szOwnerName = setAcc.szTrueName.ToString();

                //浙大农学院
                if (setAcc.dwTutorID != null && setAcc.dwTutorID != 0)
                {
                    setResv.dwHolderID = setAcc.dwTutorID;
                    setResv.szHolderName = setAcc.szTutorName;
                }
                //RESEARCHTEST[] rts = GetRTestes(rtId);
                //if (rts != null && rts.Length > 0)
                //{
                //    setResv.dwLeaderID = rts[0].dwLeaderID;
                //    setResv.szLeaderName = rts[0].szLeaderName;
                //    setResv.dwHolderID = rts[0].dwHolderID;
                //    setResv.szHolderName = rts[0].szHolderName;
                //}
                //else
                //{
                //    Response.Write("{\"ret\":0,\"msg\":\"获取项目信息失败\"}");
                //    return;
                //}
                ////////
                UNIDEVICE setDev = (UNIDEVICE)Session["CUR_DEV"];
                setResv.dwLabID = setDev.dwLabID;
                setResv.dwDevID = setDev.dwDevID;
                setResv.dwDevKind = setDev.dwKindID;
                setResv.dwManID = setDev.dwAttendantID;
                setResv.szManName = setDev.szAttendantName;
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"登录超时，请重新登录！\"}");
                return;
            }
            setResv.dwPurpose = 2 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH;
            uResponse = m_Request.Reserve.SetRTResv(setResv, out setResv);
            Response.ContentType = "application/Json";

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                //默认加入课题成员组
                RESEARCHTESTREQ vrRt = new RESEARCHTESTREQ();
                vrRt.dwRTID = setResv.dwRTID;
                RESEARCHTEST[] vtRst;
                m_Request.Reserve.GetResearchTest(vrRt, out vtRst);
                if (vtRst != null && vtRst.Length > 0)
                {
                    AddMemByAccNo(vtRst[0].dwGroupID.ToString(), setAcc.dwAccNo.ToString());
                }
                Response.Write("{\"ret\":1}");
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMsg + "\"}");
            }
        }
        else if (act == "get")
        {
            //费用
            uint useFee = 0;
            uint useFeeUnit = 0;
            uint sampleFee = 0;
            uint sampleFeeUnit = 0;
            uint subFee = 0;
            uint subFeeUnit = 0;
            string resvStat = "";
            if (Session["CUR_DEV"] != null && Session["LOGIN_ACCINFO"] != null)
            {
                UNIDEVICE dev = (UNIDEVICE)Session["CUR_DEV"];
                //浙大无需获取费用

                //FEEREQ vrGet = new FEEREQ();
                //vrGet.dwDevKind = dev.dwKindID;
                //UNIFEE[] vrResult;
                //if (m_Request.Fee.Get(vrGet, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length == 1)
                //{
                //    FEEDETAIL[] fees= vrResult[0].szFeeDetail;
                //    for (int i = 0; i < fees.Length; i++)
                //    {
                //        if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                //        {
                //            useFee = (uint)fees[i].dwUnitFee;
                //            useFeeUnit=(uint)fees[i].dwUnitTime;
                //        }
                //        else if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                //        {
                //            sampleFee = (uint)fees[i].dwUnitFee;
                //            sampleFeeUnit = (uint)fees[i].dwUnitTime;
                //        }
                //        else if (fees[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                //        {
                //            subFee = (uint)fees[i].dwUnitFee;
                //            subFeeUnit = (uint)fees[i].dwUnitTime;
                //        }
                //    }
                //}
                //else
                //{
                //    Response.Write("{\"ret\":0,\"msg\":\"获取费用出错，请尝试重新登录！\"}");
                //    return;
                //}

                //预约情况
                if (!string.IsNullOrEmpty(Request["date"]))
                {
                    string date = DateTime.Parse(Request["date"]).ToString("yyyyMMdd");
                    DEVRESVSTATREQ resvGet = new DEVRESVSTATREQ();
                    resvGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
                    resvGet.dwDevID = dev.dwDevID;
                    resvGet.szDates = date;
                    resvGet.dwResvPurpose =2|(uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH;
                    resvGet.szReqExtInfo.dwStartLine = 0;
                    resvGet.szReqExtInfo.dwNeedLines = 10000;
                    DEVRESVSTAT[] resvRlt;
                    if (m_Request.Device.GetDevResvStat(resvGet, out resvRlt) == REQUESTCODE.EXECUTE_SUCCESS && resvRlt.Length > 0)
                    {
                        string earliest = ToTimeStr(resvRlt[0].szRuleInfo.dwEarliestResvTime);
                        string latest = ToTimeStr(resvRlt[0].szRuleInfo.dwLatestResvTime);
                        string max = ToTimeStr(resvRlt[0].szRuleInfo.dwMaxResvTime);
                        string min = ToTimeStr(resvRlt[0].szRuleInfo.dwMinResvTime);
                        string start = ToTimeStr2(resvRlt[0].szOpenInfo[0].dwBegin);
                        string end = ToTimeStr2(resvRlt[0].szOpenInfo[0].dwEnd);
                        resvStat = "本仪器当日开放时间：<span style='color:red;'>" + start + "</span> 到 <span style='color:red;'>" + end + "</span>；申请预约最长可提前：<span style='color:red;'>" + earliest + "</span>" +
                "至少提前：<span style='color:red;'>" + latest + "</span>；预约时间最长：<span style='color:red;'>" + max + "</span> 最短：<span style='color:red;'>" + min + "</span>。";
                        DEVRESVTIME[] ts = resvRlt[0].szResvInfo;
                        //for (int i = 0; i < ts.Length; i++)
                        //{
                        //    resvStat += "从" + ts[i].dwBegin / 100 + ":" + ts[i].dwBegin % 100 + "到" + ts[i].dwEnd / 100 + ":" + ts[i].dwEnd % 100;
                        //}
                    }
                }
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"登录已超时，请重新登录！\"}");
                return;
            }

            string rtOpt = InitRTestes();
            string manOpt = InitManInfo();
            if (rtOpt == "")//临时 检测获取课题情况
            {
                Response.Write("{\"ret\":0,\"msg\":\"没有获取到课题！\"}");
                return;
            }

            //获取开放时间
            Response.Write("{\"ret\":2,\"resvStat\":\"" + resvStat + "\",\"rtOpt\":\"" + rtOpt + "\",\"manOpt\":\"" + manOpt +
                "\",\"useFee\":\"" + useFee + "\",\"sampleFee\":\"" + sampleFee + "\",\"subFee\":\"" + subFee +
                "\",\"useFeeUnit\":\"" + useFeeUnit + "\",\"sampleFeeUnit\":\"" + sampleFeeUnit + "\",\"subFeeUnit\":\"" + subFeeUnit +
                "\",\"myTutor\":\"" + myTutor + "\"}");
        }
    }

    string ToTimeStr(uint? t)
    {
        if (t == null || t == 0) return "不限制";
        string d = (t / 1440).ToString();
        string h = (t % 1440 / 60).ToString();
        string m = (t % 60).ToString();
        return d + "日" + h + "时" + m + "分";
    }
    string ToTimeStr2(uint? t)
    {
        if (t == null || t == 0) return "不限制";
        string h = (t / 100).ToString();
        if (h.Length == 1) h = "0" + h;
        string m = (t % 100).ToString();
        if (m.Length == 1) m = "0" + m;
        return h + ":" + m;
    }

    private string InitRTestes()
    {
        string optCourse = "";// "<option value='0'>个人实验(无课题)</option>";
        RESEARCHTEST[] opts = GetRTestes(null);
        if (opts != null && opts.Length > 0)
        {
            for (int i = 0; i < opts.Length; i++)
            {
                optCourse += "<option value='" + opts[i].dwRTID + "' tutor='" + opts[i].szHolderName + "' leader='" + opts[i].szLeaderName + "'>" + opts[i].szRTName + "</option>";
            }
        }
        return optCourse;
    }

    private string InitManInfo()
    {
        string optMan = "";
        if (Session["CUR_DEV"] != null)
        {
            UNIDEVICE dev = (UNIDEVICE)Session["CUR_DEV"];
            optMan = "<option value='" + dev.dwAttendantID + "' phone='" + dev.szAttendantTel + "'>" + dev.szAttendantName + "</option>";
            //获取管理员组
            //    GROUPMEMDETAILREQ vrGetMan = new GROUPMEMDETAILREQ();
            //    vrGetMan.dwGroupID = dev.dwManGroupID;
            //    GROUPMEMDETAIL[] vtRst;
            //    if (m_Request.Group.GetGroupMemDetail(vrGetMan, out vtRst) == REQUESTCODE.EXECUTE_SUCCESS && vtRst.Length > 0)
            //    {
            //        for (int i = 0; i < vtRst.Length; i++)
            //        {
            //            optMan += "<option value='" + vtRst[i].dwAccNo + "' phone='" + vtRst[i].szHandPhone + "' email='" + vtRst[i].szEmail + "'>" + vtRst[i].szTrueName + "</option>";
            //        }
            //    }
            return optMan;
        }
        return optMan;
    }


    RESEARCHTEST[] GetRTestes(string id)
    {
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        if (id != null)
        {
            vrGet.dwRTID = Convert.ToUInt32(id);
        }
        else
        {
            vrGet.dwLeaderID = vrAccInfo.dwAccNo;
            if (vrAccInfo.dwTutorID!=null&&vrAccInfo.dwTutorID!=0)
            {

                vrGet.dwHolderID = vrAccInfo.dwTutorID;
            }
            else
            {
                vrGet.dwHolderID = vrAccInfo.dwAccNo;
            }
            //{
            //    TUTORREQ vrPra = new TUTORREQ();
            //    vrPra.dwStudentAccNo = vrAccInfo.dwAccNo;
            //    UNITUTOR[] vrTutor;
            //    if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
            //    {
            //        TUTORSTUDENTREQ vrStuGet = new TUTORSTUDENTREQ();
            //        vrStuGet.dwTutorID = vrTutor[0].dwAccNo;
            //        TUTORSTUDENT[] vrStu;
            //        if (m_Request.Account.TutorStudentGet(vrStuGet, out vrStu) == REQUESTCODE.EXECUTE_SUCCESS && vrStu != null)
            //        {
            //            for (int i = 0; i < vrStu.Length; i++)
            //            {
            //                if (vrStu[i].dwAccNo == vrAccInfo.dwAccNo && vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_TUTOROK)
            //                {
            //                    vrGet.dwHolderID = vrTutor[0].dwAccNo;
            //                    myTutor = "&nbsp&nbsp&nbsp导师：" + vrTutor[0].szTrueName;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
        }
        RESEARCHTEST[] vtResult;
        if (m_Request.Reserve.GetResearchTest(vrGet, out vtResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vtResult.Length == 0)//没有项目则创建
            {
                RESEARCHTEST setvalue = new RESEARCHTEST();
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                string name = vrAccInfo.szTrueName;
                if (vrAccInfo.dwTutorID != null && vrAccInfo.dwTutorID != 0)
                {
                    setvalue.dwHolderID = vrAccInfo.dwTutorID;
                    setvalue.szHolderName = vrAccInfo.szTutorName;
                    name += "_" + vrAccInfo.szTutorName;
                }
                else
                {
                    setvalue.dwHolderID = vrAccInfo.dwAccNo;
                    setvalue.szHolderName = vrAccInfo.szTrueName;
                }
                setvalue.szRTName = name+ "科研课题";
                setvalue.szLeaderName = vrAccInfo.szTrueName;
                setvalue.dwLeaderID = vrAccInfo.dwAccNo;
                setvalue.dwRTLevel = (uint)RESEARCHTEST.DWRTLEVEL.RTLEVEL_OTHER;
                uint groupId = NewGroup(setvalue.szRTName + "项目组", vrAccInfo.szLogonName);
                setvalue.dwGroupID = groupId;
                uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    RESEARCHTEST[] rlt = new RESEARCHTEST[1];
                    rlt[0] = setvalue;
                    return rlt;
                }
            }
        }
        return vtResult;
    }
}