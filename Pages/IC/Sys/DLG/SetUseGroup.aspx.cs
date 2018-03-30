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
using System.Text;
using System.Reflection;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szOut = "";
    protected string szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        nDefaultNeedLine = 20;
        isImport.Value = "0";
        m_Title = "设置成员";
     
        uint uGroupID = 0;
        if (Request["id"] != null)
        {
            uGroupID = Parse(Request["dwID"]);
        }
        if (IsPostBack)
        {
            string szMyOp = Request["myOp"];
            string szcheckAccno = Request["checkAccno"];
            if (szcheckAccno != null && szcheckAccno != "")
            {
                string[] checkAccnoList = szcheckAccno.Split(',');
                for (int i = 0; i < checkAccnoList.Length; i++)
                {
                    DelGroupMember(uGroupID, Parse(checkAccnoList[i]),(uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    
                }
            }
            if ( Request.Files.Count > 0)
            {

                if (szMyOp == "view")
                {
                    string szTempPath = MyVPath + "Upload/Import_" + DateTime.Now.Ticks + ".csv";
                    string szTempRawPath = Server.MapPath(szTempPath);
                    Request.Files[0].SaveAs(szTempRawPath);
                    using (CsvReader csv = new CsvReader(new StreamReader(Server.MapPath(szTempPath), Encoding.GetEncoding("gb2312")), true))
                    {
                        //字段数量
                        int fieldCount = csv.FieldCount;
                        //标题数组
                        string[] headers = csv.GetFieldHeaders();

                        szOut = "<table class='tblCSV' style='width:120px'><thead><tr>";
                        for (int i = 0; i < fieldCount; i++)
                        {
                            szOut += "<th>" + headers[i] + "</th>";
                        }
                        szOut += "</tr></thead><tbody>";

                        int n = 0;
                        //只进的游标读取
                        while (csv.ReadNextRecord())
                        {
                            //遍历列
                            szOut += "<tr>";
                            for (int i = 0; i < fieldCount; i++)
                            {
                                szOut += "<td>" + csv[i] + "</td>";
                            }
                            szOut += "</tr>";
                        }

                        szOut += "</tbody>";

                        szOut += "<tfoot><tr><td class='importTblMore' colspan='" + fieldCount + "'></td></tr></tfoot>";

                        szOut += "</table>";
                        ViewState["path"] = szTempPath;
                        isImport.Value = "1";
                    }
                }
                else if (szMyOp == "import")
                {
                    string strline;
                    string szPath = (string)ViewState["path"];
                    if (szPath == null)
                    {
                        return;
                    }
                    System.IO.StreamReader mysr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(szPath), Encoding.GetEncoding("gb2312"));
                    while ((strline = mysr.ReadLine()) != null)
                    {
                        string[] szList = strline.Split(',');
                        string szLogonName = szList[0];
                        string szTrueName = (szList[1]);
                        UNIACCOUNT accinfoTeachar;
                        if (GetAccByLogonName(szLogonName, out accinfoTeachar,true))
                        {
                            if (uGroupID != 0)
                            {
                                AddGroupMember(uGroupID, accinfoTeachar.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL, accinfoTeachar.szTrueName);
                            }
                        }


                    }
                }
            }

        }
        ACCREQ acctest = new ACCREQ();
        GetHTTPObj(out acctest);
        GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
        
        string logonname = Request["logonname"];
        logonname = szGetReq(logonname);
        UNIACCOUNT accInfo;
        if (logonname != null && logonname!=""&&GetAccByLogonName(logonname, out accInfo))
        {
            vrGet.dwAccNo = accInfo.dwAccNo;
        }
        GetPageCtrlValue(out vrGet.szReqExtInfo);
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        string szOrderKey = vrGet.szReqExtInfo.szOrderKey;
        string szOrderMode = vrGet.szReqExtInfo.szOrderMode;
        if (szOrderKey != null && szOrderKey != "" && szOrderKey != "," && szOrderMode != null && szOrderMode != "" && szOrderMode != ",")
        {
            vrGet.szReqExtInfo.szOrderKey = szOrderKey.Split(',')[0];
            vrGet.szReqExtInfo.szOrderMode = szOrderMode.Split(',')[0];
        }
        if (szOrderKey == "," || szOrderMode == ",")
        {
            vrGet.szReqExtInfo.szOrderKey = null;
            vrGet.szReqExtInfo.szOrderMode = null;
        }
        vrGet.dwGroupID = Parse(Request["dwID"]);
        GROUPMEMDETAIL[] vtRes;
        PutMemberValue("id",vrGet.dwGroupID.ToString());
        REQUESTCODE uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {

            for (int i = 0; i < vtRes.Length; i++)
            {
                string szTurLogonName = "";
                string szTtrueName = "";
                UNIACCOUNT accTurtor = new UNIACCOUNT();
                if (GetAccByAccno(vtRes[i].dwTutorID.ToString(), out accTurtor))
                {
                    szTurLogonName = accTurtor.szLogonName;
                    szTtrueName = accTurtor.szTrueName;
                }

                m_szOut += "<tr>";
                m_szOut += "<td  data-szTtrueName=\"" + (szTtrueName) + "\" data-sLogonName=\"" + (vtRes[i].szPID) + "\" data-truename=\"" + (vtRes[i].szTrueName) + "\" data-end=\"" + GetDateStr((uint)vtRes[i].dwEndDate) + "\" data-begin=\"" + GetDateStr((uint)vtRes[i].dwBeginDate) + "\" data-tLogonName=\"" + szTurLogonName.ToString() + "\"  data-accno=\"" + vtRes[i].dwAccNo.ToString() + "\" data-handphone=\"" + vtRes[i].szHandPhone.ToString() + "\" data-email=\"" + vtRes[i].szEmail.ToString() + "\">" + "<input type='checkbox' name='checkAccno' value=" + vtRes[i].dwAccNo + " />" + "</td>";
                m_szOut += "<td  data-szTtrueName=\"" + (szTtrueName) + "\" data-sLogonName=\"" + (vtRes[i].szPID) + "\" data-truename=\"" + (vtRes[i].szTrueName) + "\" data-end=\"" + GetDateStr((uint)vtRes[i].dwEndDate) + "\" data-begin=\"" + GetDateStr((uint)vtRes[i].dwBeginDate) + "\" data-tLogonName=\"" + szTurLogonName.ToString() + "\"  data-accno=\"" + vtRes[i].dwAccNo.ToString() + "\" data-handphone=\"" + vtRes[i].szHandPhone.ToString() + "\" data-email=\"" + vtRes[i].szEmail.ToString() + "\">" + vtRes[i].szTrueName + "</td>";
                m_szOut += "<td>" + vtRes[i].szPID + "</td>";
                m_szOut += "<td>" + vtRes[i].szClassName + "</td>";
                m_szOut += "<td>" + vtRes[i].szDeptName + "</td>";
                m_szOut += "<td>" + vtRes[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vtRes[i].szEmail + "</td>";
                m_szOut += "<td>" + GetDateStr(vtRes[i].dwBeginDate) + "</td>";
                m_szOut += "<td>" +GetDateStr(vtRes[i].dwEndDate) + "</td>";
                m_szOut += "<td>" + GetJustName(vtRes[i].dwStatus, "GROUPMEMBER_Status") + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        //    UpdatePageCtrl(m_Request.Group);
        }
    }
    protected string szGetReq(string szValue)
    {
        string[] SqlForbid = @"--|'".Split('|');//禁用字符
        string[] anySqlStr = @"<|>|*|+|=".Split('|');//警告字符
        string[] anySqlKey = @"select|insert|delete|from|admin|set|alter|where|or|and|delay".Split('|');//确认字符
        if (!string.IsNullOrEmpty(szValue))
        {
            if (szValue.ToLower().IndexOf("script") > -1)
            {
                szValue = szValue.ToLower().Replace("script", "");
            }
            for (int m = 0; m < SqlForbid.Length; m++)
            {
                string szTempForbid = SqlForbid[m];
                if (szValue.ToLower().IndexOf(szTempForbid) > -1)
                {
                    szValue = szValue.ToLower().Replace(szTempForbid, "");
                }
            }
            for (int m = 0; m <anySqlStr.Length; m++)
            {
                string szTempForbid = anySqlStr[m];
                if (szValue.ToLower().IndexOf(szTempForbid) > -1)
                {
                    for (int k = 0; k < anySqlKey.Length; k++)
                    {
                        string szTempForbid2 = anySqlKey[k];
                        if (szValue.ToLower().IndexOf(szTempForbid2) > -1)
                        {
                            szValue = szValue.ToLower().Replace(szTempForbid, "");
                            szValue = szValue.ToLower().Replace(szTempForbid2, "");
                        }
                    }
                }
            }

        }
        return szValue;
    }
}

