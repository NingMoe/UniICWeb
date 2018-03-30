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

public partial class _Default : PageBase
{
    protected MyString m_szOut = new MyString();
    protected uint uDate=0;
    protected override void OnLoadComplete(EventArgs e)
    {

        m_szOut += "<div class=\"Item test\" data-kindid=\"" + "123" + "\" data-id=\"" + "24" + "\">";
        m_szOut += "<div class=\"LHead\">" + "全天(8:00-22:00)" + "空闲时间大于<select><option value=2>2小时</option><option value=4>4小时</option><option value=6>6小时</option><option value=8>8小时</option><option value=10>10小时</option></select></div>";
        m_szOut += "<div class=\"LBtn\"></div>";
        m_szOut += "</div>";

        m_szOut += "<div class=\"Item test\" data-kindid=\"" + "123" + "\" data-id=\"" + "24" + "\">";
        m_szOut += "<div class=\"LHead\">" + "上午（8：00-12:00)空闲时间大于<select><option value=1>1小时</option><option value=2>2小时</option><option value=3>3小时</option><option value=4>4小时</option></select></div>";
        m_szOut += "<div class=\"LBtn\"></div>";
        m_szOut += "</div>";

        m_szOut += "<div class=\"Item test\" data-kindid=\"" + "123" + "\" data-id=\"" + "24" + "\">";
        m_szOut += "<div class=\"LHead\">" + "下午（12：00-18:00）空闲时间大于<select><option value=1>1小时</option><option value=2>2小时</option><option value=3>3小时</option><option value=4>4小时</option></select></div>";
        m_szOut += "<div class=\"LBtn\"></div>";
        m_szOut += "</div>";

        m_szOut += "<div class=\"Item test\" data-kindid=\"" + "123" + "\" data-id=\"" + "24" + "\">";
        m_szOut += "<div class=\"LHead\">" + "晚上（18：00-22:00）空闲时间大于<select><option value=1>1小时</option><option value=2>2小时</option><option value=3>3小时</option><option value=4>4小时</option></select></div>";
        m_szOut += "<div class=\"LBtn\"></div>";
        m_szOut += "</div>";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
}
