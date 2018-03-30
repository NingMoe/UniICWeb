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
    public partial class UniUserControl : UniControl
    {
        UniRequest m_pRequest = null;

        public UniUserControl()
        {
        }
        ~UniUserControl()
        {
        }

        SessionMng sessionMng = new SessionMng();
        public UniRequest GetRequest()
        {
            if (m_pRequest != null)
                return m_pRequest;

            uint nSessionID = 0;
            uint nStationSN = 0;
            if (Session["SessionID"] != null)
            {
                nSessionID = (uint)Session["SessionID"];
            }
            if (Session["StationSN"] != null)
            {
                nStationSN = (uint)Session["StationSN"];
            }
            m_pRequest = sessionMng.GetRequest(nSessionID, nStationSN);
            m_pRequest.OnError = OnError;
            return m_pRequest;
        }
        public void ReleaseRequest(uint SessionID)
        {
            sessionMng.ReleaseRequest(SessionID);
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }

	    public void OnError(PRModule module,REQUESTCODE ret)
        {
            if (ret == REQUESTCODE.ERR_IMPORT)
            {
                Logger.Trace("ERR_IMPORT Reconnect");
                m_Request.m_UniDCom.Close();
            }
            else if (ret == REQUESTCODE.EXECUTE_FAIL)
            {
            }
            else if ((uint)ret == 0x02002001)
            {
                //MessageBox(m_Request.szErrMessage, "系统信息", MSGBOX.WARN, "~/Sys/Login.aspx");
                //登录超时
                //Response.Redirect("~/Sys/Login.aspx");
            }
        }

        public void GetHTTPObj<T>(out T obj) where T : new()
        {
            obj = (T)UniLibrary.ObjHelper.HTTP2OBJ(this, typeof(T));
        }

        public void PutHTTPObj<T>(T obj) where T : new()
        {
            UniLibrary.ObjHelper.OBJ2HTTP(this,obj);
        }

        public object GetMemberVlaue(object obj, string szName)
        {
            return UniLibrary.ObjHelper.GetMemberVlaue(obj, szName);
        }

        public void SetMemberVlaue(object obj, string szName, object val)
        {
            UniLibrary.ObjHelper.SetMemberVlaue(obj, szName, val);
        }

        public object GetReserved(object obj, string szName)
        {
            Reserved res = (Reserved)GetMemberVlaue(obj, "reserved");
            if (res.Ext == null)
            {
                return null;
            }
            return res.Ext[szName];
        }
        public void SetReserved(object obj, string szName, object val)
        {
            Reserved res = (Reserved)GetMemberVlaue(obj, "reserved");
            if (res.Ext == null)
            {
                res.Ext = new System.Collections.Generic.Dictionary<string, object>();
            }
            res.Ext.Add(szName, val);
            UniLibrary.ObjHelper.SetMemberVlaue(obj, "reserved", res);
        }

        public uint ToUint(string szValue)
        {
            uint nValue = 0;
            uint.TryParse(szValue, out nValue);
            return nValue;
        }
        public uint ToUint(uint? szValue)
        {
            if (szValue == null)
                return 0;
            else
                return (uint)szValue;
        }

        public uint GetDate(string szDate)
        {
            uint dwret = 0;
            if (string.IsNullOrEmpty(szDate))
            {
                return 0;
            }
            DateTime dtRet;
            DateTime.TryParse(szDate, out dtRet);
            dwret = (uint)(dtRet.Year * 10000 + dtRet.Month * 100 + dtRet.Day);
            return dwret;
        }
        public string GetDateStr(uint? nDate)
        {
            if (nDate == null || nDate == 0)
            {
                return "";
            }
            string szDate = (nDate / 10000).ToString() + "-" + string.Format("{0:D2}", ((nDate / 100) % 100)) + "-" + string.Format("{0:D2}", (nDate % 100));
            return szDate;
        }

        public uint GetTime(string szTime)
        {
            DateTime dt;
            DateTime.TryParse(szTime, out dt);
            return (uint)(dt.Hour * 100 + dt.Minute);
        }
        public string GetTimeStr(uint? nTime)
        {
            if (nTime == null) nTime = 0;
            string szTime = string.Format("{0:D2}", nTime / 100) + ":" + string.Format("{0:D2}", nTime % 100);
            return szTime;
        }

        public string GetOSVer(uint? _dwOSVer)
        {
            uint dwOSVer = (uint)_dwOSVer;
            uint dwBit = (uint)dwOSVer % 100;
            uint dwOS = dwOSVer / 10000;
            string szOS = "";
            if (dwOS == 500)
            {
                szOS = "Windows 2000";
            }
            else if (dwOS == 501)
            {
                szOS = "Windows XP";
            }
            else if (dwOS == 502)
            {
                szOS = "Windows Server 2003";
            }
            else if (dwOS == 600)
            {
                szOS = "Windows Vista";
            }
            else if (dwOS == 601)
            {
                szOS = "Windows 7";
            }
            else if (dwOS == 602)
            {
                szOS = "Windows 8";
            }
            return szOS + " " + dwBit.ToString()+"位";
        }
        public uint Get1970Seconds(string Date)//返回和1970的差距秒数
        {
            uint result = 0;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            try
            {
                DateTime dtDate = DateTime.Parse(Date);
                TimeSpan spDate = dtDate.Subtract(dt1970);
                result = (uint)spDate.TotalSeconds;
            }
            catch
            {
                return 0;
            }
            return result;
        }
        public string Get1970Date(uint? TotalSeconds)//根据差距秒数 算出现在是日期
        {
            if (TotalSeconds == null)
            {
                return "";
            }
            uint TotalSecondsIn = (uint)TotalSeconds;
            string result = string.Empty;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            DateTime dtNow = dt1970.AddSeconds(TotalSecondsIn);
            return result = dtNow.ToString("yyyy-MM-dd HH:mm");
        }
    }
}

