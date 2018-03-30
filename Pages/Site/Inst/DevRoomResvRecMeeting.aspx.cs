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
    protected uint uClassKind = 0;
    protected string szDevNameURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uClassKind = Parse(Request["dwClassKind"]);
        szDevNameURL = GetJustNameEqual(uClassKind, "DevClass_dwKind", false);

        RESVRECREQ vrParameter = new RESVRECREQ();
        if (!IsPostBack)
        {
            vrParameter.dwStartDate = GetDate(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" &&dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
        if (devName.Value != "")
        {
            vrParameter.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYDEVID;
            vrParameter.szGetKey = Request["szGetKey"];
        }
        if (dwPID.Value != "")
        {            
            vrParameter.dwAccNo =Parse(Request["szGetKey"]);
        }
        if (vrParameter.dwGetType == null || vrParameter.dwGetType == 0)
        {
            vrParameter.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYALL;
        }
       // vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        //vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        UNIRESVREC[] vrResult;       
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Report.ResvRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {          
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {                
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "(" + vrResult[i].szPID + ")" + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwPreBegin, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwPreEnd, "MM-dd HH:mm") + "</td>";                
                m_szOut += "<td>" + vrResult[i].szLabName+ "</td>";               
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();
    }   
}
