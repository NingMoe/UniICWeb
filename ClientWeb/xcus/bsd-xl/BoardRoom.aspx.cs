using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_BoardRoom : UniClientPage
{
    protected string clsList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_LAB))
        {
            Response.Redirect("Default.aspx");
        }
        MyCld.ClassKind = "2048";//筛选会议室
        GetClassList();
    }

    private void GetClassList()
    {
        uint classKind = (uint)UNIDEVCLS.DWKIND.CLSCOMMONS_MEETINGROOM;
        UNIDEVCLS[] rlt = GetDevCls(classKind);
        if (rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVCLS cls = rlt[i];
                clsList += "<li><a href='?classId=" + cls.dwClassID + "' classId='" + cls.dwClassID + "' onclick='selBoardRoom(this)'>" +
                    "<span class='ui-icon ui-icon-calculator'></span>" + cls.szClassName + "</a></li>";
            }
        }
    }
}