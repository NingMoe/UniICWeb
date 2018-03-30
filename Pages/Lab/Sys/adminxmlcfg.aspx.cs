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

public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected string m_szLab = "";
    protected string m_szRoom = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string szop = Request["op"];
        string szfielName=Request["fieldName"];
        string szValue = Request["delID"];
        if (szop == "del")
        {
            del(szfielName, szValue);
        }
        string szKind = Request["kind"];
        if (szKind == null ||szKind == "")
        {
            szKind = "ResvAbsTime";
        }
        ArrayList list = GetListByFieldName(szKind);
        for (int i = 0;list!=null&& i < list.Count; i++)
            {
                CStatueTemp temp = new CStatueTemp();
                temp = (CStatueTemp)list[i];
                m_szOut += "<tr>";
                m_szOut += "<td data-fieldName='" + szKind + "' data-id='" + temp.szValue.ToString() + "'>" + temp.szName.ToString() + "</td>";
                
            
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
           
        
        PutBackValue();
    }
    private void Del(string szID, string szLabID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDEVICE obj = new UNIDEVICE();
        obj.dwDevID = Parse(szID);
        obj.dwLabID = Parse(szLabID);
        uResponse = m_Request.Device.Del(obj);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
