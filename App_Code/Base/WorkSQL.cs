using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Data.SQLite;


public class WORK_DATA
{
    public uint dwID;
    public uint dwUserID;
    public string szTitle;
    public uint dwDate;
    public uint dwTime;
    public string szFileName;
    public string szUserFileName;
    public uint dwFlag;
};

public class WorkSQL
{
    static SQLiteConnection m_conn = null;
    static object m_lockobj = new object();

    static SQLiteConnection GetConn()
    {
        if (m_conn == null)
        {
            try
            {
                if (IntPtr.Size == 4)
                {
                    File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Bin/x86/SQLite.Interop.dll"), System.Web.HttpContext.Current.Server.MapPath("~/Bin/SQLite.Interop.dll"), true);
                }
                else if (IntPtr.Size == 8)
                {
                    File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Bin/x64/SQLite.Interop.dll"), System.Web.HttpContext.Current.Server.MapPath("~/Bin/SQLite.Interop.dll"), true);
                }
            }
            catch (Exception e)
            {
                //Logger.Trace(e.Message);
            }

            string szDBFile = System.Web.HttpContext.Current.Server.MapPath("~/Work.db");
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
                        //Logger.Trace(ee.Message);
                    }
                }
                //Logger.Trace(e.Message);
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
            cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='work';";
            if (0 == Convert.ToInt32(cmd.ExecuteScalar()))
            {
                cmd.CommandText = "create table work(dwUserID INTEGER,szTitle TEXT,dwDate INTEGER, dwTime INTEGER,szFileName TEXT,szUserFileName TEXT,dwFlag INTEGER);";
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            //Logger.Trace(e.Message);
        }
    }

    static public bool SetWork(WORK_DATA data)
    {
        try
        {
            lock (m_lockobj)
            {
                int dwDate = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                int dwTime = DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second;

                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into work values(@dwUserID,@szTitle,@dwDate,@dwTime,@szFileName,@szUserFileName,@dwFlag);";
                cmd.Parameters.Add(new SQLiteParameter("dwUserID", data.dwUserID));
                cmd.Parameters.Add(new SQLiteParameter("szTitle", data.szTitle));
                cmd.Parameters.Add(new SQLiteParameter("dwDate", dwDate));
                cmd.Parameters.Add(new SQLiteParameter("dwTime", dwTime));
                cmd.Parameters.Add(new SQLiteParameter("szFileName", data.szFileName));
                cmd.Parameters.Add(new SQLiteParameter("szUserFileName", data.szUserFileName));
                cmd.Parameters.Add(new SQLiteParameter("dwFlag", data.dwFlag));
                int r = cmd.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception)
        {
        }
        return false;
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

    static public WORK_DATA[] GetWork(uint dwUserID, uint dwDateMin, uint dwDateMax, uint dwID, uint dwFlag, ref ParamExt ext)
    {
        WORK_DATA[] ret = null;
        ext.nTotalCount = 0;

        try
        {
            lock (m_lockobj)
            {
                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                string szSubCMD = "";
                if (dwFlag != 0)
                {
                    //4294901760 === 0xFFFF0000
                    szSubCMD = " dwFlag = " + dwFlag + " and ";
                }
                if (dwID != 0)
                {
                    szSubCMD += " rowid = " + dwID + " and ";
                }

                cmd.CommandText = "select count(*) from work where " + szSubCMD + " dwDate >=@dwDateMin and dwDate <=@dwDateMax and dwUserID == @dwUserID;";

                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                cmd.Parameters.Add(new SQLiteParameter("dwUserID", dwUserID));
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

                cmd.CommandText = "select rowid,* from work where " + szSubCMD + " dwDate >=@dwDateMin and dwDate <=@dwDateMax and dwUserID == @dwUserID order by " + ext.szOrderField + " " + ext.szOrder + ",rowid " + ext.szOrder + " limit " + ext.nStartPage + "," + ext.nPageCount + ";";

                cmd.Parameters.Add(new SQLiteParameter("dwDateMin", dwDateMin));
                cmd.Parameters.Add(new SQLiteParameter("dwDateMax", dwDateMax));
                cmd.Parameters.Add(new SQLiteParameter("dwUserID", dwUserID));
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new WORK_DATA[datatbl.Rows.Count];
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = new WORK_DATA();
                        ret[i].dwID = GetDW(datatbl.Rows[i], "rowid");
                        ret[i].dwDate = GetDW(datatbl.Rows[i], "dwDate");
                        ret[i].dwTime = GetDW(datatbl.Rows[i], "dwTime");
                        ret[i].szTitle = GetSZ(datatbl.Rows[i], "szTitle");
                        ret[i].dwFlag = GetDW(datatbl.Rows[i], "dwFlag");
                        ret[i].szFileName = GetSZ(datatbl.Rows[i], "szFileName");
                        ret[i].szUserFileName = GetSZ(datatbl.Rows[i], "szUserFileName");
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Logger.Trace(e.Message);
        }
        return ret;
    }

    static public WORK_DATA DelWork(uint dwID)
    {
        WORK_DATA ret = null;
        try
        {
            lock (m_lockobj)
            {

                SQLiteConnection conn = GetConn();
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select rowid,* from work where rowid=" + dwID + ";";

                SQLiteDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable datatbl = new DataTable();
                    datatbl.Load(sdr);
                    sdr.Close();
                    ret = new WORK_DATA();
                    int i = 0;
                    ret.dwID = GetDW(datatbl.Rows[i], "rowid");
                    ret.dwDate = GetDW(datatbl.Rows[i], "dwDate");
                    ret.dwTime = GetDW(datatbl.Rows[i], "dwTime");
                    ret.szTitle = GetSZ(datatbl.Rows[i], "szTitle");
                    ret.dwFlag = GetDW(datatbl.Rows[i], "dwFlag");
                    ret.szFileName = GetSZ(datatbl.Rows[i], "szFileName");
                    ret.szUserFileName = GetSZ(datatbl.Rows[i], "szUserFileName");

                    cmd.CommandText = "update work set dwFlag=0 where rowid =" + dwID;
                    cmd.ExecuteNonQuery();
                }
            }
            return ret;
        }
        catch (Exception e)
        {
            //Logger.Trace(e.Message);
        }
        return ret;
    }
};