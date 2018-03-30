using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;
using UniWebLib;
public partial class LoginAll : UniPage
{
    string Rurl = "clientweb/default.aspx";
    bool bADDZeo = false;
    public static object obj = new object();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Logger.trace("开始单点登录");
        string szOP = Request["op"];
        string logouturl = Request["logouturl"];
        string szsc = Request["page"];
        string szLoginOutUrl = "";
        if (szOP != null && szOP != "" && szOP.ToLower() == "logout")
        {
           
            if (!string.IsNullOrEmpty(logouturl))
            {
                szLoginOutUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl)) + logouturl;
            }
            else
            {
               // szLoginOutUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl)) + MyVPath + "default.aspx";
               // szLoginOutUrl = HttpContext.Current.Request.Url.Host + "/loginout.aspx";
                szLoginOutUrl = Request.Url.Authority + "/login.aspx"; //HttpContext.Current.Request.Url.Host + "/login.aspx";

            }
            string loginServerURL = System.Configuration.ConfigurationManager.AppSettings["LoginAllOutURL"].ToString();
            Logout();
            Session.RemoveAll();
            if (loginServerURL != null && loginServerURL != "" && szLoginOutUrl != null && szLoginOutUrl != "")
            {
                string szloginOutUrlInfo = HttpContext.Current.Request.Url.Host;
                if (szloginOutUrlInfo.IndexOf("http") > -1)
                {
                    Response.Redirect(loginServerURL + "?service=" + HttpContext.Current.Request.Url.Host);
                    Response.End();
                }
                else
                {
                    Response.Redirect(loginServerURL + "?service=http://" + HttpContext.Current.Request.Url.Host);
                    Response.End();
                }
            }
            else {
                Response.Redirect("login.aspx");
                Response.End();
            }
        }
        else
        {
            //Response.Write("window.location.href='http://" + szLoginOutUrl + "?op=logout");
            // Response.Redirect(szLoginOutUrl + "");
          //  Response.Write("<script type=\"text/javascript\">window.location.href='http://" + szLoginOutUrl + "?op=logout'</script>");
            //Response.Write("szLoginOutUrl=" + szLoginOutUrl);
        //    Response.End();
        }
       // return;
    

        ///浙江建设学院，方正单点登录
        string szverify = Request["verify"];
        string szuserName = Request["userName"];
        string szstrSysDatetime = Request["strSysDatetime"];
        string szjsName = Request["jsName"];
        string szZFkey="zfsoft_xxx";
        if (!string.IsNullOrEmpty(szverify) && !string.IsNullOrEmpty(szuserName) && !string.IsNullOrEmpty(szstrSysDatetime) && !string.IsNullOrEmpty(szjsName))
        {
            Logger.trace("进入浙江建设学院，方正单点登录");
            string url = HttpContext.Current.Request.Url.Query;
            Logger.trace("url=" + url);
            string ma5=szuserName+szZFkey+szstrSysDatetime+szjsName;
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
            if (szverify == signature)
            {
                Session["uid"] = szuserName;
                LoginUseInfo loginUserInfo = new LoginUseInfo();
                loginUserInfo.szLogoName = szuserName;
                loginUserInfo.szPassword = "uniFound808";
                Session["LoginUseInfo"] = loginUserInfo;
                string redirectUrl = "clientweb/default.aspx";
                Response.Redirect(redirectUrl);
            }
            else {
                Logger.trace("传入szverify=" + szverify);
                Logger.trace("计算signature=" + signature);
            }
        }

        if (szsc == "center"||szsc=="admin"||string.IsNullOrEmpty(szsc))
        {
            if (string.IsNullOrEmpty(szsc))
            {
                szsc = "center";
            }
            Logger.trace("urlLoginsc");
            //CAS Server的登陆URL
            string loginServer = System.Configuration.ConfigurationManager.AppSettings["LoginAllSerURL"].ToString();
            //CAS Server的验证URL
            string validateServer = System.Configuration.ConfigurationManager.AppSettings["LoginAllValURL"].ToString();

            //当前集成系统所在的服务器和端口号，服务器可以是机器名、域名或ip，建议使用域名。端口不指定的话默认是80
            //以及应用名称和新增加的集成登录入口
          
           ;
            //登录成功重定向url参数
            string redirectUrl = Request.QueryString["clienturl"];
            if (redirectUrl == null || redirectUrl == "")
            {
                redirectUrl = Rurl;
            }
            //已经登录直接跳回
            string szIndex = Request["page"];
            if (!string.IsNullOrEmpty(szIndex))
            {
                if (szIndex == "admin")//
                {
                    redirectUrl = "pages/default.aspx";
                }
                else
                {
                    redirectUrl = "clientweb/default.aspx?page=" + szIndex;
                }
            }
            else {
                szIndex = "";
            }
            string loginaspx = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl)) + "/loginall.aspx?page=" + szIndex;
            string ticket = Request.QueryString["ticket"];
            if (ticket == null || ticket.Length == 0)
            {
                Logger.trace("urlLogin2" + loginServer + "?service=" + loginaspx);
                Response.Redirect(loginServer + "?service=" + loginaspx);
                return;
            }

            else
            {
                string validateUrl = validateServer + "?ticket=" + ticket + "&service=" + loginaspx;

                System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

                string resp = "";

             //   string resp = GetWebRequest(validateUrl);
                try
                {
                  
                    Logger.trace("validateUrl="+ validateUrl);
                    if (Application["webclient"] == null)
                    {
                        WebClient wc = new WebClient();
                        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                        wc.Headers.Add("Upgrade-Insecure-Requests", "1");
                        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36");
                        wc.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                        wc.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                        //  return;
                        StreamReader Reader = new StreamReader(wc.OpenRead(validateUrl));
                        Application["webclient"] = wc;
                        
                        resp = Reader.ReadToEnd();
                    }
                    else {
                        lock (obj)
                        {
                            WebClient wc = (WebClient)Application["webclient"];
                            wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                            wc.Headers.Add("Upgrade-Insecure-Requests", "1");
                            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36");
                            wc.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                            wc.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                            //  return;
                            StreamReader Reader = new StreamReader(wc.OpenRead(validateUrl));

                            resp = Reader.ReadToEnd();
                        }
                    }
                
                    Logger.trace("resp=" + resp);
                }
                catch (Exception ex)
                {
                    Logger.trace(ex.ToString());
                }

                NameTable nt = new NameTable();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
                XmlTextReader reader = new XmlTextReader(resp, XmlNodeType.Element, context);
              //  Logger.trace("resp:"+resp);
                

                string uid = null;
                string userName = null;
                Boolean authSuccess = false;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        string tag = reader.LocalName;
                       // Logger.trace("tag="+tag+";value="+reader.ReadString());
                        if (bADDZeo)
                        {
                            if (tag.ToUpper() == "CARDNO")
                            {
                               
                                uid = reader.ReadString();
                                uid = uid.Trim();
                                int uidLen = uid.Length;
                                Logger.trace("uid1=" + uid);
                                if (uidLen < 10)
                                {
                                    for (int i = 0; i < (10- uidLen); i++)
                                    {
                                        uid = "0" + uid;
                                    }
                                }
                                Logger.trace("uid2=" + uid);
                            }
                        }
                        else {
                            if (tag == "user")
                            {
                                if (bADDZeo)
                                {
                                    uid = reader.ReadString();
                                   
                                }

                            }
                        }

                        if (tag == "authenticationSuccess")
                            authSuccess = true;
                       
                        if (tag == "cn")
                            userName = reader.ReadString();
                    }
                }
                reader.Close();
                if (uid != null && uid != "")
                {
                    Session["uid"] = uid;
                    LoginUseInfo loginUserInfo = new LoginUseInfo();
                    loginUserInfo.szLogoName = uid;
                    loginUserInfo.szPassword = "uniFound808";
                    Session["LoginUseInfo"] = loginUserInfo;

                }
                else
                {
                    Response.Write(resp);
                    Response.End();
                }
                //如果登录成功，执行下面代码，否则按集成系统业务逻辑处理
                if (Session["clientUrl"] != null && Session["clientUrl"] != "")
                {
                    Rurl = Session["clientUrl"].ToString();

                }
                else
                {

                }
                Logger.trace("urlLogin2" + redirectUrl);
                Response.Redirect(redirectUrl);
                //Response.Redirect(Rurl);场馆临时注释


            }
 
        }
      

        string szLoginAllType = System.Configuration.ConfigurationManager.AppSettings["loginAllType"].ToString();

        if (szLoginAllType.ToLower() == "url")
        {
            Logger.trace("urlLogin");
            //CAS Server的登陆URL
            string loginServer = System.Configuration.ConfigurationManager.AppSettings["LoginAllSerURL"].ToString();
            //CAS Server的验证URL
            string validateServer = System.Configuration.ConfigurationManager.AppSettings["LoginAllValURL"].ToString();

            //当前集成系统所在的服务器和端口号，服务器可以是机器名、域名或ip，建议使用域名。端口不指定的话默认是80
            //以及应用名称和新增加的集成登录入口
            string szClientUrl = Request["clienturl"];
            if (szClientUrl != null)
            {
                Session["clientUrl"] = szClientUrl;
            }
            string szTemp=Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl));
           // szTemp = "http://place.sufe.edu.cn";
            string loginaspx = szTemp + "/loginall.aspx";
            //登录成功重定向url参数
            string redirectUrl = Request.QueryString["clienturl"];
            if (redirectUrl == null || redirectUrl == "")
            {
                redirectUrl = Rurl;
            }
            //已经登录直接跳回
            string szIndex = Request["page"];
            if (!string.IsNullOrEmpty(szIndex))
            {
                if (szIndex != "admin")//
                {
                    redirectUrl = "pages/default.aspx";
                }
                else
                {
                    redirectUrl = "clientweb/default.aspx?page=" + szIndex;
                }
            }
            if (Session["uid"] != null && Session["LoginUseInfo"]!=null)
            {
                Logger.trace("url=" + ((LoginUseInfo)Session["LoginUseInfo"]).szLogoName.ToString());
                Logger.trace("url=" + ((LoginUseInfo)Session["LoginUseInfo"]).szPassword.ToString());
                Response.Redirect(redirectUrl);
                return;
            }


            string ticket = Request.QueryString["ticket"];
            if (ticket == null || ticket.Length == 0)
            {
                Logger.trace("url=" + loginServer + "?service=" + loginaspx);
                Response.Redirect(loginServer + "?service=" + loginaspx);
               // Response.Write(System.Web.HttpUtility.UrlDecode(loginServer + "?service=" + "http://place.sufe.edu.cn/loginall.aspx"));
               // Response.End();
               // Response.Redirect(System.Web.HttpUtility.UrlDecode(loginServer + "?service=" + "http://place.sufe.edu.cn/loginall.aspx"));
                return;
            }

            else
            {
                string validateUrl = validateServer + "?ticket=" + ticket + "&service=" + loginaspx;

                System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

                 
                string resp = "";
               
                if (Application["webclient"] == null)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        StreamReader Reader = new StreamReader(client.OpenRead(validateUrl));
                        resp = Reader.ReadToEnd();
                        Application["webclient"] = client;
                        Logger.trace("resp=" + resp);
                    }
                    catch (Exception ex)
                    {
                        Logger.trace(ex.ToString());
                    }
                }
                else {
                    lock (obj)
                    {
                        WebClient client = (WebClient)Application["webclient"];
                        try
                        {
                            StreamReader Reader = new StreamReader(client.OpenRead(validateUrl));
                            resp = Reader.ReadToEnd();
                            Application["webclient"] = client;
                            Logger.trace("resp=" + resp);
                        }
                        catch (Exception ex)
                        {
                            Logger.trace(ex.ToString());
                        }
                    }
                }

              
                //string resp = GetWebRequest(validateUrl);
                NameTable nt = new NameTable();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
                XmlTextReader reader = new XmlTextReader(resp, XmlNodeType.Element, context);

                string uid = null;
                string userName = null;
                Boolean authSuccess = false;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        string tag = reader.LocalName;
                        if (tag == "authenticationSuccess")
                            authSuccess = true;
                        if (tag == "user")
                            uid = reader.ReadString();
                        if (tag == "cn")
                            userName = reader.ReadString();
                    }
                }
                reader.Close();
                if (uid != null && uid != "")
                {
                    Session["uid"] = uid;
                    LoginUseInfo loginUserInfo = new LoginUseInfo();
                    loginUserInfo.szLogoName = uid;
                    loginUserInfo.szPassword = "uniFound808";
                    Session["LoginUseInfo"] = loginUserInfo;

                }
                else
                {
                    Response.Write(resp);
                    Response.End();
                }
                //如果登录成功，执行下面代码，否则按集成系统业务逻辑处理
                if (Session["clientUrl"] != null && Session["clientUrl"] != "")
                {
                    Rurl = Session["clientUrl"].ToString();

                }
                else
                {

                }
                //Response.Write(Session["clientUrl"].ToString());
                // Response.End();
                Logger.trace("url=" + redirectUrl);
                Response.Redirect(redirectUrl);
                //Response.Redirect(Rurl);场馆临时注释
                

            }

        }
        else if (szLoginAllType.ToLower() == "com")
        {
            // object obj = Server.CreateObject("Idstar.IdentityManager");
            Logger.trace("认证方式用com组件");
            Idstar.IIdentityManager idstar = (Idstar.IIdentityManager)Server.CreateObject("Idstar.IdentityManager");
            // object obj=  Server.CreateObject("Idstar.IdentityManager");

            string login = idstar.GetLoginURL();
            Logger.trace("login=" + login);

            string logout = idstar.GetLogoutURL();
            Logger.trace("logout=" + login);
            string serverUrl = "http://sina.com.cn";
            string gotoUrl = serverUrl + Request.ServerVariables["SCRIPT_NAME"];
            string loginUrl = login + "?goto=" + Server.UrlEncode(gotoUrl);
            Logger.trace("logouturlgoto=" + loginUrl);
            string logoutUrl = logout + "?goto=" + Server.UrlEncode(gotoUrl);
            Logger.trace("logoutUrlgoto=" + logoutUrl);
            //'''''''''获取cookie''''''''
            string CookieValue;
            CookieValue = "";


            if (Request.Cookies["iPlanetDirectoryPro"] != null)
            {
                CookieValue = Request.Cookies["iPlanetDirectoryPro"].Value.ToString();
                Logger.trace("CookieValue=" + CookieValue);
            }
            else
            {
                Logger.trace("CookieValue=null");
            }
            //'''''''''获取用户名''''''''
            string currentUser;
            currentUser = "";
            currentUser = idstar.GetCurrentUser(Server.UrlDecode(CookieValue));

            Logger.trace("currentUser=" + currentUser);
            if (currentUser != null && currentUser != "")
            {
                LoginUseInfo loginUserInfo = new LoginUseInfo();
                loginUserInfo.szLogoName = currentUser;
                loginUserInfo.szPassword = "uniFound808";
                Session["LoginUseInfo"] = loginUserInfo;
            }
            if (Session["clientUrl"] != null && Session["clientUrl"] != "")
            {
                Rurl = Session["clientUrl"].ToString();
            }
            else
            {

            }
            Response.Redirect(Rurl);
        }
    }
    public string GetWebRequest(string szUrl)
    {
        string res = "";

        WebClient wc = new WebClient();


        try
        {
            Stream st = wc.OpenRead(szUrl);
            StreamReader sr = new StreamReader(st);
            res = sr.ReadToEnd();
            sr.Close();
            st.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            Response.End();
        }


        return res;
    }
    public class MyPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(
              ServicePoint srvPoint
            , X509Certificate certificate
            , WebRequest request
            , int certificateProblem)
        {

            //Return True to force the certificate to be accepted.
            return true;

        } // end CheckValidationResult
    } // class MyPolicy
    protected  void Logout()
    {
        if (Session["SessionID"] == null) return;
        if (Session["LoginResult"] == null) return;

        ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
        vrParameter.dwAccNo = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
        vrParameter.szLogonName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szLogonName;

        ADMINLOGOUTRES  vrResult;
        REQUESTCODE ret1 = m_Request.Admin.Logout(vrParameter, out vrResult);
        if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
        {
        }
        Session["SessionID"] = null;
        Session["LoginResult"] = null;
        Session["URLHistoryStack"] = null;
        m_Request.m_UniDCom.Close();
    }
}