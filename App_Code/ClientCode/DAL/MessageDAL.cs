using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Data.SqlClient;
using System.Data;

namespace SQLServerDAL
{
    public class MessageDAL
    {
        private String SQL_AddMessage = @"INSERT INTO [tblMessage]
                                        ([id]
                                        ,[name]
                                        ,[phone]
                                        ,[address]
                                        ,[email]
                                        ,[content]
                                        ,[reply]
                                        ,[time]
                                        ,[condition]
                                        ,[type])
                                         VALUES
                                        (@id
                                        ,@name
                                        ,@phone
                                        ,@address
                                        ,@email
                                        ,@content
                                        ,@reply
                                        ,@time
                                        ,@condition
                                        ,@type)";
        private string SQL_UpdateReply = @"update [tblMessage] set reply=@reply where id=@id";
        private string SQL_GetMessageAmountByType = @"select count(id) from tblMessage where type=@typeId";
        private string SQL_GetMessageAmount = @"select count(id) from tblMessage";
        private string SQL_GetMessageById = @"select * from tblMessage where id=@id";

        public int GetMessageAmount()
        {
            int result;
            result = DBUtil.QueryCount(SQL_GetMessageAmount);
            return result;
        }
        public int GetMessageAmountByType(int typeId)
        {
            int result;
            SqlParameter[] array = { new SqlParameter("@typeId", typeId) };
            result = DBUtil.QueryCount(SQL_GetMessageAmountByType, array);
            return result;
        }

        public DataSet GetMessageByType(int typeId, int size, int index)
        {
            String SQL_GetMessageByType = "select top " +
                size.ToString() + " * from tblMessage where (time NOT IN (select top " +
                (size * (index - 1)).ToString() + " time  from tblMessage where typeid=" +
                typeId + " order by time desc)) and typeid=" + typeId + " order by time desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_GetMessageByType);
            return ds;
        }


        public System.Data.DataSet GetMessage(int size, int index)
        {
            String SQL_GetMessage = "select top " +
                size.ToString() + " * from tblMessage where (time NOT IN (select top " +
                (size * (index - 1)).ToString() + " time  from tblMessage order by time desc)) order by time desc";
            DataSet ds = DBUtil.ExecuteQuery(SQL_GetMessage);
            return ds;
        }

        public int InsertMessage(Model.Message message)
        {
            SqlParameter[] array = {
                                      new SqlParameter("@id",message.Id),
                                      new SqlParameter("@name",message.Name),
                                      new SqlParameter("@phone",message.Phone),
                                      new SqlParameter("@address",message.Address),
                                      new SqlParameter("@email",message.Email),
                                      new SqlParameter("@content",message.Content),
                                      new SqlParameter("@reply",message.Reply),
                                      new SqlParameter("@time",message.Time),
                                      new SqlParameter("@condition",message.Condition),
                                      new SqlParameter("@type",message.Type),
                                  };

            return DBUtil.ExecuteNonQuery(SQL_AddMessage, array);
        }


        public int UpdateMessage(List<string> keys)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update [tblMessage] set condition='true' where id in (");
            foreach (string id in keys)
            {
                str.Append("'");
                str.Append(id);
                str.Append("',");
            }
            string sql = str.ToString().Substring(0, str.Length - 1) + ')';
            return DBUtil.ExecuteNonQuery(sql);
        }

        public int UpdateMessage(string reply,string key)
        {
            SqlParameter[] array ={
                                     new SqlParameter("@reply",reply),
                                     new SqlParameter("@id",key)
                                 };

            return DBUtil.ExecuteNonQuery(SQL_UpdateReply, array);
        }

        public Model.Message GetMessageById(string id)
        {
            SqlParameter[] array = { new SqlParameter("@id", id) };
            System.Data.DataSet ds = DBUtil.ExecuteQuery(SQL_GetMessageById, array);
            Model.Message msg = new Model.Message();
            msg.Id = ds.Tables[0].Rows[0]["id"].ToString();
            msg.Name = ds.Tables[0].Rows[0]["name"].ToString();
            msg.Phone = ds.Tables[0].Rows[0]["phone"].ToString();
            msg.Address =Util.DBUtil.DBNullReplace( ds.Tables[0].Rows[0]["address"],"").ToString();
            msg.Email = Util.DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["email"], "").ToString();
            msg.Content = ds.Tables[0].Rows[0]["content"].ToString();
            msg.Reply = Util.DBUtil.DBNullReplace(ds.Tables[0].Rows[0]["reply"], "").ToString();
            msg.Time = (DateTime)ds.Tables[0].Rows[0]["time"];
            msg.Type =Convert.ToInt32( ds.Tables[0].Rows[0]["type"]);
            msg.Condition = (bool)ds.Tables[0].Rows[0]["condition"];
            return msg;
        }
    }
}
