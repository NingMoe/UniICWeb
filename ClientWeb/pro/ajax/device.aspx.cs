using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Xml;
using UniWebLib;
using Util;
using System.Collections;

public partial class ClientWeb_pro_ajax_device : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "dev_filter")
            {
                Filter fl = InitFilter();
                DevFilter(fl);
            }
            else if (act == "get_rsv_sta")
            {
                GetRsvSta();
            }
            else if (act == "get_use_stat")
            {
                if (IsLoginReady())
                {
                    GetUseStat();
                }
            }
            else if (act == "dev_rsv_rule")//未使用
            {
                string id = Request["dev_id"];
                string kind = Request["kind_id"];
                string purpose = Request["purpose"];
                string date = Request["date"];
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(date))
                    ErrMsgP();
                DEVRESVSTAT[] rlt = GetDevRsvSta(date.Replace("-", ""), id);//,ToUInt(purpose)
                if (rlt != null && rlt.Length > 0)
                {
                    SucRlt(ConvertDevResvSta(rlt[0], date.Split(',').Length > 1,date));
                }
                else
                    ErrMsg("获取设备预约属性失败");
            }
            else if (act == "get_dev_coord")
            {
                GetDevCoord();
            }
            else if (act == "set_dev_coord")
            {
                if(IsLoginReady())
                {
                string lab_id = Request["lab_id"];
                string room_id = Request["room_id"];
                string class_id = Request["class_id"];
                string data = "\"" + Request["data"] + "\"";
                bool ret = false;
                if (!string.IsNullOrEmpty(lab_id))
                {
                    ret = SaveXmlData(lab_id, data, "lab_coorb");
                }
                else if (!string.IsNullOrEmpty(room_id))
                {
                    ret = SaveXmlData(room_id, data, "rm_coorb");
                }
                else if (!string.IsNullOrEmpty(class_id))
                {
                    ret = SaveXmlData(class_id, data, "cls_coorb");
                }
                string bk=SetDevCoord(lab_id, data);
                if (ret &&(bk =="ok"))//坐标同步到数据库共享给其它系统
                    SucMsg();
                else
                    ErrMsg("保存坐标信息失败。"+bk);
                }
            }
            else if (act == "clear_dev_coord")
            {
                string type = Request["type"];
                if (string.IsNullOrEmpty(type))
                {
                    ErrMsgP();
                    return;
                }
                if (DelXmlData(Request["id"], type))
                    SucMsg();
                else
                    ErrMsg();
            }
            //else if (act == "ck_free")
            //{
            //    string id = Request["dev_id"];
            //    string dates = Request["date"];
            //    string start = Request["start"];
            //    string end = Request["end"];
            //    if (id == null || dates == null || start == null || end == null)
            //    {
            //        ErrMsgP();
            //        return;
            //    }
            //    string[] starts = start.Split(',');
            //    string[] ends = end.Split(',');
            //    uint[] time = StringToUintArr(starts, ends);
            //    string[] dts = dates.Split(',');
            //    string nofree = "";
            //    for (int i = 0; i < dts.Length; i++)
            //    {
            //        if (dts[i] != "")
            //        {
            //            DateTime dt = DateTime.Parse(dts[i] + " 00:00");
            //            if (!getDevFreeStat(, time))
            //                nofree += dts[i] + ",";
            //        }
            //    }
            //    SucRlt("\"" + nofree + "\"");
            //}
        }
    }

    private string SetDevCoord(string id, string data)
    {
        if (Session["LoginUseInfo"] != null)
        {
            LoginUseInfo info = (LoginUseInfo)Session["LoginUseInfo"];
            Logout(curAcc.dwAccNo, curAcc.szLogonName);
            if (Login(info.szLogoName, info.szPassword, (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER))
            {
                string d = data.Substring(1, data.Length - 1);
                string[] tmp = d.Split('&');
                if (tmp.Length > 1)
                {
                    string w = tmp[0];//宽
                    string h = tmp[1];//高
                    for (int i = 3; i < tmp.Length; i++)
                    {
                        string[] m = tmp[i].Split(',');
                        if (m.Length > 3)
                        {
                            UNIDEVICE set = new UNIDEVICE();
                            set.dwDevID = ToUInt(m[0]);
                            set.szExtInfo = w + "," + h + "," + m[2].Substring(0, m[2].Length - 2) + "," + m[1].Substring(0, m[1].Length - 2);
                            if (m_Request.Device.Set(set, out set) != REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                return m_Request.szErrMsg;
                            }
                        }
                    }
                }
                Logout(curAcc.dwAccNo, curAcc.szLogonName);
                Login(info.szLogoName, info.szPassword);
                return "ok";
            }
            else
                return m_Request.szErrMsg;
        }
        return "未登录";
    }

    private void GetUseStat()
    {
        string id=Request["resv_id"];
        if (string.IsNullOrEmpty(id))
        {
            ErrMsg("缺少必要参数");
            return;
        }
        DEVREQ req = new DEVREQ();
        req.dwResvID = ToUInt(id);
        req.dwReqProp = (uint)DEVREQ.DWREQPROP.DEVREQ_NEEDDEVUSE;
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string str = "";
            for (int i = 0; i < rlt.Length; i++)
            {
            UNIDEVICE dev = rlt[i];
            string deadline = "";
            bool leave=(dev.dwRunStat & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0;
            if (leave && dev.DevUse != null && dev.DevUse.Length > 0)
            {
                deadline = DateTime.Parse(Get1970Date(dev.DevUse[0].dwLeaveTime)).AddSeconds((double)dev.DevUse[0].dwLeaveHoldSec).ToString("yyyy-MM-dd HH:mm");
            }
            str += "{\"devId\":\"" + dev.dwDevID + "\",\"devName\":\"" + dev.szDevName + "\",\"classkind\":" + dev.dwClassKind +
                ",\"code\":" + dev.dwRunStat + ",\"leave\":" + (leave ? "true" : "false") + ",\"status\":\"" + Util.Converter.GetDevRunStat((uint)dev.dwRunStat) + "\",\"deadline\":\"" + deadline + "\"},";
            }
            if (str.Length > 0) str = str.Substring(0, str.Length - 1);
            SucRlt("["+str+"]");
        }
        else
            SucRlt("[]");
    }

    private void GetDevCoord()
    {
        string lab_id = Request["lab_id"];
        string room_id = Request["room_id"];
        string class_id = Request["class_id"];
        string class_kind=Request["class_kind"];
        DEVREQ req = new DEVREQ();
        string str = "";
        if (!string.IsNullOrEmpty(lab_id))
        {
            req.szLabIDs = lab_id;
            str = GetXmlContent(lab_id, "lab_coorb");
        }
        if (!string.IsNullOrEmpty(room_id))
        {
            req.szRoomIDs = room_id;
            str = GetXmlContent(room_id, "rm_coorb");
        }
        if (!string.IsNullOrEmpty(class_id))
        {
            req.szClassIDs = class_id;
            if (str == "")
                str = GetXmlContent(class_id, "cls_coorb");
        }
        if(!string.IsNullOrEmpty(class_kind)){
        req.dwClassKind = ToUInt(class_kind);
        }
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            //解析
            uint w = 0;
            uint h = 0;
            string show = "true";
            List<box> boxs = new List<box>();
            string[] tmp = str.Split('&');
            if (tmp.Length > 1)
            {
                w = ToUInt(tmp[0]);//宽
                h = ToUInt(tmp[1]);//高
                show = tmp[2];//是否显示名称
                for (int i = 3; i < tmp.Length; i++)
                {
                    string[] m = tmp[i].Split(',');
                    if (m.Length > 3)
                    {
                        box b = new box();
                        b.id = m[0];
                        b.top = m[1];
                        b.left = m[2];
                        b.size = m[3];
                        boxs.Add(b);
                    }
                }
            }
            List<unidev> list = new List<unidev>();
            for (int i = 0; i < rlt.Length; i++)
            {
                unidev dev = new unidev();
                dev.id = rlt[i].dwDevID.ToString();
                dev.name = rlt[i].szDevName;
                for (int j = 0; j < boxs.Count; j++)
                {
                    if (dev.id == boxs[j].id)
                    {
                        dev.top = boxs[j].top;
                        dev.left = boxs[j].left;
                        dev.size = boxs[j].size;
                        break;
                    }
                }
                list.Add(dev);
            }
            SucRlt("{\"height\":" + h + ",\"width\":" + w + ",\"istitle\":" + show + ",\"objs\":" + JsonConvert.SerializeObject(list) + "}");
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }
    struct box
    {
        public string id;
        public string top;
        public string left;
        public string size;
    }

    private void GetRsvSta()
    {
        string classKind = Request["classkind"];
        string iskind = Request["iskind"];
        string islong = Request["islong"];
        uint prop = ToUInt(Request["prop"]);
        uint classId = ToUInt(Request["class_id"]);
        uint kindId = ToUInt(Request["kind_id"]);
        if (string.IsNullOrEmpty(iskind) && string.IsNullOrEmpty(islong))
        {
            if (prop > 0)
            {
                if ((prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0) islong = "true";
                if ((prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0) iskind = "true";
            }
            else if (kindId > 0)
            {
                UNIDEVKIND kind = GetDevKind(kindId);
                if (kind.dwProperty != null && kind.dwProperty > 0)
                {
                    if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 && GetConfig("resvAllDay") == "1") islong = "true";
                    if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0) iskind = "true";
                }
            }
            else if (classId > 0)
            {
                DEVKINDREQ req = new DEVKINDREQ();
                if (!string.IsNullOrEmpty(classKind)&&classKind!="0")
                    req.dwClassKind = ToUInt(classKind);
                UNIDEVKIND[] kinds;
                if (m_Request.Device.DevKindGet(req, out kinds) == REQUESTCODE.EXECUTE_SUCCESS && kinds.Length > 0)
                {
                    for (int i = 0; i < kinds.Length; i++)
                    {
                        if (kinds[i].dwClassID == ToUInt(classId))
                        {
                            if ((kinds[i].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 && GetConfig("resvAllDay") == "1") islong = "true";
                            if ((kinds[i].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0) iskind = "true";
                            break;
                        }
                    }
                }
            }
        }
        bool bydev = true;
        bool bykind = false;
        bool isLong = (!string.IsNullOrEmpty(islong) && islong.ToLower() == "true");
        List<devResvSta> rlt = new List<devResvSta>();
        if (!string.IsNullOrEmpty(iskind))
        {
            bykind = (iskind == "true");
            bydev = !bykind;
        }
        if (bydev)
        {
            List<devResvSta> list;
            string ret;
            if (isLong)
            {
                ret = getLongDevResvState(out list);
            }
            else
            {
                ret = GetDevResvState(out list);
            }
            if (ret == "ok") rlt.AddRange(list);
            else { ErrMsg(ret); return; }
        }
        if (bykind)
        {
            List<devResvSta> list2;
            string ret;
            if (isLong)
            {
                ret = getDevKindLongResvState(out list2);
            }
            else
            {
                ret = getDevKindResvState(out list2);
            }
            if (ret == "ok") rlt.AddRange(list2);
            else { ErrMsg(ret); return; }
        }
        SucRlt(rlt);
    }
    private void DevFilter(Filter fl)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ req = new DEVREQ();
        if (!string.IsNullOrEmpty(fl.id))
        {
            req.dwDevID = ToUInt(fl.id);
        }
        if (!string.IsNullOrEmpty(fl.name))
        {
            req.szSearchKey = fl.name;
        }
        req.szKindIDs = Converter.CommaValue(fl.kinds);
        req.szCampusIDs = Converter.CommaValue(fl.campus);
        req.szDeptIDs = Converter.CommaValue(fl.dept);
        req.szClassIDs = Converter.CommaValue(fl.devcls);
        req.szLabIDs = Converter.CommaValue(fl.lab);
        req.szBuildingIDs = Converter.CommaValue(fl.buildings);
        if (!string.IsNullOrEmpty(fl.clskind) && fl.clskind != "0")
        {
            req.dwClassKind = ToUInt(fl.clskind);
        }
        if (!string.IsNullOrEmpty(fl.manager))
        {
            req.szAttendantName = fl.manager;
        }
        if (!string.IsNullOrEmpty(fl.runstat))
        {
            req.dwRunStat = ToUInt(fl.runstat);
        }
        if (!string.IsNullOrEmpty(fl.devstat))
        {
            req.dwDevStat = ToUInt(fl.devstat);
        }
        if (!string.IsNullOrEmpty(fl.price))
        {
            req.dwMinUnitPrice = ToUInt(fl.price) * 10000;
        }

        //分页
        REQEXTINFO vrGetExtInfo = new REQEXTINFO();
        vrGetExtInfo.dwNeedLines = fl.pctrlNeed;
        vrGetExtInfo.dwStartLine = fl.pctrlStar;
        req.szReqExtInfo = vrGetExtInfo;
        req.szReqExtInfo.szOrderKey = "szDevName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNIDEVICE[] vtResult;

        dwRlt rlt = new dwRlt();
        rlt.needLines = (int)fl.pctrlNeed;
        rlt.pageCtrlID = fl.pctrlId;
        List<unidev> devs = new List<unidev>();
        uResponse = m_Request.Device.Get(req, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            devs = ToDevList(vtResult);
            if (fl.sortHot != null && fl.sortHot == "true")
                devs = SortHot(devs, req);
            //获取分页
            REQEXTINFO new_ext;
            if (m_Request.Device.UTPeekDetail(out new_ext) && new_ext.dwTotolLines != null && new_ext.dwTotolLines > 0)
            {
                rlt.totalLines = (int)new_ext.dwTotolLines;
                rlt.startLine = (int)new_ext.dwStartLine;
            }
            else
            {
                rlt.totalLines = vtResult.Length;
                rlt.startLine = 0;
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
            return;
        }
        //如果fl.statdate存在则查询
        if (fl.statdate != null)
        {
            convertStat(ref devs, fl.statdate);
        }
        rlt.devs = devs;
        rlt.showMode = (int)fl.showMode;
        SucRlt(rlt);
    }
    //设备
    List<unidev> ToDevList(UNIDEVICE[] devs)
    {

        List<unidev> list = new List<unidev>();
        if (devs != null)
        {
            for (int i = 0; i < devs.Length; i++)
            {
                unidev dev = new unidev();
                if (IsStat(devs[i].dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;//不支持预约 20141017
                dev.id = devs[i].dwDevID.ToString();
                dev.name = devs[i].szDevName;
                dev.model = devs[i].szModel;
                dev.campus = devs[i].szCampusName;
                dev.dept = devs[i].szDeptName;
                dev.devcls = devs[i].szClassName;
                dev.kind = devs[i].szKindName;
                dev.lab = devs[i].szLabName;
                dev.building = devs[i].szBuildingName;
                dev.manager = devs[i].szAttendantName;
                dev.phone = devs[i].szAttendantTel;
                dev.price = devs[i].dwUnitPrice.ToString();
                dev.prop = devs[i].dwProperty;
                dev.intro = devs[i].szCampusName.ToString() + "，" + devs[i].szClassName.ToString();
                dev.url = GetImg(devs[i].dwDevSN);
                //仪器状态
                if (Util.Converter.GetDevStat(devs[i].dwDevStat))
                {
                    dev.devstat = Util.Converter.GetDevRunStat(devs[i].dwRunStat);
                }
                else
                {
                    dev.devstat = "<span style='color:red'>不可用</span>";
                }
                list.Add(dev);
            }
        }
        return list;
    }

    private bool convertStat(ref List<unidev> devs, string date)
    {
        uint dt = DateToUint(date);
        uint today = Convert.ToUInt32(DateTime.Now.ToString("yyyyMMdd"));
        List<unidev> tmp = new List<unidev>();
        if (dt < today)
        {
            foreach (unidev dev in devs)
            {
                unidev d = dev;
                d.devstat = "<span style='color:grey;'>已过期</span>";
                tmp.Add(d);
                //devs.Remove(dev);
            }
            devs = tmp;
            return false;
        }
        else
        {
            foreach (unidev dev in devs)
            {
                unidev d = dev;
                d.devstat = "<span style='color:green;'>无预约</span>";
                tmp.Add(d);
                //devs.Remove(dev);
            }
            devs = tmp;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVRESVSTATREQ req = new DEVRESVSTATREQ();
        req.szDates = dt.ToString();
        req.dwResvPurpose = 319;//319所有预约类型 (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        req.dwResvPurpose = req.dwResvPurpose | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
        req.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        req.szReqExtInfo.dwStartLine = 0;
        req.szReqExtInfo.dwNeedLines = 10000;
        DEVRESVSTAT[] rlt;
        List<string> list = new List<string>();
        uResponse = m_Request.Device.GetDevResvStat(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                for (int n = 0; n < devs.Count; n++)
                {
                    unidev dev = devs[n];
                    if (rlt[i].dwDevID == ToUInt(dev.id))
                    {
                        if (rlt[i].szResvInfo != null && rlt[i].szResvInfo.Length > 0)
                        {
                            DEVRESVTIME[] tms = rlt[i].szResvInfo;
                            int span = 0;
                            for (int j = 0; j < tms.Length; j++)
                            {
                                int temp = (int)(tms[j].dwEnd - tms[j].dwBegin);
                                if (temp > span) span = temp;
                            }
                            if (rlt[i].szOpenInfo != null && rlt[i].szOpenInfo.Length > 0)
                            {
                                int opSpan = (int)(rlt[i].szOpenInfo[0].dwEnd - rlt[i].szOpenInfo[0].dwBegin);
                                if (span >= opSpan)
                                {
                                    dev.devstat = "<span style='color:grey;'>预约满</span>";
                                    break;
                                }
                            }
                            dev.devstat = "<span style='color:#039FB1;'>有预约</span>";
                            break;
                        }
                        break;
                    }
                }
            }
            return true;
        }
        return false;
    }

    //private bool getDevFreeStat(string id, uint? dt, uint[] arr)
    //{
    //    //今日时间
    //    DateTime now = DateTime.Now;
    //    uint today = ToUInt(now.ToString("yyyyMMdd"));
    //    uint here = ToUInt(now.ToString("HHmm"));
    //    if (dt == today)
    //    {
    //        for (int m = 0; m < arr.Length; m += 2)
    //        {
    //            if (arr[m] < here)
    //                return false;
    //        }
    //    }
    //    REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
    //    DEVRESVSTATREQ req = new DEVRESVSTATREQ();
    //    req.dwDevID = ToUInt(id);
    //    req.szDates = dt.ToString();
    //    req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
    //    req.szReqExtInfo.dwStartLine = 0;
    //    req.szReqExtInfo.dwNeedLines = 10000;
    //    DEVRESVSTAT[] rlt;
    //    uResponse = m_Request.Device.GetDevResvStat(req, out rlt);
    //    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
    //    {
    //        if (rlt[0].szResvInfo != null && rlt[0].szResvInfo.Length > 0)
    //        {
    //            //预约
    //            DEVRESVTIME[] tms = rlt[0].szResvInfo;
    //            for (int j = 0; j < tms.Length; j++)
    //            {
    //                for (int k = 0; k < arr.Length; k += 2)
    //                {
    //                    //预约
    //                    if (arr[k + 1] > tms[j].dwBegin && arr[k] < tms[j].dwEnd)
    //                        return false;
    //                }
    //            }
    //            //开放时间
    //            DAYOPENRULE[] open = rlt[0].szOpenInfo;
    //            for (int k = 0; k < arr.Length; k += 2)
    //            {
    //                bool outside = true;
    //                for (int m = 0; m < open.Length; m++)
    //                {

    //                    if (arr[k] >= open[m].dwBegin && arr[k + 1] <= open[m].dwEnd)
    //                        outside = false;
    //                }
    //                if (outside) return false;
    //            }
    //        }
    //        return true;
    //    }
    //    return false;
    //}

    private int getDevFreeTime(DEVRESVSTAT stat, uint[] arr)
    {
        int ret = 0;
        bool[] list = new bool[1440];

        //开放时间 刷白
        DAYOPENRULE[] open = stat.szOpenInfo;
        uint? m = ToUInt(DateTime.Now.ToString("HHmm"));
        uint? dt = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        for (int i = 0; i < open.Length; i++)
        {
            int start = toMinutes(open[i].dwBegin);
            int end = toMinutes(open[i].dwEnd);
            if (open[0].dwDate == dt)//如果是当天
            {
                if (open[i].dwEnd < m)
                    continue;
                if (open[i].dwBegin < m)
                    start = toMinutes(m);
            }

            for (int j = start; j < end; j++)
            {
                list[j] = true;
            }
        }
        //预约 刷黑
        DEVRESVTIME[] tms = stat.szResvInfo;
        if (tms != null)
        {
            for (int i = 0; i < tms.Length; i++)
            {
                DEVRESVTIME tm = tms[i];
                int start = toMinutes(tm.dwBegin);
                int end = toMinutes(tm.dwEnd);

                for (int j = start; j < end; j++)
                {
                    list[j] = false;
                }
            }
        }
        //取白
        for (int i = 0; i < arr.Length; i += 2)
        {
            int start = toMinutes(arr[i]);
            int end = toMinutes(arr[i + 1]);
            for (int j = start; j < end; j++)
            {
                if (list[j] == true) ret++;
            }
        }
        return ret;
    }
    private int toMinutes(uint? t)
    {
        return (int)(t / 100 * 60 + (t % 100));
    }

    private int getDevFreeStat(DEVRESVSTAT stat, uint[] arr)
    {
        int ret = 0;//0=空闲 >0有预约的条数 其它返回 -2=已过期 -3 不在开放时间 -1=已审核/管理员预留 不可用

        //开放时间
        DAYOPENRULE[] open = stat.szOpenInfo;
        for (int k = 0; k < arr.Length; k += 2)
        {
            bool outside = true;
            for (int m = 0; m < open.Length; m++)
            {
                if (m == 0)//只检测第一个开放规则是因为过期的开放规则服务不会返回
                {
                    if (open[0].dwDate == ToUInt(DateTime.Now.ToString("yyyyMMdd")) && arr[k] < ToUInt(DateTime.Now.ToString("HHmm")))//如果是当天 则检查开始时间
                    {
                        return -3;
                    }
                }
                if (m > 0 && open[m].dwDate != null && open[m].dwDate != open[m - 1].dwDate)//新的一天
                {
                    if (outside) return -3;//不在开放时间
                    else outside = true;
                }
                if (arr[k] >= open[m].dwBegin && arr[k + 1] <= open[m].dwEnd)
                    outside = false;
            }
            if (outside)
                return -3;
        }
        //检查预约
        DEVRESVTIME[] tms = stat.szResvInfo;
        if (tms != null)
        {
            //test 临时 计算应许的审核通过的预约条数
            uint num = 0;//占用型预约条数
            uint? count = stat.dwMaxUsers / stat.dwMinUsers;
            //////////////////
            for (int i = 0; i < tms.Length; i++)
            {
                DEVRESVTIME tm = tms[i];
                bool has = false;
                for (int k = 0; k < arr.Length; k += 2)
                {
                    //预约
                    if (arr[k + 1] > tm.dwBegin && arr[k] < tm.dwEnd)
                    {
                        if (IsStat(tm.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE))
                            return -2;//已过期
                        if (IsStat(tm.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) || IsStat(tm.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK) || IsStat(tm.dwPurpose, (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED) || tm.dwStatus == 0)//dwStatus=0 第三方预约
                        {
                            if (IsStat(stat.dwProperty, (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE))
                            {
                                //test 临时 计算应许的审核通过的预约条数
                                if (i == 0 || tm.dwPreDate == tms[i - 1].dwPreDate)
                                {
                                    num++;//test 临时 计算应许的审核通过的预约条数
                                }
                                else
                                {
                                    num = 1;
                                }
                                if (num >= count)//超出限制
                                    return -1;
                                ///////////////////

                                has = true;
                                break;
                            }
                            else
                                return -1;// 预约已占用 不可用 直接返回
                        }
                        else
                        {
                            has = true;//有预约
                            break;
                        }
                    }
                }
                if (has) ret++;//有预约 +1
            }
        }
        return ret;
    }

    private DEVRESVTIME[] mergeDate(DEVRESVSTAT stat)
    {
        //状态字符数组
        char[] numArray = new char[1440];
        for (int num = 0; num < 1440; num++) numArray[num] = '1';//初始化
        DEVRESVTIME[] tms = stat.szResvInfo;
        if (tms != null && tms.Length > 0)
        {
            uint? date = tms[0].dwPreDate;//只取第一次预约的日期
            for (int i = 0; i < tms.Length; i++)
            {
                DEVRESVTIME tm = tms[i];
                int start = (int)((tm.dwBegin / 100) * 60 + (tm.dwBegin % 100));
                int end = (int)((tm.dwEnd / 100) * 60 + (tm.dwEnd % 100));
                char v = (IsStat(tm.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK) || IsStat(tm.dwPurpose, (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED) || tm.dwStatus == 0) ? 'b' : 'r';//dwStatus=0 第三方预约
                for (int ss = start; ss <= end; ss++)
                {
                    if (numArray[ss] != 'b')
                        numArray[ss] = v;
                }
            }
            return TranResvInfo(new string(numArray), new char[] { 'b', 'r' }, date);
        }
        return null;
    }

    private string getLongDevResvState(out List<devResvSta> rs)
    {
        string devid = Request["dev_id"];
        string labid = Request["lab_id"];
        string roomId = Request["room_id"];
        string start = Request["start"];
        string end = Request["end"];
        string dt = Request["date"];
        string classId = Request["class_id"];
        string kindId = Request["kind_id"];
        string classKind = Request["classkind"];
        string purpose = Request["purpose"];
        string prop = Request["prop"];
        string isCheckClose = Request["ck_close"];
        List<devResvSta> list = new List<devResvSta>();
        rs = list;
        int date;
        uint start_date;
        uint end_date;
        if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
        {
            start_date = ToUInt(start.Replace("-", ""));
            end_date = ToUInt(end.Replace("-", ""));
        }
        else if (!string.IsNullOrEmpty(dt) && int.TryParse(dt.Replace("-", ""), out date))
        {
            DateTime datetime = new DateTime(date / 10000, (date % 10000) / 100, 1);
            start_date = ToUInt(datetime.AddDays(-15).ToString("yyyyMMdd"));
            end_date = ToUInt(datetime.AddDays(45).ToString("yyyyMMdd"));
        }
        else
            return "日期错误";

        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        DEVLONGRESVSTATREQ req = new DEVLONGRESVSTATREQ();
        req.dwStartDate = start_date;
        req.dwEndDate = end_date;
        if (!string.IsNullOrEmpty(devid) && devid != "0")
        {
            req.dwDevID = ToUInt(devid);
        }
        if (!string.IsNullOrEmpty(labid) && labid != "0")
        {
            req.szLabIDs = labid;
        }
        if (!string.IsNullOrEmpty(roomId) && roomId != "0")
        {
            req.szRoomIDs = roomId;
        }
        if (!string.IsNullOrEmpty(prop) && prop != "0")
        {
            //req.dwProperty = ToUInt(prop);//暂时缺少此字段
        }
        if (string.IsNullOrEmpty(purpose))
            //319所有预约类型(uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
            req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM | (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
        else
            req.dwResvPurpose = Convert.ToUInt32(purpose);
        if (!string.IsNullOrEmpty(classKind) && classKind != "0")
            req.dwClassKind = ToUInt(classKind);
        else
            req.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        if (!string.IsNullOrEmpty(classId) && classId != "0")
        {
            classId = classId.Split('#')[0];
            req.szClassIDs = classId;
        }
        if (!string.IsNullOrEmpty(kindId) && kindId != "0")
        {
            req.szKindIDs = kindId;
        }
        DEVLONGRESVSTAT[] rlt;
        cd = m_Request.Device.GetDevLongResvStat(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                //手机端  过滤需上传附件
                if (Session["ADMINLOGINREQ"] != null)
                {
                    ADMINLOGINREQ lgreq = (ADMINLOGINREQ)Session["ADMINLOGINREQ"];
                    if (IsStat(lgreq.dwLoginRole, (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP) && IsStat(rlt[i].szRuleInfo.dwLimit, (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NEEDAPP))
                    {
                        continue;
                    }
                }
                //
                devResvSta sta = new devResvSta();
                sta.id = rlt[i].dwDevID.ToString() + "_" + rlt[i].dwLabID.ToString();
                sta.title = rlt[i].szDevName;
                sta.name = sta.title;
                sta.kindId = rlt[i].dwKindID.ToString();
                sta.kindName = rlt[i].szKindName;
                sta.devId = rlt[i].dwDevID.ToString();
                sta.devName = rlt[i].szDevName;
                sta.classId = classId;
                sta.className = rlt[i].szClassName;
                sta.labId = rlt[i].dwLabID.ToString();
                sta.labName = rlt[i].szLabName;
                sta.roomId = rlt[i].dwRoomID;
                sta.roomName = rlt[i].szRoomName;
                sta.clskind = classKind;
                sta.iskind = false;
                sta.islong = true;
                sta.allowLong = true;
                UNIDEVKIND kind = GetDevKind(rlt[i].dwKindID);
                if (kind.dwProperty != null)
                {
                    if (!isLongResv(kind.dwProperty))
                        sta.allowLong = false;
                    //预约规则
                    sta.ruleId = rlt[i].szRuleInfo.dwRuleSN;
                    GetRuleDetail(rlt[i].szRuleInfo, kind.dwProperty, ref sta);
                }
                //预约限制
                sta.earliest = rlt[i].szRuleInfo.dwEarliestResvTime;
                sta.latest = rlt[i].szRuleInfo.dwLatestResvTime;
                sta.max = rlt[i].szRuleInfo.dwMaxResvTime;
                sta.min = rlt[i].szRuleInfo.dwMinResvTime;
                sta.cancel = rlt[i].szRuleInfo.dwCancelTime;
                sta.maxUser = rlt[i].dwMaxUsers;
                sta.minUser = rlt[i].dwMinUsers;
                //检查开放
                if (!string.IsNullOrEmpty(isCheckClose))
                {
                    DateTime today = DateTime.Now;
                    List<string> clsList = new List<string>();
                    int uStart = (int)sta.latest / 1440;
                    int uEnd = (int)sta.earliest / 1440;
                    if (uEnd > 42) uEnd = 42;
                    for (int m = uStart; m < uEnd; m++)
                    {
                        DateTime dtDate = today.AddDays(m);
                        string uDate = dtDate.ToString("yyyyMMdd");
                        if (!getOpenState(sta.devId, uDate))
                            clsList.Add(dtDate.ToString("yyyy-MM-dd"));
                    }
                    sta.clsDate = clsList.ToArray();
                }
                //预约状态
                List<plan> ts = new List<plan>();
                DEVRESVTIME[] times = rlt[i].szResvInfo;
                for (int k = 0; times != null && k < times.Length; k++)
                {
                    plan p = new plan();
                    string dstart = toDate((uint)times[k].dwBegin);
                    string dend = toDate((uint)times[k].dwEnd);
                    p.start = dstart + "00:00";
                    p.end = dend + "23:59";
                    ts.Add(p);
                }
                sta.ts = ts.ToArray();
                list.Add(sta);
            }
            rs = list;
            return "ok";
        }
        else
        {
            return m_Request.szErrMsg;
        }
    }

    private string GetDevResvState(out List<devResvSta> rs)
    {
        string devid = Request["dev_id"];
        string name = Request["dev_name"];
        string campusId = Request["campus"];
        string labid = Request["lab_id"];
        string roomId = Request["room_id"];
        string buildingId = Request["building_id"];
        string classId = Request["class_id"];
        string kindId = Request["kind_id"];
        string kinds = Request["kinds"];
        string classKind = Request["classkind"];
        string purpose = Request["purpose"];//预约用途
        string prop = Request["prop"];//属性
        string dt = Request["date"];
        string freeStart = Request["fr_start"];
        string freeEnd = Request["fr_end"];
        bool freeAllDay = Request["fr_all_day"] == "true";
        string userNum = Request["user_num"];
        string extRelate = Request["ext_id"];
        bool isAllOpenRule = Request["all_open"] == "true";
        string order = Request["dev_order"];
        string reqPr=Request["req_prop"];
        
        List<devResvSta> list = new List<devResvSta>();
        rs = list;
        if (string.IsNullOrEmpty(dt))
        {
            return "日期错误";
        }
        dt = dt.Replace("-", "");
        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        DEVRESVSTATREQ req = new DEVRESVSTATREQ();
        req.szDates = dt;
        uint reqProp = 0;
        if (isAllOpenRule)
            reqProp += (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;
        //if (GetConfig("getAllResvStat") == "1")
        //    reqProp += (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_VIEWALL;
        if (!string.IsNullOrEmpty(reqPr))
            reqProp |= ToUInt(reqPr);
        if (reqProp > 0) req.dwReqProp = reqProp;
        if (ConfigConst.GCSysFrame.ToLower() == "site")
        {
            if (req.dwReqProp == null)
            {
                req.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDTHIRDSHAREDEV;
            }
            else {
                req.dwReqProp = req.dwReqProp|(uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDTHIRDSHAREDEV;
            }
        }
        if (!string.IsNullOrEmpty(userNum))
            req.dwResvUsers = ToUInt(userNum);
        if (!string.IsNullOrEmpty(extRelate) && extRelate != "0")
            req.dwExtRelatedID = ToUInt(extRelate);
        if (!string.IsNullOrEmpty(devid) && devid != "0")
        {
            req.dwDevID = ToUInt(devid);
        }
        if (!string.IsNullOrEmpty(name))
        {
            req.szDevName = name;
        }
        if (!string.IsNullOrEmpty(labid) && labid != "0")
        {
            req.szLabIDs = labid;
        }
        if (!string.IsNullOrEmpty(roomId) && roomId != "0")
        {
            req.szRoomIDs = roomId;
        }
        if (!string.IsNullOrEmpty(buildingId) && buildingId != "0")
        {
            req.szBuildingIDs = buildingId;
        }
        if (!string.IsNullOrEmpty(campusId) && campusId != "0")
            req.szCampusIDs = campusId;
        if (string.IsNullOrEmpty(purpose) || purpose == "0")
        {
            //319所有预约类型(uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
            if (!string.IsNullOrEmpty(classKind) && classKind == "8")//座位预约只获取座位对应的预约规则
            {
                req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
            }
            else if (!string.IsNullOrEmpty(classKind) && classKind == "1")//座位预约只获取座位对应的预约规则
            {
                req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
            }
            else
            {
                req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM | (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
            }
        }
        else
            req.dwResvPurpose = Convert.ToUInt32(purpose);
        if (!string.IsNullOrEmpty(prop) && prop != "0")
            req.dwProperty = ToUInt(prop);
        if (!string.IsNullOrEmpty(classKind) && classKind != "0")
            req.dwClassKind = ToUInt(classKind);
        if (!string.IsNullOrEmpty(classId) && classId != "0")
        {
            classId = classId.Split('#')[0];
            req.szClassIDs = classId;
        }
        if (!string.IsNullOrEmpty(kindId) && kindId != "0")
        {
            req.szKindIDs = kindId;
        }
        else if (!string.IsNullOrEmpty(kinds) && kinds != "0")
        {
            req.szKindIDs = kinds;
        }
        req.szReqExtInfo.dwStartLine = 0;
        req.szReqExtInfo.dwNeedLines = 10000;
        req.szReqExtInfo.szOrderKey = string.IsNullOrEmpty(order) ? "dwUnitPrice" : order;
        req.szReqExtInfo.szOrderMode = "ASC";
        DEVRESVSTAT[] rlt;
        cd = m_Request.Device.GetDevResvStat(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (IsStat(rlt[i].dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;//不支持预约

                if (string.IsNullOrEmpty(devid))
                {
                    if (IsStat(rlt[i].dwProperty, (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV)) continue;//手机不支持长期预约
                }
                //手机端  过滤需上传附件
                if (Session["ADMINLOGINREQ"]!=null){
                    ADMINLOGINREQ lgreq=(ADMINLOGINREQ)Session["ADMINLOGINREQ"];
                    if (IsStat(lgreq.dwLoginRole, (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP) && IsStat(rlt[i].szRuleInfo.dwLimit, (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NEEDAPP))
                    {
                       
                        continue;
                    }
                }
                //
                devResvSta sta = ConvertDevResvSta(rlt[i], (dt.Split(',').Length > 1 && !isAllOpenRule),dt);
                sta.clskind = classKind;
                sta.classId = classId;
                //过滤不空闲
                int freeSta = 0;
                int freeTime = 0;
                if (sta.open != null && sta.open.Length > 1 && (!string.IsNullOrEmpty(freeStart) || !string.IsNullOrEmpty(freeEnd)||freeAllDay))
                {
                    freeAllDay = true;
                    if (freeAllDay)
                    {
                        if (DateTime.Now > DateTime.Parse(Request["date"] + " " + freeStart)) freeStart = DateTime.Now.ToString("HH:mm");
                        else freeStart = sta.open[0];
                        freeEnd = sta.open[1];
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(freeStart)) freeStart = sta.open[0];
                        if (string.IsNullOrEmpty(freeEnd)) freeEnd = sta.open[1];
                    }
                    uint[] arr = new uint[2];
                    arr[0] = ToUInt(freeStart.Replace(":", ""));
                    arr[1] = ToUInt(freeEnd.Replace(":", ""));
                    if (arr[1] < arr[0]) freeSta = -3;
                    else
                    {
                        freeSta = getDevFreeStat(rlt[i], arr);//0=空闲 >0有预约的条数 其它返回 -2=已过期 -3 不在开放时间 -1=已审核/管理员预留 不可用
                        if (dt.Split(',').Length == 1)//多天无意义
                            freeTime = getDevFreeTime(rlt[i], arr);
                    }
                }
                sta.freeSta = freeSta;
                sta.freeTime = freeTime;
                if (ConfigConst.GCSysFrame.ToLower() == "site")
                {

                    sta.title = sta.title;
                }
                    list.Add(sta);
            }
            rs = list;
            return "ok";
        }
        else
        {
            return m_Request.szErrMsg;
        }
    }

    private devResvSta ConvertDevResvSta(DEVRESVSTAT devstat, bool multi_date,string dt)
    {
        //multi_date 多日期叠加
        devResvSta sta = new devResvSta();
        sta.id = devstat.dwDevID.ToString() + "_" + devstat.dwLabID.ToString();
        sta.title = devstat.szDevName;
        sta.name = sta.title;
        sta.kindId = devstat.dwKindID.ToString();
        sta.kindName = devstat.szKindName;
        sta.devId = devstat.dwDevID.ToString();
        sta.devName = devstat.szDevName;
        sta.className = devstat.szClassName;
        sta.labId = devstat.dwLabID.ToString();
        sta.labName = devstat.szLabName;
        sta.roomId = devstat.dwRoomID;
        sta.roomName = devstat.szRoomName;
        sta.buildingId = devstat.dwBuildingID;
        sta.buildingName = devstat.szBuildingName;
        sta.campus = devstat.szCampusName;
        sta.prop = devstat.dwProperty;
        sta.devsta = devstat.dwDevStat;
        sta.runsta = devstat.dwRunStat;
        sta.ext = devstat.szExtInfo;
        sta.iskind = false;
        sta.islong = false;
        if (isLongResv(devstat.dwProperty))
            sta.allowLong = true;
        else
            sta.allowLong = false;
        //检查状态
        if (IsStat(devstat.dwDevStat, (uint)UNIDEVICE.DWDEVSTAT.DEVSTAT_DAMAGED))
        {
            sta.state = "forbid";
        }
        //开放规则
        List<plan> ps = new List<plan>();
        List<plan> op = new List<plan>();
        if (devstat.szOpenInfo!=null)
        {
            sta.open = GetOpenArray(devstat.szOpenInfo, ref ps, ref op);
            sta.cls = ps.ToArray();
            sta.ops = op.ToArray();
        }
        if (sta.open == null || sta.open.Length < 2 || sta.open[0] == sta.open[1])//不开放
        {
            sta.state = "close";
        }
        else
        {
            if (IsStat(devstat.dwOpenLimit, (uint)DEVRESVSTAT.DWOPENLIMIT.OPENLIMIT_NORESV))
            {
                sta.state = "noresv";//不能预约
            }
            sta.openStart = sta.open[0];
            sta.openEnd = sta.open[sta.open.Length - 1];
        }
        //预约规则
        if (devstat.szRuleInfo.dwRuleSN != null)
        {
            sta.ruleId = devstat.szRuleInfo.dwRuleSN;
            GetRuleDetail(devstat.szRuleInfo, devstat.dwProperty, ref sta);
            //预约限制
            sta.earliest = devstat.szRuleInfo.dwEarliestResvTime;
            sta.latest = devstat.szRuleInfo.dwLatestResvTime;
            sta.max = devstat.szRuleInfo.dwMaxResvTime;
            sta.min = devstat.szRuleInfo.dwMinResvTime;
            sta.cancel = devstat.szRuleInfo.dwCancelTime;
            if (sta.earliest>1440&&(sta.earliest % 1440) > 0)//设置了开启时刻
            {
                if (dt.IndexOf(',') < 0)
                {
                    string t_date = DateTime.Now.AddDays((double)(sta.earliest / 1440)).ToString("yyyyMMdd");
                    if (t_date == dt)
                    {
                        uint min = (uint)sta.earliest % 1440;
                        if ((DateTime.Now.Hour * 60 + DateTime.Now.Minute) < min)
                        {

                            sta.state = "close";
                            sta.ext = (min / 60).ToString("00") + ":" + (min % 60).ToString("00") + "后开放";
                        }
                    }
                }
                sta.earliest = sta.earliest / 1440 * 1440;//只取整数天
            }
        }
        sta.maxUser = devstat.dwMaxUsers;
        sta.minUser = devstat.dwMinUsers;
        //预约状态
        List<plan> ts = new List<plan>();
        DEVRESVTIME[] times = multi_date ? mergeDate(devstat) : devstat.szResvInfo;
        for (int k = 0; times != null && k < times.Length; k++)
        {
            DEVRESVTIME tm = times[k];
            if (tm.dwBegin == null) continue;
            plan p = new plan();
            string start = string.Format("{0,2:00}", ((uint)tm.dwBegin) / 100) + ":" + string.Format("{0,2:00}", ((uint)tm.dwBegin) % 100);
            string end = string.Format("{0,2:00}", ((uint)tm.dwEnd) / 100) + ":" + string.Format("{0,2:00}", ((uint)tm.dwEnd) % 100);
            string date = toDate((uint)tm.dwPreDate);
            p.start = date + start;
            p.end = date + end;
            p.occupy = !IsStat(devstat.dwProperty, (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE) && ((tm.dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0 || (tm.dwPurpose & (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED) > 0 || tm.dwStatus == 0);//dwStatus=0 第三方预约
            if (tm.dwStatus != null && tm.dwStatus != 0)
            {
                if (IsStat(tm.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING))
                    p.state = "doing";
                else if (IsStat(tm.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE))
                    p.state = "done";
                else if (IsStat(tm.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO))
                    p.state = "undo";
            }
            p.owner = tm.szOwnerName;
            p.accno = tm.dwOwner.ToString();
            
            p.member = tm.szMemberName;
            if (!string.IsNullOrEmpty(tm.szTestName))
            { if (tm.szTestName.IndexOf('＃') > -1 && tm.szTestName.IndexOf('@') > -1)
                {
                    p.title = (tm.szTestName.Replace("@＃", ","));
                }
                else {
                    p.title = tm.szOwnerName+","+tm.szTestName;
                }
            }
            ts.Add(p);
        }
        //设备所在部门
        sta.ts = ts.ToArray();
        return sta;
    }

    private string getDevKindResvState(out List<devResvSta> rs)
    {
        string kindId = Request["kind_id"];
        string classId = Request["class_id"];
        string labid = Request["lab_id"];
        string roomId = Request["room_id"];
        string classKind = Request["classkind"];
        string purpose = Request["purpose"];
        string prop = Request["prop"];
        string dt = Request["date"];
        string order=Request["kind_order"];
        dt = dt.Replace("-", "");
        uint date;
        List<devResvSta> list = new List<devResvSta>();
        rs = list;
        if (string.IsNullOrEmpty(dt) || !uint.TryParse(dt, out date))
        {
            return "日期错误";
        }
        string date_pre = toDate(date);
        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDFORRESVREQ req = new DEVKINDFORRESVREQ();
        req.dwDate = date;
        if (!string.IsNullOrEmpty(labid) && labid != "0")
        {
            req.szLabIDs = labid;
        }
        if (!string.IsNullOrEmpty(roomId) && roomId != "0")
        {
            req.szRoomIDs = roomId;
        }
        if (!string.IsNullOrEmpty(prop) && prop != "0")
        {
            req.dwProperty = ToUInt(prop);
        }
        if (string.IsNullOrEmpty(purpose))
            req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM | (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
        else
            req.dwResvPurpose = Convert.ToUInt32(purpose);
        if (!string.IsNullOrEmpty(classKind) && classKind != "0")
            req.dwClassKind = ToUInt(classKind);
        //else
        //req.dwClassKind = (uint)(UNIDEVCLS.DWKIND.CLSKIND_COMMONS |UNIDEVCLS.DWKIND.CLSKIND_SEAT | UNIDEVCLS.DWKIND.CLSKIND_LOAN | UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
        if (!string.IsNullOrEmpty(classId) && classId != "0")
        {
            classId = classId.Split('#')[0];
            req.szClassIDs = classId;
        }
        if (!string.IsNullOrEmpty(kindId) && kindId != "0")
            req.szKindIDs = kindId;
        req.szReqExtInfo.dwStartLine = 0;
        req.szReqExtInfo.dwNeedLines = 10000;
        req.szReqExtInfo.szOrderKey =string.IsNullOrEmpty(order)?"szKindName":order;
        req.szReqExtInfo.szOrderMode = "ASC";
        DEVKINDFORRESV[] rlt;
        cd = m_Request.Device.GetDevKindForResv(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (IsStat(rlt[i].dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;//不支持预约
                devResvSta sta = ConvertKindResvSta(rlt[i]);
                //sta.classId = classId;
                sta.clskind = classKind;
                sta.islong = false;
                //预约状态
                if (sta.state != "close")//不开放则不查询
                {
                    List<plan> ts = new List<plan>();
                    DEVRESVTIME[] times = TranResvInfo(rlt[i].szUsableNumArray, new char[] { '0' }, date);
                    for (int k = 0; times != null && k < times.Length; k++)
                    {
                        plan p = new plan();
                        string start = (((uint)times[k].dwBegin) / 100).ToString("00") + ":" + (((uint)times[k].dwBegin) % 100).ToString("00");
                        string end = (((uint)times[k].dwEnd) / 100).ToString("00") + ":" + (((uint)times[k].dwEnd) % 100).ToString("00");
                        p.start = date_pre + start;
                        p.end = date_pre + end;
                        ts.Add(p);
                    }
                    sta.ts = ts.ToArray();
                }
                list.Add(sta);
            }
            rs = list;
            return "ok";
        }
        else
        {
            return m_Request.szErrMsg;
        }
    }

    private string getDevKindLongResvState(out List<devResvSta> rs)
    {
        string kindId = Request["kind_id"];
        string classId = Request["class_id"];
        string labid = Request["lab_id"];
        string roomId = Request["room_id"];
        string classKind = Request["classkind"];
        string purpose = Request["purpose"];
        string prop = Request["prop"];
        string dt = Request["date"];
        string s_start = Request["start"];
        string s_end = Request["end"];
        uint start;
        uint end;
        List<devResvSta> list = new List<devResvSta>();
        rs = list;
        if (string.IsNullOrEmpty(s_start) || string.IsNullOrEmpty(s_end))
        {
            if (string.IsNullOrEmpty(dt) || dt.Length < 8)
            {
                return "日期错误";
            }
            int date = int.Parse(dt.Replace("-", ""));
            DateTime datetime = new DateTime(date / 10000, (date % 10000) / 100, 1);
            start = ToUInt(datetime.AddDays(-15).ToString("yyyyMMdd"));
            end = ToUInt(datetime.AddDays(45).ToString("yyyyMMdd"));
        }
        else
        {
            start = ToUInt(s_start.Replace("-", ""));
            end = ToUInt(s_end.Replace("-", ""));
        }
        REQUESTCODE cd = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDFORLONGRESVREQ req = new DEVKINDFORLONGRESVREQ();
        req.dwStartDate = start;
        req.dwEndDate = end;
        if (!string.IsNullOrEmpty(labid) && labid != "0")
        {
            req.szLabIDs = labid;
        }
        if (!string.IsNullOrEmpty(roomId) && roomId != "0")
        {
            req.szRoomIDs = roomId;
        }
        if (!string.IsNullOrEmpty(prop) && prop != "0")
        {
            req.dwProperty = ToUInt(prop);
        }
        if (string.IsNullOrEmpty(purpose))
            req.dwResvPurpose = 319 | (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM | (uint)UNIRESERVE.DWPURPOSE.USEFOR_LOAN | (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
        else
            req.dwResvPurpose = Convert.ToUInt32(purpose);
        if (!string.IsNullOrEmpty(classKind) && classKind != "0")
            req.dwClassKind = ToUInt(classKind);
        else
            req.dwClassKind = (uint)(UNIDEVCLS.DWKIND.CLSKIND_SEAT | UNIDEVCLS.DWKIND.CLSKIND_LOAN | UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
        if (!string.IsNullOrEmpty(classId) && classId != "0")
        {
            classId = classId.Split('#')[0];
            req.szClassIDs = classId;
        }
        if (!string.IsNullOrEmpty(kindId) && kindId != "0")
            req.szKindIDs = kindId;
        //req.szReqExtInfo.dwStartLine = 0;
        //req.szReqExtInfo.dwNeedLines = 10000;
        //req.szReqExtInfo.szOrderKey = "szKindName";
        //req.szReqExtInfo.szOrderMode = "ASC";
        DEVKINDFORRESV[] rlt;
        cd = m_Request.Device.GetDevKindForLongResv(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (IsStat(rlt[i].dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;//不支持预约
                devResvSta sta = ConvertKindResvSta(rlt[i]);
                //sta.classId = classId;
                sta.clskind = classKind;
                sta.islong = true;
                sta.state = null;
                //预约状态
                List<plan> ts = new List<plan>();
                List<string> cls = new List<string>();
                string arr = rlt[i].szUsableNumArray;
                DateTime begin = new DateTime((int)start / 10000, ((int)start % 10000) / 100, (int)start % 100);
                string tmp_start = "";
                string[] tbl = new string[arr.Length];
                int first = 0;
                if (begin < DateTime.Now)
                {
                    TimeSpan sp = DateTime.Now - begin;
                    first = sp.Days;
                }
                for (int k = first; k < arr.Length; k++)
                {
                    if (arr[k] == '0')
                    {
                        if (k == first || arr[k - 1] != '0')
                        {
                            string date = begin.AddDays(k).ToString("yyyy-MM-dd");
                            tmp_start = date + " 00:00";
                        }
                        if (k == arr.Length - 1 || arr[k + 1] != '0')
                        {
                            string date = begin.AddDays(k).ToString("yyyy-MM-dd");
                            plan p = new plan();
                            p.start = tmp_start;
                            p.end = date + " 23:59";
                            ts.Add(p);
                        }
                    }
                    if (arr[k] == 'U') cls.Add(begin.AddDays(k).ToString("yyyy-MM-dd"));
                    tbl[k] = arr[k].ToString();
                }
                sta.freeTime = Int32.Parse(begin.ToString("yyyyMMdd"));
                sta.clsDate = cls.ToArray();
                sta.freeTbl = tbl;
                sta.ts = ts.ToArray();
                list.Add(sta);
            }
            rs = list;
            return "ok";
        }
        else
        {
            return m_Request.szErrMsg;
        }
    }

    private devResvSta ConvertKindResvSta(DEVKINDFORRESV ks)
    {
        devResvSta sta = new devResvSta();
        sta.id = ks.dwKindID.ToString() + "_" + ks.dwLabID+"_"+ks.dwRoomID;
        sta.title = ks.szKindName;
        sta.name = ks.szKindName;// + "(" + ks.szLabName + ")"
        sta.devId = sta.kindId = ks.dwKindID.ToString();
        sta.kindName = ks.szKindName;
        sta.classId = ks.dwClassID.ToString();
        sta.className = ks.szClassName;
        sta.labId = ks.dwLabID.ToString();
        sta.labName = ks.szLabName;
        sta.roomId = ks.dwRoomID;
        sta.roomName = ks.szRoomName;//缺字段
        sta.iskind = true;
        sta.prop = ks.dwProperty;
        //开放规则
        List<plan> ps = new List<plan>();//关闭时段
        List<plan> op = new List<plan>();//开放时段
        sta.open = GetOpenArray(ks.szOpenInfo, ref ps, ref op);//合并后开放时间断
        sta.cls = ps.ToArray();
        sta.ops = op.ToArray();
        if (sta.open == null || sta.open.Length < 2 || sta.open[0] == sta.open[1])//不开放
        {
            sta.state = "close";
        }
        else
        {
            sta.openStart = sta.open[0];
            sta.openEnd = sta.open[sta.open.Length - 1];
        }
        //预约规则
        sta.ruleId = ks.szRuleInfo.dwRuleSN;
        GetRuleDetail(ks.szRuleInfo, ks.dwProperty, ref sta);
        //预约限制
        sta.earliest = ks.szRuleInfo.dwEarliestResvTime;
        sta.latest = ks.szRuleInfo.dwLatestResvTime;
        sta.max = ks.szRuleInfo.dwMaxResvTime;
        sta.min = ks.szRuleInfo.dwMinResvTime;
        sta.cancel = ks.szRuleInfo.dwCancelTime;
        sta.maxUser = ks.dwMaxUsers;
        sta.minUser = ks.dwMinUsers;
        if (isLongResv(ks.dwProperty))
            sta.allowLong = true;
        else
            sta.allowLong = false;
        return sta;
    }

    private bool getOpenState(string devId, string date)
    {
        DEVRESVSTATREQ req = new DEVRESVSTATREQ();
        req.szDates = date;
        req.dwDevID = ToUInt(devId);
        req.dwResvPurpose = 1343;//319所有预约类型(uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED | (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
        req.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        req.szReqExtInfo.dwStartLine = 0;
        req.szReqExtInfo.dwNeedLines = 10000;
        DEVRESVSTAT[] rlt;
        if (m_Request.Device.GetDevResvStat(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                DAYOPENRULE[] open = rlt[0].szOpenInfo;
                if (open == null || open.Length < 1 || open[0].dwBegin == open[0].dwEnd || IsStat(rlt[0].dwOpenLimit, (uint)DEVRESVSTAT.DWOPENLIMIT.OPENLIMIT_NORESV))//不开放
                {
                    return false;
                }
            }
        }
        return true;
    }

    string toDate(uint date)
    {
        uint y = date / 10000;
        uint m = (date % 10000) / 100;
        uint d = date % 100;
        return y + "-" + m.ToString("00") + "-" + d.ToString("00") + " ";
    }

    string[] GetOpenArray(DAYOPENRULE[] vtOpenInfo, ref List<plan> ps, ref List<plan> op)
    {
        if (vtOpenInfo == null || vtOpenInfo.Length == 0) return null;
        //开放规则
        List<string> open = new List<string>();
        List<string> starts = new List<string>();
        List<string> ends = new List<string>();
        uint? purpose = ToUInt(Request["purpose"]);
        int len = vtOpenInfo.Length;
        for (int i = 0; vtOpenInfo != null && i < len; i++)
        {
            if (IsStat(vtOpenInfo[i].dwOpenLimit, (uint)DEVRESVSTAT.DWOPENLIMIT.OPENLIMIT_NORESV))//当日不开放  test还需改进
            {
                string date = toDate((uint)vtOpenInfo[i].dwDate);
                plan p = new plan();
                p.date = date;
                p.start = date + "00:00";
                p.end = date + "23:59";
                p.state = "out";
                ps.Add(p);//加入到不开放时段
                continue;
            }
            if (purpose != 0 && (vtOpenInfo[i].dwOpenPurpose & purpose) == 0)//检查用途
                continue;
            if (vtOpenInfo.Length > 0 && (vtOpenInfo[i].dwBegin == vtOpenInfo[i].dwEnd))//多时段则过滤
                continue;
            string start = string.Format("{0,2:00}", ((uint)vtOpenInfo[i].dwBegin) / 100) + ":" + string.Format("{0,2:00}", ((uint)vtOpenInfo[i].dwBegin) % 100);
            string end = string.Format("{0,2:00}", ((uint)vtOpenInfo[i].dwEnd) / 100) + ":" + string.Format("{0,2:00}", ((uint)vtOpenInfo[i].dwEnd) % 100);
            if (vtOpenInfo[i].dwDate == null || i == 0 || (i > 0 && vtOpenInfo[i].dwDate != vtOpenInfo[i - 1].dwDate))//同一天取第一段
                starts.Add(start);
            if (vtOpenInfo[i].dwDate == null || i == len - 1 || (i + 1 < len && vtOpenInfo[i].dwDate != vtOpenInfo[i + 1].dwDate))//同一天取最后一段
                ends.Add(end);
            //开放时间段 作为查询用
            if (vtOpenInfo[i].dwDate != null)
            {
                plan o = new plan();
                o.date = toDate((uint)vtOpenInfo[i].dwDate);
                o.start = start;
                o.end = end;
                o.state = "open";
                o.limit = vtOpenInfo[i].dwOpenLimit;
                op.Add(o);
                //不开放时段(同一日内) 作为特殊预约段前端自动处理
                if (i > 0 && vtOpenInfo[i].dwDate == vtOpenInfo[i - 1].dwDate)
                {
                    string date = toDate((uint)vtOpenInfo[i].dwDate);
                    plan p = new plan();
                    p.date = date;
                    p.start = date + op[op.Count - 2].end;
                    p.end = date + start;
                    p.state = "out";
                    ps.Add(p);
                }
            }
        }
        //开放时间
        if (starts.Count > 0)
        {
            if (starts.Count > 1)
                starts.Sort();
            open.Add(starts[starts.Count - 1]);
        }
        if (ends.Count > 0)
        {
            if (ends.Count > 1)
                ends.Sort();
            open.Add(ends[0]);
        }
        return open.ToArray();
    }
    private void GetRuleDetail(UNIRESVRULE rule, uint? prop, ref devResvSta sta)
    {
        if (rule.CheckTbl != null && rule.CheckTbl.Length > 0)
            sta.ischeck = true;
        else
            sta.ischeck = false;
        sta.limit = rule.dwLimit;
        sta.rule = Converter.GetRsvRuleDetail(rule) + "&nbsp;" + Converter.GetDevKindPropDetail(prop);
    }
    bool isLongResv(uint? pro)
    {
        if (((uint)pro & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //m =0不空闲 =b审核通过 =r有预约
    DEVRESVTIME[] TranResvInfo(string arry, char[] m, uint? date)
    {
        List<DEVRESVTIME> list = new List<DEVRESVTIME>();
        uint start = 0;
        for (int v = 0; v < m.Length; v++)
        {
            char mk = m[v];
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i] == mk)
                {
                    if (i == 0 || arry[i - 1] != mk)
                    {
                        start = (uint)((i / 60) * 100 + (i % 60));
                    }
                    if (i == (arry.Length - 1) || arry[i + 1] != mk)
                    {
                        DEVRESVTIME item = new DEVRESVTIME();
                        item.dwBegin = start;
                        item.dwEnd = (uint)((i / 60) * 100 + (i % 60));
                        item.dwStatus = (m[v] == 'b' ? (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK : 0);
                        item.dwPurpose = 0;
                        item.dwPreDate = date;
                        list.Add(item);
                    }
                }
            }
        }
        return list.ToArray();
    }


    List<unidev> SortHot(List<unidev> list, DEVREQ devreq)
    {
        REPORTREQ req = new REPORTREQ();
        req.dwGetType = (int)REPORTREQ.DWGETTYPE.USERECGET_BYALL;
        req.dwClassKind = devreq.dwClassKind;
        DateTime now = DateTime.Now;
        DateTime m1 = now.AddMonths(-3);//三月
        DateTime m2 = now.AddMonths(1);
        req.dwStartDate = Convert.ToUInt32(m1.ToString("yyyyMMdd"));
        req.dwEndDate = Convert.ToUInt32(m2.ToString("yyyyMMdd"));
        req.szReqExtInfo.szOrderKey = "dwUseTimes";
        req.szReqExtInfo.szOrderMode = "DESC";
        DEVSTAT[] rlt;
        List<unidev> devs = new List<unidev>();
        //使用统计
        if (m_Request.Report.GetDevStat(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                DEVSTAT dev = rlt[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (ToUInt(list[j].id) == dev.dwDevID)
                    {
                        unidev d = list[j];
                        d.usetime = dev.dwUseTimes;
                        d.totaltime = dev.dwTotalUseTime;
                        devs.Add(d);
                        break;
                    }
                }
            }
        }
        return devs;
    }
    private uint[] StringToUintArr(string[] starts, string[] ends)
    {
        List<uint> arr = new List<uint>();
        int len = starts.Length < ends.Length ? starts.Length : ends.Length;
        for (int i = 0; i < starts.Length; i++)
        {
            if (starts[i] != "" && ends[i] != "")
            {
                arr.Add(ToUInt(starts[i].Replace(":", "")));
                arr.Add(ToUInt(ends[i].Replace(":", "")));
            }
        }
        return arr.ToArray();
    }

    private Filter InitFilter()
    {
        Filter fl = new Filter();
        fl.id = Request["id"];
        fl.name = Request["name"];
        fl.campus = Request["campus"];
        fl.dept = Request["dept"];
        fl.devcls = Request["devcls"];
        fl.clskind = Request["clskind"];
        fl.buildings = Request["buildings"];
        fl.kinds = Request["kind"];
        fl.lab = Request["lab"];
        fl.manager = Request["manager"];
        fl.runstat = Request["runstat"];
        fl.devstat = Request["devstat"];
        fl.statdate = Request["statdate"];
        fl.price = Request["prc"];
        fl.sortHot = Request["sortHot"];
        fl.pctrlId = Request["pctrlId"];
        uint star = (Request["pctrlStar"] == null) ? 0 : ToUInt(Request["pctrlStar"]);
        fl.pctrlStar = star > 0 ? star - 1 : 0;
        fl.pctrlNeed = Request["pctrlNeed"] == null ? 15 : ToUInt(Request["pctrlNeed"]);
        fl.showMode = Request["showMode"] == null ? 0 : ToUInt(Request["showMode"]);
        return fl;
    }
    string subCon(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            return str.Substring(0, str.Length - 1);
        }
        return "";
    }
}
public struct plan
{
    public string id;
    public string start;
    public string end;
    public string state;
    public string date;
    public string name;
    public string title;
    public string owner;
    public string accno;//预约人号
    public string member;
    public uint? limit;
    public bool occupy;//是否独占时段
}
public struct devResvSta
{
    public string id;
    public string title;
    public string name;
    public string devId;
    public string devName;
    public string clskind;
    public string kindId;
    public string kindName;
    public string classId;
    public string className;
    public string labName;
    public string labId;
    public string roomName;
    public uint? roomId;
    public uint? buildingId;
    public string buildingName;
    public string campus;
    public bool islong;//前端呈现方式
    public bool allowLong;//是否允许跨天
    public bool iskind;
    public bool ischeck;
    public uint? devsta;
    public uint? runsta;
    public string state;
    public int freeSta;//空闲状态值 单状态
    public int freeTime;//空闲时间
    public string[] freeTbl;//空闲状态表 长期状态
    public uint? ruleId;
    public string rule;
    public uint? prop;
    public uint? limit;
    public uint? earliest;
    public uint? latest;
    public uint? max;
    public uint? min;
    public uint? cancel;
    public uint? maxUser;
    public uint? minUser;
    public string ext;
    public string[] open;
    public string openStart;
    public string openEnd;
    public string[] clsDate;
    public plan[] ts;//繁忙时段
    public plan[] cls;//关闭时段
    public plan[] ops;//开放时段
}
class Filter
{
    public string id;
    public string name;
    public string campus;
    public string dept;
    public string devcls;
    public string kinds;
    public string clskind;
    public string lab;
    public string buildings;
    public string manager;
    public string runstat;
    public string devstat;
    public string statdate;
    public string price;
    public string pctrlId;
    public string sortHot;
    public uint? pctrlStar;
    public uint? pctrlNeed;
    public uint? showMode;
}
//设备
struct unidev
{
    public string id;
    public string url;
    public string name;
    public string model;
    public string campus;
    public string dept;
    public string devcls;
    public string kind;
    public string lab;
    public string building;
    public string manager;
    public string phone;
    public string intro;
    public string price;
    public uint? prop;
    public uint? usetime;//使用次数
    public uint? totaltime;//使用时间
    public string runstat;
    public string devstat;
    public string top;
    public string left;
    public string size;
}
class dwRlt
{
    public List<unidev> devs;
    public string pageCtrlID;
    public int totalLines;
    public int startLine;
    public int needLines;
    public int showMode;
}
