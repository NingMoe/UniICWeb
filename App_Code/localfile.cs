using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Collections;
using System.Globalization;
using System.Reflection;
using UniWebLib;
using UniLibrary;

/// <summary>
///  的摘要说明
/// </summary>
public partial class UniPage : UniWebLib.UniPage
{
    public XmlDocument xmlTemp = new XmlDocument();
    public class CStatueTemp
    {
        public string szValue;
        public string szName;
    }
    void LoadXmlTemp()
    {
        string strPaht = HttpRuntime.AppDomainAppPath;
        string szXmlName = "\\LocalFile\\" + "file.xml";
        try
        {
            xmlTemp.Load(strPaht + szXmlName);
        }
        catch
        { }
    }
    void savemlTemp()
    {
        string strPaht = HttpRuntime.AppDomainAppPath;
        string szXmlName = "\\LocalFile\\" + "file.xml";
        try
        {
            xmlTemp.Save(strPaht + szXmlName);
        }
        catch
        { }
    }
    public ArrayList GetListByFieldName(string szFieldName)
    {
        if (xmlTemp == null)
        {
            return null;
        }
        LoadXmlTemp();
        XmlNode nodeRoot = xmlTemp.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return null;
        }
       
        XmlNodeList nodes = nodeRoot.ChildNodes;
        ArrayList list = new ArrayList();
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            uint uValue = 0;
            uint.TryParse(szValue, out uValue);
            string szName = nodeTemp.InnerXml;
           
            {
                CStatueTemp temp = new CStatueTemp();
                temp.szValue = szValue;// uValue.ToString();
                temp.szName = szName;
                list.Add(temp);
            }
        }
        return list;
    }
    public CStatueTemp GetListByFieldName(string szFieldName, string value)
    {
        if (xmlTemp == null)
        {
            return null;
        }
        LoadXmlTemp();
        XmlNode nodeRoot = xmlTemp.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return null;
        }

        XmlNodeList nodes = nodeRoot.ChildNodes;
      
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            
            if (szValue == value)
            {
                CStatueTemp newTemp = new CStatueTemp();
                newTemp.szValue = value;
                newTemp.szName = nodeTemp.InnerText; ;
                return newTemp;
            }
            
        }
        return null;
    }
    public bool add(string szFieldName, string value,string szName)
    {
        if (xmlTemp == null)
        {
            return false;
        }
        LoadXmlTemp();
        XmlNode nodeRoot = xmlTemp.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return false;
        }
       
        XmlNodeList nodes = nodeRoot.ChildNodes;
        /*
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            if (szValue == value)
            {
                nodeTemp.InnerText = szName;
                savemlTemp();
                return true;
            }
        }*/
        XmlElement xm1 = xmlTemp.CreateElement("option");
        xm1.SetAttribute("value", value);
        xm1.InnerText = szName;
        //xm1.AppendChild(nodeRoot);
        nodeRoot.AppendChild(xm1);
        savemlTemp();

        return true;
    }
    public bool del(string szFieldName, string value)
    {
        if (xmlTemp == null)
        {
            return false;
        }
        LoadXmlTemp();
        XmlNode nodeRoot = xmlTemp.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return false;
        }

        XmlNodeList nodes = nodeRoot.ChildNodes;
      
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            if (szValue == value)
            {
                nodeRoot.RemoveChild(nodeTemp);
                savemlTemp();
                return true;
            }
        }
        return false;
    }
    public bool update(string szFieldName, string value, string szName,string szOldValue)
    {
        if (xmlTemp == null)
        {
            return false;
        }
        LoadXmlTemp();
        XmlNode nodeRoot = xmlTemp.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return false;
        }

        XmlNodeList nodes = nodeRoot.ChildNodes;

        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            if (szValue == szOldValue)
            {
                nodeTemp.InnerText = szName;
                nodeTemp.Attributes["value"].Value= value;
                savemlTemp();
                return true;
            }
        }
        return false;
    }
}
