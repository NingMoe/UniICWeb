using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_Default : UniClientPage
{
    protected bool isMobile = false;
    //aaa
    protected void Page_Load(object sender, EventArgs e)
    {
        isMobile = IsMoblie();
        Logger.trace("RawUrl："+Request.RawUrl);
    }
    public bool IsMoblie()
    {
        string agent = (Request.UserAgent + "").ToLower().Trim();

        if (agent == "" ||
            agent.IndexOf("mobile") != -1 ||
            agent.IndexOf("mobi") != -1 ||
            agent.IndexOf("nokia") != -1 ||
            agent.IndexOf("samsung") != -1 ||
            agent.IndexOf("sonyericsson") != -1 ||
            agent.IndexOf("mot") != -1 ||
            agent.IndexOf("blackberry") != -1 ||
            agent.IndexOf("lg") != -1 ||
            agent.IndexOf("htc") != -1 ||
            agent.IndexOf("j2me") != -1 ||
            agent.IndexOf("ucweb") != -1 ||
            agent.IndexOf("opera mini") != -1 ||
            agent.IndexOf("mobi") != -1 ||
            agent.IndexOf("android") != -1 ||
            agent.IndexOf("iphone") != -1)
        {
            //终端可能是手机
            return true;
        }
        return false;
    }
}