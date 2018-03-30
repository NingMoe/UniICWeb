<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewResvRule.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwRuleSN" name="dwRuleSN" type="hidden" />
          <input type="hidden" id="dwDeptID" name="dwDeptID" value="0" />
        <input id="dwDevKind" name="dwDevKind" type="hidden" />
        <input id="kind" name="kind" type="hidden" />
        <table>
                <tr>
                    <th class="tdheadRight">�������ƣ�</th>
                    <td colspan="3" class="tdContextLeft">
                        <input id="szRuleName" name="szRuleName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th class="tdheadRight">��ݣ�</th>
                    <td class="tdContextLeft">
                        <select id="dwIdent"><%=m_szIdent%></select></td>
                   <th class="tdheadRight">������Ա�飺</th>
                    <td class="tdContextLeft">
                       <select id="dwGroupID" name="dwGroupID"><%=m_szGroup%></select></td>
              
                </tr>
                <tr>
                    <th class="tdheadRight">���ƣ�</th>
                    <td colspan="3" class="tdContextLeft" ><%=m_Limit %></td>
                </tr>
            <tr>
                  <th class="tdheadRight">ԤԼ��;��</th>
                    <td colspan="3"><%=m_szResvPurpose %></td>
            </tr>
                <tr>
                    <th class="tdheadRight">���ȼ���</th>
                    <td class="tdContextLeft">
                        <select id="dwPriority" name="dwPriority"><%=m_Priority %></select></td>
                    <th class="tdheadRight"><%=ConfigConst.GCKindName %>��</th>
                    <td class="tdContextLeft">
                           <input id="szDevKindName" name="szDevKindName" /></td>
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
                        <input id="dwEarlyInTime" name="dwEarlyInTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
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
                    <th class="tdheadRight">��Чǰ֪ͨʱ��(����)��</th>
                    <td class="tdContextLeft" colspan="3">
                        <input id="dwResvBeforeNoticeTime" name="dwResvBeforeNoticeTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                 
                </tr>
            
             <tr>
                <th  title="�������ѡ������ϵͳ��һ��ԤԼ��ͬһʱ��Ͳ�����ԤԼ���������Ӧ����ϵͳԤԼ���������ԤԼ�ˡ���λ���Ͳ���ԤԼ���޼䣬����λ����ѡ������޼�Ϊ�����������ڵ���ϵͳ">��ϵͳ��ͻ��飺</th>
                <td colspan="3"><%=USEFORSYS %></td>
            </tr>
                <tr>
                    <td></td>
                     <td class="tdContextLeft" colspan="3">������ǰ��
                        <input id="dwLatestResvTime" name="dwLatestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="0" />Сʱ/
                        <select id="dwLatestResvTimeMin" name="dwLatestResvTimeMin" style="width:50px;">
                            <option value="0">0</option>
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                            <option value="40">40</option>
                            <option value="50">50</option>
                        </select>����
                        ��<input id="dwEarliestResvTime" name="dwEarliestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="86400" /> Сʱ/
                          <select id="dwEarliestResvTimeMin" name="dwEarliestResvTimeMin" style="width:50px;">
                            <option value="0">0</option>
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                            <option value="40">40</option>
                            <option value="50">50</option>
                        </select>����
                       �ſ�ԤԼ</td>

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
        $("#Cancel").button().click(Dlg_Cancel);
        var vKind = null;
        setTimeout(function () {
            vKind = $("#kind").val();
            if (vKind == 1024) {
                vKind = 1;//�ռ�
            }
            else if (vKind == 128) {
                vKind = 2;//����
            }
            else if (vKind == 64) {
                vKind = 8;//��λ
            }
            else {
                vKind = null;
            }
            AutoDevKind($("#szDevKindName"), 2, $("#dwDevKind"), vKind, true);
        }, 1000);
  
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
