using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_atydetail : UniClientPage
{
    protected string checkService = "";
    protected string checkDirector = "";
    protected string checkDevMan = "";
    protected string infoIntro = "";
    protected string infoRule = "";
    protected string infoTitle = "";
    protected string condition = "";
    protected string kinds = "";
    protected string LabList = "";
    protected string CampusList = "";
    protected string BuildingList = "";
    protected string DevClsList = "";
    protected string applyTbl = "";
    protected string multiResv = "";

    protected bool hideDevCls = true;//隐藏类型选择
    
    YARDACTIVITY activity;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            kindId.Value = Request["kindId"];
            activityId.Value = Request["activityId"];
            GetYardActivity();
            //GetCheckList();
            InitContent();
            LabList = GetLabHtm("opt");
            CampusList = GetCampusHtm("opt");
            BuildingList = GetBuildingHtm("opt", null, ToUInt(activityId.Value));
            DevClsList = GetDevClassHtm("opt");
        }
        else
            MsgBoxH("登录超时，将重新加载页面", "location.reload();");
    }

    private void InitContent()
    {
        infoIntro = GetXmlContent(activityId.Value, "aty_intro");
        infoRule = GetXmlContent(activityId.Value, "aty_rule");
    }

    private void GetYardActivity()
    {
        if (string.IsNullOrEmpty(activityId.Value)) return;
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        req.dwActivitySN = ToUInt(activityId.Value);
        YARDACTIVITY[] rlt;
        if (m_Request.Reserve.GetYardActivity(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0) 
            {
                activity = rlt[0];
                activityName.Value = activity.szActivityName;
                infoTitle = activity.szActivityName;
                //活动类型
                if (activity.szMemo != "")
                {
                    string[] list = activity.szMemo.Split('&');
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i] != "")
                            condition += "<label class='click'><input class='cdt_ckbox' type='checkbox' value='" + list[i] + "'/>" + list[i] + "</label>";
                    }
                }
                multiResv = "none";//隐藏批量预约
                //场景区别 custom
                if ((activity.dwSecurityLevel & 0x20000000) > 0)//会议模版
                {
                    applyTbl = "apply_hy";
                }
                else
                {
                    applyTbl = "apply";
                    if ((activity.dwSecurityLevel & 0x10000000) > 0)//教室借用 显示类型选择
                    {
                        hideDevCls = false;
                    }
                }
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }
    //private void GetCheckList()
    //{
    //    UNIACCOUNT acc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
    //    if (activity.dwActivitySN == null) return;
    //    CHECKTYPE[] rlt;
    //    rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN);
    //    if (rlt.Length>0)
    //    {
    //        devman.Value = rlt[0].dwCheckKind.ToString();
    //    }
    //    rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
    //    for (int j = 0; j < rlt.Length; j++)
    //    {
    //        string dept = rlt[j].szCheckName;
    //        if (rlt[j].dwDeptID == null || rlt[j].dwDeptID == 0) dept = acc.szDeptName;//申请人所在部门审核
    //        checkDirector += "<option value='" + rlt[j].dwCheckKind + "'>" + dept + "</option>";
    //    }
    //    rlt = GetCheckType(activity.dwCheckKinds, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
    //    for (int k = 0; k < rlt.Length; k++)
    //    {
    //        checkService += "<label><input type='checkbox' name='radio_group' class='service_v' value='" + rlt[k].dwCheckKind + "'>" + rlt[k].szCheckName + "</label>";
    //    }
    //}
}