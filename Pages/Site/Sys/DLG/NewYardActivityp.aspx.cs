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
    protected string m_szCheckTypeKind = "";
    protected string m_Property= "";
    protected string m_level = "";
    protected string m_szDevKind = "";

    protected string m_szIdent = "";
    protected string m_szDevCLS = "";
    protected string m_szDev = "";
    protected string m_szResvPurpose = "";
    protected string m_Priority = "";
    protected string m_Limit = "";
    protected string m_szDept = "";
    protected uint uResvFor = 2;//1devid
    protected string m_szGroup = "";
    protected string m_szDevice = "";
    protected string m_szYardActivity = "";

    protected string m_szExtCheckType = "";
    protected string m_szCheckType = "";
    protected string m_szCheckSerType = "";
    protected string m_szYardActivityMemo = "";

    protected string szCamp = "";
    protected string szBuilding = "";

    protected string szSiteAppleModel = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        YARDACTIVITY newYardActivity;
        string szOp = "新建";
        if (Request["op"] == "set")
        {
            szOp = "修改";
        }
        UNICAMPUS[] vtCamp = GetAllCampus();
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        szSiteAppleModel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "YardResvModule", true);
        UNIBUILDING[] vtBuilding = getAllBuilding();
        for (int i = 0; i < vtBuilding.Length; i++)
        {
            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }
        if (IsPostBack)
        {
            newYardActivity.szActivityName = Request["szActivityName"];
            string szkindsTemp = Request["kindsTemp"];
            GetHTTPObj(out newYardActivity);
            /* newYardActivity.szUsableKindIDs = Request["szUsableKindIDs"];
             string[] szDevKindList = newYardActivity.szUsableKindIDs.Split(',');
             string szDevKindName = "";
             for (int i = 0; i < szDevKindList.Length; i++)
             {
                 UNIDEVKIND devKindGet;
                 if (GetDevKindByID(szDevKindList[i], out devKindGet))
                 {
                     szDevKindName += devKindGet.szKindName + ",";
                 }

             }
             * */
            string szCheckType = (Request["checktype"]);
            string szCheckType2 = (Request["checktype2"]);
            uint uCheckType = CharListToUint(szCheckType) + CharListToUint(szCheckType2);
            //    newYardActivity.szUsableDevKindNames = szDevKindName;

            string[] szCheckTypeList = new string[0];// newYardActivity.szUsableKindIDs.Split(',');
            string szCheckTypeName = "";
            for (int i = 0; i < szCheckTypeList.Length; i++)
            {
                CHECKTYPEREQ vrCheckTypeTemp = new CHECKTYPEREQ();
                vrCheckTypeTemp.dwCheckKind = Parse(szCheckTypeList[i]);
                CHECKTYPE[] vtChecktypeTemp;

                if (m_Request.Admin.CheckTypeGet(vrCheckTypeTemp, out vtChecktypeTemp) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    szCheckTypeName += vtChecktypeTemp[0].szCheckName + ",";
                }
            }

            newYardActivity.dwCheckKinds = uCheckType;
            newYardActivity.szCheckNames = szCheckTypeName;
            string szMemo = Request["szMemo"];
            if (szMemo != null)
            {
                szMemo = szMemo.Replace(",", "&");
            }
            if (szMemo ==null||szMemo== "")
            {
                szMemo = "";
            }
            newYardActivity.szMemo = szMemo;
            string[] szkindsTempList = szkindsTemp.Split(',');
            ArrayList list = new ArrayList();
            for (int i = 0; i < szkindsTempList.Length; i++)
            {
                string szTemp = szkindsTempList[i];
                if (szTemp != null && szTemp != "")
                {
                    list.Add(szTemp);
                }
            }
            object[] szkindsTempListLast = (object[])list.ToArray();
            YADEVKIND[] yardkinds = new YADEVKIND[szkindsTempListLast.Length];
            for (int i = 0; i < szkindsTempListLast.Length; i++)
            {
                yardkinds[i] = new YADEVKIND();
                yardkinds[i].dwKindID = Parse((string)szkindsTempListLast[i]);
                yardkinds[i].dwActivitySN = newYardActivity.dwActivitySN;
            }
            newYardActivity.UsableDevKind = yardkinds;
            if (yardkinds == null || yardkinds.Length == 0)
            {
                YADEVKIND[] yardkindsTemp=new YADEVKIND[0];
                newYardActivity.UsableDevKind = yardkindsTemp;
            }
            uint dwKindModul = Parse(Request["dwKindModul"]);
            if (newYardActivity.dwSecurityLevel == null)
            {
                newYardActivity.dwSecurityLevel = 0;
            }
            newYardActivity.dwSecurityLevel = (uint)newYardActivity.dwSecurityLevel | dwKindModul;
            if (m_Request.Reserve.SetYardActivity(newYardActivity, out newYardActivity) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOp + "场景失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOp + "场景成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szYardActivityMemo = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "szMemo", "YardKindMemo", true);
        /* UNIDEVKIND[] devKind = GetAllDevKind();
       if (devKind != null)
       {
          
           for (int i = 0; i < devKind.Length; i++)
           {
               m_szDevKind += GetInputItemHtml(CONSTHTML.checkBox, "szUsableKindIDs", devKind[i].szKindName, devKind[i].dwKindID.ToString());
           }
            
       } * */
        CHECKTYPEREQ vrCheckType = new CHECKTYPEREQ();
        vrCheckType.dwMainKind = ((uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SECURITY + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_PUBLICITY + (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR);
        CHECKTYPE[] vtChecktype;

        if (m_Request.Admin.CheckTypeGet(vrCheckType, out vtChecktype) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtChecktype.Length; i++)
            {
                m_szCheckType+=GetInputItemHtml(CONSTHTML.checkBox,"checktype",vtChecktype[i].szCheckName.ToString(),vtChecktype[i].dwCheckKind.ToString());
            }
        }
        CHECKTYPEREQ vrCheckSerType = new CHECKTYPEREQ();
        vrCheckSerType.dwMainKind = ((uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE);
        CHECKTYPE[] vtCheckRestype;

        if (m_Request.Admin.CheckTypeGet(vrCheckSerType, out vtCheckRestype) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtCheckRestype.Length; i++)
            {
                m_szCheckSerType += GetInputItemHtml(CONSTHTML.checkBox, "checktype2", vtCheckRestype[i].szCheckName.ToString(), vtCheckRestype[i].dwCheckKind.ToString());
            }
        }

        if (Request["op"] == "set")
        {
            bSet = true;

            YARDACTIVITYREQ vrGet = new YARDACTIVITYREQ();
            vrGet.dwActivitySN = Parse(Request["dwLabID"]);
            YARDACTIVITY[] vtRes;
            if (m_Request.Reserve.GetYardActivity(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    string szMemo = vtRes[0].szMemo;
                    szMemo = szMemo.Replace("&", ",");
                    vtRes[0].szMemo = szMemo;
                    PutJSObj(vtRes[0]);
                    PutMemberValue("checktype2", vtRes[0].dwCheckKinds.ToString());
                    PutMemberValue("checktype", vtRes[0].dwCheckKinds.ToString());
                    m_Title = "修改【" + vtRes[0].szActivityName + "】";

                    UNIDEVKIND[] vtDevKind;
                    DEVKINDREQ vrKindGet = new DEVKINDREQ();
                    vrKindGet.dwExtRelatedID = vtRes[0].dwActivitySN;
                    if (m_Request.Device.DevKindGet(vrKindGet, out vtDevKind) == REQUESTCODE.EXECUTE_SUCCESS && vtDevKind != null && vtDevKind.Length > 0)
                    {
                        string szYardKinds = ",";
                        for (int i = 0; i < vtDevKind.Length; i++)
                        {
                            szYardKinds += vtDevKind[i].dwKindID.ToString() + ",";
                        }

                        PutMemberValue("kindsTemp", szYardKinds);
                        PutMemberValue("kinds", szYardKinds);
                    }
                    uint uSecurityLevel = (uint)vtRes[0].dwSecurityLevel;
                    if ((uSecurityLevel & (uint)268435456) > 0)//0x10000000教室借用
                    {
                        PutMemberValue("dwKindModul", "268435456");
                    }
                    else if ((uSecurityLevel & (uint)536870912) > 0)//0x20000000教室借用
                    {
                        PutMemberValue("dwKindModul", "536870912");
                    }
                    else if ((uSecurityLevel & (uint)8388608) > 0)//0x800000教室借用
                    {
                        PutMemberValue("dwKindModul", "8388608");
                    }
                }
            }
        }
        else
        {
            m_Title = szOp + "场景";

        }
    }
}
