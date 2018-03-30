using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_applydetail : UniClientPage
{
    protected YARDRESV resv;
    protected string profit;
    protected string media;
    protected string open;
    protected string require;
    protected string atyType;
    protected string checkService;
    protected string checkDirector;
    protected string checkMan;
    protected string resvTime;
    protected string applyTbl;
    protected uint status;
    protected bool isShare = false;
    protected bool isSports = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            InitYardResv(Request["resv_id"]);
        }
        else
            MsgBoxH("登录超时，将重新加载页面", "location.reload();");
    }

    private void InitYardResv(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        YARDRESVREQ req = new YARDRESVREQ();
        req.dwResvGroupID = ToUInt(id);
        //req.dwReqProp = (uint)YARDRESVREQ.DWREQPROP.YARDREQ_ONLYMAINRESV;必须获取多条才能判断是否批量预约
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        YARDRESV[] rlt;
        if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                resv = rlt[0];
                /////////////////
                CHECKTYPE[] mantypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN);
                CHECKTYPE[] dicttypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
                CHECKTYPE[] servtypes = GetCheckType(null, (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
                string msg;
                YARDRESVCHECKINFO[] ckInfo = GetResvCheckInfo(id, out msg);
                string man = "";
                string dict = "";
                string service = "";
                for (int j = 0; j < ckInfo.Length; j++)
                {
                    YARDRESVCHECKINFO info = ckInfo[j];
                    CHECKTYPE mantype = IsInAarray(info.dwCheckKind, mantypes);
                    CHECKTYPE dicttype = IsInAarray(info.dwCheckKind, dicttypes);
                    CHECKTYPE servtype = IsInAarray(info.dwCheckKind, servtypes);
                    if (mantype.dwCheckKind != null)
                        man = resv.szDeptName + " (" + GetCheckState(info.dwCheckStat)+")" ;
                    else if (dicttype.dwCheckKind != null)
                    {
                        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                        string name = dicttype.szDeptName;
                        if (string.IsNullOrEmpty(name.Trim())) name = acc.szDeptName;
                        dict = name + " (" + GetCheckState(info.dwCheckStat) + ")";
                    }
                    else if (servtype.dwCheckKind != null)
                        service += info.szCheckName + "&nbsp;&nbsp;&nbsp;";
                }
                checkService = service;
                checkDirector = dict;
                checkMan = man;
                ////////////////////////
                status = ToUInt(resv.dwStatus);
                profit=(resv.dwProperty&(uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT)>0?"是":"否";
                media = (resv.dwProperty & 0x4000000) > 0 ? "是" : "否";
                open = (resv.dwProperty & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN) > 0 ? "否" : "是";
                require = resv.szDesiredUser;
                if(rlt.Length>1) resvTime=resv.szCycRule;
                else resvTime = Get1970Date((int)resv.dwBeginTime) + "至" + Get1970Date((int)resv.dwEndTime).Substring(11);
                isShare = (resv.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE )> 0;//室外场地
                atyType = GetTypeList(resv.dwKind.ToString());

                //申请模版
                applyTbl = "dft";
                //申请模版
                if ((resv.dwSecurityLevel & 0x20000000) > 0)//会议模版
                {
                    applyTbl = "hy";
                }
                else if ((resv.dwSecurityLevel & 0x800000) > 0)//体育场地
                {
                    isSports = true;
                }
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }
    private string GetTypeList(string sn)
    {
        CODINGTABLE[] rlt = GetCodeTable((uint)CODINGTABLE.DWCODETYPE.CODE_YARDRESVKIND, sn);
        if (rlt != null && rlt.Length > 0)
        {
            return rlt[0].szCodeName;
        }
        else
            return "";
    }

    //获取预约审核状态
    public YARDRESVCHECKINFO[] GetResvCheckInfo(string resvId, out string ret)
    {
        YARDRESVCHECKINFOREQ req = new YARDRESVCHECKINFOREQ();
        if (!string.IsNullOrEmpty(resvId))
            req.dwResvID = ToUInt(resvId);
        req.szReqExtInfo.szOrderKey = "dwCheckStat";
        req.szReqExtInfo.szOrderMode = "DESC";
        YARDRESVCHECKINFO[] rlt;
        if (m_Request.Reserve.GetYardResvCheckInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            ret = "ok";
        }
        else
        {
            ret = m_Request.szErrMsg;
        }
        return rlt;
    }
    private CHECKTYPE IsInAarray(uint? type, CHECKTYPE[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (type == arr[i].dwCheckKind)
                return arr[i];
        }
        return new CHECKTYPE();
    }
    private string GetCheckState(uint? sta)
    {
        if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK) > 0) return "审核通过";
        if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_FAIL) > 0) return "审核未通过";
        if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0) return "正在审核";
        if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO) > 0) return "等待审核";
        if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_WAIT) > 0) return "等待上级审核";
        return "未审核";
    }
}