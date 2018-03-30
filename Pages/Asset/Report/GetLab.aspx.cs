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

public partial class _Default : UniWebLib.UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
    protected string m_szLabKind = "";
    protected MyString m_szOut = new MyString();
    protected void Page_Load(object sender, EventArgs e)
    {
        LABREQ vrGetLab = new LABREQ();
        UNILAB[] vtLab;
        if (m_Request.Device.LabGet(vrGetLab, out vtLab) != REQUESTCODE.EXECUTE_SUCCESS)
        {
           // MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
        }
        else
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td ><a>" + vtLab[i].dwLabID.ToString() + "</a></td>";
                m_szOut += "<td ><a>" + vtLab[i].szLabName.ToString() + "</a></td>";
                m_szOut += "<td ><a>" + vtLab[i].szLabSN.ToString() + "</a></td>";
                m_szOut += "<td ><a>" +GetDateStr(vtLab[i].dwCreateDate) + "</a></td>";
                m_szOut += "<td ><a>" + vtLab[i].szMemo.ToString() + "</a></td>";
               
            }


        }
        PutBackValue();
    }
}
