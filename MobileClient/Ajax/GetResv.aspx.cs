using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    public class resvMin
    {
        public uint resvID;
        public string szResvTime;
        public string devName;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (m_Request == null)
        {
            Response.Write("{\"error\":\"\"}");
            return;
        }
        RESVREQ vrGet = new RESVREQ();
        vrGet.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO;
        vrGet.szReqExtInfo.szOrderKey = "dwBeginTime";
        vrGet.szReqExtInfo.szOrderMode = "asc";
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        UNIRESERVE[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        
        if (m_Request.Reserve.Get(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS&&vtRes!=null)
        {
           
            {
                List<resvMin> list = new List<resvMin>();
                for (int i = 0; i < vtRes.Length; i++)
                {
                    resvMin temp = new resvMin();
                    temp.resvID = (uint)vtRes[i].dwResvID;
                    temp.szResvTime = Get1970Date((uint)vtRes[i].dwBeginTime) + "到" + Get1970Date((uint)vtRes[i].dwEndTime,"HH:mm");
                    temp.devName = vtRes[i].ResvDev[0].szDevName;
                    list.Add(temp);
                }
                string szRes = JsonConvert.SerializeObject(list);
                Response.Write(szRes);
                return;
            }
        }
        else {
            Response.Write("{\"error\":\"" + m_Request.szErrMessage + "\"}");
        }


    }
    public string GetHourStr(uint uMin)
    {
        if (uMin < 60)
        {
            return uMin + "分钟";
        }
        else if (uMin < (60 * 24) && uMin>=60)
        {
            if ((uMin % 60) == 0)
            {
                return uMin / 60 + "小时";
            }
            return uMin / 60 + "小时" + uMin % 60 + "分钟";
        }
        else if (uMin >= (60 * 24))
        {
            return GetTimeForSecond(uMin*60);
        }
        return "";
    }
}