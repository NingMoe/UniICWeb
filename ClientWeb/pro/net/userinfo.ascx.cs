using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_net_userinfo : UniClientModule
{
    protected UNIACCOUNT curAcc;
    protected string checkAlert = "checked";
    protected string changePsw = "none";

    protected string msgMustAct = "";
    protected string emailMustAct = "";

    protected string msgFontColor = "black";
    protected string emailFontColor = "black";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["LOGIN_ACCINFO"]!=null){
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            ACCREQ req = new ACCREQ();
            req.dwAccNo = acc.dwAccNo;
            UNIACCOUNT[] rlt;
            if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                Session["LOGIN_ACCINFO"] =curAcc= rlt[0];
                if (GetConfig("needChangePsw") == "1" && curAcc.szLogonName.ToLower() != "staadmin001" && curAcc.szLogonName.ToLower() != "sysadmin")
                {
                    changePsw = "";
                }
                if ((rlt[0].dwKind & (uint)UNIACCOUNT.DWKIND.EXTKIND_NOMSG) > 0)
                checkAlert = "";
            }
        }
        string szMustAct = GetConfig("mustAct");
       
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