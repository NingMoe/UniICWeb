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
        string szlab = Request["lab"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            m_szLab += "<option value='0'>选择" + ConfigConst.GCLabName + "</option>";
            for (int i = 0; i < lab.Length; i++)
            {
                
                m_szLab += "<option value='" + lab[i].dwLabID + "'";
                if (szlab == lab[i].dwLabID.ToString())
                {
                    m_szLab += "checked='checked'";
                }
                m_szLab += ">" + lab[i].szLabName + "</option>";
            }
        }
        else
        {
            m_szLab += "<option value='0'>选择" + "空间类别" + "</option>";      
        }
        //=========================

        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;
        if (szlab != null && szlab!="0")
        {
            vrParameter.dwLabID = Parse(szlab);
        }
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        XmlCtrl xmlCtrl = new XmlCtrl("ics_data", Server.MapPath(MyVPath + "clientweb/upload/info/xmlData/"));
       
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-openid='" + vrResult[i].dwOpenRuleSN + "' class=\"1\" data-id=" + vrResult[i].dwRoomID.ToString() + " data-ManGroupID=" + vrResult[i].dwManGroupID.ToString() + ">" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td  class='lnkAssertRoom' data-id=" + vrResult[i].dwRoomID.ToString()+" >" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td>" + GetDateStr((uint)vrResult[i].dwCreateDate) + "</td>";
                m_szOut += "<td>" + szCodeName(vrResult[i].szLabKindCode, (uint)CODINGTABLE.DWCODETYPE.CODE_LABKIND) + "</td>";
                m_szOut += "<td>" + vrResult[i].szFloorNo + "</td>";
                m_szOut += "<td>" + szCodeName(vrResult[i].szLabFromCode, (uint)CODINGTABLE.DWCODETYPE.CODE_LABFROM) + "</td>";
                m_szOut += "<td>" + szCodeName(vrResult[i].szAcademicSubjectCode, (uint)CODINGTABLE.DWCODETYPE.CODE_ACADEMICSUBJECT) + "</td>";
                m_szOut += "<td>" + szCodeName(vrResult[i].szLabLevelCode, (uint)CODINGTABLE.DWCODETYPE.CODE_LABLEVEL) + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
             
                string szMemberName=GetGroupMemberName((uint)vrResult[i].dwManGroupID);
                m_szOut += "<td>" + szMemberName + "</td>";
                XmlCtrl.XmlInfo info = xmlCtrl.GetXmlContent(vrResult[i].dwRoomID.ToString(), "hard");
                if (info.content != null && info.content.Trim() != "")
                {
                    m_szOut += "<td class='InfoLabBtn'  title='查看平面图'>" + "<img width='25px' src='../../../themes/icon_s/7.png'/>" + "</td>";
                }
                else
                {
                    m_szOut += "<td class='InfoLabBtn' title='插入平面图'>＋</td>";
                }
                info = xmlCtrl.GetXmlContent(vrResult[i].dwRoomID.ToString(), "hard2");
                if (info.content != null && info.content.Trim() != "")
                {
                    m_szOut += "<td  class='InfoLabBtn2'  title='查看插图'>" + "<img width='25px' src='../../../themes/icon_s/7.png'/>" + "</td>";
                }
                else
                {
                    m_szOut += "<td class='InfoLabBtn2'  title='插入插图'>" + "＋" + "</td>";
                }
                m_szOut += "<td style='text-align:center;'><div class='OPTD class1'></div></td>";
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            } 
        }
        PutBackValue();
    }
    private void DelRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIROOM room = new UNIROOM();
        if (GetRoomID(szID, out room))
        {
            UNILAB labDel = new UNILAB();
            labDel.dwLabID = room.dwLabID;
            uResponse = m_Request.Device.LabDel(labDel);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                room.dwRoomID = Parse(szID);
                uResponse = m_Request.Device.RoomDel(room);

                if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
                }
            }
           
        }
    }
}
