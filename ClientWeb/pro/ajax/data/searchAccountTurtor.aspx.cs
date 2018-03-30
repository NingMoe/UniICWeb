using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szTerm = Request["term"];
        string szType = Request["type"];
        Response.CacheControl = "no-cache";

        ACCREQ vrGet = new ACCREQ();
        UNIACCOUNT[] vtAccount;
        if (szType == null || szType == "")
        {
            vrGet.szTrueName = szTerm;
           // vrGet.dwGetType = (uint)ACCREQ.DWGETTYPE.ACCGET_BYTRUENAME;
        }
        else if (szType.ToLower() == "logonname")
        {
            vrGet.szLogonName = szTerm;// (uint)ACCREQ.DWGETTYPE.ACCGET_BYLOGONNAME;
        }
        else if (szType.ToLower() == "truename")
        {
            vrGet.szTrueName = szTerm;// (uint)ACCREQ.DWGETTYPE.truname;
        }

        uint dwIdent = ToUInt(Request["dwIdent"]);
        if (Request["dwIdent"] != null && dwIdent != 0)
        {
            vrGet.dwIdent = dwIdent;
        }
        else
        {
            vrGet.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
        }
       // vrGet.szGetID = szTerm;
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Account.Get(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtAccount.Length; i++)
            {
                string szTurotorIs = "";
                string szTurtorName = "";
                string szTurTorLogonName = "";
                UNITUTOR turtor = new UNITUTOR();
                if (GetTurtor((uint)vtAccount[i].dwAccNo, out turtor))
                {
                    szTurtorName =ConfigConst.GCTutorName+ ":" + turtor.szTrueName;
                    szTurotorIs = "true";
                    UNIACCOUNT[] accs=GetAccByAccNo(turtor.dwAccNo.ToString());
                    if (accs!=null&&accs.Length>0)
                    {                   
                        szTurTorLogonName = accs[0].szLogonName.ToString();
                    }
                    
                }
                else
                {
                    szTurtorName =ConfigConst.GCTutorName+ "未指定:";
                    szTurotorIs = "false";
                }

                szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + ";" + szTurtorName + "(" + vtAccount[i].szDeptName + ")" + "\",\"szIsExistTur\": \"" + szTurotorIs + "\",\"szTurtorID\": \"" + vtAccount[i].dwTutorID.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szTurTor\": \"" + szTurtorName + "\",\"szTurTorLogonName\": \"" + szTurTorLogonName + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szTurtorTrueName\": \"" + turtor.szTrueName + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                //szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "\"}";
                if (i < vtAccount.Length - 1)
                {
                    szOut += ",";
                }
            }
            
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[{}]");
        }
    }
    private bool GetTurtor(uint uAccno,out UNITUTOR res)
    {
        res = new UNITUTOR();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TUTORREQ vrGet = new TUTORREQ();
        vrGet.dwStudentAccNo = uAccno;
        UNITUTOR[] vtRes;
        uResponse = m_Request.Account.TutorGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            res = vtRes[0];
            return true;
        }
        return false;
        //return null;
    }
    
}