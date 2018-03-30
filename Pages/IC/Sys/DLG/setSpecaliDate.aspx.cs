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
    protected string m_H = "";
    protected string m_M= "";
    uint uopenpuope = 311;
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < 24; i++)
        {
            m_H += GetInputItemHtml(CONSTHTML.option, "",i.ToString("00"), i.ToString("00"));
        }
        for (int i = 0; i < 59; i++)
        {
            m_M += GetInputItemHtml(CONSTHTML.option,"", i.ToString("00"), i.ToString("00"));
        }
        if (IsPostBack)
        {
            bSet = true;
            ArrayList alist = new ArrayList();
            for (int i = 1; i <= 3; i++)
            {
                PERIODOPENRULE peri = new PERIODOPENRULE();
                string szStartDate = Request["dwStartDay"+i];
                string szEndDate = Request["dwEndDay"+i];
            
                if (szStartDate != null && szStartDate != "" && szEndDate != null && szEndDate != "")
                {
                    peri.dwStartDay = (GetDate(szStartDate));
                    peri.dwEndDay = (GetDate(szEndDate));
                    ArrayList listDateTime = new ArrayList();
                    for (int j = 1; j <= 3; j++)
                    {
                        string szStartH = Request["starth" + i.ToString() + j.ToString()];
                        string szStartM = Request["startm" + i.ToString() + j.ToString()];
                        string szEndH = Request["endh" + i.ToString() + j.ToString()];
                        string szEndM = Request["endm" + i.ToString() + j.ToString()];
                        DAYOPENRULE openDateTime = new DAYOPENRULE();
                        uint uDayBegTime = Parse(szStartH + szStartM);
                        uint uDayEndTime = Parse(szEndH + szEndM);
                        if (uDayBegTime != 0 && uDayEndTime != 0&&uDayBegTime!=uDayEndTime)
                        {
                            openDateTime.dwBegin = Parse(szStartH + szStartM);
                            openDateTime.dwEnd = Parse(szEndH + szEndM);
                            openDateTime.dwOpenLimit = 0;
                            openDateTime.dwOpenPurpose = uopenpuope;
                            listDateTime.Add(openDateTime);
                        }
                    }
                    DAYOPENRULE[] openDayTimeList = new DAYOPENRULE[listDateTime.Count];
                    for (int m = 0; m < listDateTime.Count; m++)
                    {
                        openDayTimeList[m] = new DAYOPENRULE();
                        openDayTimeList[m] = (DAYOPENRULE)listDateTime[m];
                    }
                    peri.DayOpenRule = new DAYOPENRULE[listDateTime.Count];
                    peri.DayOpenRule = openDayTimeList;
                    alist.Add(peri);
                }
            }
            DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
            vrGet.dwRuleSN = Parse(Request["dwID"]);
            DEVOPENRULE[] vtRes;
            if (m_Request.Device.DevOpenRuleGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
               GROUPOPENRULE[] openrule= vtRes[0].GroupOpenRule;
               if (openrule != null && openrule.Length > 0)
               {
                   for (int i = 0; i < openrule.Length; i++)
                   {
                      PERIODOPENRULE[] periodlist=openrule[i].PeriodOpenRule;
                      for (int j = 0; j < periodlist.Length; j++)
                      {
                          if (periodlist[j].dwEndDay < 10)//
                          {
                              alist.Add(periodlist[j]);
                          }
                          else
                          {
                              PERIODOPENRULE peroidTemp = new PERIODOPENRULE();
                              peroidTemp = periodlist[j];
                              alist.Add(isExist(peroidTemp, alist));
                          }
                      }
                      openrule[i].PeriodOpenRule=new PERIODOPENRULE[alist.Count];
                      for (int k = 0; k < alist.Count; k++)
                      {
                          openrule[i].PeriodOpenRule[k] = new PERIODOPENRULE();
                          openrule[i].PeriodOpenRule[k] = (PERIODOPENRULE)alist[k];
                      }
                      if (openrule[i].szGroup.dwGroupID == null)
                      {
                          openrule[i].szGroup = new UNIGROUP();
                          openrule[i].szGroup.dwGroupID = 0;
                      }
                      vtRes[0].GroupOpenRule=openrule;
                   }
                  
               }
                DEVOPENRULE setValue=new DEVOPENRULE();
                setValue=vtRes[0];
                if (m_Request.Device.DevOpenRuleSet(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
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
            else
            {
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
            vrGet.dwRuleSN = Parse(Request["dwID"]);
            DEVOPENRULE[] vtRes;
            if (m_Request.Device.DevOpenRuleGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
                    GROUPOPENRULE[] openrule = vtRes[0].GroupOpenRule;
                    if (openrule != null && openrule.Length > 0)
                    {
                        PERIODOPENRULE[] periooplist = openrule[0].PeriodOpenRule;
                        if (periooplist != null && periooplist.Length > 0)
                        {
                            int count = 1;
                            for (int i = 0; i < periooplist.Length; i++)
                            {
                                if (periooplist[i].dwStartDay > 20100101)
                                {
                                      
                                    uint uDayOpenTimeLen = 0;
                                    if (periooplist[i].DayOpenRule != null)
                                    {
                                        uDayOpenTimeLen = (uint)periooplist[i].DayOpenRule.Length;
                                    }
                                    DAYOPENRULE[] openDateTime = new DAYOPENRULE[uDayOpenTimeLen];
                                    openDateTime=periooplist[i].DayOpenRule;
                                    for (int m = 1; m <= uDayOpenTimeLen; m++)
                                    {
                                        PutMemberValue("dwStartDay" + count, GetDateStr(periooplist[i].dwStartDay));
                                        PutMemberValue("dwEndDay" + count, GetDateStr(periooplist[i].dwEndDay));
                                 
                                        uint uBeginTime=(uint)openDateTime[m-1].dwBegin;
                                        uint uEndTime=(uint)openDateTime[m-1].dwEnd;
                                        PutMemberValue("starth" + count.ToString() + m.ToString(), (uBeginTime/100).ToString("00"));
                                        PutMemberValue("startm" + count.ToString() + m.ToString(), (uBeginTime % 100).ToString("00"));

                                        PutMemberValue("endh" + count.ToString() + m.ToString(), (uEndTime / 100).ToString("00"));
                                        PutMemberValue("endm" + count.ToString() + m.ToString(), (uEndTime % 100).ToString("00"));
                                        count = count + 1;
                                    }
                                   

                                }
                               
                            }
                        }
                    }
                }
            }
        }
        else
        {
            

        }
    }
    public PERIODOPENRULE isExist(PERIODOPENRULE period,ArrayList list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            PERIODOPENRULE op = (PERIODOPENRULE)list[i];
            if (op.dwStartDay != null && op.dwStartDay == period.dwStartDay && op.dwEndDay != null && op.dwEndDay == period.dwEndDay)
            {
                op.dwEndDay = period.dwEndDay;
                op.dwStartDay = period.dwStartDay;
                return op;
            }
        }
        //period.DayOpenRule=null;
        
        return period;
    }
}
