<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Control.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>����̨����</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnResvRule">�½�����̨</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th name="dwConsoleSN">����̨���</th><th name="szConsoleName">����</th><th name="szIP">IP��ַ</th><th name="dwOpenTime">����ʱ��</th><th>����</th><th>����</th><th  name="dwStatus">״̬</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>    
           <uc1:PageCtrl runat="server" ID="PageCtrl"/>   
    </div>
    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '�½�����̨',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewControl.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {             
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸Ŀ���̨',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetControl.aspx?op=set&dwID=' + dwID
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