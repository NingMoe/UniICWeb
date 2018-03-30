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
using UniStruct;

public partial class WebUserControl : UniClientModule
{
    protected UNIACCOUNT curAcc;
    //protected string szTrueName = "";
    //protected string logonName = "";
    //protected string accNo = "";
    //protected string phone = "";
    //protected string email = "";
    //protected string ident = "";
    //protected string role = "";
    //protected string dept = "";
    //protected string tutorName = "";
    //protected string tutorAccNo = "";
    //protected string tutorSta = "5";
    //protected string rtestSta = "0";
    //protected string property = "0";
    //protected string creditScore = "";
    //protected string creditTotal = "";
    //protected string creditForbid = "";
    protected proacc proacc;
    protected string credit="[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] != null && IsClientLogin())
        {
            curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            proacc=common.ToProAcc(curAcc);
            credit = "[";
            if (proacc.credit != null)
            {
                for (int i = 0; i < proacc.credit.Length; i++)
                {
                    string[] arr = proacc.credit[i];
                    credit +=(i>0?",":"")+ "[";
                    if (arr.Length > 3)
                    {
                        uint uLeftScore = 0;
                        uint uMaxScore = 0;
                        uint.TryParse(arr[1],out uLeftScore);
                        uint.TryParse(arr[2],out uMaxScore);
                        if (uLeftScore > uMaxScore)
                        {
                            arr[2] = arr[1];
                        }
                    }
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (j > 0)
                        {
                            credit +=",";
                        }
                        else {

                        }

                        credit += "\"" + arr[j]+"\"";
                    }
                    credit += "]";
                }
            }
            credit += "]";
            //return;
            //ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            //role = res.dwManRole.ToString();

            //curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            //szTrueName = curAcc.szTrueName;
            //logonName = curAcc.szLogonName;
            //accNo = curAcc.dwAccNo.ToString();
            //phone = curAcc.szHandPhone;
            //email = curAcc.szEmail;
            //ident = curAcc.dwIdent.ToString();
            //dept = curAcc.szDeptName;
            
            //tutorAccNo = curAcc.dwTutorID.ToString();
            //tutorName = curAcc.szTutorName;
            ////科研
            //if (GetConfig("proTarget") == "" || (Convert.ToUInt32(GetConfig("proTarget")) & 4) > 0)//未定义或定义需要科研信息
            //{
            //    tutorSta = GetTutorCheckStatus();
            //    rtestSta = GetRtestStatus();
            //    property = GetProperty();
            //}
            ////信用积分
            //creditScore = GetCredit();
        }
    }

    //private string GetCredit()
    //{
    //    MYCREDITSCOREREQ req = new MYCREDITSCOREREQ();
    //    req.dwAccNo = curAcc.dwAccNo;
    //    MYCREDITSCORE[] rlt;
    //    REQUESTCODE cd= m_Request.System.MyCreditScoreGet(req, out rlt);
    //    if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
    //    {
    //        string str=rlt[0].dwLeftCScore.ToString();
    //        creditTotal = rlt[0].dwMaxScore.ToString();
    //        if (rlt[0].dwLeftCScore == 0 && rlt[0].dwForbidUseTime!=null)
    //        {
    //            str = "-"+rlt[0].dwForbidUseTime;
    //            creditForbid = Util.Converter.UintToDateStr(rlt[0].dwForbidStartDate) + Translate("至") + Util.Converter.UintToDateStr(rlt[0].dwForbidEndDate);
    //        }
    //        return str;
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}

    //private string GetRtestStatus()
    //{
    //    REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
    //    RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
    //    vrGet.dwMemberID = curAcc.dwAccNo;
    //    RESEARCHTEST[] vrResult;
    //    uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
    //    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
    //    {
    //        //临时方法，查询成员状态
    //        for (int i = 0; i < vrResult.Length; i++)
    //        {
    //            RTMEMBER[] mbs = vrResult[i].RTMembers;
    //            for (int j = 0; j < mbs.Length; j++)
    //            {
    //                if (mbs[j].dwAccNo == curAcc.dwAccNo && ((mbs[j].dwStatus & 2) > 0))
    //                {
    //                    return "1";//有课题
    //                }
    //            }
    //        }
    //    }
    //    return "0";
    //}

    //private string GetProperty()
    //{
    //    string pro = "0";
    //    if ((curAcc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER)>0) return pro;
    //    RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
    //    RESEARCHTEST[] vrResult;
    //    vrGet.dwLeaderID = curAcc.dwAccNo;
    //    m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
    //    if (vrResult != null && vrResult.Length > 0)
    //    {
    //        pro = "1";//有负责项目
    //    }
    //    return pro;
    //}

}
