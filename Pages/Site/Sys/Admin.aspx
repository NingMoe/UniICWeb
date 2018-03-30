<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>����Ա</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">�½�����Ա</a></div>
            <div class="tb_btn">
            </div>
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
                 <input type="hidden" id="dwAccno" name="dwAccno" />
            <table style="width:99%;margin-top:10px">
               <tr>
                   <th>����Աѧ��/����:</th>
                   <td>
                       <input type="text" name="szLogonName" id="szLogonName" />
                        </td>
              <th>����:</th>
                   <td>
                       <input type="text" name="szTrueName" id="szTrueName" />
                        </td>
                     <th>��������:</th>
                   <td>
                       <input type="text" name="szDeptName" id="szDeptName" />
                        </td>

                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLogonName">ѧ����</th>
                        <th name="szTrueName">����</th>
                        <th>����Ա����</th>
                        <th>����Ա����</th>
                        <th>���ڲ���</th>
                        <th>��ϵ��ʽ</th>
                        <th>��ע</th>
                        <th width="25px">����</th>
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
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a href="#" class="adminCopy" title="����Ա����"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a href="#" class="admingetroom" title="�鿴������"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '�½�����Ա',
                        width: '750px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAdmin.aspx?op=new'
                    });
                });
                
            $(".adminCopy").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���ƹ���Ա',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/admincopy.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸Ĺ���Ա',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetAdmin.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".admingetroom").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�鿴������',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getadminroom.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
            //$(".ListTbl").UniTable();
        });
        </script>
    </form>
</asp:Content>
