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
    protected string m_szOut = "";
    protected string m_szRoom="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
        }
        {
            UNIROOM[] roomList = GetAllRoom();
            if (roomList != null && roomList.Length > 0)
            {
                for (int i = 0; i < roomList.Length; i++)
                {
                    m_szRoom += "<input class=\"enum\" type=\"checkbox\" name=\"" + "roomID" + "\" value=\"" + roomList[i].dwRoomID.ToString() + "\" /> " + roomList[i].szRoomName + ",";
                }
            }

        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DOORCARDRECREQ vrGet = new DOORCARDRECREQ();
        GetPageCtrlValue(out vrGet.szReqExtInfo);
        GetHTTPObj(out vrGet);
        vrGet.dwStartDate = GetDate(Request["dwStartDate"]);
        vrGet.dwEndDate = GetDate(Request["dwEndDate"]);
        vrGet.dwCardMode = ((uint)DOORCARDREQ.DWCARDMODE.DOORCARD_IN);
       // ViewState["dwStartDate"] = Request["dwStartDate"].Replace(",",""); ;
       // ViewState["dwEndDate"] = Request["dwEndDate"].Replace(",","");
        //vrGet.dwAccNo = 0;
        DOORCARDREC[] vtRes;
        uResponse = m_Request.Report.GetDoorCardRec(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS&&vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vtRes[i].dwSID.ToString() + "\">" + vtRes[i].dwSID.ToString() + "</td>";
                m_szOut += "<td>" + vtRes[i].szPID.ToString() + "</td>";
                m_szOut += "<td>" + vtRes[i].szTrueName.ToString() + "</td>";
                m_szOut += "<td>" + Get1970Date(vtRes[i].dwCardTime) + "</td>";
                m_szOut += "<td>" + vtRes[i].szRoomName.ToString() + "</td>";
                m_szOut += "<td>" + vtRes[i].szMemo.ToString() + "</td>";
            
                m_szOut += "</tr>";
            }
        }
        PutBackValue();
        PutJSObj(vrGet);
        
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        if (ViewState["dwStartDate"] != null && ViewState["dwStartDate"].ToString() != "")
        {
            PutMemberValue("dwStartDate", ViewState["dwStartDate"].ToString());
        }
        if (ViewState["dwEndDate"] != null && ViewState["dwEndDate"].ToString() != "")
        {
            PutMemberValue("dwEndDate", ViewState["dwEndDate"].ToString());
        }
    }
}