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
    /// 信息对象相关类
    /// </summary>
    public class InfoItemDAL
    {
        //操作信息对象
        private String SQL_getInfoItemByKey = "select * from tblInfoItem where InfoID=@id";
        private String SQL_getInfoItemsByClass = "select * from tblInfoItem where Class=@class";
        private String SQL_getAllInfoItems = "select * from tblInfoItem";
        private String SQL_addInfoItem = "INSERT INTO tblInfoItem([Class],[Group],[Title],[Subhead],[Summary],[Content],[Ext1],[Ext2],[Ext3],[Author],[HappenDate],[CreDate],[InfoStatus],[Memo]) VALUES(@class,@group,@title,@subhead,@summary,@content,@ext1,@ext2,@ext3,@author,@happendate,@credate,@infostatus,@memo)";
        private string SQL_deleteInfoItemByKey = "delete from tblInfoItem where InfoID=@id";
        private string SQL_updateInfoItemByKey = "update tblInfoItem set [Class]=@class,[Group]=@group,[Title]=@title,[Subhead]=@subhead,[Summary]=@summary,[Content]=@content,[Ext1]=@ext1,[Ext2]=@ext2,[Ext3]=@ext3,[Author]=@author,[HappenDate]=@happendate,[CreDate]=@credate,[InfoStatus]=@infostatus,[Memo]=@memo where  [InfoID]=@id";
        private string SQL_getInfoItemNumberByClass = "select COUNT(*) from tblInfoItem where Class=@classsn";
        private string SQL_plusHitCountByKey = "update tblInfoItem set [HitCount]=[HitCount]+1 where InfoID=@id ";
        /// <summary>
        /// 通过主键获取信息对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public InfoItem GetInfoItemByKey(int id)
        {
            InfoItem i = null;
            SqlParameter[] array = { new SqlParameter("@id", id) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getInfoItemByKey, array);
            if (ds.Tables[0].Rows.Count > 0)
            {
                i = new InfoItem();
                i.Infoid = Convert.ToInt32(ds.Tables[0].Rows[0]["InfoID"]);
                i.Infoclass = Convert.ToInt32(ds.Tables[0].Rows[0]["Class"]);
                i.Infogroup = Convert.ToInt32(ds.Tables[0].Rows[0]["Group"]);
                i.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                i.Subhead = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Subhead"], "").ToString();
                i.Summary = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Summary"], "").ToString();
                i.Content = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Content"], "").ToString();
                i.Ext1 = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Ext1"], "").ToString();
                i.Ext2 = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Ext2"], "").ToString();
                i.Ext3 = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Ext3"], "").ToString();
                i.Author = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Author"], "").ToString();
                i.Happendate = Convert.ToInt64(DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["HappenDate"], -1));
                i.Credate = Convert.ToInt64(ds.Tables[0].Rows[0]["CreDate"]);
                i.Infostatus = Convert.ToInt32(ds.Tables[0].Rows[0]["InfoStatus"]);
                i.Memo = DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["Memo"], "").ToString();
                i.Hitcount = Convert.ToInt32(ds.Tables[0].Rows[0]["HitCount"]);
            }
            return i;
        }
        /// <summary>
        /// 通过信息类编码获取信息对象集合
        /// </summary>
        /// <param name="classsn">信息类编码</param>
        /// <returns></returns>
        public DataSet GetItemsByClass(int classsn)
        {
            SqlParameter[] array = { new SqlParameter("@class", classsn) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getInfoItemsByClass, array);
            return ds;
        }
        /// <summary>
        /// 通过信息分组编码获取信息对象集合
        /// </summary>
        /// <param name="groupsn">信息分组编码</param>
        /// <param name="amount">信息条数</param>
        /// <returns></returns>
        public DataSet GetItemsByGroup(int groupsn, int amount)
        {
            int a = 0;
            if (amount > 0)
            {
                a = amount;
            }
            string SQL_getInfoItemsByGroup = "select top " + a + " a.InfoID,a.[Class],a.[Group],a.[Title],a.[Subhead],a.[Summary],a.[Ext1],a.[Ext2],a.[Ext3],a.[Author],a.[HappenDate],a.[CreDate],a.[InfoStatus],a.[Memo],b.Title as TypeName from tblInfoItem a inner join tblClass b on a.Class=b.ClTypeSN where a.[Group]=" + groupsn + "  order by CreDate desc";
            //SqlParameter[] array = { new SqlParameter("@groupsn", groupsn), new SqlParameter("@top", a) };
            DataSet ds = DBUtil.ExecuteQuery(SQL_getInfoItemsByGroup);
            return ds;
        }
        /// <summary>
        /// 通过信息分组编码获取指定状态信息对象集合
        /// </summary>
        /// <param name="groupsn">信息分组编码</param>
        /// <param name="amount">信息条数</param>
        /// <param name="status">信息状态编码，-2为获取全部信息，-1为获取所有已发布信息，不小于0则为获取指定状态信息</param>
        /// <returns></returns>
        public DataSet GetItemsByGroup(int groupsn, int amount, int status)
        {
            int a = 0;
            if (amount > 0)
            {
                a = amount;
            }
            string SQL_getInfoItemsByGroup = "";
            if (status == -2)
            {
                SQL_getInfoItemsByGroup = "select top " + a + " a.InfoID,a.[Class],a.[Group],a.[Title],a.[Subhead],a.[Summary],a.[Ext1],a.[Ext2],a.[Ext3],a.[Author],a.[HappenDate],a.[CreDate],a.[InfoStatus],a.[Memo],b.Title as TypeName from tblInfoItem a inner join tblClass b on a.Class=b.ClTypeSN where a.[Group]=" + groupsn + "    order by CreDate desc";
            }
            else if (status == -1)
            {
                SQL_getInfoItemsByGroup = "select top " + a + " a.InfoID,a.[Class],a.[Group],a.[Title],a.[Subhead],a.[Summary],a.[Ext1],a.[Ext2],a.[Ext3],a.[Author],a.[HappenDate],a.[CreDate],a.[InfoStatus],a.[Memo],b.Title as TypeName from tblInfoItem a inner join tblClass b on a.Class=b.ClTypeSN where a.[Group]=" + groupsn + " and   a.[InfoStatus]>0  order by CreDate desc";
            }
            else
            {
                SQL_getInfoItemsByGroup = "select top " + a + " a.InfoID,a.[Class],a.[Group],a.[Title],a.[Subhead],a.[Summary],a.[Ext1],a.[Ext2],a.[Ext3],a.[Author],a.[HappenDate],a.[CreDate],a.[InfoStatus],a.[Memo],b.Title as TypeName from tblInfoItem a inner join tblClass b on a.Class=b.ClTypeSN where a.[Group]=" + groupsn + " and   a.[InfoStatus]=" + status + "  order by CreDate desc";
            }
            DataSet ds = DBUtil.ExecuteQuery(SQL_getInfoItemsByGroup);
            return ds;
        }
        /// <summary>
        /// 获取全部信息对象
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllInfoItems()
        {
            DataSet ds = DBUtil.ExecuteQuery(SQL_getAllInfoItems);
            return ds;
        }
        /// <summary>
        /// 获取分页信息对象集合
        /// </summary>
        /// <returns></returns>
        public DataSet GetPagingItemsByClass(int classsn, int size, int index)
        {
            String SQL_getPagingItemsByClass = "select top " +
                size.ToString() + " * from tblInfoItem where (CreDate NOT IN (select top " +
                (size * (index - 1)).ToString() + " CreDate  from tblInfoItem where Class=" +
                classsn + " order by CreDate desc)) and Class=" + classsn + " order by CreDate desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_getPagingItemsByClass);
            return ds;
        }
        public DataSet GetPagingItemsByClass(int classsn, int size, int index, int status)
        {
            string sta = ToStatusStr(status);
            String SQL_getPagingItemsByClass = "select top " +
                size.ToString() + " * from tblInfoItem where (CreDate NOT IN (select top " +
                (size * (index - 1)).ToString() + " CreDate  from tblInfoItem where Class=" +
                classsn + sta + " order by CreDate desc)) and Class=" + classsn +
                sta + " order by CreDate desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_getPagingItemsByClass);
            return ds;
        }
        /// <summary>
        /// 获取分页信息对象标题集合
        /// </summary>
        /// <returns></returns>
        public DataSet GetPagingItemTitleByClass(int classsn, int size, int index)
        {
            String SQL_getPagingItemTitleByClass = "select top " +
                size.ToString() + " [InfoID],[Class],[Group],[Title],[Subhead],[Summary],[Ext1],[Ext2],[Ext3],[Author],[HappenDate],[CreDate],[InfoStatus],[Memo] from tblInfoItem where (CreDate NOT IN (select top " +
                (size * (index - 1)).ToString() + " CreDate  from tblInfoItem where Class=" +
                classsn + " order by CreDate desc)) and Class=" + classsn + " order by CreDate desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_getPagingItemTitleByClass);
            return ds;
        }
        /// <summary>
        /// 获取指定状态分页信息对象标题集合
        /// </summary>
        /// <param name="status">信息状态编码，-2为获取全部信息，-1为获取所有已发布信息，不小于0则为获取指定状态信息</param>
        /// <returns></returns>
        public DataSet GetPagingItemTitleByClass(int classsn, int size, int index, int status)
        {
            string s = "";
            if (status == -2)
            {
            }
            else if (status == -1)
            {
                s = " and  [InfoStatus]>0 ";
            }
            else
            {
                s = " and  [InfoStatus]=" + status;
            }
            String SQL_getPagingItemTitleByClass = "select top " +
                size.ToString() + " [InfoID],[Class],[Group],[Title],[Subhead],[Summary],[Ext1],[Ext2],[Ext3],[Author],[HappenDate],[CreDate],[InfoStatus],[Memo] from tblInfoItem where (CreDate NOT IN (select top " +
                (size * (index - 1)).ToString() + " CreDate  from tblInfoItem where Class=" +
                classsn + s + " order by CreDate desc)) and Class=" + classsn + s + "  order by CreDate desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_getPagingItemTitleByClass);
            return ds;
        }
        /// <summary>
        /// 获取搜索信息条目数
        /// </summary>
        /// <param name="key">搜索关键字</param>
        /// <returns></returns>
        public int GetSearchItemAmount(string key)
        {
            String SQL_getSearchItemAmount = "select COUNT(*) from tblInfoItem where [Title]  like '%" + key + "%'  and [InfoStatus]>0 ";
            return DBUtil.QueryCount(SQL_getSearchItemAmount);
        }
        /// <summary>
        /// 通过关键字搜索信息对象标题集合(分页数据)
        /// </summary>
        /// <param name="key">搜索关键字</param>
        /// <returns></returns>
        public DataSet SearchInfoItemTitle(string key, int size, int index)
        {
            String SQL_searchInfoItemTitle = "select top " +
                size.ToString() + " [InfoID],[Class],[Group],[Title],[Author],[CreDate],[InfoStatus],[Memo] from tblInfoItem where (CreDate NOT IN (select top " +
                (size * (index - 1)).ToString() + " CreDate  from tblInfoItem where [Title] like '%" +
                key + "%' and [InfoStatus]>0 order by CreDate desc)) and [Title] like '%" + key + "%'  and [InfoStatus]>0 order by CreDate desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_searchInfoItemTitle);
            return ds;
        }
        /// <summary>
        /// 添加信息对象
        /// </summary>
        /// <param name="i">信息对象</param>
        /// <returns>影响条目数</returns>
        public int AddInfoItem(InfoItem i)
        {
            int rowAffects = 0;
            SqlParameter[] array ={
                   new SqlParameter("@class",i.Infoclass),
                   new SqlParameter("@group",i.Infogroup),
                   new SqlParameter("@title",i.Title),
                   new SqlParameter("@subhead",Util.DBUtil.NullReplace(i.Subhead,"")),
                   new SqlParameter("@summary",Util.DBUtil.NullReplace(i.Summary,"")),
                   new SqlParameter("@content",Util.DBUtil.NullReplace(i.Content,"")),
                   new SqlParameter("@ext1",Util.DBUtil.NullReplace(i.Ext1,"")),
                   new SqlParameter("@ext2",Util.DBUtil.NullReplace(i.Ext2,"")),
                   new SqlParameter("@ext3",Util.DBUtil.NullReplace(i.Ext3,"")),
                   new SqlParameter("@author",Util.DBUtil.NullReplace(i.Author,"")),
                   new SqlParameter("@happendate",i.Happendate),
                   new SqlParameter("@credate",i.Credate),
                   new SqlParameter("@infostatus",i.Infostatus),
                   new SqlParameter("@memo",Util.DBUtil.NullReplace(i.Memo,""))
                                  };
            rowAffects = DBUtil.ExecuteNonQuery(SQL_addInfoItem, array);
            return rowAffects;
        }
        /// <summary>
        /// 根据主键删除信息对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public bool DeleteInfoItemByKey(int key)
        {
            SqlParameter[] array = { new SqlParameter("@id", key) };
            return DBUtil.ExecuteNonQuery(SQL_deleteInfoItemByKey, array) > 0;
        }
        /// <summary>
        /// 更新信息对象
        /// </summary>
        /// <param name="i">信息对象对象</param>
        /// <returns></returns>
        public bool UpdateInfoItem(InfoItem i)
        {
            SqlParameter[] array ={
                   new SqlParameter("@class",i.Infoclass),
                   new SqlParameter("@group",i.Infogroup),
                   new SqlParameter("@title",i.Title),
                   new SqlParameter("@subhead",Util.DBUtil.NullReplace( i.Subhead,"")),
                   new SqlParameter("@summary",Util.DBUtil.NullReplace(i.Summary,"")),
                   new SqlParameter("@content",Util.DBUtil.NullReplace( i.Content,"")),
                   new SqlParameter("@author",Util.DBUtil.NullReplace(i.Author,"")),
                   new SqlParameter("@happendate",Util.DBUtil.NullReplace(i.Happendate,0)),
                   new SqlParameter("@credate",i.Credate),
                   new SqlParameter("@infostatus",i.Infostatus),
                   new SqlParameter("@memo",Util.DBUtil.NullReplace(i.Memo,"")),
                   new SqlParameter("@id",i.Infoid),
                   new SqlParameter("@ext1",Util.DBUtil.NullReplace(i.Ext1,"")),
                   new SqlParameter("@ext2",Util.DBUtil.NullReplace(i.Ext2,"")),
                   new SqlParameter("@ext3",Util.DBUtil.NullReplace(i.Ext3,""))
                                 };
            return DBUtil.ExecuteNonQuery(SQL_updateInfoItemByKey, array) > 0;
        }

        public int GetInfoItemNumberByClass(int classsn)
        {
            SqlParameter[] array = { new SqlParameter("@classsn", classsn) };
            return DBUtil.QueryCount(SQL_getInfoItemNumberByClass, array);
        }
        public int GetInfoItemNumberByClass(int classsn, int status)
        {
            string str = "";
            if (status == -2)
            {
            }
            else if (status == -1)
            {
                str = " and  [InfoStatus]>0 ";
            }
            else
            {
                str = " and  [InfoStatus]=" + status;
            }
            string getInfoItemNumberByClass = "select COUNT(*) from tblInfoItem where Class=" + classsn + str;
            return DBUtil.QueryCount(getInfoItemNumberByClass);
        }
        public bool PlusHitCountByKey(int key)
        {
            SqlParameter[] array = { new SqlParameter("@id", key) };
            return DBUtil.ExecuteNonQuery(SQL_plusHitCountByKey, array) > 0;
        }
        private string ToStatusStr(int status)
        {
            string s = "";
            if (status == -2)
            {
            }
            else if (status == -1)
            {
                s = " and  [InfoStatus]>0 ";
            }
            else
            {
                s = " and  [InfoStatus]=" + status;
            }
            return s;
        }
    }
}
