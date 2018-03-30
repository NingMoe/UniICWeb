<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewUniTeacher.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
         <input type="hidden" id="dwAccNo" name="dwAccNo" />
            <table>
                <tr>
                    <th>��ʦ���ţ�</th>
                    <td>
                        <input id="szPID" name="szPID" class="validate[required]" /></td>
                    <th>��ʦ������</th>
                    <td>
                        <input id="szTrueName" name="szTrueName" class="validate[required]" /></td>
                </tr>
                 <tr>
                    <th>�ֻ���</th>
                    <td>
                        <input type="text" id="szHandPhone" name="szHandPhone" /></td>
                    <th>���䣺</th>
                    <td>
                        <input type="text" id="szEmail" name="szEmail" /></td>
                </tr>
                <tr>
                    <th>���ڲ��ţ�</th>
                    <td>
                        <input type="text" name="szDeptName" readonly="readonly" disabled="disabled" id="szDeptName"/></td>
                   
                </tr>
               
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
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
            setTimeout(function () {
                if ($("#dwAccNo").val() != "")
                {
                    $("#szTrueName").attr("readonly", "readonly");
                    $("#szTrueName").attr("disabled", "disabled");
                    $("#szPID").attr("readonly", "readonly");
                    $("#szPID").attr("disabled", "disabled");

                }
            },200);
            AutoUserByName($("#szTrueName"), 2, $("#dwAccNo"), null, null, null);
            AutoUserByLogonname($("#szPID"), 2, $("#dwAccNo"), null, null, null);
            $("#szPID").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#szTrueName").attr("readonly", "readonly");
                    $("#szTrueName").attr("disabled", "disabled");

                    $("#dwAccNo").val(ui.item.id);
                    $("#szPID").val(ui.item.szLogonName);
                    $("#szTrueName").val(ui.item.szTrueName);
                    $("#szDeptName").val(ui.item.szDeptName);
                    $("#szHandPhone").val(ui.item.szHandPhone);
                    $("#szEmail").val(ui.item.szEmail);
                }, 200);
            });
            $("#szTrueName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#szPID").attr("readonly", "readonly");
                    $("#szPID").attr("disabled", "disabled");

                    $("#dwAccNo").val(ui.item.id);
                    $("#szPID").val(ui.item.szLogonName);
                    $("#szTrueName").val(ui.item.szTrueName);
                    $("#szDeptName").val(ui.item.szDeptName);
                    $("#szHandPhone").val(ui.item.szHandPhone);
                    $("#szEmail").val(ui.item.szEmail);
                }, 200);
            });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
       
    });
    </script>
</asp:Content>
