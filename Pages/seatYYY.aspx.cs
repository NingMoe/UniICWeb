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
    }
    public class AccessToken
    {
        public string access_token;
        public int expires_in;
        public string refresh_token;
        public string openid;
        public string scope;
    }
    public class beacon_info
    {
        public string distance;
        public string major;
        public string minor;
        public string uuid;
    }
    public class SharkInfo
    {
        public string page_id;
        public beacon_info beacon_info;
        public string openid;
        public string poi_id;
    }
    public class ShakeData {
        public SharkInfo data;
        public string errcode;
        public string errmsg;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string szTicket = Request["ticket"];
        string szaccTockt = GetToken();
        string szRes = PostFunction("https://api.weixin.qq.com/shakearound/user/getshakeinfo?access_token=" + szaccTockt, szTicket);

        ShakeData sharked = new ShakeData();
        
        sharked= JSON.parse<ShakeData>(szRes);
        string szWriteRes = "";
        
        szWriteRes += "sharked.openid=" + sharked.data.openid + "<br />";
        szWriteRes = "sharked.beacon_info.distance=" + sharked.data.beacon_info.distance + "<br />";
        szWriteRes += "sharked.page_id=" + sharked.data.page_id + "<br />";
        Response.Write(szWriteRes);
    }
    public string PostFunction(string serviceAddress, string szTicket)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

        request.Method = "POST";
        request.ContentType = "application/json";
        string strContent = "{\"ticket\": \"" + szTicket + "\"}";

        using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
        {
            dataStream.Write(strContent);
            dataStream.Close();
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string encoding = response.ContentEncoding;
        if (encoding == null || encoding.Length < 1)
        {
            encoding = "UTF-8"; //默认编码  
        }
        
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
        string retString = reader.ReadToEnd();
        return retString;

    }
    public string GetToken()
    {
        
        bool bNeedNew = true;
        if (Application["access_token"] != null&& Application["expires_in"] != null)
        {
            DateTime dt = DateTime.Parse(Application["expires_in"].ToString());
            TimeSpan sp = DateTime.Now.Subtract(dt);

            if (sp.TotalMinutes > 1)
            {
                return Application["access_token"].ToString();
                bNeedNew = false;
            }
            
        }
        if (bNeedNew)
        {
            string szRes = GetWebRequest("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid="+szAppID+"&secret="+ szAPPSercert + "");
            AppAccessToken apptocken= JSON.parse<AppAccessToken>(szRes);
            return apptocken.access_token;
        }
        return null;
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
  
}
