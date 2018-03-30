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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using UniWebLib;
using System.IO;

public partial class ClientWeb_xcus_all_openaty : UniClientPage
{
    protected string isBack = "none";
    protected string atyKinds = "";
    protected string kindurl = "";
    UNIDEVICE curDev;
    protected void Page_Load(object sender, EventArgs e)
    {
        string szKindID= Request["devkind"];
        DEVKINDREQ kindGet = new DEVKINDREQ();
        kindGet.dwKindID = ToUInt(szKindID);
        UNIDEVKIND[] vtKind;
        if (m_Request.Device.DevKindGet(kindGet, out vtKind) == REQUESTCODE.EXECUTE_SUCCESS && vtKind != null && vtKind.Length > 0)
        {
            kindurl = vtKind[0].szDevKindURL.ToString();
        }
        if (Request["back"] == "true") isBack = "";
        if (IsLogined())
        {
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if ((ToUInt(GetConfig("resvKind")) & 1024) > 0)
            {
                CODINGTABLE[] list=GetCodeTable((uint)CODINGTABLE.DWCODETYPE.CODE_ACTIVITYKIND,null);
                if (list != null && list.Length > 0)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        atyKinds += "<option value='" + list[i].szCodeSN + "'>" + list[i].szCodeName + "</option>";
                    }
                }
            }
        }
    }
}
