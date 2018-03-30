using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_Default : UniClientPage
{
    uint needLines = 10;
    protected string rUseTimes = "";
    protected string wUseTimes = "";
    protected string tooltip = "";
    private DEVSTAT[] useList;
    private UNIDEVICE[] devList;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        (Master.FindControl("BannerContent") as System.Web.UI.HtmlControls.HtmlGenericControl).InnerHtml = "<div class=\"b-img\"></div>";

        //InitRank();
        InitTooltip();
        InitResv();
        InitCurTest();
        InitNewDevs();
        InitDevByCls();//一定要在InitRank()与InitNewDevs()之后
        InitDevMonthStat();
        InitLab();
    }

    private void InitDevByCls()
    {
        //if (useList == null || devList == null)
        //{
        //    return;
        //}
        int showCount = 4;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        UNIDEVCLS[] vtResult;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string list = "";
            List<string>[] clsstr = new List<string>[vtResult.Length];
            for (int i = 0; i < vtResult.Length; i++) clsstr[i] = new List<string>();

            for (int i = 0; i < devList.Length; i++)
            {
                UNIDEVICE dev = devList[i];
                for (int j = 0; j < vtResult.Length; j++)
                {
                    if (vtResult[j].dwClassID == dev.dwClassID)
                    {
                        if (clsstr[j].Count < showCount)
                        {
                            string s = "<div class='m-box_s'><div class='box_h'><a href=\"DevDetail.aspx?dev=" + dev.dwDevID + "\" class=\"\"><img alt='" + dev.szDevName + "' src='" + GetImgS(dev.dwDevSN) + "'/></a></div><div class='box_c'>" +
                                "<ul><li class='name'>" + CutStrT(dev.szDevName, 12) + "</li><li>方向：" + CutStrT(dev.szLabName, 8) + "</li><li class='f-tar'><a href='DevDetail.aspx?act=achi&dev=" + dev.dwDevID + "'>成果</a> | " +
                                "<a href=\"DevDetail.aspx?act=resv&dev=" + dev.dwDevID + "\" onclick=\"return isloginTu()\"> 预约>> </a>" + "</li></ul>"
                            + "</div></div>";
                            clsstr[j].Add(s);
                        }
                        break;
                    }
                }
            }
            for (int m = 0; m < clsstr.Length; m++)
            {
                //临时 过来掉非设备类别
                if (vtResult[m].dwClassID == 54) continue;//临时 

                list += "<div class='m-box1'><div class='box-h'><span class='click' onclick=\"search('" + vtResult[m].dwClassID + "')\">" + vtResult[m].szClassName + "</span><a class='u-more' style='top: 20px; right: 10px; padding: 5px;'><span onclick=\"search('" + vtResult[m].dwClassID + "')\" >+更多</span></a></div><div class='box-c m-boxes'>";
                for (int i = 0; i < clsstr[m].Count; i++)
                {
                    list += clsstr[m][i];
                }
                list += "</div></div>";
            }
            devByCls.InnerHtml = list;
        }
    }
    private UNIDEVICE GetDevById(uint? id)
    {
        for (int i = 0; i < devList.Length; i++)
        {
            if (devList[i].dwDevID == id)
            {
                return devList[i];
            }
        }
        return new UNIDEVICE();
    }

    private void InitLab()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        LABREQ vrGet = new LABREQ();
        UNILAB[] vtResult;
        uResponse = m_Request.Device.LabGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                if (vtResult[i].dwLabID == 31)
                {
                    continue;
                }
                rel += "<li value='" + vtResult[i].dwLabID + "' class='" + (i % 2 == 0 ? "odd" : "") + "'> ▪ " + vtResult[i].szLabName + "</li>";
            }
            labList.InnerHtml = rel;
        }
    }
    private void InitTooltip()
    {
        if (Session["LOGIN_ACCINFO"] == null)
        {
            return;
        }
        UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((vrAccInfo.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0 || (vrAccInfo.dwIdent & 512) > 0)
        {
            return;
        }
        //获取导师
        TUTORREQ vrPra = new TUTORREQ();
        vrPra.dwStudentAccNo = vrAccInfo.dwAccNo;
        UNITUTOR[] vrTutor;
        if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
        {
            //获取状态
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
                            tooltip = "您已获得导师许可，点击[<a href='DevList.aspx'>预约设备</a>]预约实验。";
                        }
                        else if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)
                        {
                            tooltip = "您未获得预约导师实验的许可。";
                        }
                        else
                        {
                            tooltip = "预约实验，需等待导师审核通过。";
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            tooltip = "预约实验，请先进入[<a href='UserCenter.aspx?act=info'>个人信息</a>]指定导师。";
        }
    }

    private void InitDevMonthStat()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        DEVMONTHSTATREQ vrGet = new DEVMONTHSTATREQ();
        DateTime now = DateTime.Now;
        vrGet.dwStartDate = (uint)(now.Year) * 10000 + 101;
        vrGet.dwEndDate = (uint)(now.Year) * 10000 + 1231;
        DEVMONTHSTAT[] vtResult;
        uResponse = m_Request.Report.GetDevMonthStat(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                DEVMONTHSTAT stat = vtResult[i];
                float w = (float)stat.dwWResvTime / 60;
                float r = (float)stat.dwRResvTime / 60;
                wUseTimes += Math.Round(w, 1).ToString() + ",";
                rUseTimes += Math.Round(r, 1).ToString() + ",";
            }
            wUseTimes = wUseTimes.Substring(0, wUseTimes.Length - 1);
            rUseTimes = rUseTimes.Substring(0, rUseTimes.Length - 1);
        }
    }

    private void InitNewDevs()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        DEVREQ vrGet = new DEVREQ();
        vrGet.szReqExtInfo.szOrderKey = "dwPurchaseDate";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        UNIDEVICE[] vtResult;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            devList = vtResult;
            string list = "";
            string imgList = "";
            int max = 7;
            for (int i = 0; i < vtResult.Length && i < max; i++)
            {
                //临时 过来掉非设备类别
                if (vtResult[i].dwClassID == 54) { max++; continue; }//临时 
                UNIDEVICE dev = vtResult[i];
                list += "<li value='" + dev.dwDevID + "'><a href='DevDetail.aspx?dev=" + dev.dwDevID + "'> ▪ " + CutStrT(dev.szDevName, 14) + "</a></li>";
                imgList += "<img class='u-newdev' id='" + dev.dwDevID + "' alt='" + dev.szDevName + "' src='" + GetImgS(dev.dwDevID) + "'/>";
            }
            newDevs.InnerHtml = list;
            devImg.InnerHtml = imgList;
        }
    }

    private void InitRank()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        REPORTREQ vrGet = new REPORTREQ();
        vrGet.dwGetType = (int)REPORTREQ.DWGETTYPE.USERECGET_BYALL;
        DateTime now = DateTime.Now;
        DateTime m1 = new DateTime(now.Year, now.Month, 1);
        DateTime m2 = m1.AddMonths(1).AddDays(-1);
        vrGet.dwStartDate = Convert.ToUInt32(m1.ToString("yyyyMMdd"));
        vrGet.dwEndDate = Convert.ToUInt32(m2.ToString("yyyyMMdd"));
        //vrGet.szReqExtInfo.dwNeedLines = needLines;
        //vrGet.szReqExtInfo.dwStartLine = 0;
        vrGet.szReqExtInfo.szOrderKey = "dwTotalUseTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        DEVSTAT[] vtResult;
        //获取月统计
        uResponse = m_Request.Report.GetDevStat(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length && i < needLines; i++)
            {
                DEVSTAT item = vtResult[i];
                list += i % 2 == 0 ? "<tr class='odd'>" : "<tr>";
                list += "<td>" + CutStrT(item.szDevName, 10) + "</td><td>" + item.dwUseTimes + "</td><td>" + item.dwTotalUseTime + " 分钟</td></tr>";
            }
            moonRank.InnerHtml = list;
        }
        //获取年统计
        int y = (now.Year) * 10000 + 101;
        vrGet.dwStartDate = (uint)y;
        vrGet.dwEndDate = (uint)(y + 10000);
        uResponse = m_Request.Report.GetDevStat(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            useList = vtResult;
            string list = "";
            for (int i = 0; i < vtResult.Length && i < needLines; i++)
            {
                DEVSTAT item = vtResult[i];
                list += i % 2 == 0 ? "<tr class='odd'>" : "<tr>";
                list += "<td>" + CutStrT(item.szDevName, 10) + "</td><td>" + item.dwUseTimes + "</td><td>" + item.dwTotalUseTime + " 分钟</td></tr>";
            }
            yearRank.InnerHtml = list;
        }
    }

    private void InitCurTest()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        DEVREQ vrGet = new DEVREQ();
        vrGet.dwRunStat = (int)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
        vrGet.dwReqProp = (uint)DEVREQ.DWREQPROP.DEVREQ_NEEDDEVUSE;
        //vrGet.szReqExtInfo.dwNeedLines = needLines - 3;
        //vrGet.szReqExtInfo.dwStartLine = 0;
        UNIDEVICE[] vtResult;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                UNIDEVICE dev = vtResult[i];
                DEVUSEINFO[] info = dev.DevUse;
                if (info == null || info.Length == 0) continue;
                list += i % 2 == 0 ? "<tr class='odd'>" : "<tr>";
                list += "<td>" + CutStrT(dev.szDevName, 10) + "</td><td>" + dev.szRoomName + "</td><td>" + info[0].szTrueName + "</td>" +
                "<td>" + dev.szDeptName + "</td><td>" + info[0].szTutorName + "</td><td>" + (Get1970Date((int)info[0].dwBeginTime)).Substring(5) + "</td>" +
                "<td><span style='color:green'>使用中</span></td><td>" + dev.szAttendantName + "</td></tr>";
            }
            curTest.InnerHtml = list;
        }
        else
        {
            MessageBox.Show(this, m_Request.szErrMsg);
        }
    }

    private void InitResv()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        RTRESVREQ vrGet = new RTRESVREQ();
        vrGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        uint intStartTime = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
        uint intEndTime = uint.Parse(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        vrGet.dwBeginDate = intStartTime;
        vrGet.dwEndDate = intEndTime;
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        vrGet.szReqExtInfo.dwNeedLines = needLines;
        vrGet.szReqExtInfo.dwStartLine = 0;
        RTRESV[] vtResult;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                list += i % 2 == 0 ? "<tr class='odd'>" : "<tr>";
                list += "<td>" + resv.szOwnerName + "</td><td>" + CutStrT(resv.szUserDeptName, 10) + "</td><td>" + resv.szHolderName + "</td><td>" + CutStrT(resv.szTestName, 10) + "</td><td>" + CutStrT(resv.szDevName, 10) +
                    "</td><td style='width:181px;'>" + Get1970Date((int)resv.dwBeginTime).Substring(5) + "--" + Get1970Date((int)resv.dwEndTime).Substring(5) + "</td>" +
                    "<td>" + Get1970Date((int)resv.dwOccurTime).Substring(5) + "</td><td style='width:76px;'>" + Converter.ResvStatusConverter(resv.dwStatus) + "</td><td>" + GetAtt(resv.dwDevID) + "</td></tr>";
            }
            newResv.InnerHtml = list;
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }

    private string GetAtt(uint? devId)
    {
        DEVREQ devReq = new DEVREQ();
        devReq.dwDevID = devId;
        UNIDEVICE[] devs;
        m_Request.Device.Get(devReq, out devs);
        if (devs != null && devs.Length > 0)
        {
            UNIDEVICE dev = devs[0];
            return dev.szAttendantName;
        }
        return "";
    }
    private UNIACCOUNT GetAcc(uint? accNo)
    {
        ACCREQ req = new ACCREQ();
        req.dwAccNo = accNo;
        UNIACCOUNT[] rlt;
        m_Request.Account.Get(req, out rlt);
        if (rlt != null && rlt.Length > 0)
        {
            return rlt[0];
        }
        return new UNIACCOUNT();
    }
}