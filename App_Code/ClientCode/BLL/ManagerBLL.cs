using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SQLServerDAL;
using Model;


namespace BLL
{
    public class ManagerBLL
    {
        ManagerDAL managerDAL = new ManagerDAL();
        /// <summary>
        /// 通过管理员账号获得一个管理员对象
        /// </summary>
        /// <param name="name">管理员账号</param>
        /// <returns></returns>
        public Manager GetManagerByName(String name)
        {
            return managerDAL.GetManagerByName(name);
        }
        /// <summary>
        /// 获取全部管理员
        /// </summary>
        /// <returns></returns>
        public DataSet GetManagers()
        {
            return managerDAL.GetManagers();
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="u">管理员对象</param>
        /// <returns>影响条目数</returns>
        public int AddManager(Manager u)
        {
            return managerDAL.AddManager(u);
        }
        /// <summary>
        /// 通过主键删除管理员
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public bool DeleteManagerByKey(int key)
        {
            return managerDAL.DeleteManagerByKey(key);
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="u">管理员对象</param>
        /// <returns></returns>
        public bool UpdateUser(Manager u)
        {
            return managerDAL.UpdateUser(u);
        }
    }
}
