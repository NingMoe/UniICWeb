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
        if (Request["room"] != null)
        {
            m_szOpts += "机房号：" + Request["room"];
        }
        if (Request["delID"] != null)
        {
            
            DelLab(Request["delID"]);
        }
        FULLLABREQ vrParameter = new FULLLABREQ();
        FULLLAB[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.FullLabGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\""+vrResult[i].dwLabID.ToString()+"\">" + vrResult[i].szLabSN + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看"+ConfigConst.GCLabName+"信息'><a href=\"#\">" + vrResult[i].szLabName + "</a></td>";
                m_szOut += "<td>" + vrResult[i].dwTotalDevNum.ToString()+ "</td>";
                m_szOut += "<td>" + vrResult[i].dwUsableDevNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwIdleDevNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName.ToString() + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNILAB lab = new UNILAB();
        lab.dwLabID=Parse(szID);
        uResponse=m_Request.Device.LabDel(lab);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
