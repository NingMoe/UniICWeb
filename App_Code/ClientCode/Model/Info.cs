using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class InfoItem
    {
        private int infoid;

        public int Infoid
        {
            get { return infoid; }
            set { infoid = value; }
        }
        private int infoclass;

        public int Infoclass
        {
            get { return infoclass; }
            set { infoclass = value; }
        }
        private int infogroup;

        public int Infogroup
        {
            get { return infogroup; }
            set { infogroup = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string subhead;

        public string Subhead
        {
            get { return subhead; }
            set { subhead = value; }
        }
        private string summary;

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        private string content;

        public string Content
        {
            get {
                if (content == null) content = "";
                return content; }
            set { content = value; }
        }
        private string ext1;

        public string Ext1
        {
            get { return ext1; }
            set { ext1 = value; }
        }
        private string ext2;

        public string Ext2
        {
            get { return ext2; }
            set { ext2 = value; }
        }
        private string ext3;

        public string Ext3
        {
            get { return ext3; }
            set { ext3 = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private Int64 happendate;

        public Int64 Happendate
        {
            get { return happendate; }
            set { happendate = value; }
        }
        private Int64 credate;

        public Int64 Credate
        {
            get { return credate; }
            set { credate = value; }
        }
        private int infostatus;

        public int Infostatus
        {
            get { return infostatus; }
            set { infostatus = value; }
        }
        private string memo;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
        private int hitcount;

        public int Hitcount
        {
            get { return hitcount; }
            set { hitcount = value; }
        }
    }
    public class ModelClass
    {
        private int sn;

        public int Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private int group;

        public int Group
        {
            get { return group; }
            set { group = value; }
        }
        private string groupTitle;

        public string GroupTitle
        {
            get { return groupTitle; }
            set { groupTitle = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string memo;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
    public class ModelGroup
    {
        private int sn;

        public int Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string memo;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
}
