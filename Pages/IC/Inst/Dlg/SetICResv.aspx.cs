using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
    protected string m_szDevName = "";
    protected string TimeHour = "";
    protected string TimeMin = "";
    protected string szPreDate ="";
    protected uint uBeginH = 0;
    protected uint uBeginM= 0;
    protected uint uEndH = 0;
    protected uint uEndM = 0;

	protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            bSet = true;
            RESVREQ vrGet = new RESVREQ();
            vrGet.dwResvID = Parse(Request["resvid"]);
            UNIRESERVE[] vtRes;
            if (m_Request.Reserve.Get(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                string szResvDate = Request["szPreDate"];
                string BeginTime = Request["startTimeHour"] + ":" + Request["startTimeMin"];
                string EndTime = Request["endTimeHour"] + ":" + Request["endTimeMin"];

                vtRes[0].dwBeginTime = Get1970Seconds(szResvDate + " " + BeginTime);
                vtRes[0].dwEndTime = Get1970Seconds(szResvDate + " " + EndTime);
                UNIRESERVE setResv = vtRes[0];
                if (m_Request.Reserve.Set(setResv, out setResv) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
                else
                {

                    MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }

            }
        }

        for (int i = 7; i < 23; i++)
        {
            TimeHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }

        for (int i = 0; i < 60; i = i + 5)
        {
            TimeMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        } 
        if (Request["op"] == "set")
        {
            bSet = true;
            RESVREQ vrGet= new RESVREQ();
            vrGet.dwResvID = Parse(Request["resvid"]);
            UNIRESERVE[] vtRes;
            if (m_Request.Reserve.Get(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS&&vtRes!=null&&vtRes.Length>0)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_szDevName = vtRes[0].ResvDev[0].szDevName;
                    szPreDate = GetDateStr(vtRes[0].dwPreDate);
                    string beginHM = Get1970Date(vtRes[0].dwBeginTime,"HHmm");
                    uBeginH = (Parse(beginHM))/100;
                    uBeginM= (Parse(beginHM)) % 100;
                    PutMemberValue("startTimeHour", uBeginH.ToString());
                    PutMemberValue("startTimeMin", uBeginM.ToString());

                    string endHM = Get1970Date(vtRes[0].dwEndTime, "HHmm");
                    uEndH = (Parse(endHM)) / 100;
                    uEndM= (Parse(endHM)) % 100;

                    PutMemberValue("endTimeHour", uEndH.ToString());
                    PutMemberValue("endTimeMin", uEndM.ToString());

                    m_Title = "修改预约";
                }
            }
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCLabName;

        }
    }
}
