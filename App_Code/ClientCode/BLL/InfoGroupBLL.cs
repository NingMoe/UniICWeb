using System;
using System.Collections.Generic;
using System.Text;
using Model;
using SQLServerDAL;
using System.Data;
using Util;

namespace BLL
{
    public class InfoGroupBLL
    {
        private InfoGroupDAL grp = new InfoGroupDAL();
        public ModelGroup GetGroupBySN(int sn)
        {
            return grp.GetGroupBySN(sn);
        }
        public List<ModelGroup> GetGroups()
        {
            return ToModelGroup(grp.GetGroups());
        }
        private List<ModelGroup> ToModelGroup(DataSet ds)
        {
            List<ModelGroup> list = new List<ModelGroup>();
            DataRowCollection rows = ds.Tables[0].Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                ModelGroup g = new ModelGroup();
                g.Sn = Convert.ToInt32(rows[i]["GrTypeSN"]);
                g.Title = rows[i]["Title"].ToString();
                g.Memo = DBUtil.DBNullReplace(rows[i]["Memo"], "").ToString();
                list.Add(g);
            }
            return list;
        }
        public int AddGroup(ModelGroup g)
        {
            return grp.AddGroup(g);
        }
        public bool DeleteGroupBySN(int sn)
        {
            return grp.DeleteGroupBySN(sn);
        }
        public bool UpdateGroupBySN(ModelGroup g)
        {
            return grp.UpdateGroupBySN(g);
        }
        public int GetMaxSN()
        {
            return grp.GetMaxSN();
        }
    }
}
