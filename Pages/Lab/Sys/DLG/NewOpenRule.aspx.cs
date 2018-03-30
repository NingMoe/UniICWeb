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
    private uint uPurpose = 15;
	protected void Page_Load(object sender, EventArgs e)
    {
        bool bIsNew = false;
        string szID=Request["dwID"] ;
        if (szID == null)
        {
            bIsNew = true;
            IsNewCtl.Value = "true";
        }
        else
        {
            bIsNew = false;
            IsNewCtl.Value = "false";
        }
        m_Title = bIsNew ? "新建开放规则" : "修改开放规则";
        if (!IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            ListItem item0 = new ListItem();
            item0.Value = "0";
            item0.Text = "所有人员";
            ddlGroup.Items.Add(item0);
            GROUPREQ vrGroupGet = new GROUPREQ();
           // vrGroupGet.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYKIND;
            vrGroupGet.dwKind = ((uint)UNIGROUP.DWKIND.GROUPKIND_OPENRULE);//.ToString();
            UNIGROUP[] vtGroupRes;
            uResponse = m_Request.Group.GetGroup(vrGroupGet, out vtGroupRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtGroupRes != null && vtGroupRes.Length > 0)
            {
                for (int i = 0; i < vtGroupRes.Length; i++)
                {
                    ListItem itemTemp = new ListItem();
                    itemTemp.Value = vtGroupRes[i].dwGroupID.ToString();
                    itemTemp.Text = vtGroupRes[i].szName.ToString();
                    ddlGroup.Items.Add(itemTemp);
                }
            }

            //开放人员增加保洁人员15-08-24
            vrGroupGet.dwKind = ((uint)UNIGROUP.DWKIND.GROUPKIND_USER);//.ToString();
            uResponse = m_Request.Group.GetGroup(vrGroupGet, out vtGroupRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtGroupRes != null && vtGroupRes.Length > 0)
            {
                for (int i = 0; i < vtGroupRes.Length; i++)
                {
                    ListItem itemTemp = new ListItem();
                    itemTemp.Value = vtGroupRes[i].dwGroupID.ToString();
                    itemTemp.Text = vtGroupRes[i].szName.ToString();
                    ddlGroup.Items.Add(itemTemp);
                }
            }


            ArrayList listProperty = GetListFromXml("Priority", 0, true);
            if (listProperty != null && listProperty.Count > 0)
            {
                int nCount=listProperty.Count;
                for (int i = 0; i < nCount; i++)
                {
                    CStatue temp=(CStatue)listProperty[i];
                    dwPriority.Items.Add(new ListItem(temp.szName, temp.szValue));
                }
            }
            BindDDL();
            if (!bIsNew)
            {
                DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
                vrGet.dwRuleSN = ToUint(szID);
                DEVOPENRULE[] vtRes;
                uResponse = m_Request.Device.DevOpenRuleGet(vrGet, out vtRes);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length> 0)
                {
                    szRuleName.Value = vtRes[0].szRuleName.ToString();
                    szMemo.Value = vtRes[0].szMemo.ToString();
                    dwRuleSN.Value = vtRes[0].dwRuleSN.ToString();
                    Session["GroupOpenRuleList"] = vtRes[0].GroupOpenRule;
                    if (vtRes[0].GroupOpenRule.Length>0&&vtRes[0].GroupOpenRule[0].szGroup.dwGroupID == null)
                    {
                        vtRes[0].GroupOpenRule[0].szGroup.dwGroupID = 0;
                        ddlGroup.SelectedValue = vtRes[0].GroupOpenRule[0].szGroup.dwGroupID.ToString();
                        if (vtRes[0].GroupOpenRule[0].dwOpenLimit != null)
                        {
                            if (((uint)vtRes[0].GroupOpenRule[0].dwOpenLimit & (uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME)>0)
                            {
                                chbLimit.Checked = true;
                            }
                            else
                            {
                                chbLimit.Checked = false;
                            }
                        }
                        if (vtRes[0].GroupOpenRule[0].dwPriority != null)
                        {
                            dwPriority.SelectedValue = vtRes[0].GroupOpenRule[0].dwPriority.ToString();
                        }
                        PutGroupOpenRuleToHtml(vtRes[0].GroupOpenRule[0]);
                    }
                }
            }

        }
       
        string szGroupID = ddlGroup.SelectedValue;
        string szGroupIDVS = "";
        if (ViewState["GroupID"] != null)
        {
            szGroupIDVS = ViewState["GroupID"].ToString();
            //保存上一个
            SaveTempGroupOpenRule(Parse(szGroupIDVS));
            bool bIsExist = false;   
            //显示选中一个
            GROUPOPENRULE[] GroupOpenRuleList = (GROUPOPENRULE[])Session["GroupOpenRuleList"];
            for (int i = 0;GroupOpenRuleList!=null&& i < GroupOpenRuleList.Length; i++)
            {
                if (GroupOpenRuleList[i].szGroup.dwGroupID.ToString() == szGroupID)
                {
                    PutGroupOpenRuleToHtml(GroupOpenRuleList[i]);
                    bIsExist = true;
                    break;
                }
            }
            if (!bIsExist)
            {
                SetHtmlToVoid();  
            }
            ViewState["GroupID"] = szGroupID;
            //保存新的viewstate的值
        }
        else
        {
            SaveTempGroupOpenRule();
            ViewState["GroupID"] = szGroupID;
        }
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void SaveTempGroupOpenRule()
    {
        SaveTempGroupOpenRuleBasic(null);
    }
    private void SaveTempGroupOpenRule(uint uGroupID)
    {
        SaveTempGroupOpenRuleBasic(uGroupID);
    }
    private void SaveTempGroupOpenRuleBasic(uint? uGroupID)
    {
        GROUPOPENRULE groupOpenRule = GetGroupOpenRuleFromHtml(uGroupID);
        if (uGroupID != null)
        {
            groupOpenRule.szGroup.dwGroupID = uGroupID;
        }
        if (Session["GroupOpenRuleList"] == null)
        {
            GROUPOPENRULE[] GroupOpenRuleList = new GROUPOPENRULE[1];
            GroupOpenRuleList[0] = groupOpenRule;
            Session["GroupOpenRuleList"] = GroupOpenRuleList;
        }
        else
        {
            bool bExist = false;
            GROUPOPENRULE[] GroupOpenRuleList = (GROUPOPENRULE[])Session["GroupOpenRuleList"];
            for (int i = 0; i < GroupOpenRuleList.Length; i++)
            {
                if (GroupOpenRuleList[i].szGroup.dwGroupID == groupOpenRule.szGroup.dwGroupID)
                {
                    GroupOpenRuleList[i] = groupOpenRule;
                    bExist = true;
                    Session["GroupOpenRuleList"] = GroupOpenRuleList;
                    break;
                }
            }
            if (!bExist)
            {
                int len=GroupOpenRuleList.Length;
                GROUPOPENRULE[] GroupOpenRuleListTemp = new GROUPOPENRULE[len+1];
                for (int m = 0; m < len; m++)
                {
                    GroupOpenRuleListTemp[m] = GroupOpenRuleList[m];
                }               
                GroupOpenRuleListTemp[len] = groupOpenRule;
                Session["GroupOpenRuleList"] = GroupOpenRuleListTemp;
            }
           
        }       
    }
    private void PutGroupOpenRuleToHtml(GROUPOPENRULE groupOpenRule)
    {
        ContentPlaceHolder content;
        Control ctlContent = Master.FindControl("Content");
        if (ctlContent == null)
        {
            return;
        }
       
        dwPriority.SelectedValue =groupOpenRule.dwPriority.ToString();
        if ((((uint)groupOpenRule.dwOpenLimit) & ((uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME))>0)
        {
            chbLimit.Checked = true;
        }
        else
        {
            chbLimit.Checked = false;
        }
        content = (ContentPlaceHolder)ctlContent;
        if((((uint)groupOpenRule.dwOpenLimit) & ((uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME))>0) 
        {
            chbLimit.Checked = true;
        }
        int nPeriodLen = groupOpenRule.PeriodOpenRule.Length;
        ArrayList listPeriod = new ArrayList();
        for (int i = 0; i < nPeriodLen; i++)
        {
            PERIODOPENRULE peroid = new PERIODOPENRULE();
            peroid=groupOpenRule.PeriodOpenRule[i];
            uint? uBeginDay = peroid.dwStartDay;
            listPeriod.Add(uBeginDay);//一周中那几天是开放的checkbox选中
            int nDayOpenRuleLen = 0;
            if (peroid.DayOpenRule != null)
            {
                nDayOpenRuleLen = peroid.DayOpenRule.Length;
            }
            if (uBeginDay == 8)
            {
                uBeginDay = 0;
            }
            else
            {
                uBeginDay++;
            }  
            for (int j = 0; j < nDayOpenRuleLen; j++)
            {
                if (peroid.DayOpenRule== null)
                {
                    continue;
                }
                           
                uint? uBegin = peroid.DayOpenRule[j].dwBegin;
                uint? uEnd = peroid.DayOpenRule[j].dwEnd;
                DropDownList ddlStartHour = (DropDownList)ctlContent.FindControl("ddlWeek" + uBeginDay.ToString() + "Time" + (j + 1).ToString() + "StartHour");
                DropDownList ddlStartMin = (DropDownList)ctlContent.FindControl("ddlWeek" + uBeginDay.ToString() + "Time" + (j + 1).ToString() + "StartMin");
                DropDownList ddlEndHour = (DropDownList)ctlContent.FindControl("ddlWeek" + uBeginDay.ToString() + "Time" + (j + 1).ToString() + "EndHour");
                DropDownList ddlEndMin = (DropDownList)ctlContent.FindControl("ddlWeek" + uBeginDay.ToString() + "Time" + (j + 1).ToString() + "EndMin");

                if (uBegin==null||uEnd==null||((uBegin / 100) == 0 && (uEnd / 100) == 0))
                {
                    ddlStartHour.SelectedValue = "0";
                    ddlStartMin.SelectedValue = "0";
                    ddlEndHour.SelectedValue = "0";
                    ddlEndMin.SelectedValue = "0";
                }
                else
                {
                    ddlStartHour.SelectedValue = (uBegin / 100).ToString();
                    ddlStartMin.SelectedValue = (uBegin % 100).ToString();
                    ddlEndHour.SelectedValue = (uEnd / 100).ToString();
                    ddlEndMin.SelectedValue = (uEnd % 100).ToString();

                    ddlStartHour.Enabled = true;
                    ddlStartMin.Enabled = true;
                    ddlEndHour.Enabled = true;
                    ddlEndMin.Enabled = true;

                    HtmlInputCheckBox chkTime = (HtmlInputCheckBox)ctlContent.FindControl("chbTime" + (j + 1).ToString());
                    if (chkTime != null)
                    {
                        chkTime.Checked = true;
                    }                    
                }
            }
            if (nDayOpenRuleLen == 0)
            {
                nDayOpenRuleLen = 1;
            }
            for (int j = (nDayOpenRuleLen+1); j <= 3; j++)//未开放的改成未勾选，开放时间段
            {
                HtmlInputCheckBox chkTime = (HtmlInputCheckBox)ctlContent.FindControl("chbTime" + j.ToString());
                chkTime.Checked = false;
            }
        }
        for (int i = 0; i <=7; i++)
        {
            int nTemp=(i);
            if (i == 0)
            {
                nTemp = 8;
            }
            else
            {
                nTemp = i - 1;
            }
            HtmlInputCheckBox chbWeek= (HtmlInputCheckBox)ctlContent.FindControl("chbWeek" + (i).ToString());//1,2,3,4,5,6,7,0
            if (listPeriod.IndexOf(((uint?)nTemp)) > -1)//0.1.2.3.4.5.6.8
            {
                chbWeek.Checked = true;
            }
            else
            {
                for (int j = 1; j <= 3; j++)
                {
                    DropDownList ddlStartHour = (DropDownList)ctlContent.FindControl("ddlWeek" + i.ToString() + "Time" + (j).ToString() + "StartHour");
                    DropDownList ddlStartMin = (DropDownList)ctlContent.FindControl("ddlWeek" + i.ToString() + "Time" + (j).ToString() + "StartMin");
                    DropDownList ddlEndHour = (DropDownList)ctlContent.FindControl("ddlWeek" + i.ToString() + "Time" + (j).ToString() + "EndHour");
                    DropDownList ddlEndMin = (DropDownList)ctlContent.FindControl("ddlWeek" + i.ToString() + "Time" + (j).ToString() + "EndMin");

                    ddlStartHour.SelectedValue = ddlStartMin.SelectedValue = ddlEndHour.SelectedValue = ddlEndMin.SelectedValue = "0";
                    ddlStartHour.Enabled = ddlStartMin.Enabled = ddlEndHour.Enabled = ddlEndMin.Enabled = false;
                }
                chbWeek.Checked = false;
            }
        }
        
    }
    private void SetHtmlToVoid()
    {
        ContentPlaceHolder content;
        Control ctlContent = Master.FindControl("Content");
        if (ctlContent == null)
        {
            return;
        }
        chbLimit.Checked = false;
        dwPriority.SelectedValue = "1";
        content = (ContentPlaceHolder)ctlContent;
        for (int i = 0; i <= 7; i++)
        {
            string szCheckWeek = "chbWeek" + i.ToString();
            for (int j = 1; j <= 3; j++)
            {
                string szStartHour = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "StartHour";
                string szEndHour = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "EndHour";
                string szStartMin = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "StartMin";
                string szEndMin = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "EndMin";

                DropDownList ddlStartHour = (DropDownList)ctlContent.FindControl(szStartHour);
                DropDownList ddlStartMin = (DropDownList)ctlContent.FindControl(szEndHour);
                DropDownList ddlEndHour = (DropDownList)ctlContent.FindControl(szStartMin);
                DropDownList ddlEndMin = (DropDownList)ctlContent.FindControl(szEndMin);

                ddlStartHour.SelectedValue = "0";                
                ddlStartMin.SelectedValue = "0";
                ddlEndHour.SelectedValue = "0";
                ddlEndMin.SelectedValue = "0";

             
                ddlStartHour.Enabled = false;
                ddlStartMin.Enabled = false;
                ddlEndHour.Enabled = false;
                ddlEndMin.Enabled = false;


                HtmlInputCheckBox chbWeek = (HtmlInputCheckBox)ctlContent.FindControl(szCheckWeek);
                if (chbWeek != null)
                {
                    chbWeek.Checked = false;
                }
                HtmlInputCheckBox chbTime = (HtmlInputCheckBox)ctlContent.FindControl("chbTime"+j.ToString());
                if (chbTime != null)
                {
                    chbTime.Checked = false;
                }
            }
        }
    }
    private GROUPOPENRULE GetGroupOpenRuleFromHtml(uint? uGroupID)
    {
        GROUPOPENRULE res = new GROUPOPENRULE();
        if (uGroupID == null)
        {
            res.szGroup.dwGroupID = Parse(ddlGroup.SelectedValue);
        }
        else
        {
            res.szGroup.dwGroupID = uGroupID;
        }
        res.dwPriority = Parse(Request["dwPriority"]);
        if (chbLimit.Checked)
        {
            res.dwOpenLimit = 2;
        }
        else
        {
            res.dwOpenLimit =0;
        }
        res.dwPriority = Parse(dwPriority.SelectedValue);
        ContentPlaceHolder content;
        Control ctlContent = Master.FindControl("Content");
        if (ctlContent == null)
        {
            return res;
        }
        content = (ContentPlaceHolder)ctlContent;
        ArrayList listProid = new ArrayList();//开放的天数，加上节假日最都位8
        ArrayList listDay = new ArrayList();//一天中开放的段数最多为3
     
        for (uint i = 0; i <= 7; i++)
        {
            bool bIsAdd = false;
            string szWeeekCheck="chbWeek" + i.ToString();
            PERIODOPENRULE itemPeriod = new PERIODOPENRULE();
            Control ctrlWeekCheck=content.FindControl(szWeeekCheck);//周一到周日开放的天数
            if (ctrlWeekCheck != null)
            {
                HtmlInputCheckBox checkWeekCtrl = (HtmlInputCheckBox)ctrlWeekCheck;
                if (checkWeekCtrl.Checked)
                {                                                         
                    for (uint j = 1; j <= 3; j++)
                    {
                       string szCheTimeCheck= "chbTime" + j.ToString();
                       Control ctrlTimeCheck = content.FindControl(szWeeekCheck);//一天中开放的时间段
                       if (ctrlTimeCheck != null)
                       {
                           HtmlInputCheckBox checkTimeCtrl = (HtmlInputCheckBox)ctrlTimeCheck;//
                           if (checkTimeCtrl.Checked)
                           {
                               ///if里面都是 添加开放时间段
                               DAYOPENRULE itemDayOpenRule = new DAYOPENRULE();

                               string szStartHour = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "StartHour";
                               string szEndHour = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "EndHour";
                               string szStartMin = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "StartMin";
                               string szEndMin = "ddlWeek" + i.ToString() + "Time" + j.ToString() + "EndMin";

                               Control ctrlStartHour = content.FindControl(szStartHour);
                               Control ctrlEndHour = content.FindControl(szEndHour);
                               Control ctrlStartMin = content.FindControl(szStartMin);
                               Control ctrlEndMin = content.FindControl(szEndMin);
                               if (ctrlStartHour == null || ctrlEndHour == null || ctrlStartMin == null || ctrlEndMin == null)
                               {
                                   continue;
                               }
                               DropDownList ddlStartHour = (DropDownList)ctrlStartHour;
                               DropDownList ddlEndHour = (DropDownList)ctrlEndHour;
                               DropDownList ddlStartMin = (DropDownList)ctrlStartMin;
                               DropDownList ddlEndMin = (DropDownList)ctrlEndMin;

                               uint uStart = Parse(ddlStartHour.SelectedValue) * 100 + Parse(ddlStartMin.SelectedValue);
                               uint uEnd = Parse(ddlEndHour.SelectedValue) * 100 + Parse(ddlEndMin.SelectedValue);
                               if (uStart == 0 && uEnd == 0)
                               {
                                   continue;
                               }
                               itemDayOpenRule.dwBegin = uStart;
                               itemDayOpenRule.dwEnd = uEnd;
                               itemDayOpenRule.dwOpenPurpose = uPurpose;

                               if (!bIsAdd)
                               {
                                   //if里面是添加天数
                                   if (i > 0)//周一到周日
                                   {
                                       itemPeriod.dwStartDay = (i - 1);
                                       itemPeriod.dwEndDay = (i - 1);
                                   }
                                   else//节假日
                                   {
                                       itemPeriod.dwStartDay = (8);
                                       itemPeriod.dwEndDay = (8);
                                   }
                                   bIsAdd = true;
                               }
                               listDay.Add(itemDayOpenRule);//添加段
                              //
                           }
                       }
                    }
                    //跳出添加时间段的循环
                    itemPeriod.DayOpenRule = new DAYOPENRULE[listDay.Count];
                    for (int m = 0; m < listDay.Count; m++)
                    {
                        DAYOPENRULE dayTemp=(DAYOPENRULE)listDay[m];
                        if (!(dayTemp.dwBegin == 0 && dayTemp.dwEnd == 0))
                        {
                            itemPeriod.DayOpenRule[m] = new DAYOPENRULE();
                            itemPeriod.DayOpenRule[m] = (dayTemp);
                        }
                    }
                    listDay.Clear();
                }
            }
            if (itemPeriod.dwEndDay != null)
            {
                listProid.Add(itemPeriod);//添加天
            }
        }
        res.PeriodOpenRule = new PERIODOPENRULE[listProid.Count];
        for (int i = 0; i < listProid.Count; i++)
        {
            res.PeriodOpenRule[i] = new PERIODOPENRULE();
            res.PeriodOpenRule[i] = (PERIODOPENRULE)listProid[i];
        }
        return res;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DEVOPENRULE openRule = new DEVOPENRULE();
        GetHTTPObj(out openRule);
        GROUPOPENRULE[] GroupOpenRule = (GROUPOPENRULE[])Session["GroupOpenRuleList"];
        if (GroupOpenRule == null)
        {
            GroupOpenRule=new GROUPOPENRULE[1];
            GroupOpenRule[0] = new GROUPOPENRULE();
            GroupOpenRule[0] = GetGroupOpenRuleFromHtml(null);
        }
        openRule.GroupOpenRule = GroupOpenRule;
        string szOp = IsNewCtl.Value == "true" ? "新建" : "修改";
        if (m_Request.Device.DevOpenRuleSet(openRule, out openRule) != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ViewState["info"] = szOp+"失败" + m_Request.szErrMessage;
            MessageBox(m_Request.szErrMessage, szOp+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
        }
        else
        {
            ViewState["info"] = szOp+"成功";
            MessageBox(szOp+"成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;
        }
    }

    private void BindDDL()
    {
        ContentPlaceHolder content;
        Control ctlContent = Master.FindControl("Content");
        if (ctlContent == null)
        {
            return;
        }
        content = (ContentPlaceHolder)ctlContent;
        for (int i = 0; i <= 7; i++)
        {
            string szID = "ddlWeek" + i.ToString();
            for (int j = 1; j <= 3; j++)
            {
                string szTemp = szID + "Time" + j.ToString();
                Control ctrlStartHour = content.FindControl(szTemp + "StartHour");
                if (ctrlStartHour != null)
                {
                    DropDownList list = (DropDownList)ctrlStartHour;
                    BindHour(list);
                }
                Control ctrlEndHour = content.FindControl(szTemp + "EndHour");
                if (ctrlEndHour != null)
                {
                    DropDownList list1 = (DropDownList)ctrlEndHour;
                    BindHour(list1);
                }
                Control ctrlStartMin = content.FindControl(szTemp + "StartMin");
                if (ctrlStartMin != null)
                {
                    DropDownList list2 = (DropDownList)ctrlStartMin;
                    BindMin(list2);
                }
                Control ctrlEndMin = content.FindControl(szTemp + "EndMin");
                if (ctrlEndMin != null)
                {
                    DropDownList list3 = (DropDownList)ctrlEndMin;
                    BindMin(list3);
                }
            }
        }
    }
    private void BindHour(DropDownList list)
    {
        ListItem item = new ListItem();
        for (int i = 0; i < 24; i++)
        {
            list.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
        }
    }
    private void BindMin(DropDownList list)
    {
        ListItem item = new ListItem();
        for (int i = 0; i < 60; i++)
        {
            list.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);       
        if (ViewState["info"] != null)
        {
            if (ViewState["info"].ToString().IndexOf("成功") > -1)
            {
                MessageBoxSuccess(ViewState["info"].ToString());
            }
            else
            {
                MessageBoxFail(ViewState["info"].ToString());
            }
            ViewState["info"] = null;
        }      
    }
    protected override void OnPreRender(EventArgs e)
    {
        
    }
   
}
