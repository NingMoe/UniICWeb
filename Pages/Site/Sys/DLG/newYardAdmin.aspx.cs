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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string szManRole = "";
    protected string m_szRoom = "";
    protected string m_szLab = "";
    protected string m_checkLab = "";
    protected string m_adminKind = "";
    protected string m_adminLevle= "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            DEVKINDREQ yardReq = new DEVKINDREQ();
            if (Request["dwLabID"] != null)
            {
                yardReq.dwExtRelatedID = Parse(Request["dwLabID"].ToString());
                UNIDEVKIND[] vtYard = new UNIDEVKIND[1];
                if (m_Request.Device.DevKindGet(yardReq, out vtYard) == REQUESTCODE.EXECUTE_SUCCESS && vtYard != null && vtYard.Length > 0)
                {
                   
                   UNIADMIN newAdmin;
                   GetHTTPObj(out newAdmin);
                   uint uManroleTemp = 0;
                   uint uRoleArea = 0;
                   uint ucheck = 0;
                   uint uProperty = (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN;// uProperty CharListToUint(Request["dwProperty"]);
                   /*
                   if ((uProperty & 536870912) > 0)
                   {
                       uManroleTemp=(uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER;//领导
                       uRoleArea = (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION;
                       //uProperty = uProperty - 536870912;
                   }
                   else{
               
                   }
                   if ((uProperty & 1073741824) > 0)
                   {
                       //uProperty = uProperty - 1073741824;
                   }
                   else {
                       ucheck = (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_EANDA;
                   }
                   */
                   newAdmin.dwProperty = uProperty;

                   uManroleTemp = (uint)ADMINLOGINRES.DWMANROLE.MANROLE_OPERATOR;
                   uRoleArea = (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM;

                   if (!((newAdmin.dwProperty & (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS) > 0))
                   {
                       newAdmin.dwProperty = newAdmin.dwProperty | (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS;
                   }
                   newAdmin.dwManRole = uRoleArea + uManroleTemp + ucheck + (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_ADMIN;
                   if (m_Request.Admin.Set(newAdmin, out newAdmin) != REQUESTCODE.EXECUTE_SUCCESS)
                   {
                       MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                       return;
                   }
                   else
                   {
                      
                       UNIACCOUNT setAcount;
                       if (GetAccByAccno(newAdmin.dwAccNo.ToString(), out setAcount))
                       {
                           setAcount.szHandPhone = newAdmin.szHandPhone;
                           m_Request.Account.Set(setAcount, out setAcount);
                       }
                       ArrayList list = new ArrayList();
                       if (vtYard != null)
                       {
                           string szDevKinds = "";
                           for (int i = 0; i < vtYard.Length; i++)
                           {
                               szDevKinds += vtYard[i].dwKindID.ToString() + ",";
                               if (((i + 1) % 10) == 0)
                               {
                                   if (szDevKinds.EndsWith(","))
                                   {
                                       szDevKinds = szDevKinds.Substring(0, szDevKinds.Length - 1);
                                   }
                                   DEVREQ devReq = new DEVREQ();
                                   devReq.szKindIDs = szDevKinds;
                                   UNIDEVICE[] vtDev;
                                   if (m_Request.Device.Get(devReq, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null && vtDev.Length > 0)
                                   {
                                       for (int m = 0; m < vtDev.Length; m++)
                                       {
                                           UNIROOM setRoom;
                                           if (GetRoomID(vtDev[m].dwRoomID.ToString(), out setRoom))
                                           {
                                               AddGroupMember(setRoom.dwManGroupID, newAdmin.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                                           }
                                       }
                                       szDevKinds = "";
                                   }
                               }
                           }
                          
                       }
                       MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                       return;
                     
                   }


                }
                }
                else
                {
                    MessageBox("获取不到该场景对应的房间", "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
            }

           
        {   
            m_Title = "新建场景管理员";
        }
	}
}