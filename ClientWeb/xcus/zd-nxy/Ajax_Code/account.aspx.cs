using System;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Xml;
using UniWebLib;
using UniStruct;
using Util;
using Newtonsoft.Json;

public partial class Page_Account : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        base.LoadPage();
        if (Request["act"] == "login")
        {
            if (common.Login(Request["id"], Request["pwd"]))
            {
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                if (vrAccInfo.szEmail.ToString().Trim() == "" || vrAccInfo.szHandPhone.ToString().Trim() == "")
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"新用户请先激活！\"}");
                    common.ClearLogin();
                }
                else
                {
                    Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "dlogin")
        {
            if (Session["Vnumber"] == null)
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"验证码超时，请重新输入验证码！\"}");
                return;
            }
            string str = Session["Vnumber"].ToString();
            string number = Request["number"];
            if (str == null || number != str)
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"验证码不正确！\"}");
                return;
            }
            if (common.Login(Request["d_id"], Request["d_pwd"]))
            {
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                if (vrAccInfo.szEmail.ToString().Trim() == "" || vrAccInfo.szHandPhone.ToString().Trim() == "")
                {
                    Response.Write("{\"MsgId\":2,\"Message\":\"新用户请先激活！\"}");
                    common.ClearLogin();
                }
                else
                {
                    Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "islg")
        {
            if (IsLogined())
            {
                Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"\"}");
            }
        }
        else if (Request["act"] == "act")
        {
            if (common.Login(Request["id"], Request["pwd"]))
            {
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                UNIACCOUNT vrParameter = new UNIACCOUNT();
                vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                vrParameter.szLogonName = vrAccInfo.szLogonName;
                vrParameter.szHandPhone = Request["phone"];
                vrParameter.szEmail = Request["mail"];
                if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    vrAccInfo.szHandPhone = vrParameter.szHandPhone;
                    vrAccInfo.szEmail = vrParameter.szEmail;
                    Session["LOGIN_ACCINFO"] = vrAccInfo;
                    Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                }
                else
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "update")
        {
            if (Session["LOGIN_ACCINFO"] != null)
            {
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                UNIACCOUNT vrParameter = new UNIACCOUNT();
                vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                vrParameter.szLogonName = vrAccInfo.szLogonName;
                vrParameter.szHandPhone = Request["phone"];
                vrParameter.szEmail = Request["mail"];
                if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    vrAccInfo.szHandPhone = vrParameter.szHandPhone;
                    vrAccInfo.szEmail = vrParameter.szEmail;
                    Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                }
                else
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"未登录\"}");
            }
        }
        else if (Request["act"] == "logout")
        {
            if (Session["LOGIN_ACCINFO"] != null)
            {
                UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
                ADMINLOGOUTRES vrResult;
                vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                vrParameter.szLogonName = vrAccInfo.szLogonName;
                m_Request.Admin.Logout(vrParameter, out vrResult);
            }
            common.ClearLogin();
            Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
        }
        else if (Request["act"] == "check")
        {
            ACCREQ vrParameter = new ACCREQ();
            UNIACCOUNT[] vrResult;
            vrParameter.szPID = Request["id"];
            if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "addmember")
        {
            ACCREQ vrParameter = new ACCREQ();
            UNIACCOUNT[] vrResult;
            string id = Request["id"].ToString();
            vrParameter.szPID = id;
            if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (vrResult.Length == 1)
                {
                    //CUniStruct<GROUPMEMBER setGroupMember = new CUniStruct<GROUPMEMBER>();
                    //setGroupMember = new CUniStruct<GROUPMEMBER>();
                    //setGroupMember.dwGroupID = new UniDW(Convert.ToUInt32(Request["groupId"]));
                    //setGroupMember.dwKind = new UniDW((uint)GROUPMEMBER_CONST.MEMBERKIND_PERSONAL);
                    //setGroupMember.dwMemberID = vrResult[0].dwAccNo;
                    //setGroupMember.szName = vrResult[0].szTrueName;
                    //setGroupMember.szMemo = new UniSZ(vrResult[0].szLogonName.ToString() + ":" + vrResult[0].szTrueName.ToString());
                    //if (m_Request.Group.SetGroupMember(setGroupMember) == REQUESTCODE.EXECUTE_SUCCESS)
                    //{
                    Response.Write("{\"ret\":1,\"name\":\"" + vrResult[0].szTrueName + "\"}");
                    //}
                    //else
                    //{
                    //    Response.Write("{\"ret\":0,\"msg\":\"添加成员失败！\"}");
                    //}
                }
                else
                {
                    Response.Write("{\"ret\":0,\"msg\":\"请确认输入的帐号是否正确完整！\"}");
                }
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "getleader")
        {
            ACCREQ vrParameter = new ACCREQ();
            UNIACCOUNT[] vrResult;
            string id = Request["id"].ToString();
            vrParameter.szPID = id;
            if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (vrResult.Length == 1)
                {
                    Response.Write("{\"ret\":1,\"get_leader\":\"" + vrResult[0].szTrueName + "\",\"get_leader_acc\":\"" + vrResult[0].dwAccNo + "\",\"get_leader_lgname\":\"" + vrResult[0].szLogonName + "\"}");
                }
                else
                {
                    Response.Write("{\"ret\":0,\"msg\":\"请确认输入的帐号是否完整！\"}");
                }
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "gettutor")
        {
            if (string.IsNullOrEmpty(Request["tutor"]))
            {
                return;
            }
            ACCREQ vrParameter = new ACCREQ();
            UNIACCOUNT[] vrResult;
            vrParameter.szTrueName = Request["tutor"];
            vrParameter.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
            if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
            {
                if (vrResult.Length >0)
                {
                        string str = JsonConvert.SerializeObject(vrResult);
                    Response.Write("{\"ret\":1,\"name\":\"" + vrResult[0].szTrueName + "\",\"acc\":\"" + vrResult[0].dwAccNo + "\",\"list\":" + str + "}");
                }
                else
                {
                    Response.Write("{\"ret\":0,\"msg\":\"没有找到\"}");
                }
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "updatetutor")
        {
            if (!IsLogined())
            {
                Response.Write("{\"ret\":0,\"msg\":\"登录超时，请重新登录！\"}");
                return;
            }
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            UNIACCOUNT vtSet = vrAccInfo;
            vtSet.szLogonName = vrAccInfo.szLogonName;
            vtSet.dwAccNo = vrAccInfo.dwAccNo;
            if (Request["accPhone"] == null || Request["accEmail"] == null)
                return;
            vtSet.szHandPhone = Request["accPhone"];
            vtSet.szEmail = Request["accEmail"];
            //修改导师
            if (!string.IsNullOrEmpty(Request["tutor_acc"]))
            {
                TUTORSTUDENT vrPra = new TUTORSTUDENT();
                vrPra.dwTutorID = Convert.ToUInt32(Request["tutor_acc"]);
                vrPra.szTutorName = Request["tutor_name"];
                vrPra.dwAccNo = vrAccInfo.dwAccNo;
                vrPra.szTrueName = vrAccInfo.szTrueName;
                vrPra.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;//默认导师审核通过(浙大)
                if (m_Request.Account.TutorStudentSet(vrPra) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Response.Write("{\"ret\":0,\"msg\":\"修改导师时出现异常！\"}");
                    return;
                }
            }
            if (m_Request.Account.Set(vtSet, out vtSet) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                vrAccInfo.szHandPhone = vtSet.szHandPhone;
                vrAccInfo.szEmail = vtSet.szEmail;
                Session["LOGIN_ACCINFO"] = vrAccInfo;
                Response.Write("{\"ret\":1}");
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "tutorcheck")
        {
            if (Request["stu_accno"]==null||Session["LOGIN_ACCINFO"]==null)
            {
                return;
            }
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            string order = Request["order"];
            //删除操作
            if (order=="del")
            {
                TUTORSTUDENT vrDel = new TUTORSTUDENT();
                vrDel.dwAccNo = Convert.ToUInt32(Request["stu_accno"]);
                vrDel.dwTutorID=vrAccInfo.dwAccNo;
                if (m_Request.Account.TutorStudentDel(vrDel) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Response.Write("{\"ret\":1}");
                }
                else
                {
                    Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
                }
                return;
            }
            //审核操作
            TUTORSTUDENTCHECK vrSet = new TUTORSTUDENTCHECK();
            if (order == "fail")
            {
                //vrSet.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_TUTORFAIL;
            }
            else if (order == "ok")
            {
                //vrSet.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_TUTOROK;
            }
            vrSet.dwStudentAccNo = Convert.ToUInt32(Request["stu_accno"]);
            vrSet.szStudentName = Request["stu_name"];
            vrSet.dwTutorID = vrAccInfo.dwAccNo;
            if (m_Request.Account.TutorStudentCheck(vrSet) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"ret\":1}");
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
    }
}
