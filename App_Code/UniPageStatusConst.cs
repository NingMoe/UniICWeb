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
    public enum CONSTHTML
    {
        radioButton = 1, checkBox = 2, option = 3
    }
    public string szSplitChar = ",";
    public XmlDocument xml = new XmlDocument();
    public class CStatue
    {
        public string szValue;
        public string szName;
    }
    void LoadXml()
    {
        string strPaht = HttpRuntime.AppDomainAppPath;
        string szXmlName ="\\ConstConfig\\"+ConfigConst.GCSysFrame+ "ConstConfig.xml";
        try
        {
            xml.Load(strPaht + szXmlName);
        }
        catch
        { }
    }
    public ArrayList GetListFromXml(string szFieldName, uint? uStatus, bool bIsAll)
    {
        if (xml == null)
        {
            return null;
        }
        LoadXml();
        XmlNode nodeRoot = xml.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return null;
        }
        ArrayList list = new ArrayList();

        XmlNodeList nodes = nodeRoot.ChildNodes;
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            uint uValue = 0;
            uint.TryParse(szValue, out uValue);
            string szName = nodeTemp.InnerXml;
            if (!bIsAll)
            {
                if ((uStatus & uValue) > 0)
                {
                    CStatue temp = new CStatue();
                    temp.szValue = uValue.ToString();
                    temp.szName = szName;
                    list.Add(temp);
                }
            }
            else
            {
                CStatue temp = new CStatue();
                temp.szValue = szValue;// uValue.ToString();
                temp.szName = szName;
                list.Add(temp);
            }
        }
        return list;
    }
    public ArrayList GetListFromXml(string szFieldName, uint? uStatus, bool bIsAll,bool isEqual)
    {
        if (xml == null)
        {
            return null;
        }
        LoadXml();
        XmlNode nodeRoot = xml.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return null;
        }
        ArrayList list = new ArrayList();

        XmlNodeList nodes = nodeRoot.ChildNodes;
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            uint uValue = 0;
            uint.TryParse(szValue, out uValue);
            string szName = nodeTemp.InnerXml;
            if (!bIsAll)
            {
                if ((uStatus == uValue))
                {
                    CStatue temp = new CStatue();
                    temp.szValue = uValue.ToString();
                    temp.szName = szName;
                    list.Add(temp);
                }
            }
            else
            {
                CStatue temp = new CStatue();
                temp.szValue = uValue.ToString();
                temp.szName = szName;
                list.Add(temp);
            }
        }
        return list;
    }

    public string GetInputHtml(uint? uStatus, CONSTHTML uType, string szInputName, string szFieldName)
    {
        return GetInputHtmlFromXml(uStatus, uType, szInputName, szFieldName, false);
    }
    public string GetAllInputHtml(CONSTHTML uType, string szInputName, string szFieldName)
    {
        return GetInputHtmlFromXml(0, uType, szInputName, szFieldName, true);
    }
    public string GetInputHtmlFromXml(uint? uStatus, CONSTHTML uType, string szInputName, string szFieldName, bool bIsAll)
    {
        string szRes = "";
        string szType = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, bIsAll);
        if (uType == CONSTHTML.radioButton)
        {
            szType = "radio";
        }
        else if (uType == CONSTHTML.checkBox)
        {
            szType = "checkbox";
        }

        if (list == null || list.Count == 0)
        {
            return szRes;
        }
        if (uType != CONSTHTML.option)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    CStatue obj = (CStatue)list[i];
                    string szTemp = "<label><input class=\"enum\" type=\"" + szType + "\" name=\"" + szInputName + "\" value=\"" + obj.szValue + "\" /> " + obj.szName + "</label>";
                    szRes += szTemp;
                }
            }
        }
        else
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    CStatue obj = (CStatue)list[i];
                    string szTemp = "<option value=\"" + obj.szValue + "\"> " + obj.szName + "</option>";
                    szRes += szTemp;
                }
            }
        }
        return szRes;
    }
    public string GetJustName(uint? uStatus, string szFieldName)
    {
        string szRes = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, false);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szName + szSplitChar;
            }
        }
        return szRes;
    }
    public string GetJustNameEqual(uint? uStatus, string szFieldName)
    {
        string szRes = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, false, true);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szName + szSplitChar;
            }
        }
        return szRes;
    }
    public string GetJustName(uint? uStatus, string szFieldName,bool bIsSplit)
    {
        string szRes = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, false);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szName;
            }
        }
        return szRes;
    }
    public string GetJustNameEqual(uint? uStatus, string szFieldName, bool bIsSplit)
    {
        string szRes = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, false, true);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szName;
            }
        }
        return szRes;
    }
    public uint CharListToUint(string szState)
    {
        uint uRes = 0;
        if (szState == null || szState == "")
        {
            return uRes;
        }
        string[] szList = szState.Split(',');
        for (int i = 0; i < szList.Length; i++)
        {
            uRes += Parse(szList[i]);
        }
        return uRes;
    }
    public string UintToCharList(uint? uStatus, string szFieldName)
    {
        string szRes = "";
        ArrayList list = GetListFromXml(szFieldName, uStatus, false);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szValue + ",";
            }
        }
        if (szRes!=""&&szRes[szRes.Length - 1] == ',')
        {
            return szRes.Substring(0, szRes.Length - 1);
        }
        return szRes;
    }

    public ArrayList GetAndValListFromXml(string szFieldName, uint? uVal)
    {
        if (xml == null)
        {
            return null;
        }
        LoadXml();
        XmlNode nodeRoot = xml.SelectSingleNode("//field[@name='" + szFieldName + "']");
        if (nodeRoot == null)
        {
            return null;
        }
        ArrayList list = new ArrayList();

        XmlNodeList nodes = nodeRoot.ChildNodes;
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode nodeTemp = nodes[i];
            string szValue = nodeTemp.Attributes["value"].InnerXml;
            uint uValue = 0;
            uint.TryParse(szValue, out uValue);
            string szName = nodeTemp.InnerXml;

            if (((uVal & uValue) > 0))
            {
                CStatue temp = new CStatue();
                temp.szValue = uValue.ToString();
                temp.szName = szName;
                list.Add(temp);
            }

        }
        return list;
    }

    public string UintToAndCharList(uint? uNum, string szFieldName)
    {
        string szRes = "";
        ArrayList list = GetAndValListFromXml(szFieldName, uNum);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CStatue obj = (CStatue)list[i];
                szRes += obj.szName;
            }
        }
        return szRes;
    }


    public string GetInputItemHtml(CONSTHTML uType, string szInputName, string szName, string szValue)
    {
        return GetInputItemHtml(uType, szInputName, szName, szValue, false);
    }
    public string GetInputItemHtml(CONSTHTML uType, string szInputName, string szName, string szValue,bool isChecked)
    {
        string szType = "";
        string szRes = "";
        if (uType == CONSTHTML.radioButton)
        {
            szType = "radio";
        }
        else if (uType == CONSTHTML.checkBox)
        {
            szType = "checkbox";
        }
        if (uType != CONSTHTML.option)
        {
            if (isChecked)
            {
                string szTemp = "<label><input class=\"enum\" checked=\"checked\" type=\"" + szType + "\" name=\"" + szInputName + "\" value=\"" + szValue + "\" /> " + szName + "</label>";
                szRes += szTemp;
            }
            else
            {
                string szTemp = "<label><input class=\"enum\" type=\"" + szType + "\" name=\"" + szInputName + "\" value=\"" + szValue + "\" /> " + szName + "</label>";
                szRes += szTemp;
            }
        }
        else if (uType == CONSTHTML.option)
        {
            if (isChecked)
            {
                string szTemp = "<option selected=\"selected\" value=\"" + szValue + "\"> " + szName + "</option>";
                szRes += szTemp;
            }
            else
            {
                string szTemp = "<option value=\"" + szValue + "\"> " + szName + "</option>";
                szRes += szTemp;  
            }
        }
        else {
            string szTemp = "<option value=\"" + szValue + "\"> " + szName + "</option>";
            szRes += szTemp;
        }
        return szRes;
    }
}
