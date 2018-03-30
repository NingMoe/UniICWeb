using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Collections;
public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szName = Request["name"];
        string szID =Request["id"];
        uint uID=Parse(szID);
        string szOP = Request["op"];
        Response.CacheControl = "no-cache";
        if (szOP==null||szOP.ToLower() != "get")
        {
            POLLITEM[] itemList = (POLLITEM[])Session["POLLITEM"];
            if (itemList == null)
            {
                POLLITEM[] ListTemp = new POLLITEM[1];
                ListTemp[0] = new POLLITEM();
                ListTemp[0].szItemName = szName;
                Session["POLLITEM"] = ListTemp;
            }
            else
            {
                POLLITEM temp = new POLLITEM();
                temp.szItemName = szName;
                int len = itemList.Length;

                bool bAdd = true;
                ArrayList list = new ArrayList();
                for (int i = 0; i < len; i++)
                {
                    if (szOP == "del")
                    {
                        if (!((szName == itemList[i].szItemName) || (itemList[i].dwItemID != null && uID != 0 && uID == (uint)itemList[i].dwItemID)))
                        {
                            list.Add(itemList[i]);
                        }
                    }
                    else if (itemList[i].szItemName == szName)
                    {
                        bAdd = false;
                    }
                }
                if (szOP == "del")
                {
                    POLLITEM[] itemListNew = new POLLITEM[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        itemListNew[i] = new POLLITEM();
                        POLLITEM tempDel = (POLLITEM)list[i];
                        itemListNew[i].dwItemID = tempDel.dwItemID;
                        itemListNew[i].szItemName = tempDel.szItemName;
                    }
                    Session["POLLITEM"] = itemListNew;
                }
                else
                {
                    if (bAdd)
                    {
                        POLLITEM[] itemListNew = new POLLITEM[len + 1];
                        for (int i = 0; i < len; i++)
                        {
                            itemListNew[i] = new POLLITEM();
                            itemListNew[i] = itemList[i];
                        }
                        itemListNew[len] = new POLLITEM();
                        itemListNew[len] = temp;
                        Session["POLLITEM"] = itemListNew;
                    }
                    else
                    {
                        Session["POLLITEM"] = itemList;
                    }
                }
            }
        }

        POLLITEM[] vtRes = (POLLITEM[])Session["POLLITEM"];
        MyString szOut = new MyString();
        szOut += "[";
        for (int i = 0; vtRes!=null&&i < vtRes.Length; i++)
        {
            if (vtRes[i].szItemName != null)
            {
                uint uTestItemID = 0;
                if (vtRes[i].dwItemID != null)
                {
                    uTestItemID  = (uint)vtRes[i].dwItemID;
                }
                szOut += "{\"id\":\"" +uTestItemID.ToString() + "\",\"label\": \"" + vtRes[i].szItemName + "\"}";
                if (i < vtRes.Length - 1)
                {
                    szOut += ",";
                }
            }
        }
        szOut += "]";
        Response.Write(szOut);
    }

}