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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szCodeing = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
        string szOrderKey = Request["_szOrderKey"];
        string szOrderMode= Request["_szOrderMode"];
        GetHTTPObj(out vrPar);
        if (szOrderKey != null&& szOrderKey!="")
        {
            vrPar.szReqExtInfo = new REQEXTINFO();
            vrPar.szReqExtInfo.szOrderKey = szOrderKey;
            vrPar.szReqExtInfo.szOrderMode = szOrderMode;
        }
        string szYardKind = Request["yardKind"];
        uint uYardKind = Parse(szYardKind);
        if (!IsPostBack)
        {
            //   dwStartDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            // dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != null && dwEndDate.Value != null && dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrPar.dwBeginDate = GetDate(dwStartDate.Value);
            vrPar.dwEndDate = GetDate(dwEndDate.Value);
        }
        else
        {
            vrPar.dwBeginDate = null;
            vrPar.dwEndDate = null;
        }
        CODINGTABLE[] vtCodeing = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_YARDRESVKIND);
        szCodeing += GetInputItemHtml(CONSTHTML.radioButton, "dwKind", "全部", "0");
        for (int i = 0; vtCodeing != null && i < vtCodeing.Length; i++)
        {
            szCodeing += GetInputItemHtml(CONSTHTML.radioButton, "dwKind", vtCodeing[i].szCodeName, vtCodeing[i].szCodeSN);
        }
        uint uKind = Parse(Request["dwKind"]);
        if (uKind != 0)
        {
            vrPar.dwKind = uKind;
        }
        else {
            vrPar.dwKind = null;
            PutMemberValue("dwKind", "0");
        }
     
        uint uBeginDate = GetDate(dwStartDate.Value);
        uint uEndDate = GetDate(dwEndDate.Value);
        vrPar.dwNeedYardResv = 1;
        if (vrPar.dwCheckStat == null || ((uint)vrPar.dwCheckStat) == 0)
        {
            vrPar.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
        }
        YARDRESVCHECKINFO[] vtRes;
        string szResvTime = "";
        string szResvTimeAll = "";
        ArrayList listResvID = new ArrayList();
        REQEXTINFO extInfo=vrPar.szReqExtInfo;
        if(extInfo.szOrderKey==null)
        {
            extInfo.szOrderKey = "dwCheckTime";
            extInfo.szOrderMode = "desc";
        }
      //  vrPar.dwCheckStat= vrPar.dwCheckStat|(uint)


        uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);

            CHECKTYPEREQ vrGet = new CHECKTYPEREQ();
        CHECKTYPE[] vtCheck;
        if (m_Request.Admin.CheckTypeGet(vrGet, out vtCheck) == REQUESTCODE.EXECUTE_SUCCESS && vtCheck != null && vtCheck.Length > 0)
        {
        }


        ArrayList yardResvListLast = new ArrayList();
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            //合并 结果到yardResvListLastID,
            for (int i = 0; i < vtRes.Length; i++)
            {
                uint uResvIDTemp = 0;
                if (vtRes[i].YardResv.dwResvGroupID != null)
                {
                    uResvIDTemp = (uint)vtRes[i].YardResv.dwResvGroupID;
                }
                else
                {
                    continue;
                }
                bool isAdd = true;
                int uPostion = -1;

                CHECKTYPE checktype = new CHECKTYPE();
                if (GetCheckType((uint)vtRes[i].dwCheckKind, out checktype, vtCheck))
                {
                    if ((checktype.dwMainKind & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE) > 0)
                    {
                        continue;
                    }
                }
                for (int k = 0; k < yardResvListLast.Count; k++)
                {
                    YARDRESVCHECKINFO tempClass = (YARDRESVCHECKINFO)yardResvListLast[k];
                    if (tempClass.YardResv.dwResvGroupID==uResvIDTemp)
                    {
                        uPostion = k;
                        isAdd = false;
                        break;
                    }
                }
                if (isAdd)
                {
                    YARDRESVCHECKINFO newClassTemp = new YARDRESVCHECKINFO();
                    newClassTemp = vtRes[i];
                    newClassTemp.szCheckDetail = newClassTemp.dwCheckID.ToString()+',';
                    newClassTemp.YardResv.dwResvGroupID = uResvIDTemp;
                    newClassTemp.szMemo= Get1970Date(vtRes[i].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[i].YardResv.dwEndTime) + "；";
                    yardResvListLast.Add(newClassTemp);
                }
                else {
                    YARDRESVCHECKINFO newClassTemp = new YARDRESVCHECKINFO();
                    newClassTemp = (YARDRESVCHECKINFO)yardResvListLast[uPostion];
                    newClassTemp.YardResv.dwResvGroupID = uResvIDTemp;
                    string szMemo = newClassTemp.szMemo.ToString();
                    newClassTemp.szMemo = szMemo + Get1970Date(vtRes[i].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[i].YardResv.dwEndTime) + "；";
                    newClassTemp.szCheckDetail = newClassTemp.szCheckDetail + vtRes[i].dwCheckID.ToString() + ',';
                    if (newClassTemp.szCheckName.IndexOf(vtRes[i].szCheckName) < 0)
                    {
                        newClassTemp.szCheckName += "," + vtRes[i].szCheckName;
                    }
                    yardResvListLast[uPostion] = newClassTemp;

                }
            }
            for (int i = 0; i < vtRes.Length; i++)
            {
                uint uResvIDTemp = 0;
                if (vtRes[i].YardResv.dwResvGroupID != null)
                {
                    uResvIDTemp = (uint)vtRes[i].YardResv.dwResvGroupID;
                }
             
                bool isAdd = true;
                int uPostion = -1;

                CHECKTYPE checktype = new CHECKTYPE();
                if (GetCheckType((uint)vtRes[i].dwCheckKind, out checktype, vtCheck))
                {
                    if ((checktype.dwMainKind & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE) > 0)
                    {
                        for (int k = 0; k < yardResvListLast.Count; k++)
                        {
                            YARDRESVCHECKINFO tempClass = (YARDRESVCHECKINFO)yardResvListLast[k];
                            if (tempClass.YardResv.dwResvGroupID == uResvIDTemp)
                            {
                                uPostion = k;
                                isAdd = false;
                                break;
                            }
                        }
                        if (isAdd)
                        {
                            YARDRESVCHECKINFO newClassTemp = new YARDRESVCHECKINFO();
                            newClassTemp = vtRes[i];
                            newClassTemp.szCheckDetail = newClassTemp.dwCheckID.ToString() + ',';
                            newClassTemp.YardResv.dwResvGroupID = uResvIDTemp;
                            newClassTemp.szMemo = Get1970Date(vtRes[i].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[i].YardResv.dwEndTime) + "；";
                            yardResvListLast.Add(newClassTemp);
                        }
                        else
                        {
                            YARDRESVCHECKINFO newClassTemp = new YARDRESVCHECKINFO();
                            newClassTemp = (YARDRESVCHECKINFO)yardResvListLast[uPostion];
                            newClassTemp.YardResv.dwResvGroupID = uResvIDTemp;
                            string szMemo = newClassTemp.szMemo.ToString();
                            newClassTemp.szMemo = szMemo + Get1970Date(vtRes[i].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[i].YardResv.dwEndTime) + "；";
                            newClassTemp.szCheckDetail = newClassTemp.szCheckDetail + vtRes[i].dwCheckID.ToString() + ',';
                            newClassTemp.szCheckName += "," + vtRes[i].szCheckName;
                            yardResvListLast[uPostion] = newClassTemp;

                        }
                    }
                }
            }
            for (int m = 0; m < yardResvListLast.Count; m++)
            {
                szResvTimeAll = "";
                szResvTime = "";
                YARDRESVCHECKINFO newClassTemp = new YARDRESVCHECKINFO();
                newClassTemp = (YARDRESVCHECKINFO)yardResvListLast[m];


                if (Session["checkid"] != null && Session["checkid"].ToString().IndexOf(newClassTemp.dwCheckID.ToString())>-1)
                {
                    m_szOut += "<tr bgcolor='#2E8B57'>";
                }
                else {
                    m_szOut += "<tr>";
                }

                m_szOut += "<td data-ActivityLevel ='" + newClassTemp.YardResv.dwSecurityLevel + "' data-checkIDs='" + newClassTemp.szCheckDetail + "' data-ActivityLevel ='" + newClassTemp.YardResv.dwSecurityLevel + "' data-resvGroupID='" + newClassTemp.YardResv.dwResvGroupID.ToString() + "' class='getInfo' data-id=" + newClassTemp.szCheckDetail.ToString() + ">" + newClassTemp.YardResv.szActivityName + "</td>";
                m_szOut += "<td>" + newClassTemp.YardResv.dwResvGroupID + "</td>";
                m_szOut += "<td>" + newClassTemp.YardResv.szResvName + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + newClassTemp.dwApplicantID.ToString() + "' data-ActivityLevel ='" + newClassTemp.YardResv.dwSecurityLevel + "' data-id=" + newClassTemp.dwCheckID.ToString() + ">" + newClassTemp.szApplicantName + "</td>";
m_szOut += "<td>" +Get1970Date(newClassTemp.YardResv.dwOccurTime) + "</td>";
                m_szOut += "<td>" + newClassTemp.YardResv.szDevName + "</td>";
                string[] szResvTimeList = newClassTemp.szMemo.Split('；');

                if (szResvTimeList != null && szResvTimeList.Length > 0)
                {
                    if (szResvTimeList.Length > 1)
                    {
                        szResvTime += "【" + (szResvTimeList.Length-1) + "】条:" + "<br/>";
                    }
                    for (int k = 0; k < szResvTimeList.Length; k++)
                    {
                        if (k< 5)
                        {
                            if (((k + 1) % 2) == 0)
                            {
                                szResvTime += szResvTimeList[k] + "；<br/>";
                            }
                            else
                            {
                                szResvTime += szResvTimeList[k] + "；";
                            }
                        }
                        szResvTimeAll += szResvTimeList[k] + ";";
                    }
                }
                YARDRESV[] yardresvList = GetYardResvByGroupID((uint)newClassTemp.YardResv.dwResvGroupID);
                if (newClassTemp.YardResv.szCycRule == null || newClassTemp.YardResv.szCycRule == "")
                {
                    m_szOut += "<td class='tdDetail' text='" + szResvTimeAll + "'>" + Get1970Date(newClassTemp.YardResv.dwBeginTime) + "到" + Get1970Date(newClassTemp.YardResv.dwEndTime) + "</td>";
                }
                else
                {
                    m_szOut += "<td class='tdDetail' text='" + szResvTimeAll + "'>" + newClassTemp.YardResv.szCycRule + "</td>";
                }
                string szCheckName = "";
                CHECKTYPE checkType = new CHECKTYPE();
                if (GetCheckType((uint)newClassTemp.dwCheckKind, out checkType, vtCheck))
                {
                    szCheckName = GetJustNameEqual((uint)checkType.dwMainKind, "CheckType_Kind");
                }
                m_szOut += "<td>" + (newClassTemp.szCheckName)  + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(newClassTemp.dwCheckStat, "Admin_CheckStatus") + "</td>";
                m_szOut += "<td>" + Get1970Date(newClassTemp.dwCheckTime) + "</td>";
                m_szOut += "<td>" + newClassTemp.szAdminName + "</td>";
                if ((newClassTemp.dwCheckStat & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO) > 0)
                {
                    m_szOut += "<td><div class='OPTD'></div></td>";
                }
                else
                {
                    m_szOut += "<td><div class='OPTD OPTD2'></div></td>";
                }

                m_szOut += "</tr>";


            }

        }
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
     
    }
    private YARDRESV[] GetYardResvByGroupID(uint uGroupID)
    {
        YARDRESVREQ vrGet = new YARDRESVREQ();
        vrGet.dwResvGroupID = uGroupID;
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;

    }
    private bool GetCheckType(uint uID,out CHECKTYPE setValue)
    {
        setValue = new CHECKTYPE();
        CHECKTYPEREQ vrGet = new CHECKTYPEREQ();
        vrGet.dwCheckKind = uID;
        CHECKTYPE[] vtCheck;
        if (m_Request.Admin.CheckTypeGet(vrGet, out vtCheck) == REQUESTCODE.EXECUTE_SUCCESS && vtCheck != null && vtCheck.Length > 0)
        {
            setValue= vtCheck[0];
            return true;
        }
        return false;
    }
    private bool GetCheckType(uint uID, out CHECKTYPE setValue, CHECKTYPE[] vtCheck)
    {
        setValue = new CHECKTYPE();
        if (vtCheck == null || vtCheck.Length == 0)
        {
            return false;
        }
        for (int i = 0; i < vtCheck.Length; i++)
        {
            if ((uint)vtCheck[i].dwCheckKind == uID)
            {
                setValue = vtCheck[0];
                return true;
            }
        }
        return false;
    }
    
}
