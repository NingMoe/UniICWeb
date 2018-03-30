using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniLibrary;

namespace UI.UserControl
{
    public partial class ResvTable : UniUserControl
    {
        public bool ShowWeek = true;  //是否显示周行
        public uint RoomID = 0;  //指定房间号，将只显示此房间。
        public string SetDate = ""; //指定日期，将只显示此日期。
        public string DefaultDate = ""; //默认显示日期。
        public bool TextMode = false; //是否为文本模式。
        public int SecCount = 0;    //显示节次数目，按最近的节次来显示。
        public bool CanResv = false; //是否可以点击预约 ,文本模式不可点击预约。
        public bool AutoScroll = true;  //是否自动滚动。
        public int ScrollSpeed = 3000;  //滚动速度,ms
        public bool HasDate = false; //是否可选择绝对日期。
        public int Width = 960;  //控件总宽度,px
        public int Height = 250;  //控件总高度,px
        public int DevColWidth = 130;  //机房列的宽度,px
        public int RowHeight = 30;  //行的高度,px
        public string Theme = "default"; //页面风格
        public int ResvMode = 0;  //0表示只选开始时间，1表示选择一个时间段(开始-结束)。
        public string ResvURL = "Dlg/SetResv.aspx"; //预约界面
        //-----------------------------------------------

        protected int m_colcount = 0;
        protected string m_szWeek = "";
        protected string m_szSec = "";
        protected string m_szDevTbl = "";
        protected string m_szSecAreaMap = "";
        protected int m_nRoomCount = 0;
        CLASSTIMETABLE[] vtSec;
        float GetSecSN(uint dwTime)
        {
            float ret = 0;
            if (vtSec == null) return 0;
            for (int i = 0; i < vtSec.Length; i++)
            {
                if (vtSec[i].dwBeginTime <= dwTime && dwTime <= vtSec[i].dwEndTime)
                {
                    ret = (float)(uint)vtSec[i].dwSecIndex - 1;
                    ret += (float)(dwTime - vtSec[i].dwBeginTime) / (float)(vtSec[i].dwEndTime - vtSec[i].dwBeginTime);
                    return ret;
                }
            }
            return 0;
        }

        CLASSTIMETABLE[] GetLimitSec(CLASSTIMETABLE[] sec, int nSecCount)
        {
            if (sec.Length > nSecCount)
            {
                int curSec = 0;
                uint dwTime = (uint)((DateTime.Now.Hour * 60) + DateTime.Now.Minute);
                for (int i = 0; i < sec.Length; i++)
                {
                    if (sec[i].dwBeginTime <= dwTime && dwTime <= sec[i].dwEndTime)
                    {
                        curSec = i;
                        break;
                    }
                }
                if (sec.Length - curSec < nSecCount)
                {
                    curSec = sec.Length - nSecCount;
                }

                CLASSTIMETABLE[] vtSec2 = new CLASSTIMETABLE[nSecCount];
                Array.Copy(sec, curSec,vtSec2,0, nSecCount);
                return vtSec2;
            }
            else
            {
                return sec;
            }
        }

        string GetSecName(CLASSTIMETABLE[] vtSec, uint dwTime)
        {
            for (int i = 0; i < vtSec.Length; i++)
            {
                if (vtSec[i].dwBeginTime <= dwTime && dwTime <= vtSec[i].dwEndTime)
                {
                    return vtSec[i].szSecName;
                }
            }
            return "";
        }
        public string GetShortName(string szName, int nMaxLen)
        {
            if (string.IsNullOrEmpty(szName)) return szName;

            if (szName.Length > nMaxLen)
            {
                //┅┄┉┈
                return "<span title=\"" + szName + "\">┉" + szName.Substring(szName.Length - nMaxLen) + "</span>";
            }
            else
            {
                return "<span title=\"" + szName + "\">" + szName + "</span>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TextMode)
            {
                if (RoomID == 0xffffffff) return;
                DateTime dtDate;
                if (string.IsNullOrEmpty(SetDate))
                {
                    if (!string.IsNullOrEmpty(DefaultDate))
                    {
                        DateTime.TryParse(DefaultDate, out dtDate);
                    }
                    else
                    {
                        dtDate = DateTime.Now;
                    }
                }
                else if(SetDate == "now")
                {
                    dtDate = DateTime.Now;
                }else
                {
                    DateTime.TryParse(SetDate, out dtDate);
                }
                CTSREQ ctsreq = new CTSREQ();
               
                REQUESTCODE ret1 = m_Request.Reserve.GetClassTimeTable(ctsreq, out vtSec);
                if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_colcount = vtSec.Length;
                }

                ROOMRESVSTATREQ vrRoomStatReq = new ROOMRESVSTATREQ();
                ROOMRESVSTAT[] vtRoomStat;
                if (RoomID > 0)
                {
                   // vrRoomStatReq.dwGetType = (uint)ROOMRESVSTATREQ.DWGETTYPE.ROOMRESVSTAT_ROOMID;
                    vrRoomStatReq.szRoomIDs = RoomID.ToString();
                }
                else
                {
                   // vrRoomStatReq.dwGetType = (uint)ROOMRESVSTATREQ.DWGETTYPE.ROOMRESVSTAT_ALL;
                }
                vrRoomStatReq.dwDate = GetDate(dtDate.ToShortDateString().Replace("/","-"));
                REQUESTCODE ret2 = m_Request.Device.GetRoomResvStat(vrRoomStatReq, out vtRoomStat);
                if (ret2 == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_nRoomCount = vtRoomStat.Length;
                    for (int i = 0; i < vtRoomStat.Length; i++)
                    {
                        ROOMRESVSTAT stat = vtRoomStat[i];
                        if (RoomID > 0 && RoomID != stat.dwRoomID)
                        {
                            continue;
                        }
                        for (int n = stat.szResvInfo.Length - 1; n >= 0; n--)
                        {
                            TEACHINGRESVINFO tinfo = stat.szResvInfo[n];
                            string szBeginSec = ((tinfo.dwTeachingTime / 100) % 100).ToString();   // GetSecName(vtSec, (uint)tinfo.dwBeginTime);
                            string szEndSec = (tinfo.dwTeachingTime % 100).ToString();   //GetSecName(vtSec, (uint)tinfo.dwEndTime);

                            string szText = "";
                            if (!string.IsNullOrEmpty(tinfo.szTeacherName))
                            {
                                szText += tinfo.szTeacherName + ",";
                            }
                            if (!string.IsNullOrEmpty(tinfo.szTestPlanName))
                            {
                                szText += "" + tinfo.szTestPlanName + " ";
                            }
                            if (!string.IsNullOrEmpty(tinfo.szTestName))
                            {
                                szText += "" + tinfo.szTestName + " ";
                            }
                            else if (!string.IsNullOrEmpty(tinfo.szGroupName))
                            {
                                szText += tinfo.szGroupName + " ";
                            }
                            else if (!string.IsNullOrEmpty(tinfo.szCourseName))
                            {
                                szText += tinfo.szCourseName + " ";
                            }
                            string str = "<td class='RoomName' >" + GetShortName(stat.szRoomName, 5) + "</td><td class='timeSec'>" + szBeginSec + "—" + szEndSec + "</td><td class='resvName'>" + GetShortName(szText, 10) + "</td>";
                            m_szDevTbl += "<tr class='resvItem'>" + str + "</tr>";
                        }
                        if (RoomID > 0)
                        {
                            m_nRoomCount = 1;
                            break;
                        }
                    }
                    m_szDevTbl = "<table class='resvTable' height='354px'><thead class='itemHeader'><tr><th class='RoomName'>实验室</th><th class='timeSec'>节次</th><th class='resvName'>班级</th></tr></thead><tbody>" + m_szDevTbl + "</tbody></table>";
                }
            }
            else
            {
                DateTime dtStart;
                if (string.IsNullOrEmpty(SetDate))
                {
                    SetDate = HF_StartDate.Value;
                }
                
                if (string.IsNullOrEmpty(SetDate))
                {
                    if (!string.IsNullOrEmpty(DefaultDate))
                    {
                        DateTime.TryParse(DefaultDate, out dtStart);
                    }
                    else
                    {
                        dtStart = DateTime.Now;
                    }
                    SetDate = dtStart.ToShortDateString().Replace("/", "-");
                }
                else if (SetDate == "now")
                {
                    dtStart = DateTime.Now;
                }
                else
                {
                    DateTime.TryParse(SetDate, out dtStart);
                }


                HF_StartDate.Value = SetDate;

                string[] szWeekName = { "日", "一", "二", "三", "四", "五", "六" };
                if (string.IsNullOrEmpty(HF_Week.Value))
                {
                    HF_Week.Value = dtStart.ToShortDateString().Replace("/", "-");
                }
                else
                {
                    DateTime dtCur;
                    DateTime.TryParse(HF_Week.Value, out dtCur);
                    if (dtCur < dtStart || dtCur >= dtStart.AddDays(7))
                    {
                        HF_Week.Value = dtStart.ToShortDateString().Replace("/", "-");
                    }
                }
                for (int i = 0; i <= 7; i++)
                {
                    DateTime d = dtStart.AddDays(i);
                    string szClass = "";
                    if (HF_Week.Value == d.ToShortDateString().Replace("/", "-"))
                    {
                        szClass = "class='curweek'";
                    }
                    m_szWeek += "<div data-d='" + d.ToShortDateString().Replace("/", "-") + "' " + szClass + ">" + d.Month + "月" + d.Day + "日<br/>周" + szWeekName[(int)d.DayOfWeek] + "</div>";
                }

                //----------------------------------------------------------
                CTSREQ ctsreq = new CTSREQ();
              
                REQUESTCODE ret1 = m_Request.Reserve.GetClassTimeTable(ctsreq, out vtSec);

                if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    if (SecCount > 0)
                    {
                        vtSec = GetLimitSec(vtSec, SecCount);
                    }
                    m_colcount = vtSec.Length;

                    int awidth = (Width - DevColWidth) / m_colcount + 1;
                    for (int i = 0; i < vtSec.Length; i++)
                    {
                        m_szSec += "<div title='" + (vtSec[i].dwBeginTime / 60) + ":" + vtSec[i].dwBeginTime % 60 + "至" + (vtSec[i].dwEndTime / 60) + ":" + vtSec[i].dwEndTime % 60 + "'>" + vtSec[i].szSecName + "</div>";
                        m_szSecAreaMap += "<area data-id='" + vtSec[i].dwSecIndex + "' shape='rect' coords='" + (i * awidth) + ",0," + ((i+1) * awidth) + "," + RowHeight + "'/>";
                    }
                }

                //----------------------------------------------------------
                if (RoomID == 0xffffffff) return;
                int nStartLine = 0;
                int nNeedLine = 10;
                int.TryParse(Request["DevStartLine"], out nStartLine);
                int.TryParse(Request["DevNeedLine"], out nNeedLine);
                if (nNeedLine == 0) nNeedLine = 10;

                ROOMRESVSTATREQ vrRoomStatReq = new ROOMRESVSTATREQ();
                ROOMRESVSTAT[] vtRoomStat;

                if (RoomID > 0)
                {
                    //vrRoomStatReq.dwGetType = (uint)ROOMRESVSTATREQ.DWGETTYPE.ROOMRESVSTAT_ROOMID;
                    vrRoomStatReq.szRoomIDs = RoomID.ToString();
                }
                else
                {
                    //vrRoomStatReq.dwGetType = (uint)ROOMRESVSTATREQ.DWGETTYPE.ROOMRESVSTAT_ALL;
                }
                vrRoomStatReq.dwDate = GetDate(HF_Week.Value);
                REQUESTCODE ret2 = m_Request.Device.GetRoomResvStat(vrRoomStatReq, out vtRoomStat);

                if (ret2 == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_nRoomCount = vtRoomStat.Length;
                    for (int i = 0; i < vtRoomStat.Length; i++)
                    {
                        if (i < nStartLine)
                        {
                            continue;
                        }
                        if (i >= nStartLine + nNeedLine)
                        {
                            break;
                        }

                        ROOMRESVSTAT stat = vtRoomStat[i];
                        if (RoomID > 0 && RoomID != stat.dwRoomID)
                        {
                            continue;
                        }
                        m_szDevTbl += "<tr><td><div class='DevName'>" + GetShortName(stat.szRoomName,6) + "</div></td><td><div class='bgWeek' data-id='" + stat.dwRoomID + "'>";
                        float nLastEndSec = 0;
                        //todo演示使用
                        stat.szResvInfo = new TEACHINGRESVINFO[2];
                        stat.szResvInfo[0] = new TEACHINGRESVINFO();
                        stat.szResvInfo[0].dwTeachingTime = 550507;
                        stat.szResvInfo[1] = new TEACHINGRESVINFO();
                        stat.szResvInfo[1].dwTeachingTime = 650204;

                        for (int n = stat.szResvInfo.Length - 1; n >= 0; n--)
                        {
                            TEACHINGRESVINFO tinfo = stat.szResvInfo[n];
                            float nBeginSec = (float)((tinfo.dwTeachingTime / 100) % 100); //GetSecSN((uint)tinfo.dwBeginTime);
                            float nEndSec = (float)(tinfo.dwTeachingTime % 100); //GetSecSN((uint)tinfo.dwEndTime);

                            string szText = "";
                            if (!string.IsNullOrEmpty(tinfo.szTeacherName))
                            {
                                szText += tinfo.szTeacherName + ",";
                            }
                            if (!string.IsNullOrEmpty(tinfo.szTestPlanName))
                            {
                                szText += "" + tinfo.szTestPlanName + " ";
                            }
                            if (!string.IsNullOrEmpty(tinfo.szTestName))
                            {
                                szText += "" + tinfo.szTestName + " ";
                            }
                            else if (!string.IsNullOrEmpty(tinfo.szGroupName))
                            {
                                szText += tinfo.szGroupName + " ";
                            }
                            else if (!string.IsNullOrEmpty(tinfo.szCourseName))
                            {
                                szText += tinfo.szCourseName + " ";
                            }
                            if ((tinfo.dwResvStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) != 0)
                            {
                                m_szDevTbl += "<div class='Resv ResvDOING' ";
                            }
                            else if ((tinfo.dwResvStat & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) != 0)
                            {
                                m_szDevTbl += "<div class='Resv ResvDONE' ";
                            }
                            else
                            {
                                m_szDevTbl += "<div class='Resv' ";
                            }
                            m_szDevTbl += "data-ny='" + stat.dwRoomID + "' data-nxs='" + nBeginSec + "' data-nxe='" + nEndSec + "' data-p='" + (nBeginSec - nLastEndSec) + "' data-w='" + (nEndSec - nBeginSec) + "'>" + szText + "</div>";
                            nLastEndSec = nEndSec;
                        }
                        m_szDevTbl += "</div></td></tr>";
                        if (RoomID > 0)
                        {
                            m_nRoomCount = 1;
                            break;
                        }
                    }
                }
            }
        }

        protected void Button_Week_Click(object sender, EventArgs e)
        {
            string v = HF_Week.Value;

            
        }

        protected void Button_Resv_Click(object sender, EventArgs e)
        {
            string v = HF_Resv.Value;
            string[] arrayv = v.Split(new char[] { ','});
            if (arrayv.Length == 2)
            {
                Response.Redirect(ResvURL+"?RoomID=" + arrayv[1] + "&dwDate=" + HF_Week.Value + "&dwBeginSec=" + arrayv[0]);
            }
        }
    }
}