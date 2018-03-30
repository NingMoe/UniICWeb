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
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Xsl;

/// <summary>
/// FileCtrl 的摘要说明
/// </summary>
public class FileCtrl
{
    System.Collections.ArrayList alst;
    public FileCtrl()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public void GetFiles(string dir)
    {
        try
        {
            string[] files = Directory.GetFiles(dir);//得到文件
            foreach (string file in files)//循环文件
            {
                string exname = file.Substring(file.LastIndexOf(".") + 1);//得到后缀名
                // if (".txt|.aspx".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//查找.txt .aspx结尾的文件
                //if (".txt".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//如果后缀名为.txt文件
                {
                    FileInfo fi = new FileInfo(file);//建立FileInfo对象
                    alst.Add(fi.Name);//把.txt文件全名加人到FileInfo对象
                }
            }
        }
        catch (Exception e)
        {

        }
    }
    public string[] readlist(string path)
    {
        alst = new System.Collections.ArrayList();//建立ArrayList对象
        GetDirs(path);//得到文件夹
        return (string[])alst.ToArray(typeof(string));//把ArrayList转化为string[]
    }

    public void GetDirs(string d)//得到所有文件夹
    {
        GetFiles(d);//得到所有文件夹里面的文件
        try
        {
            string[] dirs = Directory.GetDirectories(d);
            foreach (string dir in dirs)
            {
                GetDirs(dir);//递归
            }
        }
        catch
        {
        }
    }
}

public class XmlCtrl
{
    public string xmlFileName;//文件名（全路径）
    public string dir;//路径
    //键值对结构体
    public struct XmlInfo
    {
        public string id;
        public string type;
        public string title;
        public string content;
        public string date;
        public string alter;//修改日期
        public string state;
        public string attrs;//自定义格式 属性字符串
    }
    //构造函数
    public XmlCtrl(string xmlFileName, string dir)
    {
        InitCtrl(xmlFileName, dir);
    }
    public XmlCtrl(string path)
    {
        InitCtrl(path, "");
    }
    public XmlCtrl()
    {
        this.xmlFileName = "\\db.xml";
        this.dir = "\\";
    }
    private void InitCtrl(string xmlFileName, string dir)
    {
        //文件名(或全路径)
        if (string.IsNullOrEmpty(xmlFileName))
            xmlFileName = "db.xml";
        else if (xmlFileName.Split('.').Length == 1)
            xmlFileName += ".xml";
        //路径
        string path = dir + xmlFileName;//合并
        if (path.IndexOf("\\")<0) path = "\\"+path;
        this.xmlFileName = path;
        this.dir=path.Substring(0, path.LastIndexOf("\\")+1);
    }
    //xml读取 获取内容
    public XmlInfo GetXmlContent(string id, string type)
    {
        XmlInfo info = new XmlInfo();
        if (!Directory.Exists(dir))
        {
            return info;
        }
        string m_data_path = xmlFileName;
        if (!File.Exists(m_data_path))
        {
            return info;
        }
        try
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(m_data_path);
            XmlNode root = xml.SelectSingleNode("UNI");
            XmlNode typeNode = root.SelectSingleNode(type);
            //根据id搜索节点并返回内容
            XmlNodeList nodes = typeNode.SelectNodes("Info");
            foreach (XmlNode item in nodes)
            {
                if (item.Attributes["id"].Value == id)
                {
                    if (item.InnerText != null)
                    {
                        info.id = id;
                        info.type = type;
                        if (item.Attributes["date"] != null)//创建时间
                        {
                            info.date = item.Attributes["date"].Value;
                        }
                        if (item.Attributes["alter"] != null)//修改时间
                        {
                            info.alter = item.Attributes["alter"].Value;
                        }
                        if (item.Attributes["state"] != null)//状态
                        {
                            info.state = item.Attributes["state"].Value;
                        }
                        if (item.Attributes["title"] != null)//标题
                        {
                            info.title = item.Attributes["title"].Value;
                        }
                        if (item.Attributes["attrs"] != null)//属性
                        {
                            info.attrs = item.Attributes["attrs"].Value;
                        }
                        string str = item.InnerText;
                        if (str.Length > 2)
                            info.content = str.Substring(1, str.Length - 2);
                        else
                            info.content = "";
                        return info;
                    }
                }
            }
            return info;
        }
        catch (Exception ex)
        {
            //info.content="<span style='display:none;'>错误：" + ex.Message + "</span>";
            return info;
        }
    }
    //获取xml内容列表
    public XmlInfo[] GetXmlList(string type, int need)
    {
        return GetXmlList(type, 1, need, 0, Int32.MaxValue,true);
    }
    //获取xml内容列表 
    public XmlInfo[] GetXmlList(string type, int startline, int need)
    {
        return GetXmlList(type, startline, need, 0, Int32.MaxValue, true);
    }
    //获取xml内容列表
    public XmlInfo[] GetXmlList(string type, int startline, int need, int start, int end, bool byContent)
    {
        if (!Directory.Exists(dir))
        {
            return null;
        }
        string m_data_path =  xmlFileName;
        if (!File.Exists(m_data_path))
        {
            return null;
        }
        try
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(m_data_path);
            XmlNode root = xml.SelectSingleNode("UNI");
            XmlNode typeNode = root.SelectSingleNode(type);
            //根据id搜索节点并返回内容
            XmlNodeList nodes = typeNode.SelectNodes("Info");
            List<XmlInfo> list = new List<XmlInfo>();
            int ori = 0;
            int line = startline + need;
            foreach (XmlNode item in nodes)
            {
                if (item.Attributes["date"] == null)
                    continue;
                //日期
                int date = Convert.ToInt32(item.Attributes["date"].Value.Substring(0, 8));
                if (date < start)
                    continue;
                if (date > end)
                    break;
                //条目
                ori++;
                if (ori < startline)
                    continue;
                if (ori >= line)
                    break;
                //
                XmlInfo info = new XmlInfo();
                info.id = item.Attributes["id"].Value;
                info.type = type;
                if (byContent)
                {
                    string str = item.InnerText;
                    if (str.Length > 2)
                        info.content = str.Substring(1, str.Length - 2);
                    else
                        info.content = "";
                }
                info.date = item.Attributes["date"].Value;
                if (item.Attributes["alter"] != null)//修改时间
                {
                    info.alter = item.Attributes["alter"].Value;
                }
                if (item.Attributes["state"] != null)//状态
                {
                    info.state = item.Attributes["state"].Value;
                }
                if (item.Attributes["title"] != null)//标题
                {
                    info.title = item.Attributes["title"].Value;
                }
                if (item.Attributes["attrs"] != null)
                {
                    info.attrs = item.Attributes["attrs"].Value;
                }
                list.Add(info);
            }
            return list.ToArray();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    //xml存储 保存内容
    public bool SaveXmlData(string id, string data, string type,string title, string attrs,string state)
    {
        if (title == null) title = "";
        // 初始化数据文件路径
        string m_data_path;
        string now = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        m_data_path = xmlFileName;

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
        {
            try
            {
                xml.Load(m_data_path);
            }
            catch (Exception ex)
            {
                Logger.trace("m_data_path="+ m_data_path);
                Logger.trace(ex.ToString());
            }
        }
        try
        {
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
                    XmlElement e = (XmlElement)item;
                    e.SetAttribute("title", title);
                    e.SetAttribute("alter", now);
                    if (!string.IsNullOrEmpty(state))
                    {
                        e.SetAttribute("state", state);
                    }
                    if (!string.IsNullOrEmpty(attrs))//属性
                    {
                        e.SetAttribute("attrs", attrs);
                    }
                    xml.Save(m_data_path);
                    return true;
                }
            }
            //未搜索到同id节点则创建新节点并存储
            XmlElement newele = xml.CreateElement("Info");
            newele.InnerText = data;
            newele.SetAttribute("id", id);
            newele.SetAttribute("date", now);
            newele.SetAttribute("alter", now);
            newele.SetAttribute("title", title);
            if (!string.IsNullOrEmpty(state))
            {
                newele.SetAttribute("state", state);
            }
            if (!string.IsNullOrEmpty(attrs))//属性
            {
                newele.SetAttribute("attrs", attrs);
            }
            //typeNode.AppendChild(newele);
            typeNode.PrependChild(newele);
            xml.Save(m_data_path);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    //删除信息对象 成功则返回‘ok’
    public string DelXmlInfo(string id, string type)
    {
        if (!Directory.Exists(dir))
        {
            return "路径不存在";
        }
        string m_data_path = xmlFileName;
        if (!File.Exists(m_data_path))
        {
            return "文件不存在";
        }
        try
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(m_data_path);
            XmlNode root = xml.SelectSingleNode("UNI");
            XmlNode typeNode = root.SelectSingleNode(type);
            XmlNodeList nodes = typeNode.SelectNodes("Info");
            if (!string.IsNullOrEmpty(id))//删除单个
            {
                foreach (XmlNode item in nodes)
                {
                    if (item.Attributes["id"].Value == id)
                    {
                        typeNode.RemoveChild(item);
                        xml.Save(m_data_path);
                        return "ok";
                    }
                }
            }
            else//删除本类型全部
            {
                typeNode.RemoveAll();
                xml.Save(m_data_path);
                return "ok";
            }
            return "对象不存在";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    //test
    public void ConvertXSL()
    {
        //XmlTextWriter myWriter = new XmlTextWriter("toSolrXML.xml", null);

        //myWriter.Formatting = Formatting.Indented;

        //XslCompiledTransform t = new XslCompiledTransform();

        //XmlReaderSettings settings = new XmlReaderSettings();

        //settings.ProhibitDtd = false;

        //XmlReader xmlReader = XmlReader.Create(tbXMLFile.Text, settings);

        //XmlReader xslReader = XmlReader.Create(tbXSLFile.Text);

        //t.Load(xslReader);

        //t.Transform(xmlReader, null, myWriter);
    }

}