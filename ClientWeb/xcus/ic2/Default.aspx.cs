using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_Info : UniClientPage
{
    protected string itemList = "";
    protected string closeDevCls = "";
    YARDACTIVITY activity;
    protected bool islogin = false;
    protected string openAty = "";
    protected string helpUrl = "help";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetConfig("supMultilanguage") == "1"&& Session["language"]!=null&&Session["language"].ToString() == "en-gb")
        {
            
            helpUrl = "helpEnglish";
        }
        
        islogin = IsLogined();
        openAty = GetConfig("openActivity");
        string lg = GetConfig("mustLogin");
        if (lg == "1" && !islogin)
            Response.Redirect("Login.aspx?sys=person");
        InitSubsys();

        //RESVKINDSTATREQ req = new RESVKINDSTATREQ();
        //req.dwStartDate = 20160101;
        //req.dwEndDate = 20160222;
        ////req.dwPurpose=
        //RESVKINDSTAT[] rlt;
        //REQUESTCODE ret = m_Request.Report.GetResvKindStat(req, out rlt);
        //FULLLABREQ req = new FULLLABREQ();
        //FULLLAB[] rlt;
        //if (m_Request.Device.FullLabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        //    int len = rlt.Length;
        //}
        //else
        //{
        //    string msg = m_Request.szErrMsg;
        //}
        //
        //ROOMFORRESVREQ req = new ROOMFORRESVREQ();
        //req.dwBeginTime = (uint)Get1970Seconds("2015-09-21 11:30");
        //req.dwEndTime = (uint)Get1970Seconds("2015-09-21 17:30");
        //req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        //req.dwDate = 20150918;
        //req.dwClassKind = 8;
        //ROOMFORRESV[] rlt;
        //if (m_Request.Device.GetRoomForResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        //    int len = rlt.Length;
        //}
        //else
        //{
        //    MsgBox(m_Request.szErrMsg);
        //}

        //if (islogin)
        //{
        //    UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //    AUTORESVREQ req = new AUTORESVREQ();
        //    req.dwOwner = acc.dwAccNo;
        //    req.dwDevKind = 660;
        //    req.dwPreDate = 20150930;
        //    req.dwEarlyBeginTime = (uint)Get1970Seconds("2015-09-30 14:30");
        //    req.dwLateBeginTime = (uint)Get1970Seconds("2015-09-30 17:00");
        //    req.dwUseMin = 60;
        //    UNIRESERVE rlt;
        //    if (m_Request.Reserve.Auto(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        //    {
        //        UNIRESERVE resv = rlt;
        //    }
        //    else
        //    {
        //        string err = m_Request.szErrMsg;
        //    }
        //}
    }

    private void InitSubsys()
    {
        uint rscMode = ToUInt(GetConfig("resourceMode"));
        uint quick = 0;//ToUInt(GetConfig("quickResv"));
        //不支持子系统
        if (rscMode == 32)
        {
            itemList += GetLab();//实验室
            return;
        }
        else if (rscMode == 8)
        {
            itemList += GetDept();//部门
            return;
        }

        uint clsKind = ToUInt(GetConfig("openClsKind"));
        if (clsKind == 0)//全部
            GetSubItem(0,rscMode);
        else
        {
            uint rm=(uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            uint seat=(uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            uint cpt=(uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            uint loan=(uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
            if ((clsKind & rm) > 0)//空间
            {
                uint rsc=ToUInt(GetConfig("subRmResourceMode"));
                string name = GetConfig("SysKindRoom");
                itemList += "<h5>" + (name == "" ? "空间" : Translate(name)) + ((quick & rm) > 0 ? "<button class='btn btn-default quick_resv_btn' subsys='1'>快速预约</button>" : "") + "</h5>";
                GetSubItem(rm, rsc>0?rsc:rscMode);
            }
            if ((clsKind & seat)> 0)//座位
            {
                uint rsc = ToUInt(GetConfig("subSeatResourceMode"));
                string name = GetConfig("SysKindSeat");
                itemList += "<h5>" + (name == "" ? "座位" : Translate(name)) + ((quick & seat) > 0 ? "<button class='btn btn-default quick_resv_btn' subsys='8'>快速抢座</button>" : "") + "</h5>";
                GetSubItem(seat, rsc > 0 ? rsc : rscMode);
            }
            if ((clsKind & cpt) > 0)//电子阅览室
            {
                uint rsc = ToUInt(GetConfig("subCptResourceMode"));
                string name = GetConfig("SysKindPC");
                itemList += "<h5>" + (name == "" ? "电子阅览室" : Translate(name)) + ((quick & cpt) > 0 ? "<button class='btn btn-default quick_resv_btn' subsys='2'>快速抢位</button>" : "") + "</h5>";
                GetSubItem(cpt, rsc > 0 ? rsc : rscMode);
            }
            if ((clsKind & loan) > 0)//外借
            {
                uint rsc = ToUInt(GetConfig("subLoanResourceMode"));
                string name = GetConfig("SysKindLend");
                itemList += "<h5>" + (name == "外借设备" ? "" : Translate(name)) + "</h5>";
                GetSubItem(loan, rsc > 0 ? rsc : rscMode);
            }
        }
    }

    private void GetSubItem(uint clsKind,uint mode)
    {
        //设备
        if (mode == 16)
            itemList += GetDev(clsKind);
        //房间 / 实验室 + 房间
        else if ((mode & 4) > 0)
            itemList += GetRoom(clsKind, mode);
        else if ((mode & 2) > 0)
            itemList += GetKinds(clsKind);
        //设备类别 类别
        else
            itemList += GetDevCls(clsKind);
    }

    private string GetKinds(uint clsKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDREQ req = new DEVKINDREQ();

        req.szReqExtInfo.szOrderKey = "dwNationCode";
        req.szReqExtInfo.szOrderMode = "asc";

        if (clsKind != 0)
            req.dwClassKind = clsKind;
        UNIDEVKIND[] rlt;
        string ret = "<li class='nav_cls_li'><ul class='it_list nav'>";
        uResponse = m_Request.Device.DevKindGet(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            DEVCLSREQ devclsReq = new DEVCLSREQ();
            devclsReq.dwKind = clsKind;
            UNIDEVCLS[] devclsRes;
            uResponse = m_Request.Device.DevClsGet(devclsReq, out devclsRes);

            for (int i = 0; i < rlt.Length; i++)
            {
                if (bDevClsFalse(rlt[i],devclsRes) == true)
                {
                    continue;
                }
                ret += "<li class='it' it='devcls' url=\"../a/dftdetail.aspx?mode=2&classKind=" + rlt[i].dwClassKind + "&id=" + rlt[i].dwKindID +
                    "&name=" + Server.UrlEncode(rlt[i].szKindName) + "\"><a><span>" + rlt[i].szKindName + "</span></a></li>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return ret + "</ul></li>";
    }
    private bool bDevClsFalse(UNIDEVKIND devkind, UNIDEVCLS[] devclsRes)
    {
        for (int i = 0; i < devclsRes.Length; i++)
        {
            if (devkind.dwClassID == devclsRes[i].dwClassID&&devclsRes[i].szMemo!=null&&devclsRes[i].szMemo=="false")
            {
                return true;
            }
        }
        return false;
    }
    private string GetLab()
    {
        LABREQ req = new LABREQ();
        UNILAB[] rlt;
        string ret = "";
        if (m_Request.Device.LabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<li class='nav_cls_li'><ul class='it_list nav'>";
            for (int i = 0; i < rlt.Length; i++)
            {
                ret += "<li class='it' it='lab' url=\"../a/labdetail.aspx?labId=" + rlt[i].dwLabID + "&labName=" + Server.UrlEncode(rlt[i].szLabName) + "\"><a><span>" + rlt[i].szLabName + "</span></a></li>";
            }
            ret += "</ul><li>";
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return ret;
    }
    private string GetDept()
    {
        DEPTREQ req = new DEPTREQ();
        UNIDEPT[] rlt;
        string ret = "";
        if (m_Request.Account.DeptGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<li class='nav_cls_li'><ul class='it_list nav'>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEPT dept = rlt[i];
                ret += "<li class='it' it='dept' url=\"../a/courselist.aspx?deptId=" + dept.dwID + "&deptName=" + Server.UrlEncode(dept.szName) + "\"><a><span>" + dept.szName + "</span></a></li>";
            }
            ret += "</ul></li>";
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return ret;
    }
    private string GetDev(uint clsKind)
    {
        DEVREQ req = new DEVREQ();
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
        if (clsKind != 0)
        req.dwClassKind = clsKind;
        UNIDEVICE[] rlt;
        string ret = "";
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<li class='nav_cls_li'><ul class='it_list nav'>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVICE dev = rlt[i];
                if (IsStat(dev.dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;
                ret += "<li class='it' it='dev' devId='" + dev.dwDevID + "' url=\"../a/devdetail.aspx?back=false&classKind="+dev.dwClassKind+"&dev=" + dev.dwDevID + "&sn=" + dev.dwDevSN + "\"><a><span>" + dev.szDevName + "</span></a></li>";
            }
            ret += "</ul></li>";
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return ret;
    }
    private string GetRoom(uint clsKind, uint mode)
    {
        string ret = "";
        if (mode == 16)
        {
            LABREQ labReq = new LABREQ();
            labReq.szReqExtInfo.szOrderKey = "szLabSN";
            labReq.szReqExtInfo.szOrderMode = "ASC";
            labReq.dwLabClass = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            UNILAB[] labSeat;
            if (m_Request.Device.LabGet(labReq, out labSeat) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int m = 0; m < labSeat.Length; m++)
                {
                    ret += "<li class='nav_cls_li cls_sec' value='lab_" + labSeat[m].dwLabID + "'><a class='nav_cls_name'><span class='glyphicon glyphicon-circle-arrow-down'></span>&nbsp;" + labSeat[m].szLabName + "</a><ul class='it_list sec_it_list nav' it='lab'>";
                    ROOMREQ req = new ROOMREQ();
                    bool byLab = (mode & 32) > 0;
                    if (byLab)
                    {
                        req.szReqExtInfo.szOrderKey = "szLabSN ASC, CreateDate";
                        req.szReqExtInfo.szOrderMode = "ASC";
                    }
                    req.dwLabID = labSeat[m].dwLabID;
                    UNIROOM[] rlt;
                    if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        for (int i = 0; i < rlt.Length; i++)
                        {
                            UNIROOM rm = rlt[i];
                            if ((rm.dwProperty & 0x800000) > 0)//临时  0x800000=不开放
                            {
                                continue;
                            }

                            if (!byLab && i == 0)//一级分类
                            {
                                ret += "<li class='nav_cls_li'><ul class='it_list nav' it='rm'>";
                            }
                            ret += "<li class='it' it='lab_" + rm.dwLabID + "' url=\"../a/roomdetail.aspx?classKind=" + clsKind + "&roomId=" + rm.dwRoomID + "&roomName=" + Server.UrlEncode(rm.szRoomName) + "\"><a><span>" + rm.szRoomName + "</span></a></li>";
                        }

                    }
                    else
                    {
                        MsgBox(m_Request.szErrMsg);
                    }

                    ret += "</ul></li>";

                }

            }
            else
            {
                MsgBox(m_Request.szErrMsg);
            }
        }
        else if ((mode&4)>0)
        {

            ROOMREQ req = new ROOMREQ();
            if (clsKind != 0)
            {
                req.dwInClassKind = clsKind;
            }
            bool byLab = (mode & 32) > 0;
            if (byLab)
            {
                req.szReqExtInfo.szOrderKey = "szLabSN ASC, CreateDate";
                req.szReqExtInfo.szOrderMode = "ASC";
            }
            //else
            //{
            //    req.szReqExtInfo.szOrderKey = "szRoomName";
            //    req.szReqExtInfo.szOrderMode = "ASC";
            //}
            UNIROOM[] rlt;


            if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {

                for (int i = 0; i < rlt.Length; i++)
                {

                    UNIROOM rm = rlt[i];
                    //20170508修改，从contiue调整到前面，解决第一个区域网站不对外开放的问题。
                    if (byLab && (i == 0 || rm.dwLabID != rlt[i - 1].dwLabID))//二级分类
                    {
                        ret += "<li class='nav_cls_li cls_sec' value='lab_" + rlt[i].dwLabID + "'><a class='nav_cls_name'><span class='glyphicon glyphicon-circle-arrow-down'></span>&nbsp;" + rlt[i].szLabName + "</a><ul class='it_list sec_it_list nav' it='lab'>";
                    }
                    if ((rm.dwProperty & 0x800000) > 0)//临时  0x800000=不开放
                    {
                        if (i == rlt.Length - 1 || (byLab && rm.dwLabID != rlt[i + 1].dwLabID))
                        {
                            ret += "</ul></li>";
                        }
                        continue;
                    }
                  


                    if (!byLab && i == 0)//一级分类
                    {
                        ret += "<li class='nav_cls_li'><ul class='it_list nav' it='rm'>";
                    }
                    ret += "<li class='it' it='lab_" + rm.dwLabID + "' url=\"../a/roomdetail.aspx?classKind=" + clsKind + "&roomId=" + rm.dwRoomID + "&roomName=" + Server.UrlEncode(rm.szRoomName) + "\"><a><span>" + rm.szRoomName + "</span></a></li>";
                    if (i == rlt.Length - 1 || (byLab && rm.dwLabID != rlt[i + 1].dwLabID))
                    {
                        ret += "</ul></li>";
                    }
                }

            }
            else
            {
                MsgBox(m_Request.szErrMsg);
            }
        }
        return ret;
    }
    private string GetDevCls(uint clsKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        if (clsKind != 0)
            vrGet.dwKind = clsKind;
        UNIDEVCLS[] vtRes;
        vrGet.szReqExtInfo.szOrderKey = "dwResv1";
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        string ret = "<li class='nav_cls_li'><ul class='it_list nav'>";
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                //string isLong = "false";http://localhost:19454/web/App_Code/UniInterface/UniInterface.xml
                //string isKind = "false";
                //DEVKINDREQ req = new DEVKINDREQ();
                //req.szClassName = vtRes[i].szClassName;
                //UNIDEVKIND[] rlt;
                //if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
                //{
                //    if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 && GetConfig("resvAllDay") == "1")
                //    {
                //        isLong = "true";
                //    }
                //    if ((rlt[0].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
                //    {
                //        isKind = "true";
                //    }
                //} "&isKind=" + isKind + "&isLong=" + isLong + 
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    closeDevCls += (closeDevCls == "" ? "" : ",") + vtRes[i].dwClassID;
                    continue;
                }
                ret += "<li class='it' it='devcls' url=\"../a/dftdetail.aspx?classKind=" + vtRes[i].dwKind +"&id=" + vtRes[i].dwClassID +
                    "&name=" + Server.UrlEncode(vtRes[i].szClassName) + "\"><a><span>" + vtRes[i].szClassName + "</span></a></li>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return ret+"</ul></li>";
    }
}