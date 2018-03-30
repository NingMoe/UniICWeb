using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class ClientWeb_pro_ajax_util : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "del_data")
            {
                if (IsLoginReady())
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
                        SucMsg();
                    }
                    else
                    {
                        ErrMsg(m_Request.szErrMsg);
                    }
                }
            }
            else if (act == "get_language")
            {
                Dictionary<string, string> dic = common.GetLanguageDic(null);
                SucRlt(dic);
            }
            else if (act == "set_language")
            {
                string lang = Request["language"];
                if (SetLanguage(lang))
                    SucMsg();
                else
                    ErrMsg();
            }
            else if (act == "get_fee")
            {
                getFee();
            }
            else if (act == "save_app_settings")
            {
                if (IsLogined())
                {
                    saveAppSettings();
                }
                else
                {
                    ErrMsg("未登录或登录已超时");
                }
            }
            else if (act == "get_app_settings")
            {
                if (IsLogined())
                {
                    getAppSettings();
                }
                else
                {
                    ErrMsg("未登录或登录已超时");
                }
            }
            else if (act == "set_xml_data")
            {
                SetXmlData();
            }
            else if (act == "get_code")
            {
                GetCode();
            }
            else if (act == "set_vote")
            {
                castVote();
            }
            else
            {
                NoAct();
            }
        }
    }

    private void castVote()
    {
        uint voteId = ToUInt(Request["vote_id"]);
        string itemValues = Request["item_v"];
        if (voteId != 0)
        {
            string[] its = itemValues.Split(',');
            List<POLLVOTE> list = new List<POLLVOTE>();
            for (int i = 0; i < its.Length; i++)
            {
                POLLVOTE set = new POLLVOTE();
                set.dwPollID = voteId;
                set.dwItemID = ToUInt(its[i]);
                list.Add(set);
            }
            if (m_Request.Admin.VotePollOnLine(list.ToArray()) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                SucMsg();
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
        }
        else
        {
            ErrMsgP();
        }
    }
    private void GetCode()
    {
        uint type = ToUInt(Request["type"]);
        string sn = Request["sn"];
        CODINGTABLE[] rlt = GetCodeTable(type, sn);
        if (rlt != null)
        {
            SucRlt(rlt);
        }
        else
        {
            ErrMsg();
        }
    }

    private void SetXmlData()
    {
        string id = Request["id"];
        string type = Request["type"];
        string data = Request["data"];
        if (SaveXmlData(id, data, type))
        {
            SucMsg();
        }
        else { ErrMsg(); }
    }

    private void saveAppSettings()
    {
        string data = Request["data"];
        string[] opts = data.Split('$');
        Dictionary<string, string> dic = new Dictionary<string, string>();
        for (int i = 0; i < opts.Length; i++)
        {
            string[] kvp = opts[i].Split('&');
            if (kvp.Length == 2)
            {
                dic.Add(kvp[0], kvp[1]);
            }
        }
        string path = Request["path"];
        if (string.IsNullOrEmpty(path)) path = "web.config";
        path = HttpContext.Current.Server.MapPath("~/ClientWeb/") + path;
        //文件处理
        if (System.IO.File.Exists(path))
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                XmlNode root = xml.SelectSingleNode("configuration");
                XmlNode set = root.SelectSingleNode("appSettings");
                XmlNodeList list = set.SelectNodes("add");
                foreach (KeyValuePair<string, string> item in dic)
                {
                    XmlNode node = null;
                    foreach (XmlNode it in list)
                    {
                        if (it.Attributes["key"].Value == item.Key)
                        {
                            node = it;
                            break;
                        }
                    }
                    if (node != null)
                    {
                        XmlElement e = (XmlElement)node;
                        e.SetAttribute("value", item.Value);
                    }
                    else
                    {
                        XmlElement e = xml.CreateElement("add");
                        e.SetAttribute("key", item.Key);
                        e.SetAttribute("value", item.Value);
                        set.AppendChild(e);
                    }
                }
                xml.Save(path);
                SucMsg();
            }
            catch (Exception ex)
            {
                ErrMsg("文件操作有误：" + ex.Message);
            }
        }
        else
            ErrMsg("配置文件不存在");
    }
    private void getAppSettings()
    {
        string path = Request["path"];
        if (string.IsNullOrEmpty(path)) path = "web.config";
        path = HttpContext.Current.Server.MapPath("~/ClientWeb/") + path;
        //文件处理
        if (System.IO.File.Exists(path))
        {
            try
            {
                string ret = "";
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                XmlNode root = xml.SelectSingleNode("configuration");
                XmlNode set = root.SelectSingleNode("appSettings");
                XmlNodeList list = set.SelectNodes("add");
                foreach (XmlNode it in list)
                {
                    ret += it.Attributes["key"].Value + "&" + it.Attributes["value"].Value + "$";
                }
                if (ret.Length > 0) ret = ret.Substring(0, ret.Length - 1);
                SucRlt("\"" + ret + "\"");
            }
            catch (Exception ex)
            {
                ErrMsg("文件操作有误：" + ex.Message);
            }
        }
        else
            ErrMsg("配置文件不存在");
    }

    private void getFee()
    {
        FEEREQ req = new FEEREQ();
        string dept = Request["dept"];
        string kind = Request["dev_kind"];
        string ident = Request["ident"];
        string sn = Request["fee_sn"];
        if (!string.IsNullOrEmpty(dept) && dept != "0")
            req.dwDeptID = ToUInt(dept);
        if (!string.IsNullOrEmpty(kind) && kind != "0")
            req.dwDevKind = ToUInt(kind);
        if (!string.IsNullOrEmpty(ident) && ident != "0")
            req.dwIdent = ToUInt(ident);
        if (!string.IsNullOrEmpty(sn) && sn != "0")
            req.dwIdent = ToUInt(sn);
        UNIFEE[] rlt;
        if (m_Request.Fee.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucRlt(rlt);
        }
        else
            ErrMsg();
    }
}