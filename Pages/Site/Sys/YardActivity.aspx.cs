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
        if (Request["delID"] != null)
        {
            DelLab(Request["DelLab"]);
        }
        YARDACTIVITYREQ vrParameter = new YARDACTIVITYREQ();
        YARDACTIVITY[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetYardActivity(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Reserve);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwActivitySN.ToString() + "\">" + vrResult[i].szActivityName+ "</td>";
                m_szOut += "<td>" + "" + "</td>";
            
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        YARDACTIVITY value = new YARDACTIVITY();
        value.dwActivitySN = Parse(szID);
        uResponse = m_Request.Reserve.DelYardActivity(value);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
