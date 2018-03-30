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
    protected string szResvRate = "";
    protected string szUsiingRate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVUSINGRATEREQ vrParameter = new DEVUSINGRATEREQ();

        DEVUSINGRATE vrResultValue;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");           
        }
      
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        }        
        uResponse = m_Request.Report.GetDevUsingRate(vrParameter, out vrResultValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrResultValue.szUsingTable != null && vrResultValue.szUsingTable.Length > 0)
            {
                DEVUSINGTABLE[] vrResult = vrResultValue.szUsingTable;
                for (int i = 360; i < 1440; i = i + 60)
                {
                    string time = i / 60 + ":" + (i % 60).ToString("00");
                    object times = vrResult[i].dwUseTimes;
                    if (times == null)
                    {
                        szResvRate += "<p><span>" + time + "</span><span>0</span></p>";
                    }
                    else {
                        szResvRate += "<p><span>" + time + "</span><span>" + times.ToString() + "</span></p>";
                    }
                }
            }
        }      
    
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
       
    }
}
