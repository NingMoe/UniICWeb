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
using UniWebLib;

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szTimeID = "";
      XmlCtrl xmlCtrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        int need=25;
        szTimeID = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm")).ToString();
       xmlCtrl= new XmlCtrl("ics_data", Server.MapPath(MyVPath + "clientweb/upload/info/xmlData/"));
       if (Request["delID"] != null && Request["delID"] != "")
       {
           Del(Request["delID"]);
       }
        XmlCtrl.XmlInfo[] vrResult = xmlCtrl.GetXmlList("notice", need);
        if (vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].id.ToString() + "\">" + vrResult[i].title + "</td>";
                uint uDate = Parse(vrResult[i].date.Substring(0,8));
                m_szOut += "<td>" + GetDateStr(uDate)+ "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }
    }
    private void Del(string id)
    {
        xmlCtrl.DelXmlInfo(id, "notice");
    }
}
