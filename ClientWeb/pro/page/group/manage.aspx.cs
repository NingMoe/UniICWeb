using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_group_manage : UniClientPage
{
    protected string mbList = "";
    protected string mbDetail = "";
    //protected string pageCtrl = "";
    protected string groupName = "";
    protected string groupVol = "";
    protected string hideCls = "none";
    protected string testId;
    uint? minUser = 0;
    uint? maxUser = 0;
    public string szSearchKey = "姓名/登录名搜索";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szAccLogonName = GetConfig("searchAccLogonName");
        if (szAccLogonName == "4")
        {
            szSearchKey = ("学号/工号搜索");
        }
        szSearchKey = Translate(szSearchKey);
        if (IsFrameLogin())
        {
            //ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (!string.IsNullOrEmpty(Request["test_id"]))//教学组
            {
                testId = Request["test_id"];
                InitTestItemGroup(testId);
            }
            else//普通组
            {
                if (Request["need_cls"] == "true" && IsStat(acc.dwIdent,(uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER))
                    hideCls = "";
                DelMb();
                if (!IsPostBack)
                {
                    string group = Request["group"];
                    if (!string.IsNullOrEmpty(group)) group_id.Value = group;
                    if (string.IsNullOrEmpty(group_id.Value))
                        CreGroup();
                }
            }
        }
    }

    private void InitTestItemGroup(string testId)
    {
        //TESTITEMREQ req = new TESTITEMREQ();
        UNITESTITEM it = GetTestItemByID(testId);
        if (it.dwTestItemID != null)
        {
            TESTITEMMEMRESV[] list = GetTestMemResv(it.dwTestItemID, it.dwTestPlanID, it.dwGroupID, null);
            if (list != null)
            {
                //初始组信息
                group_id.Value = it.dwGroupID.ToString();
                group_name.Value = it.szGroupName;
                mb_num.Value = it.dwGroupUsers.ToString();
                //
                uint? num = it.dwResvTestHour;
                //int pg = 10;
                for (int i = 0; i < list.Length; i++)
                {
                    //if (i % pg == 0)
                    //{
                    //if (i != 0) mbList += "</tbody>";
                    //pageCtrl += "<li><span>" + (i / pg + 1) + "</span></li>";
                    //mbList += "<tbody>";
                    //}
                    uint? h = list[i].dwResvTestHour;
                    mbList += "<tr key='" + list[i].dwAccNo + "' kind='2' class='" + (h == num ? "it" : "done") + "'><td class='ellipsis'>" + list[i].szTrueName + "   <span class='grey'>(已预约:" + h + " 时)</span></td><td class='text-center grey click item_toggle'>" +
                        "<span class='glyphicon glyphicon-ok'></span></td></tr>";
                    //if (i == list.Length - 1) mbList += "</tbody>";
                }
            }
            else
            {
                MsgBox(m_Request.szErrMsg);
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(testId))
            InitGroup();
    }

    private void DelMb()
    {
        string user = Request["del_user_list"];
        string cls = Request["del_cls_list"];
        if (!string.IsNullOrEmpty(user))
        {
            string[] userList = user.Split(',');
            for (int i = 0; i < userList.Length; i++)
            {
                if (userList[i] != "")
                {
                    if (!DelMemByAccNo(group_id.Value, userList[i]))
                        MsgBox(m_Request.szErrMsg);
                }
            }
        }
        if (!string.IsNullOrEmpty(cls))
        {
            string[] clsList = cls.Split(',');

            for (int j = 0; j < clsList.Length; j++)
            {
                if (clsList[j] != "")
                {
                    if (!DelMember(group_id.Value, clsList[j], (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS))
                        MsgBox(m_Request.szErrMsg);
                }
            }
        }
    }

    private void CreGroup()
    {
        string template = Request["tp_group"];//初始成员组
        string accnos = Request["mb_accno"];
        string kind = Request["kind"];
        string name = Request["name"];
        string prefix = Request["prefix"];//组名前缀
        string max = Request["max"];
        string min = Request["min"];
        string line = Request["line"];
        string enrollLine = Request["enroll_line"];
        UNIGROUP para = new UNIGROUP();
        if (!string.IsNullOrEmpty(template))
        {
            GROUPREQ req = new GROUPREQ();
            //req.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
            //req.szGetKey = template;
            req.dwGroupID = ToUInt(template);
            UNIGROUP[] rlt;
            if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                para = rlt[0];
                para.dwGroupID = null;
            }
        }
        if (!string.IsNullOrEmpty(kind))
            para.dwKind = ToUInt(kind);
        else if (string.IsNullOrEmpty(template))
            para.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
        if (!string.IsNullOrEmpty(name))
            para.szName = name;
        else if (string.IsNullOrEmpty(template))
            para.szName = Translate("新建小组");
        if (!string.IsNullOrEmpty(max))
            para.dwMaxUsers = ToUInt(max);
        if (!string.IsNullOrEmpty(min))
            para.dwMinUsers = ToUInt(min);
        if (!string.IsNullOrEmpty(line))
            para.dwDeadLine = ToUInt(line.Replace("-",""));
        if (!string.IsNullOrEmpty(enrollLine))
            para.dwEnrollDeadline = ToUInt(enrollLine.Replace("-", ""));
        if (m_Request.Group.SetGroup(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (para.dwGroupID != null)
            {
                group_id.Value = para.dwGroupID.ToString();
                if (!string.IsNullOrEmpty(prefix))
                {
                    para.szName = prefix + "_" + para.dwGroupID.ToString();
                    m_Request.Group.SetGroup(para, out para);
                }
                if (!string.IsNullOrEmpty(accnos))
                {
                    string[] list = accnos.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i] != "")
                            AddMemByAccNo(group_id.Value, list[i]);
                    }
                }
                //临时 模板组成员加入新组
                if (!string.IsNullOrEmpty(template))
                {
                    GROUPMEMDETAILREQ greq = new GROUPMEMDETAILREQ();
                    greq.dwGroupID = ToUInt(template);
                    GROUPMEMDETAIL[] grlt;
                    if (m_Request.Group.GetGroupMemDetail(greq, out grlt) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        for (int i = 0; i < grlt.Length; i++)
                        {
                            AddMemByAccNo(para.dwGroupID.ToString(), grlt[i].dwAccNo.ToString());
                        }
                    }
                }
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }

    private void InitGroup()
    {
        string group = group_id.Value;
        if (string.IsNullOrEmpty(group)) return;
        GROUPREQ req = new GROUPREQ();
        //req.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
        //req.szGetKey = group;
        req.dwGroupID = ToUInt(group);
        req.dwReqProp = (uint)GROUPMEMDETAILREQ.DWREQPROP.GROUPMEMDETAILREQ_NEEDDEL;
        UNIGROUP[] rlt;
        if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                minUser = rlt[0].dwMinUsers;
                maxUser = rlt[0].dwMaxUsers;
                min_user.Value = minUser.ToString();
                if (minUser > 0) groupVol += "<span class='grey uni_trans uni_trans'>至少:</span><span class='red'>" + minUser + "</span> ";
                if (maxUser > 0 && maxUser < 1000) groupVol += "<span class='grey uni_trans'>最多:</span><span class='red'>" + maxUser + "</span> ";
                //int pg = 10;
                GROUPMEMBER[] mbs = rlt[0].szMembers;
                groupName = group_name.Value = rlt[0].szName;
                mb_num.Value = "0";//GetGroupMemCount(rlt[0].dwID).ToString();
                GROUPMEMDETAIL[] detail = GetGroupDetail(rlt[0].dwGroupID);
                if (detail != null)//成员详细列表
                {
                    mb_num.Value = detail.Length.ToString();
                    for (int i = 0; i < detail.Length; i++)
                    {
                        GROUPMEMDETAIL d = detail[i];
                        //<td>" + (d.dwSex == 0 ? "保密" : (d.dwSex == 1 ? "男" : "女")) + "</td>
                        mbDetail += "<tr><td>" + d.szTrueName + "</td><td>" + d.szPID + "</td><td>" + d.szClassName + "</td><td>"+d.szDeptName+"</td></tr>";
                    }
                }
                if (mbs != null)//成员列表
                {
                    UNIACCOUNT acc;
                    if (Session["LOGIN_ACCINFO"] != null)
                        acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    else acc.dwAccNo = null;
                    for (int i = 0; i < mbs.Length; i++)
                    {
                        //if (i % pg == 0)
                        //{
                        //    if (i != 0) mbList += "</tbody>";
                        //    //pageCtrl += "<li><span>" + (i / pg + 1) + "</span></li>";
                        //    mbList += "<tbody>";
                        //}
                        bool my = acc.dwAccNo == mbs[i].dwMemberID;
                        mbList += "<tr key='" + mbs[i].dwMemberID + "' kind='" + mbs[i].dwKind + "' class='it " + (my ? "my" : "") + "'><td class='ellipsis'>" + mbs[i].szName + "   <span class='grey'>（" + mbs[i].szMemo + "）</span></td><td class='text-center grey click item_toggle'>" +
                            (my ? "<span class='glyphicon glyphicon-user'></span>" : "<span class='glyphicon glyphicon-ok'></span>") + "</td></tr>";
                        //if (i == mbs.Length - 1) mbList += "</tbody>";
                    }
                }
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    private GROUPMEMDETAIL[] GetGroupDetail(uint? groupId)
    {
        GROUPMEMDETAILREQ req = new GROUPMEMDETAILREQ();
        req.dwGroupID = groupId;
        GROUPMEMDETAIL[] rlt;
        if (m_Request.Group.GetGroupMemDetail(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return rlt;
        }
        return null;
    }
    protected void add_class_ServerClick(object sender, EventArgs e)
    {
        string classId = Request["class_id"];
        if (!string.IsNullOrEmpty(classId))
        {
            string name = Request["class_name"];
            if (!AddMember(group_id.Value, classId, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS, name))
                MsgBox(m_Request.szErrMsg);
        }
    }
    protected void add_user_ServerClick(object sender, EventArgs e)
    {
        string user_accno = Request["user_accno"];
        if (!string.IsNullOrEmpty(user_accno))
        {
            if (!AddMemByAccNo(group_id.Value, user_accno))
                MsgBox(m_Request.szErrMsg);
        }
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }
    public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode(strFileName);
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void exportFile_ServerClick(object sender, EventArgs e)
    {
        string group = group_id.Value;
        if (string.IsNullOrEmpty(group)) return;
        GROUPMEMDETAIL[] detail = GetGroupDetail(ToUInt(group));
        if (detail != null)
        {
            System.IO.StringWriter swCSV = new System.IO.StringWriter();
            swCSV.WriteLine("姓名,学号,班级,学院");
            for (int i = 0; i < detail.Length; i++)
            {
                GROUPMEMDETAIL mb = detail[i];
                System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                sbText = AppendCSVFields(sbText, mb.szTrueName);
                sbText = AppendCSVFields(sbText, mb.szPID);
                sbText = AppendCSVFields(sbText, mb.szClassName);
                sbText = AppendCSVFields(sbText, mb.szDeptName);
                //去掉尾部的逗号
                sbText.Remove(sbText.Length - 1, 1);
                //写datatable的一行
                swCSV.WriteLine(sbText.ToString());
            }
            DownloadFile(Response, swCSV.GetStringBuilder(), "成员名单.csv");
            swCSV.Close();
            Response.End();
        }
    }
}