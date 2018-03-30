<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="AuthCheckGet.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <input type="hidden" name="dwApplyID" id="dwApplyID" />
            <table class="DlgListTbl">
                <tr>
                    <th>����������:</th>
                    <td>
                        <div id="szTrueName"></div>
                    </td>
                    <th>���������ڲ���:</th>
                    <td>
                        <div id="szDeptName"></div>
                    </td>
                </tr>
                <tr>
                    <th>�ֻ�:</th>
                    <td>
                        <div id="szHandPhone"></div>
                    </td>
                    <th>�绰:</th>
                    <td>
                        <div id="szEmail"></div>
                    </td>
                </tr>
                <tr>
                    <th>��ʦ:</th>
                    <td>
                        <div id="szTutorName"></div>
                    </td>
                    <th>����˵��:</th>
                    <td>
                        <div id="szApplyInfo"></div>
                    </td>
                </tr>
                <tr>
                     <td>��������</td>
                   <td colspan="3">
                        <div id="szCheckInfo" title="��˲�ͨ����������ԭ��" style="width:250px"></div>
                   </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                        <button type="button" id="Cancel">�ر�</button></td>
                </tr>
            </table>
        </div>
         <div id="divNoOK">
           <table>
               <tr>
                  
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

        <%if (bSet)
          {%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#divNoOK").hide();
        $("#OK").button();
        $("#Cancel").button();
        $("#Cancel").click(function () {
            $("#divNoOK").show();
        });
        $("#Cancel").button().click(Dlg_Cancel);
        $("#btnNOOK").button();
        $("#btnNOOK").click(function () {
            var szCheckInfo = $("#szCheckInfo").val();
            if (szCheckInfo == "") {
                return;
            }
            var id = $("#dwApplyID").val();
            $.get(
                     "../../ajaxpage/checkReserach.aspx",
                     { szCheckInfo: szCheckInfo, id: id },
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
