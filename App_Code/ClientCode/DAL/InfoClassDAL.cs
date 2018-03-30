using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Util;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class InfoClassDAL
    {
        //信息类
        private String SQL_getClassBySN = "select * from tblClass where ClTypeSN=@sn";
        private String SQL_getClassesByGroup = "select * from tblClass where [Group]=@group";
        private String SQL_getClasses = "select * from tblClass";
        private String SQL_addClass = "INSERT INTO tblClass([Group],[GroupTitle],[Title],[Memo]) VALUES(@group,@groupTitle,@title,@memo)";
        private string SQL_deleteClassBySN = "delete from tblClass where ClTypeSN=@sn";
        private string SQL_updateClassBySN = "update tblClass set [Group]=@group,[GroupTitle]=@groupTitle,[Title]=@title, [Memo]=@memo where  [ClTypeSN]=@sn";
        private string SQL_getMaxSN = "select MAX(ClTypeSN) from tblClass";
        private string SQL_getCount = "select COUNT(*) from tblClass where [Group]=@sn";
        /// <summary>
        /// 通过编码获取信息类
        /// </summary>
        /// <param name="sn">类型编码</param>
        /// <returns></returns>
        public ModelClass GetClassBySN(int sn)
        {
            ModelClass c = null;
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getClassBySN, array);
            if (ds.Tables[0].Rows.Count > 0)
            {
                c = new ModelClass();
                c.Sn = Convert.ToInt32(ds.Tables[0].Rows[0]["ClTypeSN"]);
                c.Group = Convert.ToInt32(ds.Tables[0].Rows[0]["Group"]);
                c.GroupTitle = ds.Tables[0].Rows[0]["GroupTitle"].ToString();
                c.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                c.Memo = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Memo"],"").ToString();
            }
            return c;
        }
        /// <summary>
        /// 通过信息分组编码获取信息类集合
        /// </summary>
        /// <param name="sn">信息分组编码</param>
        /// <returns></returns>
        public DataSet GetClassesByGroup(int groupsn)
        {
            SqlParameter[] array = { new SqlParameter("@group", groupsn) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getClassesByGroup,array);
            return ds;
        }
        /// <summary>
        /// 获取全部信息类
        /// </summary>
        /// <returns></returns>
        public DataSet GetClasses()
        {
            DataSet ds = DBUtil.ExecuteQuery(SQL_getClasses);
            return ds;
        }
        /// <summary>
        /// 添加信息类
        /// </summary>
        /// <param name="c">信息类对象</param>
        /// <returns>影响条目数</returns>
        public int AddClass(ModelClass c)
        {
            int rowAffects = 0;
            SqlParameter[] array ={
                   new SqlParameter("@group",c.Group),
                   new SqlParameter("@groupTitle",c.GroupTitle),
                   new SqlParameter("@title",c.Title),
                   new SqlParameter("@memo",c.Memo),
                                  };
            rowAffects = DBUtil.ExecuteNonQuery(SQL_addClass, array);
            return rowAffects;
        }
        /// <summary>
        /// 根据编码删除信息类
        /// </summary>
        /// <param name="sn">信息类编码</param>
        /// <returns></returns>
        public bool DeleteClassBySN(int sn)
        {
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            return DBUtil.ExecuteNonQuery(SQL_deleteClassBySN, array) > 0;
        }
        /// <summary>
        /// 更新信息类
        /// </summary>
        /// <param name="c">信息类对象</param>
        /// <returns></returns>
        public bool UpdateClassBySN(ModelClass c)
        {
            SqlParameter[] array ={
                   new SqlParameter("@group",c.Group),
                   new SqlParameter("@groupTitle",c.GroupTitle),
                   new SqlParameter("@title",c.Title),
                   new SqlParameter("@memo",c.Memo),
                   new SqlParameter("@sn",c.Sn),
                                 };
            return DBUtil.ExecuteNonQuery(SQL_updateClassBySN, array) > 0;
        }

        public int GetMaxSN()
        {
            return (int)DBUtil.QueryCount(SQL_getMaxSN);
        }
        public int GetCount(int sn)
        {
            SqlParameter[] array = { new SqlParameter("@sn", sn), };
            return (int)DBUtil.QueryCount(SQL_getCount,array);
        }
    }
}
