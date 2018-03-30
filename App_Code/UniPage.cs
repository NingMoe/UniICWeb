using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using UniWebLib;
using UniLibrary;

/// <summary>
/// UniPage 的摘要说明
/// </summary>
/// 

public partial class UniPage : UniWebLib.UniPage
{
    public string[] szWeekDayList=new string[7]{"星期一","星期二","星期三","星期四","星期五","星期六","星期天"};
    public string[] szWeeksList = new string[35] { "", "第一周", "第二周", "第三周", "第四周", "第五周", "第六周", "第七周", "第八周", "第九周", "第十周", "第十一周", "第十二周", "第十三周", "第十四周", "第十五周", "第十六周", "第十七周", "第十八周", "第十九周", "第二十周", "第二十一周", "第二十二周", "第二十三周", "第二十四周", "第二十五周", "第二十六周", "第二十七周", "第二十八周", "第二十九周", "第三十十周", "第三十一周", "第三十二周", "第三十三周", "第三十四周" };
    public string[] szSecsList = new string[21] { "", "第一节", "第二节", "第三节", "第四节", "第五节", "第六节", "第七节", "第八节", "第九节", "第十节", "第十一节", "第十二节", "第十三节", "第十四节", "第十五节", "第十六节", "第十七节", "第十八节", "第十九节", "第二十节" };
    public uint Parse(string s)
    {
        uint ret = 0;
        uint.TryParse(s, out ret);
        return ret;
    }
    public int IntParse(string s)
    {
        int ret = 0;
        int.TryParse(s, out ret);
        return ret;
    }
    public float FloatParse(string s)
    {
        float ret = 0;
        float.TryParse(s, out ret);
        return ret;
    }
    public void PutMemberValue(string szName, string szValue)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl ScriptInclude = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        ScriptInclude.Attributes.Add("type", "text/javascript");
        string szPageScript = "[['" + szName + "','"+szValue+"']]";
        ScriptInclude.InnerHtml = "function SetPageValue(){ PutHttpValue(" + szPageScript + ");}$(SetPageValue);";
        if (Header != null && Header.Controls != null)
        {
            Header.Controls.Add(ScriptInclude);
        }
        else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
        {
            Form.Controls.Add(ScriptInclude);
        }
        else
        {
            this.Controls.Add(ScriptInclude);
        }
    }
    public void PutMemberValue2(string szName, string szValue)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl ScriptInclude = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        ScriptInclude.Attributes.Add("type", "text/javascript");
        string szPageScript = "[['" + szName + "','" + szValue + "']]";
        ScriptInclude.InnerHtml = "function SetPageValue(){ PutHttpValue2(" + szPageScript + ");}$(SetPageValue);";
        if (Header != null && Header.Controls != null)
        {
            Header.Controls.Add(ScriptInclude);
        }
        else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
        {
            Form.Controls.Add(ScriptInclude);
        }
        else
        {
            this.Controls.Add(ScriptInclude);
        }
    }
    public uint GetDevSN()
    {
        uint nRes = 1;
        nRes = 1 + Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) +Parse(DateTime.Now.ToString("ffff"));
        return nRes;
    }

    public void MessageBoxSuccess(string szContent)
    {
        m_szScript = "$(function(){MessageBox(\"" + szContent + "\",\"" + "提示" + "\"," + ((uint)MSGBOX.SUCCESS).ToString() + ",function(){" + "Dlg_OK()" + "});});";
        HtmlGenericControl ScriptInclude = new HtmlGenericControl("script");
        ScriptInclude.Attributes.Add("type", "text/javascript");
        ScriptInclude.InnerHtml = m_szScript;

        if (this.Page.Header != null && this.Page.Header.Controls != null)
        {
            this.Page.Header.Controls.Add(ScriptInclude);
        }
        else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
        {
            this.Page.Form.Controls.Add(ScriptInclude);
        }
        else if (this.Controls != null && !this.Controls.IsReadOnly)
        {
            this.Page.Controls.Add(ScriptInclude);
        }
    }
    public void MessageBoxFail(string szContent)
    {
        string m_szScript = "$(function(){MessageBox(\"" + szContent + "\",\"" + "提示" + "\"," + ((uint)MSGBOX.ERROR).ToString() + ",function(){" + "Dlg_Cancel()" + "});});";
        HtmlGenericControl ScriptInclude = new HtmlGenericControl("script");
        ScriptInclude.Attributes.Add("type", "text/javascript");
        ScriptInclude.InnerHtml = m_szScript;

        if (Header != null && Header.Controls != null)
        {
           this.Page.Header.Controls.Add(ScriptInclude);
        }
        else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
        {
            this.Page.Form.Controls.Add(ScriptInclude);
        }
        else if (this.Controls != null && !this.Controls.IsReadOnly)
        {
            this.Page.Controls.Add(ScriptInclude);
        }
    }

    public uint GetDefaultTerm(string szParam)
    {
        if (Session["TermIndex"] == null)
        {
            return ToUint(szParam);
        }
        else
        {
            return (uint)Session["TermIndex"];
        }
    }

    public void TranTerm(ref uint? dwYearTerm)
    {
        if (dwYearTerm < 3 || dwYearTerm == null)
        {
            if (dwYearTerm == null)
            {
                if (Session["TermIndex"] == null)
                {
                    dwYearTerm = 0;
                }
                else
                {
                    dwYearTerm = (uint)Session["TermIndex"];
                }                
            }
            return;
        }

        uint curYear = (uint)DateTime.Now.Year;
        uint curTerm = 1;

        uint termYear = (uint)dwYearTerm / 10000;
        uint termTerm = (uint)dwYearTerm % 10;

        if (termYear == curYear && curTerm == termTerm)
        {
            dwYearTerm = 0;
        }
        else if (termYear < curYear)
        {
            dwYearTerm = 1;
        }
        else if (termYear == curYear && termTerm < curTerm)
        {
            dwYearTerm = 1;
        }
        else
        {
            dwYearTerm = 2;
        }
    }

    public uint GetTerm(string szParam)
    {
        uint uParam = ToUint(szParam);
        if (szParam != null)
        {
            Session["TermIndex"] = uParam;
        }
        else if(Session["TermIndex"] != null)
        {
            uParam = (uint)Session["TermIndex"];
        }

        //获取学期列表
        TERMREQ vrParameter = new TERMREQ();
      
        UNITERM[] vrResult;
        uint curYear = (uint)DateTime.Now.Year;
        uint curTerm = 1;
        if (DateTime.Now.Month >= 9)
        {
            curTerm = 2;
        }
        if (uParam == 1)
        {
            if (curTerm == 2)
            {
                curTerm = 1;
            }
            else
            {
                curTerm = 2;
                curYear--;
            }
        }
        else if (uParam == 2)
        {
            if (curTerm == 2)
            {
                curTerm = 1;
                curYear++;
            }
            else
            {
                curTerm = 2;
            }
        }

        if (m_Request.Reserve.GetTerm(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (vrResult[i].dwYearTerm / 10000 == curYear)
                {
                    if (vrResult[i].dwYearTerm % 10 == curTerm)
                    {
                        return (uint)vrResult[i].dwYearTerm;
                    }
                }
            }
        }
        return 0;
    }

    public string GetTermText(uint? dwYearTerm)
    {
       UNITERM[] termList= GetAllTerm();
       for (int i = 0; i < termList.Length; i++)
       {
           if (termList[i].dwYearTerm == dwYearTerm)
           {
               return termList[i].szMemo;
           }
       }
           return "";
    }
    public CLASSTIMETABLE[] GetTermClasTimeTable()
    {
        /*
        if (Session["ClassTimeTable"] != null)
        {
            return (CLASSTIMETABLE[])Session["ClassTimeTable"];
        }
        */
        TERMREQ vrGet = new TERMREQ();
        vrGet.dwStatus = (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE;
        UNITERM[] vtRes;
        if (m_Request.Reserve.GetTerm(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            Session["ClassTimeTable"] = vtRes[0].szCTS1;
            return vtRes[0].szCTS1;
        }
        else
        {
            vrGet.dwStatus = (uint)UNITERM.DWSTATUS.TERMSTAT_UNFORCE;
            if (m_Request.Reserve.GetTerm(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                Session["ClassTimeTable"] = vtRes[0].szCTS1;
                return vtRes[0].szCTS1;
            }
        }
        return null;
    }
    public UNITERM[] GetTermByID(uint uTermID)
    {
        TERMREQ vrGet = new TERMREQ();
        vrGet.dwYearTerm = uTermID;
        UNITERM[] vtRes;
        if (m_Request.Reserve.GetTerm(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            //Session["ClassTimeTable"] = vtRes[0].szCTS1;
            return vtRes;
        }
        return null;
    }
    public UNITERM[] GetTermNow()
    {
        if (Session["TermNow"] != null)
        {
            //return (UNITERM[])Session["TermNow"];
        }
        TERMREQ vrGet = new TERMREQ();
        vrGet.dwStatus = (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE;
        UNITERM[] vtRes;
        if (m_Request.Reserve.GetTerm(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            Session["TermNow"] = vtRes;
            return vtRes;
        }
        return null;
    }
    public string DateToStr(uint? dwDate)
    {
        if (dwDate == null || dwDate == 0)
        {
            return "";
        }
        return dwDate / 10000 + "-" + (dwDate % 10000) / 100 +"-" + dwDate % 100;
    }
    public int GetWeekNow()//获取当前是第几周
    {
        return GetWeekFromDate(DateTime.Now.ToString("yyyy-MM-dd"));
    }
    public int GetWeekFromDate(string szDate)
    {
        UNITERM[] termNowList = GetTermNow();
        if (termNowList != null && termNowList.Length > 0)
        {
            UNITERM termNow = new UNITERM();
            termNow = termNowList[0];
            uint uStartDate = (uint)termNow.dwBeginDate;
            uint uFirstDay = (uint)termNow.dwFirstWeekDays;
            DateTime dtStart = DateTime.Parse(uStartDate / 10000 + "-" + (uStartDate % 10000) / 100 + "-" + uStartDate % 100);
            DateTime dtEnd = DateTime.Parse(szDate);
            TimeSpan sp = dtEnd - dtStart.AddDays(uFirstDay-1);
            int uRes = (sp.Days / 7 + 1);
            if ((sp.Days % 7) > 0)
            {
                uRes = uRes + 1;
            }
            return uRes;
        }
        return 0;
    }
    public int GetDateFromWeek(uint uYearTerm, uint uWeeks,uint uWeek)//uweeks周次,uweek星期
    {
        uint uTermMoudel = 1;//表示西式日历
       
        UNITERM[] termNowList = GetTermByID(uYearTerm);
        if (termNowList != null && termNowList.Length > 0)
        {
            UNITERM termNow = new UNITERM();
            termNow = termNowList[0];
            int nDaysAdd = 0;
            int nBeginDate = (int)termNow.dwBeginDate;
            DateTime dateStart = DateTime.Parse(nBeginDate / 10000 + "-" + (nBeginDate % 10000) / 100 + "-" + nBeginDate % 100);

            if (uTermMoudel == 1)
            {
                if (uWeek == 6)
                {
                    uWeek = 0;
                }
                else
                {
                    uWeek = uWeek + 1;
                }
                int nStartWeek = (int)dateStart.DayOfWeek;
                nDaysAdd = ((int)uWeeks - 1) * 7 - nStartWeek + (int)uWeek;
            }
            else
            {
                int nStartWeek = (int)dateStart.DayOfWeek;
                if (nStartWeek == 0)
                {
                    nStartWeek = 7;
                }
                uWeek = uWeek + 1;
                nDaysAdd = ((int)uWeeks - 1) * 7 - nStartWeek + (int)uWeek;

            }
            dateStart=dateStart.AddDays(nDaysAdd);
            return int.Parse(dateStart.ToString("yyyyMMdd"));
        }
        return 0;
    }
    public int GetDateFromWeek2(uint uYearTerm, uint uWeeks, uint uWeek)//uweeks周次,uweek星期
    {
        uint uTermMoudel = 0;//表示中式日历

        UNITERM[] termNowList = GetTermByID(uYearTerm);
        if (termNowList != null && termNowList.Length > 0)
        {
            UNITERM termNow = new UNITERM();
            termNow = termNowList[0];
            int nDaysAdd = 0;
            int nBeginDate = (int)termNow.dwBeginDate;
            DateTime dateStart = DateTime.Parse(nBeginDate / 10000 + "-" + (nBeginDate % 10000) / 100 + "-" + nBeginDate % 100);

            if (uTermMoudel == 1)
            {
                if (uWeek == 6)
                {
                    uWeek = 0;
                }
                else
                {
                    uWeek = uWeek + 1;
                }
                int nStartWeek = (int)dateStart.DayOfWeek;
                nDaysAdd = ((int)uWeeks - 1) * 7 - nStartWeek + (int)uWeek;
            }
            else
            {
                int nStartWeek = (int)dateStart.DayOfWeek;
                if (nStartWeek == 0)
                {
                    nStartWeek = 7;
                }
                uWeek = uWeek + 1;
                nDaysAdd = ((int)uWeeks - 1) * 7 - nStartWeek + (int)uWeek;

            }
            dateStart = dateStart.AddDays(nDaysAdd);
            return int.Parse(dateStart.ToString("yyyyMMdd"));
        }
        return 0;
    }
    public int GetWeekTotalNow()//获取总周数
    {
        if (Session["WeekTotalNow"] != null)
        {
            return (int)Session["WeekTotalNow"];
        }
        UNITERM[] termNowList = GetTermNow();
        if (termNowList != null && termNowList.Length > 0)
        {
            UNITERM termNow = new UNITERM();
            termNow = termNowList[0];
            uint uEndDate = (uint)termNow.dwEndDate;
            uint uStartDate = (uint)termNow.dwBeginDate;
            uint uFirstDay = (uint)termNow.dwFirstWeekDays;
            DateTime dtStart = DateTime.Parse(uStartDate/10000+"-"+(uStartDate%10000)/100+"-"+uStartDate%100);
            DateTime dtEnd = DateTime.Parse(uEndDate / 10000 + "-" + (uEndDate % 10000) / 100 + "-" + uEndDate % 100);
           TimeSpan sp=dtEnd-dtStart.AddDays(uFirstDay);
           int uRes = (sp.Days / 7 + 1);
           if ((sp.Days % 7 )> 0)
           {
               uRes = uRes + 1;
           }
           Session["WeekTotalNow"] = uRes;
           return uRes;
        }
        return 0;
    }
    public uint DateToUint(string szDate)
    {
        szDate = szDate.Replace("-","");
        return Parse(szDate);
    }
    public string GetFee(uint? fee)
    {
        if (fee == null)
        {
            return "0";
        }
        uint uFee = (uint)fee;
        return fee / 100 + "." + ((int)((fee % 100))).ToString("00") + "元";
    }

    public uint GetTermList()
    {
        uint ret = 0;

        //获取学期列表
        TERMREQ vrParameter = new TERMREQ();
       
        UNITERM[] vrResult;
        uint curYear = (uint)DateTime.Now.Year;
        uint curTerm = 1;
        if (DateTime.Now.Month >= 9)
        {
            curTerm = 2;
        }

        uint prevYear = 0;
        uint prevTerm = 0;
        uint nextYear = 0;
        uint nextTerm = 0;

       
        if (curTerm == 2)
        {
            prevYear = curYear;
            prevTerm = 1;

            nextYear = curYear + 1;
            nextTerm = 1;
        }
        else
        {
            prevTerm = 2;
            prevYear = curYear-1;

            nextYear = curYear;
            nextTerm = 2;
        }


        if (m_Request.Reserve.GetTerm(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint y = (uint)vrResult[i].dwYearTerm / 10000;
                uint t = (uint)vrResult[i].dwYearTerm % 10;
                if (y == prevYear && t == prevTerm)
                {
                    ret |= 1;
                }
                if (y == curYear && t == curTerm)
                {
                    ret |= 2;
                }
                if (y == nextYear && t == nextTerm)
                {
                    ret |= 4;
                }
            }
        }
        return ret;
    }
    public UNITERM[] GetAllTerm()
    {
        /*
        if (Session["TermList"] != null)
        {
            UNITERM[] termList = (UNITERM[])Session["TermList"];
            return termList;
        }
         */
        //获取学期列表
        TERMREQ vrParameter = new TERMREQ();

        UNITERM[] vrResult;
      

        if (m_Request.Reserve.GetTerm(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Session["TermList"] = vrResult;
            return vrResult;
        }
        return null;
    }


	//导入处理函数,根据参数的类型做不同的处理。
    public REQUESTCODE ImportProcess(object newValue)
    {
        REQUESTCODE ret = REQUESTCODE.ERR_REQ_NONE;
        if (newValue.GetType() == typeof(TESTCARD))
        {
            TESTCARD setValue = (TESTCARD)newValue;
            ret = m_Request.Reserve.SetTestCard(setValue, out setValue);
        }
        return ret;
    }
      public string Get1970Teaching(uint? uTeachingTimeP)//根据差距秒数 算出现在是日期
        {
            if (uTeachingTimeP == null)
            {
                return "";
            }
          uint uTeachingTime=(uint)uTeachingTimeP;
        return szWeeksList[uTeachingTime/100000]+szWeekDayList[(uTeachingTime % 100000) / 10000] +szSecsList[(uTeachingTime % 10000) / 100 ]+ szSecsList[(uTeachingTime % 100)];
      }
      public string GetRoomNoCtrlList(string szRoomNO)
      {
          if (szRoomNO == null || szRoomNO == "")
          {
              return "";
          }
          if (!szRoomNO.EndsWith(","))
          {
              szRoomNO = (szRoomNO + ",");
          }

          if (!szRoomNO.StartsWith(","))
          {
              szRoomNO = ("," + szRoomNO);
          }
          return szRoomNO;
      }
    public string GetIdent(uint uIdent)
    {
        string szIdent = "";
        if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER) > 0)
        {
            uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER;
        }
        if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER) > 0)
        {
            uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER;
        }
        if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0)
        {
            uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
        }
        if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER) > 0)
        {
            uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER;
        }
       
        if (uIdent == 256)
        {
            szIdent = GetJustNameEqual(uIdent, "Ident");
        }
        else if (uIdent == 512)
        {
            szIdent = GetJustNameEqual(uIdent, "Ident");
        }
        else
        {
            szIdent = GetJustNameEqual(uIdent, "Ident");
            if (szIdent == "")
            {
                if ((uIdent & 256) > 0)
                {
                    szIdent = "学生";
                }
                else if ((uIdent & 256) > 0)
                {
                    szIdent = "教职工";
                }
                else if ((uIdent & (0x400)) > 0)
                {
                    szIdent = "外聘人员";
                }
                else if ((uIdent & (0x800)) > 0)
                {
                    szIdent = "离休";
                }
                else if ((uIdent & (0x2000)) > 0)
                {
                    szIdent = "教师";
                }
                else if ((uIdent & (0x4000)) > 0)
                {
                    szIdent = "其它";
                }
            }
        }
        return szIdent;
    }
}