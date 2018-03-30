using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;

public partial class _Default : UniClientPage
{
    protected string ClsList = "";
    protected string LabList = "";
    protected string ColList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        ClsList = GetCls();
        LabList = GetLab();
        //ColList = GetCollege();
        //ManagerList = GetManager();
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
                //临时 过来掉非设备类别
                if (vtResult[i].dwClassID == 54) continue;//临时 

                rel += "<span><a name=\"" + vtResult[i].dwClassID + "\"><input type='checkbox'  /> " + vtResult[i].szClassName + "</a></span>";
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
                rel += "<span><a name=\"" + vtResult[i].dwLabID + "\"><input type='checkbox'  /> " + vtResult[i].szLabName + "</a></span>";
            }
            return rel;
        }
        return "";
    }
    string GetCollege()
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
}