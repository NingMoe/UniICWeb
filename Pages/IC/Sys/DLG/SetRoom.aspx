<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwRoomID" name="dwRoomID" />

        <input id="dwDeptID" name="dwDeptID" type="hidden" />
        <input type="hidden" id="dwManGroupID" name="dwManGroupID" />

        <div class="formtable">
            <table>
                <tr>
                    <th>编号：</th>
                    <td>
                        <input type="text" id="szRoomNo" name="szRoomNo" /></td>

                    <th>名称：</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>所属<%=ConfigConst.GCDeptName %>：</th>
                    <td>
                        <input type="text" id="szDeptName" name="szDeptName" /></td>
                    <th>开放规则：</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
                </tr>
                <tr>
                    <th>授权IP地址范围：</th>
                    <td>
                        <input type="text" id="szMAIP" name="szMAIP" /></td>
                    <th>智能监控方式：</th>
                    <td>
                        <select id="propyMode" name="propyMode">
                            <option value="0">无限制</option>
                            <option value="1">通道机</option>
                            <option value="2">IP地址</option>
                        </select></td>
                </tr>
                <%if (ConfigConst.GCICLabRoom == 1)
                    {%>
                <tr>
                    <th>所在楼层：</th>
                    <td>
                        <select id="dwLabID" name="dwLabID"><%=m_szLab %></select></td>
                    <td colspan="2" align="left" style="text-align: left">
                        <label>
                            <input name="prop" value="8388608" type="checkbox" id="prop" />网站预约不开放</label>
                        
                    </td>
                </tr>

                <% }%>
                <tr>
                    <th>楼层编号:</th>
                    <td>
                        <select id="floorNOPre" name="floorNOPre">
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                        </select>
                    </td>
                    <th>预约台显示顺序:
                    </th>

                    <td>
                        <select id="floorNONext" name="floorNONext">
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                            <option value="13">13</option>
                            <option value="14">14</option>
                            <option value="15">15</option>
                        </select>
                    </td>

                </tr>
                <tr>
                    <th>校区：</th>
                    <td>
                        <select id="dwCampusID" name="dwCampusID"><%=szCamp %></select></td>
                    <th>网站显示顺序：</th>
                    <td>
                        <select id="dwCreateDate" name="dwCreateDate">
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                            <option value="13">13</option>
                            <option value="14">14</option>
                            <option value="15">15</option>
                            <option value="16">16</option>
                            <option value="17">17</option>
                            <option value="18">18</option>
                            <option value="19">19</option>
                        </select></td>
                </tr>
                <tr>
                    <th>二维码限制:</th>
                    <td colspan="3">
                        <label>
                            <input name="scprop" value="16" type="checkbox" id="noresv" />不支持手机扫码预约</label>

                        <label>
                            <input name="scprop" value="32" type="checkbox" id="nocheck" />不支持手机扫码签到</label>

                        <label>
                            <input name="scprop" value="64" type="checkbox" id="noleaveout" />不支持手机扫码暂时离开</label>
                        
                        <label>
                            <input name="scprop" value="128" type="checkbox" id="noend" />不支持手机扫码结束使用</label>

                    </td>
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
            AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
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
