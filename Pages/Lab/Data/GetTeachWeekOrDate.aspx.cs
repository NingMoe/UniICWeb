using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string week = Request["week"];
        string date = Request["date"];
        if (week != null && week != "")
        {
            UNITERM[] termNowList = GetTermNow();
            if (termNowList != null && termNowList.Length > 0)
            {
                uint uWeek = Parse(week);
                uint uWeekDate=1;
                if (Session["selectDate"] != null)
                {
                    int uDate = IntParse(Session["selectDate"].ToString());
                    DateTime dt = new DateTime((uDate / 10000), (uDate % 10000) / 100, uDate % 100);
                    uWeekDate = ((uint)dt.DayOfWeek) + 1;
                }
                int nRes = GetDateFromWeek2((uint)termNowList[0].dwYearTerm, uWeek, uWeekDate);
                nRes = nRes - 100;//变态的地方 非要月份减去一百 js的恶心的地方 需要后台c#来将就。。别的函数调用要小心
                Response.Write("{\"message\":\"" + nRes.ToString()+ "\"}");
            }
        }
        else if (date != null && date != "")
        {
            date = date.Replace("-", "");
            uint nDate = Parse(date);
            date = nDate / 10000 + "-" + (1+(nDate % 10000) / 100) + "-" + nDate % 100;//空间中月份少加一个1
            int uWeek = GetWeekFromDate(date);

            Response.Write("{\"message\":\"" + uWeek.ToString() + "\"}");
        }
        else
        {
            Response.Write("{\"message\":\"" + m_Request.szErrMessage + "\"}");
        }


    }
}