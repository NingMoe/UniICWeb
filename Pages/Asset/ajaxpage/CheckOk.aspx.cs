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
using UniStruct;
using System.Xml;
using System.Text;


public partial class Page_Search : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szResvID = Request["id"];    
        string szMemo = Request["memo"];
        string szOwerID = Request["ownerID"];
        string szOwnerName = Request["ownerName"];        
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;       
        ADMINCHECK setCheck = new ADMINCHECK();
        setCheck.dwApplicantID = Parse(szOwerID);     
        setCheck.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
        setCheck.dwSubjectType = (uint)ADMINCHECK.DWSUBJECTTYPE.CHECK_RESV;
        setCheck.dwSubjectID = Parse(szResvID);
        setCheck.szApplicantName = szOwnerName;
        uResponse = m_Request.Admin.AdminCheck(setCheck);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("success");
            }
            else
            {
                Response.Write(m_Request.szErrMessage.ToString());
            }
        }
    }
   
}
