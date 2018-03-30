using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount :UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                if (i < uWeekTotal)
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

            szOut += "[";
          
            uint uPupose=0;
            if(vtCouse!=null&&vtCouse.Length==1)
            {
                if((((uint)vtCouse[0].dwCourseProperty)&(uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_NOTHEORY)>0)
                {
                    uPupose=(uint)UNIRESERVE.DWPURPOSE.USEFOR_NOTHEORY;
                }
                else{
                     uPupose=(uint)UNIRESERVE.DWPURPOSE.USEFOR_WITHTHEORY;
                }
            }
            RESVRULEREQ vrResvGet = new RESVRULEREQ();
            UNIRESVRULE[] vtResvRes;
            vrResvGet.dwResvPurpose = uPupose;
            if (m_Request.Reserve.ResvRuleGet(vrResvGet, out vtResvRes) == REQUESTCODE.EXECUTE_SUCCESS && vtResvRes != null && vtResvRes.Length > 0)
            {
                string szCon = vtResvRes[0].szOtherCons;
                string[] szConList = szCon.Split(';');
                for (int m = 0; m < szConList.Length; m++)
                {
                    string szTemp = szConList[m];
                    szTemp = szTemp.Replace("T02","");
                    string[] szWeekList = szTemp.Split('-');
                    int nBegin = 0;
                    int nEnd = 0;
                    if (szWeekList.Length == 2)
                    {
                        nBegin =IntParse(szWeekList[0]);
                        nEnd = IntParse(szWeekList[1]);
                        int k=0;
                        for (k= nBegin; k<=nEnd; k++)
                        {
                            szOut += "{\"id\":\"" +k.ToString() + "\",\"label\": \"" + szWeeksList[k] + "\"}";
                            if (m < szConList.Length-1)
                            {
                                szOut += ",";
                            }
                        }
                    }
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