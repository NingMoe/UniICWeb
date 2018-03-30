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


public partial class LoginAll : UniPage
{
    string szXuexiao = "beihang";

    string Rurl = "default.aspx";
    string corpid = "wxb8d93ebc8e0844c5";
    string corpsecret = "A8bT1g1Tj1OwGEYk4PWLZR1dgBB0EZLQ0vYrC8Wk0gZwHKQnEmIdIbYoLSDM61xl";
    protected string szPasswd = "uniFound808";

    string client_id = "0fdbf549d3294690872d02c51b3831a8";
    string client_secret = "B9AE49B3FE8C1A7CB0CB2361570B804F";
    string szauthUrl = "https://open.17wanxiao.com/api";
    string szoutUrl = "http://115.236.69.117:1082/LoginMAll.aspx";
    protected string szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
    
    //汇文对接参数配置--沈航，警察学院
    string weixinApiKey = "90GDLKG41677636828F9BK8CGD8F685CDJD1A3JE128H30C";
    string appid = "wxbe525cd6a6a0cab7";
    string fuwuAddr = "http://lib-mobile.sau.edu.cn/m";



    protected void Page_Load(object sender, EventArgs e)
    {

        if (szXuexiao == "szXuexiao")
        {
            huiwen();
        }
        if (szXuexiao == "beihang")
        {
            Logger.trace("beihanglogin");
            huiwen();

        }

        //北科大对接方式
        string szSN = Request["sn"];
        if (szSN != null)
        {
            Logger.trace("szsn=" + szSN);
            GetUserInfoFromUrl(szSN);
        }



        string url = HttpContext.Current.Request.Url.Query;

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
    public void huiwen()
    {
        DateTime DateStart= new DateTime(1970,1,1,8,0,0);     
        string code=Request["code"];
        Logger.trace("code="+code);

        string timeStamp =  Convert.ToInt32((DateTime.Now - DateStart).TotalSeconds).ToString();
		string random = (Convert.ToInt32((DateTime.Now - DateStart).TotalSeconds)+11).ToString();//生成随机数
	

		string ma5 =code+random+timeStamp+weixinApiKey;
        string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
       

		string requestUrl = fuwuAddr + "/weixin/weixin_get_info.action";
        string fromdata="code="+code+"&timeStamp="+timeStamp+"&random="+random+"&signature="+signature ;

       string szRes= GetInfoFromUrl(requestUrl,fromdata);
        Logger.trace("req="+requestUrl+fromdata+"##andres="+szRes);

        string szSuccess= GetStrInfoJsonIndex(szRes,"success",false);
        string szState= GetStrInfoJsonIndex(szRes,"state",true);
        
		if(szSuccess=="false" && szState=="1")
		{
            Logger.trace("success=" + szSuccess + "&state=" + szSuccess);
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
			/*返回数据库结构
			{"CERT_ID":"05200525",//证件号
			"NAME":"黄05200525",//姓名
			"DEPT":"化生学院",
			"EMAIL":"sina@sina.com",
			"TOTAL_LEND_QTY":795,
			"VOLT_FLAG":0,
			"R_DEP_ID":null,
			"DEBT_FLAG":0,
			"DEPOSIT":0,
			"REDR_CERT_ID":"05200525",//条码号
			"OPER_DATE":"2009-11-16",
			"BGN_DATE":"2009-11-16",
			"CERT_FLAG":"1",
			"REDR_FLAG":"1",
			"END_DATE":"2018-06-13",
			"CHARGE":0,
			"REDR_TYPE_NAME":"学生",
			"REDR_TYPE_CODE":"23",
			"LEND_GRD":"01",
			"CHK_VALIDITY_PERIOD":"1",
			"MAX_LEND_QTY":10,
			"MAX_DEBT":2,
			"SYS_DATE":"2016-07-04",
			"MAX_RELE_QTY":5,
			"MAX_RELE_DAYS":10,
			"TELE":"555-555-5555",
			"ADDRESS":"753 Main Street",
			"POSTCODE":"33",
			"PWD_CHECK_FLAG":"1",
			"DEFAULT_TAKE_LOCA":"00023",
			"RELE_SEND_ADDR":"南京大学图书馆513室",
			"RELE_SEND_FLAG":"1",
			"MAX_PREG_DAYS":7,
			"REDR_ATTR_1":"1",
			"MAX_PREG_QTY":5,
			"CREDITTOTALNUM":500,
			"CREDITNUM":495,
			"success":true,
			"msg":"获取数据成功",
			"state":"2"}
			*/
			//TODO
			//进行登录操作
            Logger.trace("szRes=" +szRes);
            string szLogonName = GetStrInfoJsonIndex(szRes, "CERT_ID", true);
            if (szLogonName != "")
            {
                LoginUseInfo loginUserInfo = new LoginUseInfo();
                loginUserInfo.szLogoName = szLogonName;
                loginUserInfo.szPassword = "uniFound808";
                Session["LoginUseInfo"] = loginUserInfo;
            }
		}
    }
    public string GetStrInfoJsonIndex(string szContanct,string szColum,bool valueyinhao)
    {
        string szRes = "";
        
        int nLogonNameS = szContanct.IndexOf(szColum);
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

            string FormData = "code=" + szAppcode + "&client_id=" + client_id + "&client_secret=" + client_secret + "&redirect_uri=" + (szoutUrl) + "&grant_type=authorization_code";

            CookieContainer cc = new CookieContainer();
            string FormURL = szauthUrl;  //处理表单的绝对URL地址  

            //把下面的xxxxxx,yyyyyy替换为你的账号，密码，这个地方临时代替的，你懂的：）  


            //表单需要提交的参数，注意改为你已注册的信息。  
            byte[] data = Encoding.UTF8.GetBytes(FormData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FormURL+"/accessToken");
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
            string WebContent = new StreamReader(stream, System.Text.Encoding.Default).ReadToEnd();//反馈得到的\\\\

            string szTemp = "access_token";
            int iPoststart = WebContent.IndexOf(szTemp) + szTemp.Length + 3;
            int iPostend = WebContent.LastIndexOf("\"");
            string szTockent = WebContent.Substring(iPoststart, iPostend - iPoststart);

            string szCodeINFO = FormURL + "/1/user/base?access_token=" + szTockent;

            /*
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(szCodeINFO);
            request2.Method = "GET";//数据提交方式  
            request2.ContentType = "application/x-www-form-urlencoded";
            request2.ContentLength = data.Length;
            request2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";//模拟一个UserAgent  

            Stream newStream2 = request2.GetRequestStream();
            ;

            newStream.Close();

            request2.CookieContainer = cc;

            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            cc.Add(response.Cookies);
            Stream stream2 = response2.GetResponseStream();
            string WebContent2 = new StreamReader(newStream2, System.Text.Encoding.Default).ReadToEnd();//反馈得到的\\\\

            */
            Logger.trace(szCodeINFO);
            StreamReader Reader = new StreamReader(new WebClient().OpenRead(szCodeINFO));
           string resp = Reader.ReadToEnd();

           Response.Write("webcont=" + WebContent + ";;<BR />info=" + szTockent + "<br />resp=" + resp);
            
        }
        catch (Exception e)
        {
            Response.Write(e.ToString());
        }
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
        string WebContent = new StreamReader(stream, System.Text.Encoding.Default).ReadToEnd();//反馈得到的\\\\

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
    private void GetUserInfoFromUrl(string szSN)
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
            Response.Write(info.szLogoName + ":psd=:" + info.szPassword);
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

        string ma5 = uid + szKey + szDate;
        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
        if (szSignKey.ToLower() == EnPswdStr.ToLower())
        {
            LoginUseInfo info = new LoginUseInfo();
            info.szPassword = szPasswd;
            info.szLogoName = uid;
            Session["LoginUseInfo"] = info;

            Logger.trace(uid + "微信跳转登录成功");
            Logger.trace("登录账户：" + uid);
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