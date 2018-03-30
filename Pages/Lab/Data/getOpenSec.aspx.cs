using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Collections;
public partial class searchAccount :UniPage
{
    public string[] szSecsListTwo = new string[21] { "", "第一二节", "", "第三四节", "", "第五六节", "", "第七八节", "", "第九十节", "", "第十一十二节", "", "第十三十四节", "", "第十五十六节", "", "第十七节", "第十八节", "第十九节", "第二十节" };
    public string[] szSecsListFour = new string[21] { "", "第一~四节", "", "", "", "第五~八节", "", "", "", "第九~十二节", "", "", "", "第十三~十六节", "", "", "", "第十七~二十节", "", "", "" };
  
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uCouserProp = 0;
        uint dwTestPlanid =Parse(Request["dwTestplanid"]);
        uint uCourseID = Parse(Request["dwCourseID"]);
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();
        if (dwTestPlanid == 0 && uCourseID == 0)
        {
            szOut += "[";
            int uWeekTotal = GetWeekTotalNow();
            for (int i = 1; i <= uWeekTotal; i++)
            {
                szOut += "{\"id\":\"" + (i).ToString() + "\",\"label\": \"" + szWeeksList[i] + "\"}";
                if (i < (uWeekTotal))
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
            Response.End();
        }
        if (uCourseID == 0)
        {
            UNITESTPLAN[] vtTestPlan = GetTestPlanByID(dwTestPlanid);
            if (vtTestPlan != null && vtTestPlan.Length > 0)
            {
                uCourseID = (uint)vtTestPlan[0].dwCourseID;
            }
        }
        COURSEREQ courseGet = new COURSEREQ();
        courseGet.dwCourseID = uCourseID;
        UNICOURSE[] vtCouse;
        if (m_Request.Reserve.GetCourse(courseGet, out vtCouse) == REQUESTCODE.EXECUTE_SUCCESS && vtCouse != null)
        {
              UNICOURSE couse = new UNICOURSE();
            couse = vtCouse[0];
            uint uProp = (uint)couse.dwCourseProperty;
            if (((uProp & (uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_WITHTHEORY)) > 0)//理论课
            {
             
                if (couse.szMemo != null && couse.szMemo == "1")
                {
                    uCouserProp = 2;
                }
                else
                {
                    uCouserProp = 1;
                }
            }
            else if (((uProp & (uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_NOTHEORY)) > 0)//实践课
            {
               
                uCouserProp = 3;
            }

            CLASSTIMETABLE[] classTimeTable = GetTermClasTimeTable();
            szOut += "[";
            if (uCouserProp == 1)
            {
                //     for (int i = 1; i <=6; i = i + 2)
                for (int i = 1; i <= classTimeTable.Length; i = i + 2)
                    {
                    int nBegin = i * 100;
                    int nEnd = (i + 1);
                    if (nEnd > classTimeTable.Length)
                    {
                        nEnd = classTimeTable.Length;
                    }
                    int nValue = nBegin + nEnd;
                    szOut += "{\"id\":\"" + (nValue).ToString() + "\",\"label\": \"" + szSecsListTwo[i] + "\"}";
                    if (i <= (classTimeTable.Length - 1))
                    {
                        szOut += ",";
                    }
                    //szOut += GetInputItemHtml(CONSTHTML.radioButton, "resvTime", , );
                }
            }
            else if (uCouserProp == 2)
            {


                //                for (int i = 1; i <= classTimeTable.Length; i = i + 4)

                for (int i = 1; i <= 8; i = i + 4)
                {
                    int nBegin = i * 100;
                    int nEnd = (i + 3);
                    if (nEnd > classTimeTable.Length)
                    {
                        nEnd = classTimeTable.Length;
                    }
                    int nValue = nBegin + nEnd;
                    szOut += "{\"id\":\"" + (nValue).ToString() + "\",\"label\": \"" + szSecsListFour[i] + "\"}";
                    if (i <=(classTimeTable.Length- 1))
                    {
                        szOut += ",";
                    }

                  //  szOut += GetInputItemHtml(CONSTHTML.radioButton, "", szSecsListFour[i], i.ToString());
                }
            }
            else if (uCouserProp == 3)
            {
                ArrayList list = GetListByFieldName("ResvAbsTime");
                 for (int i = 0; list != null && i < list.Count; i++)
                 {
                     CStatueTemp temp = new CStatueTemp();
                     temp = (CStatueTemp)list[i];

                     szOut += "{\"id\":\"" + temp.szValue + "\",\"label\": \"" + temp.szName + "\"},";
                 }
            

            }
           
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[{}]");
        }
    }
        
}