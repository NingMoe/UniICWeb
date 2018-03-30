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
        string type = Request["dwDCSKind"];
        if (type == "1")
        {
            szKindName = "集控器";
        }
        else if (type == "2")
        {
            szKindName = "摄像机";
        }
        if (IsPostBack)
        {
            UNIDCS newDCS;
            GetHTTPObj(out newDCS);
            if (m_Request.DoorCtrlSrv.SetDCS(newDCS, out newDCS) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }

        STATIONREQ vrGetSta = new STATIONREQ();
        UNISTATION[] vrResult;
        vrGetSta.dwGetType = (uint)STATIONREQ.DWGETTYPE.STATIONGET_BYALL;
        if (m_Request.Station.GetStation(vrGetSta, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szSta += "<option value='" + vrResult[i].dwStaSN + "'>" + vrResult[i].szStaName + "</option>";
            }
        }

        if (Request["op"] == "set")
        {
            bSet = true;

            DCSREQ vrGetDCS = new DCSREQ();
            vrGetDCS.dwGetType = (uint)DCSREQ.DWGETTYPE.DCSGET_BYSN;
            vrGetDCS.szGetKey = Request["dwSN"];
            vrGetDCS.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
            UNIDCS[] vrResultStation;
            if (m_Request.DoorCtrlSrv.GetDCS(vrGetDCS, out vrResultStation) != REQUESTCODE.EXECUTE_SUCCESS)
            {                
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrResultStation.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrResultStation[0]);
                    m_Title = "修改" + szKindName + "【" + vrResultStation[0].szName + "】";
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
            m_Title = "新建" + szKindName;
        }
	}
}
