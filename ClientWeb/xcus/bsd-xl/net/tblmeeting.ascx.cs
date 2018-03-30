using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientWeb_xcus_bsd_xl_net_tblmeeting : System.Web.UI.UserControl
{
    bool isLong = false;
    public bool IsLong
    {
        get { return isLong; }
        set { isLong = value; }
    }
    string devId = "";
    public string DevId
    {
        get { return devId; }
        set { devId = value; }
    }
    string devClassId = "";
    public string DevClassId
    {
        get { return devClassId; }
        set { devClassId = value; }
    }
    string classKind = "";
    public string ClassKind
    {
        get { return classKind; }
        set { classKind = value; }
    }
    string mode = "dm";
    public string Mode
    {
        get { return mode; }
        set { mode = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}