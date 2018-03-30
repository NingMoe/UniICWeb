using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
   {
        string szTerm = Request["term"];
        string szType = Request["type"];
        string szIdent = Request["ident"];
        string isTutor = Request["tutor"];
        string isLeader=Request["leader"];
        Response.CacheControl = "no-cache";

        ACCREQ vrGet = new ACCREQ();
        UNIACCOUNT[] rlt;
        if (szType == null || szType == "")//若未定义则先检索姓名再检索登录名
        {
            vrGet.szTrueName = szTerm;
        }
        else if (szType.ToLower() == "logonname")
        {
            vrGet.szPID = szTerm;
        }
        else if (szType.ToLower() == "truename")
        {
            vrGet.szTrueName = szTerm;
        }

        uint dwIdent = ToUInt(szIdent);
        if (dwIdent != 0)
        {
            vrGet.dwIdent = dwIdent;
        }
         if (isTutor == "true")
        {
            vrGet.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
        }
        vrGet.szReqExtInfo.dwNeedLines = 15; //最多10条
        string searchAccLogonName = GetConfig("searchAccLogonName");
        if (m_Request.Account.Get(vrGet, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            if (string.IsNullOrEmpty(szType)&&rlt.Length==0)
            {
                szType = "logonname";
              
                if(searchAccLogonName!=null&& searchAccLogonName=="1")
                {
                    vrGet.szLogonName = szTerm;
                }
                else { 
                vrGet.szPID = szTerm;
                }
                vrGet.szTrueName = null;
                if (m_Request.Account.Get(vrGet, out rlt) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Response.Write("[{}]");
                    return;
                }
            }
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < rlt.Length; i++)
            {
                if (searchAccLogonName != null && searchAccLogonName == "1")
                {
                    if (string.IsNullOrEmpty(szType) || szType == "truename")
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + rlt[i].szLogonName + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                    else
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + rlt[i].szLogonName + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                }
                else if (searchAccLogonName != null && searchAccLogonName == "2")
                {
                    if (string.IsNullOrEmpty(szType) || szType == "truename")
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + (rlt[i].szTrueName) + "(" + (rlt[i].szLogonName) + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                    else
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + (rlt[i].szTrueName) + "(" + (rlt[i].szLogonName) + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                }
                else if (searchAccLogonName != null && searchAccLogonName == "3")
                {
                    if (string.IsNullOrEmpty(szType) || szType == "truename")
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + replaceName(rlt[i].szLogonName) + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                    else
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + replaceName(rlt[i].szLogonName) + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                }
                else {
                    if (string.IsNullOrEmpty(szType) || szType == "truename")
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + rlt[i].szLogonName + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                    else
                    {
                        szOut += "{\"id\":\"" + rlt[i].dwAccNo + "\",\"name\": \"" + replaceName(rlt[i].szTrueName) + "\",\"label\": \"" + replaceName(rlt[i].szTrueName) + "(" + rlt[i].szLogonName + ")" + "\",\"szLogonName\": \"" + rlt[i].szLogonName + "\",\"szHandPhone\": \"" + rlt[i].szHandPhone + "\",\"szTel\": \"" + rlt[i].szTel + "\",\"szEmail\": \"" + rlt[i].szEmail + "\"}";
                    }
                }
                if (i < rlt.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[{}]");
        }
    }
    string replaceName(string name)
    {
        if (name.Length > 1)
        {
            return name.Substring(0, name.Length - 1) + "*";
        }
        else
        {
            return name;
        }
    }
        
}