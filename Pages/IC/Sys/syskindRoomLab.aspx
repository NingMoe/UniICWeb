<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="syskindRoomLab.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom%>����</h2>
        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">
                    <a href="syskindRoom.aspx" id="syskindRoom"><%=ConfigConst.GCSysKindRoom%>����</a>
                    <a href="syskindRoomLab.aspx" id="syskindRoomLab"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCLabName%></a>
                    <a href="DevKindRoom.aspx" id="DevKindRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCKindName %></a>
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="DevClassRoom.aspx" id="DevClassRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleroom.aspx?kind=1024" id="resvrule">ԤԼ����</a>
                      <a href="SysKindRoomSeat.aspx" id="SysKindRoomSeat">���λ����</a>
                </div>
            </div>
            <div class="FixBtn"><a id="btnNewLab">�½�����</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLabSN">���</th>
                        <th name="szLabName"><%=ConfigConst.GCLabName%>����</th>
                        <th>����<%=ConfigConst.GCDeptName %></th>
                        <th width="25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
          <!--  <uc1:PageCtrl runat="server" ID="PageCtrl" />-->
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
                    content: 'url:Dlg/SetLab.aspx?op=set&dwLabID=' + dwLabID
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
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewLab.aspx?op=new&dwLabClass=1'
                });
            });
            $(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>