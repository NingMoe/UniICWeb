<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewResvRule.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwRuleSN" name="dwRuleSN" type="hidden" />
        <input id="dwDevKind" name="dwDevKind" type="hidden" value="0" />
        <table>
                <tr>
                    <th class="tdheadRight">规则名称：</th>
                    <td class="tdContextLeft">
                        <input id="szRuleName" name="szRuleName" class="validate[required]" /></td>
                    <th class="tdheadRight">特殊人员组：</th>
                    <td class="tdContextLeft">
                       <select id="dwGroupID" name="dwGroupID"><%=m_szGroup%></select></td>
                </tr>
                <tr>
                    <th class="tdheadRight">身份：</th>
                    <td class="tdContextLeft">
                        <select id="dwIdent" name="dwIdent"><%=m_szIdent%></select></td>
                    <th class="tdheadRight"><%=ConfigConst.GCDeptName %>:</th>
                    <td class="tdContextLeft">
                          <select id="dwDeptID" name="dwDeptID"><%=m_szDept%></select>
                      </td>
                </tr>
                <tr>
                    <th class="tdheadRight">限制：</th>
                    <td class="tdContextLeft" style="width: 350px"><%=m_Limit %></td>
                    <th class="tdheadRight">预约用途：</th>
                    <td><%=m_szResvPurpose %></td>
                </tr>
                <tr>
                      <th class="tdheadRight"><%=ConfigConst.GCKindName %>：</th>
                    <td class="tdContextLeft">
                           <input id="szDevKindName" name="szDevKindName" value="全部" /></td>
                      <th class="tdheadRight"><%=ConfigConst.GCDevName %>：</th>
                    <td class="tdContextLeft">   
                   <select  id="dwDevID" name="dwDevID" ><%=m_szDevice %></select>
                        </td>
                </tr>
            <tr>
                 <th class="tdheadRight">优先级：</th>
                    <td colspan="3" class="tdContextLeft">
                        <select id="dwPriority" name="dwPriority"><%=m_Priority %></select></td>
            </tr>
                <tr>
                    <th class="tdheadRight">最短预约时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwMinResvTime" name="dwMinResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">最长预约时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwMaxResvTime" name="dwMaxResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">提前生效时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwEarlyInTime" name="dwResvBeforeNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">生效不来通知时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwResvAfterNoticeTime" name="dwResvAfterNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">结束提前通知时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwResvEndNoticeTime" name="dwResvEndNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">连续预约时间间隔(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwSeriesTimeLimit" name="dwSeriesTimeLimit" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">预约不来取消预约时间(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwCancelTime" name="dwCancelTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">结束前指定时间内可新建(分钟)：</th>
                    <td class="tdContextLeft">
                        <input id="dwResvEndNewTime" name="dwResvEndNewTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="tdContextLeft" colspan="3">必须提前：<input id="dwLatestResvTime" name="dwLatestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="0" />天到<input id="dwEarliestResvTime" name="dwEarliestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="6" />天才可预约</td>
                </tr>
           
                <tr>
                    <td colspan="42" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
      
        .tdheadRight {
            width: 240px;
            text-align:right;
        }

        .tdContextLeft {
            width:180px;
            text-align:left;
        }
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {             
        $("#OK").button();
        $("#dwGroupID").change(function () {
            if ($(this).val() != "0") {
                $("#dwDeptID").val("0");
                $("#dwDeptID").attr("disabled", "disabled");
            } else {
                $("#dwDeptID").removeAttr("disabled");
            }
        });
        $("#dwDevID").change(function () {
            if ($(this).val() != "0") {
                $("#szDevKindName").val("全部");
                $("#dwDevKind").val("0");
                $("#szDevKindName").attr("disabled", "disabled");
            } else {
                $("#szDevKindName").removeAttr("disabled");
            }
        });
        $("#szDevKindName").on("autocompleteselect", function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        $("#szDevKindName").val(ui.item.label);
                        $("#dwDevKind").val(ui.item.id);

                        if (ui.item.id != "0") {
                            $("#dwDevID").val("0");
                            $("#dwDevID").attr("disabled", "disabled");
                        } else {
                            $("#dwDevID").removeAttr("disabled");
                        } }, 5);
                }
            }
        });

        if ($("#dwGroupID").val() != "0")
        {
            $("#dwDeptID").val("0");
            $("#dwDeptID").attr("disabled", "disabled");
        }
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDevKind($("#szDevKindName"), 2, $("#dwDevKind"), null, true);
        
        $("#szDeptName").autocomplete({
            source: "../../data/searchDept.aspx",
            minLength: 0,
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szDeptName").val(ui.item.label);
                        $("#dwDeptID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwDeptID").val("");
                    $("#szDeptName").val("");
                    ui.content.push({ label: " 未找到相匹配 " });
                }
            }
        }).blur(function () {
            if ($("#dwDeptID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szDeptName").val());
            }
        }).click(function () { $(this).autocomplete("search", ""); });
    });
</script>
</asp:Content>
