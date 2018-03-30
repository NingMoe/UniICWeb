using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using UniWebLib;

/// <summary>
/// UniPage µÄ°ïÖúÀà
/// </summary>
/// 

namespace UniWebLib
{
    public class UniConfig
    {
        public string m_szServerIP{
            get
            {
                string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerIP"];
                if (string.IsNullOrEmpty(s))
                {
                    return "127.0.0.1";
                }
                else
                {
                    return s;
                }
            }
        }//"10.121.0.3";//114.242.96.140   192.168.11.17
        public int m_nServerPort{
            get
            {
                string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerPort"];
                if (string.IsNullOrEmpty(s))
                {
                    return 10403;
                }
                else
                {
                    return int.Parse(s);
                }
            }
        }//10403;

        static public string m_szVersion = "6.00.20121018";

        public bool m_bKeepLive = true;
        public int m_Timeout{
            get
            {
                string s = System.Web.Configuration.WebConfigurationManager.AppSettings["Timeout"];
                if (string.IsNullOrEmpty(s))
                {
                    return 30;
                }
                else
                {
                    return int.Parse(s);
                }
            }
        }// = 30;

        public void InitConfig()
        {
        }
    }
}

