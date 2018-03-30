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
                    <th>��ţ�</th>
                    <td>
                        <input type="text" id="szRoomNo" name="szRoomNo" /></td>

                    <th>���ƣ�</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>����<%=ConfigConst.GCDeptName %>��</th>
                    <td>
                        <input type="text" id="szDeptName" name="szDeptName" /></td>
                    <th>���Ź���</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
                </tr>
                <tr>
                    <th>��ȨIP��ַ��Χ��</th>
                    <td>
                        <input type="text" id="szMAIP" name="szMAIP" /></td>
                    <th>���ܼ�ط�ʽ��</th>
                    <td>
                        <select id="propyMode" name="propyMode">
                            <option value="0">������</option>
                            <option value="1">ͨ����</option>
                            <option value="2">IP��ַ</option>
                        </select></td>
                </tr>
                <%if (ConfigConst.GCICLabRoom == 1)
                    {%>
                <tr>
                    <th>����¥�㣺</th>
                    <td>
                        <select id="dwLabID" name="dwLabID"><%=m_szLab %></select></td>
                    <td colspan="2" align="left" style="text-align: left">
                        <label>
                            <input name="prop" value="8388608" type="checkbox" id="prop" />��վԤԼ������</label>
                        
                    </td>
                </tr>

                <% }%>
                <tr>
                    <th>¥����:</th>
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
                    <th>ԤԼ̨��ʾ˳��:
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
                    <th>У����</th>
                    <td>
                        <select id="dwCampusID" name="dwCampusID"><%=szCamp %></select></td>
                    <th>��վ��ʾ˳��</th>
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
                    <th>��ά������:</th>
                    <td colspan="3">
                        <label>
                            <input name="scprop" value="16" type="checkbox" id="noresv" />��֧���ֻ�ɨ��ԤԼ</label>

                        <label>
                            <input name="scprop" value="32" type="checkbox" id="nocheck" />��֧���ֻ�ɨ��ǩ��</label>

                        <label>
                            <input name="scprop" value="64" type="checkbox" id="noleaveout" />��֧���ֻ�ɨ����ʱ�뿪</label>
                        
                        <label>
                            <input name="scprop" value="128" type="checkbox" id="noend" />��֧���ֻ�ɨ�����ʹ��</label>

                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
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
