using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class DevWeb_Ajax_Code_rtestes : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        Response.ContentType = "application/Json";
        string act = Request["act"].ToString();
        if (act == "new")
        {
            CreateRTest();
        }
        else if (act == "get")
        {
            Get();
        }
        else if (act == "del")
        {
            DelRTest();
        }
        else if (act == "set")
        {
            AlterRTest();
        }
        else if(act=="addm")
        {
            if (Request["id"] != null && Request["lg"]!=null)
            {
                if (AddMember(Request["id"], Request["lg"]))
                {
                    Response.Write("{\"ret\":1}");
                    return;
                }
            }
            Response.Write("{\"ret\":0}");
        }
        else if (act=="delm")
        {
            if (Request["id"] != null && Request["lg"] != null)
            {
                if (DelMember(Request["id"], Request["lg"]))
                {
                    Response.Write("{\"ret\":1}");
                    return;
                }
            }
            Response.Write("{\"ret\":0}");
        }

    }

    private void DelRTest()
    {
        uint id = Convert.ToUInt32(Request["id"]);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTEST setvalue = new RESEARCHTEST();
        setvalue.dwRTID = id;
        uResponse = m_Request.Reserve.DelResearchTest(setvalue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("{\"ret\":1,\"act\":\"del\",\"id\":\"" + id + "\"}");
        }
        else
        {
            Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
        }
    }

    private void Get()
    {
        string id = Request["id"];
        RESEARCHTEST[] vtResult = GetRTestes(id);
        if (vtResult != null && vtResult.Length > 0)
        {
            RESEARCHTEST rtest = vtResult[0];
            string rlt = "{\"ret\":1,\"act\":\"get\",";
            rlt += "\"get_rtname\":\"" + rtest.szRTName + "\",";
            rlt += "\"get_leader\":\"" + rtest.szLeaderName + "\",";
            rlt += "\"get_leader_acc\":\"" + rtest.dwLeaderID + "\",";
            rlt += "\"get_group_id\":\"" + rtest.dwGroupID + "\",";
            string menberList = "";
            RTMEMBER[] menTbl = rtest.RTMembers;
            for (int i = 0; i < menTbl.Length; i++)
            {
                ACCREQ vrGetAcc = new ACCREQ();
                vrGetAcc.dwAccNo = menTbl[i].dwAccNo;
                UNIACCOUNT[] vrAccResult;
                m_Request.Account.Get(vrGetAcc, out vrAccResult);
                menberList += "<li><span name='memid'>" + vrAccResult[0].szLogonName + "</span>|<span>" + vrAccResult[0].szTrueName + "</span>|<a href='#' onclick='$(this).parent().hide();return false;'>删除</a></li>";
            }
            rlt += "\"get_member\":\"" + menberList + "\"}";
            Response.Write(rlt);
        }
        else
        {
            Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
        }
    }

    void AlterRTest()
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((Convert.ToUInt32(acc.dwIdent) & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR)>0)
        {
            string rtId = Request["id"];
            string rtName = Request["set_rt_name"];
            string leader = Request["set_leader"];
            uint leaderAcc = Convert.ToUInt32(Request["set_leader_acc"]);
            string leaderLgName = Request["set_leader_lgname"];
            string groupId = Request["set_group_id"];
            string addmemList = Request["set_addmem_list"];
            string delmemList = Request["set_delmem_list"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            vrGet.dwRTID = Convert.ToUInt32(rtId);
            RESEARCHTEST[] vtResult ;
            m_Request.Reserve.GetResearchTest(vrGet, out vtResult);
            RESEARCHTEST setvalue = new RESEARCHTEST();
            setvalue = vtResult[0];
            setvalue.szRTName = rtName;
            setvalue.dwLeaderID = leaderAcc;
            setvalue.szLeaderName = leader;


            if (!string.IsNullOrEmpty(leaderLgName))
            {
                addmemList += leaderLgName;
            }
            else if (!string.IsNullOrEmpty(addmemList))
            {
                addmemList.Substring(0, addmemList.Length - 1);
            }
            else if (!string.IsNullOrEmpty(delmemList))
            {
                delmemList.Substring(0, delmemList.Length - 1);
            }
            
            AlterGroup(groupId, addmemList,delmemList);
            uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"ret\":1,\"act\":\"set\"}");
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else
        {
            Response.Write("{\"ret\":0,\"msg\":\"您不是导师，没有权限！\"}");
        }
    }

    void CreateRTest()
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((Convert.ToUInt32(acc.dwIdent) & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR)>0)
        {
            string rtName = Request["rtname"];//Server.UrlDecode(
            string memList = Request["memlist"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RESEARCHTEST setvalue = new RESEARCHTEST();
            setvalue.dwHolderID = acc.dwAccNo;
            setvalue.szHolderName = acc.szTrueName;
            setvalue.szRTName = rtName;
            setvalue.dwLeaderID = acc.dwAccNo;
            setvalue.szLeaderName = acc.szTrueName;

            memList += acc.szLogonName.ToString();
            uint groupId = NewGroup(rtName, memList);

            setvalue.dwGroupID = groupId;
            uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"ret\":1}");
            }
            else
            {
                Response.Write("{\"ret\":0,\"msg\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else
        {
            Response.Write("{\"ret\":0,\"msg\":\"您不是导师，没有权限！\"}");
        }
    }


    RESEARCHTEST[] GetRTestes(string id)
    {
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        RESEARCHTEST[] vtResult;
        if (id != null)
        {
            vrGet.dwRTID = Convert.ToUInt32(id);
        }
        m_Request.Reserve.GetResearchTest(vrGet, out vtResult);
        return vtResult;
    }
    private uint NewGroup(string szName, string memList)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIGROUP setGroup = new UNIGROUP();
        setGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
        setGroup.szName = szName;
        uResponse = m_Request.Group.SetGroup(setGroup, out setGroup);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && setGroup.dwGroupID != null)
        {
            string szLogonNameList = memList;
            if (szLogonNameList.IndexOf(",") > -1)
            {
                string[] szLogonName = szLogonNameList.Split(',');
                for (int i = 0; i < szLogonName.Length; i++)
                {
                    AddMember(setGroup.dwGroupID.ToString(), szLogonName[i]);
                }
            }
            else
            {
                AddMember(setGroup.dwGroupID.ToString(), szLogonNameList);
            }
            return (uint)setGroup.dwGroupID;
        }
        return 0;
    }
}