using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Collections;

public partial class ClientWeb_pro_net_calendar : UniClientModule
{
    UNIRESERVE.DWPURPOSE usetype = UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
    bool isLong = false;
    public bool IsLong
    {
        get { return isLong; }
        set { isLong = value; }
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
    string mode = "d";
    public string Mode
    {
        get { return mode; }
        set { mode = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}