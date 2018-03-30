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
            
            DelSample(Request["delID"]);
        }
        SAMPLEINFOREQ vrParameter = new SAMPLEINFOREQ();
        SAMPLEINFO[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetSampleInfo(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\""+vrResult[i].dwSampleSN.ToString()+"\">" + vrResult[i].szSampleName + "</td>";
                m_szOut += "<td>" + vrResult[i].szUnitName.ToString() + "</td>";
                m_szOut += "<td>" + GetFee((uint)vrResult[i].dwUnitFee2) + "</td>";
                m_szOut += "<td>" + GetFee((uint)vrResult[i].dwUnitFee3) + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();
    }
    private void DelSample(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        SAMPLEINFO sample = new SAMPLEINFO();
        sample.dwSampleSN= Parse(szID);
        uResponse = m_Request.Reserve.DelSampleInfo(sample);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
