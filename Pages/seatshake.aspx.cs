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
using System.IO;
using System.Net;
using UniWebLib;
using System.Text;
public partial class _Default : System.Web.UI.Page
{
    protected string szICURL = "http://pay.unifound.net";
    protected string szAppID = "wx399a60eb3f4f1842";
    protected string szAPPSercert = "548f12a8f356d28baaa5419f15b21198";
    protected string szWxUrl = "https://api.weixin.qq.com/shakearound/user/getshakeinfo";
    protected string szGetTockentUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
    public class AppAccessToken
    {
        public string access_token;
        public int expires_in;
        public string refresh_token;
        public string openid;
        public string scope;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string urlInfo= HttpContext.Current.Request.Url.Query;
        string szopenid = Request["openid"];
        if (string.IsNullOrEmpty(GetCode())&&string.IsNullOrEmpty(szopenid))
        {
            string szParm = Request["ticket"];
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + szAppID + "&redirect_uri=" + Server.UrlEncode(szICURL + "/pages/") + "shareredirt.aspx&response_type=code&scope=snsapi_base&state=" + szParm;
            Response.Redirect(url);
            Response.End();
        }
        else
        {
            
            if (!string.IsNullOrEmpty(szopenid))
            {
             //   openid.Value = szopenid;
            }
            else
            {
                AppAccessToken accTocken = GetAppToken();
                string szTicket = Request["state"];//乱转获取ticket
                string szWxUserID = GetInfoFromUrl(szWxUrl, "access_token=" + accTocken.access_token + "&ticket=" + szTicket);
                Response.Write(urlInfo);
                Response.Write("<br />");
                Response.Write(accTocken.ToString());
                Response.Write("<br />");
                Response.Write("tickt=" + szTicket + "<br />");
                Response.Write("szWxUserIDtext=" + szWxUserID);
                Response.End();
            }
        }
        
    }

  
    public AppAccessToken GetAppToken()
    {
        AppAccessToken accToken = null;
        if (
        (Session["WAccessToken"] == null) || Session["WAccessToken"].GetType() != typeof(AppAccessToken) ||
        (string.IsNullOrEmpty(((AppAccessToken)Session["WAccessToken"]).openid)) ||
        (Session["WAccessToken_Time"] == null) ||
        ((DateTime.Now - (DateTime)Session["WAccessToken_Time"]).TotalSeconds >= ((AppAccessToken)Session["WAccessToken"]).expires_in))
        {
            String tok = Request["tok"];
            if (!String.IsNullOrEmpty(tok))
            {
                string szAccToken2 = GetWebRequest("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" +szAppID + "&grant_type=refresh_token&refresh_token=" + tok);
                accToken = JSON.parse<AppAccessToken>(szAccToken2);
                if (accToken != null && accToken.access_token != null)
                {
                    Session["WAccessToken"] = accToken;
                    Session["WAccessToken_Time"] = DateTime.Now;
                    //Logger.trace("oauth2 get refresh_token:" + accToken.openid);
                    return accToken;
                }
                else
                {
                    Logger.Trace(szAccToken2);
                }
            }

            string szCode = GetCode();
            string szAccToken = GetWebRequest("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + szAppID + "&secret=" + szAPPSercert + "&code=" + szCode + "&grant_type=authorization_code");
            
            accToken = JSON.parse<AppAccessToken>(szAccToken);
            if (accToken != null && accToken.access_token != null)
            {
                Session["WAccessToken"] = accToken;
                Session["WAccessToken_Time"] = DateTime.Now;
                //Session.Timeout = 10;
                Logger.trace("oauth2 get access_token:" + accToken.openid);
            }
            else
            {
                Logger.Trace(szAccToken);
            }
        }
        else
        {
            accToken = (AppAccessToken)Session["WAccessToken"];
        }
        return accToken;
    }
    public string GetCode()
    {
        string szCode = Request["code"];
        return szCode;
    }
    public string GetWebRequest(string szUrl)
    {
        WebClient wc = new WebClient();
        Stream st = wc.OpenRead(szUrl);
        StreamReader sr = new StreamReader(st);
        string res = sr.ReadToEnd();
        sr.Close();
        st.Close();
        return res;
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
}
