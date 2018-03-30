using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using UniWebLib;
using UniLibrary;

/// <summary>
/// UniPage 的摘要说明
/// </summary>
/// 

namespace UniWebLib
{
    public partial class UniPage : System.Web.UI.Page
    {
        public UniCommon Common = new UniCommon();

        public string m_szScript="";
        public bool m_bRemember = true;
        private string m_szPutJsObj = "";
        public uint nDefaultNeedLine = 0;
        uint m_szMessageBox_Type;
        string m_szMessageBox_Msg;
        string m_szMessageBox_Title;
        string m_szMessageBox_Action;
        UniRequest m_pRequest = null;

        public bool m_Export = false;
        protected CUseTime m_UseTime = new CUseTime();
        protected string m_szCurrentExecutionFilePath = "";

        protected string MyVPath = "/";

        public UniPage()
        {
            m_UseTime.Add("UniPage");
        }
        ~UniPage()
        {
            string szSessionID = "";
            m_UseTime.Add("~UniPage");
            try
            {
                if (Session["SessionID"] != null)
                {
                    szSessionID = ((uint)Session["SessionID"]).ToString();
                }
            }
            catch (Exception e)
            {
            }
           // Logger.Trace(m_szCurrentExecutionFilePath + ":" + m_UseTime.GetAllUseTime());
            SysConsole.LogPageStatLast(szSessionID, m_szCurrentExecutionFilePath, m_UseTime);
        }
        protected override void OnUnload(EventArgs e)
        {
            m_UseTime.Add("OnUnload_Start");
            base.OnUnload(e);
            m_UseTime.Add("OnUnload");

            string szSessionID = "";
            try
            {
                if (Session["SessionID"] != null)
                {
                    szSessionID = ((uint)Session["SessionID"]).ToString();
                }
            }
            catch (Exception ee)
            {
            }
            //Logger.Trace(m_szCurrentExecutionFilePath + ":" + m_UseTime.GetAllUseTime());
            SysConsole.LogPageStat(szSessionID, m_szCurrentExecutionFilePath, m_UseTime);
        }

        protected override void OnInit(EventArgs e)
        {
            m_szCurrentExecutionFilePath = Request.CurrentExecutionFilePath;

            m_UseTime.Add("OnInit_Start");
            base.OnInit(e);
            m_UseTime.Add("OnInit");
        }
        protected override void OnLoad(EventArgs e)
        {
            m_UseTime.Add("OnLoad_Start");

            if (Request.ApplicationPath != "/")
            {
                MyVPath = Request.ApplicationPath + "/";
            }
            else
            {
                MyVPath = "/";
            }

            base.OnLoad(e);


            m_UseTime.Add("OnLoad");
        }
        protected override void OnPreLoad(EventArgs e)
        {
            m_UseTime.Add("OnPreLoad_Start");
            base.OnPreLoad(e);
            m_UseTime.Add("OnPreLoad");
        }
        protected override void OnAbortTransaction(EventArgs e)
        {
            m_UseTime.Add("OnAbortTransaction_Start");
            base.OnAbortTransaction(e);
            m_UseTime.Add("OnAbortTransaction");
        }
        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            m_UseTime.Add("OnBubbleEvent_Start");
            bool ret = base.OnBubbleEvent(source, args);
            m_UseTime.Add("OnBubbleEvent");
            return ret;
        }
        protected override void OnCommitTransaction(EventArgs e)
        {
            m_UseTime.Add("OnCommitTransaction_Start");
            base.OnCommitTransaction(e);
            m_UseTime.Add("OnCommitTransaction");
        }
        protected override void OnDataBinding(EventArgs e)
        {
            m_UseTime.Add("OnDataBinding_Start");
            base.OnDataBinding(e);
            m_UseTime.Add("OnDataBinding");
        }
        protected override void OnError(EventArgs e)
        {
            m_UseTime.Add("OnError_Start");
            base.OnError(e);
            m_UseTime.Add("OnError");
        }
        protected override void OnPreRender(EventArgs e)
        {
            m_UseTime.Add("OnPreRender_Start");
            base.OnPreRender(e);
            m_UseTime.Add("OnPreRender");
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            m_UseTime.Add("OnPreRenderComplete_Start");
            base.OnPreRenderComplete(e);
            m_UseTime.Add("OnPreRenderComplete");
        }
        protected override void OnSaveStateComplete(EventArgs e)
        {
            m_UseTime.Add("OnSaveStateComplete_Start");
            base.OnSaveStateComplete(e);
            m_UseTime.Add("OnSaveStateComplete");
        }

        protected override void OnPreInit(EventArgs e)
        {
            m_UseTime.Add("OnPreInit_Start");
            base.OnPreInit(e);

            if (!string.IsNullOrEmpty(Request["_AjaxID"]))
            {
                Response.Filter = new AjaxFilter(this,Response.Filter, Response.ContentEncoding,Request["_AjaxID"]);
            }
            if (!string.IsNullOrEmpty(Request["_ExportID"]) && Request["_Export"] == "CSV")
            {
                if (Session["ExportFlag"] != null && (int)Session["ExportFlag"] > 0)
                {
                    Logger.Trace("Export Error");
                    Response.Write("Error");
                    Response.End();
                    m_UseTime.Add("OnPreInit");
                    return;
                }
                Session["ExportFlag"] = (int)1;

                m_Export = true;
                string szName = Request["_ExportName"];
                if (string.IsNullOrEmpty(szName)) szName = "新的导出文件";

                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(szName + ".xls"));
                Response.ContentType = "application/ms-excel";

                m_Request.m_UniDCom.m_dwTimeout = 3 * 60 * 60 * 1000;//3小时
                Response.Filter = new ExportCSVFilter(Response.Filter, Response.ContentEncoding, Request["_ExportID"], szName);
            }
            m_UseTime.Add("OnPreInit");
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            m_UseTime.Add("OnLoadComplete_Start");
            if (m_bRemember && Header != null && Header.Controls != null)
            {
                PushHistory();
            }
            if (m_szMessageBox_Action == null) m_szMessageBox_Action = "";
            if (!string.IsNullOrEmpty(m_szMessageBox_Msg))
            {
                m_szScript += "$(function(){MessageBox(\"" + m_szMessageBox_Msg + "\",\"" + m_szMessageBox_Title + "\"," + m_szMessageBox_Type + ",function(){" + m_szMessageBox_Action + "});});";
                HtmlGenericControl ScriptInclude = new HtmlGenericControl("script");
                ScriptInclude.Attributes.Add("type", "text/javascript");
                ScriptInclude.InnerHtml = m_szScript;

                if (Header != null && Header.Controls != null)
                {
                    Header.Controls.Add(ScriptInclude);
                }
                else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
                {
                    Form.Controls.Add(ScriptInclude);
                }
                else if (this.Controls != null && !this.Controls.IsReadOnly)
                {
                    this.Controls.Add(ScriptInclude);
                }
            }

            //==================PutHttpObj===========
            if (string.IsNullOrEmpty(m_szPutJsObj))
            {
                if (Request.QueryString.AllKeys.Length > 0 || Request.Form.AllKeys.Length > 0)
                {
                    string szResult = "[";
                    foreach (string szKey in Request.QueryString.AllKeys)
                    {
                        if (szKey == null || (szKey.Length >= 2 && szKey.Substring(0, 2) == "__"))
                        //if (szKey == "__VIEWSTATE" || szKey == "__EVENTVALIDATION")
                        {
                            continue;
                        }

                        string szVal = Request.QueryString[szKey];
                        szVal = szVal.Replace("\\", "\\\\").Replace("'", "\\'");
                        szVal = Server.HtmlDecode(szVal);
                        szResult += "['" + szKey + "','" + szVal + "'],";
                    }
                    foreach (string szKey in Request.Form.AllKeys)
                    {
                        //if (szKey.Length >=2 && szKey.Substring(0,2) == "__") {
                        if (szKey == "__VIEWSTATE" || szKey == "__EVENTVALIDATION")
                        {
                            continue;
                        }
                        string szVal = Request.Form[szKey];
                        szVal = szVal.Replace("\\", "\\\\").Replace("'", "\\'");
                        szResult += "['" + szKey + "','" + szVal + "'],";
                    }
                    if (szResult.EndsWith(","))
                    {
                        szResult = szResult.Substring(0, szResult.Length - 1);
                    }
                    szResult += "]";

                    m_szPutJsObj = szResult;
                }
            }
            if (!string.IsNullOrEmpty(m_szPutJsObj))
            {
                HtmlGenericControl ScriptInclude2 = new HtmlGenericControl("script");
                ScriptInclude2.Attributes.Add("type", "text/javascript");
                //ScriptInclude2.InnerHtml = "$(function(){ PutHttpValue(" + m_szPutJsObj + ");});";
                ScriptInclude2.InnerHtml = "function SetDefaultValue(){ PutHttpValue(" + m_szPutJsObj + ");}$(SetDefaultValue);";

                if (Header != null && Header.Controls != null)
                {
                    Header.Controls.Add(ScriptInclude2);
                }
                else if (Form != null && Form.Controls != null)
                {
                    if (!Form.Controls.IsReadOnly)
                    {
                        Form.Controls.Add(ScriptInclude2);
                    }
                    else
                    {
                        if (this.Controls != null && !this.Controls.IsReadOnly)
                        {
                            this.Controls.Add(ScriptInclude2);
                        }
                    }
                }
            }
            //==================PutHttpObj=============
            m_Request.m_UniDCom.m_dwTimeout = 0;
            if (m_Export)
            {
                Session["ExportFlag"] = (int)100;
            }
            m_UseTime.Add("OnLoadComplete");
        }

        SessionMng sessionMng = new SessionMng();
        public UniRequest GetRequest()
        {
            if (m_pRequest != null)
                return m_pRequest;

            uint nSessionID = 0;
            uint nStationSN = 0;
            if (Session["SessionID"] != null)
            {
                nSessionID = (uint)Session["SessionID"];
            }
            if (Session["StationSN"] != null && Session["StationSN"].GetType() == typeof(uint))
            {
                nStationSN = (uint)Session["StationSN"];
            }
            m_pRequest = sessionMng.GetRequest(nSessionID, nStationSN);
            m_pRequest.OnError = OnError;
            return m_pRequest;
        }
        public void ReleaseRequest(uint SessionID)
        {
            sessionMng.ReleaseRequest(SessionID);
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }

	    public void OnError(PRModule module,REQUESTCODE ret)
        {
            if (ret == REQUESTCODE.ERR_IMPORT)
            {
                Logger.Trace("ERR_IMPORT Reconnect");
                m_Request.m_UniDCom.Close();
            }
            else if (ret == REQUESTCODE.EXECUTE_FAIL)
            {
            }
            else if ((uint)ret == 0x02002001)
            {
                Session["SessionID"] = null;
                Session["LoginResult"] = null;
                Session["URLHistoryStack"] = null;
                m_Request.m_UniDCom.Close();
                //MessageBox(m_Request.szErrMessage, "系统信息", MSGBOX.WARN, "~/Sys/Login.aspx");
                //登录超时
                //Response.Redirect("~/Sys/Login.aspx");
            }
        }

        public void GetHTTPObj<T>(out T obj) where T : new()
        {
            obj = (T)UniLibrary.ObjHelper.HTTP2OBJ(this, typeof(T));
        }

        public void PutHTTPObj<T>(T obj) where T : new()
        {
            PutJSObj(obj);
            //废弃旧的方式
            //UniLibrary.ObjHelper.OBJ2HTTP(this,obj);
        }

        public void PutJSObj<T>(T obj) where T : new()
        {
            m_szPutJsObj = UniLibrary.ObjHelper.OBJ2JS(obj);
        }

        public object GetMemberVlaue(object obj, string szName)
        {
            return UniLibrary.ObjHelper.GetMemberVlaue(obj, szName);
        }

        public void SetMemberVlaue(object obj, string szName, object val)
        {
            UniLibrary.ObjHelper.SetMemberVlaue(obj, szName, val);
        }

        public object GetReserved(object obj, string szName)
        {
            Reserved res = (Reserved)GetMemberVlaue(obj, "reserved");
            if (res.Ext == null)
            {
                return null;
            }
            return res.Ext[szName];
        }
        public void SetReserved<T>(ref T objp, string szName, object val)
        {
            object obj = objp;
            Reserved res = (Reserved)GetMemberVlaue(obj, "reserved");
            if (res.Ext == null)
            {
                res.Ext = new System.Collections.Generic.Dictionary<string, object>();
            }
            res.Ext[szName] = val;
            UniLibrary.ObjHelper.SetMemberVlaue(obj, "reserved", res);
            objp = (T)obj;
        }

        public enum MSGBOX
        {
            INFO = 0, WARN = 1, ERROR = 2, SUCCESS = 3
        }
        public void MessageBox(string szMsg, string szTitle, MSGBOX nType)
        {
            MessageBox_Action(szMsg, szTitle, nType, null);
        }
        public enum MSGBOX_ACTION
        {
            NONE = 0, BACK = 1, OK = 2,CANCEL = 3
        }
        public void MessageBox(string szMsg, string szTitle, MSGBOX nType, MSGBOX_ACTION nBack)
        {
            string szAction = "";
            if (nBack == MSGBOX_ACTION.BACK)
            {
                //szAction = "history.go(-2);";
                string szUrl = PopHistory();
                if (szUrl != null)
                {
                    szAction = "location.href='" + szUrl + "';";
                }
            }
            else if (nBack == MSGBOX_ACTION.OK)
            {
                szAction = "Dlg_OK()";
            }
            else if (nBack == MSGBOX_ACTION.CANCEL)
            {
                szAction = "Dlg_Cancel()";
            }
            MessageBox_Action(szMsg, szTitle, nType, szAction);
        }
        public void MessageBox(string szMsg, string szTitle, MSGBOX nType, string szURL)
        {
            string szAction = "";
            if (!string.IsNullOrEmpty(szURL))
            {
                szAction = "location.href='" + szURL+"';";
            }
            MessageBox_Action(szMsg, szTitle, nType, szAction);
        }
        public void MessageBox_Action(string szMsg, string szTitle, MSGBOX nType, string szAction)
        {
            m_szMessageBox_Type = (uint)nType;
            m_szMessageBox_Msg = szMsg;
            m_szMessageBox_Title = szTitle;
            m_szMessageBox_Action = szAction;
        }

        public string[] GetSelectID(/*string szTableID*/)
        {
            //TODO:实现按szTableID获取
            string szTableID = "";

            string szSelect = Request[szTableID+"tblSelector"];
            if (string.IsNullOrEmpty(szSelect))
            {
                return new string[0];
            }
            return szSelect.Split(new char[] { ','});
        }

        struct URLHISTORY_NODE
        {
            public string szKey;
            public string szURL;
        }

        public void PushHistory()
        {
            //if (!IsPostBack)
            {
                URLHISTORY_NODE node = new URLHISTORY_NODE();
                System.Collections.Stack stSession = (System.Collections.Stack)Session["URLHistoryStack"];
                node.szKey = Request.FilePath.ToLower();
                node.szURL = Request.Url.ToString();
                if (stSession != null)
                {
                    if (stSession.Count > 0)
                    {
                        URLHISTORY_NODE topnode = (URLHISTORY_NODE)stSession.Peek();
                        if (topnode.szKey != node.szKey)
                        {
                            stSession.Push(node);
                        }
                    }
                    else
                    {
                        stSession.Push(node);
                    }
                }
                else
                {
                    System.Collections.Stack stNow = new System.Collections.Stack();
                    stNow.Push(node);
                    Session["URLHistoryStack"] = stNow;
                }
            }
        }

        public string PopHistory()
        {
            System.Collections.Stack stSession = (System.Collections.Stack)Session["URLHistoryStack"];
            if (stSession != null)
            {
                if (stSession.Count > 1)
                {
                    stSession.Pop();
                    URLHISTORY_NODE topnode = (URLHISTORY_NODE)stSession.Pop();
                    return (topnode.szURL);
                }
            }
            return null;
        }

        public void Back()
        {
            string szHistory = PopHistory();
            if (szHistory != null)
            {
                Response.Redirect(szHistory);
            }
        }

        public uint ToUint(string szValue)
        {
            uint nValue = 0;
            uint.TryParse(szValue, out nValue);
            return nValue;
        }
        public uint ToUint(uint? szValue)
        {
            if (szValue == null)
                return 0;
            else
                return (uint)szValue;
        }

        public uint GetDate(string szDate)
        {
            uint dwret = 0;
            if (string.IsNullOrEmpty(szDate))
            {
                return 0;
            }
            DateTime dtRet;
            DateTime.TryParse(szDate, out dtRet);
            dwret = (uint)(dtRet.Year * 10000 + dtRet.Month * 100 + dtRet.Day);
            return dwret;
        }
        public uint GetDate(DateTime dtRet)
        {
            uint dwret = (uint)(dtRet.Year * 10000 + dtRet.Month * 100 + dtRet.Day);
            return dwret;
        }
        public string GetDateStr(uint? nDate)
        {
            if (nDate == null || nDate == 0)
            {
                return "";
            }
            string szDate = (nDate / 10000).ToString() + "-" + string.Format("{0:D2}", ((nDate / 100) % 100)) + "-" + string.Format("{0:D2}", (nDate % 100));
            return szDate;
        }

        public uint GetTime(string szTime)
        {
            DateTime dt;
            DateTime.TryParse(szTime, out dt);
            return (uint)(dt.Hour * 100 + dt.Minute);
        }
        public string GetTimeStr(uint? nTime)
        {
            if (nTime == null) nTime = 0;
            string szTime = string.Format("{0:D2}", nTime / 100) + ":" + string.Format("{0:D2}", nTime % 100);
            return szTime;
        }
        public string GetTimeForSecond(uint uSec)//把秒数转化为天、小时、分钟
        {           
            string szTime ="";
            if ((uSec / 86400) > 0)            
            {
                szTime += ((uSec / 86400)).ToString("0") + "天";
            }
            if (((uSec % 86400) / 3600) > 0)
            {
                szTime += ((uSec % 86400) / 3600).ToString("0") + "小时";
            }
            if ((((uSec % 86400) % 3600) / 60) > 0)
            {
                szTime += (((uSec % 86400) % 3600) / 60).ToString("0") + "分钟";
            }  
            if(szTime=="")
            {
                szTime="0分钟";
            }
            return szTime;
        }
        public double ChinaRound(double value, int decimals)//四舍五入
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
        public string GetOSVer(uint? _dwOSVer)
        {
            uint dwOSVer = (uint)_dwOSVer;
            uint dwBit = (uint)dwOSVer % 100;
            uint dwOS = dwOSVer / 10000;
            string szOS = "";
            if (dwOS == 500)
            {
                szOS = "Windows 2000";
            }
            else if (dwOS == 501)
            {
                szOS = "Windows XP";
            }
            else if (dwOS == 502)
            {
                szOS = "Windows Server 2003";
            }
            else if (dwOS == 600)
            {
                szOS = "Windows Vista";
            }
            else if (dwOS == 601)
            {
                szOS = "Windows 7";
            }
            else if (dwOS == 602)
            {
                szOS = "Windows 8";
            }
            return szOS + " " + dwBit.ToString()+"位";
        }
        public uint Get1970Seconds(string Date)//返回和1970的差距秒数
        {
            uint result = 0;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            try
            {
                DateTime dtDate = DateTime.Parse(Date);
                TimeSpan spDate = dtDate.Subtract(dt1970);
                result = (uint)spDate.TotalSeconds;
            }
            catch
            {
                return 0;
            }
            return result;
        }
        public string Get1970Date(uint? TotalSeconds)//根据差距秒数 算出现在是日期
        {
            if (TotalSeconds == null || TotalSeconds == 0)
            {
                return "";
            }
            uint TotalSecondsIn = (uint)TotalSeconds;
            string result = string.Empty;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            DateTime dtNow = dt1970.AddSeconds(TotalSecondsIn);
            return result = dtNow.ToString("yyyy-MM-dd HH:mm");
        }
        public string Get1970Date(uint? TotalSeconds, string szFormat)//根据差距秒数 算出现在是日期
        {
            if (TotalSeconds == null ||TotalSeconds==0)
            {
                return "";
            }
            uint TotalSecondsIn = (uint)TotalSeconds;
            string result = string.Empty;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            DateTime dtNow = dt1970.AddSeconds(TotalSecondsIn);
            return result = dtNow.ToString(szFormat);
        }     
        public string GetMinToStr(uint? TotalMin)//根据差距秒数 算出现在是日期
        {
            if (TotalMin == null)
            {
                return "";
            }
            if (TotalMin < 60)
            {
                return TotalMin + "分钟";
            }
            else if (TotalMin < 1440)
            {
                string szMin = (TotalMin % 60) == 0 ? "" : (TotalMin % 60).ToString()+"分钟";
                return TotalMin / 60 + "小时" + szMin;
            }
            else 
            {
                string szHour = (TotalMin % 1440/60) == 0 ? "" : (TotalMin % 1440/60).ToString() + "小时";
                string szMin = (TotalMin % 1440%60) == 0 ? "" : (TotalMin % 1440%60).ToString() + "分钟";
                return TotalMin / 1440 + "天" + szHour + szMin;
            }
            return "";
        }

        public void GetPageCtrlValue(out REQEXTINFO ext)
        {
            ext = (REQEXTINFO)UniLibrary.ObjHelper.HTTP2OBJ(this, "_", typeof(REQEXTINFO));

            if (ext.dwStartLine != null && ext.dwStartLine != 0)
            {
                ext.dwStartLine--;
            }
            if (ext.dwNeedLines == null || ext.dwNeedLines==0)
            {
                ext.dwNeedLines = nDefaultNeedLine;
            }
            if (ext.dwNeedLines == null || ext.dwNeedLines == 0)
            {
                ext.dwNeedLines = 10;
            }
        }
		
        public void UpdatePageCtrl()
		{
			UpdatePageCtrl(null);
		}

        public void UpdatePageCtrl(PRModule ActionModule)
        {
            REQEXTINFO extold;
            GetPageCtrlValue(out extold);
            REQEXTINFO ext;
            if (ActionModule == null)
            {
                ActionModule = m_Request.Station;
            }
            if (ActionModule.UTPeekDetail(out ext))
            {
                if (extold.dwNeedLines != null && extold.dwNeedLines != 0)
                {
                    ext.dwNeedLines = extold.dwNeedLines;
                }
               
                if (ext.dwTotolLines != null)
                {
                    string szPageScript = UniLibrary.ObjHelper.OBJ2JS(ext, "_", ext.GetType());
                    if (!string.IsNullOrEmpty(szPageScript))
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl ScriptInclude = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
                        ScriptInclude.Attributes.Add("type", "text/javascript");
                        ScriptInclude.InnerHtml = "function SetPageValue(){ PutHttpValue(" + szPageScript + ");}$(SetPageValue);";
                        if (Header != null && Header.Controls != null)
                        {
                            Header.Controls.Add(ScriptInclude);
                        }
                        else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
                        {
                            Form.Controls.Add(ScriptInclude);
                        }
                        else
                        {
                            this.Controls.Add(ScriptInclude);
                        }
                    }
                }
            }
        }
        public bool GetMaxValue(ref uint? uMaxValue,uint uID,string szFieldName)
        {
            MAXVALUEREQ Req=new MAXVALUEREQ();
            Req.dwUID=uID;
            Req.szName=szFieldName;
            MAXVALUE res=new MAXVALUE();
            if (m_Request.Admin.GetMaxValue(Req, out res) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                uMaxValue = res.dwValue;
                if (uMaxValue == null)
                {
                    uMaxValue=0;
                }
                uMaxValue++;
                return true;
            }
            return false;
            
        }
        public static string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/>", "\n");
            return theString;
        }
		public void PutBackValue()
		{
			if (Header == null || Header.Controls == null)
			{
				string szPutJsObj = "";
				if (Request.QueryString.AllKeys.Length > 0 || Request.Form.AllKeys.Length > 0)
				{
					string szResult = "[";
					foreach (string szKey in Request.QueryString.AllKeys)
					{
						if (szKey.Substring(0, 2) == "__")
						//if (szKey == "__VIEWSTATE" || szKey == "__EVENTVALIDATION")
						{
							continue;
						}
						if (szKey == "_dwTotolLines" || szKey == "_dwStartLine" || szKey == "_dwNeedLines")
						{
							continue;
						}

						string szVal = Request.QueryString[szKey];
                        szVal = HtmlDiscode(szVal); 
						szResult += "['" + szKey + "','" + szVal + "'],";
					}
					foreach (string szKey in Request.Form.AllKeys)
					{
						if (string.IsNullOrEmpty(szKey) || szKey.Substring(0,2) == "__")
						//if (szKey == "__VIEWSTATE" || szKey == "__EVENTVALIDATION")
						{
							continue;
						}
						if (szKey == "_dwTotolLines" || szKey == "_dwStartLine" || szKey == "_dwNeedLines")
						{
							continue;
						}
						string szVal = Request.Form[szKey];
						szVal = szVal.Replace("\\", "\\\\").Replace("'", "\\'");
						szResult += "['" + szKey + "','" + szVal + "'],";
					}
					if (szResult.EndsWith(","))
					{
						szResult = szResult.Substring(0, szResult.Length - 1);
					}
					szResult += "]";

					szPutJsObj = szResult;
				}
				if (!string.IsNullOrEmpty(szPutJsObj))
				{
                    m_szPutJsObj = szPutJsObj;
                    /*
                    HtmlGenericControl ScriptInclude = new HtmlGenericControl("script");
                    ScriptInclude.Attributes.Add("type", "text/javascript");
                    ScriptInclude.InnerHtml = "function SetDefaultValue(){ PutHttpValue(" + szPutJsObj + ");}$(SetDefaultValue);";
                    if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
                    {
                        Form.Controls.Add(ScriptInclude);
                    }
                    else
                    {
                        this.Controls.Add(ScriptInclude);
                    }*/
				}
			}
		}

		public uint Parse(string s)
		{
			uint ret = 0;
			uint.TryParse(s, out ret);
			return ret;
		}

		public uint EnumToUint(string s)
		{
			uint ret = 0;
			if (string.IsNullOrEmpty(s))
			{
				return ret;
			}
			if (s.IndexOf(",") >= 0)
			{
				string[] ar = s.Split(new char[] { ',' });
				foreach (string ss in ar)
				{
					ret |= Parse(ss);
				}
			}
			else
			{
				ret = Parse(s);
			}
			return ret;
		}

		public bool IsNullOrZero(uint? v)
		{
			if (v == null || v == 0)
			{
				return true;
			}
			return false;
		}
        protected bool GetMinStr(string szStr, out string szRes)//截取一部分字符串
        {
            int uLen = 10;
            szRes = "";
            if (szStr.Length > uLen)
            {
                szRes = szStr.Substring(0, uLen) + "...";
                return true;
            }
            szRes = szStr;
            return false;
        }

        private double KBCount = 1024;
        private double MBCount = Math.Pow(1024,2);
        private double GBCount = Math.Pow(1024,3);
        private double TBCount= Math.Pow(1024,4);

        /// <summary>
        /// 得到适应的大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string</returns>
        public string GetFileSizeString(double size)
        {
            int roundCount=1;
            if (KBCount > size)
            {
                return Math.Round(size, roundCount) + "B";
            }
            else if (MBCount > size)
            {
                return Math.Round(size / KBCount, roundCount) + "KB";
            }
            else if (GBCount > size)
            {
                return Math.Round(size / MBCount, roundCount) + "MB";
            }
            else if (TBCount > size)
            {
                return Math.Round(size / GBCount, roundCount) + "GB";
            }
            else
            {
                return Math.Round(size / TBCount, roundCount) + "TB";
            }
        }
    }
    
}

