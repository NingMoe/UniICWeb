using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_apply_hy : UniClientPage
{
    protected string checkService = "";
    protected string checkDirector = "";
    protected string checkDevMan = "";
    protected string infoIntro = "";
    protected string infoRule = "";
    protected string infoTitle = "";
    protected string condition = "";
    protected string atyId = "";
    protected string atyName = "";
    protected string ClassList = "";
    protected string BuildingList = "";
    protected string CampusList = "";
    protected string typeList = "";
    protected YARDRESV resv;
    protected string profit;
    protected string open="true";
    protected string media;

    protected string date = "";
    protected string start = "";
    protected string end = "";

    protected YARDACTIVITY activity;
    protected string ckIntro;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            atyId = Request["activityId"];
            ClassList = GetDevClassHtm("opt");
            BuildingList = GetBuildingHtm("opt",null,ToUInt(atyId));
            CampusList = GetCampusHtm("opt");
            GetYardActivity();
            GetCheckList();
            copyResv(Request["resv_id"]);//复制申请
            GetTypeList();
            //审核说明
        ckIntro=GetXmlContent(atyId, "ck_intro");
        }
        else
            MsgBoxH("登录超时，将重新加载页面", "location.reload();");
    }

    private void copyResv(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        YARDRESVREQ req = new YARDRESVREQ();
        req.dwResvGroupID = ToUInt(id);
        req.dwReqProp = (uint)YARDRESVREQ.DWREQPROP.YARDREQ_ONLYMAINRESV;
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        YARDRESV[] rlt;
        if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                resv = rlt[0];
                profit = (resv.dwProperty & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT) > 0 ? "true" : "false";
                open = (resv.dwProperty & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN) > 0 ? "false" : "true";
                media = (resv.dwProperty & 0x4000000) > 0 ? "true" : "false";
                date = Get1970Date((int)resv.dwBeginTime).Substring(0, 10);
                start = Get1970Date((int)resv.dwBeginTime).Substring(11);
                end = Get1970Date((int)resv.dwEndTime).Substring(11);
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }

    private void GetTypeList()
    {
        CODINGTABLE[] rlt = GetCodeTable((uint)CODINGTABLE.DWCODETYPE.CODE_YARDRESVKIND, null);
        if (rlt != null)
        {
            string sn ="";
            if (resv.dwKind != 0) sn=resv.dwKind.ToString();//复制申请
            for (int i = 0; i < rlt.Length; i++)
            {
                string check = rlt[i].szCodeSN == sn ? "checked" : "";
                typeList += "<label><input type='radio'  name='aty_type' value='"+rlt[i].szCodeSN+"' "+check+"/>"+rlt[i].szCodeName+"</label>&nbsp;&nbsp;";
            }
        }
    }
    private void GetYardActivity()
    {
        if (string.IsNullOrEmpty(atyId)) return;
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        req.dwActivitySN = ToUInt(atyId);
        YARDACTIVITY[] rlt;
        if (m_Request.Reserve.GetYardActivity(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0) 
            {
                activity = rlt[0];
                atyName = activity.szActivityName;
                infoTitle = activity.szActivityName;
                //UNIDEVKIND kind=GetDevKind(ToUInt(kindId.Value));
                //if (kind.dwKindID != null)
                //{
                //    infoTitle = kind.szKindName + " <small>【" + activity.szActivityName + "】</small>";
                //}
                if (activity.szMemo != "")
                {
                    string[] list = activity.szMemo.Split('&');
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i] != "")
                            condition += "<label class='click'><input class='cdt_ckbox' type='checkbox' value='" + list[i] + "'/>" + list[i] + "</label>";
                    }
                }
                //支持类型
                //kinds = activity.szUsableKindIDs;
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }

    private void GetCheckList()
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if (activity.dwActivitySN == null) return;
        CHECKTYPE[] rlt;
        rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN);
        if (rlt.Length > 0)
        {
            devman.Value = rlt[0].dwCheckKind.ToString();
        }
        rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
        int num = 0;
        for (int j = 0; j < rlt.Length; j++)
        {
            string dept = rlt[j].szCheckName;
            if (rlt[j].dwDeptID == null || rlt[j].dwDeptID == 0)
            {//申请人所在部门审核
                num++;
                dept = acc.szDeptName;
                if ((acc.dwIdent & 512) > 0)//教师只显示本部门 custom
                {
                    checkDirector = "<option value='" + rlt[j].dwCheckKind + "'>" + dept + "</option>";
                    break;
                }
                else
                {
                    checkDirector += "<option value='" + rlt[j].dwCheckKind + "'>" + dept + "</option>";
                }
            }
            else
            {
                checkDirector += "<option value='" + rlt[j].dwCheckKind + "'>" + dept + "</option>";
            }
        }
        if (num > 1) checkDirector = "<option value=''>未选择</option>" + checkDirector;
        rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
        for (int k = 0; k < rlt.Length; k++)
        {
            checkService += "<label><input type='checkbox' name='radio_group' class='service_v' value='" + rlt[k].dwCheckKind + "'>" + rlt[k].szCheckName + "</label>&nbsp;&nbsp;";
        }
    }
}