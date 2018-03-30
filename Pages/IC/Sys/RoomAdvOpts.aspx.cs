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
    protected string m_szLab = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(60);

        UNILAB[] lab = GetAllLab();
        if (lab != null)
        {
            for (int i = 0; i < lab.Length; i++)
            {
                m_szLab += "<label><input name='lab' value='" + lab[i].dwLabID + "' type='radio' />" + lab[i].szLabName + "</label>  ";
            }
        }
    }
}
