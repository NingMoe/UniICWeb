using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace UniWebLib
{
    /// <summary>
    /// UniClientAjax 的摘要说明
    /// </summary>
    public partial class UniClientAjax : UniClientPage
    {
        protected UNIACCOUNT curAcc;
        protected string act = "";
        protected bool LoadPage()
        {
                return LoadPage(true);
        }
        //load=false 则不验证并初始化登录 未使用
        protected bool LoadPage(bool load)
        {
            if (load)
                base.LoadPage();
            Response.Clear();
            if ("InternetExplorer".Equals(Request.Browser.Browser))
                Response.ContentType = "text/html";
            else
                Response.ContentType = "application/Json";
            if (string.IsNullOrEmpty(Request["act"]))
            {
                NoAct();
                return false;
            }
            else
            {
                act = Request["act"];
                if (!CheckSign(act))
                {
                    act = "sign_dangerous";
                    ErrMsg("包含危险字符，拒绝响应。");
                    return false;
                }
                return true;
            }
        }
        protected virtual bool IsLoginReady()
        {
            if (Session["LOGIN_ACCINFO"] == null )
            {
                act = act + "/nologin/timeout";
                JsRet(-1, "未登录或登录超时 session =null，请重新登录。");
                return false;
            }
            else if(!IsClientLogin()) {
                act = act + "/nologin/timeout";
                JsRet(-1, "未登录或登录超时，请重新登录。");
                return false;
            }
            else
            {
                curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                return true;
            }
        }
        protected void NoBuffer()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
        }
        protected void ErrMsg()
        {
            ErrMsg("出现异常，操作失败！");
        }
        //参数有误
        protected void ErrMsgP()
        {
            ErrMsg("参数有误");
        }
        protected void ErrMsg(string msg)
        {
            if (string.IsNullOrEmpty(msg)) msg = "未获取到相关提示信息";
            string lower = msg.ToLower();
            if (lower.IndexOf("管理员未登录") > -1 || lower.IndexOf("匿名用户") > -1 || lower.IndexOf("无法访问已释放的对象") > -1)
            {
                act = act + "/timeout";
                Session["LOGIN_ACCINFO"] = null;
                JsRet(-1, "登录超时，将重新登录。");
            }
            else
            {
                JsRet(0, msg);
            }
        }
        protected void NoAct()
        {
            act = "no_act";
            ErrMsg("当前的操作没有相关处理程序。");
        }
        protected void SucMsg()
        {
            SucMsg(Translate("操作成功！"));
        }
        protected void SucMsg(string msg)
        {
            JsRet(1, msg);
        }
        protected void SucRlt(object data)
        {
            JsRet(1, "ok", CodeJson(data), "null");
        }
        protected void SucRlt(int ret, object data)
        {
            JsRet(ret, "ok", CodeJson(data), "null");
        }
        protected void SucRlt(string msg, object data)
        {
            JsRet(1, msg, CodeJson(data), "null");
        }
        protected void SucRlt(int ret, object data, string ext)
        {
            JsRet(ret, "ok", CodeJson(data), ext);
        }
        protected void SucRltPCtrl(object data, unipctrl pc)
        {
            JsRet(1, "ok", CodeJson(data), "{\"total\":\"" + pc.total + "\",\"start\":\"" + pc.start + "\",\"need\":\"" + pc.need + "\",\"name\":\"" + pc.name + "\"}");
        }
        protected void JsRet(int ret, string msg)
        {
            JsRet(ret, msg, "null", "null");
        }
        protected void JsRet(int ret, string msg, string data, string ext)
        {
            Response.Write("{\"ret\":" + ret + ",\"act\":\"" + act + "\",\"msg\":\"" + msg + "\",\"data\":" + data + ",\"ext\":" + ext + "}");
        }

        protected string CodeJson(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            if (obj is string)
            {
                return obj.ToString();
            }
            if (obj is Int32 || obj is UInt32)
            {
                return obj.ToString();
            }
            return JsonConvert.SerializeObject(obj);
        }

        //静态资源
        static string[] SqlForbid = @"--|'".Split('|');//禁用字符
        static string[] anySqlStr = @"<|>|*|+|=".Split('|');//警告字符
        static string[] anySqlKey = @"select|insert|delete|from|admin|set|alter|where|or|and|delay".Split('|');//确认字符
        static string[] actWhiteList =
    "set_dev_coord|set_xml_data|save_app_settings".Split('|');//白名单的data字段内容可以不参与危险字符检查
        protected bool CheckSign(string act)
        {
            //白名单
            bool white = false;
            foreach (string item in actWhiteList)
            {
                if (item == act)
                {
                    white = true;
                    break;
                }
            }
            bool safe = CheckUrl();
            //检查传参安全性
            System.Collections.Specialized.NameValueCollection fm = Request.Form;
            if (fm.Count > 0 && safe)
            {
                List<string> strList = new List<string>();
                for (int i = 0; i < fm.Count; i++)
                {
                    if (white && fm.GetKey(i) == "data") continue;//跳过白名单data
                    strList.Add(fm[i].ToLower());
                }
                safe = ProcessSqlStr(strList);
            }
            return safe;
        }
        //检查url
        protected bool CheckUrl()
        {
            bool safe = true;
            System.Collections.Specialized.NameValueCollection query = Request.QueryString;
            if (query.Count > 0)
            {
                List<string> strList = new List<string>();
                for (int i = 0; i < query.Count; i++)
                {
                    strList.Add(query[i].ToLower());
                }
                safe = ProcessSqlStr(strList);
            }
            return safe;
        }
        private bool ProcessSqlStr(List<string> strList)
        {
            try
            {
                if (strList.Count>0)
                {
                    bool flag = true;
                    for (int i = 0; i < strList.Count; i++)
                    {
                        string[] arr = strList[i].Split(' ');
                    foreach (string ss in SqlForbid)
                    {
                        if (IsInArray(arr,ss))
                        {
                            return false;
                        }
                    }
                    foreach (string ss in anySqlStr)
                    {
                        if (IsInArray(arr, ss))
                        {
                            flag= false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        foreach (string key in anySqlKey)
                        {
                            if (IsInArray(arr, key))
                            {
                                return false;
                            }
                        }
                    }
                    }

                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool IsInArray(string[] arr,string str)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == str)
                {
                    return true;
                }
            }
            return false;
        }
    }
}