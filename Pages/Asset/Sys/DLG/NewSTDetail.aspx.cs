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
	protected void Page_Load(object sender, EventArgs e)
	{       
       // if (IsPostBack)
        {
            STDETAILREQ vrGet=new STDETAILREQ();
            STDETAIL[] vtRes;
            GetHTTPObj(out vrGet);
            vrGet.dwSTID = Parse(Request["id"]);
            if (m_Request.Assert.STDetailGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "入库", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox("入库成功", "入库成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
        m_Title = "资产入库";
	}
   
}