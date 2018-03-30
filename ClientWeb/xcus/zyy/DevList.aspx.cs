using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;

public partial class _Default : UniClientPage
{
    protected string CampusList = "";
    protected string LabList = "";
    protected string ColList = "";
    protected string ClsList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        CampusList = GetCampus();
        LabList = GetLab();
        ColList = GetCollege();
        ClsList = GetDevCls();
        //ManagerList = GetManager();
    }

    private string GetDevCls()
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
                rel += "<option value='" + vtResult[i].dwClassID + "'>" + vtResult[i].szClassName + "</option>";
            }
            return rel;
        }
        return "";
    }
    string GetCampus()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        CAMPUSREQ vrGet = new CAMPUSREQ();
        UNICAMPUS[] vtResult;
        uResponse = m_Request.Account.CampusGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwCampusID + "'>" + vtResult[i].szCampusName + "</option>";
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
                rel += "<option value='" + vtResult[i].dwLabID + "'>" + vtResult[i].szLabName + "</option>";
            }
            return rel;
        }
        return "";
    }
    string GetCollege()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEPTREQ vrGet = new DEPTREQ();
        vrGet.dwKind = (uint)UNIDEPT.DWKIND.DEPTKIND_SCHOOL;
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
}