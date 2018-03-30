using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Data.SQLite;

public class LOCALUSER
{
    public string szOpenID;
    public string szLogonName;
    public string szPassword;

    public string szTrueName;
};

public class LocalSQL
{
    static SQLiteConnection m_conn = null;
    static object m_lockobj = new object();

    static SQLiteConnection GetConn()
    {
        if (m_conn == null)
        {
            string szDBFile = System.Web.HttpContext.Current.Server.MapPath("~/User.db");
            if (!File.Exists(szDBFile))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(szDBFile);
            }
            try
            {
                System.Data.SQLite.SQLiteConnectionStringBuilder connStrBuilder = new SQLiteConnectionStringBuilder("Data Source=" + szDBFile + "");

                //-----------------
                //connStrBuilder.JournalMode = SQLiteJournalModeEnum.Off;
                //connStrBuilder.SyncMode = SynchronizationModes.Off;
                //-----------------
                //connStrBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
                //connStrBuilder.SyncMode = SynchronizationModes.Off;
                //-----------------

                m_conn = new SQLiteConnection(connStrBuilder.ConnectionString);
                m_conn.Open();
                VerifyTable();
            }
            catch (Exception e)
            {
                Logger.Trace(e.Message);
            }
        }
        return m_conn;
    }
    static void ReleaseConn()
    {
        if (m_conn != null)
        {
            m_conn.Close();
        }
        m_conn = null;
    }

    static public void VerifyTable()
    {
        try
        {
            SQLiteConnection conn = GetConn();
            SQLiteCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='User';";
            if (0 == Convert.ToInt32(cmd.ExecuteScalar()))
            {
                cmd.CommandText = "create table User(szOpenID TEXT PRIMARY KEY, szLogonName TEXT, szPassword TEXT);";
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
    }

    static string GetSZ(DataRow row, string szName)
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
        return 0;
    }

    static public bool SetUser(LOCALUSER data)
    {
        try
        {
            int r = 0;
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "replace into User values(@szOpenID, @szLogonName, @szPassword);";
                cmd.Parameters.Add(new SQLiteParameter("szOpenID", data.szOpenID));
                cmd.Parameters.Add(new SQLiteParameter("szLogonName", data.szLogonName));
                cmd.Parameters.Add(new SQLiteParameter("szPassword", data.szPassword));
                r = cmd.ExecuteNonQuery();
            }
            return r == 1;
        }
        catch (Exception e)
        {
            Logger.trace(e.Message);
        }
        return false;
    }

    static public LOCALUSER GetUser(string szOpenID)
    {
        LOCALUSER ret = new LOCALUSER();
        if (string.IsNullOrEmpty(szOpenID))
        {
            return ret;
        }

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select * from User where szOpenID='" + szOpenID + "';";

                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret.szOpenID = GetSZ(datatbl.Rows[0], "szOpenID");
                    ret.szLogonName = GetSZ(datatbl.Rows[0], "szLogonName");
                    ret.szPassword = GetSZ(datatbl.Rows[0], "szPassword");
                }
            }
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }

    static public LOCALUSER DelUser(string szOpenID, string szLogonName)
    {
        LOCALUSER ret = null;
        try
        {
            lock (m_lockobj)
            {

                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select * from User where szOpenID='" + szOpenID + "' or szLogonName='" + szLogonName + "';";

                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new LOCALUSER();
                    ret.szOpenID = GetSZ(datatbl.Rows[0], "szOpenID");
                    ret.szLogonName = GetSZ(datatbl.Rows[0], "szLogonName");
                    ret.szPassword = GetSZ(datatbl.Rows[0], "szPassword");

                    cmd.CommandText = "delete from User where szOpenID='" + szOpenID + "' or szLogonName='" + szLogonName + "';";
                    cmd.ExecuteNonQuery();
                }
            }
            return ret;
        }
        catch (Exception e)
        {
            Logger.Trace(e.Message);
        }
        return ret;
    }
};