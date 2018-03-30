<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetDevice.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" />
        <div class="formtable">
            <table>
                <tr>
                    <td>编号：</td>
                    <td> <input id="dwDevSN" name="dwDevSN" class="validate[required]" /></td>
                </tr>
                <tr>
                    <td>计算机名：</td>
                    <td>
                        <input id="szPCName" name="szPCName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <td>原厂系列号：</td>
                    <td>
                        <input id="szOriginSN" name="szOriginSN" /></td>
                </tr>
             
                <tr>
                    <td>所属<%=ConfigConst.GCLabName %>：</td>
                    <td>
                        <select id="dwLabID" name="dwLabID">
                            <%=m_szLab %>
                        </select></td>
                </tr>
                 <tr>
                    <td>所属<%=ConfigConst.GCRoomName %>：</td>
                    <td>
                        <select id="dwRoomID" name="dwRoomID">
                            <%=m_szRoom %>
                        </select></td>
                </tr>
                 <tr>
                    <td>所属类型：</td>
                    <td>
                        <select id="dwKindID" name="dwKindID">
                            <%=m_szDevKind %>
                        </select></td>
                </tr>
                <tr>
                    <td>控制方式：</td>
                    <td>
                        <select id="dwCtrlMode" name="dwCtrlMode">
                               <option value="1">登录认证</option>
                             <option value="2">刷卡认证</option>
                            <option value="3">人工管理</option>
                            <%=m_szDoorCtrl %>
                        </select></td>
                </tr>
              
                <tr>
                    <td>备注：</td>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                       
                        <button type="button" id="Cancel">关闭</button></td>
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
