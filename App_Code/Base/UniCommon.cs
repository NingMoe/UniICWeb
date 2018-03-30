using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// UniCommon 的摘要说明
/// </summary>
public class UniCommon
{
	public UniCommon()
	{
	}

    public bool IsLogon
    {
        get
        {
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                return false;
            }
            USER user = (USER)System.Web.HttpContext.Current.Session["user"];
            if (user.szLogonName == null)
            {
                return false;
            }
            return true;
        }
    }

    USER m_user = null;
    public USER LogonUser
    {
        get
        {
            if (m_user != null)
            {
                return m_user;
            }
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                return null;
            }
            m_user = (USER)System.Web.HttpContext.Current.Session["user"];
            return m_user;
        }
    }

    public bool IsSuperAdmin
    {
        get
        {
            if (!IsLogon) return false;
            return LogonUser.dwType == 1;
        }
    }

}
