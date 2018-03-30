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
using System.Collections;
using System.Globalization;
using System.Reflection;
using UniWebLib;
using UniLibrary;

/// <summary>
/// UniPage 的摘要说明
/// </summary>
/// 

namespace UniWebLib
{
    public partial class UniPage : System.Web.UI.Page
    {
        public int PageWidth{
            get
            {
                string s = System.Web.Configuration.WebConfigurationManager.AppSettings["PageWidth"];
                if (string.IsNullOrEmpty(s))
                {
                    return 960;
                }
                else
                {
                    return int.Parse(s);
                }
            }
        }//960;
    }
}

