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

public partial class _Default : UniWebLib.UniPage
{
	protected string m_szSecData = "[]";
	protected uint m_dwRoomID = 0xffffffff;
	protected string m_szDate = "";
	protected string m_szResvInfo = "";
	protected string m_szRoomName = "";

	string GetSecName(CLASSTIMETABLE[] vtSec, uint dwTime)
	{
		for (int i = 0; i < vtSec.Length; i++)
		{
			if (vtSec[i].dwBeginTime <= dwTime && dwTime <= vtSec[i].dwEndTime)
			{
				return vtSec[i].szSecName;
			}
		}
		return "";
	}

	bool GetSecTime(CLASSTIMETABLE[] vtSec, ref uint? dwBeginSec,ref uint? dwEndSec, out uint? dwBeginTime, out uint? dwEndTime)
	{
		dwBeginTime = 0;
		dwEndTime = 0;
		int dwBegin_I = -1;
		int dwEnd_I = -1;
		for (int i = 0; i < vtSec.Length; i++)
		{
			if (vtSec[i].dwSecIndex == dwBeginSec)
			{
				dwBegin_I = i;
			}
			if (vtSec[i].dwSecIndex == dwEndSec)
			{
				dwEnd_I = i;
			}
		}
		if (dwBegin_I == -1 || dwEnd_I == -1)
		{
			return false;
		}
		dwBeginTime = vtSec[dwBegin_I].dwBeginTime;
		dwEndTime = vtSec[dwEnd_I].dwEndTime;
		dwBeginSec = (uint)dwBegin_I; //TODO:
		dwEndSec = (uint)dwEnd_I;//TODO:
		return true;
	}

	void DelSecSN(CLASSTIMETABLE[] vtSec, uint dwBeginTime, uint dwEndTime)
	{
		for (int i = 0; i < vtSec.Length; i++)
		{
			if (vtSec[i].dwBeginTime > dwEndTime || dwBeginTime > vtSec[i].dwEndTime)
			{
			}else
			{
				vtSec[i].dwSN = 0;
			}
		}
	}

	CLASSTIMETABLE[] vtSec;

	protected void Page_Load(object sender, EventArgs e)
	{
		m_bRemember = false;

		{
			ResvTable.Height = 100;
			ResvTable.ShowWeek = false;
			ResvTable.RoomID = 0xffffffff;

			m_szDate = Request["dwDate"];
			if (m_szDate == null || m_szDate == "")
			{
				MessageBox("无效的日期", "无法创建预约", MSGBOX.ERROR,MSGBOX_ACTION.CANCEL);
				return;
			}
			DateTime dtDate;
			DateTime.TryParse(Request["dwDate"], out dtDate);
			m_szDate = dtDate.ToShortDateString().Replace("/", "-");


			uint dwRoomID = 0;
			uint.TryParse(Request["RoomID"], out dwRoomID);
			if (dwRoomID == 0)
			{
				MessageBox("无效的房间", "无法创建预约", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
				return;
			}
			m_dwRoomID = dwRoomID;
			ResvTable.SetDate = m_szDate;
			ResvTable.RoomID = m_dwRoomID;


			//-------------------选择节次--------------------
			//过滤已被预约的时间段
            /*

			REQUESTCODE ret1 = m_Request.Web.GetClassTimeTable(out vtSec);
			if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
			{
				ROOMRESVSTATREQ vrRoomStatReq = new ROOMRESVSTATREQ();
				ROOMRESVSTAT[] vtRoomStat;
				vrRoomStatReq.dwRoomID = m_dwRoomID;
				vrRoomStatReq.dwDate = GetDate(m_szDate);
				REQUESTCODE ret2 = m_Request.Web.GetRoomResvStat(vrRoomStatReq, out vtRoomStat);
				if (ret2 == REQUESTCODE.EXECUTE_SUCCESS && vtRoomStat != null && vtRoomStat.Length > 0)
				{
					m_szRoomName = vtRoomStat[0].szRoomName;
					TEACHINGRESVINFO[] ResvInfo = vtRoomStat[0].szResvInfo;
					m_szResvInfo += "<ul class='resv'>";
					for (int i = 0; i < ResvInfo.Length; i++)
					{
						DelSecSN(vtSec, (uint)ResvInfo[i].dwBeginTime, (uint)ResvInfo[i].dwEndTime);

						m_szResvInfo += "<li><div class='sec'>" + GetSecName(vtSec, (uint)ResvInfo[i].dwBeginTime) + "至" + GetSecName(vtSec, (uint)ResvInfo[i].dwEndTime) + "</div>";
						string szText = "";
						if (!string.IsNullOrEmpty(ResvInfo[i].szTeacherName))
						{
							szText += ResvInfo[i].szTeacherName + ",";
						}
						if (!string.IsNullOrEmpty(ResvInfo[i].szResvName))
						{
							szText += "" + ResvInfo[i].szResvName + " ";
						}
						else if (!string.IsNullOrEmpty(ResvInfo[i].szGroupName))
						{
							szText += ResvInfo[i].szGroupName + " ";
						}
						else if (!string.IsNullOrEmpty(ResvInfo[i].szCourseName))
						{
							szText += ResvInfo[i].szCourseName + " ";
						}
						m_szResvInfo += "<div class='memo'>" + szText + "</div>";
						m_szResvInfo += "</li>";
					}
					m_szResvInfo += "</ul>";
				}

				m_szSecData = "[";
				for (int i = 0; i < vtSec.Length; i++)
				{
					if (vtSec[i].dwSN == 0)
					{
						continue;
					}
					string item = "{i:'" + vtSec[i].dwSecIndex + "',v:'" + vtSec[i].szSecName + "'}";

					m_szSecData += "[" + item;
					for (int j = i; j < vtSec.Length; j++)
					{
						if (vtSec[j].dwSN == 0)
						{
							break;
						}
						string itemend = "{i:'" + vtSec[j].dwSecIndex + "',v:'" + vtSec[j].szSecName + "'}";
						m_szSecData += "," + itemend;
					}
					m_szSecData += "],";
				}
				if (m_szSecData.EndsWith(","))
				{
					m_szSecData = m_szSecData.Substring(0, m_szSecData.Length - 1);
				}
				m_szSecData += "]";
			}*/
		}
	}

	uint NewGroupFromClient()
	{
		string szGroup = Request["GroupList"];
		if (string.IsNullOrEmpty(szGroup))
		{
			MessageBox("班级组不能为空", "创建预约失败", MSGBOX.ERROR);
			return 0;
		}
		UNIGROUP vrGroup = new UNIGROUP();
		vrGroup.szName = ""+DateTime.Now.Ticks;
        if (m_Request.Group.SetGroup(vrGroup, out vrGroup) != REQUESTCODE.EXECUTE_SUCCESS || vrGroup.dwGroupID == 0)
		{
			MessageBox("创建预约班级组失败", "创建预约失败", MSGBOX.ERROR);
			return 0;
		}


		string[] arrayGroupName = Request["GroupListName"].Split(new char[] { ',' });
		string[] arrayGroup = szGroup.Split(new char[] { ',' });
		for (int i = 0; i < arrayGroup.Length; i++)
		{
			uint nClsID = 0;
			uint.TryParse(arrayGroup[i], out nClsID);
			if (nClsID != 0)
			{
				GROUPMEMBER vrGrpMember = new GROUPMEMBER();
                vrGrpMember.dwGroupID = vrGroup.dwGroupID;
				vrGrpMember.dwMemberID = nClsID;
				vrGrpMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS;
				vrGrpMember.szName = arrayGroupName[i];
                if (m_Request.Group.SetGroupMember(vrGrpMember) != REQUESTCODE.EXECUTE_SUCCESS)
				{
                    m_Request.Group.DelGroup(vrGroup); 
					MessageBox("设置预约班级组失败", "创建预约失败", MSGBOX.ERROR);
					return 0;
				}
			}
		}
        return (uint)vrGroup.dwGroupID;
	}

	protected void Button_OK_Click(object sender, EventArgs e)
	{
		if (Session["SessionID"] == null)
		{
			Response.Redirect("Login.aspx");
		}

		uint.TryParse(Request["RoomID"], out m_dwRoomID);
		if (m_dwRoomID == 0)
		{
			MessageBox("无效的房间", "创建预约失败", MSGBOX.ERROR);
			return;
		}

        ADMINLOGINRES loginres = (ADMINLOGINRES)Session["LoginResult"];
	 
		UNIRESERVE vrParameter = new UNIRESERVE();
		GetHTTPObj(out vrParameter);

		if (string.IsNullOrEmpty(vrParameter.szTestName))
		{
			MessageBox("课程名不能为空", "创建预约失败", MSGBOX.ERROR);
			return;
		}

        vrParameter.ResvDev = new RESVDEV[1];
        vrParameter.ResvDev[0].szRoomNo = m_dwRoomID.ToString();
		vrParameter.dwOwner = loginres.AdminInfo.dwAccNo;
        vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
		vrParameter.dwMemberID = NewGroupFromClient();
        if (vrParameter.dwMemberID == 0)
		{
			return;
		}

		REQUESTCODE ret1 = m_Request.Reserve.Set(vrParameter, out vrParameter);
		if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
		{
			MessageBox("预约成功", "预约成功", MSGBOX.SUCCESS,"RoomList.aspx?dwDate="+Request["dwDate"]);
		}
		else
		{
			MessageBox("预约失败," + m_Request.szErrMessage, "预约失败", MSGBOX.ERROR);
		}
		PutJSObj(vrParameter);
	}
}
