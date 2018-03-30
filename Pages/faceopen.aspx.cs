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
using Newtonsoft.Json;
public partial class _Default : UniPage
{
    public class openRes
    {
        public int res;
        public string szMessage;
        public string szTrueName;

    };
    protected void Page_Load(object sender, EventArgs e)
    {
        openRes openres = new openRes();
        MOBILEOPENDOORREQ req = new MOBILEOPENDOORREQ();
        string szLogonName = Request["uid"];

        string szSignKey = Request["signkey"];
        string szuid = Request["uid"];
        string szDcssn = Request["dcssn"];
        string ctrlsn = Request["ctrlsn"];

        string szKey = "X(J@L*!IA";

        string szDate = DateTime.Now.ToString("yyyyMMdd");

        string ma5 = szuid + szKey;

        ma5 = ma5 + szDate;

        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");

        if (szSignKey.ToLower() == EnPswdStr.ToLower())
        {
            req.szLogonName = szLogonName;
            req.szPassword = "Punifound808";

            MOBILEOPENDOORRES res;
            req.dwDCSSN = uint.Parse(szDcssn);
            req.dwCtrlSN = uint.Parse(ctrlsn);
            req.szMSN = "";
            req.dwCardMode = (uint)DOORCARDREQ.DWCARDMODE.DOORCARD_IN+ (uint)DOORCARDREQ.DWCARDMODE.MOBILE_OPENDOOR;
            REQUESTCODE uResponse = m_Request.DoorCtrlSrv.MobilOpenDoor(req, out res);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if ((res.dwUserKind & (uint)DOORCARDRES.DWUSERKIND.CARDUSER_PERMIT) > 0)
                {
                    openres.res = 1;
                    openres.szTrueName=res.szTrueName;
                }
                else {
                    openres.res = 0;
                    openres.szMessage = res.szDispInfo;
                }
                
            }
           
            else
            {
                openres.res = 0;
                openres.szMessage = openres.szMessage;
            }
         

        }
        else
        {
            openres.res = 0;
            openres.szMessage = "MD5加密信息错误";
        }
        


    Response.Write(JsonConvert.SerializeObject(openres));
        Response.End();

    }


}