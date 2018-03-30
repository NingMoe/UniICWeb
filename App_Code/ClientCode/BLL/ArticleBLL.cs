using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SQLServerDAL;
using Model;
using Util;

namespace BLL
{
    public class ArticleBLL
    {
        private InfoItemDAL item = new InfoItemDAL();

        public InfoItem GetArticleByKey(int id)
        {
            return item.GetInfoItemByKey(id);
        }
        public List<InfoItem> GetAllArticlesByClass(int classsn)
        {
            return ToInfoItems(item.GetItemsByClass(classsn));
        }
        public List<InfoItem> GetPubArticlesByClass(int classsn, int start, int need)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            DataSet ds = item.GetPagingItemsByClass(classsn, need, size, -1);
            return ToInfoItems(ds);
        }
        public List<InfoItem> GetAllArticlesByClass(int classsn, int start, int need)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            DataSet ds = item.GetPagingItemTitleByClass(classsn, need, size, -2);
            return ToInfoItems(ds);
        }
        public List<InfoItem> GetArticlesByClass(int classsn, int start, int need, int status)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            return ToInfoItems(item.GetPagingItemsByClass(classsn, need, size, status));
        }
        public int AddArticle(InfoItem i)
        {
            return item.AddInfoItem(i);
        }
        public bool DeleteAriticleByKey(int key)
        {
            return item.DeleteInfoItemByKey(key);
        }
        public bool UpdateArticle(InfoItem i)
        {
            return item.UpdateInfoItem(i);
        }

        public int GetAllArticleNumberByClass(int classsn)
        {
            return item.GetInfoItemNumberByClass(classsn);
        }
        public int GetPubArticleNumberByClass(int classsn)
        {
            return item.GetInfoItemNumberByClass(classsn, -1);
        }
        public bool PlusHitCountByKey(int key)
        {
            return item.PlusHitCountByKey(key);
        }
        public List<InfoItem> GetPubArticlesTitleByClass(int classsn, int start, int need)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            return ToInfoItems(item.GetPagingItemTitleByClass(classsn, need, size,-1));
        }
        public List<InfoItem> GetAllArticlesTitleByClass(int classsn, int start, int need)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            return ToInfoItems(item.GetPagingItemTitleByClass(classsn, need, size,-2));
        }
        public List<InfoItem> GetArticlesByGroup(int groupsn, int amount)
        {
            return ToInfoItems(item.GetItemsByGroup(groupsn, amount));
        }
        public List<InfoItem> GetArticlesByGroup(int groupsn, int amount, int status)
        {
            return ToInfoItems(item.GetItemsByGroup(groupsn, amount, status));
        }
        public List<InfoItem> SearchAriticle(string key, int start, int need)
        {
            int size = start / need + 1;
            if (start % need == 0)
                --size;
            return ToInfoItems(item.SearchInfoItemTitle(key, need, size));
        }
        public int GetSearchAmount(string key)
        {
            return item.GetSearchItemAmount(key);
        }
        private List<InfoItem> ToInfoItems(DataSet ds)
        {
            List<InfoItem> list = new List<InfoItem>();
            DataRowCollection rows = ds.Tables[0].Rows;
            DataColumnCollection cols = ds.Tables[0].Columns;
            string[] s = new string[cols.Count];
            for (int n = 0; n < cols.Count; n++)
            {
                s[n] = cols[n].ColumnName;
            }
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow r = rows[0];
                InfoItem it = new InfoItem();
                it.Infoid = Convert.ToInt32(rows[i]["InfoID"]);
                it.Infoclass = Convert.ToInt32(rows[i]["Class"]);
                it.Infogroup = Convert.ToInt32(rows[i]["Group"]);
                it.Title = DBUtil.CheckDataRow(rows[i], "Title", s, "").ToString();
                it.Subhead = DBUtil.CheckDataRow(rows[i], "Subhead", s, "").ToString();
                it.Summary = DBUtil.CheckDataRow(rows[i], "Summary", s, "").ToString();
                it.Content = DBUtil.CheckDataRow(rows[i], "Content", s, "").ToString();
                it.Author = DBUtil.CheckDataRow(rows[i], "Author", s, "").ToString();
                it.Happendate = Convert.ToInt64(DBUtil.CheckDataRow(rows[i], "HappenDate", s, -1));
                it.Credate = Convert.ToInt64(DBUtil.CheckDataRow(rows[i], "CreDate", s, -1));
                it.Infostatus = Convert.ToInt32(DBUtil.CheckDataRow(rows[i], "InfoStatus", s, -1));
                it.Memo = DBUtil.CheckDataRow(rows[i], "Memo", s, "").ToString();
                it.Hitcount = Convert.ToInt32(DBUtil.CheckDataRow(rows[i], "HitCount", s, 0));
                it.Ext1 = DBUtil.CheckDataRow(rows[i], "Ext1", s, "").ToString();
                it.Ext2 = DBUtil.CheckDataRow(rows[i], "Ext2", s, "").ToString();
                it.Ext3 = DBUtil.CheckDataRow(rows[i], "Ext3", s, "").ToString();
                list.Add(it);
            }
            return list;
        }

    }
    public class ArticleConverter
    {

        public string StatusConverter(int value)
        {
            if (value == 1)
            {
                return "<span style='color:green;'>正常发布</span>";
            }
            if (value == 2)
            {
                return "<span style='color:green;'>图片新闻</span>";
            }
            if (value == 3)
            {
                return "作图片发布";
            }
            if (value == 4)
            {
                return "作幻灯片发布";
            }
            if (value == 5)
            {
                return "作视频发布";
            }
            if (value == 6)
            {

            }
            return "<span style='color:red;'>未发布</span>";
        }
        public string ValueToDateConverter(Int64 value)
        {
            string date = "";
            if (value != 0)
            {
                DateTime dt = DateTime.FromFileTime(value);
                date = string.Format("{0:yyyy-MM-dd}", dt);
            }
            return date;
        }
        public string ValueToTimeConverter(Int64 value)
        {
            string time = "";
            if (value != 0)
            {
                DateTime dt = DateTime.FromFileTime(value);
                time = string.Format("{0:T}", dt);
            }
            return time;
        }
        public string ValueToAllDateConverter(Int64 value)
        {
            string date = "";
            if (value != 0)
            {
                DateTime dt = DateTime.FromFileTime(value);
                date = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            }
            return date;
        }
        public string ValueToDateStringConverter(Int64 value)
        {
            string str = "";
            if (value != 0)
            {
                string y = value.ToString().Substring(0, 4);
                string m = value.ToString().Substring(4, 2);
                string s = value.ToString().Substring(6, 2);
                str = y + '-' + m + '-' + s;
            }
            return str;
        }
        public Int64 DateStringToValueConverter(string str)
        {
            Int64 value = 0;
            if (str != "")
            {
                Int64 y = Convert.ToInt64(str.Substring(0, 4)) * 10000;
                Int64 m = Convert.ToInt64(str.Substring(5, 2)) * 100;
                Int64 s = Convert.ToInt64(str.Substring(8, 2));
                value = y + m + s;
            }
            return value;
        }
    }
}
