<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WInRoomDevLab.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>������Դ����</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="WInRoomDev.aspx" id="WDevTeachingRoom">������Դ����</a>
                    <a href="WInRoomDevKind.aspx" id="WInRoomDevKind">������Դ���͹���</a>
                    <a href="WInRoomDevLab.aspx" id="WInRoomDevLab">������Դ¥�����</a>
                </div>
            </div>
            <div class="FixBtn">
                <a id="btnNew" class="btnClss">�½�¥��</a>
            </div>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 5px 0px">                
            </div>
        </div>
       <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLabSN">���</th>
                        <th name="szLabName">¥������</th>
                        <th>����<%=ConfigConst.GCDeptName %></th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/WSetLabInRoom.aspx?op=set&dwLabID=' + dwLabID
                    });
                });
                $(".setLabManager").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwMangGroupID = $(this).parents("tr").children().first().siblings().attr("data-manGroupID");
                    $.lhdialog({
                        title: '����<%=ConfigConst.GCLabName %>����Ա',
                    width: '660px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwMangGroupID
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
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '300px',
                        lock: true,
                        content: 'url:Dlg/WNewLabInRoom.aspx?op=new&dwLabClass=513'
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
    </form>
</asp:Content>
