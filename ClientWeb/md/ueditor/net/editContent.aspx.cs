using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class ClientWeb_md_ueditor_net_editContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //与项目相关，判断是否登录，可去除
        if (Session["LOGIN_ACCINFO"] == null)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('未登录或已超时')", true);
            return;
        }
        ////
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
                //获取编辑内容
                editContent.Value = GetContent(m_info_id, m_info_type);
            }
            else
            {
                //m_info_id = "99999";//测试
                //测试
                //infoId.Value = m_info_id;
                //editContent.Value = GetContent(m_info_id, "OV");
                //if (Request.UrlReferrer != null)
                //    Response.Redirect(Request.UrlReferrer.ToString());
                //else
                //    Response.Redirect("../Login.aspx");
            }
        }
    }

    private string GetContent(string id, string type)
    {
        // 初始化数据文件路径
        //获取数据文件路径
        string dir = Server.MapPath("../../../upload/info/xmlData/");
        if (!Directory.Exists(dir))
        {
            return "";
        }
        string m_data_path = dir + "ics_data.xml";
        if (!File.Exists(m_data_path))
        {
            return "";
        }
        XmlDocument xml = new XmlDocument();
        xml.Load(m_data_path);

        XmlNode root = xml.SelectSingleNode("UNI");
        if (root.SelectSingleNode(type) == null)
        {
            return "";
        }
        XmlNode typeNode = root.SelectSingleNode(type);
        //根据id搜索节点并返回内容
        XmlNodeList nodes = typeNode.SelectNodes("Info");
        foreach (XmlNode item in nodes)
        {
            if (item.Attributes["id"].Value == id)
            {
                if (item.InnerText != null)
                {
                    string str = item.InnerText;
                    return str.Substring(1, str.Length - 2);
                }
            }
        }
        return "";
    }
}