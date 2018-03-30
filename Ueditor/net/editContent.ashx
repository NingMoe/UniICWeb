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
        if (data != null && id != null && type != null&&id!=""&&type!="")
        {
            string result = SaveData(id, data, type);
            res.ContentType = "text/plain";
            res.Write(result);
            res.End();
        }
        else
        {
            res.ContentType = "text/plain";
            res.Write("error");
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
        //获取数据文件路径
        string dir = HttpContext.Current.Server.MapPath("IntroFile");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        m_data_path = dir + "\\"+type+".xml";

        XmlDocument xml = new XmlDocument();

        //若未创建xml数据文件，则初始化xml文件
        if (!File.Exists(m_data_path))
        {
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(dec);
            XmlElement ele = xml.CreateElement(type);
            xml.AppendChild(ele);
        }
        else
        {
            xml.Load(m_data_path);
        }

        //获取根节点
        XmlNode root = xml.SelectSingleNode(type);
        //获取类型节点
        if (root == null)
        {
            //XmlElement ele = xml.CreateElement(type);
            //root.AppendChild(ele);
        }
        //根据id搜索到节点，并存储
        XmlNodeList nodes = root.SelectNodes("Info");
        foreach (XmlNode item in nodes)
        {
            if (item.Attributes["id"].Value == id)
            {
                item.InnerText = data;
                xml.Save(m_data_path);
                return "ok";
            }
        }
        //未搜索到同id节点则创建新节点并存储
        XmlElement newele = xml.CreateElement("Info");
        newele.InnerText = data;
        newele.SetAttribute("id", id);
        newele.SetAttribute("memo", "");
        root.AppendChild(newele);
        xml.Save(m_data_path);
        return "ok";
    }

}