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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szKind = "";
    protected string szCLS= "";
    protected string szRoom = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        OOSDETAILREQ vrParameter = new OOSDETAILREQ();
        OOSDETAIL[] vrResult;
        string szID=Request["ID"];
        GetHTTPObj(out vrParameter);
        if (vrParameter.dwOOSStat ==0)
        {
            vrParameter.dwOOSStat = null;
        }
        if (szID != null)
        {
            vrParameter.dwOOSID = Parse(szID);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Assert.OOSDetailGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
    
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td style='text-decoration:underline' class='setDev' data-id=\"" + vrResult[i].dwOOSID.ToString() + "\">" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td>"+vrResult[i].szDevName+"</td>";
                m_szOut += "<td>" + vrResult[i].szModel + "</td>";
                m_szOut += "<td>" +(vrResult[i].dwUnitPrice) + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwPurchaseDate) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szRoomName) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szDeptName) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szApplyName) + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwApplyDate) + "</td>";
                uint uState = (uint)vrResult[i].dwOOSStat;
                if (uState == 1)
                {
                    m_szOut += "<td>" + "已申请" + "</td>";
                }
                else if (uState == 2)
                {
                    m_szOut += "<td>" + "已批准" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + "不通过" + "</td>";
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Assert);
        }
       
         PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        string szRoomIDs = Request["szRoomIDs"];
        string szKindIDs = Request["szKindIDs"];
        string szClassIDs = Request["szClassIDs"];
        if (szRoomIDs != null && szRoomIDs != "")
        {
            PutMemberValue2("szRoomIDs", szRoomIDs);
        }
        if (szKindIDs != null && szKindIDs != "")
        {
            PutMemberValue2("szKindIDs", szKindIDs);
        }
        if (szClassIDs != null && szClassIDs != "")
        {
            PutMemberValue2("szClassIDs", szClassIDs);
        }
    }   
}
