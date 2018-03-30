using System;
using System.Data;
using System.Configuration;
using UniWebLib;
using System.Threading;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;

/// <summary>
/// SessionMng 的摘要说明
/// </summary>
/// 

public sealed class MyString
{
    public MyString(StringBuilder sb) { m_sb = sb; }
    public MyString() { m_sb = new StringBuilder(); }
    public StringBuilder m_sb;

    public static implicit operator MyString(StringBuilder sb)
    {
        return new MyString(sb);
    }
    public char this[int index] {
        get{return m_sb[index];}
        set{m_sb[index] = value;}
    }
    public static MyString operator +(MyString sbw, string s)
    {
        sbw.m_sb.Append(s);
        return sbw;
    }

    public override string ToString()
    {
        return m_sb.ToString(); 
    }
}

namespace UniLibrary
{
    public class SessionMng
    {
        UniConfig m_Config = new UniConfig();

        Hashtable m_htRequest = null;
        Hashtable HTRequest
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                {
                    return m_htRequest;
                }
                if (System.Web.HttpContext.Current.Application == null)
                {
                    return m_htRequest;
                }
                Hashtable htRequest = (Hashtable)System.Web.HttpContext.Current.Application["UniRequest"];
                if (htRequest == null)
                {
                    htRequest = m_htRequest;
                    System.Web.HttpContext.Current.Application["UniRequest"] = htRequest;
                }
                return htRequest;
            }

            set
            {
                if ((System.Web.HttpContext.Current == null) ||(System.Web.HttpContext.Current.Application == null))
                {
                    m_htRequest = value;
                    return;
                }
                System.Web.HttpContext.Current.Application["UniRequest"] = value;
            }
        }

        Thread m_TimeoutThread = null;
        Thread TimeoutThread
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                {
                    return m_TimeoutThread;
                }
                if (System.Web.HttpContext.Current.Application == null)
                {
                    return m_TimeoutThread;
                }
                Thread timeoutThread = (Thread)System.Web.HttpContext.Current.Application["TimeoutThread"];
                if (timeoutThread == null)
                {
                    timeoutThread = m_TimeoutThread;
                    System.Web.HttpContext.Current.Application["TimeoutThread"] = timeoutThread;
                }
                return timeoutThread;
            }

            set
            {
                if ((System.Web.HttpContext.Current == null) || (System.Web.HttpContext.Current.Application == null))
                {
                    m_TimeoutThread = value;
                    return;
                }
                System.Web.HttpContext.Current.Application["TimeoutThread"] = value;
            }
        }

        class UniRequestApp
        {
            public UniRequest Request;
            public DateTime nTime;
        }

        public SessionMng()
        {
            m_Config.InitConfig();

            StartTimeoutThread();
        }

        ~SessionMng()
        {
            StopTimeoutThread();
            ClearRequest();
        }


        public UniRequest GetRequest(uint SessionID, uint StationSN)
        {
            UniRequest pRequest = null;
            if (SessionID == 0)
            {
                if (System.Web.HttpContext.Current.Session != null)
                {
                    if (System.Web.HttpContext.Current.Session["RequestSession"] != null)
                    {
                        pRequest = (UniRequest)System.Web.HttpContext.Current.Session["RequestSession"];
                    }
                    else
                    {
                        pRequest = new UniRequest();
                        System.Web.HttpContext.Current.Session["RequestSession"] = pRequest;
                    }
                    pRequest.m_UniDCom.StaSN = StationSN;
                    return pRequest;
                }
            }

            Hashtable htRequest = HTRequest;

            if (htRequest == null)
            {
                htRequest = new Hashtable();
                HTRequest = htRequest;
            }
            UniRequestApp papp = (UniRequestApp)htRequest[SessionID];
            if (papp == null)
            {
                papp = new UniRequestApp();
                htRequest[SessionID] = papp;
            }
            pRequest = papp.Request;
            if (pRequest == null)
            {
                pRequest = new UniRequest();
                papp.Request = pRequest;
            }
            pRequest.m_UniDCom.SessionID = SessionID;
            pRequest.m_UniDCom.StaSN = StationSN;
            papp.nTime = DateTime.Now;

            //ClearTimeoutRequest();
            return pRequest;
        }
        
        public void ClearTimeoutRequest()
        {
            Hashtable htRequest = HTRequest;
            if (htRequest == null)
            {
                return;
            }
            ArrayList Kyes = new ArrayList(htRequest.Keys);
            foreach (uint key in Kyes)
            {
                UniRequestApp papp = (UniRequestApp)htRequest[key];
                if ((object)papp == null || papp.Request == null)
                {
                    htRequest.Remove(key);
                    continue;
                }
                if ((DateTime.Now - papp.nTime).TotalSeconds > m_Config.m_Timeout * 60)//
                {
                    Logger.Trace("ClearTimeoutRequest Session=" + papp.Request.m_UniDCom.SessionID);
                    papp.Request.m_UniDCom.Close();
                    htRequest.Remove(key);
                }
            }
        }

        public void ReleaseRequest(uint SessionID)
        {
            Hashtable htRequest = HTRequest;
            if (htRequest == null)
            {
                return;
            }
            UniRequestApp papp = (UniRequestApp)htRequest[SessionID];
            if ((object)papp == null)
            {
                return;
            }
            if (papp.Request != null)
            {
                papp.Request.m_UniDCom.Close();
            }
            htRequest.Remove(SessionID);
        }

        void ClearRequest()
        {
            Hashtable htRequest = HTRequest;
            if (htRequest == null)
            {
                return;
            }
            ArrayList Kyes = new ArrayList(htRequest.Keys);
            foreach (uint key in Kyes)
            {
                UniRequestApp papp = (UniRequestApp)htRequest[key];
                if ((object)papp == null || papp.Request == null)
                {
                    htRequest.Remove(key);
                    continue;
                }
                Logger.Trace("ClearRequest Session=" + papp.Request.m_UniDCom.SessionID);
                papp.Request.m_UniDCom.Close();
                htRequest.Remove(key);
            }
        }
        
        void StartTimeoutThread()
        {
            if (TimeoutThread == null)
            {
                Logger.Trace("StartTimeoutThread");
                TimeoutThread = new Thread(new ThreadStart(ThreadTimeout));
                TimeoutThread.Start();
            }
        }

        void StopTimeoutThread()
        {
            if (TimeoutThread != null)
            {
                if (TimeoutThread.IsAlive)
                {
                    try
                    {
                        TimeoutThread.Abort();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        
        void ThreadTimeout()
        {
            int n = 1;
            while (true)
            {
                Thread.Sleep(1000);
                n++;
                if (n >= 10)
                {
                    n = 0;
                    ClearTimeoutRequest();
                }
            }
        }

    }

}

