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
        SYSFUNCRULEREQ vrParameter = new SYSFUNCRULEREQ();
        SYSFUNCRULE[] vrResult;   
        string szID=Request["id"];
        if (szID != null && szID != "")
        {
            Del(szID);
        }
        if (m_Request.System.SysFuncRuleGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"1\" data-id=" + vrResult[i].dwSFRuleID.ToString() + ">" + vrResult[i].szSFRuleName+ "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szSFName + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwAuthType, "AuthType") + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
       
        PutBackValue();
    }
    private void Del(string szID)
    {
        /*
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        SYSFUNCRULE room = new SYSFUNCRULE();
        room.dwSFRuleID = Parse(szID);
        uResponse = m_Request.System.SysFuncRuleSet.RoomDel(room);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
         * */
    }
}
