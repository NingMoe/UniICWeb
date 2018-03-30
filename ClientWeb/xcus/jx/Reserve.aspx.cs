using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Reserve : UniClientPage
{
    protected string testitem_id = "";
    protected string groupName = "";
    protected string testName = "";
    protected string planLinkList = "";
    protected string curLink = "";
    protected string curTerm = "";
    protected string termList = "";
    protected string LabList = "";
    protected string resvRule = "";
    protected string purpose = "";
    protected string selTimeOpt = "";
    protected string selSecOpt = "";
    protected UNICOURSE myCourse;
    private UNITERM yearTerm;
    protected string openWeeks = "";
    protected string TeachResvMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ClientRedirect("Login.aspx"))
        {
            testitem_id = Request["test_id"];
            if (string.IsNullOrEmpty(testitem_id))
            {
                MsgBox("未指定实验项目", "location.href='Default.aspx'");
                return;
            }
            InitTerm();
            InitTest();
            InitLink();
            InitOpts();
            LabList = GetLabHtm("opt");
            resvRule = GetXmlContent("resv_rule", "other");
            if (GetConfig("TeachResvMode") == "1")
            {
                TeachResvMode = "1";
                FilerRule();//最后
            }
            if (GetConfig("clientTab") == "gs")
            {
                XmlNodeList list = common.GetXMLConst(Server.MapPath("~/LocalFile/file.xml"), "ResvAbsRec");
                if (list != null)
                {
                    foreach (XmlNode item in list)
                    {
                        string text = item.InnerText;
                        string value = item.Attributes["value"].Value;
                        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(value))
                        {
                            value = ToUInt(value).ToString("D4");
                            selSecOpt += "<option value='" + value + "'>" + text + "</option>";
                        }
                    }
                }
            }
        }
    }

    private void InitOpts()
    {
        XmlNodeList list = common.GetXMLConst(Server.MapPath("~/LocalFile/file.xml"), "ResvAbsTime");
        if (list != null)
        {
            foreach (XmlNode item in list)
            {
                string text = item.InnerText;
                string value = item.Attributes["value"].Value;
                if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(value))
                {
                    value = ToUInt(value).ToString("D8");
                    selTimeOpt += "<option value='" + value + "'>" + text + "</option>";
                }
            }
        }
    }

    private void FilerRule()
    {
        RESVRULEREQ req = new RESVRULEREQ();
        bool isTheory=(myCourse.dwCourseProperty&(uint)UNICOURSE.DWCOURSEPROPERTY.COURSEPROP_WITHTHEORY)>0;
        req.dwResvPurpose =( isTheory ? (uint)UNIRESERVE.DWPURPOSE.USEFOR_WITHTHEORY : (uint)UNIRESERVE.DWPURPOSE.USEFOR_NOTHEORY);
        purpose = ((uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | req.dwResvPurpose).ToString();
        UNIRESVRULE[] rlt;
        if (m_Request.Reserve.ResvRuleGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            if (!string.IsNullOrEmpty(rlt[0].szOtherCons))
            {
                string[] list = rlt[0].szOtherCons.Split(';');
                for (int i = 0; i < list.Length; i++)
                {
                    string con = list[i];
                    if (!string.IsNullOrEmpty(con))
                    {
                        if (con.Substring(0, 1) == "T")
                        {
                            if (con.Substring(1, 2) == "02")
                            {
                                if (con.Length == 8)
                                {
                                    uint start = ToUInt(con.Substring(3, 2));
                                    uint end = ToUInt(con.Substring(6, 2));
                                    for (uint j = start; j <= end; j++)
                                    {
                                        openWeeks += j + ",";
                                    }
                                }
                            }
                        }
                    }
                }
                if (openWeeks.Length > 0) openWeeks = openWeeks.Substring(0, openWeeks.Length - 1);
            }
        }
    }
    private void CheckTestHours(UNITESTITEM test,uint? hour)
    {
        //TESTITEMMEMRESV[] rlt = GetTestMemResv(test.dwTestItemID, test.dwTestPlanID, test.dwGroupID, hour);

    }
    private void InitLink()
    {
        UNIACCOUNT acc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
        TESTPLANREQ req = new TESTPLANREQ();
        req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
        req.szGetKey = acc.dwAccNo.ToString();
        req.dwYearTerm = yearTerm.dwYearTerm;
        req.szReqExtInfo.szOrderKey = "szCourseName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            planLinkList = "{";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNITESTPLAN plan = rlt[i];
                planLinkList += (i == 0 ? "'" : ",'") + plan.dwTestPlanID + "':'" + plan.szTestPlanName + "'";
            }
            planLinkList += "}";
        }
        else
            MsgBox(m_Request.szErrMsg);
    }

    private void InitTest()
    {
        test_id.Value = testitem_id;
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
        req.szGetKey = testitem_id;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS&&rlt.Length>0)
        {
            UNITESTITEM test = rlt[0];
            valid_period.Value = GetValidPeriod(test);
            group_id.Value = test.dwGroupID.ToString();
            mb_num.Value = GetGroupMemCount(test.dwGroupID).ToString();
            groupName = test.szGroupName;
            testName = test.szTestName;
            curLink = test.dwTestPlanID+","+test.dwTestItemID;
            uint? h = test.dwResvTestHour;
            //CheckTestHours(test,null);
            InitCourse(test.dwCourseID);
        }
    }
    private void InitCourse(uint? id)
    {
        COURSEREQ req = new COURSEREQ();
        req.dwCourseID=id;
        UNICOURSE[] rlt;
        if(m_Request.Reserve.GetCourse(req,out rlt)==REQUESTCODE.EXECUTE_SUCCESS&&rlt.Length==1){
            myCourse = rlt[0];
        }
    }

    private string GetValidPeriod(UNITESTITEM test)
    {
        uint? ret = 0;
        UNIRESERVE[] rsvs = test.ResvInfo;
        foreach (UNIRESERVE info in rsvs)
        {
            ret += info.dwTestHour;
        }
        ret = test.dwTestHour - ret;
        if (ret < 0) ret = 0;
        return ret.ToString();
    }
    private void InitTerm()
    {
        UNITERM term;
        UNITERM[] rlt = InitTermList(out term, Request["term"]);
        curTerm = term.szMemo;
        yearTerm = term;
        Master.Year = (uint)term.dwYearTerm;
        for (int i = 0; i < rlt.Length; i++)
        {
            termList += "<li><a onclick='selTermYear(\"" + rlt[i].dwYearTerm + "\")'>" + rlt[i].szMemo + "</a></li>";
        }
    }
}