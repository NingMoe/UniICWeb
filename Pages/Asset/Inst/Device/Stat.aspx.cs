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
public partial class Sub_Summary :UniPage
{
    protected string m_DevTeaching = "";
    protected void Page_Load(object sender, EventArgs e)
    {        
        CURDEVSTAT curDevStat;// = new CURDEVSTAT(); ;
        m_Request.Device.CurDevStat(out curDevStat);
        uint? uTotal =curDevStat.TeachingDevStat.dwTotalNum;//总数
        uint? uIdelNum = curDevStat.TeachingDevStat.dwIdleNum;//空闲中       
        uint? uUseNum = curDevStat.TeachingDevStat.dwUseNum;//使用中
        if (uTotal == null)            
        {
            uTotal = 0;
        }
        if(uIdelNum==null)
        {
            uIdelNum = 0;
        }
        if (uUseNum == null)
        {
            uUseNum = 0;
        }
        m_DevTeaching += "<p data-value=" + uTotal + ">教学所需数：" + uTotal + "台</p>";
        m_DevTeaching += "<p data-value=" + uUseNum + ">使用中：" + uUseNum + "台</p>";
        m_DevTeaching += "<p data-value=" + uIdelNum + ">空闲中：" + uIdelNum + "台</p>";
    }
}
