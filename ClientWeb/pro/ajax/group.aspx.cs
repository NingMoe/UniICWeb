using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_group : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage() && IsLoginReady())
        {
            if (act == "get_g_mbs")
            {
                GetGroupMbs();
            }
            else if (act == "get_g_info")
            {
                GetGroupInfo();
            }
            else if (act == "del_g_mb_accno")
            {
                string id = Request["accno"];
                string groupId = Request["group_id"];
                if (DelMemByAccNo(groupId, id))
                    SucMsg();
                else
                    ErrMsg();
            }
            else if (act == "set_group_name")
            {
                string id = Request["group_id"];
                string name = Request["group_name"];
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
                {
                    ErrMsg("参数有误");
                    return;
                }
                UNIGROUP para = new UNIGROUP();
                para.dwGroupID = ToUInt(id);
                para.szName = name;
                if (m_Request.Group.SetGroup(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
                    SucMsg();
                else
                    ErrMsg();
            }
            else if (act == "add_g_mb" || act == "add_g_mb_accno")
            {
                string id = Request["id"];
                string groupId = Request["group_id"];
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(groupId))
                {
                    ErrMsgP();
                    return;
                }
                string[] mbs = id.Split(',');
                int num = 0;
                for (int i = 0; i < mbs.Length; i++)
                {
                    if (mbs[i] != "")
                    {
                        if (act == "add_g_mb_accno" ? AddMemByAccNo(groupId, mbs[i]) : AddMember(groupId, mbs[i]))
                        {
                            num++;
                        }
                    }
                }
                SucRlt(num);
            }
            else if (act == "del_g_mb")
            {
                string id = Request["id"];
                string groupId = Request["group_id"];
                if (DelMember(groupId, id))
                    SucMsg();
                else
                    ErrMsg();
            }
            else if (act == "del_group")
            {
                string id = Request["group_id"];
                UNIGROUP set = new UNIGROUP();
                set.dwGroupID = ToUInt(id);
                if (m_Request.Group.DelGroup(set) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    SucMsg();
                }
                else
                {
                    ErrMsg();
                }
            }
        }
    }

    private void GetGroupInfo()
    {
        string id = Request["group_id"];
        GROUPREQ req = new GROUPREQ();
        //req.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
        //req.szGetKey = id;
        req.dwGroupID = ToUInt(id);
        UNIGROUP[] rlt;
        if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucRlt(rlt);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetGroupMbs()
    {
        string groupId = Request["group_id"];
        string kind = Request["kind"];
        if (string.IsNullOrEmpty(groupId))
            ErrMsg("缺少组ID");
        else
        {
            //uint? k;
            //if (string.IsNullOrEmpty(kind)) k = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
            //else k = ToUInt(kind);
            GROUPMEMDETAIL[] list = GetMembers(ToUInt(groupId), ToUInt(kind));
            if (list != null)
            {
                SucRlt(list);
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
        }
    }
}