using System;
using System.Collections.Generic;
using System.Web;

namespace UniWebLib
{
    /// <summary>
    /// UniClientFunc 公共方法库
    /// </summary>
    public class UniClientFunc
    {
        public UniClientCommon common = new UniClientCommon();
        public void LoadPage()
        {
            common.LoadPage();
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }
        public UniRequest GetRequest()
        {
            return common.GetRequest();
        }
        public string GetConfig(string cfg)
        {
            return common.GetConfig(cfg);
        }
    }

    public partial class UniClientPage : System.Web.UI.Page
    {
        //xml幻灯片
        public void InitXmlSlide(string id, string type, ref string szPicZoom, ref string szPicPath)
        {
            string tmp = GetXmlContent(id, type);
            List<string> list = GetSrcFromHtml(tmp);
            for (int i = 0; i < list.Count; i++)
            {
                string src = list[i];
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
        //获取楼宇 下拉框=默认 单选=radio
        public string GetBuildingHtm(string type)
        {
            return GetBuildingHtm(type, null, null);
        }
        public string GetBuildingHtm(string type, string campus, uint? aty)
        {
            string ret = "";
            BUILDINGREQ req = new BUILDINGREQ();
            if (!string.IsNullOrEmpty(campus))
                req.szCampusIDs = campus;
            if (aty != null && aty != 0)
                req.dwActivitySN = aty;
            UNIBUILDING[] rlt;
            if (m_Request.Device.BuildingGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    ret += "<option value=\"" + rlt[i].dwBuildingID + "\" depend=" + rlt[i].dwCampusID + ">" + rlt[i].szBuildingName + "</option>";
                }
            }
            return ret;
        }
        //获取设备类别 下拉框=默认 单选=radio
        public string GetDevClassHtm(string type)
        {
            string ret = "";
            DEVCLSREQ req = new DEVCLSREQ();
            UNIDEVCLS[] rlt;
            if (m_Request.Device.DevClsGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    if (type == "radio")
                        ret += "<a class='it' value=\"" + rlt[i].dwClassID + "\"><input type='radio'  /> " + rlt[i].szClassName + "</a>";
                    else
                        ret += "<option value=\"" + rlt[i].dwClassID + "\">" + rlt[i].szClassName + "</option>";
                }
            }
            return ret;
        }
        //获取校区 下拉框=默认 单选=radio
        public string GetCampusHtm(string type)
        {
            string ret = "";
            CAMPUSREQ req = new CAMPUSREQ();
            UNICAMPUS[] rlt;
            if (m_Request.Account.CampusGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    if (type == "radio")
                        ret += "<a class='it' value=\"" + rlt[i].dwCampusID + "\"><input type='radio'  /> " + rlt[i].szCampusName + "</a>";
                    else
                        ret += "<option value=\"" + rlt[i].dwCampusID + "\">" + rlt[i].szCampusName + "</option>";
                }
            }
            return ret;
        }
        //获取实验室 下拉框=默认 单选=radio
        public string GetLabHtm(string type)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            LABREQ vrGet = new LABREQ();
            UNILAB[] vtResult;
            uResponse = m_Request.Device.LabGet(vrGet, out vtResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
            {
                string rel = "";
                for (int i = 0; i < vtResult.Length; i++)
                {
                    if (type == "radio")
                        rel += "<a class='it' value=\"" + vtResult[i].dwLabID + "\"><input type='radio'  /> " + vtResult[i].szLabName + "</a>";
                    else
                        rel += "<option value=\"" + vtResult[i].dwLabID + "\">" + vtResult[i].szLabName + "</option>";
                }
                return rel;
            }
            return "";
        }
        //获取子系统名称
        public string GetSubSysName(uint? kind)
        {
            if ((kind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) > 0)
            {
                string name = GetConfig("SysKindRoom");
                return Translate(name == "" ? "空间" : name);
            }
            else if ((kind & (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT) > 0)
            {
                string name = GetConfig("SysKindSeat");
                return Translate(name == "" ? "座位" : name);
            }
            else if ((kind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER) > 0)
            {
                string name = GetConfig("SysKindPC");
                return Translate(name == "" ? "电子阅览室" : name);
            }
            else if ((kind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)
            {
                string name = GetConfig("SysKindLend");
                return Translate(name == "" ? "外借设备" : name);
            }
            else
                return "";
        }
    }
}