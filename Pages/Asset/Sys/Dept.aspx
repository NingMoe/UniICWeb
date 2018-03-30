<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Dept.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��λ����</h2>
        <div class="toolbar">
               <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="Dept.aspx?" id="dwCodeType5">��λ����</a>
                    
                    <a href="CodeTable.aspx?dwCodeType=4" id="dwCodeType4">ѧ�ƹ���</a>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <!--<th name="szDeptSN">��λ����</th>-->
                        <th name="szName">��λ����</th>
                        <th name="szMemo">��д</th>
                        <th style="width:25px">����</th>
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

                $(".ListTbl").UniTable({ HeaderIndex: false });
                var tabl = $(".UniTab").UniTab();

                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '�½��γ�',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewCourse.aspx?op=new'
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setdept.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
        });
        </script>
    </form>
</asp:Content>
