<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckDevUnuse.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwApproveID" name="dwApproveID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>������</th>
                    <td>
                        <div id="szOOSInfo" name="szOOSInfo"></div>
                    </td>
                </tr>
                  <tr>
                    <th>�������ڣ�</th>
                    <td>
                        <div id="dwApplyDate2" name="dwApplyDate2"></div>
                    </td>
                </tr>
                  <tr>
                    <th>�����ˣ�</th>
                    <td>
                        <div id="szApplyName" name="szApplyName"></div>
                    </td>
                </tr>
                <tr>
                    <th>�������</th>
                    <td>
                 <select name="dwstae" id="dwstae">
                     <option value="2">����ͨ��</option>
                     <option value="4">������ͨ��</option>
                 </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                    <a target="_blank" href="<%=szHref %>">�鿴����</a>
                    </td>
                </tr>
              
                <tr>
                    <th></th>
                    <td>
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">�ر�</button></td>
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
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoUserByName($("#szApproveName"), 2, $("#dwApproveID"), null, null, null);
            AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, null);
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
