using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Util;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    class ManagerTypeDAL
    {
        //管理员类型
        private String SQL_getManagerTypeBySN = "select * from tblManagerType where MaTypeSN=@sn";
        private String SQL_getManagerTypes = "select * from tblManagerType";
        private String SQL_addManagerType = "INSERT INTO tblManagerType(*) VALUES(@sn,@name,@memo)";
        private string SQL_deleteManagerTypeBySN = "delete from tblManagerType where MaTypeSN=@sn";
        private string SQL_updateManagerTypeBySN = "update tblManagerType set [Name]=@name, [Memo]=@memo where  [MaTypeSN]=@sn";
        /// <summary>
        /// 通过编码获取管理员类型
        /// </summary>
        /// <param name="sn">类型编码</param>
        /// <returns></returns>
        public ManagerType GetManagerTypeBySN(int sn)
        {
            ManagerType t = null;
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getManagerTypeBySN, array);
            if (ds.Tables[0].Rows.Count > 0)
            {
                t = new ManagerType();
                t.Sn = Convert.ToInt32(ds.Tables[0].Rows[0]["MaTypeSN"]);
                t.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                t.Memo = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Memo"],"").ToString();
            }
            return t;
        }
        /// <summary>
        /// 获取全部管理员类型
        /// </summary>
        /// <returns></returns>
        public DataSet GetManagers()
        {
            DataSet ds = DBUtil.ExecuteQuery(SQL_getManagerTypes);
            return ds;
        }
        /// <summary>
        /// 添加管理员类型
        /// </summary>
        /// <param name="t">管理员类型对象</param>
        /// <returns>影响条目数</returns>
        public int AddManagerType(ManagerType t)
        {
            int rowAffects = 0;
            SqlParameter[] array ={
                   new SqlParameter("@sn",t.Sn),
                   new SqlParameter("@name",t.Name),
                   new SqlParameter("@memo",t.Memo),
                                  };
            rowAffects = DBUtil.ExecuteNonQuery(SQL_addManagerType, array);
            return rowAffects;
        }
        /// <summary>
        /// 根据主键删除管理员类型
        /// </summary>
        /// <param name="sn">类型编码</param>
        /// <returns></returns>
        public bool DeleteManagerTypeBySN(int sn)
        {
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            return DBUtil.ExecuteNonQuery(SQL_deleteManagerTypeBySN, array) > 0;
        }
        /// <summary>
        /// 更新管理员类型信息
        /// </summary>
        /// <param name="t">管理员类型对象</param>
        /// <returns></returns>
        public bool UpdateManagerTypeBySN(ManagerType t)
        {
            SqlParameter[] array ={
                   new SqlParameter("@name",t.Name),
                   new SqlParameter("@memo",t.Memo),
                   new SqlParameter("@sn",t.Sn),
                                 };
            return DBUtil.ExecuteNonQuery(SQL_updateManagerTypeBySN, array) > 0;
        }
    }
}
