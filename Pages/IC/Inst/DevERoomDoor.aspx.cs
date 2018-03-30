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
using UniWebLib;

public partial class Sub_Course : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string m_szCamp = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevCls = "";
    protected string m_szDevKind = "";
    protected string m_szDevStat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        FULLROOMREQ vrParameter = new FULLROOMREQ();
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        FULLROOM[] vrResult;
        uResponse= m_Request.Device.FullRoomGet(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<td >" + vrResult[i].szRoomName + "</td>"; 
                uint uRunState = 0;
                if (vrResult[i].dwStatus != null)
                {
                    uRunState = (uint)vrResult[i].dwStatus;
                }
                string szRoomState = GetJustName(uRunState, "Unidcs_dwStatusDev");
                m_szOut += "<td >" + szRoomState + "</td>"; 
                m_szOut += "<td >" +vrResult[i].dwTotalDevNum+ "</td>";   
                m_szOut += "<td class='thCenter'><div class=''></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        string szlab = Request["lab"];
        string szCampus = Request["campus"];
        string szRoom = Request["szRoom"];
        string szDevCls = Request["szDevCls"];
        string szDevKinds = Request["szDevKinds"];
        if (szlab != null && szlab != "")
        {
            PutMemberValue2("lab", szlab);
        }
        if (szCampus != null && szCampus != "")
        {
            PutMemberValue2("campus", szCampus);
        }
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
        if (szDevCls != null && szDevCls != "")
        {
            PutMemberValue2("szDevCls", szDevCls);
        }
        if (szDevKinds != null && szDevKinds != "")
        {
            PutMemberValue2("szDevKinds", szDevKinds);
        }
    }   
}
