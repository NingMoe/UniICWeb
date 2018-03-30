<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDevice.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" />
        <div class="formtable">
            <table>
                <tr>
                    <td>��ţ�</td>
                    <td> <input id="dwDevSN" name="dwDevSN" class="validate[required]" /></td>
                </tr>
                <tr>
                    <td>���������</td>
                    <td>
                        <input id="szPCName" name="szPCName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <td>ԭ��ϵ�кţ�</td>
                    <td>
                        <input id="szOriginSN" name="szOriginSN" /></td>
                </tr>
             
                <tr>
                    <td>����<%=ConfigConst.GCLabName %>��</td>
                    <td>
                        <select id="dwLabID" name="dwLabID">
                            <%=m_szLab %>
                        </select></td>
                </tr>
                 <tr>
                    <td>����<%=ConfigConst.GCRoomName %>��</td>
                    <td>
                        <select id="dwRoomID" name="dwRoomID">
                            <%=m_szRoom %>
                        </select></td>
                </tr>
                 <tr>
                    <td>�������ͣ�</td>
                    <td>
                        <select id="dwKindID" name="dwKindID">
                            <%=m_szDevKind %>
                        </select></td>
                </tr>
                <tr>
                    <td>���Ʒ�ʽ��</td>
                    <td>
                        <select id="dwCtrlMode" name="dwCtrlMode">
                               <option value="1">��¼��֤</option>
                             <option value="2">ˢ����֤</option>
                            <option value="3">�˹�����</option>
                            <%=m_szDoorCtrl %>
                        </select></td>
                </tr>
              
                <tr>
                    <td>��ע��</td>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtitle {
            padding: 6px;
            background: #d0d0d0;
            height: 30px;
            color: #fff;
            font-size: 20px;
        }

        .formtable table {
            text-align: center;
            margin: auto;
        }

        td {
            padding: 6px;
        }

        input, select {
            width: 200px;
        }
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
