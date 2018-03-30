using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UniWebLib;


public partial class ClientWeb_m_all_resvsub : UniClientPage
{
    public string szSearchKey = "姓名/登录名搜索";
    protected string themeOptions = "";
    protected string themeKind= "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szAccLogonName = GetConfig("searchAccLogonName");
        if (szAccLogonName == "4")
        {
            szSearchKey = Translate("校园卡号/读者证号/学号/工号搜索");
        }
        string themes = GetConfig("fixTheme");
        if (themes == "1")
        {
            XmlNodeList list = common.GetXMLConst(Server.MapPath("~/LocalFile/file.xml"), "ResvTheme");
            if (list != null)
            {
                foreach (XmlNode item in list)
                {
                    string opt = item.InnerText;
                    themeOptions += "<option value='" + opt + "'>" + opt + "</option>";
                }
            }
        }
        CODINGTABLEREQ req = new CODINGTABLEREQ();
        req.dwCodeType = (uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND;
        
        CODINGTABLE[] rlt;
        if (m_Request.System.GetCodingTable(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                themeKind += "<option value='" + rlt[i].szCodeSN + "'>" + rlt[i].szCodeName + "</option>";
            }
        }

    }
}