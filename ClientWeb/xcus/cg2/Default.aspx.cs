using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_Info : UniClientPage
{
    protected string itemList = "";
    protected string itemClsList = "";
    YARDACTIVITY activity;
    protected bool islogin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["activityPage"] = null;
        islogin = LoadPage();
        string lg = GetConfig("mustLogin");
        if (lg == "1" && !islogin)
            Response.Redirect("Login.aspx");
        if (islogin)
            InitDevKind();
    }
    private string ToTime(uint? t)
    {
        int h = (int)(t / 100);
        int m = (int)(t % 100);
        return h.ToString("D2") + ":" + m.ToString("D2");
    }

    private void InitDevKind()
    {
        GetYardActivity();
        //GetKindList();
        return;
        //THIRDRESVSHAREDEV set = new THIRDRESVSHAREDEV();
        //set.szResvTitle = "教务排课";
        ////时间
        //List<TRESVTIME> tmList = new List<TRESVTIME>();
        //        TRESVTIME tm = new TRESVTIME();
        //        tm.dwResvDate = 20151127;
        //        tm.dwStartHM = 1600;
        //        tm.dwEndHM = 1800;
        //        tmList.Add(tm);
        //set.TimeTbl = tmList.ToArray();
        ////地点
        //List<TRESVDEV> devList = new List<TRESVDEV>();
        //        TRESVDEV dev = new TRESVDEV();
        //        dev.szAssertSN = "20020104";
        //        devList.Add(dev);
        //set.DevTbl = devList.ToArray();
        //if (m_Request.Reserve.ThirdResvShareDev(set, out set) == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        //    uint? id = set.dwThirdResvID;
        //}
        //else
        //{
        //    string err = m_Request.szErrMsg;
        //}
    }
    private void GetYardActivity()
    {
        string aty = Request["activity"];
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        YARDACTIVITY[] rlt;
        REQUESTCODE cd = m_Request.Reserve.GetYardActivity(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                //string select = "";
                //if (aty == rlt[i].dwActivitySN.ToString()) select = "selected";
                //itemClsList += "<option value='" + rlt[i].dwActivitySN + "' include='" + rlt[i].szUsableKindIDs + "' " + select + ">" + rlt[i].szActivityName + "</option>";
                if (rlt[i].dwSecurityLevel == 1) continue;//不开放
                uint? type = rlt[i].dwSecurityLevel;
                UNIACCOUNT acc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
                if (((type & 0x20000000) > 0 || (type & 0x10000000) > 0) && (acc.dwIdent & 512) == 0) continue;//非教师不开放会议和教室批量预约
                bool auto = false;//(type & 0x20000000) > 0;//会议申请 自动搜索 暂时不启用 会议室数量不少 刷新缓慢
                itemList += "<li url='atydetail.aspx?rsch="+(auto?"auto":"")+"&activityId=" + rlt[i].dwActivitySN + "'  con='#detail_con' class='click_load'><a>" + rlt[i].szActivityName + "</a></li>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }

    private void GetKindList()
    {
        DEVKINDREQ req = new DEVKINDREQ();
        req.szReqExtInfo.szOrderKey = "szKindName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNIDEVKIND[] rlt;
        m_Request.Device.DevKindGet(req, out rlt);
        if (rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVKIND kind = rlt[i]; 
                itemList += "<li it='" + kind.dwKindID + "' url='atydetail.aspx?kindId=" + kind.dwKindID + "'><a>" + kind.szKindName + "</a></li>";
            }
        }
    }
}