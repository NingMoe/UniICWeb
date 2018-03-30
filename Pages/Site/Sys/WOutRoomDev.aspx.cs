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

public partial class Sub_Device : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_szLab = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string szlab = Request["lab"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            /*
            if (string.IsNullOrEmpty(szlab))
            {
                szlab = lab[0].dwLabID.ToString();
            }
             */
            m_szLab += "<option value='0'>选择" + ConfigConst.GCLabName + "</option>";
            for (int i = 0; i < lab.Length; i++)
            {
                m_szLab += "<option value='" + lab[i].dwLabID + "'";
                if (szlab == lab[i].dwLabID.ToString())
                {
                    m_szLab += "checked='checked'";
                }
                m_szLab += ">" + lab[i].szLabName + "</option>";
            }
        }
        //=========================

        DEVREQ vrParameter = new DEVREQ();
        UNIDEVICE[] vrResult;
        if (szlab != null && szlab != "0")
        {
            vrParameter.szLabIDs = szlab;
        }
        vrParameter.dwClassKind = 1024;// (uint)UNIDEVCLS.DWKIND.CLSCOMMONS_ACTIVITY;
        if (Request["delID"] != null)
        {
            DelDevAndRoom(Request["delID"]);

        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"1\" data-id=" + vrResult[i].dwDevID.ToString() + " ManGroupID=" + vrResult[i].dwManGroupID.ToString() + ">" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName + "</td>";
                
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szKindName + "</td>";
                DEVOPENRULE devOpenRule;
                if (getOpenRuleByID(vrResult[i].dwOpenRuleSN.ToString(), out devOpenRule))
                {
                    m_szOut += "<td>" + devOpenRule.szRuleName.ToString() + "</td>";
                }
                else
                {
                    m_szOut += "<td></td>";
                }
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            }
        }
        PutBackValue();
    }
    private void DelDevAndRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDEVICE dev;
        if (getDevByID(szID, out dev))
        {
            UNIROOM room;
            if (GetRoomID(dev.dwRoomID.ToString(), out room))
            {
                if (m_Request.Device.Del(dev) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_Request.Device.RoomDel(room);
                }
            }
        }
    }
}