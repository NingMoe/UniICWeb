using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SQLServerDAL;
using Model;
using Util;

namespace BLL
{
    public class InfoClassBLL
    {
        private InfoClassDAL cls = new InfoClassDAL();
        public List<ModelClass> GetClassesByGroupSN(int groupsn)
        {
            DataSet ds = cls.GetClassesByGroup(groupsn);
            return ToModelClass(ds);
        }
        public ModelClass GetClassBySN(int classsn)
        {
            return cls.GetClassBySN(classsn);
        }
        public List<ModelClass> GetClasses()
        {
            return ToModelClass(cls.GetClasses());
        }
        public int AddClass(ModelClass c)
        {
            if (cls.GetCount(c.Group)>7)
            {
                return -1;
            }
            return cls.AddClass(c);
        }
        public bool DeleteClassBySN(int sn)
        {
            return cls.DeleteClassBySN(sn);
        }
        public bool UpdateClassBySN(ModelClass c)
        {
            return cls.UpdateClassBySN(c);
        }
        public int GetMaxSN()
        {
            return cls.GetMaxSN();
        }
        private List<ModelClass> ToModelClass(DataSet ds)
        {
            List<ModelClass> list = new List<ModelClass>();
            DataRowCollection rows = ds.Tables[0].Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                ModelClass c = new ModelClass();
                c.Sn = Convert.ToInt32(rows[i]["ClTypeSN"]);
                c.Group = Convert.ToInt32(rows[i]["Group"]);
                c.GroupTitle = rows[i]["GroupTitle"].ToString();
                c.Title = rows[i]["Title"].ToString();
                c.Memo = DBUtil.DBNullReplace(rows[i]["Memo"], "").ToString();
                list.Add(c);
            }
            return list;
        }
    }
}
