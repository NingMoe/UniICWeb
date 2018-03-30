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
        LoginIn("staadmin001", "unifound808");
        UNIDEPT[] deptList = GetAllDept();
        Logger.trace("同步部门实验室开始");
        SyLab(deptList);
        Logger.trace("同步部门实验室结束");
        string ConnectionString = "Data Source=IDC_U_DC;user=idc_u_cs;password=idc_u_cs;";//写连接串
        OracleConnection conn = new OracleConnection(ConnectionString);//创建一个新连接
        DataSet dsHourse = new DataSet();
        dsHourse = getHourse(conn);
        string szDateTime = DateTime.Now.ToString("HHmm");
        uint nDateTime = Parse(szDateTime);
       
        Logger.trace("同步楼宇开始");
        SyHouse(dsHourse);
        Logger.trace("同步楼宇结束");
        //return;
        int nHourseCount = 0;
        int nRoomCount = 0;
        if (dsHourse != null)
        {
            DataTable dtHouse = new DataTable();
            dtHouse = dsHourse.Tables[0];
            nHourseCount = dtHouse.Rows.Count;
        }
        DataSet dsDevClass = new DataSet();
       dsDevClass = getDevClass(conn);
        Logger.trace("同步类别开始");
        SyDevClass(dsDevClass);
        Logger.trace("同步类别结束");

        DataSet dsRoom = new DataSet();
        dsRoom = getRoom(conn);
        Logger.trace("同步房间开始");
        SyRoom(dsRoom);
        Logger.trace("同步房间结束");
        if (dsRoom != null)
        {
            DataTable dtRoom= new DataTable();
            dtRoom = dsRoom.Tables[0];
            nRoomCount = dtRoom.Rows.Count;
        }
        Logger.trace("楼宇数目："+nHourseCount+"房间数目："+nRoomCount);
        Application["sydevice"] = "0";
        if(nHourseCount==0)
        {
            return;
        }

    }
    protected void LoginIn(string szLogonName, string szPassword)
    {
        ADMINLOGINREQ vrParameter = new ADMINLOGINREQ();
        ADMINLOGINRES vrResult;
        vrParameter.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER;
        vrParameter.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrParameter.szIP = "127.0.0.1";
        vrParameter.szLogonName = szLogonName;
        vrParameter.szPassword = "P" + szPassword;
        REQUESTCODE ret1;
        if ((vrParameter.dwLoginRole & (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER) > 0)
        {
            m_Request.m_UniDCom.StaSN = 0;
            //vrParameter.dwLoginRole =vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
            ret1 = m_Request.Admin.Login(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {

                if (m_Request.szErrMessage != "")
                {
                   
                }
                else
                {
                    
                }
                return;
                //ret1 = m_Request.Admin.Login(vrParameter, out vrResult);
            }
            else
            {
               
            }
        }
        else
        {
            vrParameter.dwLoginRole = vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
            ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {
                ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            }
            else
            {
                if (m_Request.szErrMessage != "")
                {
                    
                }
                else
                {
                   
                }
                return;
            }
        }

        if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrParameter.dwLoginRole == (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER)
            {
                if (vrParameter.szLogonName.ToLower() == "sysadmin")//vrResult.dwManRole == (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER
                {
                    Session["StationSN"] = (uint)0;
                    Session["SessionID"] = vrResult.dwSessionID;
                    Session["LoginResult"] = vrResult;
                  
                }
                else
                {
                    vrParameter.dwStaSN = 1;
                    m_Request.m_UniDCom.StaSN = 1;
                    m_Request.m_UniDCom.SessionID = (uint)vrResult.dwSessionID;
                    vrParameter.dwLoginRole = vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
                    ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
                    if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Session["StationSN"] = vrParameter.dwStaSN;
                        Session["SessionID"] = vrResult.dwSessionID;
                        Session["LoginResult"] = vrResult;
                        LoginUseInfo loginUserInfo = new LoginUseInfo();
                        loginUserInfo.szLogoName = szLogonName;
                        loginUserInfo.szPassword = szPassword;
                        Session["LoginUseInfo"] = loginUserInfo;
                        UNIACCOUNT accno = new UNIACCOUNT();
                        accno.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
                        Session["LOGIN_ACCINFO"] = accno;
                        m_Request.m_UniDCom.SessionID = (uint)vrResult.dwSessionID;
                        
                        if (vrResult.AdminInfo.dwAccNo == null)
                        {
                         
                            return;
                        }
                       // Response.Redirect("Inst/Main.aspx");
                    }
                    else
                    {
                       
                    }
                }
            }
        }
        else
        {
            
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
                        else {
                            newLab.szLabSN = deptList[i].dwID.ToString();
                        }
                        newLab.dwDeptID = deptList[i].dwID;
                        newLab.dwManGroupID = manGroup.dwGroupID;
                        string szLabNameTemp = newLab.szLabName.ToString();
                        if (m_Request.Device.LabSet(newLab, out newLab) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            uAddSuccse = uAddSuccse + 1;
                            Logger.trace("新建实验室成功" + newLab.szLabName.ToString());
                            allLab = GetAllLab();
                        }
                        else
                        {
                            uAddFail = uAddFail +1;
                            Logger.trace("新建实验室失败:" + szLabNameTemp + ":" + m_Request.szErrMessage.ToString());
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
                            Logger.trace("更新实验室失败:" + deptList[i].szName.ToString() + ":" + m_Request.szErrMessage.ToString());
                        }
                    }
                }
            }
        }
        Logger.trace("实验室总共需同步数据:" + uAll + ";新建数据" + uADD + "；新建成功:" + uAddSuccse + "；新建失败:" + uAddFail + "；更新总数据:" + uSet + "；更新成功:" + uSetSuccse + "；更新失败:" + uSetFail);
    }
    private DataSet getHourse(OracleConnection conn)
    {
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select *
from idc_u_dc.v_ast_house_infor
where  (house_code in ('1001','1002','1003','1004','2002','3001','3002','3005','4001','4002')) or (HOUSE_SMALL_CLASSIFY_CODE not in('3.1','3.2','3.3','3.4','1.2') and HOUSE_BIG_CLASSIFY_CODE<>4 and CAMPUS_NAME<>'其他校区' and HOUSE_code   in(select distinct HOUSE_code from  idc_u_dc.V_AST_ROOM_INFOR))";//在这儿写sql语句
            Logger.trace(cmd.CommandText);
            cmd.Connection = conn;
            OracleDataAdapter adpt = new OracleDataAdapter(cmd);
           
            adpt.Fill(ds);
            return ds;
        }
        catch (Exception ee)
        {
            Response.Write("获取楼宇"+ee.Message); //如果有错误，输出错误信息
        }
        finally
        {
            Response.Write("获取楼宇"+ ds.Tables[0].Rows.Count);
              conn.Close(); //关闭连接
            
        }
        return null;
    }
    private DataSet getDevClass(OracleConnection conn)
    {
        try
        {
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select distinct classroom_type_code,classroom_type_name
from idc_u_dc.V_AST_ROOM_INFOR";//在这儿写sql语句
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

    private void SyDevClass(DataSet dsDevClass)
    {
        int uAll = 0;
        int uADD = 0;
        int uAddSuccse = 0;
        int uAddFail = 0;
        int uSet = 0;
        int uSetSuccse = 0;
        int uSetFail = 0;
        DataTable dt = new DataTable();
        UNIDEVCLS[] devClass = GetAllDevCls();
        dt = dsDevClass.Tables[0];
        uAll = dt.Rows.Count;
        Logger.trace("房间类别个数" + dt.Rows.Count.ToString());
        for (int i = 0; i < dt.Rows.Count; i++)
        {
           
            string szDevClassSN = "";
            if (dt.Rows[i]["classroom_type_code"] != null)
            {
                szDevClassSN = dt.Rows[i]["classroom_type_code"].ToString();
            }
            string szDevClassName = "";
            if (dt.Rows[i]["classroom_type_name"] != null)
            {
                szDevClassName = dt.Rows[i]["classroom_type_name"].ToString();
            }
            if (szDevClassName == "")
            {
                szDevClassName = "空白";
            }
            UNIDEVCLS setDevClass = new UNIDEVCLS();
            bool bAdd = true;
            for (int m = 0; devClass != null && m < devClass.Length; m++)
            {
                if (devClass[m].szClassName.ToString() == szDevClassName)
                {
                    bAdd = false;
                    setDevClass = devClass[m];
                    break;
                }

            }
            if (bAdd)
            {
                UNIDEVCLS newDevClass = new UNIDEVCLS();
                newDevClass.szClassName = szDevClassName;
                newDevClass.szClassSN = szDevClassSN;
                newDevClass.dwKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
                string szClassNameTemp = newDevClass.szClassName.ToString();
                if (m_Request.Device.DevClsSet(newDevClass, out newDevClass) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uAddSuccse = uAddSuccse + 1;
                    Logger.trace("新建类别成功" + newDevClass.szClassName.ToString());
                    devClass = GetAllDevCls();
                }
                else
                {
                    uAddFail = uAddFail + 1;
                    Logger.trace("新建类别成功:" + szClassNameTemp + ":" + m_Request.szErrMessage.ToString());
                }
            }
            else
            {
                uSet = uSet + 1;
                setDevClass.szClassName = szDevClassName;
                setDevClass.szClassSN = szDevClassSN;
                if (m_Request.Device.DevClsSet(setDevClass, out setDevClass) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uSetSuccse = uSetSuccse + 1;
                    Logger.trace("更新类别成功" + szDevClassName);
                    devClass = GetAllDevCls();
                }
                else
                {
                    uSetFail = uSetFail + 1;
                    Logger.trace("更新类别成功:" + szDevClassName + ":" + m_Request.szErrMessage.ToString());
                }
            }
        }
        Logger.trace("类别总共需同步数据:" + uAll + ";新建数据" + uADD + "；新建成功:" + uAddSuccse + "；新建失败:" + uAddFail + "；更新总数据:" + uSet + "；更新成功:" + uSetSuccse + "；更新失败:" + uSetFail);
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
            string szHouseName = dt.Rows[i]["house_Name"].ToString();
            UNIBUILDING setBuilding = new UNIBUILDING();
            bool bAdd = true;
            for (int j = 0; j < allBuilding.Length; j++)
            {
                /*
                if (allBuilding[j].szBuildingName.ToString() == szHouseName)
                {
                    setBuilding = allBuilding[j];
                    bAdd = false;
                    break;
                }
                 * * */
                //下面是按照楼宇代码作为唯一
                
                if (allBuilding[j].szBuildingNo.ToString() == szHouseSN)
                {
                    setBuilding = allBuilding[j];
                    bAdd = false;
                    break;
                }
                 
            }
            string szCampID = dt.Rows[i]["campus_code"].ToString();
            string szCampName = dt.Rows[i]["campus_name"].ToString();
            bool isCamp = false;
            for (int k = 0; k < camp.Length; k++)
            {
                if (camp[k].szCampusName.ToString() == szCampName)
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
               // newBuliding.szCampusName = dt.Rows[i]["house_name"].ToString();
                newBuliding.dwCampusID = Parse(szCampID);
                UNIBUILDING newBulidingBak = new UNIBUILDING();
                newBulidingBak = newBuliding;
                newBulidingBak.szBuildingName = dt.Rows[i]["house_name"].ToString() +"(" + szCampName + ")";
                if (m_Request.Device.BuildingSet(newBuliding, out newBuliding) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uAddSuccse = uAddSuccse + 1;
                    Logger.trace("新建楼宇室成功" + newBuliding.szBuildingName.ToString());
                    allBuilding = getAllBuilding();
                }
                else
                {
                    uAddFail = uAddFail + 1;
                    Logger.trace("新建楼宇室失败:" + dt.Rows[i]["house_name"].ToString() + ":" + m_Request.szErrMessage.ToString());
                    if(m_Request.szErrMessage.ToString().IndexOf("已存在")>-1)
                    {
                        if (m_Request.Device.BuildingSet(newBulidingBak, out newBulidingBak) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            Logger.trace("再次新建楼宇室成功" + newBulidingBak.szBuildingName.ToString());
                        }
                        else
                        {
                            Logger.trace("再次新建楼宇室失败" + dt.Rows[i]["house_name"].ToString() + "(" + szCampName + ")");
                        }
                    }
                }
            }
            else
            {
                uSet = uSet + 1;
                string szHouseID = setBuilding.dwBuildingID.ToString();
                string szHouseCodeTemp = setBuilding.szBuildingNo.ToString();
                //楼宇按照名称来作为唯一字段
                setBuilding.szBuildingName = szHouseName;// dt.Rows[i]["house_name"].ToString();

               // setBuilding.szBuildingNo = dt.Rows[i]["house_code"].ToString();
                setBuilding.dwCampusID = Parse(szCampID);
                if (m_Request.Device.BuildingSet(setBuilding, out setBuilding) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uSetSuccse = uSetSuccse + 1;
                    Logger.trace("更新楼宇室成功" +setBuilding.szBuildingName.ToString());
                    allBuilding = getAllBuilding();
                }
                else
                {
                    uSetFail = uSetFail + 1;
                    Logger.trace("更新楼宇室失败:" + "ID号:" + szHouseID + ";编号:" + szHouseCodeTemp + "楼宇名称" + dt.Rows[i]["house_name"].ToString() + ":" + m_Request.szErrMessage.ToString());
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
           /* cmd.CommandText = @"select *
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
           
            */
            cmd.CommandText = @"select *
from  idc_u_dc.V_AST_ROOM_INFOR A
where ( a.HOUSE_CODE   in (
select  b.HOUSE_CODE
from  idc_u_dc.v_ast_house_infor b where house_code in ('4040'))) or ( room_type_code = 'F1' and  a.HOUSE_CODE   in (
select  b.HOUSE_CODE
from  idc_u_dc.v_ast_house_infor b where house_code in ('1001','1002','1003','2040','1004','2002','3001','3002','3005','4001','4002'))) or ((room_type_code in('F4','F5') and HOUSE_CODE not like '9%')
or(  room_type_code='F14' and HOUSE_CODE not like '9%'))
order by room_code ";
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
    private void SyRoom(DataSet dsTable)
    {
        int uAll = 0;
        int uUnSy = 0;
        int uADD = 0;
        int uAddSuccse = 0;
        int uAddFail = 0;
        int uSet = 0;
        int uSetSuccse = 0;
        int uSetFail = 0;
        DEVOPENRULE setOpenRule = new DEVOPENRULE();
        DEVOPENRULEREQ vrOpenRule = new DEVOPENRULEREQ();
        DEVOPENRULE[] devOpenRule;
        if (m_Request.Device.DevOpenRuleGet(vrOpenRule, out devOpenRule) == REQUESTCODE.EXECUTE_SUCCESS && devOpenRule != null && devOpenRule.Length > 0)
        {
            setOpenRule = devOpenRule[0];
        }
        else
        {
            Logger.trace("对应的开放规则不存在");
        }
        UNICAMPUS[] camp = GetAllCampus();
        UNIBUILDING[] allBuilding = getAllBuilding();
        UNIDEPT[] allDept = GetAllDept();
        UNIROOM[] allRoom = GetAllRoom();
        UNILAB[] allLab = GetAllLab();
        UNIDEVCLS[] allDevClass = GetAllDevCls();
        UNIDEVKIND[] allDevKind = GetAllDevKind();
        UNIDEVICE[] allDev;
        GetAllDev(out allDev);
        if (camp == null || camp.Length == 0)
        {
            Logger.trace("校区内容为空 不同步房间");
            return;
        }
        if (allLab == null || allLab.Length == 0)
        {
            Logger.trace("实验室内容为空 不同步房间");
            return;
        }
        if (allBuilding == null || allBuilding.Length == 0)
        {
            Logger.trace("楼宇内容为空 不同步房间");
            return;
        }
        if (allDevClass == null || allDevClass.Length == 0)
        {
            Logger.trace("楼宇类型为空 不同步房间");
            return;
        }
        DataTable dtRoom = new DataTable();
        dtRoom = dsTable.Tables[0];
        uAll = dtRoom.Rows.Count;
        Logger.trace("allroomList"+allRoom.Length);
        for (int op = 0; op < allRoom.Length; op++)
        {
            UNIROOM setRoom = new UNIROOM();
            setRoom=allRoom[op];
            if (setRoom.dwBuildingID != null&&setRoom.dwBuildingID.ToString()!=""&&setRoom.dwBuildingID.ToString()!="0")
            {
                continue;
            }
            Logger.trace("房间需要设置楼宇"+setRoom.szRoomName);
            string szRoomNameObj = setRoom.szRoomName;
            for (int i = 0; i < dtRoom.Rows.Count; i++)
            {
                string szRoomName = "";
                if (dtRoom.Rows[i]["room_name"] != null)
                {
                    szRoomName = dtRoom.Rows[i]["room_name"].ToString();
                }
                string szRoomNo = "";
                if (dtRoom.Rows[i]["room_no"] != null)
                {
                    szRoomNo = dtRoom.Rows[i]["room_no"].ToString();
                }
                if (!(szRoomName.IndexOf(szRoomNo) > -1))
                {
                    szRoomName = szRoomName + szRoomNo;
                }
                string szBulidingName="";
                if (szRoomNameObj == szRoomName)
                {
                    szBulidingName = dtRoom.Rows[i]["house_name"].ToString();
                }
                bool bRead = false;
                if (szBulidingName != "")
                {
                    Logger.trace("房间需要设置楼宇" + setRoom.szRoomName + szBulidingName);
                    for (int n = 0; n < allBuilding.Length; n++)
                    {
                        if (szBulidingName == allBuilding[n].szBuildingName.ToString())
                        {
                            Logger.trace("找到对应房间需要设置楼宇" + setRoom.szRoomName + szBulidingName);
                            setRoom.dwBuildingID = allBuilding[n].dwBuildingID;
                            m_Request.Device.RoomSet(setRoom, out setRoom);
                            Logger.trace("房间所在楼宇设置成功");
                            bRead = true;
                            break;
                        }
                    }
                }
                if (bRead)
                {
                    break;
                }
            }

        }
       // return;


        Logger.trace("dtRoom.Rows.Count.ToString()"+dtRoom.Rows.Count.ToString());
            for (int i = 0; i < dtRoom.Rows.Count; i++)
            {
                string szRoomName = "";
                if (dtRoom.Rows[i]["room_name"] != null)
                {
                    szRoomName = dtRoom.Rows[i]["room_name"].ToString();
                }
                string szRoomNo = "";
                if (dtRoom.Rows[i]["room_no"] != null)
                {
                    szRoomNo = dtRoom.Rows[i]["room_no"].ToString();
                }
                if (!(szRoomName.IndexOf(szRoomNo) > -1))
                {
                    szRoomName = szRoomName + szRoomNo;
                }
                string szRoomNameCamp = szRoomName + "(" + dtRoom.Rows[i]["campus_name"].ToString() + ")";
                bool bbread = false;
                for (int j = 0; j < allRoom.Length; j++)
                {
                    UNIROOM setRoom = new UNIROOM();

                    setRoom = allRoom[j];
                    //Logger.trace("房间所在楼宇设置已存在"+setRoom.szRoomName);
                    if (setRoom.dwBuildingID != null)
                    {
                        //Logger.trace("房间所在楼宇设置已存在");
                        continue;
                    }
                    if (GetRoomByName(szRoomName, out setRoom))
                    {
                        // Logger.trace("房间所在楼宇设置开始" + setRoom.szRoomName);

                        string szBuilding = dtRoom.Rows[i]["house_name"].ToString();
                        for (int n = 0; n < allBuilding.Length; n++)
                        {
                            if (szBuilding == allBuilding[n].szBuildingName.ToString())
                            {
                                Logger.trace("找到房间所在楼宇设置开始" + setRoom.szRoomName);
                                setRoom.dwBuildingID = allBuilding[n].dwBuildingID;
                                m_Request.Device.RoomSet(setRoom, out setRoom);
                                // Logger.trace("房间所在楼宇设置成功");
                                break;
                            }
                        }
                    }
                    else
                    {
                        //  szRoomName = szRoomName + "(" + dtRoom.Rows[i]["campus_name"].ToString() + ")";
                        string szBuilding = dtRoom.Rows[i]["house_name"].ToString();
                        if (GetRoomByName(szRoomNameCamp, out setRoom))
                        {
                            
                        }
                    }
                    if (bbread)
                    {
                        break;
                    }
                }
            }
        //return;
        //
        for (int i = 0; i < dtRoom.Rows.Count; i++)
        {
            string szCampName="";
            if(dtRoom.Rows[i]["campus_name"]!=null)
            {
                szCampName=dtRoom.Rows[i]["campus_name"].ToString();
            }
            string szDeptName = "";
            if (dtRoom.Rows[i]["department_name"] != null)
            {
                szDeptName = dtRoom.Rows[i]["department_name"].ToString();
            }
            if (szDeptName == "")
            {
                uUnSy = uUnSy + 1;
                Logger.trace("房间对应的实验室名称为空 不同步房间，第三方部门名称:" + dtRoom.Rows[i]["room_name"].ToString() + "保存为其他" + szDeptName);
                szDeptName = "上海财经大学";
                //continue;
            }
            string szBuidlName = "";
            if (dtRoom.Rows[i]["house_name"] != null)
            {
                szBuidlName = dtRoom.Rows[i]["house_name"].ToString();
            }
            if (szBuidlName == "")
            {
                uUnSy = uUnSy + 1;
                Logger.trace("房间对应的楼宇名称为空 不同步房间，第三方房间名称:" + szBuidlName);
                continue;
            }
            UNILAB setLab = new UNILAB();
            UNILAB setOtherLab = new UNILAB();
            for (int m = 0; m < allLab.Length; m++)
            {
                if (allLab[m].szLabName.ToString() == szDeptName)
                {
                    setLab = allLab[m];
                    break;
                }
                if (allLab[m].szLabName.ToString() == "其他")
                {
                    setOtherLab = allLab[m];
                }
            }
            if (setLab.dwLabID == null)
            {
                if (setOtherLab.dwLabID != null)
                {
                    setLab = setOtherLab;
                }
                else
                {
                    /*
                    UNILAB tempLab;
                    if (GetLabByID(100661724, out tempLab))
                    {
                        uUnSy = uUnSy + 1;
                        Logger.trace("房间对应的实验室不存在 不同步房间，第三方部门名称:" + szDeptName);
                        setLab = tempLab;
                    }
                   */
                    uUnSy = uUnSy + 1;
                    Logger.trace("房间对应的实验室不存在 不同步房间，第三方部门名称:" + szDeptName);
                    continue;
                }
            }
            UNIBUILDING setBuliding = new UNIBUILDING();
            for (int m = 0; m < allBuilding.Length; m++)
            {
                if (allBuilding[m].szBuildingName.ToString() == szBuidlName)
                {
                    setBuliding = allBuilding[m];
                    break;
                }
            }
            if (setBuliding.dwBuildingID == null)
            {
                uUnSy = uUnSy + 1;
                Logger.trace("房间对应的楼宇名称为空 不同步房间，第三方房间名称:" + szBuidlName);
                continue;
            }
            string szDevClssName = "";
            if (dtRoom.Rows[i]["classroom_type_name"] != null)
            {
                if (dtRoom.Rows[i]["classroom_type_name"].ToString() == "")
                {
                    szDevClssName = "空白";
                }
                else
                {
                    szDevClssName = dtRoom.Rows[i]["classroom_type_name"].ToString();
                }
            }
            UNIDEVCLS setDevClass = new UNIDEVCLS();
            for (int m = 0; m < allDevClass.Length; m++)
            {
                if (allDevClass[m].szClassName.ToString() == szDevClssName)
                {
                    setDevClass = allDevClass[m];
                    break;
                }
            }
            if (setDevClass.dwClassID == null)
            {
                uUnSy = uUnSy + 1;
                Logger.trace("房间对应的类型不存在 不同步房间:" + szDevClssName);
                continue;
            }
           
            string szRoomName = "";
            if (dtRoom.Rows[i]["room_name"] != null)
            {
                szRoomName = dtRoom.Rows[i]["room_name"].ToString();
            }
			
			 uint uMaxUser = 1;
            if (dtRoom.Rows[i]["seat_amt"] != null)
            {
                uMaxUser = Parse(dtRoom.Rows[i]["seat_amt"].ToString());
            }
            string szRoomNo = "";
            if (dtRoom.Rows[i]["room_no"] != null)
            {
                szRoomNo = dtRoom.Rows[i]["room_no"].ToString();
            }
            if (!(szRoomName.IndexOf(szRoomNo) > -1))
            {
                szRoomName = szRoomName + szRoomNo;
            }
            UNIDEVKIND setDevKind = new UNIDEVKIND();
            UNIDEVKIND setDevKindCampName = new UNIDEVKIND();
            bool bAddKind = true;
            bool bAddKindCampName = true;
            for (int m = 0; allDevKind!=null&&m < allDevKind.Length; m++)
            {
                if (allDevKind[m].szProducer == dtRoom.Rows[i]["room_code"].ToString())
                {
                    for (int k = 0; k < allDevKind.Length; k++)
                    {
                        if (allDevKind[k].szKindName == szRoomName)
                        {
                            bAddKind = false;
                            bAddKindCampName = false;
                            setDevKind = allDevKind[k];
                            break;
                        }
                    }
                    
                }
            }
            if (bAddKind)
            {
                    setDevKind.dwClassID = setDevClass.dwClassID;
                    setDevKind.dwMaxUsers = uMaxUser;
                    setDevKind.dwMinUsers = 1;
                    setDevKind.szKindName = szRoomName;
                    setDevKind.szProducer = dtRoom.Rows[i]["room_code"].ToString();
                    setDevKind.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
                    setDevKindCampName = setDevKind;
                    if (m_Request.Device.DevKindSet(setDevKind, out setDevKind) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        //uAddSuccse = uAddSuccse + 1;
                        Logger.trace("房间对应的类型新建成功：" + szRoomName);
                        allDevKind = GetAllDevKind();
                    }
                    else
                    {
                       // uAddFail = uAddFail + 1;
                        Logger.trace("房间对应的类型新建失败：" + m_Request.szErrMessage.ToString());
                        if (m_Request.szErrMessage.ToString().IndexOf("已存在") > -1)
                        {
                            szRoomName=szRoomName+ "(" + szCampName + ")";
                            setDevKind.szKindName = szRoomName;
                            if (m_Request.Device.DevKindSet(setDevKindCampName, out setDevKindCampName) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                Logger.trace("房间对应的类型新建成功-添加校区名：" + szRoomName);
                                allDevKind = GetAllDevKind();
                            }
                            else
                            {
                                Logger.trace("房间对应的类型新建失败-添加校区名：" + szRoomName);
                                continue;
                            }
                        }
                    }
            }
            else
            {
                if (!((((uint)setDevKind.dwProperty) & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE) > 0))
                {
                    if (uMaxUser > 0)
                    {
                        setDevKind.dwMaxUsers = uMaxUser;
                    }
                    else
                    {
                        setDevKind.dwMaxUsers = 1;
                    }
                    setDevKind.dwMinUsers = 1;
					setDevKind.dwMaxUsers = uMaxUser;
                    setDevKind.szProducer = dtRoom.Rows[i]["room_code"].ToString();
                    if (m_Request.Device.DevKindSet(setDevKind, out setDevKind) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Logger.trace(setDevKind.dwKindID+"房间类型修改成功：" +setDevKind.dwMaxUsers+ szRoomName);
						
                        allDevKind = GetAllDevKind();
                    }
                    else
                    {
                        Logger.trace("房间类型修改失败：" +m_Request.szErrMessage+ szRoomName);
                    }
                }
                else
                {
                    setDevKind.dwMinUsers = 1;
                    setDevKind.szProducer = dtRoom.Rows[i]["room_code"].ToString();
					
                    setDevKind.szKindName = szRoomName;
                    if (m_Request.Device.DevKindSet(setDevKind, out setDevKind) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Logger.trace("房间类型修改成功：" + szRoomName);
                        allDevKind = GetAllDevKind();
                    }
                    else
                    {
                        Logger.trace("房间类型修改失败：" + m_Request.szErrMessage + szRoomName);
                    }
                }
              //  uSet = uSet + 1;

            }
            string szRoomCode = "";
            if (dtRoom.Rows[i]["room_code"] != null)
            {
                szRoomCode = dtRoom.Rows[i]["room_code"].ToString();
            }
            uint uRoomSize = 0;
            if (dtRoom.Rows[i]["build_area"] != null)
            {
                uRoomSize = Parse(dtRoom.Rows[i]["build_area"].ToString());
            }
            UNIROOM newRoom = new UNIROOM();
            UNIROOM newRoomCampName = new UNIROOM();
            bool bNewRoom = true;
            UNIROOM bExistRoom = new UNIROOM();
            for (int k = 0; k < allRoom.Length; k++)
            {
                if (allRoom[k].szRoomNo == szRoomCode)
                {
                    //if (allRoom[k].szRoomNo == szRoomCode)
                    {
                        bExistRoom = allRoom[k];
                        bNewRoom = false;
                        break;
                    }
                }
            }
            if (!bNewRoom)
            {
                uint uOldBuilding=(uint)bExistRoom.dwBuildingID;
                string szOldName= bExistRoom.szRoomName;
                if (bExistRoom.dwBuildingID != setBuliding.dwBuildingID)
                {
                    bExistRoom.dwBuildingID = setBuliding.dwBuildingID;
                    if (m_Request.Device.RoomSet(bExistRoom, out bExistRoom) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Logger.trace("房间所在楼宇变动" + uOldBuilding.ToString() + "改为" + setBuliding.dwBuildingID.ToString());
                        
                    }
                }
                if (bExistRoom.szRoomName != szRoomName)
                {
                    bExistRoom.szRoomName = szRoomName;
                    bExistRoom.dwBuildingID = setBuliding.dwBuildingID;
                    if (m_Request.Device.RoomSet(bExistRoom, out bExistRoom) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Logger.trace("房间名称变动" + szOldName.ToString() + "改为" + szRoomName.ToString());
                    }
                }

            }
            if (bNewRoom)
            {
                newRoom.szRoomName = szRoomName;
                newRoom.dwBuildingID = setBuliding.dwBuildingID;
                //newRoom.dwCampusID = setBuliding.dwCampusID;
                newRoom.dwCampusID = Parse(dtRoom.Rows[i]["CAMPUS_CODE"].ToString());
                newRoom.dwLabID = setLab.dwLabID;
               
                newRoom.dwRoomSize = uRoomSize;
                newRoom.szRoomNo = szRoomCode;
                DEVOPENRULE newOpenRule = new DEVOPENRULE();
                if (newOpenRuleSN(setOpenRule, out newOpenRule,szRoomName))
                {
                    newRoom.dwOpenRuleSN = newOpenRule.dwRuleSN;
                }
                else
                {
                    Logger.trace("开放规则新建成功：" + m_Request.szErrMessage);
                    newRoom.dwOpenRuleSN = setOpenRule.dwRuleSN;
                }
                UNIGROUP manGroup = new UNIGROUP();
                if (NewGroup(szRoomName, (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out manGroup))
                {
                    Logger.trace("房间对应的管理员新建成功：");
                    newRoom.dwManGroupID = manGroup.dwGroupID;
                }
                else
                {
                    Logger.trace("房间对应的管理员新建失败" + "；" +m_Request.szErrMessage.ToString());
                    continue;
                }
                newRoomCampName = newRoom;
                
                if (m_Request.Device.RoomSet(newRoom, out newRoom) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Logger.trace("房间新建成功" + szRoomName);
                    allRoom = GetAllRoom();
                }
                else
                {
                    uAddFail = uAddFail + 1;
                    Logger.trace("房间新建失败：" + szRoomName + "；" + m_Request.szErrMessage.ToString());
                    szRoomName=szRoomName+ "(" + szCampName + ")";
                    newRoomCampName.szRoomName =szRoomName;
                    if (m_Request.szErrMessage.IndexOf("已存在") > -1)
                    {
                        if (m_Request.Device.RoomSet(newRoomCampName, out newRoomCampName) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            Logger.trace("房间新建成功" + szRoomName);
                            allRoom = GetAllRoom();
                        }
                        else
                        {
                            Logger.trace("房间新建失败：" + szRoomName + "；" + m_Request.szErrMessage.ToString());
                           
                            continue;
                        }

                    }
                }
            }
            GetAllDev(out allDev);
            UNIDEVICE setDev = new UNIDEVICE();
            bool bAddDev = true;
            for (int m = 0; m < allDev.Length; m++)
            {
                if (allDev[m].dwDevSN.ToString() == szRoomCode)
                {
                    setDev = allDev[m];
                    bAddDev = false;
                    break;
                }
            }
            if (bAddDev)
            {
                setDev.szDevName = szRoomName;
                setDev.dwRoomID = newRoom.dwRoomID;
                setDev.dwKindID = setDevKind.dwKindID;
                uint uDevSN = Parse(szRoomCode);
                if (uDevSN == 0)
                {
                    uDevSN = (uint)newRoom.dwRoomID;
                }
                setDev.dwDevSN = uDevSN;
                setDev.szAssertSN = uDevSN.ToString();
                if (setDev.dwRoomID == null || setDev.dwRoomID == 0)
                {
                    Logger.trace("房间编号为空或者0：" + setDev.szDevName.ToString());
                    continue;
                }
                if (m_Request.Device.Set(setDev, out setDev) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uAddSuccse = uAddSuccse + 1;
                    Logger.trace("房间新建成功：" + setDev.szDevName.ToString());
                }
                else
                {
                    uAddFail=uAddFail+1;
                    Logger.trace("房间新建失败：" + szRoomName + m_Request.szErrMessage.ToString());
                }
            }
            else {
                setDev.dwRoomID = newRoom.dwRoomID;
                setDev.dwKindID = setDevKind.dwKindID;
                uint uDevSN = Parse(szRoomCode);
                if (uDevSN == 0)
                {
                    uDevSN = (uint)newRoom.dwRoomID;
                }
                setDev.dwDevSN = uDevSN;
                setDev.szDevName = szRoomName;
                setDev.szAssertSN = uDevSN.ToString();
                if (m_Request.Device.Set(setDev, out setDev) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uSetSuccse = uSetSuccse + 1;
                    Logger.trace("房间更新成功：" + setDev.szDevName.ToString());
                }
                else
                {
                    uSetFail = uSetFail + 1;
                    Logger.trace("房间更新失败：" + m_Request.szErrMessage.ToString());
                }
            }
        }
        Logger.trace("房间总共需同步数据:" + uAll + ";新建数据" + uADD + "；新建成功:" + uAddSuccse + "；新建失败:" + uAddFail + "；更新总数据:" + uSet + "；更新成功:" + uSetSuccse + "；更新失败:" + uSetFail);
    }
    private bool newOpenRuleSN(DEVOPENRULE openRule,out DEVOPENRULE returnOpenRule,string szRoomName)
    {
        returnOpenRule = new DEVOPENRULE();
        GROUPOPENRULE[] openrule = openRule.GroupOpenRule;
        returnOpenRule.szRuleName = szRoomName;
        openrule[0].szGroup.dwGroupID = 0;
        returnOpenRule.GroupOpenRule = openrule;
        if (m_Request.Device.DevOpenRuleSet(returnOpenRule, out returnOpenRule) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        else
        {
            Logger.trace("新建开放规则失败");
            return false;
        }
    }
    
}
