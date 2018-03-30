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

public partial class _Default :UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szExt = "";
    protected string szLogonName = "";
    protected string szTruename = "";
    protected uint uTrCount = 8;
    protected void Page_Load(object sender, EventArgs e)
    {

        ADMINREQ vrParameter = new ADMINREQ();
        UNIADMIN[] vrResult;
        vrParameter.dwAccNo = Parse(Request["dwID"]);
        if (m_Request.Admin.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            szLogonName=vrResult[0].szLogonName;
            szTruename = vrResult[0].szTrueName;
            uint uManrole = (uint)vrResult[0].dwManRole;
            if ((uManrole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LABCTR) > 0)
            {
                m_szExt = "管理所有房间";
            }
            else if ((uManrole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LAB) > 0)
            {
                UNILAB[] vtLab = GetAllLab();
                uint uCount = 0;
                m_szExt = "<table class='ListTbl'>";
                string szTd = "";
                for (int i=0;i<vtLab.Length;i++)
                {
                    if (vtLab.Length < uTrCount)
                    {
                        szTd += "<td>" + vtLab[i].szLabName.ToString() + "</td>";
                    }
                    else{
                        if (IsInGroupPersonMember(vtLab[i].dwManGroupID, vrParameter.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL))
                        {
                            szTd += "<td>" + vtLab[i].szLabName.ToString() + "</td>";
                            if (uCount > 1 && uCount % uTrCount == 0)
                            {
                                m_szExt = m_szExt + "<tr>" + szTd + "</tr>";
                                szTd = "";
                            }
                            else
                            {
                                if (i == (vtLab.Length - 1))
                                {
                                    m_szExt = m_szExt + "<tr>" + szTd + "</tr>";
                                    szTd = "";
                                }
                            }
                        }
                        uCount = uCount + 1;
                    }
                }
                if (vtLab.Length < uTrCount)
                {
                    m_szExt += "<tr>" + szTd + "</tr>";
                }
                m_szExt += "</table>";
            }
            else if ((uManrole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM) > 0)
            {
                UNIROOM[] vtRoom = GetAllRoom();
                uint uCount = 0;
                m_szExt = "<table class='ListTbl'>";
                string szTd= "";
                for (int i = 0; i < vtRoom.Length; i++)
                {
                   
                    if (IsInGroupPersonMember(vtRoom[i].dwManGroupID, vrParameter.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL))
                    {
                        if (vtRoom.Length < uTrCount)
                        {
                            szTd += "<td>" + vtRoom[i].szRoomName.ToString() + "</td>";
                        }
                        else
                        {
                            szTd += "<td>" + vtRoom[i].szRoomName.ToString() + "</td>";
                            if (uCount > 1 && uCount % uTrCount == 0)
                            {
                                m_szExt = m_szExt + "<tr>" + szTd + "</tr>";
                                szTd = "";
                            }
                            else
                            {
                                if (i == (vtRoom.Length - 1))
                                {
                                    m_szExt = m_szExt + "<tr>" + szTd + "</tr>";
                                    szTd = "";
                                }

                            }
                        }
                        uCount = uCount + 1;
                    }
                }
                if (vtRoom.Length < uTrCount)
                {
                    m_szExt += "<tr>" + szTd + "</tr>";
                }
                m_szExt += "</table>";
            }
        }
        m_Title = "查看管理范围";
    }
}
