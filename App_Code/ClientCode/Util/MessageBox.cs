using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Util
{
    public class MessageBox
    {
        public static void Show(Page page, string p)
        {
            string msg = Regex.Replace(p, "[\"|\']+", "’");
             ScriptManager.RegisterStartupScript(page,page.GetType(), "msg", "window.onload=function(){if(uni){uni.msgBox(\"" + msg + "\")}else{alert(\"" + msg + "\")}}", true);
        }
    }
    public static class GROUPENUM
    {
        public static int introduce = 1;
        public static int news = 2;
        public static int rule = 3;
        public static int resource = 4;
    }
}