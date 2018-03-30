<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewAccount.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwAccNo" name="dwAccNo" />
        <input type="hidden" id="dwClassID" name="dwClassID" />
        <input type="hidden" id="dwKind" name="dwKind" value="1" />
        <input type="hidden" id="dwStatus" name="dwStatus" value="65536" />
        <div class="formtable">
         
            <table>
                <tr>
                    <th>ѧ���ţ�</th>
                    <td>
                        <input id="szLogonName" name="szLogonName" class="validate[required]" /></td>
                    <th>������</th>
                    <td>
                        <input id="szTrueName" name="szTrueName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>�༶��</th>
                    <td>
                        <input id="szClassName" name="szClassName"  /></td>
                   <th>��� </th>
                       <td><select id="dwIdent" name="dwIdent"><%=szIdent %></select></td>
                 
                </tr>
               <tr>
                   <th>�ֻ�;</th>
                   <td>  <input id="szHandPhone" name="szHandPhone"  /></td>
                   <th>����;</th>
                   <td>  <input id="szEmail" name="szEmail"  /></td>
               </tr>
                <tr>
                     <th>�Ա�</th>
                       <td><select id="dwSex" name="dwSex"><%=szSex %></select></td>
                    <th>��ע��</th>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
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
            AutoClass($("#szClassName"), 2, $("#dwClassID"), null);
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#szManName2").autocomplete({
            source: "../../data/searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szManName").val(ui.item.label);
                        $("#szManName2").val(ui.item.label);
                        $("#dwManagerID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwManagerID").val("");
                    $("#szManName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwManagerID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szManName").val());
            }
        });

        $("#szAttendantName2").autocomplete({
            source: "../../data/searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szAttendantName").val(ui.item.label);
                        $("#szAttendantName2").val(ui.item.label);
                        $("#dwAttendantID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwAttendantID").val("");
                    $("#szAttendantName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwAttendantID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szAttendantName").val());
            }
        });

        setTimeout(function () {
            if ($("#dwManagerID").val() == "") {
                $("#szManName2").val("");
            } else {
                $("#szManName2").val($("#szManName").val());
            }

            if ($("#dwAttendantID").val() == "") {
                $("#szAttendantName2").val("");
            } else {
                $("#szAttendantName2").val($("#szAttendantName").val());
            }
        }, 1);
    });
    </script>
</asp:Content>
