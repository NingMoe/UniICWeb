using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Collections;
using UniWebLib;
using System.Security.Cryptography;
using System.Net.Security;

public partial class LoginAll : UniPage
{
    string szXuexiao = "";//wanxiao.beihang,huadong
    string szSingKeyDate = "1";//1表示要时间搓，0不要时间搓

    string Rurl = "default.aspx";
    string corpid = "wxebee63fa4a23266d";
    string corpsecret = "dg4ef8ZP9wxNJXnH-5dMpQxmpkykX8uLZk9kIiqETM-HT8eQJrMpPk0A-GLPfZHo";
    
    protected string szPasswd = "uniFound808";

    string client_id = "0fdbf549d3294690872d02c51b3831a8";
    string client_secret = "B9AE49B3FE8C1A7CB0CB2361570B804F";
    string szauthUrl = "https://open.17wanxiao.com/api";
    string szoutUrl = "http://210.46.82.146/LoginMAll.aspx";
    protected string szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();

	
	//abcefgghkmop123
    //汇文对接参数配置--沈航，
    /*
    string weixinApiKey = "90GDLKG41677636828F9BK8CGD8F685CDJD1A3JE128H30C";
    string appid = "wxbe525cd6a6a0cab7";
    string fuwuAddr = "http://lib-mobile.sau.edu.cn/m";
    */
    //汇文对接参数配置--北京电子科技学院
    //  string weixinApiKey = "HKN8NM725FK3NNCCG10ALD4EL89CG6C2FG1HEFIIEEF5F";
    //  string appid = "wx1488201f71b01ba0";
    //  string fuwuAddr = "http://123.127.3.4/m/";

    //汇文对接参数配置--华南师范研修间服务器地址
    string weixinApiKey = "7AA1BF7G118AA874D2D52D912912IBF9E1H4B9";
   
    string fuwuAddr = "http://opac.lib.zjhu.edu.cn/m/";


    string CorpID = "wxebee63fa4a23266d";
    string Secrect = "dg4ef8ZP9wxNJXnH-5dMpQxmpkykX8uLZk9kIiqETM-HT8eQJrMpPk0A-GLPfZHo";
    string AgentID = "5";
    string redirect_uri = "http://www.room-reservation.lib.ecnu.edu.cn/loginmall.aspx";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.Url.Query;
        if (szXuexiao == "huadong")
        { 
            weixin();
        }

        if (szXuexiao == "wanxiao")
        {
            wanxiao();
        }


        if (szXuexiao == "beihang")
        {
            Logger.trace("beihanglogin");
            huiwen();
            return;
        }
          

        //北科大对接方式
        string szSN = Request["sn"];
        if (szSN != null)
        {
            Logger.trace("szsn=" + szSN);
            GetUserInfoFromUrl(szSN);
        }
        

        string szSignKey = Request["signkey"];
        string szuid = Request["uid"];
        if ((!string.IsNullOrEmpty(szSignKey)) && (!string.IsNullOrEmpty(szuid)))
        {
            Logger.trace("szSignKey=" + szSignKey + ";szuid=" + szuid);
            Logger.trace("szuid=" + szuid);
            GetUserInfoFromUrl(szuid, szSignKey);
        }
        string szOP = Request["op"];
        if (System.Configuration.ConfigurationManager.AppSettings["loginAllType"].ToString().ToLower() == "url")
        {
            szOP = Request["op"];
            if (szOP != null && szOP != "" && szOP.ToLower() == "logout")
            {
                string loginServerURL = System.Configuration.ConfigurationManager.AppSettings["LoginAllOutURL"].ToString();
                Response.Redirect(loginServerURL);
                return;
            }
            casLogin();
        }


        Logger.trace("加载页面url传入参数" + url);

        string logouturl = Request["logouturl"];
        string szIDINFO = "";
        if (Application["tocketid"] == null || Application["tocketid"].ToString() == "")
        {
            Logger.trace("需要获取新的ticket");
            bool szid = GetTocekID(out szIDINFO);
        }
        else
        {
            Logger.trace("无需获取新的ticket");
            int nRes = GetUserInfo();
            if (nRes == 4)//重连
            {
                GetTocekID(out szIDINFO);
                GetUserInfo();
            }
        }
    }
    public void weixin()
    {
        string code = Request.QueryString["code"];
        Logger.trace("weixincode="+code);
        if (String.IsNullOrEmpty(code))
        {
            Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + CorpID + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=SCOPE&agentid=" + AgentID + "&state=STATE#wechat_redirect");
            Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + CorpID + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=SCOPE&agentid=" + AgentID + "&state=STATE#wechat_redirect");
            return;
        }

        string netid = null;


        string ACCESS_TOKEN = GetAccessToken();

        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateCertificate);
        
        string validateurl = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + ACCESS_TOKEN + "&code=" + code;
        
        StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateurl));
        string resp = Reader.ReadToEnd();
        Logger.trace(resp);
        Hashtable token = JSON.parse<Hashtable>(resp);
        if (token != null)
        {
            netid = (string)token["UserId"];
            LoginUseInfo loginUserInfo = new LoginUseInfo();
            loginUserInfo.szLogoName = netid;
            loginUserInfo.szPassword = "uniFound808";
            Session["LoginUseInfo"] = loginUserInfo;
            Response.Redirect("clientweb/m/ic2/default.aspx?version=" + szVersion);
        }

        if (netid == null)
        {
            Logger.Trace("身份验证失败");
        }
        else
        {

        }

    }
    static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }

    string GetAccessToken()
    {
        string access_token = "";// (string)Application["access_token"];

        if (!string.IsNullOrEmpty(access_token))
        {
            DateTime t = (DateTime)Application["access_token_time"];
            int expires = (int)(int?)Application["access_token_expires"];
            if (expires < 0)
            {
                expires = 10;
            }
            if ((DateTime.Now - t).Seconds < expires)
            {
                return access_token;
            }
        }
        string resp = "";
        string acurl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + CorpID + "&corpsecret=" + Secrect;
        try
        {
            StreamReader Reader = new StreamReader(new WebClient().OpenRead(acurl));

            resp = Reader.ReadToEnd();
        }
        catch (Exception e)
        {
            Response.Write("acurl");
            Response.Write("<br />"+e.ToString());
        }
        Logger.trace(resp);
        Hashtable token = JSON.parse<Hashtable>(resp);
        if (token != null)
        {
            access_token = (string)token["access_token"];
        }

        if (!string.IsNullOrEmpty(access_token))
        {
            Application["access_token"] = access_token;
            Application["access_token_time"] = DateTime.Now;
            Application["access_token_expires"] = token["expires_in"];
        }
        return access_token;
    }
    public string RequestWebAPI(string url, string sendData)
    {
        string backMsg = "";
        try
        {
            System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
            httpRquest.Method = "GET";
            //这行代码很关键，不设置ContentType将导致后台参数获取不到值  
            httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            byte[] dataArray = System.Text.Encoding.UTF8.GetBytes(sendData);
            //httpRquest.ContentLength = dataArray.Length;  
            System.IO.Stream requestStream = null;
            if (string.IsNullOrEmpty(sendData) == false)
            {
                requestStream = httpRquest.GetRequestStream();
                requestStream.Write(dataArray, 0, dataArray.Length);
                requestStream.Close();
            }
            System.Net.WebResponse response = httpRquest.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
            backMsg = reader.ReadToEnd();
            

            reader.Close();
            reader.Dispose();
            
            requestStream.Dispose();
            responseStream.Close();
            responseStream.Dispose();
            Response.Write("backMsg");
            
        }
        catch (Exception)
        {
            throw;
        }
        return backMsg;
    }

    public void huiwen()
    {
        DateTime DateStart= new DateTime(1970,1,1,8,0,0);     
        string code=Request["code"];
        Logger.trace("code="+code);
        
        if (code == null || code == "")
        {
            Response.Write("请先绑定账户");
            Response.Redirect(fuwuAddr+"/weixin/weixin_reg.php");
            Response.End();
        }
        string timeStamp =  Convert.ToInt32((DateTime.Now - DateStart).TotalSeconds).ToString();
		string random = (Convert.ToInt32((DateTime.Now - DateStart).TotalSeconds)+11).ToString();//生成随机数
	

		string ma5 =code+random+timeStamp+weixinApiKey;
        string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5").ToLower();
        Logger.trace("ma5=" + ma5 + "&&&MD5=" + signature);

		string requestUrl = fuwuAddr + "/weixin/weixin_get_info.action";
        string fromdata="code="+code+"&timeStamp="+timeStamp+"&random="+random+"&signature="+signature ;

       string szRes= GetInfoFromUrl(requestUrl,fromdata);
        Logger.trace("req="+requestUrl+fromdata+"##andres="+szRes);
      

        string szSuccess= GetStrInfoJsonIndex(szRes,"success",false);
        string szState= GetStrInfoJsonIndex(szRes,"state",true);
        Logger.trace("szSuccess="+ szSuccess+",szState="+ szState);
		if(szRes.IndexOf("用户未注册")>-1)
		{
            Logger.trace("success=" + szSuccess + "&state=" + szSuccess);
            string apiBackUrl = fuwuAddr + "weixin/weixin_reg.php";
            Logger.trace(apiBackUrl);
            Response.Redirect(apiBackUrl);
            return;
            /*返回数据库结构
			{"success":false,
			"msg":"读者未注册",
			"state":"1"}
			*/
            /*
			String apiBackUrl = "http://www.example.com:81/user_auth/login" ;//当前页面的对外地址，用来回调，根据实际情况填写


        
			signature = DigestUtils.md5Hex(apiBackUrl+random+timeStamp+weixinApiKey) ;
			String loginUrl = null;
			try {
				loginUrl = Common.getAuthUrl(fuwuAddr+"/weixin/api_reg.action?apiBackUrl="
						+URLEncoder.encode(apiBackUrl,"utf-8")+"&random="+URLEncoder.encode(random,"utf-8")
						+"&timeStamp="+timeStamp+"&signature="+signature,appid);
			} catch (UnsupportedEncodingException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			return "redirect:"+ loginUrl ;
             * */
        }
        else if (szSuccess == "false" && szState == "0")
		{
            Logger.trace("success=" + szSuccess + "&state=" + szSuccess);
			/*返回数据库结构
			{"success":false,
			"msg":"参数错误",
			"state":"0"}
			*/
			//TODO
			//调用出错
			//return "error.jsp" ;
		}
		else
		{
			//进行登录操作
            Logger.trace("szRes=" +szRes);
            string szLogonName = GetStrInfoJsonIndex(szRes, "CERT_ID", true);
            if (szLogonName != "")
            {
                LoginUseInfo loginUserInfo = new LoginUseInfo();
                loginUserInfo.szLogoName = szLogonName;
                loginUserInfo.szPassword = "uniFound808";
                Session["LoginUseInfo"] = loginUserInfo;
                Response.Redirect("clientweb/m/ic2/default.aspx?version=" + szVersion);
            }
		}
    }
    public string GetStrInfoJsonIndex(string szContanct,string szColum,bool valueyinhao)
    {
        string szRes = "";
        
        int nLogonNameS = szContanct.IndexOf(szColum);
        if (nLogonNameS < 0)
        {
            return szRes;
        }
        int nLogonNameE = szContanct.IndexOf(",",nLogonNameS);
        if (nLogonNameE < 0)
        {
            nLogonNameE = szContanct.IndexOf("}", nLogonNameS);
        }
        try
        {
            if(valueyinhao==true)
            {
                szRes = szContanct.Substring(nLogonNameS + szColum.Length +3, nLogonNameE - nLogonNameS - szColum.Length - 4);
            }
            else{
                szRes = szContanct.Substring(nLogonNameS + szColum.Length + 2, nLogonNameE - nLogonNameS - szColum.Length - 2);
            }
        }
        catch{
        }
        return szRes;
 
    }
    public void wanxiao()
    {
        try
        {
            string szAppcode = Request["code"];
            string FormURL = szauthUrl;  //处理表单的绝对URL地址  
            string FormData = "code=" + szAppcode + "&client_id=" + client_id + "&client_secret=" + client_secret + "&redirect_uri=" + (szoutUrl) + "&grant_type=authorization_code";

            string postString = FormData;// "arg1=a&arg2=b";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = FormURL + "/accessToken";//地址  
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string WebContent = Encoding.UTF8.GetString(responseData);//解码  


            //第二次请求
            string szTemp = "access_token";
            int iPoststart = WebContent.IndexOf(szTemp) + szTemp.Length + 3;
            int iPostend = WebContent.LastIndexOf("\"");
            string szTockent = WebContent.Substring(iPoststart, iPostend - iPoststart);

            WebClient webClient2 = new WebClient();
            string url2 = szauthUrl + "/1/user/base_senior?access_token=" + szTockent;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Byte[] pageData = webClient2.DownloadData(url2);
            string WebContent2 = enc.GetString(pageData);

            if (!string.IsNullOrEmpty(WebContent2))
            {
                
                string szLogonName = GetStrInfoJsonIndex(WebContent2, "outid", true);
            
                if(szLogonName != "")
                {
                    LoginUseInfo loginUserInfo = new LoginUseInfo();
                    loginUserInfo.szLogoName = szLogonName;
                    loginUserInfo.szPassword = "uniFound808";
                    Session["LoginUseInfo"] = loginUserInfo;
                    Response.Redirect("clientweb/m/ic2/default.aspx?version=" + szVersion);
                }
            }
        }
        catch (Exception e)
        {
             Response.Write(e.ToString());
        }
      
    }
    private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
    {

        //为了通过证书验证，总是返回true

        return true;

    }
    public string GetInfoFromUrl(string szauthUrl, string FormData)
    {

        CookieContainer cc = new CookieContainer();
        string FormURL = szauthUrl;  //处理表单的绝对URL地址  

        
        //表单需要提交的参数，注意改为你已注册的信息。  
        byte[] data = Encoding.UTF8.GetBytes(FormData);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FormURL);
        request.Method = "POST";//数据提交方式  
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";//模拟一个UserAgent  

        Stream newStream = request.GetRequestStream();
        newStream.Write(data, 0, data.Length);

        newStream.Close();

        request.CookieContainer = cc;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        cc.Add(response.Cookies);
        Stream stream = response.GetResponseStream();
        string WebContent = new StreamReader(stream, System.Text.Encoding.UTF8).ReadToEnd();//反馈得到的\\\\

        return WebContent;
    }
    private void casLogin()
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
        string szTemp = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl));
        // szTemp = "http://place.sufe.edu.cn";
        string loginaspx = szTemp + "/loginmall.aspx";
        //登录成功重定向url参数
        string redirectUrl = Request.QueryString["clienturl"];
        if (redirectUrl == null || redirectUrl == "")
        {
            redirectUrl = Rurl;
        }
        //已经登录直接跳回

        if (Session["uid"] != null && Session["LoginUseInfo"] != null)
        {
            Response.Redirect(redirectUrl);
            return;
        }


        string ticket = Request.QueryString["ticket"];
        if (ticket == null || ticket.Length == 0)
        {

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

            StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateUrl));
            string resp = Reader.ReadToEnd();
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
                Logger.trace("cas登录：uid+" + loginUserInfo.szLogoName + ",passwd=" + loginUserInfo.szPassword);
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

            Response.Redirect("clientweb/default.aspx?version=" + szVersion);

        }
    }
    private readonly string strDesKey = "EW2sdfkj";
    private void GetUserInfoFromUrl(string szSN)//超星
    {
        string szInfo = Decrypt_DES(szSN, strDesKey);

        string szLogonNameS = "<uid>";
        string szLogonNameE = "</uid>";
        string szPasswdS = "<pwd>";
        string szPasswdE = "</pwd>";
        int nLogonNameS = szInfo.IndexOf(szLogonNameS);
        int nLogonNameE = szInfo.IndexOf(szLogonNameE);
        string szLogonName = "";
        try
        {
            szLogonName = szInfo.Substring(nLogonNameS + szLogonNameS.Length, nLogonNameE - nLogonNameS - szLogonNameS.Length);
        }
        catch
        {

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
        if (szLogonName != null && szLogonName != "")
        {
            LoginUseInfo info = new LoginUseInfo();
            info.szPassword = szPasswd;
            info.szLogoName = szLogonName;
            Session["LoginUseInfo"] = info;
          //  Response.Write(info.szLogoName + ":psd=:" + info.szPassword);
            Logger.trace("szLogonName=" + szLogonName + ";szPasswd=" + szPasswd);
            // Response.End();

            Response.Redirect("clientweb/default.aspx?version=" + szVersion);

        }
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
    private bool GetUserInfoFromUrl(string uid, string szSignKey)
    {
        //string szKey = "G(Z@L*!IA";

        //华东科大的Key
        string szKey = "X(J@L*!IA";

        string szDate = DateTime.Now.ToString("yyyyMMdd");

        string ma5 = uid + szKey;
        if (szSingKeyDate == "1")
        {
            ma5 = ma5 + szDate;
        }
        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
        if (szSignKey.ToLower() == EnPswdStr.ToLower())
        {
            LoginUseInfo info = new LoginUseInfo();
            info.szPassword = szPasswd;
            info.szLogoName = uid;
            Session["LoginUseInfo"] = info;

            Logger.trace(uid + "微信跳转登录成功");
            Logger.trace("登录账户：" + uid);
           // Response.Write(uid+"__"+szPasswd);
           // return true;
            Response.Redirect("clientweb/m/ic2/default.aspx?version=" + szVersion);

            return true;
        }
        else
        {
            Logger.trace(uid + "微信跳转登录失败；本地加密:" + EnPswdStr + ";传入加密值:" + szSignKey);
            return false;
        }

    }
    public int GetUserInfo()
    {
        string url = HttpContext.Current.Request.Url.Query;
        Logger.trace("url传入参数" + url);
        string szUID = Request["code"];
        string FormURL = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo";
        Logger.trace("提交code=" + szUID);
        string szTocketid = "";
        if (Application["tocketid"] != null && Application["tocketid"].ToString() != "")
        {
            szTocketid = Application["tocketid"].ToString();
            Logger.trace("无需新的tickedt 旧的=" + szTocketid);
        }
        if (Session["LoginUseInfo"] != null && ((LoginUseInfo)Session["LoginUseInfo"]).szLogoName != null)
        {
            Logger.trace("session中有值直接跳转" + ((LoginUseInfo)Session["LoginUseInfo"]).szLogoName.ToString());
            Response.Redirect("clientweb/m/ic2/default.aspx");
        }

        FormURL = FormURL + "?access_token=" + szTocketid + "&code=" + szUID;
        Logger.trace("获取个人信息url=FormURL" + FormURL);
        //Logger.trace("获取个人信息url=FormURL" + FormURL + "formdata=" + FormData);
        string responseContent = GetWebRequest(FormURL);
       

        Logger.trace("个人信息responseContent=" + responseContent);

        if (responseContent.ToLower().IndexOf("userid") > -1)
        {
            Logger.trace("用户信息=" + responseContent);

            string acctockect = "access_token";
            int uStart = acctockect.Length + 3;
            int len = responseContent.IndexOf("DeviceId", 11) - 14;
            string useid = responseContent.Substring(11, len);
            Logger.trace("useid=" + useid);
            LoginUseInfo loginUserInfo = new LoginUseInfo();
            loginUserInfo.szLogoName = useid;
            loginUserInfo.szPassword = "uniFound808";
            Session["LoginUseInfo"] = loginUserInfo;
            Logger.trace("登录账户：" + loginUserInfo.szLogoName);
            Response.Redirect("clientweb/m/ic2/default.aspx");
            //Response.Write("useid=" + useid);
            //Response.End();

            return 1;//成功
        }
        else if (responseContent.ToLower().IndexOf("openid") > -1)
        {

        }
        else if (responseContent.ToLower().IndexOf("errcode") > -1)
        {
            //if (responseContent.IndexOf("access_token expired") > -1)
            {
                return 4;
            }
        }
        else
        {
            return 4;//重连
        }
        return 0;
    }
    public bool GetTocekID(out string szRes)
    {
        szRes = "";

        string FormURL = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret;  //处理表单的绝对URL地址  
        Logger.trace("FormURL=" + FormURL);
        //把下面的xxxxxx,yyyyyy替换为你的账号，密码，这个地方临时代替的，你懂的：）  
        string FormData = "corpid=" + corpid + "&corpsecret=" + corpsecret;

        /*
        //表单需要提交的参数，注意改为你已注册的信息。  
        byte[] data = Encoding.UTF8.GetBytes(FormData);
        CookieContainer cc = new CookieContainer();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FormURL);
        
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
        request.ContentLength = data.Length;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";//模拟一个UserAgent  
        Logger.trace("开始获取");
        Stream newStream = request.GetRequestStream();
        newStream.Write(data, 0, data.Length);

        newStream.Close();

        request.CookieContainer = cc;
        request.Method = "POST";//数据提交方式  

        HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
        Logger.trace("开始完成");
        StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
        string responseContent = streamReader.ReadToEnd();
        
        httpWebResponse.Close();
        streamReader.Close();
        Logger.trace("responseContent=" + responseContent);
         * */

        string responseContent = GetWebRequest(FormURL);
        Logger.trace("responseContent=" + responseContent);
        if (responseContent.ToLower().IndexOf("access_token") > -1)
        {
            string acctockect = "access_token";
            int uStart = acctockect.Length + 3;
            int len = responseContent.IndexOf("expires_in", 17) - 20;
            Logger.trace("uStart=" + uStart + ";len=" + len);
            string szTicket = responseContent.Substring(17, len);
            Logger.trace(szTicket);
            Application["tocketid"] = szTicket;
            Logger.trace("获取行的tickect=" + szTicket);
            return true;
        }
        return false;
    }
    public string PostWebRequest(string szUrl, string szPost)
    {
        try
        {
            WebClient wc = new WebClient();
            wc.Encoding = UTF8Encoding.UTF8;
            return wc.UploadString(szUrl, szPost);
        }
        catch (Exception e)
        {
            Logger.Trace(e.ToString());
        }
        return "";
    }
    public string GetWebRequest(string szUrl)
    {
        string res = "";
        try
        {
            WebClient wc = new WebClient();
            Stream st = wc.OpenRead(szUrl);
            StreamReader sr = new StreamReader(st);
            res = sr.ReadToEnd();
            sr.Close();
            st.Close();
        }
        catch (Exception e)
        {
            Logger.Trace(e.ToString());
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
    protected void Logout()
    {
        if (Session["SessionID"] == null) return;
        if (Session["LoginResult"] == null) return;

        ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
        vrParameter.dwAccNo = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
        vrParameter.szLogonName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szLogonName; 

        ADMINLOGOUTRES vrResult;
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