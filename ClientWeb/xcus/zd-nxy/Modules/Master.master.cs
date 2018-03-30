using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_Modules_Master : UniClientMaster
{
    private string banner = "";
    public string Banner
    {
        get { return banner; }
        set { banner = value; }
    }
    protected string ClassNav = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        banner = "<div class='b-img'></div>";
        InitClassList();
    }

    private void InitClassList()
    {
        DEVCLSREQ req = new DEVCLSREQ();
        UNIDEVCLS[] rlt;
        if (m_Request.Device.DevClsGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (rlt[i].dwClassID == 54)
                {
                    continue;
                }
                ClassNav += "<li><a href='DevList.aspx?cls=" + rlt[i].dwClassID + "'>" + rlt[i].szClassName + "</a></li>";
            }
        }
    }
}
