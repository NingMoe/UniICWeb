using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_LabUseRole : UniClientPage
{
    protected string labRoleList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!LoadPage())
        {
            ClientRedirect("Default.aspx");
            return;
        }
        InitLabRole();
    }
    private void InitLabRole()
    {
        SFROLEINFO[] rlt = GetUseRole();
        if (rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                SFROLEINFO role = rlt[i];
                labRoleList += "<tr><td>" + role.szLabName + "</td><td>" + Util.Converter.GetRoleState(role.dwStatus) + "</td><td>" + applyUseAct(role.dwStatus, role) + "</td></tr>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
    //使用资格申请操作
    static public string applyUseAct(uint? sta, SFROLEINFO role)
    {
        string ret = "";
        if ((sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_NOAPPLY) > 0 || (sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
        {
            ret = "[<span class='click' onclick='applyLabUseRole(" + role.dwLabID + "," + role.dwSFRuleID + "," + role.dwApplyID + ",\"" + role.szLabName + "\")'>申请资格</span>]";
        }
        if (((sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0 || (sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT) > 0) && !string.IsNullOrEmpty(role.szCheckInfo))
        {
            string msg = "[<span class='click' onclick='uni.msgBox(\"" + role.szCheckInfo + "\")'>审核反馈</span>]";
            ret = "<div>" + msg + ret + "</div>";
        }
        return ret;
    }
}