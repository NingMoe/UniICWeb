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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            
            DelLab(Request["delID"]);
        }
        ROOMGROUPREQ vrParameter = new ROOMGROUPREQ();
        ROOMGROUP[] vrResult;
        vrParameter.dwRoomID = 132;
   
        if (m_Request.Device.RoomGroupGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\""+vrResult[i].dwRGID.ToString()+"\">" + vrResult[i].szRGName + "</td>";
                m_szOut += "<td >" + vrResult[i].dwRoomNum.ToString() + "</a></td>";
                string szRoomNameList = "";
                for (int j = 0; j < vrResult[i].rgMember.Length; j++)
                {
                    szRoomNameList += vrResult[i].rgMember[j].szRoomName + ",";
                }
                m_szOut += "<td>" + szRoomNameList + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ROOMGROUP roomGroup = new ROOMGROUP();
        roomGroup.dwRGID = Parse(szID);
        uResponse = m_Request.Device.RoomGroupDel(roomGroup);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
