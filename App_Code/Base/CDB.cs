using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

/// <summary>
/// CDB 的摘要说明
/// </summary>
public class CDB
{
    //================数据库访问=======================
    private const int MaxPool = 10;//最大连接数
    private const int MinPool = 2;//最小连接数
    private const bool Asyn_Process = true;//设置异步访问数据库
    private const int Conn_Timeout = 15;//设置连接等待时间
    //private const int Conn_Lifetime = 15;//设置连接的生命周期
    //private const bool Mars = true;//在单个连接上得到和管理多个、仅向前引用和只读的结果集(ADO.NET2.0)  
    static private DbConnection Conn = null;//连接对象

    static private string m_szError = "";
    static public string szError
    {
        get
        {
            return szError;
        }
    }

    static private string ConnString
    {
        get
        {
            return "Server=10.121.0.3;"
                + "initial catalog=LabInfo;user id=CCMAdmin;password=lchz761UF;"
                + "Max Pool Size=" + MaxPool + ";"
                + "Min Pool Size=" + MinPool + ";"
                + "Connect Timeout=" + Conn_Timeout + ";"
                //+ "Connection Lifetime=" + Conn_Lifetime + ";"
                + "Asynchronous Processing=" + Asyn_Process + ";";
            //+"MultipleActiveResultSets="+Mars+";";
        }
    }

    static public DataTable GetSQLData(string StrSql)
    {
        m_szError = "";
        //当连接处于打开状态时关闭,然后再打开,避免有时候数据不能及时更新
        if (Conn == null)
        {
            Conn = new SqlConnection(ConnString);
        }
        if (Conn.State == ConnectionState.Open)
        {
            Conn.Close();
        }
        try
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbCommand cmd = Conn.CreateCommand();
            cmd.CommandText = StrSql;
            DbDataReader dreader = cmd.ExecuteReader();
            DataTable datatbl = new DataTable();
            if (dreader.HasRows)
            {
                datatbl.Load(dreader);
                dreader.Close();
                Conn.Close();
                return datatbl;
            }
            return datatbl;
        }
        catch (Exception e)
        {
            m_szError = e.Message;
            System.Console.Out.WriteLine(e.Message);
        }
        finally
        {
            Conn.Close();
        }
        return null;
    }
}
