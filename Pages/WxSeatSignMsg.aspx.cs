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
    public string m_szResvMsg = "";
    public string m_szTitle = "";
    public string m_szColorBg = "#fff";
    public string m_szColor = "#000";
    public uint m_nType = 0;
    public string m_szTrueName = "";
    public string m_szDevName = "";
    public uint m_dwMinUseMin = 0;
    public uint m_dwMaxUseMin = 0;
    public string m_szTimes = "";
    public string sysid = "";
    public string aluserid = "";
    public string wxuserid = "";
    public uint status=0;//是否显示提前预约按钮
    protected void Page_Load(object sender, EventArgs e)
	{
        m_nType = ToUint(Request["type"]);
		m_szTitle = Server.UrlDecode(Request["title"]);
		m_szMsg = Server.UrlDecode(Request["msg"]);
		m_szMsg2 = Server.UrlDecode(Request["msg2"]);
        m_szResvMsg= Server.UrlDecode(Request["ResvMsg"]);

        m_szMsg = m_szMsg.Replace("\n", "<br/>");
        m_szMsg2 = m_szMsg2.Replace("\n", "<br/>");
       // m_szResvMsg= m_szResvMsg.Replace("\n", "<br/>");
        m_dwMinUseMin = ToUint(Request["dwMinUseMin"]);
        m_dwMaxUseMin = ToUint(Request["dwMaxUseMin"]);
        m_szTrueName = Server.UrlDecode(Request["szTrueName"]);
        m_szDevName = Server.UrlDecode(Request["szDevName"]);

        sysid = Server.UrlDecode(Request["sysid"]);
        aluserid = Server.UrlDecode(Request["aliUserid"]);
        wxuserid = Server.UrlDecode(Request["wxUserid"]);

        status= ToUint(Request["status"]);
        if (m_nType == 1 || m_nType == 3 || m_nType == 4 || m_nType == 8 || m_nType == 16)
		{
			m_szColorBg = "#0c8";
			m_szColor = "#fff";
		}else{
			m_szColorBg = "#d00";
			m_szColor = "#fff";
		}

        if( m_dwMaxUseMin != 0 && m_dwMaxUseMin >= m_dwMinUseMin)
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["scanResvMaxMins"];
            if (s != null && s.ToString() == "1")
            {
                m_dwMaxUseMin = m_dwMaxUseMin - 20;
                m_szTimes += "<option value='" + m_dwMaxUseMin + "'>" + m_dwMaxUseMin + "分钟" + "</option>";
            }
            else
            {
                for (uint i = m_dwMinUseMin; i <= m_dwMaxUseMin; i += 30)
                {
                    m_szTimes += "<option value='" + i + "'>" + i + "分钟" + "</option>";
                }
            }
        }
	}
}
