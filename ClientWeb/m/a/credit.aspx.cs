using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_view : UniClientPage
{
    protected string detail = "";
    protected string creditrec = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitCreditRec();
    }
    private void InitCreditRec()
    {
        if (Session["LOGIN_ACCINFO"] == null) return;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //信用记录
        CREDITREC[] list = GetCreditRecByAccNo(acc.dwAccNo);
        if (list != null)
        {
            for (int i = 0; i < list.Length; i++)
            {
                CREDITREC rec = list[i];
                //处罚
                string punish = "<span class='grey uni_trans'>不处罚</span>";
                string location=string.IsNullOrEmpty(rec.szDevName)?Translate("未签到使用"):rec.szLabName + "," + rec.szDevName;
                if ((rec.dwUserCStat & (uint)CREDITREC.DWUSERCSTAT .USERCSTAT_VALID)>0&& rec.dwForbidStartDate != null && rec.dwForbidEndDate != 0)
                    punish = "<span class='red uni_trans'>禁止预约</span>：" + ToDate(rec.dwForbidStartDate) + Translate("至") + ToDate(rec.dwForbidEndDate);
                creditrec += "<li class='item-content'>" +
            "<div class='item-inner'>" +
              "<div class='item-title-row'>" +
                "<div class='item-title'>" + rec.szCTName + "/" + rec.szCreditName + "</div>" +
                "<div class='item-after'>" + Util.Converter.GetCreditRecState(rec.dwUserCStat) + "</div></div>" +
              "<div class='item-subtitle'>"+Translate("扣") + rec.dwThisUseCScore +Translate("分")+ "  (" + Get1970Date((int)rec.dwOccurTime) + ")</div>" +
              "<div class='item-text'>" + location + "<br/>" + punish + "</div></div></li>";
            }
        }
        //个人信用
        proacc pacc= common.ToProAcc(acc);
        if (pacc.credit != null)
        {
            for (int i = 0; i < pacc.credit.Length; i++)
            {
                string[] arr = pacc.credit[i];
                string left=ToUInt(arr[2])>ToUInt(arr[1])?arr[1]:arr[2];
                detail += "<tr><td>" + arr[0] + "</td><td class='text-center'>" + left + "</td><td class='text-center'>" + arr[2] + "</td></tr>";
            }
        }
    }
    private string ToDate(uint? dt)
    {
        if(dt==null)return "";
        uint? y = dt / 10000;
        uint? m = (dt / 100) % 100;
        uint? d = dt % 100;
        return y + "/" + m + "/" + d;
    }
}