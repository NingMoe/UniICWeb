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
                        UNIROOM dev = new UNIROOM();
                        if (GetRoomID(nKindID.ToString(), out dev))
                        {
                            dev.dwOpenRuleSN = Parse(szKindID);
                            m_Request.Device.RoomSet(dev, out dev);
                        }
                    }
                }

            }
            MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        }
        if (Request["op"] == "set")
        {

            DEVOPENRULE[] kindList = GetAllOpenRule();
            for (int i = 0; i < kindList.Length; i++)
            {
                szDevKind += GetInputItemHtml(CONSTHTML.option, "", kindList[i].szRuleName, kindList[i].dwRuleSN.ToString());
            }
        }
       
    }
}
