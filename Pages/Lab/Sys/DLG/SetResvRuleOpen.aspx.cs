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
    protected string m_Title = "设置开放周次";
    protected string m_Property = "";
    protected string szConTimeDiv = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            RESVRULEREQ vrGet = new RESVRULEREQ();
            vrGet.dwRuleSN = Parse(Request["dwID"]);
            UNIRESVRULE[] vtRes;
            if (m_Request.Reserve.ResvRuleGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length>0)
            {
                UNIRESVRULE newCourse = new UNIRESVRULE();
                newCourse = vtRes[0];
                newCourse.szOtherCons = Request["szValue"];
                if (m_Request.Reserve.ResvRuleSet(newCourse, out newCourse) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                   
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                  
                }
            }
        }
        m_Property = GetAllInputHtml(CONSTHTML.option, "", "Course_Property");
        if (Request["op"] == "set")
        {
            bSet = true;
          
            RESVRULEREQ vrGet = new RESVRULEREQ();
            vrGet.dwRuleSN = Parse(Request["dwID"]);
            UNIRESVRULE[] vtRes;
            if (m_Request.Reserve.ResvRuleGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
                    string[] bigdate = { "一", "二", "三", "四", "五", "六", "天" };
                    string[] bigSecs = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八" };
                    string[] bigWeeks = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一", "二十二", "二十三", "二十四", "二十五", "二十六", "二十七", "二十八", "二十九", "三十" };
  
                    string szCon = vtRes[0].szOtherCons;
                    if (szCon != "" && szCon != ";")
                    {
                        string[] szConList = szCon.Split(';');
                        if (szCon.IndexOf("；")>-1)
                        {
                            szConList = szCon.Split('；');
                        }
                        string szConDev = "";
                       
                        string szConTimeHidden = "";
                        for (int i = 0; i < szConList.Length; i++)
                        {
                            string szTemp = szConList[i].ToString().Replace(";",""); ;
                            if (szTemp != ";")
                            {
                                if (szTemp.IndexOf("T") > -1)
                                {
                                    string szID = szTemp;
                                    szTemp = szTemp.Replace("-", "");
                                    szConTimeHidden += szID + ";";
                                    string szTemp2 = szTemp;
                                    szTemp = szTemp.Substring(3);
                                    int nTemp = int.Parse(szTemp);
                                    int weekS = (nTemp / 100);
                                    int weekE = (nTemp % 100);

                                    string szInfo = "第" + bigWeeks[weekS] + "周到第" + bigWeeks[weekE] + "周";
                                    szConTimeDiv += "<a style=\"margin-top:10px;margin-left:10px;\" id=\"" + szID + "\" onclick=\"delA('" + szID + "')\" href=\"#\">" + szInfo + "(点击删除)" + "</a><br />";

                                }
                            }
                        }
                        ViewState["szConTimeHidden"] = szConTimeHidden;
                        ViewState["szConTimeDiv"] = szConTimeDiv;
                      //  PutMemberValue("szValue", szConTimeHidden);
                       // PutMemberValue("szLimitTime", szConTimeDiv);
                        /*
                        xmlSetAttribute(outDoc, "//field[@name='szValue']", "default", szConTimeHidden);
                        XmlNodeList node = outDoc.SelectNodes("//DIV[@name='szLimitTime']");
                         * 
                        node[0].InnerXml = szConTimeDiv;
                        */
                    }
                  
                }
            }
        }
        else
        {
            m_Title = "";

        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        if (ViewState["szConTimeHidden"] != null && ViewState["szConTimeHidden"].ToString() != "")
        {
            PutMemberValue("szValue", ViewState["szConTimeHidden"].ToString());
        }
      
    }
}
