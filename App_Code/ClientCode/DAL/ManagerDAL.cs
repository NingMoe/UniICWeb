using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Util;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    /// <summary>
    /// 用户相关操作类
    /// </summary>
    public class ManagerDAL
    {
        //操作管理员
        private String SQL_getManagerByName = "select * from tblManager where LogName=@logname";
        private String SQL_getManagers = "SELECT * FROM [InfoPortal].[dbo].[tblManager]";
        private String SQL_addManager = "INSERT INTO tblManager([LogName],[TrueName],[PSD],[Sex],[Age],[Tel],[Email],[ManagerType],[ManagerStatus],[Memo]) VALUES(@logname,@truename,@password,@sex,@age,@tel,@email,@type,@status,@memo)";
        private string SQL_deleteManagerByKey = "delete from tblManager where ManagerID=@managerid";
        private string SQL_UpdateManager = "UPDATE [tblManager] SET [LogName] = @logname, [PSD] = @password " +
            ",[TrueName] = @truename, [sex] = @sex ,[Age] = @age ,[Email] = " +
            "@email ,[Tel] = @tel,[ManagerStatus]=@status, [ManagerType] = @type ,[Memo] = @memo  WHERE [ManagerID]=@managerid";


        /// <summary>
        /// 通过管理员账号获得一个管理员对象
        /// </summary>
        /// <param name="name">管理员账号</param>
        /// <returns></returns>
        public Manager GetManagerByName(String name)
        {

            Manager u = null;

            SqlParameter[] array = { new SqlParameter("@logname", name) };

            DataSet ds = DBUtil.ExecuteQuery(SQL_getManagerByName, array);//***********************************************

            if (ds.Tables[0].Rows.Count > 0)
            {
                u = new Manager();
                u.Managerid = Convert.ToInt32(ds.Tables[0].Rows[0]["ManagerID"]);
                u.Logname = ds.Tables[0].Rows[0]["LogName"].ToString();
                u.Truename = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["TrueName"],"").ToString();
                u.Password = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["PSD"],"").ToString();
                u.Sex = Convert.ToInt32(DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Sex"],-1));
                u.Age = Convert.ToInt32(DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Age"],-1));
                u.Email = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Email"],"").ToString();
                u.Tel = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Tel"],"").ToString();
                u.Managertype = Convert.ToInt32(ds.Tables[0].Rows[0]["ManagerType"]);
                u.Status = Convert.ToInt32(DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["ManagerStatus"],-1));
                u.Memo = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Memo"],"").ToString();
            }

            return u;

        }
        
        /// <summary>
        /// 获取全部管理员
        /// </summary>
        /// <returns></returns>
        public DataSet GetManagers()
        {
            DataSet ds = DBUtil.ExecuteQuery(SQL_getManagers);
            return ds;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="u">管理员对象</param>
        /// <returns>影响条目数</returns>
        public int AddManager(Manager u)
        {
            int rowAffects = 0;
            SqlParameter[] array ={
                   new SqlParameter("@logname",u.Logname),
                   new SqlParameter("@truename",u.Truename),
                   new SqlParameter("@password",u.Password),
                   new SqlParameter("@sex",u.Sex),
                   new SqlParameter("@age",u.Age),
                   new SqlParameter("@email",u.Email),
                   new SqlParameter("@tel",u.Tel),
                   new SqlParameter("@type",u.Managertype),
                   new SqlParameter("@staus",u.Status),
                   new SqlParameter("@memo",u.Memo),
                                  };
            rowAffects = DBUtil.ExecuteNonQuery(SQL_addManager, array);
            return rowAffects;
        }
        /// <summary>
        /// 通过主键删除管理员
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public bool DeleteManagerByKey(int key)
        {
            SqlParameter[] array = { new SqlParameter("@managerid", key) };
            return DBUtil.ExecuteNonQuery(SQL_deleteManagerByKey, array) > 0;
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="u">管理员对象</param>
        /// <returns></returns>
        public bool UpdateUser(Manager u)
        {
            SqlParameter[] array ={
                   new SqlParameter("@logname",u.Logname),
                   new SqlParameter("@password",u.Password),
                   new SqlParameter("@truename",u.Truename),
                   new SqlParameter("@sex",u.Sex),
                   new SqlParameter("@age",u.Age),
                   new SqlParameter("@email",u.Email),
                   new SqlParameter("@tel",u.Tel),
                   new SqlParameter("@status",u.Status),
                   new SqlParameter("@type",u.Managertype),
                   new SqlParameter("@memo",u.Memo),
                   new SqlParameter("@managerid",u.Managerid),
                                 };
            return DBUtil.ExecuteNonQuery(SQL_UpdateManager, array) > 0;
        }
    }
}
