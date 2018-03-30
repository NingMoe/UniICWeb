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

        LABREQ vrParameter = new LABREQ();
        vrParameter.dwLabClass = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        UNILAB[] vrResult;      
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.LabGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"1\" data-id=" + vrResult[i].dwLabID.ToString()+">" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";                                
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
    private void DelRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNILAB lab = new UNILAB();
        lab.dwLabID = Parse(szID);
        uResponse = m_Request.Device.LabDel(lab);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
