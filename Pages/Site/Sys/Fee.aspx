<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Fee.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>���ù���</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewLab">�½��շѱ�׼</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwFeeSN">���</th>
                        <th name="szFeeName">����</th>
                         <th name="dwIdent">���</th>
                        <th name="dwPriority">���ȼ�</th>
                        <th name="dwPurpose">ԤԼ��;</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a class="setFeedetail" title="�����շ���ϸ"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\<%if(ConfigConst.GCDebug==1){%><a class="CopyFee"  href="#" title="���Ƹ������豸"><img src="../../../themes/iconpage/edit.png"/></a>\<%}%> </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".CopyFee").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���Ƹ������豸',
                        width: '660px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/COPYFEE.aspx?op=set&dwID=' + dwID
                    });
                }); 
                $(".setLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetFee.aspx?op=set&dwID=' + dwLabID
                    });
                });
                $(".setFeedetail").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�����շ���ϸ',
                        width: '750px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetFeeDetailList.aspx?op=set&dwID=' + dwLabID
                    });
                });
                $(".InfoLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����',
                        width: '760px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwLabID + "&type=LabInfo"
                    });
                });
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
            });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '750px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewFee.aspx?op=new'
                });
            });
            //$(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
