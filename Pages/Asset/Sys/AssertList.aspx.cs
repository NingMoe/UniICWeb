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
    public int nIsAdminSup = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
        uint uManRole = (uint)adminAcc.dwManRole;
        if ((uManRole & ((uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER + (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION + (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER)) > 0)
        {
            nIsAdminSup = 1;
        }

        ASSERTREQ vrParameter = new ASSERTREQ();
        UNIASSERT[] vrResult;
        UNIDEVKIND[] allDevKind = GetAllDevKind();
        UNIROOM[] allRoom= GetAllRoom();
        UNIDEVCLS[] alldevCls = GetAllDevCls();
        string szOp=Request["op"];
        string szID = Request["delID"];
        if (szOp == "setEmpty" && szID != "")
        {
            SetEmpty(szID);
        }
        if (allRoom != null)
        {
            for (int i = 0; i < allRoom.Length; i++)
            {
                szRoom += GetInputItemHtml(CONSTHTML.checkBox, "szRoomIDs", allRoom[i].szRoomName, allRoom[i].dwRoomID.ToString());
            }
        }
        if (allDevKind != null)
        {
            for (int i = 0; i < allDevKind.Length; i++)
            {
                szKind += GetInputItemHtml(CONSTHTML.checkBox, "szKindIDs", allDevKind[i].szKindName, allDevKind[i].dwKindID.ToString());
            }
        }
         if (alldevCls != null)
        {
            for (int i = 0; i < alldevCls.Length; i++)
            {
                szCLS += GetInputItemHtml(CONSTHTML.checkBox, "szClassIDs", alldevCls[i].szClassName, alldevCls[i].dwClassID.ToString());
            }
        }
        
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderMode == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwDevID";
            vrParameter.szReqExtInfo.szOrderMode = "asc";
        }
        if (m_Request.Assert.AssertGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            XmlCtrl xmlCtrl = new XmlCtrl("ics_data", Server.MapPath(MyVPath + "clientweb/upload/info/xmlData/"));
    
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-kindid=\"" + vrResult[i].dwKindID.ToString() + "\" data-id=\"" + vrResult[i].dwDevID.ToString() + "\">" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td class='lnkAssertDevice' data-id=\"" + vrResult[i].dwDevID.ToString() + "\">" + vrResult[i].szDevName + "</td>";
                m_szOut += "<td>" + vrResult[i].szClassName.ToString()+ "</td>";
                m_szOut += "<td>" + vrResult[i].szModel.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUnitPrice + "</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwPurchaseDate) + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
             //   m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwDevStat, "UNIDEVICE_DevStat",false) + "</td>";
                XmlCtrl.XmlInfo info = xmlCtrl.GetXmlContent(vrResult[i].dwKindID.ToString(), "hard3");
                if (info.content != null && info.content.Trim() != "")
                {
                    m_szOut += "<td class='InfoSet' title='查看插图'>"+ "<img width='25px' src='../../../themes/icon_s/19.png'/>" + "</td>";
                }
                else
                {
                    m_szOut += "<td class='InfoSet' title='插入插图'>" + "＋" + "</td>";
                }
               
                if (vrResult[i].szTagID == null || vrResult[i].szTagID == "")
                {
                    m_szOut += "<td class='InfoSet2'>" + "＋" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + "√" + "</td>";
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
    private void SetEmpty(string szDevID)
    {
        UNIDEVICE dev;
        if (getDevByID(szDevID, out dev))
        {
            dev.szTagID = "";
            m_Request.Device.Set(dev, out dev);
        }
    }
}
