using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Model;
using BLL;
using Util;
using System.Text.RegularExpressions;

public partial class ClientWeb_Default : UniClientPage
{
    uint needLines = 7;
    protected string rUseTimes = "";
    protected string wUseTimes = "";
    protected string tooltip = "";
    protected string slideImgs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        urlLogin();
        InitTooltip("rtest");
        InitCampus();
        InitResv();
        InitCurTest();
        InitBreach();
        InitRank();
        InitNewDevs();
        InitDevMonthStat();
        InitNews();
        InitSlide();
        //InitStat();
    }

    private bool urlLogin()
    {
        string szUrl = Request.Url.ToString();
        if (!string.IsNullOrEmpty(szUrl)&&szUrl.IndexOf("verify")>0)
        {
            string szPassword = "";
            string szLogonName = "";
            if (IsCheckLogin(szUrl, out szLogonName, out szPassword))
            {
                if (common.Login(szLogonName, szPassword))
                {
                    UNIACCOUNT curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    if (curAcc.szEmail.ToString().Trim() == "" || curAcc.szHandPhone.ToString().Trim() == "")
                    {
                        MsgBox("新用户请先激活！");
                        common.ClearLogin();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void InitNews()
    {
    }

    private void InitSlide()
    {
        ArticleBLL abll = new ArticleBLL();
        List<InfoItem> arts = abll.GetArticlesByClass(10003, 1, 5, 2);
        for (int i = 0; i < arts.Count; i++)
        {
            string con = arts[i].Content;
            MatchCollection matchs = Regex.Matches(con,
                "<IMG.*?src=\"(.*?.*?)\".*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (matchs.Count>0)
            {
                if (matchs[0].Groups.Count > 1)
                {
                    slideImgs += "<li><a href='ArticleList.aspx?gr="+arts[i].Infogroup+"&art="+arts[i].Infoid+"' title='"+arts[i].Title+"'><img alt='" + arts[i].Title + "' src='" + matchs[0].Groups[1] + "' /></a></li>";
                }
            }
        }
    }
    private void InitTooltip(string type)
    {
        if (Session["LOGIN_ACCINFO"] == null)
        {
            return;
        }
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if (type == "tutor")
        {
            if ((acc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
            {
                return;
            }
            //获取导师
            TUTORREQ vrPra = new TUTORREQ();
            vrPra.dwStudentAccNo = acc.dwAccNo;
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
                        if (vrStu[i].dwAccNo == acc.dwAccNo)
                        {
                            Session["TutorInfo"] = vrStu[i];
                            if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)
                            {
                                tooltip = "您已获得导师许可，点击[<a href='DevList.aspx'>预约设备</a>]预约实验。";
                            }
                            else if (vrStu[i].dwStatus == (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)
                            {
                                tooltip = "您未获得预约导师项目实验的许可。";
                            }
                            else
                            {
                                tooltip = "预约项目实验，需等待导师审核通过。";
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                tooltip = "预约项目实验，请先进入[<a href='UserCenter.aspx?tab=4'>个人信息</a>]指定导师。";
            }
        }
        else if (type == "rtest")
        {
            string msg;
            RESEARCHTEST[] rlt = GetrtList("rtest", out msg);
            if (rlt != null && rlt.Length > 0)
            {
                //临时方法，查询成员状态
                for (int i = 0; i < rlt.Length; i++)
                {
                    RTMEMBER[] mbs = rlt[i].RTMembers;
                    for (int j = 0; j < mbs.Length; j++)
                    {
                        if (mbs[j].dwAccNo == acc.dwAccNo && ((mbs[j].dwStatus & 2) > 0))
                        {
                            tooltip = "您好，点击[<a href='DevList.aspx'>预约设备</a>]预约实验。";
                            return;
                        }
                    }
                }
            }
            tooltip = "您还没有成功参与任何项目，点击[<a href='UserCenter.aspx?tab=2'>我的项目</a>]查看。";
        }
    }

    private void InitDevMonthStat()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        DEVMONTHSTATREQ vrGet = new DEVMONTHSTATREQ();
        DateTime now = DateTime.Now;
        vrGet.dwStartDate = (uint)(now.Year) * 10000 + 101;
        vrGet.dwEndDate = (uint)(now.Year) * 10000 + 1231;
        DEVMONTHSTAT[] vtResult = new DEVMONTHSTAT[12];
        uResponse = m_Request.Report.GetDevMonthStat(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            DEVMONTHSTAT[] list = new DEVMONTHSTAT[12];
            for (int i = 0; i < vtResult.Length; i++)
            {
                int num = ((int)vtResult[i].dwYearMonth % 100) - 1;
                list[num] = vtResult[i];
            }
            int c = DateTime.Now.Month;
            for (int i = 0; i < c && i < list.Length; i++)
            {
                if (i < c && i<vtResult.Length)
                {
                    DEVMONTHSTAT stat = vtResult[i];
                    float w = (float)stat.dwWResvTime / 60;
                    float r = (float)stat.dwRResvTime / 60;
                    wUseTimes += Math.Round(w, 1).ToString() + ",";
                    rUseTimes += Math.Round(r, 1).ToString() + ",";
                }
                else
                {
                    wUseTimes += "0,";
                    rUseTimes += "0,";
                }
            }
            if (wUseTimes != "")
            {
                wUseTimes = wUseTimes.Substring(0, wUseTimes.Length - 1);
            }
            if (rUseTimes != "")
            {
                rUseTimes = rUseTimes.Substring(0, rUseTimes.Length - 1);
            }
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
            string list = "";
            string head = "";
            string items = "";
            string imgList = "";
            int line = Convert.ToInt32(DateTime.Now.AddMonths(-18).ToString("yyyyMMdd"));//筛选1年半内的设备
            int n = 0;
            int m = 0;
            for (int i = 0; i < vtResult.Length; i++)
            {
                UNIDEVICE dev = vtResult[i];
                uint date = (uint)dev.dwPurchaseDate;
                if (date < line)
                {
                    continue;
                }
                list += "<li value='" + dev.dwDevID + "'><a href='DevDetail.aspx?dev=" + dev.dwDevID + "'> ▪ " + CutStrT(dev.szDevName, 11) + "</a></li>";
                imgList += "<img id='" + dev.dwDevID + "' alt='" + dev.szDevName + "' src='" + GetImg(dev.dwDevSN) + "'/>";
                n++;
                if (n != 0 && (n % needLines) == 0)
                {
                    m++;
                    head += "<li><a>" + m + "</a></li>";
                    items += "<div class='item'><ul>" + list + "</ul></div>";
                    list = "";
                }
            }
            if (list != "")
            {
                m++;
                head += "<li><a>" + m + "</a></li>";
                items += "<div class='item'><ul>" + list + "</ul></div>";
            }
            newDevs.InnerHtml = "<div class='tabs ist'><ul class='tab_head'>" + head + "</ul><div class='tab_con'>" + items + "</div></div>";
            devImg.InnerHtml = imgList;
        }
    }

    private void InitRank()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        REPORTREQ vrGet = new REPORTREQ();
        vrGet.dwGetType = (int)REPORTREQ.DWGETTYPE.USERECGET_BYALL;
        //vrGet.dwPurpose = (int)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH;
        DateTime now = DateTime.Now;
        DateTime m1 = new DateTime(now.Year, now.Month, 1);
        DateTime m2 = m1.AddMonths(1).AddDays(-1);
        vrGet.dwStartDate = Convert.ToUInt32(m1.ToString("yyyyMMdd"));
        vrGet.dwEndDate = Convert.ToUInt32(m2.ToString("yyyyMMdd"));
        vrGet.szReqExtInfo.dwNeedLines = 10;
        vrGet.szReqExtInfo.dwStartLine = 0;
        vrGet.szReqExtInfo.szOrderKey = "dwTotalUseTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        DEVSTAT[] vtResult;
        //获取月统计
        uResponse = m_Request.Report.GetDevStat(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                DEVSTAT item = vtResult[i];
                list += "<tr class='"+(i%2==0?"even":"odd")+"'><td style='text-align:left;padding-left:2px;'>" +ToNavDev(  CutStrT(item.szDevName, 8) ,item.dwDevID)+ "</td><td style='text-align:left;padding-left:2px;'>" + CutStrT(item.szDeptName, 8) + "</td><td>" + item.dwUseTimes + "</td><td>" + MinToHour(item.dwTotalUseTime) + "</td></tr>";
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
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                DEVSTAT item = vtResult[i];
                list += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + "'><td style='text-align:left;padding-left:2px;'>" + ToNavDev(CutStrT(item.szDevName, 8), item.dwDevID) + "</td><td style='text-align:left;padding-left:2px;'>" + CutStrT(item.szDeptName, 8) + "</td><td>" + item.dwUseTimes + "</td><td>" + MinToHour(item.dwTotalUseTime) + "</td></tr>";
            }
            yearRank.InnerHtml = list;
        }
    }

    private void InitBreach()
    {
        //REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        //DISCIRECREQ vrGet = new DISCIRECREQ();
        //vrGet.dwGetType = (int)DISCIRECREQ.DWGETTYPE.DISCIRECGET_BYALL;
        //uint intStartTime = uint.Parse(DateTime.Now.AddMonths(-10).ToString("yyyyMMdd"));
        //uint intEndTime = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
        //vrGet.dwStartDate = intStartTime;
        //vrGet.dwEndDate = intEndTime;
        //vrGet.szReqExtInfo.dwNeedLines = needLines;
        //vrGet.szReqExtInfo.dwStartLine = 0;
        //DISCIREC[] vtResult;
        //uResponse = m_Request.Account.DisciRecGet(vrGet, out vtResult);
        //if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        //    string list = "";
        //    for (int i = 0; i < vtResult.Length; i++)
        //    {
        //        DISCIREC dis = vtResult[i];
        //        list += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + "'><td>" + dis.szTrueName + "</td><td>" + ToNavDev(dis.szDevName, dis.dwDevID) + "</td><td><span class='red'>" + dis.szDisciName + "</span></td>" +
        //            "<td>" + Get1970Date((int)dis.dwDisciTime) + "</td></tr>";
        //    }
        //    Breach.InnerHtml = list;
        //}
    }

    private void InitCurTest()
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        DEVREQ vrGet = new DEVREQ();
        vrGet.dwRunStat = (int)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
        vrGet.szReqExtInfo.dwNeedLines = needLines;
        vrGet.szReqExtInfo.dwStartLine = 0;
        UNIDEVICE[] vtResult;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                UNIDEVICE dev = vtResult[i];
                list += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + "'><td>" + ToNavDev(CutStrT(dev.szDevName, 10), dev.dwDevID) + "</td><td>" + CutStrT(dev.szDeptName, 8) + "</td>" +
                "<td>" + CutStrT(dev.szLabName, 8) + "</td><td>" + dev.DevUse[0].szTrueName + "</td><td>" + Get1970Date((int)dev.DevUse[0].dwBeginTime).Substring(5) + "</td>" +
                "<td><span style='color:green'>使用中</span></td><td>" +CutStrT( dev.szAttendantName,4) + "</td></tr>";
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
        uint intStartTime = uint.Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
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
                list += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + "'><td>" + resv.szOwnerName + "</td><td>" + ToNavDev(CutStrT(resv.szDevName, 14), resv.dwDevID) + "</td><td>" + Get1970Date((int)resv.dwBeginTime).Substring(5) + "--" + Get1970Date((int)resv.dwEndTime).Substring(5) + "</td>" +
                    "<td>" + Get1970Date((int)resv.dwOccurTime).Substring(5) + "</td><td>" + Util.Converter.RsvCheckStaConverter(resv.dwStatus) + "</td><td>" + resv.szManName + "</td></tr>";
            }
            newResv.InnerHtml = list;
        }
        else
        {
            MessageBox.Show(this, m_Request.szErrMsg);
        }
        vrGet.dwCheckStat = (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING;
        uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string list = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                RTRESV resv = vtResult[i];
                list += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + "'><td>" + resv.szOwnerName + "</td><td>" + ToNavDev(CutStrT(resv.szDevName, 16), resv.dwDevID) + "</td><td>" + resv.szCampusName +
                    "</td><td>" + Get1970Date((int)resv.dwBeginTime).Substring(5) + "--" + Get1970Date((int)resv.dwEndTime).Substring(5) + "</td>" +
                    "<td><span style='color:green'>" + Get1970Date((int)resv.dwOccurTime).Substring(5) + "</span></td></tr>";
            }
            unCheckResv.InnerHtml = list;
        }
        else
        {
            MessageBox.Show(this, m_Request.szErrMsg);
        }
    }

    void InitCampus()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CAMPUSREQ vrGet = new CAMPUSREQ();
        UNICAMPUS[] vtResult;
        uResponse = m_Request.Account.CampusGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<li value='" + vtResult[i].dwCampusID + "'> ▪ <a>" + vtResult[i].szCampusName + "</a></li>";
            }
            campusList.InnerHtml = rel;
        }
    }

    string ToNavDev(string name, uint? id)
    {
        return "<a class='click' href='DevDetail.aspx?dev="+id+"'>"+name+"</a>";
    }
}