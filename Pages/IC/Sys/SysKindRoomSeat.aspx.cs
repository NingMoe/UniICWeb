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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_szLab = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        DEVREQ vrParameter = new DEVREQ();
        UNIROOM[] vtRoom=GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        string szRoomIDs="";
        for(int i=0;i<vtRoom.Length;i++)
        {
            if (i < (vtRoom.Length-1))
            {
                szRoomIDs += vtRoom[i].dwRoomID.ToString() + ",";
            }
            else
            {
                szRoomIDs += vtRoom[i].dwRoomID.ToString();
            }
        }
        vrParameter.szRoomIDs = szRoomIDs;
        
        if (Request["delID"] != null)
        {
            Del(Request["delID"]);
        }
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        UNIDEVICE[] vrResult;
        //vrParameter.dwProperty = (uint)(UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE | UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-Name='" + vrResult[i].szDevName.ToString() + "' data-labid='" + vrResult[i].dwLabID.ToString() + "'  data-id='" + vrResult[i].dwDevID.ToString() + "' data-labid='" + vrResult[i].dwLabID.ToString() + "'>" + vrResult[i].dwDevSN.ToString() + "</td>";
                            
                m_szOut += "<td data-id='" + vrResult[i].dwDevID.ToString() + "' data-labid='" + vrResult[i].dwLabID.ToString() + "'>" + vrResult[i].szDevName + "</td>";
                string szDevStat = "";
                if (vrResult[i].dwDevStat == null || vrResult[i].dwDevStat == 0)
                {
                    szDevStat = "正常";
                }
                else if ((((uint)vrResult[i].dwDevStat) & ((uint)UNIDEVICE.DWDEVSTAT.DEVSTAT_DISABLED)) > 0)
                {
                    szDevStat = "禁用";
                }
                m_szOut += "<td>" + szDevStat + "</td>";
                m_szOut += "<td >" + vrResult[i].szKindName + "</td>";             
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "'>" + vrResult[i].szRoomName + "</td>";
               // m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td >" + vrResult[i].szAssertSN + "</td>"; 
                if (vrResult[i].szTagID == null || vrResult[i].szTagID == "")
                {
                    m_szOut += "<td>" + "X" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + "√" + "</td>";
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private void Del(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDEVICE dev;
        if (getDevByID(szID, out dev))
        {
            m_Request.Device.Del(dev);
        }
    }
}
