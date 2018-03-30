<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetDevice.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" />
        <div class="formtable">
            <table class="ListTbl2">
                <tr>
                    <th>��ţ�</th>
                    <td> <div name="dwDevSN"></div></td>
                </tr>
                <tr>
                    <th>���������</th>
                    <td>
                        <div name="szPCName"></div></td>
                </tr>
                <tr>
                    <th>ԭ��ϵ�кţ�</th>
                    <td>
                        <div name="szOriginSN" ></div></td>
                </tr>
             
                <tr>
                    <th>����<%=ConfigConst.GCLabName %>��</th>
                    <td><div name="szLabName" ></div></td>
                </tr>
                 <tr>
                    <th>����<%=ConfigConst.GCRoomName %>��</th>
                    <td><div name="szRoomName" ></div></td>
                </tr>
                 <tr>
                    <th>�������ͣ�</th>
                    <td><div name="szKindName" ></div></td>
                </tr>
                <tr>
                    <th>���Ʒ�ʽ��</th>
                    <td><%=m_szDoorCtrl %></td>
                </tr>
              
                <tr>
                    <th>��ע��</th>
                    <td>
                        <div name="szMemo" ></div></td>
                </tr>
                <tr>
                    <td colspan="2" class="tblBtn">
                       
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
        <%if (bSet)
          {%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#szManName2").autocomplete({
            source: "searchAccount.aspx",
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
            source: "searchAccount.aspx",
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
