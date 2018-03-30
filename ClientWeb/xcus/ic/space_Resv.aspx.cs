using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;

public partial class Page_ : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (!this.Page.IsPostBack)
        {
            uint cls_kind = 0;
            string clsKind = ConfigurationManager.AppSettings["icClsKind"];
            if (clsKind.IndexOf("common") >= 0)
                cls_kind+=(uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            if (clsKind.IndexOf("seat") >= 0)
                cls_kind+=(uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            if (clsKind.IndexOf("loan") >= 0)
                cls_kind+=(uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
            if (clsKind.IndexOf("computer") >= 0)
                cls_kind+=(uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            REQUESTCODE uResponse;
            DEVCLSREQ vrGet = new DEVCLSREQ();
            vrGet.dwKind = cls_kind;
            UNIDEVCLS[] vtRes;
            uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
            if (vtRes != null && vtRes.Length > 0)
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                    {
                        continue;
                    }
                    ListItem item = new ListItem(vtRes[i].szClassName.ToString(), vtRes[i].dwClassID.ToString());
                    ddlDevClass.Items.Add(item);
                }
                GetDevCls(vtRes[0].dwClassID.ToString());
                //ListItem itemAll = new ListItem("全部", "0");
                //ddlDevClass.Items.Add(itemAll);
                //ddlDevClass.SelectedValue = "0";
                //MyCld.DevClassId = vtRes[0].dwClassID.ToString();
                //MyCld.ClassKind = vtRes[0].dwKind.ToString();
                //if (vtRes[0].dwClassID.ToString() == "610")//长期预约
                //    MyCld.IsLong = true;
                //Session["szBackPage"] = "space_Resv.aspx?kindid=0";
            }
        }
    }
    protected void ddlDevClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDevCls(ddlDevClass.SelectedValue.ToString());
    }
    void GetDevCls(string id)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        string szClassID = id;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        vrGet.dwClassID = ToUInt(szClassID);
        UNIDEVCLS[] vtDevClass;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtDevClass);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDevClass != null && vtDevClass.Length > 0)
        {
            MyCld.ClassKind = vtDevClass[0].dwKind.ToString();
            MyCld.DevClassId = szClassID;
            MyCld.IsLong = false;
            //长期预约
            DEVKINDREQ req = new DEVKINDREQ();
            req.szClassName = vtDevClass[0].szClassName;
            UNIDEVKIND[] rlt;
            if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                {
                    MyCld.IsLong = true;
                }
            }
        }
        //Session["szBackPage"] = "space_Resv.aspx?ClassKindID=" + ddlDevClass.SelectedValue.ToString();
    }
}
