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

public partial class Sub_Course : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string Title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szState=Request["state"];
        m_szOut += "<tr>";
        m_szOut += "<td>1</td>";
        m_szOut += "<td>DellPC001</td>";
        m_szOut += "<td>10.121.0.1</td>";
        m_szOut += "<td>Dell电脑</td>";
        m_szOut += " <td>XT711</td>";
        m_szOut += " <td>Mode1001</td>";
        m_szOut += " <td>房间1</td>";
        m_szOut += "<td>实验室1</td>";
        m_szOut += "<td></td>";
        m_szOut += "<td>正常</td>";

        if (szState != null)
        {
            if (szState == "1")
            {
                Title = "设备关机状态";
                m_szOut += "<td><div class=\"OPTD OPTD1\"></div> </td>";
            }
            else if (szState == "2")
            {
                Title = "设备开机状态";
                m_szOut += "<td><div class=\"OPTD OPTD2\"></div> </td>";
            }
            else if (szState == "3")
            {
                Title = "免登录状态";
                m_szOut += "<td><div class=\"OPTD OPTD3\"></div> </td>";
            }
            else if (szState == "4")
            {
                Title = "故障报修中";
                m_szOut += "<td><div class=\"OPTD OPTD4\"></div> </td>";
            }
        }
        m_szOut += " </tr>";
        PutBackValue();
    }
}
