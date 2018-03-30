using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Manager
    {
        private int managerid;

        public int Managerid
        {
            get { return managerid; }
            set { managerid = value; }
        }

        private String logname;

        public String Logname
        {
            get { return logname; }
            set { logname = value; }
        }


        private String truename;

        public String Truename
        {
            get { return truename; }
            set { truename = value; }
        }


        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }


        private int sex;

        public int Sex
        {
            get { return sex; }
            set { sex = value; }
        }


        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private String tel;

        public String Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }


        private int managertype;

        public int Managertype
        {
            get { return managertype; }
            set { managertype = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private string memo;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

    }
    public class ManagerType
    {
        private int sn;

        public int Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string memo;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
}
