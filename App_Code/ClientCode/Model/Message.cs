using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace Model
{
    public class Message
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private String address;

        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private String content;

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private String reply;

        public String Reply
        {
            get { return reply; }
            set { reply = value; }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        private bool condition;

        public bool Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
