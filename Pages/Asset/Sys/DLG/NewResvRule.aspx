<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewResvRule.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwRuleSN" name="dwRuleSN" type="hidden" />
        <input id="dwDevKind" name="dwDevKind" type="hidden" value="0" />
        <table>
                <tr>
                    <th class="tdheadRight">�������ƣ�</th>
                    <td class="tdContextLeft">
                        <input id="szRuleName" name="szRuleName" class="validate[required]" /></td>
                    <th class="tdheadRight">������Ա�飺</th>
                    <td class="tdContextLeft">
                       <select id="dwGroupID" name="dwGroupID"><%=m_szGroup%></select></td>
                </tr>
                <tr>
                    <th class="tdheadRight">��ݣ�</th>
                    <td class="tdContextLeft">
                        <select id="dwIdent" name="dwIdent"><%=m_szIdent%></select></td>
                    <th class="tdheadRight"><%=ConfigConst.GCDeptName %>:</th>
                    <td class="tdContextLeft">
                          <select id="dwDeptID" name="dwDeptID"><%=m_szDept%></select>
                      </td>
                </tr>
                <tr>
                    <th class="tdheadRight">���ƣ�</th>
                    <td class="tdContextLeft" style="width: 350px"><%=m_Limit %></td>
                    <th class="tdheadRight">ԤԼ��;��</th>
                    <td><%=m_szResvPurpose %></td>
                </tr>
                <tr>
                      <th class="tdheadRight"><%=ConfigConst.GCKindName %>��</th>
                    <td class="tdContextLeft">
                           <input id="szDevKindName" name="szDevKindName" value="ȫ��" /></td>
                      <th class="tdheadRight"><%=ConfigConst.GCDevName %>��</th>
                    <td class="tdContextLeft">   
                   <select  id="dwDevID" name="dwDevID" ><%=m_szDevice %></select>
                        </td>
                </tr>
            <tr>
                 <th class="tdheadRight">���ȼ���</th>
                    <td colspan="3" class="tdContextLeft">
                        <select id="dwPriority" name="dwPriority"><%=m_Priority %></select></td>
                  
            </tr>
                <tr>
                    <th class="tdheadRight">���ԤԼʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwMinResvTime" name="dwMinResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">�ԤԼʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwMaxResvTime" name="dwMaxResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">��ǰ��Чʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwEarlyInTime" name="dwResvBeforeNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">��Ч����֪ͨʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwResvAfterNoticeTime" name="dwResvAfterNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">������ǰ֪ͨʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwResvEndNoticeTime" name="dwResvEndNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">����ԤԼʱ����(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwSeriesTimeLimit" name="dwSeriesTimeLimit" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">ԤԼ����ȡ��ԤԼʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwCancelTime" name="dwCancelTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">����ǰָ��ʱ���ڿ��½�(����)��</th>
                    <td class="tdContextLeft">
                        <input id="dwResvEndNewTime" name="dwResvEndNewTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="tdContextLeft" colspan="3">������ǰ��<input id="dwLatestResvTime" name="dwLatestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="0" />�쵽<input id="dwEarliestResvTime" name="dwEarliestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="6" />��ſ�ԤԼ</td>
                </tr>
                <tr>
                    <td colspan="42" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
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
                $("#szDevKindName").val("ȫ��");
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
                    ui.content.push({ label: " δ�ҵ���ƥ�� " });
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
