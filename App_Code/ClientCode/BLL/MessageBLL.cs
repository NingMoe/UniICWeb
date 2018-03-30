using System;
using System.Collections.Generic;
using System.Text;
using SQLServerDAL;
using Model;

namespace BLL
{
    public class MessageBLL
    {
        private MessageDAL messageDAL =new MessageDAL();
        /// <summary>
        /// 获取留言的条数
        /// </summary>
        /// <param name="typeId">留言的类型，1是普通留言</param>
        /// <returns></returns>
        public int GetMessageAmount()
        {
            return messageDAL.GetMessageAmount();
        }

        public int GetMessageAmount(int typeId)
        {
            return messageDAL.GetMessageAmountByType(typeId);
        }
        /// <summary>
        /// 获取留言
        /// </summary>
        /// <param name="pageSize">显示的页条目数</param>
        /// <param name="pageIndex">显示的页索引，从1开始</param>
        /// <returns></returns>
        public System.Data.DataSet GetMessage( int pageSize, int pageIndex)
        {
            return messageDAL.GetMessage( pageSize, pageIndex);
        }
        /// <summary>
        /// 根据类型，获取留言
        /// </summary>
        /// <param name="pageSize">显示的页条目数</param>
        /// <param name="pageIndex">显示的页索引，从1开始</param>
        /// <returns></returns>
        public System.Data.DataSet GetMessageByType(int typeId, int pageSize, int pageIndex)
        {
            return messageDAL.GetMessageByType(typeId,pageSize,pageIndex);
        }

        public bool InsertMessage(Model.Message message)
        {
            return messageDAL.InsertMessage(message)==1;
        }


        public int CheckMessage(List<string> keys)
        {
            return messageDAL.UpdateMessage(keys);
        }

        public int ReplyMessage(string reply,string key)
        {
            return messageDAL.UpdateMessage(reply,key);
        }

        public Model.Message GetMessageById(string id)
        {
            return messageDAL.GetMessageById(id);
        }
    }
    public class Converter
    {
        public string TypeToStringConverter(int type)
        {
            string str = "";
            if (type==1)
            {
                str = "普通留言";
            }
            return str;
        }

    }
}
