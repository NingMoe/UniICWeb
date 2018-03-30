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
            
            DelFee(Request["delID"]);
        }
        FEEREQ vrParameter = new FEEREQ();
        UNIFEE[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Fee.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwFeeSN.ToString() + "\">" + vrResult[i].dwFeeSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szFeeName + "</td>";
                string szIdent = (uint)vrResult[i].dwIdent == 0 ? "全部" :GetJustNameEqual((uint)vrResult[i].dwIdent, "Fee_Ident");
                m_szOut += "<td>" + szIdent + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPriority.ToString() + "</td>";
                if(vrResult[i].dwPurpose==0)
                {
                m_szOut += "<td>" +"全部"+ "</td>";
                }
                else
                {
                m_szOut += "<td>" + GetJustName(vrResult[i].dwPurpose, "ResvPurpose") + "</td>";
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Fee);
        }
        PutBackValue();
    }
    private void DelFee(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIFEE fee = new UNIFEE();
        fee.dwFeeSN = Parse(szID);
        uResponse = m_Request.Fee.Del(fee);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
