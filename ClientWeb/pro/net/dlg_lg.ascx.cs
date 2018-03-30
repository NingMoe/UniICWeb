using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_net_dlg_lg : UniClientModule
{
    protected bool mustAct = true;
    protected string msgMustAct = "";
    protected string emailMustAct = "";

    protected string msgFontColor= "black";
    protected string emailFontColor = "black";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szMustAct = GetConfig("mustAct");
        if (szMustAct == ""||szMustAct=="0")
        {
            mustAct = false;
        }
        if (szMustAct == "1" || szMustAct == "4")
        {
            msgMustAct = "required,";
            msgFontColor = "red";
        }
        if (szMustAct == "1" || szMustAct == "3")
        {
            emailMustAct = "required,";
            emailFontColor = "red";
        }

    }
}