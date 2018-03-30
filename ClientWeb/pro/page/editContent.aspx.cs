using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UniWebLib;

public partial class ClientWeb_md_ueditor_net_editContent : UniClientPage
{
    protected string url;
    protected string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
        if (!IsPostBack)
        {
            //初始化模块
            string m_name = Request["name"];
            if (!string.IsNullOrEmpty(m_name)) name ="："+Server.UrlDecode(m_name);
            string m_info_id=Request["id"];
            string m_info_type=Request["type"];
            if (Request["title"] != null)
                infoTitle.Value = Server.UrlDecode(Request["title"]);
            if (!string.IsNullOrEmpty(m_info_id) && !string.IsNullOrEmpty(m_info_type))
            {
                //保存id与信息类型值用于ajax
                infoId.Value = m_info_id;
                infoType.Value = m_info_type;
                XmlCtrl.XmlInfo info=GetDftXmlInfo(m_info_id, m_info_type);
                //获取编辑内容
                editContent.Value = info.content;
                infoTitle.Value = info.title;
            }
        }
    }
}