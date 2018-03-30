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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        SFROLEINFOREQ vrPar = new SFROLEINFOREQ();
        GetHTTPObj(out vrPar);
        vrPar.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER;
        if (vrPar.dwStatus == null || ((uint)vrPar.dwStatus) == 0)
        {
            vrPar.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
        }
        GetPageCtrlValue(out vrPar.szReqExtInfo);
        SFROLEINFO[] vtRes;
        uResponse = m_Request.System.SFRoleGet(vrPar,out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"1\" data-id=" + vtRes[i].dwApplyID.ToString() + ">" + vtRes[i].szTrueName + "(" + vtRes[i].szPID + ")" + "</td>";
                m_szOut += "<td>" + vtRes[i].szDeptName + "</td>";
                m_szOut += "<td>" + vtRes[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vtRes[i].szTutorName + "</td>";
                m_szOut += "<td>" + Get1970Date(vtRes[i].dwApplyTime) + "</td>";
                m_szOut += "<td>" + (vtRes[i].szLabName) + "</td>";
                m_szOut += "<td>" + GetJustName((vtRes[i].dwStatus), "Reserve_Status") + "</td>";
                m_szOut += "<td>" + Get1970Date(vtRes[i].dwCheckTime) + "</td>";
                string szOp = "";
                if (((uint)vtRes[i].dwStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO)) > 0)
                {
                    szOp = "OPTD";
                }
                else
                {
                    szOp = "OPTD OPTD2";
                }
                m_szOut += "<td><div class='"+szOp+"'></div></td>";
                m_szOut += "</tr>";
            }
             UpdatePageCtrl(m_Request.System);
        }
        PutBackValue();
        
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
     
    }   
}
