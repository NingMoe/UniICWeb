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
    uint rscMode;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetConfig("visitMode") == "login")//访问模式为login
            Response.Redirect("Login.aspx");
        rscMode = ToUInt(GetConfig("resourceMode"));
        islogin = IsLogined();
        //教学转普通预约系统 重新登录
        if (islogin && Request["login"] == "relogin")
        {
            if (IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_IC))
                Response.Redirect("Default.aspx");
        }
        string lg = GetConfig("mustLogin");
        if (lg == "1" && !islogin)
            Response.Redirect("Login.aspx?sys=person");
        InitItemCls();
        if ((rscMode&1)>0)
        InitDevCls();
        if ((rscMode & 4) > 0)
        InitRoom();
        if ((rscMode & 8) > 0)
        InitDept();
    }

    private void InitDept()
    {
        DEPTREQ req = new DEPTREQ();
        UNIDEPT[] rlt;
        if (m_Request.Account.DeptGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEPT dept = rlt[i];
                itemList += "<li it='dept' url=\"../a/courselist.aspx?deptId=" + dept.dwID + "&deptName=" +Server.UrlEncode( dept.szName) + "\"><a><span>" +dept.szName + "</span></a></li>";
            }
        }
    }

    private void InitRoom()
    {
        ROOMREQ req = new ROOMREQ();
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                itemList += "<li it='lab_"+rm.dwLabID+"' url=\"../a/roomdetail.aspx?roomId=" + rm.dwRoomID + "&roomName="+Server.UrlEncode( rm.szRoomName)+"\"><a><span>" + rm.szRoomName + "</span></a></li>";
            }
        }
    }

    private void InitItemCls()
    {
        //设备类别 类别
        if ((rscMode & 1) > 0)
        itemClsList += "<option value='devcls' selected>设备类别</option>";
        //实验室 房间
        if ((rscMode & 4) > 0)
        {
            LABREQ req = new LABREQ();
            UNILAB[] rlt;
            if (m_Request.Device.LabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    itemClsList += "<option value='lab_" + rlt[i].dwLabID + "'>" + rlt[i].szLabName + "</option>";
                }
            }
        }
        //部门
        if ((rscMode & 8) > 0)
        itemClsList += "<option value='dept'>开课部门</option>";
    }
    private void InitDevCls()
    {
        uint clsKind = ToUInt(GetConfig("openClsKind"));
        if (clsKind == 0)
            GetDevCls(0);
        else
        {
            if ((clsKind & 1) > 0)
                GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
            if ((clsKind & 2) > 0)
                GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
            if ((clsKind & 4) > 0)
                GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN);
            if ((clsKind & 8) > 0)
                GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        }
    }
    private void GetDevCls(uint kind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        if(kind!=0)
        vrGet.dwKind = kind;
        UNIDEVCLS[] vtRes;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    continue;
                }
                itemList += "<li it='devcls' url=\"../a/dftdetail.aspx?classKind=" + vtRes[i].dwKind +"&id=" + vtRes[i].dwClassID +
                    "&name="+Server.UrlEncode(vtRes[i].szClassName)+"\"><a><span>"+ vtRes[i].szClassName + "</span></a></li>";
            }
        }
    }
}