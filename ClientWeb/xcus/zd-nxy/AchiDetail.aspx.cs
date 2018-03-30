using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_zd_nxy_AchiDetail : UniClientPage
{
    uint devID;
    uint achi;
    protected string pagePosition = "";
    protected string CurDevName = "";
    protected string devLab = "";
    protected string devMan = "";
    protected REWARDREC rec;
    protected string devs = "";
    protected UNIACCOUNT acc;
    protected string szPicZoom = "";
    protected string szPicPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["kind"] != null)//test
        {
            rec.dwRewardKind = ToUInt(Request["kind"]);
        }
        if (Request["dev"] != null)
        {
            devID = Convert.ToUInt32(Request["dev"]);
            InitDevInfo(devID);
        }
        if (Request["achi"] != null)
        {
            achi = Convert.ToUInt32(Request["achi"]);
            InitReward(achi);
        }
    }

    private void InitReward(uint achi)
    {
        REWARDRECREQ req = new REWARDRECREQ();
        req.dwRewardID = achi;
        req.dwStartDate = 0;
        req.dwEndDate = 20990909;
        req.dwReqProp = (uint)REWARDRECREQ.DWREQPROP.RRREQ_NEEDDEV;
        REWARDREC[] rlt;
        if (m_Request.Device.RewardRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            rec = rlt[0];
            UNIACCOUNT[] accs=GetAccByAccNo(rec.dwOpID.ToString());
            if (accs != null && accs.Length > 0)
                acc = accs[0];
            REWARDUSEDEV[] list = rec.UseDev;
            if(list!=null)
            for (int i = 0; i < list.Length; i++)
            {
                devs +=(i==0?"":"，")+ list[i].szDevName;
            }
            if ((rec.dwRewardKind & (uint)REWARDREC.DWREWARDKIND.REKIND_PRIZE) > 0)
            {
                string[] srcs = rec.szMemo.Split(',');
                for (int i = 0; i < srcs.Length; i++)
                {
                    string src = ToUploadUrl("UploadFile/" + srcs[i]);
                    if (i == 0)
                    {
                        szPicZoom = "<img src='" + src + "'>  ";
                        szPicPath += "<li><a class='cur' ><img src='" + src + "'></a></li>";
                    }
                    else
                    {
                        szPicPath += "<li><a><img src='" + src + "'></a></li>";
                    }
                }
            }
        }
    }
    private void InitDevInfo(uint devID)
    {
        Session["CUR_DEV"] = null;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtResult;
        vrGet.dwDevID = devID;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            UNIDEVICE dev = vtResult[0];
            pagePosition = dev.szLabName + " | " + dev.szDevName+" >> 成果详情";
            CurDevName = dev.szDevName;
            devLab = dev.szLabName;
            devMan = dev.szAttendantName;
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    public string ConvertLevel(uint? level)
    {
        if (level == 1) return "SCI";
        if (level == 2) return "国家级";
        if (level == 3) return "省部级";
        if (level == 101) return "国家级";
        if (level == 102) return "省部级";
        if (level == 103) return "院校级";
        return "其它";
    }
    public string ConvertIdent(uint? ident)
    {
        string ret = "";
        if ((ident & 256) > 0) ret = "学生";
        if ((ident & 512) > 0) ret = "教师";
        if ((ident & (int)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0) ret = "导师";
        if ((ident & (int)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0) ret = "管理员";
        return ret;
    }
}