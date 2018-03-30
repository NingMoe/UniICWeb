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

public partial class _Default : UniPage
{
    public string m_szMsg = "";
    public string m_szMsg2 = "";
    public string m_szTitle = "";
    public string m_szColorBg = "#fff";
    public string m_szColor = "#000";
	public uint m_nType = 0;
	
	protected void Page_Load(object sender, EventArgs e)
	{
		m_nType = ToUint(Request["type"]);
		m_szTitle = Server.UrlDecode(Request["title"]);
		m_szMsg = Server.UrlDecode(Request["msg"]);
		m_szMsg2 = Server.UrlDecode(Request["msg2"]);

        m_szMsg = m_szMsg.Replace("\n", "<br/>");
        m_szMsg2 = m_szMsg2.Replace("\n", "<br/>");
		if(m_nType == 1)
		{
			m_szColorBg = "#0c8";
			m_szColor = "#fff";
		}else{
			m_szColorBg = "#d00";
			m_szColor = "#fff";
		}
	}
}
