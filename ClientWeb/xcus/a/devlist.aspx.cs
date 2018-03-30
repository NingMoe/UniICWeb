using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_devlist : UniClientPage
{
    protected string ClsList = "";
    protected string LabList = "";
    protected string CampusList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        CampusList = GetCampus();
        ClsList = GetCls();
        LabList = GetLab();
    }
    string GetCls()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        UNIDEVCLS[] vtResult;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<a class='it' value=\"" + vtResult[i].dwClassID + "\"><input type='checkbox'  /> " + vtResult[i].szClassName + "</a>";
            }
            return rel;
        }
        return "";
    }
    string GetLab()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        LABREQ vrGet = new LABREQ();
        UNILAB[] vtResult;
        uResponse = m_Request.Device.LabGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                if (vtResult[i].dwLabID == 31)
                {
                    continue;
                }
                rel += "<a class='it' value=\"" + vtResult[i].dwLabID + "\"><input type='checkbox'  /> " + vtResult[i].szLabName + "</a>";
            }
            return rel;
        }
        return "";
    }
    string GetDept()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEPTREQ vrGet = new DEPTREQ();
        UNIDEPT[] vtResult;
        uResponse = m_Request.Account.DeptGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwID + "'>" + vtResult[i].szName + "</option>";
            }
            return rel;
        }
        return "";
    }
    string GetManager()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.dwIdent = (int)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
        UNIACCOUNT[] vtResult;
        uResponse = m_Request.Account.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwAccNo + "'>" + vtResult[i].szTrueName + "</option>";
            }
            return rel;
        }
        return "";
    }
    string GetCampus()
    {
        string ret = "";
        CAMPUSREQ req = new CAMPUSREQ();
        UNICAMPUS[] rlt;
        if (m_Request.Account.CampusGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                ret += "<a class='it' value=\"" + rlt[i].dwCampusID + "\"><input type='checkbox'  /> " + rlt[i].szCampusName + "</a>";
            }
        }
        return ret;
    }
}