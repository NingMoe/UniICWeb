<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="checkActivityPlan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwActivityPlanID" id="dwActivityPlanID" />
        <input type="hidden" name="dwResvID" id="dwResvID" />
        <div class="formtable">
            <table>
                <tr>
                    <th style="width:100px">����ƣ�</th>
                    <td>
                        <div id="szActivityPlanName" /></td>
                    <th  style="width:100px">�ռ����ƣ�</th>
                    <td>
                      <div id="devName"></div></td>
                </tr>
                <tr>
                    <th>���쵥λ��</th>
                    <td>
                        <div id="szHostUnit"  /></td>
                    <th>�а쵥λ��</th>
                    <td>
                        <div id="szOrganizer" /></td>
                </tr>
                <tr>
                    <th>�����ˣ�</th>
                    <td>
                        <div id="szPresenter"  /></td>
                     <th>������Ҫ��</th>
                    <td>
                        <div id="szDesiredUser"  /></td>
                </tr>
                <tr>
                     <th>��ϵ�ˣ�</th>
                    <td>
                        <div id="szContact" /></td>
                      <th>�绰��</th>
                    <td>
                        <div id="szTel" /></td>
                </tr>
                <tr>
                    <th>���䣺</th>
                    <td>
                        <div id="szEmail" /></td>
                    <th>�ֻ���</th>
                    <td>
                        <div id="szHandPhone" /></td>
                </tr>
                 <tr>
                      <th>���ٱ���������</th>
                    <td>
                        <div id="dwMinUsers" /></td>
                    <th>��౨��������</th>
                    <td>
                        <div id="dwMaxUsers" /></td>
                </tr>
                <tr>
                        <th>��ֹ�������ڣ�</th>
                    <td>
                        <div id="dwEnrollDeadline" /></td>
                    <th>����ڣ�</th>
                    <td>
                        <div id="dwActivityDate" /></td>
                </tr>
                <tr>
                      <th>��ʼʱ�䣺</th>
                    <td>
                        <div id="dwBeginTime" /></td>
                    <th>����ʱ�䣺</th>
                    <td>
                        <div  id="dwEndTime" /></td>
                </tr>
                <tr>
                    <th>���飺</th>
                    <td colspan="3"><div id="szIntroInfo"></div></td>
                </tr>
                <tr>
                    <th>״̬��</th>
                    <td>
                    <select id="dwStatus" name="dwStatus">
                        <option value="256">������</option>
                        <option value="512">����</option>
                        <option value="1024">����</option>
                    </select>
                        </td>
                    <th>
                        ���븽��
                    </th>
                    <td>
                        <a target="_blank" style="color:blue;text-decoration:underline" href="..\..\..\..\ClientWeb\upload\UpLoadFile\<%=szFile %>">�������</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                    <button type="submit" id="OK">���ͨ��</button>
                        <button type="button" id="Cancel">��˲�ͨ��</button></td>
                </tr>
            </table>
        </div>
         <div id="divNoOK">
           <table>
               <tr>
                   <td>��������</td>
                   <td>
                        <input type="text" name="szCheckInfo" id="szCheckInfo" title="��˲�ͨ����������ԭ��" style="width:250px" />
                   </td>
                   <td>
                       <input type="button" id="btnNOOK" value="ȷ����ͨ��" style="width:90px" />
                       <input type="button" id="btnClose" value="�ر�" style="width:80px" />
                   </td>
               </tr>
           </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#divNoOK").hide();
            $("#OK").button();
            $("#Cancel").button();
            $("#Cancel").click(function () {
                $("#divNoOK").show();
            });
            $("#btnClose").button().click(Dlg_Cancel);
            $("#btnNOOK").button();
            $("#btnNOOK").click(function () {
                var szCheckInfo = $("#szCheckInfo").val();
                var dwstate = $("#dwStatus").val();
                if (szCheckInfo == "") {
                    return;
                }
                var id = $("#dwActivityPlanID").val();
                $.get(
                         "../../ajaxpage/checkActivityplan.aspx",
                         { szCheckInfo: szCheckInfo, id: id, statue: dwstate },
                         function (data) {
                             if (data == "success") {
                                 MessageBox("��˲�ͨ��", "��ʾ", 3, function () { Dlg_OK() });
                             }
                             else {
                                 MessageBox("���ʧ��" + data, "��ʾ", 3, function () { Dlg_OK() });
                             }

                         }
                       );

            });
        });
    </script>
</asp:Content>
