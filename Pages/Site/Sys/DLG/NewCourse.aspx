<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCourse.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
         
            <table>
                <tr>
                    <th>课程编号：</th>
                    <td>
                        <input id="szCourseCode" name="szCourseCode" class="validate[required]" /></td>
                    <th>课程名称：</th>
                    <td>
                        <input id="szCourseName" name="szCourseName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>实验学时数：</th>
                    <td>
                        <input id="dwTestHour" name="dwTestHour" class="validate[required,validate[custom[onlyNumber]]" /></td>
                    <th>实验次数：</th>
                    <td>
                        <input id="dwTestNum" name="dwTestNum" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>课程属性：</th>
                    <td>
                        <select id="dwCourseProperty" name="dwCourseProperty"><%=m_Property %></select></td>
                    <th>实践学时数：</th>
                    <td>
                        <input id="dwPracticeHour" name="dwPracticeHour" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>理论学时数：</th>
                    <td>
                        <input id="dwTheoryHour" name="dwTheoryHour" class="validate[required,validate[custom[onlyNumber]]" /></td>
                    <th>学分：</th>
                    <td>
                        <input id="dwCreditHour" name="dwCreditHour" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>备注：</th>
                    <td colspan="3">
                        <input id="szMemo" name="szMemo" /></td>
                </tr>

                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
                    ui.content.push({ label: " 未找到配置项 " });
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
                    ui.content.push({ label: " 未找到配置项 " });
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
