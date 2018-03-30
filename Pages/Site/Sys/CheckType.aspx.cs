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
        CHECKTYPEREQ vrParameter = new CHECKTYPEREQ();
        vrParameter.dwMainKind = ((uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SECURITY + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_PUBLICITY + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
        CHECKTYPE[] vrResult;
      
        if (m_Request.Admin.CheckTypeGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Admin);
            for (int i = 0; i < vrResult.Length; i++)
            {
                string szMnagroup = "0";
               
                m_szOut += "<tr>";
                m_szOut += "<td data-manGroupID=\"" + szMnagroup + "\" data-id=\"" + vrResult[i].dwCheckKind.ToString() + "\">" + vrResult[i].szCheckName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwMainKind, "CheckType_MainKind") + "</td>";
                if (vrResult[i].szDeptName == null || vrResult[i].szDeptName == "")
                {
                    m_szOut += "<td>" + "所属部门" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + (vrResult[i].szDeptName) + "</td>";
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CHECKTYPE delValue = new CHECKTYPE();
        delValue.dwCheckKind = Parse(szID);
        //uResponse=m_Request.Admin.CheckTypeGet(lab);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
