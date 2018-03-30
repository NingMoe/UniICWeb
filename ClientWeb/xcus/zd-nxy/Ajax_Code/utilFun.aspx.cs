using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class ClientWeb_Ajax_Code_utilFun : UniClientPage
{
    UNIACCOUNT curAcc;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        Response.ContentType = "application/Json";
        string act = Request["act"].ToString();
        if (act == "get_dev_m_rank")
        {
            GetDevRank(act);
        }
        else if (act == "get_dev_y_rank")
        {
            GetDevRank(act);
        }
        else
        {
            if (Session["LOGIN_ACCINFO"] == null || !IsLogined())
            {
                ErrMsg("out_time", "登录超时，请重新登录。");
                return;
            }
            curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (act == "del_data")
            {
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                uint id = Convert.ToUInt32(Request["id"]);
                UNITESTDATA vrData = new UNITESTDATA();
                vrData.dwSID = id;
                vrData.dwAccNo = curAcc.dwAccNo;
                vrData.dwStatus = (uint)UNITESTDATA.DWSTATUS.TDSTAT_FILEDEL;
                uResponse = m_Request.Account.TestDataChgStat(vrData);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    ActSuc(act, "");
                }
                else
                {
                    ErrMsg(act, m_Request.szErrMsg);
                }
            }
            else if (act == "del_rt_resv")
            {
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                uint id = Convert.ToUInt32(Request["id"]);
                RTRESV vrRTResv = new RTRESV();
                vrRTResv.dwResvID = id;
                uResponse = m_Request.Reserve.DelRTResv(vrRTResv);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    ActSuc(act, "");
                }
                else
                {
                    ErrMsg(act, m_Request.szErrMsg);
                }
            }
            else
            {
                ErrMsg("no_act", "对应的操作没有相关处理程序。");
            }
        }
    }

    private void GetDevRank(string act)
    {
        uint start = Convert.ToUInt32(Request["start"]);
        uint end = Convert.ToUInt32(Request["end"]);
        uint need = Convert.ToUInt32(Request["need"]);
        List<unidev> devs = GetRank(start, end, need);
        if (devs == null)
        {
            ErrMsg(act, m_Request.szErrMsg);
        }
        else
        {
            Response.Write("{\"ret\":1,\"act\":\"" + act + "\",\"msg\":\"ok\",\"data\":" + JsonConvert.SerializeObject(devs) + "}");
        }
    }

    private List<unidev> GetRank(uint start, uint end, uint need)
    {
        REQUESTCODE uResponse = REQUESTCODE.DBERR_FAILED;
        REPORTREQ req = new REPORTREQ();
        req.dwGetType = (int)REPORTREQ.DWGETTYPE.USERECGET_BYALL;
        req.szReqExtInfo.szOrderKey = "dwTotalUseTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        req.dwStartDate = start;
        req.dwEndDate = end;
        if (need != 0)
        {
            req.szReqExtInfo.dwNeedLines = need;
            req.szReqExtInfo.dwStartLine = 0;
        }
        DEVSTAT[] rlt;
        uResponse = m_Request.Report.GetDevStat(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return ToRankDev(rlt);
        }
        return null;
    }
    private void ErrMsg(string act, string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            msg = "操作失败！";
        }
        Response.Write("{\"ret\":0,\"act\":\"" + act + "\",\"msg\":\"" + msg + "\"}");
    }
    private void ActSuc(string act, string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            msg = "操作成功！";
        }
        Response.Write("{\"ret\":1,\"act\":\"" + act + "\",\"msg\":\"" + msg + "\"}");
    }
    private List<unidev> ToRankDev(DEVSTAT[] list)
    {
        List<unidev> devs = new List<unidev>();
        for (int i = 0; i < list.Length; i++)
        {
            DEVSTAT item = list[i];
            unidev dev = new unidev();
            dev.id = item.dwDevID.ToString();
            dev.name = item.szDevName;
            dev.count = item.dwUseTimes.ToString();
            dev.time = item.dwTotalUseTime.ToString();
            devs.Add(dev);
        }
        return devs;
    }
}
class unidev
{
    public string id;
    public string name;
    public string time;
    public string count;
}