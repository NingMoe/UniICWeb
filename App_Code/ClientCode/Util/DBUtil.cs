//导入一个dll文件（System.Configuration.dll）
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;//****************************************导入命名空间
namespace Util
{
    public class DBUtil
    {
        //private static readonly String sqlConn = @"server=.;database=InfoPortal;uid=sa;pwd=unifound808";
        private static readonly String sqlConn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        public static SqlConnection Getconn()
        {
            return new SqlConnection(sqlConn);

        }
        /// <summary>
        /// 增 删 改
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(String sql)
        {
            SqlConnection conn = null;
            int rowAffected = 0;
            try
            {
                conn = Getconn();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;

                rowAffected = cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return rowAffected;

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回查询结果集合</returns>
        public static DataSet ExecuteQuery(String sql)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = Getconn();
                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                ada.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            return ds;

        }


        public static DataSet ExecuteQuery(string sql, SqlParameter[] array)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = Getconn();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(array);

                //SqlDataAdapter ada1 = new SqlDataAdapter(SQL_getUserByName, conn);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                ada.Fill(ds);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
            return ds;

        }
        public static int QueryCount(string sql, SqlParameter[] array)
        {
            SqlConnection conn = null;
            int result;
            try
            {
                conn = Getconn();
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(array);
                result = (int)cmd.ExecuteScalar();
                //SqlDataAdapter ada1 = new SqlDataAdapter(SQL_getUserByName, conn);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public static int QueryCount(string sql)
        {
            SqlConnection conn = null;
            int result;
            try
            {
                conn = Getconn();
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                result = (int)cmd.ExecuteScalar();
                //SqlDataAdapter ada1 = new SqlDataAdapter(SQL_getUserByName, conn);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public static int ExecuteNonQuery(string sql, SqlParameter[] array)
        {
            SqlConnection conn = null;
            int rowAffected = 0;
            try
            {
                conn = Getconn();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(array);


                rowAffected = cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return rowAffected;
        }
        public static Object DBNullReplace(Object oldValue, Object repValue)
        {
            if (oldValue == null || DBNull.Value.Equals(oldValue))
            {
                return repValue;
            }
            else
            {
                return oldValue;
            }


        }
        public static object CheckDataRow(DataRow row, string col, string[] s, object repValue)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (col == s[i])
                {
                    return DBUtil.DBNullReplace(row[col], repValue);
                }
            }
            return repValue;
        }
        public static Object NullReplace(Object oldValue, Object repValue)
        {
            if (oldValue == null)
            {
                return repValue;
            }
            else
            {
                return oldValue;
            }


        }
        public static DataSet GetDataSet(string p)
        {
            throw new NotImplementedException();
        }

    }


}