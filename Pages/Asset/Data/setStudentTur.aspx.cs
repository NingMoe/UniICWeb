using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sAccno = Request["sLogonName"];
        string tLogonName = Request["tLogonName"];
        string szHandPhone = Request["Handphone"];
        string szEmail = Request["email"];
        Response.CacheControl = "no-cache";
        UNIACCOUNT studentAcc=new UNIACCOUNT();      
        UNIACCOUNT teacherAcc=new UNIACCOUNT();

        if (GetAccByLogonName(sAccno, out studentAcc) && GetAccByLogonName(tLogonName, out teacherAcc))
        {
            int uAuto = ConfigConst.GCTurtorReacher;
            if (uAuto == 1)
            {
                TUTORREQ tutorReq = new TUTORREQ();
                tutorReq.dwTutorID = teacherAcc.dwAccNo;
                UNITUTOR[] vtTutor;
                if (m_Request.Account.TutorGet(tutorReq, out vtTutor) == REQUESTCODE.EXECUTE_SUCCESS && vtTutor != null && vtTutor.Length > 0)
                {

                }
                else
                {
                    EXTIDENTACC setTutor = new EXTIDENTACC();
                    setTutor.dwAccNo = teacherAcc.dwAccNo;
                    setTutor.szTrueName = teacherAcc.szTrueName;
                    RESEARCHTEST setResarch = new RESEARCHTEST();
                    setResarch.szRTName = teacherAcc.szTrueName;
                    setResarch.dwRTKind = (uint)RESEARCHTEST.DWRTKIND.RTKIND_RTASK;
                    UNIGROUP setGroup = new UNIGROUP();
                    setGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
                    setGroup.szName = teacherAcc.szTrueName + ConfigConst.GCTutorName+"组";

                    if (m_Request.Group.SetGroup(setGroup, out setGroup) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        setResarch.dwGroupID = setGroup.dwGroupID;
                    }
                    setResarch.szRTName = setTutor.szTrueName + ConfigConst.GCReachTestName;
                    setResarch.dwLeaderID = setTutor.dwAccNo;
                    setResarch.szLeaderName = setTutor.szTrueName;
                    if (m_Request.Reserve.SetResearchTest(setResarch, out setResarch) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        m_Request.Account.ExtIdentAccSet(setTutor);
                    }

                }
               
            }
            TUTORSTUDENT turtorStudent = new TUTORSTUDENT();
            turtorStudent.dwTutorID = teacherAcc.dwAccNo;
            turtorStudent.szTutorName = teacherAcc.szTrueName;
            turtorStudent.dwAccNo = studentAcc.dwAccNo;
            //turtorStudent.dwStatus = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK);
            turtorStudent.szPID = studentAcc.szLogonName;
            turtorStudent.szTrueName = studentAcc.szTrueName;            
                      
            if (szHandPhone != null)
            {
                studentAcc.szHandPhone = szHandPhone;
            }
            if (szEmail != null)
            {
                studentAcc.szEmail = szEmail;
            }
            m_Request.Account.Set(studentAcc, out studentAcc);
            if (m_Request.Account.TutorStudentSet(turtorStudent) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"message\":\"succ\"}");                
            }
            else
            {
                Response.Write("{\"message\":\"" + m_Request.szErrMessage + "\"}");                
            }
        }
        else
        {
            Response.Write("{\"message\":\"" + ConfigConst.GCTutorName+"信息未指定" + "\"}");   
        }
    }
}