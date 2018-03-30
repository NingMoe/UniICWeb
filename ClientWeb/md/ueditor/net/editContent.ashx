<%@ WebHandler Language="C#" Class="editContent" %>

using System;
using System.Web;
using System.IO;
using System.Xml;

public class editContent : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpResponse res = context.Response;
        HttpRequest req = context.Request;

        string data = req["content"];
        string id = req["id"];
        string type = req["type"];
        if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            string result = SaveData(id, data, type);
            res.ContentType = "text/plain";
            res.Write(result);
            res.End();
        }
        else
        {
            res.ContentType = "text/plain";
            res.Write("{\"ret\":0}");
            res.End();
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private string SaveData(string id, string data, string type)
    {
        // 初始化数据文件路径
        string m_data_path;
        string now=DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //获取数据文件路径
        string dir = HttpContext.Current.Server.MapPath("../../../upload/info/xmlData/");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        m_data_path = dir + "ics_data.xml";

        XmlDocument xml = new XmlDocument();

        //若未创建xml数据文件，则初始化xml文件
        if (!File.Exists(m_data_path))
        {
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(dec);
            XmlElement ele = xml.CreateElement("UNI");
            xml.AppendChild(ele);
        }
        else
            xml.Load(m_data_path);

        //获取根节点
        XmlNode root = xml.SelectSingleNode("UNI");
        //获取类型节点
        if (root.SelectSingleNode(type) == null)
        {
            XmlElement ele = xml.CreateElement(type);
            root.AppendChild(ele);
        }
        XmlNode typeNode = root.SelectSingleNode(type);
        //根据id搜索到节点，并存储
        XmlNodeList nodes = typeNode.SelectNodes("Info");
        foreach (XmlNode item in nodes)
        {
            if (item.Attributes["id"].Value == id)
            {
                item.InnerText = data;
                XmlElement e=(XmlElement)item;
                e.SetAttribute("alter",now);
                xml.Save(m_data_path);
                return "{\"ret\":1}";
            }
        }
        //未搜索到同id节点则创建新节点并存储
        XmlElement newele = xml.CreateElement("Info");
        newele.InnerText = data;
        newele.SetAttribute("id", id);
        newele.SetAttribute("date", now);
        newele.SetAttribute("alter", now);
        typeNode.AppendChild(newele);
        xml.Save(m_data_path);
        return "{\"ret\":1}";
    }
}