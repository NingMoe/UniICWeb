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
    protected string m_szIdent = "";
    protected string m_szDevKind = "";
    protected string m_szResvPurpose= "";

    protected string m_dwPriority = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        EXTIDENTACC newTutor;
        m_Title = "新建"+ConfigConst.GCTutorName;
        if (IsPostBack)
        {
            string szAccno = Request["dwAccNo"];
            UNIACCOUNT accTutor=new UNIACCOUNT();
            if (szAccno == null || (!GetAccByAccno(szAccno, out accTutor)))
            {
                MessageBox("未找到该人员", "新建"+ConfigConst.GCTutorName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            GetHTTPObj(out newTutor);
            newTutor.dwAccNo = accTutor.dwAccNo;
            newTutor.szTrueName = accTutor.szTrueName;
            int uAuto = ConfigConst.GCTurtorReacher;
            if (uAuto == 1)
            {
                RESEARCHTEST setResarch = new RESEARCHTEST();
                setResarch.szRTName = newTutor.szTrueName;
                setResarch.dwRTKind = (uint)RESEARCHTEST.DWRTKIND.RTKIND_RTASK;
                UNIGROUP setGroup=new UNIGROUP();
                setGroup.dwKind=(uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
                setGroup.szName=newTutor.szTrueName+ConfigConst.GCTutorName+"组";
              
                if (m_Request.Group.SetGroup(setGroup, out setGroup) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    setResarch.dwGroupID = setGroup.dwGroupID;
                }
                setResarch.szRTName = newTutor.szTrueName + "ConfigConst.GCReachTestName";
                setResarch.dwLeaderID = newTutor.dwAccNo;
                setResarch.szLeaderName = newTutor.szTrueName;
                m_Request.Reserve.SetResearchTest(setResarch, out setResarch);
            }
            if (m_Request.Account.ExtIdentAccSet(newTutor) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建"+ConfigConst.GCTutorName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建"+ConfigConst.GCTutorName+"成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }       
    }
}
