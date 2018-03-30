<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Stockaking.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�̵�ƻ�</h2>
          <div class="tb_info" style="margin-bottom:15px">
            <div class="UniTab" id="tabl">
                <a href="stockaking.aspx" id="stockaking">�̵�ƻ�</a>
                <a href="stockakingDetail.aspx" id="stockakingDetail">�̵���ϸ</a>
            </div>
              </div>
        <div>
            <div class="FixBtn"><a id="btnStockings">�½��̵�ƻ�</a></div>
         
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th>�̵㿪ʼ����:</th>
                   <td><input type="text" id="dwStartDate" runat="server" name="dwStartDate" /></td>
                   <th>�̵��������:</th>
                   <td><input type="text" id="dwEndDate" runat="server" name="dwEndDate" /></td>
                    <th>�̵�״̬:</th>
                    <td><select id="dwSTStat" name="dwSTStat"><%=sz_Staues %></select></td>
                    <td><input type="submit" id="btn" value="��ѯ" style="width:80px" /></td>
                </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>�ƻ���������</th>
                        <th name="dwSTDate">�̵�����</th>
                        <th name="szKindName">�̵�����</th>
                        <th name="szRoomName">�̵㷿��</th>
                        <th name="dwMinUnitPrice">���۷�Χ(Ԫ)</th>
                        <th name="dwSTStat">״̬</th>
                        <th name="dwSTEndDate">�̵��������</th>
                        <th>����</th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="getStockDetail" title="�鿴�̵���ϸ"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="setLabBtn" title="�����̵����"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btn").button();
            $("#<%=dwEndDate.ClientID%>,#<%=dwStartDate.ClientID%>").datepicker({
            });
            $(".setLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ���̵��Ѿ�����?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=over&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
                $(".getStockDetail").click(function () {
                    var id =$(this).parents("tr").children().first().attr("data-id");
                    fdata = "dwSTID=" + id;
                    TabInJumpReload("stockakingDetail", fdata);
                });
            $(".delLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
            $("#btnStockings").click(function () {
                $.lhdialog({
                    title: '�½��̵�ƻ�',
                    width: '720px',
                    height: '500px',
                    lock: true,
                    content: 'url:Dlg/NewStocking.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
