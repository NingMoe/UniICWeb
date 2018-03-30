using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
      //  string szTemp = Decode("YXRnMTc4Z2hqa285ODJnbTIyMDgwMjE5ODAwOTE2MTcxMGZnaGVydDYyMXl1aWxzcDExMTExMXB3");
      //123
      //345
        string szINFO = "10035209X(J@L*!IA20160322";
        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(szINFO, "MD5");
       

        //社科院单点登录
        string szUserInfo = Request.QueryString["userinfo"];
        if (szUserInfo != null)
        {
            string szA = "atg178ghjko982gm";
            string szB = "fghert621yuilsp";
            string szC = "pwfgcvb";
            Logger.trace("szUserInfo=" + szUserInfo);
            string szCodwIn = Decode(szUserInfo);
            Logger.trace("szCodwIn=" + szCodwIn);
            int nA = szCodwIn.IndexOf(szA) + szA.Length;
            Logger.trace("nA=" + nA);
            int nB = szCodwIn.IndexOf(szB) + szB.Length;
            Logger.trace("nB=" + nB);
            int nC = szCodwIn.IndexOf(szC) + szC.Length;
            Logger.trace("nC=" + nC);
            string logonnameCode = szCodwIn.Substring(nA, szCodwIn.IndexOf(szB) - nA);
            string passwordCode = szCodwIn.Substring(nB, szCodwIn.IndexOf(szC) - nB);
            Logger.trace("logonnameCode=" + logonnameCode);
            Logger.trace("passwordCode=" + passwordCode);

            LoginUseInfo loginUserInfo = new LoginUseInfo();
            loginUserInfo.szLogoName = logonnameCode;
            loginUserInfo.szPassword = "uniFound808";
            Session["LoginUseInfo"] = loginUserInfo;

        }
        Response.Redirect("pages/default.aspx" + HttpContext.Current.Request.Url.Query);

       /*
            string szStartTime = ddlStartTime.SelectedItem.Text.ToString();
            string szEndTime = ddlEndTime.SelectedItem.Text.ToString();

        for (int i = 8; i < 22; i++)
        {
            for (int j = 0; j <= 50; j = j + 10)
            {
                ListItem item1 = new ListItem(i.ToString("00") + ":" + j.ToString("00"), i.ToString("00") + j.ToString("00"));
                ddlStartTime.Items.Add(item1);
            }
        }
        for (int i = 8; i < 22; i++)
        {
            for (int j = 0; j <= 50; j = j + 10)
            {
                ListItem item1 = new ListItem(i.ToString("00") + ":" + j.ToString("00"), i.ToString("00") + j.ToString("00"));
                ddlEndTime.Items.Add(item1);
            }
        }
        */
    }
    public string Encode(string str)
    {
        byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(encbuff);
    }
    public string Decode(string str)
    {
        byte[] decbuff = Convert.FromBase64String(str);
        return System.Text.Encoding.UTF8.GetString(decbuff);
    }
}