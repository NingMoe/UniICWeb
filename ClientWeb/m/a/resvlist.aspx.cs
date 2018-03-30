using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_view : UniClientPage
{
    protected string detail;
    protected string resvlist3;
    protected string resvlist6;
    protected string resvlist12;
    protected void Page_Load(object sender, EventArgs e)
    {
        InitResvList();
    }

    private void InitResvList()
    {
        RESVREQ req = new RESVREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"));
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(12).ToString("yyyyMMdd"));
        uint classkind = ToUInt(GetConfig("openClsKind"));
        if (classkind > 0) req.dwClassKind = classkind;
        req.szReqExtInfo.szOrderKey = "dwBeginTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        UNIRESERVE[] rlt;
        if (m_Request.Reserve.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            int num = 0;
            uint month3 = ToUInt(DateTime.Now.AddMonths(-3).ToString("yyyyMMdd"));
            uint month6 = ToUInt(DateTime.Now.AddMonths(-6).ToString("yyyyMMdd"));
            resvlist3 = GetResvList(rlt, ToUInt(req.dwEndDate), month3,ref num);
            resvlist6 = GetResvList(rlt, month3, month6, ref num);
            resvlist12 = GetResvList(rlt, month6, ToUInt(req.dwBeginDate), ref num);
        }
    }

    private string GetResvList(UNIRESERVE[] rlt,uint startLine,uint deadLine,ref int num)
    {
        string ret = "";
        for (int i = 0; i < rlt.Length; i++)
        {
            if (rlt[i].dwPreDate > startLine) continue;
            if (rlt[i].dwPreDate < deadLine) continue;
            num++;
            UNIRESERVE resv = rlt[i];
            //预约时间
            string start = Get1970Date((int)rlt[i].dwBeginTime);
            string end = Get1970Date((int)rlt[i].dwEndTime);
            string timeDesc = Get1970Date((int)rlt[i].dwBeginTime, "MM/dd HH:mm") + "-" + Get1970Date((int)rlt[i].dwEndTime, (start.Substring(0, 10) == end.Substring(0, 10) ? "" : "MM/dd ") + "HH:mm");
            //预约对象
            string objs = "";
            string location = "";
            RESVDEV[] resvDev = resv.ResvDev;
            if (resvDev != null && resvDev.Length > 0)
            {
                string devName = string.Empty;
                for (int j = 0; j < resvDev.Length; j++)
                {
                    if (j == 0) location = resvDev[0].szLabName;
                    devName = devName + resvDev[j].szDevName.ToString();
                }
                objs = devName != "" ? devName : Translate("未分配");
            }
            //提交时间
            string occur = Get1970Date((int)resv.dwOccurTime);
            //预约信息
            string state = Translate( Util.Converter.ResvStatusConverter(resv.dwStatus));
            ret+="<li class='item-content'><div class='item-inner'><div class='item-title-row'>" +
                "<div class='item-title'>"+objs+"</div>" +
                "<div class='item-after'>"+state+"</div></div>" +
              "<div class='item-subtitle'>"+timeDesc+"</div>" +
              "<div class='item-text'>" + location + "</br>" + resv.szOwnerName + Translate("提交于")+":" + occur + "</div></div></li>";
        }
        detail += "<td class='text-center'>" + num +"</td>";//+ Translate("条")
        if (ret == "")
        {
            ret = "<li class='item-content'><div class='item-inner'>" +
    "<div class='item-title text-center'>" + Translate("无数据") + "</div></div></li>";
        }
        return ret;
    }
}