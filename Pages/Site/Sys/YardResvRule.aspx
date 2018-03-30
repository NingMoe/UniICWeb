<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="YardResvRule.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��������</h2>
        <div class="toolbar">
           <div class="tb_info">
              <div class="UniTab" id="tabl">
                    <a id="YardActivity" href="YardActivity.aspx">����͹���</a><a id="YardResvRule"  href="YardResvRule.aspx">��������</a>
            </div>
        </div>
            <div>
                <input type="hidden" id="extValue" name="extValue" />
            </div>
        <div class="FixBtn"><a id="btnResvRule">�½���������</a></div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRuleName">��������</th>
                        <th name="dwIdent">����</th>
                        <th><%=ConfigConst.GCKindName %></th>
                        <th>ԤԼʱ��</th>
                        <th>��ԤԼʱ�䷶Χ</th>
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
                $(".ListTbl").UniTable();
                $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    var extValue = $("#extValue").val();
                    if (extValue == null || extValue == "") {
                        $.lhdialog({
                            title: '�½�',
                            width: '920px',
                            height: '700px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewYardResvRule.aspx?op=new'
                        });
                    }
                    else {
                        $.lhdialog({
                            title: '�½�',
                            width: '920px',
                            height: '700px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewYardResvRule.aspx?op=new&extValue=' + extValue
                        });
                    }
                  
                });
                $(".CopyResvRule").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���ƹ���������豸',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CopyResvRule.aspx?op=set&dwID=' + dwLabID
                    });
                });
                
                $(".setResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '920px',
                        height: '700px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewYardResvRule.aspx?op=set&dwID=' + dwLabID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
            });
            //$(".ListTbl").UniTable();
        });
        </script>
    </form>
</asp:Content>