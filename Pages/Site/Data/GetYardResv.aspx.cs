using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Newtonsoft.Json;
using UniWebLib;
using System.Data.OracleClient;
public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string m_szDev = "[";
        Response.CacheControl = "no-cache";
        string szStart = Request["start"];
        szStart = szStart.Replace("-","");
        string szEnd = Request["end"];
        szEnd = szEnd.Replace("-", "");

        string szCamp=Request["campuid"];
        string szBuildingID=Request["buildingid"];
        string szRoomID = Request["roomid"];

        string GetResvID = "";
        /*
        YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
        vrPar.dwBeginDate = Parse(szStart);
        vrPar.dwEndDate = Parse(szEnd);
        vrPar.dwNeedYardResv = 1;
     //   vrPar.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO + (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK + +(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
        YARDRESVCHECKINFO[] vtRes;
        if (m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
               YARDRESV yaresev=vtRes[i].YardResv;
                string szTime ="";
               if (yaresev.dwBeginTime != null)
               {
                   szTime = Get1970Date(yaresev.dwBeginTime, "HH:mm") + Get1970Date(yaresev.dwEndTime, "到HH:mm");
               }
               string szDevName = "";
               if (yaresev.szDevName != null)
               {
                   szDevName = yaresev.szDevName;
               }
               string szApplicantName = "";
               if (yaresev.szApplicantName != null)
               {
                   szApplicantName = yaresev.szApplicantName;
               }
               string szActivityName = "";
               if (yaresev.szActivityName != null)
               {
                   szActivityName = yaresev.szActivityName;
               }
               string dwPreDate = "";
               if (yaresev.dwPreDate != null)
               {
                   dwPreDate = GetDateStr(yaresev.dwPreDate);
               }
              uint dwSecurityLevel=0;
              if (yaresev.dwSecurityLevel != null)
              {
                  dwSecurityLevel = (uint)yaresev.dwSecurityLevel;
              }
              m_szDev += "{\"dwSecurityLevel\":" + dwSecurityLevel.ToString()+ ",\"id\":" + vtRes[i].dwCheckID.ToString() + ",\"title\": \"" + szTime + "," + szDevName + "," + szApplicantName + "," + szActivityName + "\",\"start\": \"" + dwPreDate + "\"},";

            }
        }


      */
        YARDRESVREQ vrGet = new YARDRESVREQ();
        if (!string.IsNullOrEmpty(szCamp)&&szCamp!="0")
        {
            vrGet.szCampusIDs = szCamp;
        }
        if (!string.IsNullOrEmpty(szBuildingID) && szBuildingID != "0")
        {
            vrGet.szBuildingIDs = szBuildingID;
        }
        if (!string.IsNullOrEmpty(szRoomID) && szRoomID != "0")
        {
            vrGet.szRoomIDs = szRoomID;
        }
        vrGet.dwBeginDate = Parse(szStart);
        vrGet.dwEndDate = Parse(szEnd);
        vrGet.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
           // Logger.trace("yarresv count="+vtRes.Length.ToString());
            for (int i = 0; i < vtRes.Length; i++)
            {
                GetResvID = GetResvID + vtRes[i].dwResvID.ToString() + ",";
                string szTime = Get1970Date(vtRes[i].dwBeginTime, "HH:mm") + Get1970Date(vtRes[i].dwEndTime, "到HH:mm");
                YARDRESV yaresev = vtRes[i];

                string szIsTody = "false";
                if (Get1970Date(vtRes[i].dwCheckTime, "yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    szIsTody = "true";
                }

                if (yaresev.dwBeginTime != null)
                {
                    szTime = Get1970Date(yaresev.dwBeginTime, "HH:mm") + Get1970Date(yaresev.dwEndTime, "到HH:mm");
                }
                string szDevName = "";
                if (yaresev.szDevName != null)
                {
                    szDevName = yaresev.szDevName;
                }
                string szApplicantName = "";
                if (yaresev.szApplicantName != null)
                {
                    szApplicantName = yaresev.szApplicantName;
                }
                string szActivityName = "";
                if (yaresev.szActivityName != null)
                {
                    szActivityName = yaresev.szActivityName;
                }
                string dwPreDate = "";
                if (yaresev.dwPreDate != null)
                {
                    dwPreDate = GetDateStr(yaresev.dwPreDate);
                }
                uint dwSecurityLevel = 0;
                if (yaresev.dwSecurityLevel != null)
                {
                    dwSecurityLevel = (uint)yaresev.dwSecurityLevel;
                }
              //  Logger.trace("szDevName=" + szDevName);


                m_szDev += "{\"dwSecurityLevel\":" + dwSecurityLevel.ToString() + ",\"id\":" + vtRes[i].dwResvID.ToString() + ",\"title\": \"" + szDevName + "," + szTime + "," + szApplicantName + "," + szActivityName + "," + szIsTody + "\",\"start\": \"" + dwPreDate + "\",\"istody\": \"" + szIsTody + "\"},";

            }
        }
        //Logger.trace("yardresv=" + m_szDev);
        RESVREQ getResv = new RESVREQ();
        getResv.dwBeginDate= Parse(szStart);
        getResv.dwEndDate=Parse(szEnd);
        getResv.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
        getResv.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        getResv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
            //(uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        UNIRESERVE[] vtResv;
        if (m_Request.Reserve.Get(getResv, out vtResv) == REQUESTCODE.EXECUTE_SUCCESS && vtResv != null && vtResv.Length > 0)
        {
            
            for (int i = 0; i < vtResv.Length; i++)
            {
                if (GetResvID.IndexOf(vtResv[i].dwResvID.ToString()) > -1)
                {
                    continue;
                }
                GetResvID = GetResvID + vtResv[i].dwResvID.ToString() + ",";
                RESVDEV[] vtResvDev = vtResv[i].ResvDev;
                if (vtResvDev != null && vtResvDev.Length > 0)
                {
                    UNIDEVICE devRes=new UNIDEVICE();

                    if (GetDevInfo(vtResvDev[0].dwDevStart.ToString(), vtResvDev[0].dwRoomID.ToString(), out devRes) == true)
                    {

                        if (!string.IsNullOrEmpty(szCamp) && szCamp != "0" && devRes.dwCampusID.ToString() != szCamp)
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(szBuildingID) && szBuildingID != "0" && devRes.dwBuildingID.ToString() != szBuildingID)
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(szRoomID) && szRoomID != "0" && devRes.dwRoomID.ToString() != szRoomID)
                        {

                            continue;
                        }
                    }
                    else
                    {
                     //   Logger.trace("resvid="+ vtResv[i].dwResvID.ToString() + ";devsn="+vtResvDev[0].dwDevStart.ToString()+";roomid="+vtResvDev[0].dwRoomID.ToString()+"不存在"+"");
                        continue;
                    }
                }
                else
                {
                    continue;
                }
                if ((((uint)vtResv[i].dwPurpose) & ((uint)UNIRESERVE.DWPURPOSE.USEFOR_YARD)) > 0)
                {
                    continue;
                }
                string szRoomName="";
                if(vtResv[i].ResvDev!=null&&vtResv[i].ResvDev.Length>0)
                {
                    szRoomName=vtResv[i].ResvDev[0].szRoomName;
                }
                string szTime = Get1970Date(vtResv[i].dwBeginTime, "HH:mm") + Get1970Date(vtResv[i].dwEndTime, "到HH:mm");
                string szDate = GetDateStr(vtResv[i].dwPreDate);
                string szIsTody = "false";
                if (Get1970Date(vtResv[i].dwCheckTime, "yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    szIsTody = "true";
                }
                szDate = szDate.Replace("/","-");
                m_szDev += "{\"title\": \"" + szRoomName + "," + szTime + "," + vtResv[i].szOwnerName + "," + vtResv[i].szTestName + "\",\"start\": \"" + szDate + "\",\"id\": \"" + vtResv[i].dwResvID + "\",\"istody\": \"" + szIsTody + "\"}";
              
                if (i < (vtResv.Length - 1))
                {
                    m_szDev += ",";
                }
             
            }
        }

       // Logger.trace("reserve=" + m_szDev);
        if (m_szDev!="["&&!m_szDev.EndsWith(","))
        {
            m_szDev = m_szDev + ",";
        }
     //   GetThirdDevResv thirdResv=new GetThirdDevResv();

        UNIDEVICE[] devResv = (UNIDEVICE[])Session["devInfo"];
        ADMINLOGINRES loginRes = (ADMINLOGINRES)Session["LoginResult"];
        if (devResv == null)
        {
            UNIDEVICE[] devResvRes;
            if (Session["devInfo"] == null)
            {
                DEVREQ devReqGet = new DEVREQ();

                if (m_Request.Device.Get(devReqGet, out devResvRes) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    devResv = devResvRes;

                }
                else
                {
 
                }
            }
        }

        if (GetResvID.EndsWith(","))
        {
            GetResvID = GetResvID.Substring(0,GetResvID.Length-1);
        }

        string szThirdResvInfo =GetResvInfo(szStart, szEnd, szCamp, szBuildingID, szRoomID, devResv, loginRes, GetResvID);
        // Logger.trace("szThirdResvInfo:"+szThirdResvInfo);
        szThirdResvInfo=szThirdResvInfo.Replace("<br />","");
         m_szDev = m_szDev + szThirdResvInfo;
        if (m_szDev.EndsWith(","))
        {
            m_szDev = m_szDev.Substring(0, m_szDev.Length - 1);
        }
      

        m_szDev+="]";
       // Logger.trace("thirdresv=" + m_szDev);
        Response.Write(m_szDev);
    }

    private bool GetDevInfo(string dwDevSN, string dwRoomID,out UNIDEVICE deviceRes)
    {
        deviceRes = new UNIDEVICE();
        UNIDEVICE[] devResv;
        if (Session["devInfo"] == null)
        {
            DEVREQ devReqGet = new DEVREQ();
           
            if (m_Request.Device.Get(devReqGet, out devResv) == REQUESTCODE.EXECUTE_SUCCESS && devResv.Length > 0)
            {
                Session["devInfo"] = devResv;
            }
        }
            devResv = (UNIDEVICE[])Session["devInfo"];
        
        for (int i = 0; i < devResv.Length; i++)
        {
            if (devResv[i].dwDevSN.ToString() == dwDevSN && devResv[i].dwRoomID.ToString() == dwRoomID)
            {
                deviceRes=devResv[i];
                return true;
            }
        }
        return false;

        

    }
    public string GetResvInfo(string szStart, string szEnd, string szCamp, string szBuildingID, string szRoomID, UNIDEVICE[] devInfo, ADMINLOGINRES loginRes, string GetResvID)
    {
        /*
        string szStart = Request["start"];
        szStart = szStart.Replace("-", "");
        string szEnd = Request["end"];
        szEnd = szEnd.Replace("-", "");

        string szCamp = Request["campuid"];
        string szBuildingID = Request["buildingid"];
        string szRoomID = Request["roomid"];
        */



        string ConnectionString = "Data Source=IDC_U_DC;user=idc_u_cs;password=idc_u_cs;";//写连接串
        OracleConnection conn = new OracleConnection(ConnectionString);//创建一个新连接
        string szRes = "";
        string szSql = "select a.*,b.devname from tbldevresv a,vwunidevice b where a.resvdate>=" + szStart + " and a.resvdate<=" + szEnd;
        if (!string.IsNullOrEmpty(szCamp) && szCamp != "0")
        {
            szSql = szSql + " and b.CampusID=" + szCamp;
        }
        if (!string.IsNullOrEmpty(szBuildingID) && szBuildingID != "0")
        {
            szSql = szSql + " and b.BuildingID=" + szBuildingID;
        }
        if (!string.IsNullOrEmpty(szRoomID) && szRoomID != "0")
        {
            szSql = szSql + " and b.RoomID=" + szRoomID;
        }
        szSql = szSql + " and a.devid=b.devid";
        if (GetResvID != "")
        {
            szSql = szSql + " and a.resvid not in(" + GetResvID + ")";
        }
        if (((uint)loginRes.AdminInfo.dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_OPERATOR) > 0)
        {
            string szDevID = "";
            if (devInfo != null)
            {
                for (int i = 0; i < devInfo.Length; i++)
                {
                    if (i < (devInfo.Length - 1))
                    {
                        szDevID = szDevID + devInfo[i].dwDevID.ToString() + ",";
                    }
                    else
                    {
                        szDevID = szDevID + devInfo[i].dwDevID.ToString();
                    }
                }
            }
            if (szDevID == "")
            {
                szDevID = "''";
            }
            szSql = szSql + " and a.devid in(" + szDevID + ")";
        }
        szSql = szSql + " and resvid not in(select resvid from TBLRESERVE where predate >= " + szStart + " and predate<= " + szEnd + " and bitand(status,2)= 0 or STATFLAG = 4)";
        Logger.trace("sql:" + szSql);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = szSql;
            cmd.Connection = conn;
            OracleDataAdapter adpt = new OracleDataAdapter(cmd);
            adpt.Fill(ds);
        }
        catch (Exception exp)
        {
            Logger.trace(exp.ToString());
            conn.Close();
            return szRes;
        }
        conn.Close();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        int uAll = dt.Rows.Count;
        Logger.trace("uAll:" + uAll);
        for (int i = 0; i < uAll; i++)
        {
            string resvid = dt.Rows[i]["RESVID"].ToString();
            uint uBegin = Parse(dt.Rows[i]["STARTHM"].ToString());
            uint uEnd = Parse(dt.Rows[i]["ENDHM"].ToString());
            string time = (uBegin / 100).ToString("00") + ":" + (uBegin % 100).ToString("00") + "到" + (uEnd / 100).ToString("00") + ":" + (uEnd % 100).ToString("00");
            string szResvTitle = dt.Rows[i]["devname"].ToString() + "<br />," + time + "<br />," + dt.Rows[i]["TrueName"].ToString() + "," + dt.Rows[i]["ResvTitle"].ToString();
            szResvTitle = szResvTitle.Replace("@＃", "<br />");

            string szDate = GetDateStr(Parse(dt.Rows[i]["resvdate"].ToString()));
            szDate = szDate.Replace("-", "/");
            //szRes += "{\"title\": \"" + szResvTitle + "\",\"start\": \"" + szDate + "\",\"istody\": \"" + "false" + "\"}";
            szRes += "{\"dwSecurityLevel\":" + "0" + ",\"id\":" + resvid + ",\"title\": \"" + szResvTitle + "," + "" + "\",\"start\": \"" + szDate + "\",\"istody\": \"" + "false" + "\"}";
            if (i < (dt.Rows.Count - 1))
            {
                szRes += ",";
            }
        }



        return szRes;
    }


}