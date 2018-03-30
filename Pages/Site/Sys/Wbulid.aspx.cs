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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string m_szCamp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNICAMPUS[] camp =GetAllCampus();
        if (camp != null && camp.Length > 0)
        {

            m_szCamp += "<option value='0'>" + "全部" + "</option>";
            for (int i = 0; i < camp.Length; i++)
            {
                m_szCamp += "<option value='" + camp[i].dwCampusID + "'";
                m_szCamp += ">" + camp[i].szCampusName + "</option>";
            }
        }

        if (Request["delID"] != null)
        {
            DelLab(Request["delID"]);
        }
        BUILDINGREQ vrParameter = new BUILDINGREQ();
        GetHTTPObj(out  vrParameter);
        if (vrParameter.szCampusIDs != null && vrParameter.szCampusIDs.ToString() == "0")
        {
            vrParameter.szCampusIDs = null;
        }
        UNIBUILDING[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.BuildingGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
               
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwBuildingID.ToString() + "\">" + vrResult[i].szBuildingNo + "</td>";
                m_szOut += "<td>" + vrResult[i].szBuildingName.ToString()+ "</td>";
                m_szOut += "<td>" + vrResult[i].szCampusName.ToString() + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
           
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIBUILDING lab = new UNIBUILDING();
        lab.dwBuildingID=Parse(szID);
        uResponse=m_Request.Device.BuildingDel(lab);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
