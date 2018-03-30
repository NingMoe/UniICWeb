using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_teachtest : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "get_testitem")
            {
                GetTestItem();
            }
            else if (act == "report_correct")
            {
                if (IsLoginReady())
                {
                    Correct();
                }
            }
            else if (act == "report_upload")
            {
                if (IsLoginReady())
                {
                    string file = Request["file"];
                    string sid = Request["sid"];
                    if (!string.IsNullOrEmpty(file) && !string.IsNullOrEmpty(sid))
                    {
                        REPORTUPLOAD para = new REPORTUPLOAD();
                        para.dwAccNo = curAcc.dwAccNo;
                        para.dwSID = ToUInt(sid);
                        para.szReportURL = file;
                        if (m_Request.Reserve.UploadReport(para) == REQUESTCODE.EXECUTE_SUCCESS)
                            SucMsg("上传成功");
                        else
                            ErrMsg(m_Request.szErrMsg);
                    }
                    else
                        ErrMsgP();
                }
            }
            else if(act=="set_plan_status"){
                setPlanStatus();
            }
            else if (act == "del_plan")
            {
                string id = Request["plan_id"];
                if (string.IsNullOrEmpty(id))
                {
                    ErrMsg("参数有误");
                    return;
                }
                UNITESTPLAN para = new UNITESTPLAN();
                para.dwTestPlanID = ToUInt(id);
                if (m_Request.Reserve.DelTestPlan(para) == REQUESTCODE.EXECUTE_SUCCESS)
                    SucMsg();
                else
                    ErrMsg(m_Request.szErrMsg);
            }
            else if (act == "del_testitem")
            {
                string id = Request["test_id"];
                string card_id = Request["card_id"];
                UNITESTITEM para = new UNITESTITEM();
                para.dwTestItemID = ToUInt(id);
                if (m_Request.Reserve.DelTestItem(para) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    SucMsg();
                    //附带删除项目卡，否则不能创建同名项目
                    if (!string.IsNullOrEmpty(card_id))
                    {
                        TESTCARD card = new TESTCARD();
                        card.dwTestCardID = ToUInt(card_id);
                        m_Request.Reserve.DelTestCard(card);
                    }
                }
                else
                    ErrMsg(m_Request.szErrMsg);
            }
        }
    }

    private void setPlanStatus()
    {
        string id = Request["plan_id"];
        string status = Request["status"];
        string deadline=Request["deadline"];
        string max=Request["max_user"];
        TESTPLANREQ req = new TESTPLANREQ();
                req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
        req.szGetKey = id;
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS&&rlt.Length>0)
        {
            UNITESTPLAN set = rlt[0];
            set.dwStatus = ToUInt(status);
            if (!string.IsNullOrEmpty(deadline)&&deadline!="0")
            {
                set.dwEnrollDeadline = ToUInt(deadline.Replace("-",""));
            }
            if (!string.IsNullOrEmpty(max)&&max!="0")
            {
                set.dwMaxUsers = ToUInt(max);
            }
            if (m_Request.Reserve.SetTestPlan(set, out set) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                SucMsg();
                return;
            }
        }
            ErrMsg("设置开放状态失败"+m_Request.szErrMsg);
    }
    private UNIGROUP GetGroup(uint? id)
    {
        GROUPREQ req = new GROUPREQ();
        req.dwGroupID = id;
        UNIGROUP[] rlt;
        if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS&&rlt.Length==0)
        {
            return rlt[0];
        }
        return new UNIGROUP();
    }

    private void Correct()
    {
        string accno=Request["accno"];
        string sid = Request["sid"];
        string score = Request["score"];
        string eval=Request["eval"];
        if (string.IsNullOrEmpty(accno) || string.IsNullOrEmpty(sid) || sid == "0")
        {
            ErrMsg("参数有误");
            return;
        }
        REPORTCORRECT para = new REPORTCORRECT();
        para.dwTeacherID = curAcc.dwAccNo;
        para.dwSID = ToUInt(sid);
        para.dwAccNo = ToUInt(accno);
        para.dwReportScore = ToUInt(score);
        para.szReportMarkInfo = eval;
        if (m_Request.Reserve.CorrectReport(para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }

    private void GetTestItem()
    {
        string planId = Request["plan_id"];
        string courseId = Request["course_id"];
        TESTITEMREQ req = new TESTITEMREQ();
        if (!string.IsNullOrEmpty(planId))
        {
            req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
            req.szGetKey = planId;
        }
        else
        {
            req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYALL;
        }
        if (!string.IsNullOrEmpty(courseId)&&courseId!="0")
            req.dwCourseID = ToUInt(courseId);
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            List<testitem> list = new List<testitem>();
            for (int i = 0; i < rlt.Length; i++)
            {
                testitem item = new testitem();
                item.id = rlt[i].dwTestItemID.ToString();
                item.name = rlt[i].szTestName;
                item.testhour = rlt[i].dwTestHour;
                list.Add(item);
            }
            SucRlt(list);
        }
        else
            ErrMsg(m_Request.szErrMsg);
    }
    struct testitem
    {
        public string id;
        public string name;
        public uint? testhour;
    }
}