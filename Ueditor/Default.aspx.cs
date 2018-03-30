using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected string MyVPath = "/";
    public string Theme = "redmond";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ApplicationPath != "/")
        {
            MyVPath = Request.ApplicationPath + "/";
        }
        else
        {
            MyVPath = "/";
        }
        if (!IsPostBack)
        {
            //初始化概览模块id
            string szID=Request["id"];
            string szType=Request["type"];           
            if ( szID!= null &&szID != ""&&szType!=null&&szType!="")
            {                          
                infoId.Value = szID;
                infoType.Value = szType;
                //获取编辑内容
                editContent.Value = GetContent(szID, szType);
            }
            else
            {            
                if (Request.UrlReferrer != null)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    //Response.Redirect("../Login.aspx");
                }
            }
        }
        
    }

    private string GetContent(string id, string type)
    {
        // 初始化数据文件路径
        //获取数据文件路径 
        string path1 = Request.ApplicationPath;
        string dir = Server.MapPath(path1+"/Ueditor/net/IntroFile");
        if (!Directory.Exists(dir))
        {
            return "";
        }
        string m_data_path = dir + "\\"+type+".xml";
        if (!File.Exists(m_data_path))
        {
            return "";
        }
        XmlDocument xml = new XmlDocument();
        xml.Load(m_data_path);

        XmlNode root = xml.SelectSingleNode(type);
        if (root==null)
        {
            return "";
        }
        //根据id搜索节点并返回内容
        XmlNodeList nodes = root.SelectNodes("Info");        
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