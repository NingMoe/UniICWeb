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
using System.Data.Sql;
using System.Data.SqlClient;
public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        string szConSql = ConfigurationManager.ConnectionStrings["constr"].ToString();
        SqlConnection conn = new SqlConnection(szConSql);

        ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
        if (!IsPostBack)
        {
            vrParameter.dwStartDate = GetDate(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "")
        {
            vrParameter.dwStartDate = GetDate(dwStartDate.Value);
        }

        UNIACTIVITYPLAN[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (Request["delID"] != null)
        {
            Del(Request["delID"]);
        }
        uint uState = Parse(Request["dwStatue1"]) + Parse(Request["dwStatue2"]);
        if (uState != 0)
        {
            vrParameter.dwStatus = uState;
        }
        if (m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Reserve);
            for (int i = 0; i < vrResult.Length; i++)
            {
                UNIDEVICE devset;
                string szDevName = "";
                if (getDevByID(vrResult[i].dwDevID.ToString(), out devset))
                {
                    szDevName = devset.szDevName;
                }
                string activityplan = vrResult[i].szActivityPlanName;
                if (activityplan.Length > 8)
                {
                    activityplan = activityplan.Substring(0, 8);
                }
                UNIRESERVE setResv;
                if (GetResvByID(vrResult[i].dwResvID.ToString(), out setResv))
                {

                }
                m_szOut += "<tr>";
                m_szOut += "<td data-groupid='" + vrResult[i].dwGroupID.ToString() + "' title='" + vrResult[i].szActivityPlanName + "' data-id=" + vrResult[i].dwActivityPlanID.ToString() + ">" + activityplan + "</td>";
                m_szOut += "<td  class='lnkDevice' data-id='" + vrResult[i].dwDevID + "'>" + szDevName + "</td>";
                m_szOut += "<td>" + vrResult[i].szPresenter.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szContact.ToString() + "</td>";
                m_szOut += "<td >" + vrResult[i].szTel + "</td>";
                m_szOut += "<td >" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td >" + vrResult[i].dwEnrollUsers + "</td>";//已申请人数 
                m_szOut += "<td >" + vrResult[i].dwRealUsers + "</td>";// 
                string szDateTime = GetDateStr(vrResult[i].dwActivityDate) +" "+ GetTimeStr(vrResult[i].dwBeginTime) +"至"+ GetTimeStr(vrResult[i].dwEndTime);
                m_szOut += "<td >" + szDateTime + "</td>";
                uint uStatue = (uint)vrResult[i].dwStatus;
                if ((uStatue & 2) > 0 && (uStatue & 4) > 0)
                {
                    vrResult[i].dwStatus = uStatue - 4;
                }
                if (uStatue == 3)
                {
                    vrResult[i].dwStatus = 4;
                }
                m_szOut += "<td >" + GetJustName(vrResult[i].dwStatus, "activity_status") + "</td>";
              if (((uint)vrResult[i].dwStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING)) > 0)
                {
                    m_szOut += "<td><div class='OPTD'></div></td>";
                }
               else
              {
                    m_szOut += "<td><div class='OPTD OPTD1'></div></td>";
                }
                m_szOut += "</tr>";
            }
        }
        PutBackValue();
    }
    private void Del(string szID)
    {
        ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
        vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        vrGet.szGetKey = szID;
        UNIACTIVITYPLAN[] vtRes;
        if (m_Request.Reserve.GetActivityPlan(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            uint uResvID=(uint)vtRes[0].dwResvID;
            UNIRESERVE Resv = new UNIRESERVE();
            if (GetResvByID(uResvID.ToString(), out Resv))
            {
                m_Request.Reserve.Del(Resv);
            }
            m_Request.Reserve.DelActivityPlan(vtRes[0]);
            
        }
    }
}
