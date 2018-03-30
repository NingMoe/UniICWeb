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
using System.IO;
using System.Text;

public partial class _Default : UniPage
{
    protected string m_Title = "";
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szPorperty = "";
    protected string szDevCLS = "";
    protected string szRoom = "";
    protected string szFunction = "";
    protected string szDevSN = "";
    protected string szDept = "";


    protected string m_sKindSer = "";
    protected string m_sKindSell = "";
    protected string m_sKindPro = "";

	protected void Page_Load(object sender, EventArgs e)
	{
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uint uDevCount = 0;
        uint uPrice = 0;
        string szPostion = "";
        COMPANYREQ campGet = new COMPANYREQ();
        UNICOMPANY[] vtCamp;
        if (m_Request.Assert.GetCompany(campGet, out vtCamp) == REQUESTCODE.EXECUTE_SUCCESS && vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                if ((vtCamp[i].dwComKind & (uint)UNICOMPANY.DWCOMKIND.COM_PRODUCER) > 0)
                {
                    m_sKindPro += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szComName, vtCamp[i].dwComID.ToString());
                }
                else if ((vtCamp[i].dwComKind & (uint)UNICOMPANY.DWCOMKIND.COM_SELLER) > 0)
                {
                    m_sKindSell += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szComName, vtCamp[i].dwComID.ToString());
                }
                else if ((vtCamp[i].dwComKind & (uint)UNICOMPANY.DWCOMKIND.COM_SERVICE) > 0)
                {
                    m_sKindSer += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szComName, vtCamp[i].dwComID.ToString());
                }
            }
        }
        UNIDEVCLS[] vtDevCls = GetAllDevCls();
        UNIDEPT[] vtdept = GetAllDept();
        for (int i = 0; i < vtdept.Length; i++)
        {
            szDept += GetInputItemHtml(CONSTHTML.option, "", vtdept[i].szName, vtdept[i].dwID.ToString());
        }
            // szDevSN = GetDevSN().ToString();
            if (vtDevCls != null && vtDevCls.Length > 0)
            {
                for (int i = 0; i < vtDevCls.Length; i++)
                {
                    szDevCLS += GetInputItemHtml(CONSTHTML.option, "", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
                }
            }
        UNIROOM[] vtRoom = GetAllRoom();
        if (vtRoom != null && vtRoom.Length > 0)
        {
            for (int i = 0; i < vtRoom.Length; i++)
            {
                szRoom += GetInputItemHtml(CONSTHTML.option, "", vtRoom[i].szRoomName, vtRoom[i].dwRoomID.ToString());
            }
        }
        CODINGTABLE[] vtCodeTable = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_DEVFUNC);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                szFunction += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName.ToString(), vtCodeTable[i].szCodeSN.ToString());
            }
        }
        if (IsPostBack)
        {
            uint szPurchaseDate=GetDate(Request["dwPurchaseDate"]);
            DEVREQ devGet = new DEVREQ();
            devGet.szAssertSN = szPurchaseDate.ToString();
            devGet.szReqExtInfo.dwNeedLines = 1;
            devGet.szReqExtInfo.dwStartLine = 0;
            devGet.szReqExtInfo.szOrderKey = "szAssertSN";
            devGet.szReqExtInfo.szOrderMode = "desc";
            UNIDEVICE[] vtDevList;
            int iStart = 0;
            if (m_Request.Device.Get(devGet, out vtDevList) == REQUESTCODE.EXECUTE_SUCCESS && vtDevList != null && vtDevList.Length > 0)
            {
                //string szAssertSN = vtDevList[0].szAssertSN.ToString();
                //uint uStart = Parse(szAssertSN.Substring(8,4));
                iStart = szGetMaxValue(vtDevList[0].szAssertSN.ToString());
            }
            
            iStart = iStart + 1;
            string szDept = Request["dwdept"];
            UNIDEPT deptValue;
            string szDeptMemo = "";
            if(GetDeptByID(szDept,out deptValue))
            {
                szDeptMemo = deptValue.szMemo;
            }
       
            uint uNum = Parse(Request["dwNum"]);
            int NextLen = IntParse(Request["dwNextLen"]);
            string LenFix = Request["LenFix"];
            string szPreName = Request["szPreName"];//为assertsn
            string szPreNameTemp = szPreName;

            string szPreOriginSN = Request["szPreName"];// Request["szPreOriginSN"];//原厂序列号
            string szNextOriginSN = Request["szNextName"];//后缀编号
            uint uNextOriginSN = Parse(szNextOriginSN);
            string szPreOriginSNTemp = szPreOriginSN;
            string dwOriginSNLenFix = Request["LenFix"]; // Request["dwOriginSNLenFix"];
            int dwOriginSNLen = IntParse(Request["dwNextLen"]); //; IntParse(Request["dwOriginSNLen"]);

            uint uNextName = Parse(Request["szNextName"]);
            uint uRoomID = Parse(Request["dwRoomID"]);
            uint uLabID = Parse(Request["dwLabID"]);
            uint uKindID = Parse(Request["dwKindID"]);
            uint uStartNum = Parse(Request["dwStartNum"]);
            uint uCtrlMode = Parse(Request["dwCtrlMode"]);

            string szPreDevName = Request["szPreDevName"];
            string szNextNameTemp = szPreDevName;
            uint uNextDevName = (uint)iStart;// Parse(Request["szNextDevName"].ToString());
            int uNextDevLen = 0;//暂时取消，为了资产名能够自由编辑 IntParse(Request["dwNextDevLen"].ToString());
            string uDevLenFix = (Request["DevLenFix"]);
            UNIASSERT newDev = new UNIASSERT();
            GetHTTPObj(out newDev);
            string szRoomMemo = "";
            string szDevClsMemo = "";
            UNIROOM roomGet;
            if (GetRoomID(newDev.dwRoomID.ToString(), out roomGet))
            {
                szRoomMemo = roomGet.szMemo;
            }
            newDev.szDevName = szPreDevName;
            string szKindName = newDev.szDevName + "-" + newDev.szModel + "-" + newDev.szFuncCode;
            uint uKindIDTemp = GetDevKindByName(szKindName);
            if (uKindIDTemp > 0)
            {
                newDev.dwKindID = uKindIDTemp;
                UNIDEVKIND kindValue;
                if (GetDevKindByID(uKindIDTemp.ToString(), out kindValue))
                {
                    kindValue.dwClassID = newDev.dwClassID;
                    UNIDEVCLS setDevCls;
                    if (GetDevCLSByID(kindValue.dwClassID.ToString(), out setDevCls))
                    {
                        szDevClsMemo = setDevCls.szMemo;
                    }
                }
            }
            else
            {
                UNIDEVKIND kindValue;
                GetHTTPObj(out kindValue);
                kindValue.dwClassID = newDev.dwClassID;
                UNIDEVCLS setDevCls;
                if (GetDevCLSByID(kindValue.dwClassID.ToString(), out setDevCls))
                {
                    szDevClsMemo = setDevCls.szMemo;
                }
                kindValue.dwMaxUsers = 1;
                kindValue.dwMinUsers = 1;
                kindValue.dwProperty = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE + (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV;
                kindValue.szKindName = newDev.szDevName + "-" + newDev.szModel + "-" + newDev.szFuncCode;
                if (m_Request.Device.DevKindSet(kindValue, out kindValue) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uKindIDTemp = (uint)kindValue.dwKindID;
                }
                else
                {
                    return;
                }
            }
            //for (uint i = uNextName; i <= (uNum + uNextName - 1); i++)
                for (uint i = uNextName; i <= (uNum + uNextName - 1); i++)
            {
               
                newDev.dwPurchaseDate = GetDate(Request["dwPurchaseDate"]);
                if (LenFix == "true")
                {
                    szPreName = szPreNameTemp + i.ToString().PadLeft(NextLen, '0');
                }
                else
                {
                    szPreName = szPreNameTemp + i.ToString();
                }
                    /*
                if (uDevLenFix == "true")
                {
                    szPreDevName = szNextNameTemp + uNextDevName.ToString().PadLeft(uNextDevLen, '0');
                }
                else
                {
                    szPreDevName = szNextNameTemp + uNextDevName.ToString();
                }
               */
                if (dwOriginSNLenFix == "true")
                {
                    szPreOriginSN = szPreOriginSNTemp + uNextOriginSN.ToString().PadLeft(dwOriginSNLen, '0');
                    szPreDevName = szNextNameTemp + uNextOriginSN.ToString().PadLeft(dwOriginSNLen, '0');
                }
                else
                {
                    szPreOriginSN = szPreOriginSNTemp + uNextOriginSN.ToString();
                    szPreDevName = szNextNameTemp + uNextOriginSN.ToString();
                }
               
                newDev.szDevName = szPreDevName;
                string szLabEX = "";
                UNILAB lab = new UNILAB();
                if (GetLabByID((uint?)uLabID, out lab))
                {
                    szLabEX = lab.szMemo.ToString();
                }
                //newDev.szAssertSN = szPurchaseDate + iStart.ToString("0000") + szDeptMemo + szRoomMemo; //GetDevSN().ToString();//szPreName;
                //newDev.szAssertSN = szPurchaseDate + iStart.ToString("0000") + szDeptMemo + szRoomMemo; //GetDevSN().ToString();//szPreName;
               // newDev.szAssertSN = szPurchaseDate +szDevClsMemo+ iStart.ToString("0000");
                newDev.szAssertSN = szPreOriginSN;
              // newDev.szAssertSN = szPurchaseDate + szLabEX + iStart.ToString("0000");


                newDev.dwLabID = uLabID;
                newDev.dwKindID = uKindIDTemp;
                newDev.dwRoomID = uRoomID;
                newDev.dwSellerID = Parse(Request["dwSellerID"]);
                newDev.dwProducerID = Parse(Request["dwProducerID"]);
                newDev.dwServiceID = Parse(Request["dwServiceID"]);
                newDev.dwProperty = CharListToUint(Request["dwProperty"]);
                UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                newDev.dwKeeperID = vrAccInfo.dwAccNo;
                newDev.dwDevID = null;
                if (m_Request.Assert.AssertWarehousing(newDev, out newDev) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    UNIROOM getRoom;
                    if(GetRoomID(newDev.dwRoomID.ToString() ,out getRoom))
                    {
                        szPostion = getRoom.szRoomName.ToString();
                    }
                    iStart = iStart + 1;
                    uNextDevName = uNextDevName + 1;
                    uStartNum = uStartNum + 1;
                    uDevCount = uDevCount + 1;
                    uNextOriginSN = uNextOriginSN + 1;
                    uPrice = uPrice + (uint)newDev.dwUnitPrice;
                    
                  
                }
            }
            MessageBox("批量新建" + ConfigConst.GCDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            PrintAssign(uDevCount, uPrice, szPostion);
          
            return;
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        m_Title = "批量新建" + ConfigConst.GCDevName;
      
	}
    public uint GetDevKindByName(string szKindName)
    {
        DEVKINDREQ vrGet = new DEVKINDREQ();
        vrGet.szKindName = szKindName;
        UNIDEVKIND[] vtDevKind;
        if (m_Request.Device.DevKindGet(vrGet, out vtDevKind) == REQUESTCODE.EXECUTE_SUCCESS && vtDevKind != null && vtDevKind.Length > 0)
        {
            return (uint)vtDevKind[0].dwKindID;
        }
        return 0;
    }
    public int szGetMaxValue(string szValue)
    {
        int iRes = 1;
        szValue = szValue.ToLower();
        int nPostrtionEnd = -1;
        for (int i = 1; i <= szValue.Length; i++)
        {
            int iTemp = -1;
            int nPostrtionS = -1;
         
            try
            {
                iTemp = int.Parse(szValue[i-1].ToString());
                nPostrtionS = i;
            }
            catch
            {
                nPostrtionEnd = i;
            }
           
        }
        if (nPostrtionEnd == -1)
        {
            nPostrtionEnd = szValue.Length;
        }
        string szRes = szValue.Substring(nPostrtionEnd, szValue.Length - nPostrtionEnd);
        iRes = IntParse(szRes);
        return iRes;
    }

    protected void PrintAssign(uint uDevCount, uint uPrice, string szDevName)
    {
        System.IO.StringWriter swCSV = new System.IO.StringWriter();
        swCSV.WriteLine("<table  width=\"100%\" border=\"1px\" cellpadding=\"2\" cellspacing=\"0\"><tr style=\"height:35px;\"><td colspan=\"4\" style=\"text-align:center;height:45px;font-size:23px;\">资产入库表</td></tr><tr style=\"height:35px;\"><td style=\"width:180px;\">本次录入资产数量：</td><td colspan=\"3\" style='text-align:left'>");
        swCSV.WriteLine(uDevCount);
        swCSV.WriteLine("</td></tr><tr style=\"height:35px;\"><td>本次录入总价格：</td><td colspan=\"3\" style='text-align:left'>");
        swCSV.WriteLine(uPrice);
        swCSV.WriteLine("(元)</td></tr><tr style=\"height:35px;\"><td>本次录入实验室地址：</td><td colspan=\"3\" style='text-align:left'>");
        swCSV.WriteLine(szDevName);
        swCSV.WriteLine("</td></tr><tr style=\"height:35px;\"><td colspan=\"4\"></td></tr><tr style=\"height:35px;\"><td style=\"text-align:right\" colspan=\"3\">签名：</td><td></td></tr><tr style=\"height:35px;\"><td style=\"text-align:right\" colspan=\"3\">录入时间：</td><td>");
        swCSV.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
        swCSV.WriteLine("</td></tr></table>");
        DownloadFile(Response, swCSV.GetStringBuilder(), "RuleDaySum.csv");
        swCSV.Close();
        Response.End();
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("入库.xls");
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
}