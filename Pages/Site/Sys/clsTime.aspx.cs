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
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CTSREQ vrParameter = new CTSREQ();       
        CLASSTIMETABLE[] vrResult;
        if (Request["delID"] != null)
        {
            DelTerm(Request["delID"]);
        }      
        if (m_Request.Reserve.GetClassTimeTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwSecIndex.ToString() + "\">" + vrResult[i].dwSecIndex + "</td>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwSecIndex.ToString() + "\">" + vrResult[i].szSecName + "</td>";
                m_szOut += "<td>" + GetTimeStr(vrResult[i].dwBeginTime) + "</td>";
                
                m_szOut += "<td>" +GetTimeStr(vrResult[i].dwEndTime)+ "</td>";                
                string szDivOPTD = "OPTD";
              
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();        
    }
    private void DelTerm(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CTSREQ vrParameter = new CTSREQ();       
        CLASSTIMETABLE[] vrResult;       
        if (m_Request.Reserve.GetClassTimeTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (vrResult[i].dwSecIndex.ToString() != szID)
                {
                    list.Add(vrResult[i]);
                }
            }
            CLASSTIMETABLE[] res2=new CLASSTIMETABLE[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                res2[i] = new CLASSTIMETABLE();
                res2[i] = (CLASSTIMETABLE)list[i];
            }
            m_Request.Reserve.SetClassTimeTable(res2);
        }
    }
}
