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

public partial class WebUserControl : UniClientModule
{
    public string szDevCLS = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string lg = ConfigurationManager.AppSettings["mustLogin"];
        string clsKind = ConfigurationManager.AppSettings["icClsKind"];
        if (!string.IsNullOrEmpty(lg)&&lg == "1")
        {
            if (!IsLogined())
            {
                Response.Redirect("Login.aspx");
            }
        }
        if (clsKind == null) clsKind = "";
        if(clsKind.IndexOf("common")>=0)
        InitDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        if (clsKind.IndexOf("seat") >= 0)
            InitDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        if (clsKind.IndexOf("loan") >= 0)
            InitDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN);
        if (clsKind.IndexOf("computer") >= 0)
            InitDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
    }
    private void InitDevCls(uint kind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        vrGet.dwKind = kind;
        UNIDEVCLS[] vtRes;
        vrGet.szReqExtInfo.szOrderKey = "szClassName";//编号：dwClassID  名称：szClassName
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                //string szLongResv = "false";
                //string isKind = "false";
                //DEVKINDREQ req = new DEVKINDREQ();
                //req.szClassName = vtRes[i].szClassName;
                //UNIDEVKIND[] rlt;
                //if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
                //{
                //    if (GetConfig("resvAllDay") == "1" && (rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                //    {
                //        szLongResv = "true";
                //    }
                //    if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
                //    {
                //        isKind = "true";
                //    }
                //}"&isLongResv=" + szLongResv + "&isKind=" + isKind +
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    continue;
                }
                szDevCLS += "<li clskind='" + kind + "' class_id='" + vtRes[i].dwClassID + "'><a href=\"" + "space_kind_research" + ".aspx?classKind=" + vtRes[i].dwKind +  "&classId=" + vtRes[i].dwClassID.ToString() + "\"><span>" + vtRes[i].szClassName.ToString() + "</span></a></li>";
            }
        }
    }
}
