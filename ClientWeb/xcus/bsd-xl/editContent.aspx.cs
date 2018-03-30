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
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
        if (!IsPostBack)
        {
            //初始化模块
            string m_info_id;
            string m_info_type;
            if (!string.IsNullOrEmpty(Request["id"]) && !string.IsNullOrEmpty(Request["type"]))
            {
                m_info_id = Request["id"].ToString();
                m_info_type = Request["type"];
                //保存id与信息类型值用于ajax
                infoId.Value = m_info_id;
                infoType.Value = m_info_type;
                if (Request["title"] != null)
                infoTitle.Value = Request["title"];
                //获取编辑内容
                editContent.Value = GetXmlContent(m_info_id, m_info_type);
            }
        }
    }
}