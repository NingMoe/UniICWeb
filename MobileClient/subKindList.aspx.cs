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

    protected override void OnLoadComplete(EventArgs e)
    {
        uint uClassKind = Parse(Request["classKind"]);

        DEVKINDREQ vrGet = new DEVKINDREQ();
        vrGet.dwClassKind = uClassKind;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        
        UNIDEVKIND[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Device.DevKindGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                UNIDEVCLS devCls;
                if (vtRes[i].dwClassID!=null&&GetDevCLSByID(vtRes[i].dwClassID.ToString(), out devCls))
                {
                    if (devCls.szMemo == "false")
                    {
                       continue;
                    }
                }
                if(! ((((uint)vtRes[i].dwClassKind) & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) > 0))
                {
                    continue;
                }
                uint uProp = (uint)vtRes[i].dwProperty;
                if ((uProp & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                {
                    continue;
                }
                m_szOut += "<div class=\"Item\">";
                

                //TODO:DEBUG
                vtRes[i].dwUsableNum = 1;

                if (vtRes[i].dwUsableNum > 0)
                {
                    m_szOut += "<a class=\"KHead actDevice\" href=\"#\" data-id=\"" + vtRes[i].dwKindID + "\">" + vtRes[i].szKindName + "<span class=\"memo\">" + vtRes[i].szMemo + "</span> <span class=\"stat1\">" + "" + "</span>";
                }
                else
                {
                    m_szOut += "<a class=\"KHead\">" + vtRes[i].szKindName + "<span class=\"memo\">" + vtRes[i].szMemo + "</span> <span class=\"stat2\">" + "" + "</span>";
                }
                m_szOut += "</a></div>";
            }
        }
        else
        {
            //Logger.trace(vtRes.Length.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
}
