﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using UniWebLib;

public partial class _Default : UniPage
{
	public bool m_nLocSigned = false;
	
    public string m_szTitle = "";
    public string m_szMsg = "";
    public string m_szMsg2 = "";
	public string m_szType = "";
    public string m_szOther = "";
    public string m_status = "0";
    public string validateServer = "http://update.unifound.net/unialipay/BindSchoolAli.aspx";
    //http://update.unifound.net/wxnotice/qrcode.aspx?pcid=1&id=1&session=Seat&msg=座位号:001

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["ls"] == "true")
        {
            Session["LocSigned"] = DateTime.Now;
        }

       
       
        m_nLocSigned = true;//禁用位置认证
                            /*
                                 if(Session["LocSigned"] == null || (DateTime.Now - (DateTime)Session["LocSigned"]).TotalSeconds > 300)
                                 {
                                     m_nLocSigned = false;
                                     return;
                                 }else{
                                     m_nLocSigned = true;
                                 }
                             */
        uint dwStaSN = 1;
        string sta = Request["sta"];
        if (string.IsNullOrEmpty(sta))
        {
            dwStaSN = 1;
        }
        else
        {
            dwStaSN = ToUint(sta);
        }

        m_Request.m_UniDCom.StaSN = dwStaSN;

        string aliUserid = Request["msn"];
        string szSchoolCode = Request["sysid"];
        string szwxuserid = Request["wxuseridform"];
        if (!string.IsNullOrEmpty(aliUserid))
        {
            if (Request.UserAgent.IndexOf("MicroMessenger") < 0)//
            {
                Session["aliUserid"] = aliUserid;
            }
            else {
                Session["szwxuserid"] = aliUserid;
            }
            
        }
        
        if (!string.IsNullOrEmpty(szSchoolCode))
        {
            Session["szSchoolCode"] = szSchoolCode;
        }
       

        if (Request["DoUserIn"] == "true")
        {

            MOBILEUSERINREQ req = new MOBILEUSERINREQ();
            MOBILEUSERINRES res;
            req.dwUseMin = ToUint(Request["dwUseMin"]);
            REQUESTCODE uResponse = m_Request.Console.MobileUserIn(req, out res);
            Logger.trace("MobileUserIn");
            if (res.szDispInfo != null && res.szDispInfo != "")
            {
                res.szDispInfo = res.szDispInfo.Replace("设备", "座位");
                res.szDispInfo = res.szDispInfo.Replace("微信", "该");
            }
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                m_szTitle = "操作成功";
                m_szMsg = res.szDispInfo;
                m_szType = "1";
            }
            else
            {
                m_szTitle = "操作失败";
                m_szMsg = res.szDispInfo;
                m_szType = "0";
            }
        }
        else if (Request["DoUserDelay"] == "true")
        {

            MOBILEDELAYREQ req = new MOBILEDELAYREQ();
            MOBILEDELAYRES res;
            req.dwDelayMin = ToUint(Request["dwDelayMin"]);
            REQUESTCODE uResponse = m_Request.Console.MobileDelay(req, out res);
            res.szDispInfo = res.szDispInfo.Replace("设备", "座位");
            res.szDispInfo = res.szDispInfo.Replace("微信", "该");
            Logger.trace("MobileDelay");
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                m_szTitle = "操作成功";
                m_szMsg = res.szDispInfo;
                m_szType = "1";
            }
            else
            {
                m_szTitle = "操作失败";
                m_szMsg = res.szDispInfo;
                m_szType = "0";
            }
        }
        else if (Request["DoUserOut"] == "1")
        {

            MOBILEUSEROUTREQ req = new MOBILEUSEROUTREQ();
            MOBILEUSEROUTRES res;
            req.dwOutType = (uint)MOBILEUSEROUTREQ.DWOUTTYPE.MSUSEROUT_LEAVE;
            REQUESTCODE uResponse = m_Request.Console.MobileUserOut(req, out res);
            res.szDispInfo = res.szDispInfo.Replace("设备", "座位");
            res.szDispInfo = res.szDispInfo.Replace("微信", "该");
            Logger.trace("MobileUserOut");
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                m_szTitle = "操作成功";
                DateTime timenow = DateTime.Now.AddMinutes(double.Parse(res.szDispInfo));
                m_szMsg = "请在【" + timenow.ToString("HH:mm") + "】前返回";
                m_szType = "1";
            }
            else
            {
                m_szTitle = "操作失败";
                m_szMsg = res.szDispInfo;
                m_szType = "0";
            }
        }
        else if (Request["DoUserOut"] == "2")
        {
            MOBILEUSEROUTREQ req = new MOBILEUSEROUTREQ();
            MOBILEUSEROUTRES res;
            req.dwOutType = (uint)MOBILEUSEROUTREQ.DWOUTTYPE.MSUSEROUT_EXIT;
            REQUESTCODE uResponse = m_Request.Console.MobileUserOut(req, out res);
            if (res.szDispInfo != null && res.szDispInfo != "")
            {
                res.szDispInfo = res.szDispInfo.Replace("设备", "座位");
                res.szDispInfo = res.szDispInfo.Replace("微信", "该");
            }
            Logger.trace("MobileUserOut2");
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                m_szTitle = "操作成功";
                m_szMsg = res.szDispInfo;
                m_szType = "1";
            }
            else
            {
                m_szTitle = "操作失败";
                m_szMsg = res.szDispInfo;
                m_szType = "0";
            }
        }
        else
        {

            string lab = Request["lab"];
            string dev = Request["dev"];
            string msn = Request["msn"];

            MOBILESCANREQ req = new MOBILESCANREQ();
            MOBILESCANRES res;

            if (Request["DoLogon"] == "true")
            {
                if (Session["mobilescanreq"] == null)
                {
                    m_szTitle = "开门失败";
                    m_szMsg = "请重试";
                    m_szType = "0";
                    Response.Redirect("WxOpenDoorMsg.aspx?type=" + m_szType + "&title=" + Server.UrlEncode(m_szTitle) + "&msg=" + Server.UrlEncode(m_szMsg));
                    return;
                }
                req = (MOBILESCANREQ)Session["mobilescanreq"];
                req.szLogonName = Request["szLogonName"];
                req.szPassword = "P" + Request["szPassword"];
                if (Request["dwBind"] == "1")
                {
                    req.dwProperty = (uint)MOBILEOPENDOORREQ.DWPROPERTY.MODPROP_BINDMSN;
                }
            }
            else
            {
                req.dwStaSN = dwStaSN;
                req.dwLabID = ToUint(lab);
                req.dwDevID = ToUint(dev);
                req.szMSN = msn;
                req.szIP = GetRealIP();

                Session["mobilescanreq"] = req;
            }
            m_Request.m_UniDCom.StaSN = dwStaSN;

            req.szIP = GetRealIP();// "192.168.3.299";
            REQUESTCODE uResponse = m_Request.Console.MobileScan(req, out res);
            string szschoolCode = Request["sysidform"];
            string szUserID = Request["aluseridform"];
            
            string szLogonName = Request["szLogonName"];
            if (string.IsNullOrEmpty(szLogonName)&&res.szPID!=null&&res.szPID!="")
            {
                szLogonName = res.szPID;
            }
            
            if (Session["aliUserid"]!=null)
            {
                szUserID = Session["aliUserid"].ToString();
            }
            if (Session["szSchoolCode"] != null)
            {
                szschoolCode=Session["szSchoolCode"].ToString();
            }
            if (Session["szwxuserid"] != null)
            {
                szwxuserid = Session["szwxuserid"].ToString();
            }

            if (Request.UserAgent.IndexOf("MicroMessenger") < 0)//
            {
                if (!string.IsNullOrEmpty(szschoolCode) && !string.IsNullOrEmpty(szUserID) && !string.IsNullOrEmpty(szLogonName) && uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Logger.trace("szschoolCode=" + szschoolCode + ",szUserID=" + szUserID + ",szLogonName=" + szLogonName);
                    if (!string.IsNullOrEmpty(szschoolCode) && !string.IsNullOrEmpty(szLogonName) && !string.IsNullOrEmpty(szLogonName))
                    {
                        BindUniCloud(szLogonName, szUserID, szschoolCode,"");
                    }
                }
            }
            else {

                if (!string.IsNullOrEmpty(szschoolCode) && !string.IsNullOrEmpty(szwxuserid) && !string.IsNullOrEmpty(szLogonName) && uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Logger.trace("szschoolCode=" + szschoolCode + ",szwxuserid=" + szwxuserid + ",szLogonName=" + szLogonName);
                    if (!string.IsNullOrEmpty(szschoolCode) && !string.IsNullOrEmpty(szLogonName) && !string.IsNullOrEmpty(szLogonName))
                    {
                        BindUniCloud(szLogonName, "", szschoolCode,szwxuserid);
                    }
                }
            }
            Logger.trace("MobileScan");
            if (res.dwSessionID != null && res.dwSessionID != 0)
            {
                m_Request.m_UniDCom.SessionID = (uint)res.dwSessionID;
            }

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_SIGNOK) != 0)
            {
                m_szTitle = "签到成功";
                m_szMsg = res.szDispInfo;
                m_szType = "1";

                //if (Request["dwBind"] == "1")
                //{
                //    m_szMsg2 = "√ 已绑定此微信号";
                //}
            }
            else if (uResponse == REQUESTCODE.ERR_MSN_NOBIND)
            {
                m_szType = "2";
                m_szTitle = "未绑定用户";
                m_szMsg = "请输入账号和密码,绑定用户";
            }
            else if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if ((res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_IDLE) != 0 && (res.dwMaxUseMin != 0 && res.dwMaxUseMin >= res.dwMinUseMin))
                {
                    //设备空闲(使用需调用MSREQ_MOBILE_USERIN）
                    m_szTitle = "可用";
                    //m_szMsg = res.szDispInfo;
                    if (!string.IsNullOrEmpty(m_szMsg))
                    {
                        m_szMsg += ",";
                    }
                    m_szMsg += "空闲:请选择使用时长，离开请扫码";

                    m_szType = "3";

                    m_szOther = "&dwMinUseMin=" + res.dwMinUseMin + "&dwMaxUseMin=" + res.dwMaxUseMin + "&szTrueName=" + Server.UrlEncode(res.szTrueName) + "&szDevName=" + Server.UrlEncode(res.szDevName);
                }
                else if ((res.dwUserStat & ((uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_INUSE | (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_CANDELAY)) != 0)
                {
                    //使用中(可调用MSREQ_MOBILE_USEROUT进行后续操作）
                    m_szTitle = "当前使用中";
                    m_szMsg = res.szDispInfo;
                    m_szType = "4";
                    if ((res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_CANDELAY) != 0)
                    {
                        m_szType = "8";
                        m_szOther = "&dwMinUseMin=" + res.dwMinUseMin + "&dwMaxUseMin=" + res.dwMaxUseMin + "&szTrueName=" + Server.UrlEncode(res.szTrueName) + "&szDevName=" + Server.UrlEncode(res.szDevName);
                    }
                }
                else if ((res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_EXIT) != 0)
                {
                    //刷卡退出成功（无需后续操作）
                    m_szTitle = "刷卡退出成功";
                    m_szMsg = res.szDispInfo;
                    m_szType = "1";
                }
                else if ((res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_GOBACK) != 0)
                {
                    //暂时离开返回成功(无需后续操作）
                    m_szTitle = "暂时离开返回成功";
                    m_szMsg = res.szDispInfo;
                    m_szType = "1";
                }
                else if ((res.dwUserStat & (uint)MOBILESCANRES.DWUSERSTAT.MSUSERSTAT_RESVUNDO) != 0)
                {
                    //预约未生效(无需后续操作）
                    m_szTitle = "预约未生效";
                    m_szMsg = res.szDispInfo;
                    m_szMsg=m_szMsg.Replace("-", null);
                    m_status = "1";
                    m_szType = "0";
                }
                else
                {
                    m_szTitle = "操作失败";
                    m_szMsg = res.szDispInfo;
                    m_szType = "0";
                }
            }
            else
            {
                m_szTitle = "操作失败";
                m_szMsg = res.szDispInfo;
                m_szType = "0";
            }
        }
        //提前预约
        if (Request["Advance"] == "true")
        {
            MOBILEUSERINREQ req = new MOBILEUSERINREQ();
            MOBILEUSERINRES res;
            REQUESTCODE uResponse = m_Request.Console.MobileUserIn(req, out res);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS) {
                m_szTitle = "提前预约成功";
                m_szMsg = res.szDispInfo;
                m_szType = "32";
            }
            else
            {
                m_szTitle = "提前预约失败";
                m_szMsg = res.szDispInfo;
                m_szType = "32";
            }

        }

        if (m_szType == "0")
        {
            if (string.IsNullOrEmpty(m_szMsg))
            {
                if (string.IsNullOrEmpty(m_Request.szErrMessage) || m_Request.szErrMessage.IndexOf("Socket") > 0)
                {
                    m_szMsg = "操作失败，请重试";
                }
                else
                {
                    m_szMsg = m_Request.szErrMessage;
                }
            }
            if (string.IsNullOrEmpty(m_szMsg))
            {
                m_szMsg = "操作失败，请重试";
            }
        }
        //  Response.Write("WxSeatSignMsg.aspx?type=" + m_szType + "&title=" + Server.UrlEncode(m_szTitle) + "&msg=" + Server.UrlEncode(m_szMsg) + "&msg2=" + Server.UrlEncode(m_szMsg2) + m_szOther);
        // Response.End();
        
        if (!string.IsNullOrEmpty(aliUserid) &&!string.IsNullOrEmpty(szSchoolCode))
        {
            if (Request.UserAgent.IndexOf("MicroMessenger") < 0)
            {
                Response.Redirect("WxSeatSignMsg.aspx?type=" + m_szType + "&aliUserid=" + aliUserid + "&sysid=" + szSchoolCode + "&title=" + Server.UrlEncode(m_szTitle) + "&msg=" + Server.UrlEncode(m_szMsg) + "&msg2=" + Server.UrlEncode(m_szMsg2) + m_szOther);
            }
            else {
                Response.Redirect("WxSeatSignMsg.aspx?type=" + m_szType + "&wxUserid=" + aliUserid + "&sysid=" + szSchoolCode + "&title=" + Server.UrlEncode(m_szTitle) + "&msg=" + Server.UrlEncode(m_szMsg) + "&msg2=" + Server.UrlEncode(m_szMsg2) + m_szOther);
            }
            
        }
        else {
            Response.Redirect("WxSeatSignMsg.aspx?type=" + m_szType + "&title=" + Server.UrlEncode(m_szTitle) + "&msg=" + Server.UrlEncode(m_szMsg) + "&msg2=" + Server.UrlEncode(m_szMsg2) + m_szOther+ "&status="+ m_status);
        }
    }
	
	protected string GetRealIP()
    {
        try
        {
            string ip = "";
            if (Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        catch (Exception)//e)
        {
            //throw e;
        }
        return "";
    }
    public void BindUniCloud(string szLogonName, string userid, string szschoolCode, string szwxuserid)
    {
        string validateUrl = "";
        if (userid != null && userid != "")
        {
            validateUrl = validateServer + "?schoolCode=" + szschoolCode + "&logonName=" + szLogonName + "&userid=" + userid;
        }
        else if (szwxuserid != null && szwxuserid != "")
        {
            validateUrl = validateServer + "?schoolCode=" + szschoolCode + "&logonName=" + szLogonName + "&wxuserid=" + szwxuserid;
        }
        if (validateUrl == "")
        {
            return;
        }
        WebClient client = new WebClient();

        StreamReader Reader = new StreamReader(client.OpenRead(validateUrl));
        string resp = Reader.ReadToEnd();
        client.Dispose();
        Logger.trace("resp:" + resp);

    }
}
