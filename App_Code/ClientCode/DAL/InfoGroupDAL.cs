using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Util;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class InfoGroupDAL
    {
        //信息分组
        private String SQL_getGroupBySN = "select * from tblGroup where GrTypeSN=@sn";
        private String SQL_getGroups = "select * from tblGroup";
        private String SQL_addGroup = "INSERT INTO tblGroup(*) VALUES(@sn,@title,@memo)";
        private string SQL_deleteGroupBySN = "delete from tblGroup where GrTypeSN=@sn";
        private string SQL_updateGroupBySN = "update tblGroup set [Title]=@title, [Memo]=@memo where  [GrTypeSN]=@sn";
        private string SQL_getMaxSN = "select MAX(GrTypeSN) from tblGroup";
        /// <summary>
        /// 通过编码获取信息分组
        /// </summary>
        /// <param name="sn">信息分组编码</param>
        /// <returns></returns>
        public ModelGroup GetGroupBySN(int sn)
        {
            ModelGroup g = null;
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getGroupBySN, array);
            if (ds.Tables[0].Rows.Count > 0)
            {
                g = new ModelGroup();
                g.Sn = sn;
                g.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                g.Memo = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Memo"],"").ToString();
            }
            return g;
        }
        /// <summary>
        /// 获取全部信息分组
        /// </summary>
        /// <returns></returns>
        public DataSet GetGroups()
        {
            DataSet ds = DBUtil.ExecuteQuery(SQL_getGroups);
            return ds;
        }
        /// <summary>
        /// 添加信息分组
        /// </summary>
        /// <param name="g">信息分组对象</param>
        /// <returns>影响条目数</returns>
        public int AddGroup(ModelGroup g)
        {
            int rowAffects = 0;
            SqlParameter[] array ={
                   new SqlParameter("@sn",g.Sn),
                   new SqlParameter("@title",g.Title),
                   new SqlParameter("@memo",g.Memo),
                                  };
            rowAffects = DBUtil.ExecuteNonQuery(SQL_addGroup, array);
            return rowAffects;
        }
        /// <summary>
        /// 根据编码删除信息分组
        /// </summary>
        /// <param name="sn">信息分组编码</param>
        /// <returns></returns>
        public bool DeleteGroupBySN(int sn)
        {
            SqlParameter[] array = { new SqlParameter("@sn", sn) };
            return DBUtil.ExecuteNonQuery(SQL_deleteGroupBySN, array) > 0;
        }
        /// <summary>
        /// 更新信息分组
        /// </summary>
        /// <param name="g">信息分组对象</param>
        /// <returns></returns>
        public bool UpdateGroupBySN(ModelGroup g)
        {
            SqlParameter[] array ={
                   new SqlParameter("@title",g.Title),
                   new SqlParameter("@memo",g.Memo),
                   new SqlParameter("@sn",g.Sn),
                                 };
            return DBUtil.ExecuteNonQuery(SQL_updateGroupBySN, array) > 0;
        }

        public int GetMaxSN()
        {
            return (int)DBUtil.QueryCount(SQL_getMaxSN);
        }
    }
}
