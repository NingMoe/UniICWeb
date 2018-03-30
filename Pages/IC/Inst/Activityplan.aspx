<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Activityplan.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�����</h2>
        <div class="toolbar">
                      <div class="FixBtn"><a>�½��</a></div>
         
        </div>
         <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                  <tr>
                        <th>�����:</th>
                        <td colspan="3">
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>���״̬:</th>
                        <td>
                             <label><input name="dwStatue1" class="enum" type="radio" checked="checked" value="0"> ȫ��</label>
                            <label><input name="dwStatue1" class="enum" type="radio" value="2"> ���ͨ��</label>
                            <label><input name="dwStatue1" class="enum" type="radio"value="4"> ��ͨ��</label>
                           </td>
                          <th>����״̬:</th>
                        <td>
                            <label><input name="dwStatue2" class="enum" type="radio" checked="checked" value="0"> ȫ��</label>
                              <label><input name="dwStatue2" class="enum" type="radio" value="256"> ������</label>
                              <label><input name="dwStatue2" class="enum" type="radio" value="512"> ����</label>
                           </td>
                        <tr>
                        <td colspan="4" style="text-align:center">
                             <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" />
                             <input type="button" id="btnExport" value="����" style="height: 25px" />
                            </td>
                    </tr>
                    </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szActivityPlanName">�����</th>
                        <th>�ռ�����</th>
                        <th>������</th>
                        <th>��ϵ��</th>
                        <th>�绰</th>
                        <th>�ֻ�</th>
                        <th>�����</th>
                         <th>ʹ������</th>
                        <th name="dwBeginTime">�ʱ��</th>
                        <th>״̬</th>
                        <th width="25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
                $("#btnOK,#btnExport,#btnofflinecheck").button();
                
                $("#btnExport").click(function () {
                    var vDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwStatue1 = $("input[name='dwStatue1']:checked").val();
                    var dwStatue2 = $("input[name='dwStatue2']:checked").val();
                    $.lhdialog({
                        title: '����',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/outPortActitityPlan.aspx?op=set&vDate=' + vDate + '&dwStatue1=' + dwStatue1 + '&dwStatue2=' + dwStatue2
                    });
                });
                $("#<%=dwStartDate.ClientID%>").datepicker();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="checkActivityPlan" href="#" title="���"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/icon_s/15.png"/></a>\
<a class="getSeatMember" href="#" title="�鿴��λ�������"><img src="../../../themes/iconpage/mangerlist.png"/></a>\
 <a class="getGroupMember" href="#" title="�鿴������Ա"><img src="../../../themes/icon_s/16.png"/></a>\
<a class="offlinecheck" href="#" title="����ǩ��"><img src="../../../themes/icon_s/16.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTD1").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
<a class="getSeatMember" href="#" title="�鿴��λ�������"><img src="../../../themes/iconpage/mangerlist.png"/></a>\
 <a class="getGroupMember" href="#" title="�鿴������Ա"><img src="../../../themes/icon_s/16.png"/></a>\
<a class="offlinecheck" href="#" title="����ǩ��"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewActitityPlan.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".checkActivityPlan").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkActivityPlan.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".getSeatMember").click(function () {
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�鿴��λ�������',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getSeatMember.aspx?op=set&id=' + dwid
                    });
                });
                $(".offlinecheck").click(function () {
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����ǩ��',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/offlinecheck.aspx?op=set&planid='+dwid
                    });
                });
                
                $(".getGroupMember").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�鿴������Ա',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getactivitymember.aspx?op=set&id=' + dwDevID + '&dwID=' + dwDevID+"&activityid="+dwid
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '��ʾ', 1, function () { });
            });
                $(".FixBtn").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewActitityPlan.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
            
        </script>
    </form>
</asp:Content>
