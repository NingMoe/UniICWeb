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

public partial class _Default : UniWebLib.UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    private string szKindName = "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            UNITERM newTerm;
            GetHTTPObj(out newTerm);
            if (m_Request.Reserve.SetTerm(newTerm, out newTerm) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
      
        if (Request["op"] == "set")
        {
            bSet = true;

            TERMREQ vrTermGet = new TERMREQ();
            //vrTermGet.dwGetType = (uint)TERMREQ.DWGETTYPE.TERMGET_BYYEARTERM;
            vrTermGet.dwYearTerm =Parse(Request["dwID"]);
            UNITERM[] vrTermRes;
            if (m_Request.Reserve.GetTerm(vrTermGet, out vrTermRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {                
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrTermRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrTermRes[0]);
                    m_Title = "修改" + "【" + vrTermRes[0].szMemo + "】";
                }
            }
        }
        else
        {
            uint? uMax = 0;
            uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;
     
            if (GetMaxValue(ref uMax, uID, "dwSN"))
            {
                UNIDCS setValue = new UNIDCS();
                setValue.dwSN = uMax;
                PutJSObj(setValue);
            }
            m_Title = "新建学期";
        }
	}
}
