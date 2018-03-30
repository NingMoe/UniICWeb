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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_manGroupList = "";
	protected void Page_Load(object sender, EventArgs e)
	{
     //   if (IsPostBack)
        {
            GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
            vrGet.dwGroupID = Parse(Request["dwID"]);
            GROUPMEMDETAIL[] vtRes;
            if (m_Request.Group.GetGroupMemDetail(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    m_manGroupList += "<tr>"+"<td>"+vtRes[i].szPID+"</td>"+"<td>" + vtRes[i].szTrueName + "</td>" + "</tr>";
                } 
            }

        }
       
	}
}
