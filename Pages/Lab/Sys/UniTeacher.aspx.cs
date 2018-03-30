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
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UNITEACHERREQ vrParameter = new UNITEACHERREQ();
        UNITEACHER[] vrResult;
        string delID = Request["delID"];
        if (delID != null && delID != "")
        {
            UNITEACHER tecacheDel = new UNITEACHER();
            tecacheDel.dwAccNo = Parse(Request["delID"]);
            m_Request.Account.TeacherDel(tecacheDel);
        }
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Account.TeacherGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwAccNo.ToString()+"'>" + vrResult[i].szPID + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                
                m_szOut += "<td>" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vrResult[i].szEmail + "</td>";
              
                    m_szOut += "<td><div class='OPTD'></div></td>";
                
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Account);
        }
        PutBackValue();
    }
   
}
