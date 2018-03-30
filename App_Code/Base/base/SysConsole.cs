using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Data.SQLite;
using System.Collections.Generic;

public struct SYSLOG
{
    public uint dwID;
    public uint dwDate;
    public uint dwTime;
    public uint dwCmd;
    public uint dwRetCode;
    public uint dwUseTime;
    public uint dwParamSize;
    public uint dwResultSize;
    public byte[] param;
    public string szMessage;
    public string szSessionID;
    public string szStationSN;    
};

public struct SYSLOG_CMDSTAT
{
    public uint dwCmdModule;
    public uint dwOKCount;
    public uint dwErrorCount;
    public long dwTotalUseTime;
    public long dwParamTotalSize;
    public long dwResultTotalSize;
    public uint dwStationCount;
};
public struct SYSLOG_STATIONSTAT
{
    public string szStationSN; 
    public uint dwOKCount;
    public uint dwErrorCount;
    public long dwTotalUseTime;
    public long dwParamTotalSize;
    public long dwResultTotalSize;
};

public struct SYSLOG_URLSTAT
{
    public string szURL;
    public uint dwIndex;
    public uint dwCount;
    public long dwAvgUseTime;
    public string szMemo;
};

public struct ParamExt
{
    public uint nStartPage;
    public uint nPageCount;
    public uint nTotalCount;
    public string szOrder;
    public string szOrderField;
}

public struct SessionData
{
    public string szASPSessionID;
    public string szSessionID;
    public string szStationSN;
    public string szUserName;
    public long tick;
    public string ip;
}

public class CUseTime
{
    public List<DateTime> m_UseTime = new List<DateTime>();
    public List<string> m_UseTimeMemo = new List<string>();
    public CUseTime()
    {
        Add();
    }

    public void Add()
    {
        m_UseTime.Add(DateTime.Now);
        m_UseTimeMemo.Add(m_UseTime.Count.ToString());
    }
    public void Add(string szMemo)
    {
        m_UseTime.Add(DateTime.Now);
        m_UseTimeMemo.Add(szMemo);
    }
    public uint GetCount()
    {
        return (uint)m_UseTime.Count;
    }
    public uint GetUseTime(string szMemo,out uint nUseTimeBegin)
    {
        string szMemo_Start = szMemo + "_Start";

        int index_Start = -1;
        int index = -1;
        for (int i = 0; i < m_UseTimeMemo.Count; i++)
        {
            if (m_UseTimeMemo[i] == szMemo_Start)
            {
                index_Start = i;
            }
            if (m_UseTimeMemo[i] == szMemo)
            {
                index = i;
            }
            if ((index_Start != -1) && (index != -1))
            {
                break;
            }
        }
        if (index_Start == -1)
        {
            nUseTimeBegin = 0;
        }
        else
        {
            nUseTimeBegin = (uint)(m_UseTime[index_Start] - m_UseTime[0]).Milliseconds;
        }
        if (index == -1) return 0;
        return (uint)(m_UseTime[index] - m_UseTime[0]).Milliseconds;
    }
    public uint GetUseTime(int index, out string szMemo)
    {
        szMemo = "1";
        if (index == 0) return 0;
        if(index >= m_UseTime.Count)
        {
            return 0;
        }
        szMemo = m_UseTimeMemo[index];
        return (uint)(m_UseTime[index] - m_UseTime[0]).Milliseconds;
    }
    public uint GetTotalUseTime()
    {
        return (uint)(m_UseTime[m_UseTime.Count - 1] - m_UseTime[0]).Milliseconds;
    }
    public uint GetLastUseTime()
    {
        if (m_UseTime.Count <= 1) return 0;
        return (uint)(m_UseTime[m_UseTime.Count - 1] - m_UseTime[m_UseTime.Count - 2]).Milliseconds;
    }
    public string GetAllUseTime()
    {
        MyString szRet = new MyString();
        DateTime s = m_UseTime[0];
        for (int i = 0; i < m_UseTime.Count; i++)
        {
            int usetime = (m_UseTime[i] - s).Milliseconds;
            szRet += m_UseTimeMemo[i] + "=" + usetime + ",";
        }
        return szRet.ToString();
    }
}

public class SysConsole
{
    static SQLiteConnection m_conn = null;
    static DateTime m_lastconn = DateTime.Now;
    static object m_lockobj = new object();
    static Hashtable m_SessionList = new Hashtable();

    static SQLiteConnection GetConn()
    {
        if (m_conn == null)
        {
            try
            {
                string szFSrc = System.Web.HttpContext.Current.Server.MapPath("~/Bin/x86/SQLite.Interop.dll");
                string szFDest = System.Web.HttpContext.Current.Server.MapPath("~/Bin/SQLite.Interop.dll");
                if (IntPtr.Size == 8)
                {
                    szFSrc = System.Web.HttpContext.Current.Server.MapPath("~/Bin/x64/SQLite.Interop.dll");
                }
                FileInfo fisrc = new FileInfo(szFSrc);
                FileInfo fidest = new FileInfo(szFDest);
                if (fisrc.LastWriteTime != fidest.LastWriteTime || fisrc.Length != fidest.Length)
                {
                    File.Copy(szFSrc, szFDest, true);
                }
            }
            catch (Exception e)
            {
                Logger.Trace(e.Message);
            }

            string szLogDBFile = System.Web.HttpContext.Current.Server.MapPath("~/SysLog.db");
            if (!File.Exists(szLogDBFile))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(szLogDBFile);
            }
            try
            {
                System.Data.SQLite.SQLiteConnectionStringBuilder connStrBuilder = new SQLiteConnectionStringBuilder("Data Source=" + szLogDBFile);
                //-----------------速度快----
                //connStrBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
                //-----------------
                //connStrBuilder.JournalMode = SQLiteJournalModeEnum.Off;
                //connStrBuilder.SyncMode = SynchronizationModes.Off;
                //-----------------

                m_conn = new SQLiteConnection(connStrBuilder.ConnectionString);
                m_conn.Open();

                VerifyTable();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("试图加载格式不正确的程序。 (异常来自 HRESULT:0x8007000B)") >= 0)
                {
                    string os_platform = System.Environment.OSVersion.Platform.ToString();

                    try
                    {
                        if (os_platform.IndexOf("Win32NT") >= 0)
                        {
                            File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Bin/x86/SQLite.Interop.dll"), System.Web.HttpContext.Current.Server.MapPath("~/Bin/SQLite.Interop.dll"), true);
                        }
                        else if (IntPtr.Size == 8)
                        {
                            File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Bin/x64/SQLite.Interop.dll"), System.Web.HttpContext.Current.Server.MapPath("~/Bin/SQLite.Interop.dll"), true);
                        }
                    }
                    catch (Exception ee)
                    {
                        Logger.Trace(ee.Message);
                    }
                }
                Logger.Trace(e.Message);
            }
            m_lastconn = DateTime.Now;
        }
        else
        {
            if ((((TimeSpan)(DateTime.Now - m_lastconn)).Seconds > 60) || (m_conn.Changes > 0))
            {
                m_lastconn = DateTime.Now;
                m_conn.Close();
                m_conn.Open();
            }
        }
        return m_conn;
    }
    static public void ReleaseConn()
    {
        lock (m_lockobj)
        {
            if (m_conn != null)
            {
                m_conn.Close();
            }
            m_conn = null;
        }
    }

    static public void VerifyTable()
    {
        try
        {
            SQLiteConnection conn = GetConn();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='log';";
            if (0 == Convert.ToInt32(cmd.ExecuteScalar()))
            {
                cmd.CommandText = "create table log(szSessionID TEXT,szStationSN TEXT,dwDate INTEGER, dwTime INTEGER,dwCmd INTEGER,dwRetCode INTEGER,dwUseTime INTEGER, dwParamSize INTEGER,dwResultSize INTEGER,param TEXT, szMessage TEXT);";
                cmd.ExecuteNonQuery();
            }
            cmd.Dispose();

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='pagestat';";
            if (0 == Convert.ToInt32(cmd.ExecuteScalar()))
            {
                cmd.CommandText = "create table pagestat(szSessionID TEXT,szStationSN TEXT,szURL TEXT,dwDate INTEGER, dwTime INTEGER,dwIndex INTEGER,dwUseTime INTEGER,szMemo TEXT);";
                cmd.ExecuteNonQuery();
            }
            cmd.Dispose();
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static string GetStationSN(string szSessionID)
    {
        foreach(DictionaryEntry de in m_SessionList)
        {
            SessionData sd = (SessionData)de.Value;
            if (sd.szSessionID == szSessionID)
            {
                sd.tick = DateTime.Now.Ticks;
                m_SessionList[de.Key] = sd;
                return (string)de.Key;
            }
        }
        return "";
    }
    static public void SetSessionStationSN(string szStationSN, string szSessionID, string szASPSessionID,string szUserName,string szIP)
    {
        lock (m_lockobj)
        {
            SessionData sd = new SessionData();
            sd.szASPSessionID = szASPSessionID;
            sd.szSessionID = szSessionID;
            sd.szStationSN = szStationSN;
            sd.szUserName = szUserName;
            sd.ip = szIP;
            sd.tick = DateTime.Now.Ticks;
            m_SessionList[szStationSN] = sd;
        }
    }
    static public void ReleaseSession( string szSessionID)
    {
        lock (m_lockobj)
        {
            foreach (DictionaryEntry de in m_SessionList)
            {
                SessionData sd = (SessionData)de.Value;
                if (sd.szSessionID == szSessionID)
                {
                    sd.szSessionID = "";
                    m_SessionList[de.Key] = sd;
                    break;
                }
            }
        }
    }

    static public SessionData[] GetSessionList()
    {
        lock (m_lockobj)
        {
            SessionData[] ret = new SessionData[m_SessionList.Values.Count];
            m_SessionList.Values.CopyTo(ret, 0);
            return ret;
        }
    }


    static public void LogPageStatLast(string szSessionID, string szURL, CUseTime useTime)
    {
		string cfg_s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysLog"];
		if (string.IsNullOrEmpty(cfg_s) || cfg_s.ToLower() != "true")
		{
			return;
		}
        try
        {
            lock (m_lockobj)
            {
                int dwDate = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int dwTime = DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;

                string szStationSN = GetStationSN(szSessionID);

                SQLiteConnection conn = GetConn();

                DateTime s = useTime.m_UseTime[0];
                int i = useTime.m_UseTime.Count - 1;
                {
                    int usetime = (useTime.m_UseTime[i] - s).Milliseconds;

                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into pagestat values(@szSessionID,@szStationSN,@szURL,@dwDate,@dwTime,@dwIndex,@dwUseTime,@szMemo);";
                    cmd.Parameters.Add(new SQLiteParameter("szSessionID", szSessionID));
                    cmd.Parameters.Add(new SQLiteParameter("szStationSN", szStationSN));
                    cmd.Parameters.Add(new SQLiteParameter("szURL", szURL));
                    cmd.Parameters.Add(new SQLiteParameter("dwDate", dwDate));
                    cmd.Parameters.Add(new SQLiteParameter("dwTime", dwTime));
                    cmd.Parameters.Add(new SQLiteParameter("dwIndex", i));
                    cmd.Parameters.Add(new SQLiteParameter("dwUseTime", usetime));
                    cmd.Parameters.Add(new SQLiteParameter("szMemo", useTime.m_UseTimeMemo[i]));
                    int r = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static public void LogPageStat(string szSessionID, string szURL, CUseTime useTime)
    {
		string cfg_s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysLog"];
		if (string.IsNullOrEmpty(cfg_s) || cfg_s.ToLower() != "true")
		{
			return;
		}
		
        try
        {
            lock (m_lockobj)
            {
                int dwDate = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int dwTime = DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;

                string szStationSN = GetStationSN(szSessionID);

                SQLiteConnection conn = GetConn();


                DateTime s = useTime.m_UseTime[0];
                for (int i = 0; i < useTime.m_UseTime.Count; i++)
                {
                    int usetime = (useTime.m_UseTime[i] - s).Milliseconds;

                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into pagestat values(@szSessionID,@szStationSN,@szURL,@dwDate,@dwTime,@dwIndex,@dwUseTime,@szMemo);";
                    cmd.Parameters.Add(new SQLiteParameter("szSessionID", szSessionID));
                    cmd.Parameters.Add(new SQLiteParameter("szStationSN", szStationSN));
                    cmd.Parameters.Add(new SQLiteParameter("szURL", szURL));
                    cmd.Parameters.Add(new SQLiteParameter("dwDate", dwDate));
                    cmd.Parameters.Add(new SQLiteParameter("dwTime", dwTime));
                    cmd.Parameters.Add(new SQLiteParameter("dwIndex", i));
                    cmd.Parameters.Add(new SQLiteParameter("dwUseTime", usetime));
                    cmd.Parameters.Add(new SQLiteParameter("szMemo", useTime.m_UseTimeMemo[i]));
                    int r = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static public void Log(string szSessionID,uint dwRetCode,uint dwUseTime, uint nCmd, byte[] param, byte[] result, string szMessage)
    {
        string cfg_s = "false";
        try
        {
            cfg_s=System.Web.Configuration.WebConfigurationManager.AppSettings["SysLog"];
        }
        catch { 
        }
		if (string.IsNullOrEmpty(cfg_s) || cfg_s.ToLower() != "true")
		{
			return;
		}

        try
        {
            lock (m_lockobj)
            {
                int dwDate = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int dwTime = DateTime.Now.Hour  * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;

                uint dwParamSize = 0;
                if (param != null) dwParamSize = (uint)param.Length;
                uint dwResultSize = 0;
                if (result != null) dwResultSize = (uint)result.Length;
                string szParam = "";

                string szStationSN = GetStationSN(szSessionID);


                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into log values(@szSessionID,@szStationSN,@dwDate,@dwTime,@dwCmd,@dwRetCode,@dwUseTime,@dwParamSize,@dwResultSize,@param,@szMessage);";
                cmd.Parameters.Add(new SQLiteParameter("szSessionID", szSessionID));
                cmd.Parameters.Add(new SQLiteParameter("szStationSN", szStationSN));
                cmd.Parameters.Add(new SQLiteParameter("dwDate", dwDate));
                cmd.Parameters.Add(new SQLiteParameter("dwTime", dwTime));
                cmd.Parameters.Add(new SQLiteParameter("dwCmd", nCmd));
                cmd.Parameters.Add(new SQLiteParameter("dwRetCode", dwRetCode));
                cmd.Parameters.Add(new SQLiteParameter("dwUseTime", dwUseTime));
                cmd.Parameters.Add(new SQLiteParameter("dwParamSize", dwParamSize));
                cmd.Parameters.Add(new SQLiteParameter("dwResultSize", dwResultSize));
                cmd.Parameters.Add(new SQLiteParameter("param", szParam));
                cmd.Parameters.Add(new SQLiteParameter("szMessage", szMessage));
                int r = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static public void LogError(string szSessionID, uint nCmd, string szMessage)
    {
        string cfg_s = "false";
        try
        {
            cfg_s=System.Web.Configuration.WebConfigurationManager.AppSettings["SysLog"];
        }
        catch { 
        }
		if (string.IsNullOrEmpty(cfg_s) || cfg_s.ToLower() != "true")
		{
			return;
		}

        try
        {
            lock (m_lockobj)
            {
                int dwDate = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int dwTime = DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;

                string szStationSN = GetStationSN(szSessionID);

                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into log values(@szSessionID,@szStationSN,@dwDate,@dwTime,@dwCmd,@dwUseTime,@dwRetCode,@dwParamSize,@dwResultSize,@param,@szMessage);";
                cmd.Parameters.Add(new SQLiteParameter("szSessionID", szSessionID));
                cmd.Parameters.Add(new SQLiteParameter("szStationSN", szStationSN));
                cmd.Parameters.Add(new SQLiteParameter("dwDate", dwDate));
                cmd.Parameters.Add(new SQLiteParameter("dwTime", dwTime));
                cmd.Parameters.Add(new SQLiteParameter("dwCmd", nCmd));
                cmd.Parameters.Add(new SQLiteParameter("dwUseTime", 0));
                cmd.Parameters.Add(new SQLiteParameter("dwRetCode", -1));
                cmd.Parameters.Add(new SQLiteParameter("dwParamSize", 0));
                cmd.Parameters.Add(new SQLiteParameter("dwResultSize", 0));
                cmd.Parameters.Add(new SQLiteParameter("param", ""));
                cmd.Parameters.Add(new SQLiteParameter("szMessage", szMessage));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static string GetSZ(DataRow row,string szName)
    {
        if (row == null)
        {
            return null;
        }
        object v = row[szName];
        if (v == null)
        {
            return null;
        }
        return v.ToString();
    }
    static uint GetDW(DataRow row, string szName)
    {
        if (row == null)
        {
            return 0;
        }
        object v = row[szName];
        if (v == null)
        {
            return 0;
        }
        Type t = v.GetType();
        if (t == typeof(uint))
        {
            return (uint)v;
        }
        else if (t == typeof(int))
        {
            return (uint)(int)v;
        }
        else if (t == typeof(long))
        {
            return (uint)(long)v;
        }
        return 0;
    }

    static long GetLONG(DataRow row, string szName)
    {
        if (row == null)
        {
            return 0;
        }
        object v = row[szName];
        if (v == null)
        {
            return 0;
        }
        Type t = v.GetType();
        if (t == typeof(uint))
        {
            return (long)(uint)v;
        }
        else if (t == typeof(int))
        {
            return (long)(int)v;
        }
        else if (t == typeof(long))
        {
            return (long)v;
        }
        else if (t == typeof(float))
        {
            return (long)(float)v;
        }
        else if (t == typeof(double))
        {
            return (long)(double)v;
        }
        return 0;
    }

    static public SYSLOG[] GetLog(uint dwDateMin, uint dwDateMax, uint dwRetCode,uint dwCmd, ref ParamExt ext)
    {
        SYSLOG[] ret = new SYSLOG[0];
        ext.nTotalCount = 0;

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                string szSubCMD = "";
                if (dwCmd != 0)
                {
                    //4294901760 === 0xFFFF0000
                    szSubCMD = " (dwCmd & 4294901760) = " + dwCmd + " and ";
                }

                //GetTotalCount
                if (dwRetCode == 0)
                {
                    cmd.CommandText = "select count(*) from log where " + szSubCMD + " dwDate >=@dwDateMin and dwDate <=@dwDateMax;";
                }
                else
                {
                    cmd.CommandText = "select count(*) from log where " + szSubCMD + " dwRetCode != 0 and dwDate >=@dwDateMin and dwDate <=@dwDateMax;";
                }
                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ext.nTotalCount = (uint)(long)datatbl.Rows[0][0];
                }
                //=============
                if (ext.nStartPage < 0) ext.nStartPage = 0;
                if (ext.nStartPage > ext.nTotalCount) ext.nStartPage = ext.nTotalCount;
                if (ext.nPageCount == 0) ext.nPageCount = ext.nTotalCount;
                if (string.IsNullOrEmpty(ext.szOrderField)) ext.szOrderField = "dwDate";
                if (string.IsNullOrEmpty(ext.szOrder)) ext.szOrder = "asc";

                if (dwRetCode == 0)
                {
                    cmd.CommandText = "select rowid,* from log where " + szSubCMD + " dwDate >=@dwDateMin and dwDate <=@dwDateMax order by " + ext.szOrderField + " " + ext.szOrder + ",rowid " + ext.szOrder + " limit " + ext.nStartPage + "," + ext.nPageCount + ";";
                }
                else
                {
                    cmd.CommandText = "select rowid,* from log where " + szSubCMD + " dwRetCode != 0 and dwDate >=@dwDateMin and dwDate <=@dwDateMax order by " + ext.szOrderField + " " + ext.szOrder + ",rowid " + ext.szOrder + "  limit " + ext.nStartPage + "," + ext.nPageCount + ";";
                }
                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new SYSLOG[datatbl.Rows.Count];
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = new SYSLOG();
                        ret[i].dwID = GetDW(datatbl.Rows[i], "rowid");
                        ret[i].dwDate = GetDW(datatbl.Rows[i], "dwDate");
                        ret[i].dwTime = GetDW(datatbl.Rows[i], "dwTime");
                        ret[i].dwCmd = GetDW(datatbl.Rows[i],"dwCmd");
                        ret[i].dwRetCode = GetDW(datatbl.Rows[i], "dwRetCode");
                        ret[i].dwUseTime = GetDW(datatbl.Rows[i], "dwUseTime");
                        ret[i].dwParamSize = GetDW(datatbl.Rows[i], "dwParamSize");
                        ret[i].dwResultSize = GetDW(datatbl.Rows[i], "dwResultSize");
                        //ret[i].param = GetDW(datatbl.Rows[i],"param");
                        ret[i].szMessage = GetSZ(datatbl.Rows[i], "szMessage");
                        ret[i].szSessionID = GetSZ(datatbl.Rows[i], "szSessionID");
                        ret[i].szStationSN = GetSZ(datatbl.Rows[i], "szStationSN");
                    }
                }
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }

    static public SYSLOG_STATIONSTAT[] GetLogStationStat(uint dwDateMin, uint dwDateMax)
    {
        SYSLOG_STATIONSTAT[] ret = new SYSLOG_STATIONSTAT[0];

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select szStationSN , count(*) as dwTotal,count(nullif(dwRetCode,0)) as dwErrorCount,sum(dwUseTime) as dwTotalUseTime, sum(dwParamSize) as dwParamTotalSize , sum(dwResultSize) as dwResultTotalSize from log where dwDate >=@dwDateMin and dwDate <=@dwDateMax group by szStationSN order by dwTotal desc,dwParamTotalSize desc,dwResultTotalSize desc;";

                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new SYSLOG_STATIONSTAT[datatbl.Rows.Count];
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = new SYSLOG_STATIONSTAT();
                        ret[i].szStationSN = GetSZ(datatbl.Rows[i], "szStationSN");
                        ret[i].dwOKCount = GetDW(datatbl.Rows[i], "dwTotal");
                        ret[i].dwErrorCount = GetDW(datatbl.Rows[i], "dwErrorCount");
                        ret[i].dwOKCount -= ret[i].dwErrorCount;
                        ret[i].dwTotalUseTime = GetLONG(datatbl.Rows[i], "dwTotalUseTime");
                        ret[i].dwParamTotalSize = GetLONG(datatbl.Rows[i], "dwParamTotalSize");
                        ret[i].dwResultTotalSize = GetLONG(datatbl.Rows[i], "dwResultTotalSize");
                    }
                }
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }

    static public SYSLOG_CMDSTAT[] GetLogCmdStat(uint dwDateMin, uint dwDateMax, uint ByModule)
    {
        SYSLOG_CMDSTAT[] ret = new SYSLOG_CMDSTAT[0];

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                string szSQL = "select ";
                if (ByModule == 1)
                {
                    szSQL += "(dwCmd & 4294901760)";
                }
                else
                {
                    szSQL += "dwCmd";
                }
                szSQL += " as dwCmdModule , count(*) as dwTotal,count(nullif(dwRetCode,0)) as dwErrorCount, sum(dwUseTime) as dwTotalUseTime,sum(dwParamSize) as dwParamTotalSize , sum(dwResultSize) as dwResultTotalSize, count(DISTINCT  szStationSN) as dwStationCount from log where dwDate >=@dwDateMin and dwDate <=@dwDateMax group by dwCmdModule order by dwTotal desc,dwParamTotalSize desc,dwResultTotalSize desc;";

                cmd.CommandText = szSQL;
                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new SYSLOG_CMDSTAT[datatbl.Rows.Count];
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = new SYSLOG_CMDSTAT();
                        ret[i].dwCmdModule = GetDW(datatbl.Rows[i], "dwCmdModule");
                        ret[i].dwOKCount = GetDW(datatbl.Rows[i], "dwTotal");
                        ret[i].dwErrorCount = GetDW(datatbl.Rows[i], "dwErrorCount");
                        ret[i].dwOKCount -= ret[i].dwErrorCount;
                        ret[i].dwTotalUseTime = GetLONG(datatbl.Rows[i], "dwTotalUseTime");
                        ret[i].dwParamTotalSize = GetLONG(datatbl.Rows[i], "dwParamTotalSize");
                        ret[i].dwResultTotalSize = GetLONG(datatbl.Rows[i], "dwResultTotalSize");
                        ret[i].dwStationCount = GetDW(datatbl.Rows[i], "dwStationCount");
                    }
                }
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }

    static public SYSLOG_URLSTAT[] GetLogURLStat(uint dwDateMin, uint dwDateMax, uint rs)
    {
        SYSLOG_URLSTAT[] ret = new SYSLOG_URLSTAT[0];

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                string szSQL = "";
                if (rs == 1)
                {
                    szSQL = "select lower(szURL) as URL ,max(dwIndex) as dwIndex, count(*) as dwCount, avg(dwUseTime) as dwAvgUseTime ,'' as szMemo from pagestat where szMemo ='OnLoad' and dwDate >=@dwDateMin and dwDate <=@dwDateMax group by URL order by URL desc;";
                }
                else
                {
                    szSQL = "select lower(szURL) as URL , dwIndex,count(*) as dwCount, avg(dwUseTime) as dwAvgUseTime,szMemo  from pagestat where dwIndex!=0 and dwDate >=@dwDateMin and dwDate <=@dwDateMax group by URL,dwIndex,szMemo order by URL desc,dwIndex asc;";
                }
                cmd.CommandText = szSQL;
                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new SYSLOG_URLSTAT[datatbl.Rows.Count];
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = new SYSLOG_URLSTAT();
                        ret[i].szURL = GetSZ(datatbl.Rows[i], "URL");
                        ret[i].dwIndex = GetDW(datatbl.Rows[i], "dwIndex");
                        ret[i].dwCount = GetDW(datatbl.Rows[i], "dwCount");
                        ret[i].dwAvgUseTime = GetLONG(datatbl.Rows[i], "dwAvgUseTime");
                        ret[i].szMemo = GetSZ(datatbl.Rows[i], "szMemo");
                    }
                }
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }
    
    static public void ClearLog(uint dwDateMin, uint dwDateMax)
    {
        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "delete from log where dwDate >=@dwDateMin and dwDate <=@dwDateMax";
                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }
};
