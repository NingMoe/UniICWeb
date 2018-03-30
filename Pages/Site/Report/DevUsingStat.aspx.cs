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
    protected string m_TermList = "";
    protected string sz_Room = "";
    protected string sz_Kind = "";
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
       
        UNIROOM[] allRoom = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT + (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
        if (allRoom != null)
        {
            sz_Room += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < allRoom.Length; i++)
            {
                if (i == 0)
                {
                    sz_Room += GetInputItemHtml(CONSTHTML.option, "", allRoom[i].szRoomName, allRoom[i].dwRoomID.ToString(), true);
                }
                else
                {
                    sz_Room += GetInputItemHtml(CONSTHTML.option, "", allRoom[i].szRoomName, allRoom[i].dwRoomID.ToString());
                }
            }
        }
        UNIDEVKIND[] vtDevKind = GetAllDevKind();
        if (vtDevKind != null && vtDevKind.Length > 0)
        {
            for (int i = 0; i < vtDevKind.Length; i++)
            {
                sz_Kind += GetInputItemHtml(CONSTHTML.option, "", vtDevKind[i].szKindName.ToString(), vtDevKind[i].dwKindID.ToString());
            }
        }
        REPORTREQ vrParameter = new REPORTREQ();
        GetHTTPObj(out vrParameter);
        DEVSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }

        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
        uint uYeartermNow = Parse(szYearTerm);

        if (termList != null)
        {
            m_TermList += GetInputItemHtml(CONSTHTML.option, "", "选择学期", "0");
            for (int i = 0; i < termList.Length; i++)
            {
                m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                uint uYearTermState = (uint)termList[i].dwStatus;
                if (szYearTerm == null && (uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    uYeartermNow = (uint)termList[i].dwYearTerm;
                }
            }
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        if (ConfigConst.GCICTypeMode == 1)
        {
            UNITERM[] termnow = GetTermByID(Parse(Request["dwYearTerm"]));
            if (termnow != null && termnow.Length > 0)
            {
                // vrParameter.dwStartDate = termnow[0].dwBeginDate;
                // vrParameter.dwEndDate = termnow[0].dwEndDate;
            }
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
        if (m_Request.Report.GetDevStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Report);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwDevID.ToString() + ">" + vrResult[i].szDevName + "</td>";
                string szClsName = vrResult[i].szClassName;
                if (szClsName == null)
                {
                    szClsName = "";
                }
                m_szOut += "<td>" + szClsName.ToString() + "</td>";
                // m_szOut += "<td class='lnkTeacher' data-id='" + vrResult[i].dwAttendantID + "'>" + vrResult[i].szAttendantName + "</td>";
                uint uUseTime = (uint)vrResult[i].dwTotalUseTime;

                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                //m_szOut += "<td>" + vrResult[i].szModel+"/"+ vrResult[i].szSpecification  + "</td>";
                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPIDNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";
                m_szOut += "</tr>";
            }
            
        }
        PutBackValue();
    }
}
