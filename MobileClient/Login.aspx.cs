using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class _Default : PageBase
{
    protected string szMsg = "";
    protected string szFormID = "null";
    protected string szPasswd = "uniFound808";
    protected override void OnLoadComplete(EventArgs e)
    {
       // szFormID = DateTime.Now.Ticks.ToString();
       // Session["FID"] = szFormID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
Session.RemoveAll();
        string szSN=Request["sn"];
        if (szSN != null)
        {
            Response.Redirect("../loginmall.aspx?sn="+szSN);
            Response.End();
            Logger.trace("szsn="+szSN);
            GetUserInfoFromUrl(szSN);
        }
        string szOp = Request["op"];
        if (szOp == "out" && (Request["szLogonName"] == null || Request["szLogonName"]==""))
        {
            return;
        }
        string szSignKey = Request["signkey"];
        string szuid = Request["uid"];
        if ((!string.IsNullOrEmpty(szSignKey)) && (!string.IsNullOrEmpty(szuid)))
        {
            Logger.trace("szSignKey=" + szSN+";szuid="+ szuid);
Logger.trace("szuid="+ szuid);
            GetUserInfoFromUrl(szuid,szSignKey);
        }

        string szUrl = Request["url"];
        string szCode = Request["code"];
        string szOpenID = Request["openid"];
       
        //   Response.Write("url=" + szUrl + ";szcode=" + szCode + ";szopenid=" + szOpenID);
          Session["LoginUseInfo"] = null;
        /*
        cn.edu.tongji.lib.Service tjser = new cn.edu.tongji.lib.Service();
        if (szCode != null && szCode != "")
        {
            szOpenID = tjser.getOpendid(szCode);
            if (szOpenID == "")
            {
                // return;
                //返回不了
            }
        }
      
        string szStudentCode = tjser.getStudentcode(szOpenID);  
        //  Response.Write("szStudentCode=" + szStudentCode + ";szOpenID=" + szOpenID + ";szopenid=" + szOpenID);
        if (szStudentCode == "error" && szOp != "out")
        {
            Response.Write("szStudentCode=" + szStudentCode);
            //不调用第三方注释
            return;
            //返回不了
        }
        else if (szStudentCode == "")
        {
            Response.Redirect("http://lib.tongji.edu.cn/wxauth/default.aspx?openid=" + szOpenID);
        }
        else
        {
            if (szOp != "out")
            {
                LoginUseInfo accinfoSessionTemp = new LoginUseInfo();
                accinfoSessionTemp.szLogoName = szStudentCode;
                accinfoSessionTemp.szPassword = "uniFound808";
                Session["LoginUseInfo"] = accinfoSessionTemp;
            }
            else
            {
                Session["LoginUseInfo"] = null;

            }
        }
       
    */
        if (IsPostBack)
        {
            Response.Redirect("index.aspx");
            return;
        }
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
       
       LOCALUSER curUser=new LOCALUSER();
        if (szOp != "out")
        {
           Logger.trace("调用GetUser（）");
            curUser = GetUser();
             if (curUser == null || !string.IsNullOrEmpty(curUser.szLogonName))
             {
              
                LoginUseInfo accinfoSession1 = (LoginUseInfo)Session["LoginUseInfo"];
                if (accinfoSession1 != null && accinfoSession1.szLogoName != "")
                {
                    Logger.trace("因为调用GetUser（）跳转，logonname="+ accinfoSession1.szLogoName);
                }
                Response.Redirect("Index.aspx");
             }
        }

        if(Session["LoginUseInfo"]!=null)
        {
            LoginUseInfo accinfoSession = (LoginUseInfo)Session["LoginUseInfo"];
            if (accinfoSession != null && accinfoSession.szLogoName != "")
            {
                Logger.trace("Session['LoginUseInfo']不等于空"+ accinfoSession.szLogoName.ToString());
                curUser.szLogonName = accinfoSession.szLogoName;
                curUser.szPassword = accinfoSession.szPassword;
                if (Logon(curUser, out szMsg))
                {
                    Logger.trace("Session['LoginUseInfo']login ok"+ accinfoSession.szLogoName.ToString());
                    LocalSQL.SetUser(curUser);
 
                    Response.Redirect("index.aspx");
                }
            }
        }
         string szLoginAllType = System.Configuration.ConfigurationManager.AppSettings["loginAllType"].ToString();

         if (szLoginAllType.ToLower() == "url")
         {
             Session["clientUrl"] = "MobileClient/login.aspx";
             Response.Redirect("../loginall.aspx");
         }

        string sfid = (string)Session["FID"];

        if (Request["szLogonName"] != null && Request["szLogonName"].ToString() != "")
        {
            curUser.szOpenID = Request["szLogonName"];

            curUser.szLogonName = Request["szLogonName"];
            if (curUser.szPassword == null || curUser.szPassword == "")
            {
                curUser.szPassword = Request["szPassword"];
            }
            if (string.IsNullOrEmpty(curUser.szLogonName))
            {
                szMsg = "登录名不能为空";
            }
            else
            {
                if (Logon(curUser, out szMsg))
                {
                    LocalSQL.SetUser(curUser);
                    Response.Redirect("index.aspx");
                }
            }

        }

        //szFormID = DateTime.Now.Ticks.ToString();
        //Session["FID"] = szFormID;
    }
    private readonly string strDesKey = "EW2sdfkj";
    private void GetUserInfoFromUrl(string szSN)
    {
        string szInfo = Decrypt_DES(szSN,strDesKey);

        string szLogonNameS="<uid>";
        string szLogonNameE="</uid>";
        string szPasswdS="<pwd>";
        string szPasswdE="</pwd>";
        int nLogonNameS = szInfo.IndexOf(szLogonNameS);
        int nLogonNameE = szInfo.IndexOf(szLogonNameE);
        string szLogonName="";
        try
        {
            szLogonName = szInfo.Substring(nLogonNameS + szLogonNameS.Length, nLogonNameE - nLogonNameS - szLogonNameS.Length);
        }
        catch { 

        }
        int nPasswdS = szInfo.IndexOf(szPasswdS);
        int nPasswdE = szInfo.IndexOf(szPasswdE);
        string szPasswd = "";
        try
        {
            szPasswd = szInfo.Substring(nPasswdS + szPasswdS.Length, nPasswdE - nPasswdS - szPasswdS.Length);
        }
        catch
        {

        }
        if (szLogonName != null&&szLogonName!="")
        {
            LoginUseInfo info=new LoginUseInfo();
            info.szPassword=szPasswd;
            info.szLogoName=szLogonName;
            Session["LoginUseInfo"] = info;
        }
    }
    private bool GetUserInfoFromUrl(string uid,string szSignKey)
    {
        string szKey = "G(Z@L*!IA";
        string szDate = DateTime.Now.ToString("yyyyMMdd");

        string ma5 = uid + szKey + szDate;
        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
        if (szSignKey == EnPswdStr)
        {
            LoginUseInfo info = new LoginUseInfo();
            info.szPassword = szPasswd;
            info.szLogoName = uid;
            Session["LoginUseInfo"] = info;

            Logger.trace(uid + "微信跳转登录成功");
            return true;
        }
        else
        {
            Logger.trace(uid + "微信跳转登录失败；本地加密:"+ EnPswdStr+";传入加密值:"+ szSignKey);
            return false;
        }
        
    }
    public String Encrypt_DES(string Text, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        des.Mode = System.Security.Cryptography.CipherMode.ECB;
        byte[] inputByteArray;
        inputByteArray = Encoding.Default.GetBytes(Text);
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }


    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="str">要解密字符串</param>
    /// <returns>返回解密后字符串</returns>
    public String Decrypt_DES(string Text, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        des.Mode = System.Security.Cryptography.CipherMode.ECB;
        int len;
        len = Text.Length / 2;
        byte[] inputByteArray = new byte[len];
        int x, i;
        for (x = 0; x < len; x++)
        {
            i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Encoding.Default.GetString(ms.ToArray());
    }
}
