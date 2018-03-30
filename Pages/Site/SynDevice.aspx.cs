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
using System.Data.OracleClient;
public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEPT[] deptList = GetAllDept();
        SyLab(deptList);

        string ConnectionString = "Data Source=IDC_U_DC;user=idc_u_cs;password=idc_u_cs;";//写连接串
        OracleConnection conn = new OracleConnection(ConnectionString);//创建一个新连接
        DataSet dsHourse = new DataSet();
        dsHourse = getHourse(conn);
        SyHouse(dsHourse);
        int nHourseCount = 0;
        int nRoomCount = 0;
        if (dsHourse != null)
        {
            DataTable dtHouse = new DataTable();
            dtHouse = dsHourse.Tables[0];
            nHourseCount = dtHouse.Rows.Count;
        }
        DataSet dsRoom = new DataSet();
        dsRoom = getRoom(conn);
        if (dsRoom != null)
        {
            DataTable dtRoom= new DataTable();
            dtRoom = dsRoom.Tables[0];
            nRoomCount = dtRoom.Rows.Count;
        }
        Logger.trace("楼宇数目："+nHourseCount+"房间数目："+nRoomCount);
        if(nHourseCount==0)
        {
            return;
        }

    }
    private void SyLab(UNIDEPT[] deptList)//同步实验室
    {
        UNILAB[] allLab = GetAllLab();
        int uAll = 0;
        int uADD = 0;
        int uAddSuccse = 0;
        int uAddFail = 0;
        int uSet = 0;
        int uSetSuccse = 0;
        int uSetFail = 0;
        if (deptList != null && deptList.Length > 0)
        {
            uAll = deptList.Length;
            for (int i = 0; i < deptList.Length; i++)
            {
                bool bAdd=true;
                UNILAB setLab = new UNILAB();
                string szDeptName = deptList[i].szName.ToString();
                if(allLab==null||allLab.Length==0)
                {
                    bAdd=true;
                }
                else
                {
                    for (int j = 0; j < allLab.Length; j++)
                    {
                        if (allLab[j].szLabName.ToString() == szDeptName)
                        {
                            setLab = allLab[j];
                            bAdd = false;
                            break;
                        }
                    }
                }
                if (bAdd)
                {
                    uADD = uADD + 1;
                    UNIGROUP manGroup = new UNIGROUP();
                    if (NewGroup(deptList[i].szName.ToString(), (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out manGroup))
                    {
                        UNILAB newLab = new UNILAB();
                        newLab.szLabName = deptList[i].szName.ToString();
                        if (deptList[i].szDeptSN != null)
                        {
                            newLab.szLabSN = deptList[i].szDeptSN.ToString();
                        }
                        newLab.dwDeptID = deptList[i].dwID;
                        newLab.dwManGroupID = manGroup.dwGroupID;
                        if (m_Request.Device.LabSet(newLab, out newLab) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            uAddSuccse = uAddSuccse + 1;
                            Logger.trace("新建实验室成功" + newLab.szLabName.ToString());
                            allLab = GetAllLab();
                        }
                        else
                        {
                            uAddFail = uAddFail +1;
                            Logger.trace("新建实验室失败:" + newLab.szLabName.ToString() + ":" + m_Request.szErrMessage.ToString());
                        }
                    }
                    else
                    {
                        uAddFail = uAddFail+1;
                        Logger.trace("新建实验室失败:" + deptList[i].szName.ToString() + "管理员组新建失败:" + m_Request.szErrMessage.ToString());
                    }
                }
                else//修改
                {
                    uSet = uSet + 1;
                    if (deptList[i].szDeptSN != null)
                    {
                        setLab.szLabSN = deptList[i].szDeptSN.ToString();
                        if (m_Request.Device.LabSet(setLab, out setLab) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            uSetSuccse = uSetSuccse + 1;
                            Logger.trace("更新实验室成功" + setLab.szLabName.ToString());
                        }
                        else
                        {
                            uSetFail = uSetFail + 1;
                            Logger.trace("更新实验室失败:" + setLab.szLabName.ToString() + ":" + m_Request.szErrMessage.ToString());
                        }
                    }
                }
            }
        }
        Logger.trace("实验室总共需同步数据:" + uAll + ";新建数据" + uADD + "；新建成功:" + uAddSuccse + "；新建失败:" + uAddFail + "；更新总数据:" + uSet + "；更新成功:" + uSetSuccse + "；更新失败:" + uSetFail);
    }
    private DataSet getHourse(OracleConnection conn)
    {
        try
        {
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select *
from idc_u_dc.v_ast_house_infor
where  HOUSE_SMALL_CLASSIFY_CODE not in('3.1','3.2','3.3','3.4','1.2') and HOUSE_BIG_CLASSIFY_CODE<>4 and CAMPUS_NAME<>'其他校区' and HOUSE_code   in(select distinct HOUSE_code from  idc_u_dc.V_AST_ROOM_INFOR)";//在这儿写sql语句
            Logger.trace(cmd.CommandText);
            cmd.Connection = conn;
            OracleDataAdapter adpt = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message); //如果有错误，输出错误信息
        }
        finally
        {
            conn.Close(); //关闭连接
            
        }
        return null;
    }
    private void SyHouse(DataSet dsHouse)
    {
        int uAll = 0;
        int uADD = 0;
        int uAddSuccse = 0;
        int uAddFail = 0;
        int uSet = 0;
        int uSetSuccse = 0;
        int uSetFail = 0;
        UNICAMPUS[] camp = GetAllCampus();
        UNIBUILDING[] allBuilding = getAllBuilding();
        if (camp == null || camp.Length == 0)
        {
            Logger.trace("校区内容为空 不同步楼宇");
            return;
        }
        DataTable dt = new DataTable();
        dt = dsHouse.Tables[0];
        uAll = dt.Rows.Count;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string szHouseSN = dt.Rows[i]["house_code"].ToString();
            UNIBUILDING setBuilding = new UNIBUILDING();
            bool bAdd = true;
            for (int j = 0; j < allBuilding.Length; j++)
            {
                if (allBuilding[j].szBuildingNo.ToString() == szHouseSN)
                {
                    setBuilding = allBuilding[i];
                    bAdd = false;
                    break;
                }
            }
            string szCampID = dt.Rows[i]["campus_code"].ToString();
                bool isCamp = false;
                for (int k = 0; k < camp.Length; k++)
                {
                    if (camp[k].dwCampusID.ToString() == szCampID)
                    {
                        isCamp = true;
                        break;
                    }
                }
                if (isCamp == false)
                {
                    Logger.trace("操作楼宇失败:楼宇对应的校区编号不存在");
                    continue;
                }
            if (bAdd)
            {
                uADD = uADD + 1;
                
                UNIBUILDING newBuliding = new UNIBUILDING();
                newBuliding.szBuildingNo = dt.Rows[i]["house_code"].ToString();
                newBuliding.szBuildingName = dt.Rows[i]["house_name"].ToString();
                newBuliding.szCampusName = dt.Rows[i]["house_name"].ToString();
                newBuliding.dwCampusID = Parse(szCampID);
                if (m_Request.Device.BuildingSet(newBuliding, out newBuliding) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uAddSuccse = uAddSuccse + 1;
                    Logger.trace("新建楼宇室成功" + newBuliding.szBuildingName.ToString());
                    allBuilding = getAllBuilding();
                }
                else
                {
                    uAddFail = uAddFail + 1;
                    Logger.trace("新建楼宇室成功:" + newBuliding.szBuildingName.ToString() + ":" + m_Request.szErrMessage.ToString());
                }
            }
            else
            {
                uSet = uSet + 1;
                setBuilding.szBuildingName = dt.Rows[i]["house_name"].ToString();
                setBuilding.dwCampusID = Parse(szCampID);
                if (m_Request.Device.BuildingSet(setBuilding, out setBuilding) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uSetSuccse = uSetSuccse + 1;
                    Logger.trace("更新楼宇室成功" + setBuilding.szBuildingName.ToString());
                    allBuilding = getAllBuilding();
                }
                else
                {
                    uSetFail = uSetFail + 1;
                    Logger.trace("更新楼宇室成功:" + setBuilding.szBuildingName.ToString() + ":" + m_Request.szErrMessage.ToString());
                }
            }
        }
        Logger.trace("楼宇总共需同步数据:" + uAll + ";新建数据" + uADD + "；新建成功:" + uAddSuccse + "；新建失败:" + uAddFail + "；更新总数据:" + uSet + "；更新成功:" + uSetSuccse + "；更新失败:" + uSetFail);
    }
    private DataSet getRoom(OracleConnection conn)
    {
        try
        {
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            Logger.trace("sz");
            cmd.CommandText = @"select *
from  idc_u_dc.V_AST_ROOM_INFOR
where room_code not in(
select b.room_code
from  idc_u_dc.V_AST_ROOM_INFOR  b, (select *
from idc_u_dc.v_ast_house_infor
where  HOUSE_SMALL_CLASSIFY_CODE not in('3.1','3.2','3.3','3.4','1.2') and HOUSE_BIG_CLASSIFY_CODE<>4 and CAMPUS_NAME<>'其他校区' and HOUSE_code   in(select distinct HOUSE_code from  idc_u_dc.V_AST_ROOM_INFOR)
)  a
where ((a.house_small_classify_code='1.1' and( b.classroom_type_code=9 or b.classroom_type_code is  null))  or (a.house_small_classify_code='1.3' and (b.classroom_type_code=9 or b.classroom_type_code is  null) ) 
)
and a.house_code=b.house_code
) and house_code in(
select house_code
from idc_u_dc.v_ast_house_infor
where  HOUSE_SMALL_CLASSIFY_CODE not in('3.1','3.2','3.3','3.4','1.2') and HOUSE_BIG_CLASSIFY_CODE<>4 and CAMPUS_NAME<>'其他校区' and HOUSE_code   in(select distinct HOUSE_code from  idc_u_dc.V_AST_ROOM_INFOR))";//在这儿写sql语句
            cmd.Connection = conn;
            Logger.trace(cmd.CommandText);
            OracleDataAdapter adpt = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message); //如果有错误，输出错误信息
        }
        finally
        {
            conn.Close(); //关闭连接

        }
        return null;
    }
    private void SyRoom(DataSet ds)
    {

    }
}
