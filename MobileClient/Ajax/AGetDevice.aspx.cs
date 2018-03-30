﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Collections;
public partial class MobileClient_Ajax_ALogin : PageBase
{
    public class DevAjax {
        public uint devID;
        public string DevName;
        public uint uStatus;//1空闲。2忙碌
        public string x;
        public string y;
        public string openTime;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string szOp = Request["op"];
        uint uBeginTime = Parse(Request["dwBeginTime"]);
        uint uEndTime = Parse(Request["dwEndTime"]);
        string uResvDate = Request["resvDate"];
        uint uNeedHour = Parse(Request["NeedHour"]);
        uint timePart = uBeginTime * 10000 + uEndTime;
        uint uPassBeginTime = timePart / 10000;
        uint uPassEndTime = timePart % 10000;
        string szroomid = (Request["szLabID"]);

        if (szOp == "getPostion")
        {
            if (m_Request == null)
            {
                Response.Write("{\"error1\":\"\"}");
                return;
            }
            DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
            vrGet.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;
            if (szroomid != null && szroomid != "")
            {
                vrGet.szRoomIDs = szroomid;
            }
            if (uResvDate == "" || uResvDate == null)
            {
                vrGet.szDates = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                vrGet.szDates = uResvDate.Replace("-", "");
            }
            vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
            vrGet.szReqExtInfo.dwStartLine = 0;
            vrGet.szReqExtInfo.dwNeedLines = 10000;
            vrGet.szReqExtInfo.szOrderKey = "szDevName";
            vrGet.szReqExtInfo.szOrderMode = "desc";
            DEVRESVSTAT[] vtRes;
            m_Request.m_UniDCom.StaSN = 1;
            if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
            {

                ArrayList listPostion = new ArrayList();
                if(vtRes.Length>0)
                listPostion = GetListFromXml(vtRes[0].dwRoomID);
                List<DevAjax> list = new List<DevAjax>();
                for (int i = 0; i < vtRes.Length; i++)
                {
                    DevAjax dev = new DevAjax();
                    dev.uStatus = 1;//忙碌
                    dev.x = "0";
                    dev.y = "0";
                    for (int k = 0; k < listPostion.Count; k++)
                    {
                        devPostion postionTemp = (devPostion)listPostion[k];
                        if (postionTemp.devid == vtRes[i].dwDevID.ToString())
                        {
                            dev.x = postionTemp.x;
                            dev.y = postionTemp.y;
                            break;
                        }
                    }
                    dev.devID = (uint)vtRes[i].dwDevID;
                    dev.DevName = vtRes[i].szDevName;
                    list.Add(dev);
                }

                string szRes = JsonConvert.SerializeObject(list);
                Response.Write(szRes);
                return;
            }
        }
        if (szOp == "get")
        {
            //当做roomid使用需要小心

            if (m_Request == null)
            {
                Response.Write("{\"error\":\"\"}");
                return;
            }
            DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
            vrGet.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;
            if (szroomid != null && szroomid != "")
            {
                vrGet.szRoomIDs = szroomid;
            }
            if (uResvDate == "" || uResvDate == null)
            {
                vrGet.szDates = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                vrGet.szDates = uResvDate.Replace("-", "");
            }
            vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
            vrGet.szReqExtInfo.dwStartLine = 0;
            vrGet.szReqExtInfo.dwNeedLines = 10000;
            vrGet.szReqExtInfo.szOrderKey = "szDevName";
            vrGet.szReqExtInfo.szOrderMode = "desc";
            DEVRESVSTAT[] vtRes;
            m_Request.m_UniDCom.StaSN = 1;
            if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null&&vtRes.Length>0)
            {
             
                ArrayList listPostion = new ArrayList();
                if(vtRes.Length>0)
                listPostion = GetListFromXml(vtRes[0].dwRoomID);
                List<DevAjax> list = new List<DevAjax>();

                for (int i = 0; i < vtRes.Length; i++)
                {

                    DevAjax dev = new DevAjax();
                    dev.uStatus = 2;//忙碌

                    DAYOPENRULE[] openRuleList = vtRes[i].szOpenInfo;

                    uint uBeginMin = 0;
                    for (int k = 0; k < openRuleList.Length; k++)
                    {
                        if (uPassBeginTime < (uint)openRuleList[k].dwBegin)
                        {
                            uPassBeginTime = (uint)openRuleList[k].dwBegin;
                        }
                        if (uPassEndTime > (uint)openRuleList[k].dwEnd)
                        {
                            uPassEndTime = (uint)openRuleList[k].dwEnd;
                        }

                    }

                    string dtNowDate = DateTime.Now.ToString("yyyMMdd");
                    string dateNowHour = DateTime.Now.ToString("HHmm");
                    uint udateNowHour = Parse(dateNowHour);
                    if (dtNowDate == vrGet.szDates.ToString())
                    {
                        if (udateNowHour > uPassBeginTime)
                        {
                            uPassBeginTime = udateNowHour;
                        }

                    }
                    uBeginMin = uPassBeginTime / 100 * 60 + uPassBeginTime % 100;
                    int nLen = (int)(uPassEndTime / 100 * 60 + uPassEndTime % 100) - ((int)(uPassBeginTime / 100 * 60 + uPassBeginTime % 100));
                    if (nLen < 0)
                    {
                        nLen = 0;
                    }
                    uint[] uOpenList = new uint[nLen];
                    for (int k = 0; k < nLen; k++)
                    {
                        uOpenList[k] = 1;//1表示空闲
                    }

                    if (openRuleList != null && openRuleList.Length > 0)
                    {
                        string szTemp = "";
                        for (int m = 0; m < openRuleList.Length; m++)
                        {
                            szTemp += ((uint)openRuleList[m].dwBegin * 10000 + (uint)openRuleList[m].dwEnd).ToString() + ",";
                        }
                        dev.openTime = szTemp;
                    }
                    DEVRESVTIME[] resvTime = vtRes[i].szResvInfo;
                    Logger.trace("info2"+vtRes[i].szDevName.ToString());
                   
                    if (resvTime != null)
                    {
                        for (int m = 0; m < resvTime.Length; m++)
                        {
                            uint uBeginTemp = (uint)resvTime[m].dwBegin;// / 100 * 60 + (uint)resvTime[m].dwBegin%100;
                            uint uEndTemp = (uint)resvTime[m].dwEnd;// / 100 * 60 + (uint)resvTime[m].dwEnd%100;

                            if (uBeginTemp < uPassBeginTime)
                            {
                                uBeginTemp = uPassBeginTime;
                            }
                            if (uEndTemp > uPassEndTime)
                            {
                                uEndTemp = uPassEndTime;
                            }
                            if (dtNowDate == vrGet.szDates.ToString())
                            {
                                if (udateNowHour > uBeginTemp)
                                {
                                    uBeginTemp = udateNowHour;
                                }

                            }
                            uBeginTemp = uBeginTemp / 100 * 60 + uBeginTemp % 100 - uBeginMin;
                            if(uEndTemp / 100 * 60 + uEndTemp % 100 >uBeginMin)
                            {
                                uEndTemp = uEndTemp / 100 * 60 + uEndTemp % 100 - uBeginMin;
                            }
                            for (uint j = uBeginTemp; j < uEndTemp; j++)
                            {
                                if (uOpenList.Length > j)
                                {
                                    uOpenList[j] = 0;//0表示被预约
                                }

                            }
                        }
                    }
                    Logger.trace("info3");
                    uint uCount = 0;
                    for (uint m = 0; m < nLen; m++)
                    {
                        if (uOpenList[m] == 0)//0表示被预约
                        {
                            uCount = uCount + 1;
                        }
                    }

                    dev.x = "0";
                    dev.y = "0";
                    for (int k = 0; k < listPostion.Count; k++)
                    {
                        devPostion postionTemp = (devPostion)listPostion[k];
                        if (postionTemp.devid == vtRes[i].dwDevID.ToString())
                        {
                            dev.x =postionTemp.x;
                            dev.y = postionTemp.y;
                            break;
                        }
                    }
                    dev.devID = (uint)vtRes[i].dwDevID;
                    dev.DevName = vtRes[i].szDevName;
                    dev.uStatus = 1;
                    double fResv = 1 - ((uCount * 1.0) / nLen);//ucount预约数量，n总数量
                    if (fResv <= 0)
                    {
                        dev.uStatus = 0;
                    }
                    else if (fResv > 0 && fResv <= 0.25)
                    {
                        dev.uStatus = 25;
                    }
                    else if (fResv > 0.25 && fResv <= 0.5)
                    {
                        dev.uStatus = 50;
                    }
                    else if (fResv > 0.5 && fResv <= 0.75)
                    {
                        dev.uStatus = 75;
                    }
                    else
                    {
                        dev.uStatus = 100;
                    }
                    list.Add(dev);
                }
           
                 
              /*
                for (int i = 0; i < vtRes.Length; i++)
                {
                    DevAjax dev = new DevAjax();
                    dev.uStatus = 2;//忙碌
             
                    DAYOPENRULE[] openRuleList = vtRes[i].szOpenInfo;
                    uint uBeginMin = 0;
                    if (uPassBeginTime < (uint)openRuleList[0].dwBegin)
                    {
                        uPassBeginTime = (uint)openRuleList[0].dwBegin;
                    }
                    if (uPassEndTime > (uint)openRuleList[0].dwEnd)
                    {
                        uPassEndTime = (uint)openRuleList[0].dwEnd;
                    }

                    string dtNowDate = DateTime.Now.ToString("yyyMMdd");
                    string dateNowHour = DateTime.Now.ToString("HHmm");
                    uint udateNowHour = Parse(dateNowHour);
                    if (dtNowDate == vrGet.szDates.ToString())
                    {
                        if (udateNowHour > uPassBeginTime)
                        {
                            uPassBeginTime = udateNowHour;
                        }

                    }
                    uBeginMin = uPassBeginTime / 100 * 60 + uPassBeginTime % 100;
                    int nLen = (int)(uPassEndTime / 100 * 60 + uPassEndTime % 100) - ((int)(uPassBeginTime / 100 * 60 + uPassBeginTime % 100));
                    if (nLen < 0)
                    {
                        nLen = 0;
                    }
                    uint[] uOpenList = new uint[nLen];
                    for (int k = 0; k < nLen; k++)
                    {
                        uOpenList[k] = 1;//1表示空闲
                    }

                    if (openRuleList != null && openRuleList.Length > 0)
                    {
                        string szTemp = "";
                        for (int m = 0; m < openRuleList.Length; m++)
                        {
                            szTemp += ((uint)openRuleList[m].dwBegin * 10000 + (uint)openRuleList[m].dwEnd).ToString() + ",";
                        }
                        dev.openTime = szTemp;
                    }
                    DEVRESVTIME[] resvTime = vtRes[i].szResvInfo;
                    if (resvTime != null)
                    {

                        for (int m = 0; m < resvTime.Length; m++)
                        {
                            uint uBeginTemp = (uint)resvTime[m].dwBegin;// / 100 * 60 + (uint)resvTime[m].dwBegin%100;
                            uint uEndTemp = (uint)resvTime[m].dwEnd;// / 100 * 60 + (uint)resvTime[m].dwEnd%100;

                            if (uBeginTemp < uPassBeginTime)
                            {
                                uBeginTemp = uPassBeginTime;
                            }
                            if (uEndTemp > uPassEndTime)
                            {
                                uEndTemp = uPassEndTime;
                            }
                            if (dtNowDate == vrGet.szDates.ToString())
                            {
                                if (udateNowHour > uBeginTemp)
                                {
                                    uBeginTemp = udateNowHour;
                                }

                            }
                            uBeginTemp = uBeginTemp / 100 * 60 + uBeginTemp % 100 - uBeginMin;
                            uEndTemp = uEndTemp / 100 * 60 + uEndTemp % 100 - uBeginMin;
                            for (uint j = uBeginTemp; j < uEndTemp; j++)
                            {
                                if (uOpenList.Length > j)
                                {
                                    uOpenList[j] = 0;//0表示被预约
                                }

                            }
                        }
                    }
                    uint uCount = 0;
                    for (uint m = 0; m < nLen; m++)
                    {
                        if (uOpenList[m] == 0)//0表示被预约
                        {
                            uCount = uCount + 1;
                        }
                    }
                    
                    dev.x = "0";
                    dev.y = "0";
                    for (int k = 0; k < listPostion.Count; k++)
                    {
                        devPostion postionTemp = (devPostion)listPostion[k];
                        if (postionTemp.devid == vtRes[i].dwDevID.ToString())
                        {
                            dev.x = (FloatParse(postionTemp.x) + 13).ToString();
                            dev.y = postionTemp.y;
                            break;
                        }
                    }
                    dev.devID = (uint)vtRes[i].dwDevID;
                    dev.DevName = vtRes[i].szDevName;
                    dev.uStatus = 1;
                    double fResv = 1 - ((uCount * 1.0) / nLen);//ucount预约数量，n总数量
                    if (fResv <= 0)
                    {
                        dev.uStatus = 0;
                    }
                    else if (fResv > 0 && fResv <= 0.25)
                    {
                        dev.uStatus = 25;
                    }
                    else if (fResv > 0.25 && fResv <= 0.5)
                    {
                        dev.uStatus = 50;
                    }
                    else if (fResv > 0.5 && fResv <= 0.75)
                    {
                        dev.uStatus = 75;
                    }
                    else
                    {
                        dev.uStatus = 100;
                    }
                    list.Add(dev);
                }
              */
                string szRes = JsonConvert.SerializeObject(list);
                Response.Write(szRes);
                return;

            }
            else
            {
                Response.Write("{\"error\":\"" + m_Request.szErrMessage + "\"}");
            }

        }
        else if (szOp == "save")
        {
            string devid = Request["devid"];
            string x = Request["x"];
            string y = Request["y"];
            SavedevPostion(devid, x, y);
        }
        else if (szOp == "getSingnal")
        {
            uint uDevID = Parse(Request["devid"]);
            DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
            vrGet.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;

            if (uResvDate == "" || uResvDate == null)
            {
                vrGet.szDates = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                vrGet.szDates = uResvDate.Replace("-", "");
            }
            vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
            if (uDevID != 0)
            {
                vrGet.dwDevID = uDevID;
            }
            else
            {
                Response.Write("获取预约信息失败");
            }
            vrGet.szReqExtInfo.dwStartLine = 0;
            vrGet.szReqExtInfo.dwNeedLines = 10000;
            vrGet.szReqExtInfo.szOrderKey = "szDevName";
            vrGet.szReqExtInfo.szOrderMode = "desc";
            DEVRESVSTAT[] vtRes;
            m_Request.m_UniDCom.StaSN = 1;
            string szResvTime = "空闲";
            if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                DEVRESVTIME[] resvTime = vtRes[0].szResvInfo;
                if (resvTime != null && resvTime.Length > 0)
                {
                    szResvTime = "";
                    for (int i = 0; i < resvTime.Length; i++)
                    {
                        uint uEnd = (uint)resvTime[i].dwEnd;
                        uint uBegin = (uint)resvTime[i].dwBegin;
                        szResvTime += uBegin / 100 + ":" + (uBegin % 100).ToString("00") + "-" + uEnd / 100 + ":" + (uEnd % 100).ToString("00") + "，";
                    }
                }
            }
            Response.Write(szResvTime);
        }
    }
    public XmlDocument xml = new XmlDocument();
    void LoadXml()
    {
        string strPaht = HttpRuntime.AppDomainAppPath;
        string szXmlName = "MobileClient/DevPostion.xml";
        try
        {
            xml.Load(strPaht + szXmlName);
        }
        catch
        { }
    }
    public class devPostion
    {
        public string devid;
        public string x;
        public string y;
    }
    public ArrayList GetListFromXml(uint? rm)
    {
        XmlCtrl ctrl = new XmlCtrl("ics_data",Server.MapPath("~/clientweb/upload/info/xmlData/"));
        XmlCtrl.XmlInfo info = ctrl.GetXmlContent(rm.ToString(), "rm_coorb");
        string str = "";
if(info.content==null) return new ArrayList();
        str = info.content;
        //解析
        ArrayList boxs = new ArrayList();
        string[] tmp = str.Split('&');
        if (tmp.Length > 1)
        {
            for (int i = 3; i < tmp.Length; i++)
            {
                string[] m = tmp[i].Split(',');
                if (m.Length > 3)
                {
                    devPostion b = new devPostion();
                    b.devid = m[0];
                    b.y =  m[1].Substring(0,m[1].Length-2);
                    b.x =  m[2].Substring(0,m[2].Length-2);
                    boxs.Add(b);
                }
            }
        }
        return boxs;
    }
    public bool SavedevPostion(string devid,string x,string y)
    {
        if (xml == null)
        {
            return false;
        }
        LoadXml();
        XmlNode nodeRoot = xml.SelectSingleNode("//field[@devid='" + devid + "']");
        if (nodeRoot == null)
        {
            //新建
            XmlNode root = xml.SelectSingleNode("const");//查找<bookstore>
            XmlElement xe1 = xml.CreateElement("field");//创建一个<book>节点
            xe1.SetAttribute("devid", devid);//设置该节点genre属性
            xe1.SetAttribute("x", x);//设置该节点ISBN属性
            xe1.SetAttribute("y",y);//设置该节点ISBN属性
            root.AppendChild(xe1);//添加到<bookstore>节点中
             
         
        }
        else
        {
            XmlElement xe = (XmlElement)nodeRoot;
            xe.SetAttribute("x", x);//设置该节点ISBN属性
            xe.SetAttribute("y", y);//设置该节点ISBN属性
            //修改
        }
        try
        {
            string strPaht = HttpRuntime.AppDomainAppPath;
            string szXmlName = "MobileClient/DevPostion.xml";
            xml.Save(strPaht + szXmlName);
            return true;
        }
        catch
        { }
        return false;
    }
}