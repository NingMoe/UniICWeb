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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_szLab = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CHANNELGATEREQ vrParameter = new CHANNELGATEREQ();
        vrParameter.dwGetType = (uint)CHANNELGATEREQ.DWGETTYPE.CHANNELGATEGET_BYALL;
        UNICHANNELGATE[] vrResult;
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        UNIROOM[] vtRoom = GetAllRoom();
        if (m_Request.Device.ChannelGateGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS&&vrResult.Length>0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td ManGroupID='"+vrResult[i].dwManGroupID.ToString()+"' class=\"1\" data-id=" + vrResult[i].dwChannelGateID.ToString() + " data-ManGroupID=" + vrResult[i].dwManGroupID.ToString() + ">" + vrResult[i].szChannelGateName + "</td>";
                m_szOut += "<td>" + szGetRoomName(vrResult[i].szRelatedRooms,vtRoom,vrResult)+ "</td>";
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private string szGetRoomName(string szRoomNo, UNIROOM[] vtRoom, UNICHANNELGATE[] vtRes)
    {
        string szRes = "";
        string[] szRoomNoList=szRoomNo.Split(',');
        for (int i = 0; i < szRoomNoList.Length; i++)
        {
            string szRoomNOTemp = szRoomNoList[i];
            if (vtRoom != null)
            {
                int flag = 0;
                for (int j = 0; j < vtRoom.Length; j++)
                {
                    if (szRoomNOTemp == vtRoom[j].szRoomNo)
                    {
                        szRes += vtRoom[j].szRoomName + "，";
                        flag = 1;
                        break;

                    }
                }
                if (flag == 0 && vtRes != null && vtRes.Length>0)
                {
                    for (int j = 0; j < vtRes.Length; j++)
                    {
                        if (szRoomNOTemp == vtRes[j].szChannelGateNo)
                        {
                            szRes += vtRes[j].szChannelGateName + "，";
                            break;
                        }
                    }
                }
            }
        }
        return szRes;
    }
    private void DelRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICHANNELGATE channel = new UNICHANNELGATE();
        channel.dwChannelGateID = Parse(szID);
        uResponse = m_Request.Device.ChannelGateDel(channel);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
