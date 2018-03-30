using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;
using System.Text;
using System.Reflection;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public partial class _Default : UniPage
{
    protected string szRate = "";
    protected string szRateMin = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TEACHINGRESVRECREQ vrGet = new TEACHINGRESVRECREQ();
        vrGet.dwCourseID = (uint.Parse(Request["dwCourseID"]));
        vrGet.dwTeacherID = (uint.Parse(Request["dwTeacherID"]));
        vrGet.dwTestPlanID =(uint.Parse(Request["dwTestPlanID"]));
        vrGet.dwStartDate = (uint.Parse(Get1970Date(int.Parse(Request["time"]))));
        vrGet.dwEndDate = (uint.Parse(Get1970Date(int.Parse(Request["time"]))));
        TEACHINGRESVREC[] vtRes;
        float fpoist = 0;// float.Parse(Request["poist"]);
        uResponse = m_Request.Report.GetTeachingResvRec(vrGet, out vtRes);

        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length> 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].dwResvID.ToString() == Request["dwResvID"])
                {
                    USERSPERMINUTE[] vtUserPerMinte= vtRes[i].UsersPerMinute;
                    uint uTotal = (uint)vtRes[i].dwGroupUsers;
                    bool bIs = true;
                    string szFloatRate = "";
                    int k = 0;
                    for (k = 0; k < vtUserPerMinte.Length; k = k + 15)
                    {
                        uint uUser = 0;
                        uint.TryParse(vtUserPerMinte[k].dwUsers.ToString(), out uUser);

                        float fTemp = (((uint)uUser * (float)1.0) / uTotal) * 100;
                        if (fTemp > 100)
                        {
                            fTemp = (float)100.0;
                        }
                        if (bIs && k > 10)
                        {
                            string szTemp = fTemp.ToString(".0");
                            uUser = 0;
                            uint.TryParse(vtUserPerMinte[9].dwUsers.ToString(), out uUser);

                            fTemp = (((uint)uUser * (float)1.0) / uTotal) * 100;
                            szRate += "{ y:" + fTemp.ToString(".0") + ",marker:{symbol: 'url(sun.png)'}" + "},";
                            bIs = false;

                            szRate += szTemp + ",";
                        }
                        else
                        {
                            szRate += fTemp.ToString(".0") + ",";
                        }
                    }
                    int m = 0;
                    bIs = true;
                    for (m = 0; m < vtUserPerMinte.Length; m = m + 15)
                    {
                        if (bIs && m > 10)
                        {
                            szRateMin += "10,";
                            bIs = false;
                        }
                        szRateMin += (m).ToString() + ",";
                    }

                    break;
                }

            }
        }
        if (szRateMin.EndsWith(","))
        {
            szRateMin = szRateMin.Substring(0, szRateMin.Length - 1);
        }
        if (szRate.EndsWith(","))
        {
            szRate = szRate.Substring(0, szRate.Length - 1);
        }
    }
    public string Get1970Date(int TotalSeconds)//根据差距秒数 算出现在是日期
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("yyyyMMdd");
    }
}

