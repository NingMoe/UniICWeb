<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAttend.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server" enctype="multipart/form-data">
    <input type="hidden" id="dwAttendantID" name="dwAttendantID"/>
    <input type="hidden" id="dwDevID" name="dwDevID"/>
    <input type="hidden" id="dwLabID" name="dwLabID"/>
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <table>
            <tr>
                <th>�ʲ���ţ�</th>
                <td><div id="szAssertSN"></div></td>
                <th>�ʲ����ƣ�</th>
                <td><div id="szDevName"></div></td>
            </tr>
            <tr>
                 <th>����ʵ���ң�</th>
                <td><div id="szRoomName"></div></td>
                <th>�������ţ�</th>
                <td><div id="szDeptName"></div></td>
            </tr>
            <tr><th>�����ˣ�</th><td><input id="szAttendantName" name="szAttendantName" class="validate[required]"/></td>
                <th>˵����</th><td><input id="szMemo" name="szMemo"/></td></tr>
            <tr>
                <th>����˵��</th>
                <td><input type="file" name="fileurl" id="fileurl" size="45" class="validate[required]"/></td>
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
       AutoUserByName($("#szAttendantName"),2, $("#dwAttendantID"), null, null, null);
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
