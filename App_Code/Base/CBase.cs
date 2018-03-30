using System;
using System.Data;
using System.Configuration;
using System.Text;

/// <summary>
/// CBase 的摘要说明
/// </summary>

public struct PARAM_EXT
{
    public int nStartLine;
    public int nNeedLine;
}

public class CBase
{
    static public int UserCheck(CHECKUSER check, out USER result)
    {
        result = null;
        DataTable dt = CDB.GetSQLData("select top 1 * from tblUser where szLogonName = '" + check.szLogonName + "'");
        if (dt == null)
        {
            return 1;
        }
        if (dt.Rows.Count == 0)
        {
            return 1;
        }
        if ((string)dt.Rows[0]["szPassword"] != check.szPassword)
        {
            return 1;
        }
        result = new USER();

        result.dwUserID = (int)dt.Rows[0]["dwUserID"];
        result.szLogonName = (string)dt.Rows[0]["szLogonName"];
        result.szTrueName = (string)dt.Rows[0]["szTrueName"];
        result.dwType = (int)dt.Rows[0]["dwType"];
        return 0;
    }

    static public int GetUser(GETUSER getUser, out USER[] result)
    {
        result = null;
        DataTable dt = CDB.GetSQLData("select top " + getUser.ext.nNeedLine + " * from tblUser");
        if (dt == null)
        {
            return 1;
        }
        result = new USER[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result[i] = new USER();
            result[i].dwUserID = (int)dt.Rows[i]["dwUserID"];
            result[i].szLogonName = (string)dt.Rows[i]["szLogonName"];
            result[i].szTrueName = (string)dt.Rows[i]["szTrueName"];
            result[i].dwType = (int)dt.Rows[i]["dwType"];
        }
        return 0;
    }

    //-------------------------
    static public int GetModules(out MODULES[] result)
    {
        result = null;
        DataTable dt = CDB.GetSQLData("select * from tblModule");
        if (dt == null)
        {
            return 1;
        }
        result = new MODULES[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result[i] = new MODULES();
            result[i].dwModuleID = (int)dt.Rows[i]["dwModuleID"];
            result[i].szModuleName = (string)dt.Rows[i]["szModuleName"];
        }
        return 0;
    }
    static public int GetClasses(int dwModuleID, out CLASS[] result)
    {
        result = null;
        DataTable dt = CDB.GetSQLData("select * from tblClass where dwModuleID=" + dwModuleID);
        if (dt == null)
        {
            return 1;
        }
        result = new CLASS[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result[i] = new CLASS();
            result[i].dwClsID = (int)dt.Rows[i]["dwClsID"];
            result[i].dwModuleID = (int)dt.Rows[i]["dwModuleID"];
            result[i].szClassName = (string)dt.Rows[i]["szClsName"];
        }
        return 0;
    }

    static public int GetClassDetail(int dwClsID, out CLASS result)
    {
        result = null;
        DataTable dt = CDB.GetSQLData("select * from tblClass where dwClsID=" + dwClsID);
        if (dt == null)
        {
            return 1;
        }
        result = new CLASS();
        if(dt.Rows.Count > 0)
        {
            result.dwClsID = (int)dt.Rows[0]["dwClsID"];
            result.dwModuleID = (int)dt.Rows[0]["dwModuleID"];
            result.szClassName = (string)dt.Rows[0]["szClsName"];
            return 0;
        }
        return 1;
    }

    static public int NewClass(CLASS newcls)
    {
        DataTable dt = null;
        if (newcls.dwClsID != 0)
        {
            dt = CDB.GetSQLData("update tblClass set dwModuleID=" + newcls.dwModuleID + " ,szClsName='" + newcls.szClassName + "' where dwClsID=" + newcls.dwClsID);
        }
        else
        {
            dt = CDB.GetSQLData("insert into tblClass (dwModuleID,szClsName) values (" + newcls.dwModuleID + ",'" + newcls.szClassName + "')");
        }
        if (dt == null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    static public int DelClass(int dwClsID)
    {
        DataTable dt = CDB.GetSQLData("delete from tblNews where dwClsID=" + dwClsID);
        dt = CDB.GetSQLData("delete from tblClass where dwClsID=" + dwClsID);
        if (dt == null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    static public NEWS GetNewsDetail(int dwNewsID)
    {
        NEWS result = null;
        DataTable dt = CDB.GetSQLData("select top 1 * from tblNews where dwNewsID=" + dwNewsID);
        if (dt == null || dt.Rows.Count == 0)
        {
            return result;
        }
        result = new NEWS();
        result.dwNewsID = (int)dt.Rows[0]["dwNewsID"];
        result.dwDate = (int)dt.Rows[0]["dwDate"];
        result.dwTime = (int)dt.Rows[0]["dwTime"];
        result.dwClsID = (int)dt.Rows[0]["dwClsID"];
        result.dwType = (int)dt.Rows[0]["dwType"];
        result.szTitle = (string)dt.Rows[0]["szTitle"];
        result.szContent = (string)dt.Rows[0]["szContent"];
        return result;
    }

    static public int GetNews(GETNEWS getnews, out NEWS[] result)
    {
        result = null;
        if (getnews.ext.nNeedLine == 0) getnews.ext.nNeedLine = 10;
        DataTable dt = CDB.GetSQLData("select top " + getnews.ext.nNeedLine + " * from tblNews where dwDate >= " + getnews.dwStartDate + " and dwDate <= " + getnews.dwEndDate + " and dwClsID=" + getnews.dwClsID);
        if (dt == null)
        {
            return 1;
        }
        result = new NEWS[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result[i] = new NEWS();
            result[i].dwNewsID = (int)dt.Rows[i]["dwNewsID"];
            result[i].dwDate = (int)dt.Rows[i]["dwDate"];
            result[i].dwTime = (int)dt.Rows[i]["dwTime"];
            result[i].dwClsID = (int)dt.Rows[i]["dwClsID"];
            result[i].dwType = (int)dt.Rows[i]["dwType"];
            result[i].szTitle = (string)dt.Rows[i]["szTitle"];
            result[i].szContent = (string)dt.Rows[i]["szContent"];
        }
        return 0;
    }

    static public int NewNews(NEWS newnews)
    {
        DataTable dt = null;
        if (newnews.dwNewsID == 0)
        {
            dt = CDB.GetSQLData("insert into tblNews (dwClsID,dwDate,dwTime,dwType,szTitle,szContent) values (" + newnews.dwClsID + "," + newnews.dwDate + "," + newnews.dwTime + "," + newnews.dwType + ",'" + newnews.szTitle + "','" + newnews.szContent + "')");
        }
        else
        {
            dt = CDB.GetSQLData("update tblNews set szTitle='" + newnews.szTitle + "' ,szContent='" + newnews.szContent + "'  where dwNewsID =" + newnews.dwNewsID);
        }
        if (dt == null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    static public int DelNews(int dwNewsID)
    {
        DataTable dt = CDB.GetSQLData("delete from tblNews where dwNewsID=" + dwNewsID);
        if (dt == null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
