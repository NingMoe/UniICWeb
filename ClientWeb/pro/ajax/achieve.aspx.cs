using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class ClientWeb_pro_ajax_achi : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "set_achi")
            {
                if (IsLoginReady())
                {
                    SetAchieve();
                }
            }
            else if (act == "del_achi")
            {
                DelAchieve();
            }
            else
            {
                NoAct();
            }
        }
    }

    private void DelAchieve()
    {
        string id = Request["achi_id"];
        REWARDREC set = new REWARDREC();
        set.dwRewardID = ToUInt(id);
        if (m_Request.Device.RewardRecDel(set) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }

    private void SetAchieve()
    {
        string id = Request["achi_id"];
        string kind = Request["achi_kind"];
        string devId = Request["dev_id"];
        string devs = Request["achi_devs"];
        string name = Request["achi_name"];
        string org = Request["org"];
        string member = Request["member"];
        string certId = Request["cert_id"];
        string level = Request["level"];
        string ext = Request["ext"];
        string memo = Request["memo"];
        string rewardDate=Request["reward_date"];
        if (!string.IsNullOrEmpty(devId))
        {
            devs += (string.IsNullOrEmpty(devs) ? "" : ",") + devId;
        }
        REWARDREC set = new REWARDREC();
        if (!string.IsNullOrEmpty(rewardDate))
            set.dwRewardDate = ToUInt(rewardDate);
        //仪器
        if (!string.IsNullOrEmpty(devs))
        {
            string[] list = devs.Split(',');
            set.UseDev = new REWARDUSEDEV[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                set.UseDev[i].dwDevID = ToUInt(list[i]);
            }
        }
        //申请人
        set.dwOpID = curAcc.dwAccNo;
        set.szOpName = curAcc.szTrueName;
        //项目
        RESEARCHTEST[] rts=GetRTestes(null, null, null, curAcc.dwAccNo.ToString(), null);
        if (rts != null && rts.Length > 0)
        {
            RESEARCHTEST rt = rts[0];
            set.dwRTID = rt.dwRTID;
            set.szRTName = rt.szRTName;
            set.dwLeaderID = rt.dwLeaderID;
            set.szLeaderName = rt.szLeaderName;
            set.dwHolderID = rt.dwHolderID;
            set.szHolderName = rt.szHolderName;
        }
        else if (curAcc.dwTutorID != null)
        {
            set.dwHolderID = curAcc.dwTutorID;
            set.szHolderName = curAcc.szTutorName;
        }
            set.dwRewardType = (uint)REWARDREC.DWREWARDTYPE.RETYPE_RESEARCH;//默认科研
            if(!string.IsNullOrEmpty(kind))
            set.dwRewardKind = ToUInt(kind);
            set.szRewardName = name;
            set.szAuthOrg = org;
            set.szMemberNames = member;
            set.szCertID = certId;
            if (!string.IsNullOrEmpty(level))
                set.dwRewardLevel = ToUInt(level);
            set.szExtInfo = ext;
            set.szMemo = memo;
            if (m_Request.Device.RewardRecSet(set, out set) == REQUESTCODE.EXECUTE_SUCCESS)
                SucMsg();
            else
                ErrMsg(m_Request.szErrMsg);

    }
}