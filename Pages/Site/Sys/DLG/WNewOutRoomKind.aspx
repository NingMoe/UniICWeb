<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="WNewOutRoomKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />
          
                    <input id="dwUsableNum" name="dwUsableNum" type="hidden" value="0" />
            <table>

                <tr>
                    <th>名称：</th>
                    <td><input id="szKindName" name="szKindName" class="validate[required]" /></td>
                     <th>属性：</th>
                    <td><%=m_KindProperty %></td>
                </tr>   
                <tr>
                     <th id="thminsuers">最少使用人数：</th>
                    <td><input id="dwMinUsers" name="dwMinUsers" type="text" value="1" /></td>
                     <th id="thmaxsuers">最多使用人数：</th>
                    <td><input id="dwMaxUsers" name="dwMaxUsers" type="text" value="1" /></td>
                </tr>          
                    <tr>
                       <%if(ConfigConst.GCKindAndClass==0) {%>
                     <th>所属<%=ConfigConst.GCClassName %>：</th>
                    <td colspan="3"><select id="dwClassID" name="dwClassID" ><%=m_dwDevClass %></select></td>
                  <%} %>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
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
            $("input[name='dwProperty']").click(function () {
                var b = false;
                $('input[name="dwProperty"]:checked').each(function () {
                    if ($(this).val() == "2") {
                        b=true;
                    }
                });
                if (b == true) {
                    $("#thminsuers").html("单次申请最少人数");
                    $("#thmaxsuers").html("资源可容纳总人数");
                }
                else {
                    $("#thminsuers").html("最少使用人数");
                    $("#thmaxsuers").html("最多使用人数");
                }
            });
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
