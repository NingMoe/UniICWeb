<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAttend2.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server"  enctype="multipart/form-data">
    <input type="hidden" id="dwAttendantID2" name="dwAttendantID2"/>
    <input type="hidden" id="dwDevID" name="dwDevID"/>
    <input type="hidden" id="dwLabID" name="dwLabID"/>
    <input type="hidden" id="dwOldKeeperID" name="dwOldKeeperID" />
    <input type="hidden" id="szOlderName" name="szOlderName" />
    <input type="hidden" id="szOlderRoomName" name="szOlderRoomName" />
    <input type="hidden" id="dwOldRoomID" name="dwOldRoomID" />
    <input type="hidden" id="dwNewKeeperID" name="dwNewKeeperID" />
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <table>
            <tr>
                <th>�ʲ����:</th>
                <td><div id="szAssertSN"></div></td>
                <th>�ʲ�����:</th>
                <td><div id="szDevName"></div></td>
            </tr>
            <tr>
                <th>��<%=ConfigConst.GCRoomName%>:</th>
                <td><select id="dwNewRoomID" name="dwNewRoomID"><%=m_szRoom %>:</select></td>
               <th>��������ѧ����:</th>
                <td><input type="text" id="szLogonName" name="szLogonName" /></td>
            </tr>
             <tr>
                <th>����˵��:</th>
                <td><input type="file" name="fileurl" id="fileurl" size="45" class="validate[required]" /></td>
            </tr>
            <tr>
                <td style="text-align:center" colspan="4"><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button>
                    
                    <button type="button" id="print">��ӡ</button>
                </td>

            </tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {  
       AutoUserByLogonname($("#szLogonName"), 2, $("#dwNewKeeperID"), null, null, null);
       
       $("#OK,#print").button();
       $("#print").click(function () {
           window.print();
       });
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
