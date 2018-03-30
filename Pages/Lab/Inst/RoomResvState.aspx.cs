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

public partial class Sub_Course : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string m_szCamp = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevCls = "";
    protected string m_szDevKind = "";
    protected string m_szDevStat = "";
    protected uint uClassKind = 0;
    protected string szDevNameURL = "";
    protected uint selectDate = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        uClassKind = Parse(Request["dwClassKind"]);
		string szTestPlanID=Request["testplanid"];
		PutMemberValue("testplanid",szTestPlanID);
        szDevNameURL = GetJustNameEqual(uClassKind, "DevClass_dwKind", false);
        if (Session["selectDate"] == null)
        {
            selectDate = Parse(DateTime.Now.ToString("yyyyMMdd"))-100;
        }
        else
        {
            selectDate = Parse(Session["selectDate"].ToString());
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
      
    }   
}
