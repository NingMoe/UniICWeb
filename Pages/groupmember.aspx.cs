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
using Newtonsoft.Json;
public partial class _Default : UniPage
{
    protected string szTable = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szActivitySN = Request["sn"];

        ACTIVITYPLANREQ req = new ACTIVITYPLANREQ();
        req.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        req.szGetKey = szActivitySN;
        UNIACTIVITYPLAN[] res;
        m_Request.m_UniDCom.SessionID = (uint)Session["SessionID"];
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Reserve.GetActivityPlan(req, out res) == REQUESTCODE.EXECUTE_SUCCESS && res != null && res.Length > 0)
        {
            uint uGroupID = (uint)res[0].dwGroupID;
            GROUPMEMDETAILREQ memberReq = new GROUPMEMDETAILREQ();
            memberReq.dwGroupID = uGroupID;
            GROUPMEMDETAIL[] vtMember;
            m_Request.m_UniDCom.SessionID = (uint)Session["SessionID"];
            m_Request.m_UniDCom.StaSN = 1;
            if (m_Request.Group.GetGroupMemDetail(memberReq, out vtMember) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (vtMember != null)
                {
                    for (int i = 0; i < vtMember.Length; i++)
                    {
                        szTable = "<tr>" + "<td>" + vtMember[i].szTrueName + "</td>" + "<td>" + vtMember[i].szPID + "</td>" + "</tr>";
                    }
                }
             
                
            }
            else {
                Response.Write(m_Request.szErrMessage);
                Response.End();
            }
        }
    }

}