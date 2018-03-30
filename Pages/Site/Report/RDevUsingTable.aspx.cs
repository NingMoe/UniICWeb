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
    protected void Page_Load(object sender, EventArgs e)
    {
       
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
        DEVUSINGRATEREQ vrParameter = new DEVUSINGRATEREQ();
        GetHTTPObj(out vrParameter);
        DEVUSINGRATE vrResultValue;
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        }
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwTotalUseTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        if (vrParameter.dwClassKind == null || vrParameter.dwClassKind == 0)
        {
            vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
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
        uResponse = m_Request.Report.GetDevUsingRate(vrParameter, out vrResultValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrResultValue.szUsingTable != null && vrResultValue.szUsingTable.Length > 0)
            {
                DEVUSINGTABLE[] vrResult = vrResultValue.szUsingTable;
                for (int i = 360; i < 1440; i = i + 60)
                {
                    string time = i / 60 + ":" + (i % 60).ToString("00");
                    object times = vrResult[i].dwUseTimes;
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
