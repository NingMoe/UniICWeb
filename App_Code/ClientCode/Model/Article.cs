using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Article
    {
            private int infoid;

            public int Articleid
            {
                get { return infoid; }
                set { infoid = value; }
            }
            private int infoclass;

            public int Articleclass
            {
                get { return infoclass; }
                set { infoclass = value; }
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
                get { return content; }
                set { content = value; }
            }
            private string img1;

            public string Img1
            {
                get { return img1; }
                set { img1 = value; }
            }
            private string img2;

            public string Img2
            {
                get { return img2; }
                set { img2 = value; }
            }
            private string img3;

            public string Img3
            {
                get { return img3; }
                set { img3 = value; }
            }
            private string author;

            public string Author
            {
                get { return author; }
                set { author = value; }
            }
            private int happendate;

            public int Happendate
            {
                get { return happendate; }
                set { happendate = value; }
            }
            private int credate;

            public int Credate
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
}
