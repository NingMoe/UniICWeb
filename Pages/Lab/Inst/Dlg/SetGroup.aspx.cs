﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
    protected string m_szLabKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIGROUP setGroup;
        
        if (IsPostBack)
        {
            GetHTTPObj(out setGroup);
            if (m_Request.Group.SetGroup(setGroup, out setGroup) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            GROUPREQ vrGetLab = new GROUPREQ();
           // vrGetLab.dwGetType=(uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
            vrGetLab.dwGroupID=Parse((Request["dwID"]));
            UNIGROUP[] vtLab;
            if (m_Request.Group.GetGroup(vrGetLab, out vtLab) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtLab.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtLab[0]);
                    m_Title = "修改站点【" + vtLab[0].szName + "】";
                }
            }
        }
        else
        {

        }
    }
}
