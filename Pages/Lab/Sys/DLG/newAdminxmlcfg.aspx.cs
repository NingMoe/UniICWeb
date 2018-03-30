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
    protected string m_szHour = "";
    protected string m_szMin = "";
    protected string m_szValue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i <= 59; i++)
        {
            m_szMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), i.ToString());
        }

        for (int i = 6; i <= 23; i++)
        {
            m_szHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), i.ToString());
        }
        if (IsPostBack)
        {
            string szOldValue = Request["oldValue"];
            CStatueTemp temp = new CStatueTemp();
            string szValue = "";
            string szName = "";
            string szfieldName = Request["selectValue"];
            CStatueTemp value = new CStatueTemp();
            if (szfieldName == "ResvTheme")
            {
                szValue = Request["szThemeName"];
                szName = szValue;
            }
            else if (szfieldName == "ResvAbsTime")
            {
                uint uValue = (Parse(Request["startHour"]) * 100 + Parse(Request["startMin"])) * 10000 + (Parse(Request["endHour"]) * 100 + Parse(Request["endMin"]));
                szValue = uValue.ToString();
                szName = Request["startHour"] + "点" + Request["startMin"] + "分到" + Request["endHour"] + "点" + Request["endMin"] + "分";

                value.szName = szName;
                value.szValue = szValue;

            }

            if (!(add(szfieldName, szValue, szName)))
            {
                MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            string szName = Request["fieldName"];
            string szValue = Request["id"];
            CStatueTemp value = GetListByFieldName(szName, szValue);
            m_szValue = szName;
            if (szName == "ResvTheme")
            {
                PutMemberValue2("szThemeName", szValue.ToString());
            }
            else if (szName == "ResvAbsTime")
            {
                uint uValue = Parse(szValue);
                uint uStart = uValue / 10000;
                uint uEnd = uValue % 10000;

                PutMemberValue2("startHour", (uStart / 100).ToString());
                PutMemberValue2("startMin", (uStart % 100).ToString());


                PutMemberValue2("EndHour", (uEnd / 100).ToString());
                PutMemberValue2("EndMin", (uEnd % 100).ToString());

            }
            PutMemberValue2("oldValue", szValue);
            if (value == null)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                //  PutJSObj(value);

            }
        }
        else
        {
            m_Title = "新建";

        }
    }

}
