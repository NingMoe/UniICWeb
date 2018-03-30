using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;
using System.Data.Sql;
using System.Data.SqlClient;
public partial class _Default : UniPage
{
    protected string m_Title = "审核";
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        uint uPlanID = Parse(Request["planid"]);

        ACTIVITYPLANREQ planyReq = new ACTIVITYPLANREQ();
        planyReq.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        planyReq.szGetKey = uPlanID.ToString();
        UNIACTIVITYPLAN[] planRes;
        uint? uplanDate = 0;
        uint? uResvID = 0;
        if (m_Request.Reserve.GetActivityPlan(planyReq, out planRes) == REQUESTCODE.EXECUTE_SUCCESS && planRes != null && planRes.Length > 0)
        {
            uResvID = planRes[0].dwResvID;
            uplanDate = planRes[0].dwActivityDate;
        }
        DataTable dt = GetCardUser();
        if (!IsPostBack)
        {

            for (int i = 0; dt != null && i < dt.Rows.Count; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + dt.Rows[i]["kahao"] + "</ td > ";
                m_szOut += "<td>" + dt.Rows[i]["name"] + "</ td > ";
                m_szOut += "<td>" + dt.Rows[i]["shuakatime"] + "</ td > ";
                m_szOut += "</tr>";

            }
        }
        else {
          int uCount=ControlDT(dt,planRes[0]);
            MessageBox("成功处理" + uCount.ToString() + "条", "处理完毕", MSGBOX.SUCCESS);
        }
       



    }
    public int ControlDT(DataTable dt, UNIACTIVITYPLAN plan)
    {

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        AOFFLINESIGN req = new AOFFLINESIGN();
        req.dwActivityPlanID = plan.dwActivityPlanID;
        req.dwResvID = plan.dwResvID;
        List<ASIGNUSER> users = new List<ASIGNUSER>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ASIGNUSER temp = new ASIGNUSER();
            int nCard = IntParse(dt.Rows[i]["cardid"].ToString());
            temp.dwCardID =(uint)nCard;
            temp.dwInTime = Get1970Seconds(dt.Rows[i]["shuakatime"].ToString());
            users.Add(temp);
        }
        req.SignUser = users.ToArray();
        AOFFLINESIGN res;
        REQUESTCODE ucode = m_Request.Reserve.ActivityMemberOffLineSign(req, out res);
        if (ucode == REQUESTCODE.EXECUTE_SUCCESS &&res.SignUser!=null&&res.SignUser.Length>0)
        {
         return   updateDT(res.SignUser);
        }
        return 0;
    }
    public int updateDT(ASIGNUSER[] user)
    {
        string szLogonNames = "";
        for (int i = 0; i < user.Length; i++)
        {
            szLogonNames += "'"+user[i].szLogonName.ToString() +"'"+ ",";
        }
        if (szLogonNames.EndsWith(","))
        {
            szLogonNames = szLogonNames.Substring(0, szLogonNames.Length - 1);
        }
        
        string szSQL = ConfigurationManager.ConnectionStrings["constr"].ToString();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(szSQL);
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            MessageBox("获取刷卡信息失败，连接数据库失败" + ex.ToString(), "错误", MSGBOX.ERROR);
            
        }
        string strSQL = "update jilu set status=4 where kahao in("+szLogonNames+")";
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        DataSet ds = new DataSet();
        try
        {
         return   cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        return 0;
    }
    public DataTable GetCardUser()
    {
        string szSQL = ConfigurationManager.ConnectionStrings["constr"].ToString();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(szSQL);
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            MessageBox("获取刷卡信息失败，连接数据库失败" + ex.ToString(),"错误",MSGBOX.ERROR);
            return null;
        }
        string strSQL = "select * from jilu where status=2";
        SqlDataAdapter adapter = new SqlDataAdapter(strSQL, conn);
        DataSet ds = new DataSet();
        try
        {
            adapter.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
        return dt;

    }
}
