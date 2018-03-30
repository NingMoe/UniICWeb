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

       
    }
    
}
