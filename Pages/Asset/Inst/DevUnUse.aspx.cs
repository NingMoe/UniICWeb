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

        OUTOFSERVICEREQ vrParameter = new OUTOFSERVICEREQ();
        OUTOFSERVICE[] vrResult;
        
        GetHTTPObj(out vrParameter);
        if (vrParameter.dwOOSStat == 0)
        {
            vrParameter.dwOOSStat = null;
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Assert.OutOfSericeGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            XmlCtrl xmlCtrl = new XmlCtrl("ics_data", Server.MapPath(MyVPath + "clientweb/upload/info/xmlData/"));
    
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td style='text-decoration:underline' class='setDev' data-id=\"" + vrResult[i].dwOOSID.ToString() + "\">" + vrResult[i].szOOSInfo + "</td>";
                m_szOut += "<td>"+vrResult[i].szApplyName+"</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwApplyDate) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szApproveName) + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwApproveDate) + "</td>";
                uint uState=(uint)vrResult[i].dwOOSStat;
                if(uState==1)
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
