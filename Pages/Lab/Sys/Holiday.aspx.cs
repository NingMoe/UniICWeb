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
        HOLIDAYREQ vrGet = new HOLIDAYREQ();
        UNIHOLIDAY[] vtRes;
        if (Request["delName"] != null)
        {
            Del(Request["delName"]);
        }
        if(m_Request.Admin.HolidDayGet(vrGet,out vtRes)==REQUESTCODE.EXECUTE_SUCCESS)
        {    for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vtRes[i].szName.ToString() + "\">" + vtRes[i].szName.ToString() + "</td>";               
                m_szOut += "<td>" +GetDateStr(vtRes[i].dwStartDay)+ "</td>";
                m_szOut += "<td>" +GetDateStr(vtRes[i].dwEndDay)+ "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Admin);
        }
        PutBackValue();        
    }
    private void Del(string szName)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIHOLIDAY delRule = new UNIHOLIDAY();
        delRule.szName = szName;
        uResponse=m_Request.Admin.HolidDayDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
