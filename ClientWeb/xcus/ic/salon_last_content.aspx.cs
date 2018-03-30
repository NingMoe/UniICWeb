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
using System.Collections.Generic;
using UniWebLib;

public partial class Page_ : UniClientPage
{
    protected string m_szInfo = "";
    public string szImgBig = "";
    public string szImgSmall = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        m_szInfo = "";

        ACTIVITYPLANREQ vrParameter = new ACTIVITYPLANREQ();
        UNIACTIVITYPLAN[] vrResult;
        string id = Request["id"];
        vrParameter.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        vrParameter.szGetKey = id;
        FileCtrl fileCtrl = new FileCtrl();
        string[] szImgList = fileCtrl.readlist(Server.MapPath(@"ActivityPlanImg\" + id));
        if (szImgList == null || szImgList.Length <= 0)
        {
            space_tabs.Style.Add("display", "none");
        }
        else
        {
            string szTemp = "none";
            foreach (string f in szImgList)//xiaobaigang为文件夹名称
            {
                if (szTemp == "none")
                {
                    szImgBig += "<img src='ActivityPlanImg/" + id + "/" + f + "' width='510' height='350'>";
                    szTemp = "";
                }
                szImgSmall += "<li><a href='' class='cur'><img src='ActivityPlanImg/" + id + "/" + f + "' width='84' height='55'></a></li>";

            }
            space_tabs.Style.Add("display", "inline");
        }
        if (m_Request.Reserve.GetActivityPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                string szRepory = "";
                uint uStatue = (uint)vrResult[i].dwStatus;
                if ((uStatue & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) <= 0)
                {
                    continue;
                }
                if ((uStatue & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN) > 0)
                {
                    continue;
                }
                uint dwPublishDate = (uint)vrResult[i].dwPublishDate;
                uint dwEnrollDeadline = (uint)vrResult[i].dwEnrollDeadline;
                uint dwActivityDate = (uint)vrResult[i].dwActivityDate;
                uint dwBegIntime = (uint)vrResult[i].dwBeginTime;
                uint dwEndTime = (uint)vrResult[i].dwEndTime;               
               
                m_szInfo += "</tr><tr class=\"detail\" style=\"display:inline;\"><td colspan=\"2\">";
                m_szInfo += "<p><table style=\"margin:12px\">";
                m_szInfo += "<tr><td colspan=\"2\">" + szRepory + "</td></tr>";
                m_szInfo += "<tr><td style=\"width:100px\">活动主题</td><td>" + vrResult[i].szActivityPlanName.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>主办单位</td><td>" + vrResult[i].szHostUnit.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>承办单位</td><td>" + vrResult[i].szOrganizer.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>主讲人</td><td>" + vrResult[i].szPresenter.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>活动介绍</td><td>" + vrResult[i].szIntroInfo.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>参与者要求</td><td>" + vrResult[i].szDesiredUser.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>活动介绍</td><td>" + vrResult[i].szIntroInfo + "</td></tr>";
                m_szInfo += "<tr><td>联系人</td><td>" + vrResult[i].szContact.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>联系人电话</td><td>" + vrResult[i].szTel.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>限制报名人数</td><td>" + vrResult[i].dwMaxUsers.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>已申请人数</td><td>" + vrResult[i].dwEnrollUsers.ToString() + "</td></tr>";
                if (dwActivityDate != 20990101)
                {
                    m_szInfo += "<tr><td>申请加入截止日期</td><td>" + dwEnrollDeadline / 10000 + "-" + (dwEnrollDeadline / 100 % 100) + "-" + (dwEnrollDeadline % 100) + "</td></tr>";
                    string szResvTime = ((int)vrResult[i].dwBeginTime / 100) + ":" + ((int)vrResult[i].dwBeginTime % 100).ToString("00") + "—" + ((int)vrResult[i].dwEndTime / 100) + ":" + ((int)vrResult[i].dwEndTime % 100).ToString("00");
                    m_szInfo += "<tr><td>活动安排时间</td><td>" + dwActivityDate / 10000 + "-" + (dwActivityDate / 100 % 100) + "-" + (dwActivityDate % 100) + "  " + szResvTime + "</td></tr>";
                }
                else
                {

                    m_szInfo += "<tr><td>申请加入截止日期</td><td>待定</td></tr>";

                    m_szInfo += "<tr><td>活动安排时间</td><td>待定</td></tr>";
                }
                m_szInfo += "<tr><td>主办地点</td><td>" + vrResult[i].szSite.ToString() + "</td></tr>";
                m_szInfo += "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>";

              
                m_szInfo += "</table></p></td></tr>";
            }
        }
        else
        {
            m_szInfo = m_Request.szErrMessage;
        }
    }
}
