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
    protected string szDevKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        
        if (IsPostBack)
        {
            string szKindID = Request["devKind"];
            string szKindIDs=Request["id"];
            string[] szKindIDList = szKindIDs.Split(',');
            if (szKindID != null && szKindID != "")
            {
                for (int i = 0; i < szKindIDList.Length; i++)
                {
                    uint nKindID = Parse(szKindIDList[i]);
                    if (nKindID != 0)
                    {
                        UNIDEVICE dev = new UNIDEVICE();
                        if (getDevByID(nKindID.ToString(), out dev))
                        {
                            dev.dwKindID = Parse(szKindID);
                            m_Request.Device.Set(dev, out dev);
                        }
                    }
                }

            }
            MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        }
        if (Request["op"] == "set")
        {
            UNIDEVKIND[] kindList = GetAllDevKind();
            for (int i = 0; i < kindList.Length; i++)
            {
                szDevKind += GetInputItemHtml(CONSTHTML.option, "", kindList[i].szKindName, kindList[i].dwKindID.ToString());
            }
        }
       
    }
}
