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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szResvRate = "";
    protected string m_TermList = "";
    protected string szUsiingRate = "";
    protected string szBuilding = "";
    protected string szCamp = "";
    protected string szWeeks = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        uint uWeekValue= Parse(Request["dwWeeks"]);
        if (uWeekValue == 0)
        {
            uWeekValue=1;
        }
        for (int i = 1; i < 15; i++)
        {
            szWeeks += GetInputItemHtml(CONSTHTML.option, "",i.ToString(),i.ToString());
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        szBuilding = GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        if (vtBuilding != null)
        {
            for (int i = 0; i < vtBuilding.Length; i++)
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName, vtBuilding[i].dwBuildingID.ToString());
            }
        }
        UNICAMPUS[] vtCamp = GetAllCampus();
        szCamp += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < vtCamp.Length; i++)
        {
            szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
        }


        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVWEEKUSINGRATEREQ vrParameter = new DEVWEEKUSINGRATEREQ();
        GetHTTPObj(out vrParameter);
        DEVWEEKUSINGRATE vrResultValue;
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
           
        }

        if (dwStartDate.Value != "" && uWeekValue!=0)
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwWeeks = uWeekValue;
        }
        if (vrParameter.szBuildingIDs == null || vrParameter.szBuildingIDs == "0")
        {
            vrParameter.szBuildingIDs = null;
        }

        if (vrParameter.szCampusIDs == null || vrParameter.szCampusIDs == "0")
        {
            vrParameter.szCampusIDs = null;
        }
        string szroomid = Request["roomid"];
        if (!string.IsNullOrEmpty(szroomid))
        {
            vrParameter.szRoomIDs = szroomid;
        }
        vrParameter.dwClassKind = null;
        uResponse = m_Request.Report.GetDevWeekUsingRate(vrParameter, out vrResultValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrResultValue.szUsingTable != null && vrResultValue.szUsingTable.Length > 0)
            {
                DEVUSINGTABLE[] vrResult = vrResultValue.szUsingTable;
                for (int i = 0; i<vrResult.Length; i++)
                {
                    string time = i.ToString();// / 60 + ":" + (i % 60).ToString("00");
                    object times = vrResult[i].dwResvTimes;
                    if (times == null)
                    {
                        szResvRate += "<p><span>" + time + "</span><span>0</span></p>";
                    }
                    else
                    {
                        szResvRate += "<p><span>" + time + "</span><span>" + times.ToString() + "</span></p>";
                    }
                }
            }
        }

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);

    }
}
