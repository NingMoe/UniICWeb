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

public partial class Page_Salon : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        base.LoadPage();
		
        if (Request["act"] == "res")
        {
            uint nGroupID = 0;
            uint.TryParse(Request["gid"], out nGroupID);
            string szPurpose = Request["purpose"];
            //TODO:加入组成员。

			//$out = SetGroupMember($_SESSION['SOAP']['AccNo'],$_SESSION['SOAP']['Name'],$_GET['gid']);
			//Response.Write("{\"MsgId\":1,\"Message\":\"未实现\"}");
            
                string groupID = nGroupID.ToString();          
          
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                GROUPREQ vrGet = new GROUPREQ();
                //vrGet.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
                //vrGet.szGetKey = groupID;
                vrGet.dwGroupID = ToUInt(groupID);
                UNIGROUP[] vtGroup;
                uResponse = m_Request.Group.GetGroup(vrGet, out vtGroup);

                if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtGroup == null || vtGroup.Length == 0)
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"还没指定成员\"}");                            
                    return;
                }
                object obj = Session["LOGIN_ACCINFO"];
                if (obj == null)
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"未登录或已超时\"}");
                    return;
                }
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)obj;
                GROUPMEMBER setGroupMember = new GROUPMEMBER();
                setGroupMember.dwGroupID = vtGroup[0].dwGroupID;
                setGroupMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
                setGroupMember.dwMemberID = vrAccInfo.dwAccNo;
                setGroupMember.szName = vrAccInfo.szTrueName.ToString();
                setGroupMember.szMemo = vrAccInfo.szLogonName.ToString() + ":" + vrAccInfo.szTrueName.ToString();
                if (szPurpose != null && szPurpose == "in")
                {
                    uResponse = m_Request.Group.SetGroupMember(setGroupMember);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"报名成功\"}");
                    }
                    else
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage.ToString()+ "\"}");
                    }
                }
                else if (szPurpose != null && szPurpose == "out")
                {
                    uResponse = m_Request.Group.DelGroupMember(setGroupMember);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"退出成功\"}");
                    }
                }
            

        }
    }
}
