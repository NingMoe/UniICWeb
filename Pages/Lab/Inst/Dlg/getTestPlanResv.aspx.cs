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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "查看排课情况";
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_ManMode = "";
    protected string m_szOpenRule = "";
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szID = Request["id"];
        if (string.IsNullOrEmpty(szID))
        {
            return;
        }
        UNITESTPLAN[] vrResult = GetTestPlanByID(Parse(szID));
        if (vrResult == null || vrResult.Length == 0)
        {
            return;
        }
        int i = 0;

        UNIACCOUNT accnoTemp;
        string szTecahcname = "";
        if (GetAccByAccno(vrResult[i].dwTeacherID.ToString(), out accnoTemp))
        {
            szTecahcname = vrResult[i].szTeacherName + "(" + accnoTemp.szLogonName + ")";
        }
        else
        {
            szTecahcname = vrResult[i].szTeacherName;
        }
        string szGroupName = vrResult[i].szGroupName;
        
        TESTITEMREQ testitemreq = new TESTITEMREQ();
        testitemreq.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        testitemreq.szGetKey = szID;
        UNITESTITEM[] testitemRes;
        if (m_Request.Reserve.GetTestItem(testitemreq, out testitemRes) == REQUESTCODE.EXECUTE_SUCCESS && testitemRes != null && testitemRes.Length > 0)
        {
            for (int j = 0; j < testitemRes.Length; j++)
            {
                string szTestName = testitemRes[j].szTestName;

                UNIRESERVE[] resvInfo = testitemRes[j].ResvInfo;
                if (resvInfo != null && resvInfo.Length > 0)
                {
                    for (int k = 0; k < resvInfo.Length; k++)
                    {
                        m_szOut += "<tr>";
                        m_szOut += "<td>"+szTecahcname+"</td>";
                        m_szOut+= "<td>"+vrResult[i].szGroupName.ToString()+"</td>";
                     
                        m_szOut+= "<td>"+szTestName+"</td>";
                        m_szOut += "<td>" + testitemRes[j].dwTestHour.ToString() + "</td>";
                        string szResvTime = GetTeachingTime((uint)resvInfo[k].dwTeachingTime)+"</td>";
                        m_szOut+= "<td>"+szResvTime+"</td>";

                        string szRoomInfo = "";
                        RESVDEV[] resvdevInfo = resvInfo[k].ResvDev;
                        if (resvInfo != null && resvInfo.Length > 0)
                        {
                            for (int w = 0; w < resvdevInfo.Length; w++)
                            {
                                szRoomInfo += resvdevInfo[w].szRoomName + ";";
                            }
                        }
                        m_szOut+= "<td>"+szRoomInfo+"</td>";
                        m_szOut += "</tr>";
                    }
                }
                else
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td>" + szTecahcname + "</td>";
                    m_szOut += "<td>" + vrResult[i].szGroupName.ToString() + "</td>";
                    m_szOut += "<td>"+szTestName+"</td>";
                    m_szOut += "<td>" + "" + "</td>";
                    m_szOut += "<td>" + "" + "</td>";
                    m_szOut += "<td>"+""+"</td>";
                    m_szOut+= "<td>"+""+"</td>";
                    m_szOut += "</tr>";
                }
            }
        }
        else
        {
            m_szOut += "<tr>";
            m_szOut += "<td>" + szTecahcname + "</td>";
            m_szOut += "<td>" + vrResult[i].szGroupName.ToString() + "</td>";
            m_szOut += "<td>" + "" + "</td>";
            m_szOut += "<td>" + "" + "</td>";
            m_szOut += "<td>"+""+"</td>";
            m_szOut+= "<td>"+""+"</td>";
            m_szOut+= "<td>"+""+"</td>";
            m_szOut += "</tr>";
        }

    }
}
