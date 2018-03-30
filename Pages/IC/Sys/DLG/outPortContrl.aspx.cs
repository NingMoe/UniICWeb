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
using System.Collections;
using Newtonsoft.Json;
using System.IO;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string dwKindList = "";
    protected string szKindObject = "";
    protected string m_szRoom = "";
    public class consoleRoom
    {
        public string szRoomNo;
        public string szIP;
    };
    protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "生成配置文件";

        if (IsPostBack)
        {
            string szKindError = "";
            string szRoomNoNull = "";
            string szRoomNoMut = "";
            CONREQ vrParameter = new CONREQ();
            UNICONSOLE[] vrResult;
            ArrayList list = new System.Collections.ArrayList();
            string szError = "";
            if (m_Request.Console.ConGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                int nCountError = 0;

                for (int i = 0; i < vrResult.Length; i++)
                {
                    uint uKind = (uint)vrResult[i].dwKind;
                    if ((uKind & (uint)UNICONSOLE.DWKIND.CONKIND_DISPLAY) == 0)
                    {
                        nCountError = nCountError + 1;
                        szKindError += vrResult[i].szConsoleName + ",";
                        continue;
                    }
                    string szRoomNo = vrResult[i].szManRooms;
                    if (string.IsNullOrEmpty(szRoomNo))
                    {
                        nCountError = nCountError + 1;
                        szRoomNoNull += vrResult[i].szConsoleName + ",";
                        continue;
                    }
                    else if ((szRoomNo.Length - szRoomNo.Replace(",", "").Length) > 2)
                    {
                        nCountError = nCountError + 1;
                        szRoomNoMut += vrResult[i].szConsoleName + ",";
                        continue;
                    }
                    consoleRoom value = new consoleRoom();
                    value.szIP = vrResult[i].szIP;
                    value.szRoomNo = vrResult[i].szManRooms;
                    
                    list.Add(value);
                }
                szError += "成功导出" + (vrResult.Length - nCountError).ToString() + "个,";
                if (szKindError != "")
                {
                    szError += "不是触控柜不能导出:" + szKindError;
                }
                if (szRoomNoNull != "")
                {
                    szError += "不关联房间不能导出:" + szKindError;
                }
                if (szRoomNoMut != "")
                {
                    szError += "关联多个房间不能导出:" + szKindError;
                }

            }
            string json = JsonConvert.SerializeObject(list);

            string path = Server.MapPath("~/")+("padtxt\\ctrlroom.txt");

            FileStream myFs = new FileStream(path, FileMode.Create);//txtFilePath为生成txt文件的路径
            StreamWriter mySw = new StreamWriter(myFs);
            mySw.Write(json);//writeStr为要写入的字符串
            mySw.Close();
            myFs.Close();


            MessageBox("提示:" + szError, "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);


        }

    }
}
